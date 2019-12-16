using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFishBlade : MonoBehaviour
{
	Rigidbody2D rb;
	GameObject fish;
	public float speedJerk;
	Vector3 positionJerk;
	bool near;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		fish = GameObject.FindWithTag("fish");
	}

	private void FixedUpdate()
	{
		rb.velocity = new Vector2(-GManager.speedObject, 0);
		if (near)
		{
			transform.position = Vector3.MoveTowards(transform.position, positionJerk, speedJerk);
			if(transform.position.x <= positionJerk.x) { near = false; }
		}
	}

	void OutRunAnglerFish()
	{
		transform.position = Vector3.MoveTowards(transform.position, positionJerk, speedJerk);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("fish"))
		{
			positionJerk = new Vector3(transform.position.x - 3.5f, transform.position.y, transform.position.z);
			if (transform.position.y > fish.transform.position.y)
				fish.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
			else
				fish.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3);
			near = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "endScene")
			Destroy(gameObject);
	}
}
