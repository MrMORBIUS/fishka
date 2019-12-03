using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMove : MonoBehaviour
{
	Rigidbody2D rb;
	public float speedUp;
	GameObject fish;
	float speedShark = 3f;
	Quaternion angleShark;
	bool _mouth = false; // Проверка на то, находится ли рыбка в пасти у акулы.
	Vector3 startPosition;
	Vector3 fishPosition;
	bool checkAttack = false;
	public bool sharkOnBorder = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		fish = GameObject.FindWithTag("fish");
		StartCoroutine(StartMove());
	}

	void MoveUp()
	{
		rb.velocity = new Vector2(0, speedUp);
		angleShark = Quaternion.Euler(0, 0, Mathf.Clamp(rb.velocity.y * 5, -60, 75));
		transform.rotation = Quaternion.Slerp(transform.rotation, angleShark, Time.deltaTime * 7);
	}

	void MoveDown()
	{
		rb.velocity = new Vector2(0, -speedUp);
		angleShark = Quaternion.Euler(0, 0, Mathf.Clamp(rb.velocity.y * 5, -60, 75));
		transform.rotation = Quaternion.Slerp(transform.rotation, angleShark, Time.deltaTime * 7);
	}

	void MoveAngelEndMove()
	{
		if (transform.position.y <= fish.transform.position.y)
		{
			angleShark = Quaternion.Euler(0, 0, Mathf.Clamp(Time.time * 0.5f, -60, 75));
			transform.rotation = Quaternion.Slerp(transform.rotation, angleShark, Time.deltaTime * 7);
		}
		else
		{
			angleShark = Quaternion.Euler(0, 0, Mathf.Clamp(Time.time * -0.7f, -60, 75));
			transform.rotation = Quaternion.Slerp(transform.rotation, angleShark, Time.deltaTime * 7);
		}
	}

	IEnumerator StartMove()
	{
		while (true)
		{
			yield return new WaitForSeconds(0);
			rb.velocity = new Vector2(speedShark, 0);
			if (transform.position.x >= -7.19f)
			{
				rb.velocity = new Vector2(0, 0);
				StartCoroutine(Attack()); 
				while (true)
				{
					yield return new WaitForSeconds(0);
					if (transform.position.y + 0.5f < fish.transform.position.y && checkAttack == false && sharkOnBorder == false)
					{
						MoveUp();
					}
					else if (transform.position.y - 0.4f > fish.transform.position.y && checkAttack == false && sharkOnBorder == false)
					{
						MoveDown();
					}
					else
					{
						rb.velocity = new Vector2(0, 0);
						transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * 5);
					}
					if (GManager.gameOver == true && _mouth == false || GManager.gameOver == true && _mouth == true)                   
					{
						yield return StartCoroutine(EndMove());
						yield break;
					}
				}
			}
		}
	}

	IEnumerator Attack()
	{
		for (int attack = 1; attack <= 3; attack++)
		{
			if (GManager.gameOver == false)
			{
				switch (attack)
				{
					case 1:
						yield return new WaitForSeconds(4);
						startPosition = transform.position;
						fishPosition = fish.transform.position;
						yield return StartCoroutine(SharkAttack(startPosition, fishPosition));
						break;
					case 2:
						yield return new WaitForSeconds(6);
						startPosition = transform.position;
						fishPosition = fish.transform.position;
						yield return StartCoroutine(SharkAttack(startPosition, fishPosition));
						break;
					case 3:
						yield return new WaitForSeconds(3);
						startPosition = transform.position;
						fishPosition = fish.transform.position;
						yield return StartCoroutine(SharkAttack(startPosition, fishPosition));
						break;
				}
			}
			else yield break;
		}
	}

	IEnumerator SharkAttack(Vector3 startPosition, Vector3 fishPosition)
	{
		while (true)
		{
			yield return new WaitForSeconds(0);
			checkAttack = true;
			transform.position = Vector3.MoveTowards(transform.position, fishPosition, 0.1f);
			if (transform.position.x >= fish.transform.position.x - 0.5f && _mouth == false && GManager.gameOver == false)
			{
				startPosition.y = transform.position.y;
				while (true)
				{ 
					yield return new WaitForSeconds(0);
					transform.position = Vector3.MoveTowards(transform.position, startPosition, 0.07f);
					if (transform.position.x <= startPosition.x || GManager.gameOver == true)
					{
						checkAttack = false;
						yield break;
					}
				}
			}
			else if (GManager.gameOver == true){ checkAttack = false; yield break;}
		}
	}

   IEnumerator EndMove()
   {
		if (GManager.gameOver == true && _mouth == false)
		{
			rb.velocity = new Vector2(0, 0);
			while (true)
			{
				yield return new WaitForSeconds(0);
				MoveAngelEndMove();
				transform.position = Vector3.MoveTowards(transform.position, fish.transform.position, 0.07f);
				if (_mouth == true)
				{
					rb.velocity = new Vector2(0, 0);
					yield break;
				}
			}
		}
		else yield break;
   }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "fish")
		{
			GManager.gameOver = true;
			_mouth = true;
			GameObject.FindWithTag("Spawn").GetComponent<Spawn>().StopSpawn();
			collision.gameObject.GetComponent<FishController>().jumpForce = 0;
			collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
			collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			rb.velocity = new Vector2(0, 0);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "minPosition" || collision.tag == "maxPosition")
			sharkOnBorder = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.tag == "minPosition" || collision.tag == "maxPosition")
			sharkOnBorder = false;
	}
}