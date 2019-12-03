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
		StartMoveRock();
	}

	void StartMoveRock()
	{
		if (GManager.gameOver == false)
			rb.velocity = new Vector2(-GManager.speedObject, 0);
	}

	void StopMoveRock()
	{
		rb.velocity = new Vector2(0, 0);
		rb.simulated = false;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "endScene")
			Destroy(gameObject, 20);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("fish"))
		{
			GManager.gameOver = true;
			GameObject.FindWithTag("Spawn").GetComponent<Spawn>().StopSpawn();
			StopMoveRock();
			collision.gameObject.GetComponent<FishController>().X = 0;
			collision.gameObject.GetComponent<FishController>().jumpForce = 0;
		}
	}
}
