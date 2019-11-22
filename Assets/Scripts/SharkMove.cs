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
	bool checkaAttack = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		fish = GameObject.FindWithTag("fish");
		StartCoroutine(StartMove());
	}

	void MoveUp()
	{
		rb.velocity = new Vector2(0, speedUp);
		angleShark = Quaternion.Euler(0, 0, Mathf.Clamp(rb.velocity.y * 4, -60, 75));
		transform.rotation = Quaternion.Slerp(transform.rotation, angleShark, Time.deltaTime * 7);
	}

	void MoveDown()
	{
		rb.velocity = new Vector2(0, -speedUp);
		angleShark = Quaternion.Euler(0, 0, Mathf.Clamp(rb.velocity.y * 4, -60, 75));
		transform.rotation = Quaternion.Slerp(transform.rotation, angleShark, Time.deltaTime * 7);
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
					if (transform.position.y + 0.5f < fish.transform.position.y && checkaAttack == false)
					{
						MoveUp();
					}
					else if (transform.position.y - 0.5f > fish.transform.position.y && checkaAttack == false)
					{
						MoveDown();
					}
					else
						transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * 5);
					if (GManager.gameOver == true && _mouth == false)                   
					{
						StartCoroutine(EndMove());
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
			checkaAttack = true;
			transform.position = Vector3.MoveTowards(transform.position, fishPosition, 0.1f);
			if (transform.position.x >= fish.transform.position.x - 0.5f && _mouth == false && GManager.gameOver == false)
			{
				while (true)
				{
					transform.position = Vector3.MoveTowards(transform.position, startPosition, Time.deltaTime);
					if (transform.position.x <= startPosition.x)
					{
						checkaAttack = false;
						yield break;
					}
				}
			}
			if (GManager.gameOver == true){ checkaAttack = false; yield break;}
		}
	}

   IEnumerator EndMove()
	{
		while (true)
		{
			yield return new WaitForSeconds(0);
			rb.velocity = new Vector2(0, 0);
			transform.position = Vector2.Lerp(transform.position, fish.transform.position, Time.deltaTime);
			if (_mouth == true)
			{
				rb.velocity = new Vector2(0, 0);
				yield break;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "fish")
		{
			GManager.gameOver = true;
			_mouth = true;
			collision.gameObject.GetComponent<FishController>().jumpForce = 0;
			collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
			collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		}
		if (collision.tag == "minPosition")
		{
			
		}
	}

}
