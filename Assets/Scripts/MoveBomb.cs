using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBomb : MonoBehaviour
{
	Rigidbody2D rb;
	bool check = false;
	GameObject fish;
	GManager Gm;

	private void Start()
	{
		Gm = GameObject.FindWithTag("Menedger").GetComponent<GManager>();
		fish = GameObject.FindWithTag("fish");
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(_MoveBomb());
	}

	IEnumerator _MoveBomb()
	{
		rb.velocity = new Vector2(-GManager.speedObject, 0);
		while (true)
		{
			yield return new WaitForSeconds(0);
			if (GManager.gameOver == true)
			{
				rb.velocity = new Vector2(0, 0);
				yield break;
			}
			if (transform.position.x <= fish.transform.position.x && check == false)
			{
				GManager.point++;
				Gm.TextView();
				check = true;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "endScene")
		{
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("fish"))
		{
			collision.gameObject.GetComponent<FishController>().jumpForce = 0;
			collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
			rb.velocity = new Vector2(0, 0);
			collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			GManager.gameOver = true;
			GameObject.FindWithTag("Spawn").GetComponent<Spawn>().StopSpawn();
		}
	}
}
