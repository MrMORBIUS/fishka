using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpikePlatform : MonoBehaviour
{
	Rigidbody2D rb;
	GameObject fish;
	GManager Gm;
	bool check;

	void Start()
	{
		Gm = GameObject.FindWithTag("Menedger").GetComponent<GManager>();
		fish = GameObject.FindWithTag("fish");
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		if (GManager.gameOver == false) { rb.velocity = new Vector2(-GManager.speedObject, 0); }
		else { rb.velocity = new Vector2(0, 0); }
		if (transform.position.x <= fish.transform.position.x && check == false) { GManager.point++; Gm.TextView(); check = true; }
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("fish"))
		{
			GManager.gameOver = true;
			//GameObject.FindWithTag("Spawn").GetComponent<Spawn>().StopSpawn();
			collision.gameObject.GetComponent<FishController>().jumpForce = 0;
			collision.gameObject.GetComponent<Rigidbody2D>().simulated = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "endScene")
		{
			Destroy(gameObject);
		}
	}
}
