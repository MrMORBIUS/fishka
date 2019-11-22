using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnglerFish : MonoBehaviour
{
	GameObject fish;
	Rigidbody2D rb;
	public float speed = 2f;
	float randomDistanceAttack;
	public Vector3 fixedPositionFish;
	public float speedAttack;
	public float speedY;
	bool check = false;
	float deadTime = 1f;



	private void Start()
	{
		randomDistanceAttack = Random.Range(4.08f, 8.8f);
		fish = GameObject.FindWithTag("fish");
		rb = GetComponent<Rigidbody2D>();
		MoveAnglerX();

	}

	private void FixedUpdate()
	{
		if (check == false)
		{
			MoveAnglerX();
		}
		if (Vector3.Distance(fish.transform.position, transform.position) <= randomDistanceAttack && check == false)
		{
			print("Hi");
			check = true;
			//StartCoroutine(Attack());
		}
		if (transform.position.x <= -11f)
		{
			Destroy(gameObject, deadTime);
		}
	}

	void MoveAnglerX()
	{
		rb.velocity = new Vector2(-speed, 0);
	}

	void MoveAnglerDown()
	{
		rb.velocity = new Vector2(-speed, -speedY);
	}

	IEnumerator Attack()
	{
		fixedPositionFish = fish.transform.position;
		if(fixedPositionFish.y > transform.position.y)
		{
			print("Я в первом ифе");
			while (transform.position.x > fixedPositionFish.x && transform.position.y < fixedPositionFish.y)
			{
				print("Я в первом вайле");
				Vector2.MoveTowards(transform.position, fixedPositionFish, -speedAttack);
			}
		}
		else if (fixedPositionFish.y < transform.position.y)
		{
			print("Я во втором ифе");
			while (transform.position.x > fixedPositionFish.x && transform.position.y > fixedPositionFish.y)
			{
				print("Я во втором вайле");
				Vector2.MoveTowards(transform.position, fixedPositionFish, -speedAttack);
			}
		}
		else if (fixedPositionFish.y == transform.position.y)
		{
			print("Я в третьем ифе");
			while (transform.position.x > fixedPositionFish.x && transform.position.y == fixedPositionFish.y)
			{
				print("Я в третьем вайле");
				Vector2.MoveTowards(transform.position, fixedPositionFish, -speedAttack);
			}
		}
		MoveAnglerX();
		yield return new WaitForSeconds(1f);
		MoveAnglerDown();
		yield break;						//Устранить баг!
	}
}
