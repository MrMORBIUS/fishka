using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTwoColumn : MonoBehaviour
{
	Rigidbody2D rb;
	GameObject fish;
	public float speedY;
	public float maxPositionY;
	public float minPositionY;
	bool checkPositionY = false;		//проверка : где находиться объект на максимальной или минимальной позиции


	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		fish = GameObject.FindWithTag("fish");
		maxPositionY = Random.Range(-2.21f, -3.25f);
		minPositionY = Random.Range(-5.6f, -4.7f);
	}

	private void FixedUpdate()
	{
		if (GManager.gameOver == false)
		{
			if (checkPositionY == false)
			{
				MoveDown();
				if (transform.position.y <= minPositionY)
				{											
					checkPositionY = true;
				}
			}
			else if (checkPositionY == true)
			{
				MoveUp();
				if  (transform.position.y >= maxPositionY)
				{
					checkPositionY = false;
				}
			}	
		}
		else if (GManager.gameOver == true)
		{
			rb.velocity = new Vector2(0, 0);
		}
	}

	void MoveDown()
	{
		rb.velocity = new Vector2(-GManager.speedObject, -speedY);
	}

	void MoveUp()
	{
		rb.velocity = new Vector2(-GManager.speedObject, speedY);
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
