using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTwoColumn : MonoBehaviour
{
	Rigidbody2D rb;
	GameObject fish;
	public float speed;
	public float speedY;
	float deadTime = 1f;
	public float maxPositionY;
	public float minPositionY;
	bool checkPositionY = false;		//проверка : где находиться объект на максимальной или минимальной позиции


	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		fish = GameObject.FindWithTag("fish");
		maxPositionY = Random.Range(1.36f, 2.4f);
		minPositionY = Random.Range(-1, 0.5f);
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
		if (GManager.gameOver == true)
		{
			rb.velocity = new Vector2(0, 0);
		}
		if (transform.position.x <= -11f)
		{
			Destroy(gameObject, deadTime);
		}
	}

	void MoveDown()
	{
		rb.velocity = new Vector2(-speed, -speedY);
	}

	void MoveUp()
	{
		rb.velocity = new Vector2(-speed, speedY);
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
