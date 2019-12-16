using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class columnDown : MonoBehaviour
{
	Rigidbody2D rb;
	GameObject fish;
	public float moveDown;
	public float randPositionY;
	bool finish = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		fish = GameObject.FindWithTag("fish");
		randPositionY = Random.Range(4f, 3f);
	}

	private void FixedUpdate()
	{
		if (GManager.gameOver == false)
		{
			if (finish == false)
			{
				MoveColDown();
				if (transform.position.y <= randPositionY)
					finish = true;
			}
			else if (finish == true)
			{
				MoveColUp();
				if (transform.position.y >= 6.3f)
					finish = false;
			}
		}
		else if (GManager.gameOver == true)
		{
			rb.velocity = new Vector2(0, 0);
		}
	}

	public void MoveColDown()
	{
		rb.velocity = new Vector2(-GManager.speedObject, -moveDown);
	}

	public void MoveColUp()
	{
		rb.velocity = new Vector2(-GManager.speedObject, moveDown);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("fish"))
		{
			GManager.gameOver = true;
			//GameObject.FindWithTag("Spawn").GetComponent<Spawn>().StopSpawn();
			collision.gameObject.GetComponent<Rigidbody2D>().simulated = false;
			collision.gameObject.GetComponent<FishController>().jumpForce = 0;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "endScene") { Destroy(gameObject); }
	}
}
