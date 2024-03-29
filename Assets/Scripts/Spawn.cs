﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject platform;
	public GameObject hook;
	public GameObject bomb;
	public GameObject shark;
	public GameObject rock;
	public GameObject fishBlade;
	public GameObject AnglerFish;
	public GameObject SpikePlatform;
	public GameObject Spike;
	public GameObject SpikeBox;
	public GameObject column;
	public GameObject columnDown;
	public GameObject TwoColumn;

	FishController fish;
	List<GameObject> objectsBase = new List<GameObject>();
    public List<GameObject> rockObjects = new List<GameObject>();
	public GameObject rockOne;
	public GameObject rockTwo;
	public GameObject rockFree;
	//int _point;

	void Start()
    {
		rockObjects.Add(rockOne);
		rockObjects.Add(rockTwo);
		rockObjects.Add(rockFree);

		fish = GameObject.FindWithTag("fish").GetComponent<FishController>();
		//_point = GManager.point;
		objectsBase.Add(platform);	//0
		objectsBase.Add(hook);		//1
		objectsBase.Add(bomb);		//2
		objectsBase.Add(shark);		//3
		objectsBase.Add(fishBlade);	//4
		objectsBase.Add(AnglerFish);//5
		objectsBase.Add(SpikePlatform);//6
		objectsBase.Add(Spike);     //7
		objectsBase.Add(SpikeBox);  //8
		objectsBase.Add(column);    //9
		objectsBase.Add(columnDown); //10
		objectsBase.Add(TwoColumn); //11
		StartCoroutine(PlatformSpawn());
	}

	/*private void FixedUpdate()
	{
		if (fish.gameOver == true)
		{
			StopCoroutine(PlatformSpawn());
		}
	}*/

	IEnumerator PlatformSpawn()
	{
		bool arr = true;
		while (arr == true)
		{
			GManager.point = 14;
			if (GManager.point >= 0 && GManager.point <= 4) // 1 Спавн 4-х Сетей. Вход point = 0; Выход point = 3; максимум point после спавна = 4;
			{
				for (int i = 0; i < 4; i++)
				{
					yield return new WaitForSeconds(6f);
					Instantiate(objectsBase[0], new Vector3(11, Random.Range(4, 10.8f), 0.294265f), Quaternion.identity);
				}
			}
			if (GManager.point >= 3 && GManager.point <= 7) // 2 Спавн 3-х Крючков. Вход point = 3; Выход point = 6; максимум point после спавна = 7 + 1 сеть;
			{
				for (int i = 0; i < 3; i++)
				{
					yield return new WaitForSeconds(3f);
					Instantiate(objectsBase[1], new Vector3(11, Random.Range(4, -3.62f), 0.294265f), Quaternion.identity);
				}
				yield return new WaitForSeconds(3f);
				Instantiate(objectsBase[0], new Vector3(11, Random.Range(4, 10.8f), 0.294265f), Quaternion.identity);
			}
			if (GManager.point >= 6 && GManager.point <= 13) // 3 Спавн 4-х Крючков. Вход point = 6; Выход point = 11; максимум point после спавна = 13; !!!!!!
			{
				print("Я зашёл в третий иф и начинаю спавн Крючков");
				for (int i = 0; i < 4; i++)
				{
					yield return new WaitForSeconds(3f);
					Instantiate(objectsBase[1], new Vector3(11, Random.Range(4, -3.62f), 0.294265f), Quaternion.identity);
				}
				yield return new WaitForSeconds(2f);
				Instantiate(objectsBase[0], new Vector3(11, Random.Range(4, 10.8f), 0.294265f), Quaternion.identity);
			}
			if (GManager.point >= 11 - 1 && GManager.point <= 16) // 4 Спавн 3-х бомбочек и 1 сеть. Вход point = 11; Выход point = 14; максимум point после спавна = ;
			{
				print("Я зашёл в четвертый иф и начинаю спавн бомб");
				for (int i = 0; i < 3; i++)
				{
					yield return new WaitForSeconds(2f);
					Instantiate(objectsBase[2], new Vector3(11, Random.Range(4, -3.62f), 0.294265f), Quaternion.identity);
				}
				yield return new WaitForSeconds(2f);
				Instantiate(objectsBase[0], new Vector3(11, Random.Range(4, 10.8f), 0.294265f), Quaternion.identity);
			}
			if (GManager.point >= 13 - 1 && GManager.point <= 19) //5 Спавн 1 акула и 4-х бомб + 1 сеть. Вход point = 14; Выход point = 20; максимум point после спавна = ;
			{
				print("Я зашёл в пятый иф и начинаю спавн акулы и бомб");
				yield return new WaitForSeconds(5.5f);
				Instantiate(objectsBase[3], new Vector3(-11.68f, fish.transform.position.y, 1), Quaternion.identity);
				for (int i = 0; i < 4; i++)
				{
					Instantiate(objectsBase[2], new Vector3(11, Random.Range(4, -3.62f), 0.294265f), Quaternion.identity);
					yield return new WaitForSeconds(2f);
				}
				yield return new WaitForSeconds(1f);
				Instantiate(objectsBase[0], new Vector3(11, Random.Range(4, 10.8f), 0.294265f), Quaternion.identity);
			}
			if (GManager.point >= 18 - 1 && GManager.point <= 23) //6 Спавн 4-х бомб и 1 сеть. Вход point = 20; Выход point = ; максимум point после спавна = ;
			{
				print("Я зашёл в шестой иф и начинаю спавн бомб");
				yield return new WaitForSeconds(0.5f);
				for (int i = 0; i < 4; i++)
				{
					if (GManager.gameOver == false)
					{
						Instantiate(objectsBase[2], new Vector3(11, Random.Range(4, -3.62f), 0.294265f), Quaternion.identity);
						yield return new WaitForSeconds(2f);
					}
				}
				yield return new WaitForSeconds(0.5f);
				Instantiate(objectsBase[0], new Vector3(11, Random.Range(4, 10.8f), 0.294265f), Quaternion.identity);
			}
			if (GManager.point >= 21 && GManager.point <= 26) // 7 Спавн СКАЛЫ. Вход point = ; Выход point = ; максимум point после спавна = ;
			{
				print("Я зашёл в седьмой иф и начинаю спавн скалы");
				yield return new WaitForSeconds(4f);
				Instantiate(rockObjects[Random.Range(0, 2)], new Vector3(13, -3.1f, 1), Quaternion.identity);
			}
			if (GManager.point >= 26 && GManager.point <= 32) // 8 Спавн 6 Крючков + 1 сеть. Вход point = ; Выход point = ; максимум point после спавна = ;
			{
				print("Я зашёл в Восьмой иф и начинаю спавн крючков");
				yield return new WaitForSeconds(25f);
				for (int i = 0; i < 6; i++)
				{
					Instantiate(objectsBase[1], new Vector3(11, Random.Range(4, -3.62f), 0.294265f), Quaternion.identity);
					yield return new WaitForSeconds(1.3f);
				}
				yield return new WaitForSeconds(1.5f);
				Instantiate(objectsBase[0], new Vector3(11, Random.Range(4, 10.8f), 0.294265f), Quaternion.identity);
			}
			if (GManager.point >= 28 && GManager.point <= 39) //9 Спавн 10 рыб мечей. Вход point = ; Выход point = ; максимум point после спавна = ;
			{
				print("Я зашёл в девятый иф и начинаю спавн рыба мечей");
				yield return new WaitForSeconds(4f);
				for (int i = 0; i < 10; i++)
				{
					Instantiate(objectsBase[4], new Vector3(11, Random.Range(4, -3.62f), 0), Quaternion.identity);
					yield return new WaitForSeconds(2.5f);
				}
			}
			if (GManager.point >= 34 && GManager.point <= 39) //10 Спавн СКАЛЫ. Вход point = ; Выход point = ; максимум point после спавна = ;
			{
				print("Я зашёл в десятый иф и начинаю спавн скалы");
				yield return new WaitForSeconds(2.5f);
				Instantiate(rockObjects[Random.Range(0, 2)], new Vector3(13, -3.1f, 1), Quaternion.identity);
			}
			if (GManager.point >= 39 && GManager.point <= 50) //11 1)Спавн 3 рыб удильщиков, 2)5 бомб, 3) 3 рыбы удилька и 3 бомбы. Вход point = ; Выход point = ; максимум point после спавна = ;
			{
				print("Я зашёл в одинадцатый иф и начинаю спавн бомб и рыб удильщиков");
				yield return new WaitForSeconds(3f);
				if (GManager.gameOver == false)
				{
					for (int i = 0; i < 3; i++)
					{
						if (GManager.gameOver == false)
						{
							Instantiate(objectsBase[5], new Vector3(11, Random.Range(4, -3.62f), 0), Quaternion.identity);
							yield return new WaitForSeconds(4f);
						}
					}
				}
				if (GManager.gameOver == false)
				{
					for (int i = 0; i < 5; i++)
					{
						if (GManager.gameOver == false)
						{
							Instantiate(objectsBase[2], new Vector3(11, Random.Range(4, -3.62f), 0.294265f), Quaternion.identity);
							yield return new WaitForSeconds(2f);
						}
					}
				}
				if (GManager.gameOver == false)
				{
					for (int i = 0; i < 3; i++)
					{
						if (GManager.gameOver == false)
						{
							yield return new WaitForSeconds(2.5f);
							Instantiate(objectsBase[5], new Vector3(11, Random.Range(4, -3.62f), 0), Quaternion.identity);
							yield return new WaitForSeconds(2f);
							Instantiate(objectsBase[2], new Vector3(11, Random.Range(4, -3.62f), 0.294265f), Quaternion.identity);
						}
					}
				}
			}
			if (GManager.point >= 50 && GManager.point <= 54) //11
			{
				if (GManager.gameOver == false)
				{
					yield return new WaitForSeconds(4f);
					Instantiate(objectsBase[6], new Vector3(11, -2.66f, 0), Quaternion.identity);
				}
				if (GManager.gameOver == false)
				{
					yield return new WaitForSeconds(2f);
					Instantiate(objectsBase[7], new Vector3(11, 4.65f, 0), Quaternion.identity);
				}
				if (GManager.gameOver == false)
				{
					yield return new WaitForSeconds(2f);
					Instantiate(objectsBase[6], new Vector3(11, 2.42f, 0), Quaternion.identity);
					Instantiate(objectsBase[7], new Vector3(10.28f, -4.9f, 0), Quaternion.identity);
				}
				if (GManager.gameOver == false)
				{
					yield return new WaitForSeconds(3f);
					Instantiate(objectsBase[6], new Vector3(11, -2.66f, 0), Quaternion.identity);
				}
				if (GManager.gameOver == false)
				{
					yield return new WaitForSeconds(2f);
					Instantiate(objectsBase[7], new Vector3(11, 5.21f, 0), Quaternion.identity);
				}
				if (GManager.gameOver == false)
				{
					yield return new WaitForSeconds(3f);
					Instantiate(objectsBase[6], new Vector3(11, -2.66f, 0), Quaternion.identity);
				}
				if (GManager.gameOver == false)
				{
					yield return new WaitForSeconds(2f);
					Instantiate(objectsBase[6], new Vector3(11, -2.66f, 0), Quaternion.identity);
				}
				if (GManager.gameOver == false)
				{
					yield return new WaitForSeconds(1.5f);
					Instantiate(objectsBase[7], new Vector3(10.28f, -4.9f, 0), Quaternion.identity);
					Instantiate(objectsBase[7], new Vector3(11, 5.21f, 0), Quaternion.identity);
				}
				if (GManager.gameOver == false)
				{
					yield return new WaitForSeconds(2f);
					Instantiate(objectsBase[6], new Vector3(11, -2.66f, 0), Quaternion.identity);
				}
				if (GManager.gameOver == false)
				{
					yield return new WaitForSeconds(1.5f);
					Instantiate(objectsBase[7], new Vector3(10.28f, -4.9f, 0), Quaternion.identity);
					Instantiate(objectsBase[7], new Vector3(11, 5.21f, 0), Quaternion.identity);
				}
				if (GManager.gameOver == false)
				{
					yield return new WaitForSeconds(2f);
					Instantiate(objectsBase[6], new Vector3(11, 2.42f, 0), Quaternion.identity);
				}
			}
			if (GManager.point >= 60 && GManager.point <= 64)
			{
				for (int i = 0; i < 2; i++)
				{
					yield return new WaitForSeconds(3.5f);
					Instantiate(objectsBase[8], new Vector3(11, 0.23f, 0), Quaternion.identity);
					yield return new WaitForSeconds(3.5f);
					Instantiate(objectsBase[8], new Vector3(11, -2.13f, 0), Quaternion.identity);
				}
			}
			if (GManager.point >= 62 && GManager.point <= 68)
			{
				yield return new WaitForSeconds(3.5f);
				Instantiate(objectsBase[8], new Vector3(11, 0.23f, 0), Quaternion.identity);
				yield return new WaitForSeconds(3.5f);
				Instantiate(objectsBase[8], new Vector3(11, 0.23f, 0), Quaternion.identity);
				yield return new WaitForSeconds(3.5f);
				Instantiate(objectsBase[8], new Vector3(11, -2.13f, 0), Quaternion.identity);
				yield return new WaitForSeconds(3.5f);
				Instantiate(objectsBase[8], new Vector3(11, 0.23f, 0), Quaternion.identity);
			}
			if (GManager.point >= 66 && GManager.point <= 74)
			{
				yield return new WaitForSeconds(0.5f);
				for (int i = 0; i < 2; i++)
				{
					yield return new WaitForSeconds(2.5f);
					Instantiate(objectsBase[6], new Vector3(11, -2.62f, 0), Quaternion.identity);
					Instantiate(objectsBase[7], new Vector3(11, 4.8f, 0), Quaternion.identity);
					yield return new WaitForSeconds(2.5f);
					Instantiate(objectsBase[6], new Vector3(11, 2.47f, 0), Quaternion.identity);
					Instantiate(objectsBase[7], new Vector3(11, -4.81f, 0), Quaternion.identity);
				}
			}
			if (GManager.point >= 72 && GManager.point <= 85)
			{
				for(int i = 0; i < 5; i++)
				{
					yield return new WaitForSeconds(2f);
					Instantiate(objectsBase[9], new Vector3(11, -5.54f, 0), Quaternion.identity);
				}
			}
			if (GManager.point >= 81 && GManager.point <= 95)
			{
				print("fds");
				yield return new WaitForSeconds(2f);
				Instantiate(objectsBase[9], new Vector3(11, -5.54f, 0), Quaternion.identity);
				for (int i = 0; i < 12; i++)
				{
					yield return new WaitForSeconds(2f);
					Instantiate(objectsBase[9], new Vector3(11, -5.54f, 0), Quaternion.identity);
					yield return new WaitForSeconds(2f);
					Instantiate(objectsBase[10], new Vector3(11, 6.3f, 0), Quaternion.identity);
				}
			}
			if (GManager.point >= 86 && GManager.point <= 95)
			{
				for (int i = 0; i <= 11; i++)
				{
					if (GManager.gameOver == false)
					{
						yield return new WaitForSeconds(1.5f);
						Instantiate(objectsBase[11], new Vector3(11, 1.773808f, 0), Quaternion.identity);
					}
				}
			}
			if (GManager.point >= 96 && GManager.point <= 100) 
			{
				yield return new WaitForSeconds(2f);
				Instantiate(objectsBase[3], new Vector3(-7.97f, fish.transform.position.y, 1), Quaternion.identity);
				yield return new WaitForSeconds(2.5f);
				Instantiate(rockObjects[Random.Range(0, 2)], new Vector3(13, -3.1f, 1), Quaternion.identity);
			}


			arr = false;
		}
    }

}
