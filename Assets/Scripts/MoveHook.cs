using System.Collections;
using System.Collections.Generic;
using UnityEngine;									

public class MoveHook : MonoBehaviour
{
	Rigidbody2D rb;
	public float speed;
	public float speedY;
	float maxPosition;				//Максимальная позиция при анимации Волнения.
	float minPosition;              //Минимальная позиция при анимации Волнения.
	bool finishPosition = false;    //Проверка на достижение позиции максимум/минимум.
	GameObject fish;
	Quaternion fishAngle;           //Угол поворота рыбки при подъеме крючком.
	bool gripTheHook = false;
	GManager Gm;

	private void Start()
	{
		Gm = GameObject.FindWithTag("Menedger").GetComponent<GManager>();
		fish = GameObject.FindWithTag("fish");
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(_MoveHook());
		maxPosition = transform.position.y + 0.3f;
		minPosition = transform.position.y - 0.3f;
	}

	private void OnCollisionStay2D(Collision2D collision)							
	{
		if (collision.gameObject.CompareTag("fish"))
		{
			GManager.gameOver = true;
			gripTheHook = true;
			Gm.hookMaxPosition.SetActive(true);
			collision.gameObject.GetComponent<FishController>().jumpForce = 0;
			collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
			rb.velocity = new Vector2(0, speedY);
			collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speedY);
			StartCoroutine(MoveFishDead());
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "endScene")
		{
			Destroy(gameObject);
		}
		if (collision.tag == "hookMaxPosition")
		{
			speedY = 0;
			rb.velocity = new Vector2(0, 0);
			fish.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		}
	}

	IEnumerator _MoveHook()
	{
		bool check = false;
		while (true)
		{
			yield return new WaitForSeconds(0);
			if (GManager.gameOver == false)
			{
				if (finishPosition == false)
				{
					rb.velocity = new Vector2(-speed, speedY);
					if (transform.position.y >= maxPosition)
					{
						finishPosition = true;
					}
				}
				else if (finishPosition == true)
				{
					rb.velocity = new Vector2(-speed, -speedY);
					if (transform.position.y <= minPosition)
					{
						finishPosition = false;
					}
				}
			}
			if (GManager.gameOver == true && gripTheHook == false)
			{
				rb.velocity = new Vector2(0, 0);
			}
			if (transform.position.x <= fish.transform.position.x && check == false)
			{
				GManager.point++;
				Gm.TextView();
				check = true;
			}
			else if (GManager.gameOver == true)
			{
				yield break;
			}
		}
	}

	IEnumerator MoveFishDead()
	{
		while (true)
		{
			yield return new WaitForSeconds(0f);
			fishAngle = Quaternion.Euler(0, 0, Mathf.Clamp(Time.time * 4, 0, 90));          //Доделать на балансе, подобрать числа для плавного увеличения угла.
			fish.transform.rotation = Quaternion.Slerp(transform.rotation, fishAngle, Time.deltaTime * 34);
			if (fish.transform.eulerAngles.z == 90f)
			{
				yield break;
			}
		}
	}
}
