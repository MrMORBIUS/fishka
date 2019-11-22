using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
     Rigidbody2D rb;
	 public float jumpForce;			  // сила прыжка.
     public float X;					  // скорость по оси x.

	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(FishTurn());
	}

    void FixedUpdate()
    {
		if (Input.GetKeyDown(KeyCode.Space) && GManager.gameOver == false)
		{
			rb.velocity = new Vector2(X, jumpForce);
		}
	}
	
	IEnumerator FishTurn()
	{
		while (true)
		{
			yield return new WaitForSeconds(0);
			Quaternion fish = Quaternion.Euler(0, 0, Mathf.Clamp(rb.velocity.y * 7.5f, -15, 35));
			transform.rotation = Quaternion.Slerp(transform.rotation, fish, Time.deltaTime * 4);
			if (GManager.gameOver == true)
				yield break;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "maxPosition" && GManager.gameOver == false)
			rb.velocity = new Vector2(0, -2);
		if (collision.tag == "minPosition")
			rb.velocity = new Vector2(0, 3);
	}
}
