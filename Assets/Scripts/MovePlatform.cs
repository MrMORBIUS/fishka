using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    Rigidbody2D rb;
	GameObject fish;
	GManager Gm;
	bool check = false;

    void Start()
    {
		fish = GameObject.FindWithTag("fish");
		Gm = GameObject.FindWithTag("Menedger").GetComponent<GManager>();
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(_MovePlatform());
    }
    
	IEnumerator _MovePlatform()
	{
		while (true)
		{
			yield return new WaitForSeconds(0);
			if (GManager.gameOver == false)						// Изменить иф, сделать так чтобы на то что геймОвер фолз, была проверка 1 раз
				rb.velocity = new Vector2(-GManager.speedObject, 0);			// дальше проверялось только на тру. (Сделать Везде).
			else if(GManager.gameOver == true)
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
			Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("fish"))
		{
			GManager.gameOver = true;
			GameObject.FindWithTag("Spawn").GetComponent<Spawn>().StopSpawn();  //Остановка спавна после смерти.
			collision.gameObject.GetComponent<FishController>().jumpForce = 0;
			collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
			collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		}
	}
}
