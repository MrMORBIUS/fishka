using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFishBlade : MonoBehaviour
{
	public float speed;
	float deadTime = 1f;
	Rigidbody2D rb;
	GameObject fish;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		fish = GameObject.FindWithTag("fish");
	}

	private void FixedUpdate()
	{
		if (GManager.gameOver == false)
		{
			rb.velocity = new Vector2(-speed, 0);
		}
		if (GManager.gameOver == true)
		{
			rb.velocity = new Vector2(0, 0);
			fish.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		}
		if (transform.position.x <= -15f)
		{
			Destroy(gameObject, deadTime);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("fish"))
		{
			collision.gameObject.GetComponent<FishController>().X = 0;
			collision.gameObject.GetComponent<FishController>().jumpForce = 0;
			collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
			GManager.gameOver = true;
		}
	}
}
