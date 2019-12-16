using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRock : MonoBehaviour
{
	Rigidbody2D rb;
	GameObject fish;


	private void Start()
	{
		fish = GameObject.FindWithTag("fish");
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (GManager.gameOver == false)
			rb.velocity = new Vector2(-GManager.speedObject, 0);
	}
	
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "endScene")
			Destroy(gameObject, 18);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("fish"))
		{
			GManager.gameOver = true;
			GameObject.FindWithTag("Spawn").GetComponent<Spawn>().StopSpawn();
			collision.gameObject.GetComponent<FishController>().X = 0;
			collision.gameObject.GetComponent<FishController>().jumpForce = 0;
			rb.velocity = new Vector2(0, 0);
		}
	}
}
