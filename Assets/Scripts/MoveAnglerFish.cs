using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveAnglerFish : MonoBehaviour
{
    public float jumpSpeed;
    Transform fish;
	GameObject fishOne;
    Rigidbody2D rb;
    public float distanceAttack;
    bool attack;
    bool backAttack;
    Vector3 saveTarget;
    Vector3 finishPoint;
	float rotationValue = 8.5f;
	GManager Gm;


	private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		fishOne = GameObject.FindWithTag("fish");
		fish = GameObject.FindWithTag("fish").GetComponent<Transform>();
		Gm = GameObject.FindWithTag("Menedger").GetComponent<GManager>();
	}

	private void FixedUpdate()
	{
		if (attack) { Attack(); }
		else { Move(); } 
	}

    private void Attack()
    {
        if (backAttack == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, saveTarget, jumpSpeed);
			if(transform.position.y - 0.5f > saveTarget.y) { rotationValue = -8.5f; RotationAnglerFish(ref rotationValue); }
			else if(transform.position.y + 0.5f < saveTarget.y) { rotationValue = 8.5f; RotationAnglerFish(ref rotationValue); }
			if (transform.position == saveTarget) { backAttack = true; GManager.point++; Gm.TextView(); }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, finishPoint, jumpSpeed);
			if (transform.position.y - 0.5f > finishPoint.y) { rotationValue = -8.5f; RotationAnglerFish(ref rotationValue); }
			else if (transform.position.y + 0.5f < finishPoint.y) { rotationValue = 8.5f; RotationAnglerFish(ref rotationValue); }
			if (transform.position == finishPoint)
			{
				attack = false;
				transform.rotation = Quaternion.identity;
			}
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(-GManager.speedObject, 0);
		if (transform.position.x >= saveTarget.x + 1.5f || transform.position.x < saveTarget.x) { attack = false; }
		else if (Vector2.Distance(transform.position, fish.position) < distanceAttack && backAttack == false)
        {
            attack = true;
            saveTarget = fish.position;
            finishPoint = new Vector3(transform.position.x - ((transform.position.x - saveTarget.x) * 2), transform.position.y,transform.position.z);
            rb.velocity = new Vector2(0, 0);
        }
    }

	void RotationAnglerFish(ref float rotationValue)
	{
		Quaternion anglerFish = Quaternion.Euler(0, 0, Mathf.Clamp(transform.position.y * rotationValue, -25, 45));
		transform.rotation = Quaternion.Slerp(transform.rotation, anglerFish, Time.deltaTime * 5f);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("fish"))
		{
			//GManager.gameOver = true;
			//GameObject.FindWithTag("Spawn").GetComponent<Spawn>().StopSpawn();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "endScene")
			Destroy(gameObject);
	}
}
