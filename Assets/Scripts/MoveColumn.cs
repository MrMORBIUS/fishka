using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveColumn : MonoBehaviour
{
	Rigidbody2D rb;
	float deadTime = 1f;
	GameObject fish;
	public float moveUp;
	public float randPositionY;
	bool finish = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		fish = GameObject.FindWithTag("fish");
		randPositionY = Random.Range(-5f, 0.86f);
	}

	private void FixedUpdate()
	{
		if (GManager.gameOver == false)
		{
			if (finish == false)
			{
				MoveColUp();
				if (transform.position.y >= randPositionY)
					finish = true;
			}
			else if (finish == true)
			{
				MoveColDown();
				if (transform.position.y <= -5.54f)
					finish = false;
			}
		}
		else if (GManager.gameOver == true)
		{
			rb.velocity = new Vector2(0, 0);
		}
		if (transform.position.x <= -11f) Destroy(gameObject, deadTime);
	}

	public void MoveColUp()
	{
		rb.velocity = new Vector2(-GManager.speedObject, moveUp);
	}

	public void MoveColDown()
	{
		rb.velocity = new Vector2(-GManager.speedObject, -moveUp);
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
