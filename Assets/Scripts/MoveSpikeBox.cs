using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpikeBox : MonoBehaviour
{
	Rigidbody2D rb;
	float deadTime = 1f;
	GameObject fish;

	void Start()
	{
		fish = GameObject.FindWithTag("fish");
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		if (GManager.gameOver == false)
		{
			rb.velocity = new Vector2(-GManager.speedObject, 0);
		}
		if (transform.position.x <= -14f) Destroy(gameObject, deadTime);
		if (GManager.gameOver == true)
		{
			rb.velocity = new Vector2(0, 0);
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
