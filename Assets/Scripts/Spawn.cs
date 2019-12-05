using System.Collections;
using System.Collections.Generic;
using UnityEngine;							//В КОНЦЕ ЗАКОМЕНТИРОВАТЬ ВСЕ!

public class Spawn : MonoBehaviour
{
	GameObject fish;
	public GameObject[] objectBase = new GameObject[15];
	int[] objectQueue = new int[] { 0, 0, 1, 0, 2, 0, 3, 2, 4, 1, 0, 5, 4, 6, 2, 6, 9, 10, 8, 9, 10, 9, 10, 9, 10, 9, 11, 7, 12, 13, 14, 3, 15 }; //Какой объект на очереди?
	int[] itemToSpawn = new int[] { 3, 4, 6, 1, 7, 1, 1, 10, 1, 10, 1, 10, 1, 3, 6, 3, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 3, 2, 5, 3, 9, 1, 1 }; //Сколько объектов нужно заспавнить.
	float[] timeForSpawn = new float[] { 5, 3, };         //Время для yield return new ...
				
	int j = 0;
	int use_j = 0;														//Написать координаты для появления объектов.				Сделано.
	int numberObjectQueue = 0;											//Переписать objectQueue and itemToSpawn.					Сделано.
	float positionX;													//Написать массив для yield.
	float positionY;                                                    //Написать увеличение speed для всех objectBase.
	int numberObjectBase = 0; // для доп for.
	int checkSpikeBox = 1;

	internal Coroutine SpawnCoroutine = null;

	public List<GameObject> rockObjects = new List<GameObject>();
	public GameObject rockOne;
	public GameObject rockTwo;
	public GameObject rockFree;

	void Start()
	{
		fish = GameObject.FindWithTag("fish");
		rockObjects.Add(rockOne);
		rockObjects.Add(rockTwo);
		rockObjects.Add(rockFree);
		SpawnCoroutine = StartCoroutine(PlatformSpawn());
	}

	internal IEnumerator PlatformSpawn()
	{
		if (GManager.gameOver == false)
		{
			for (int i = 0; i <= objectBase.Length; )
			{
				for (j = use_j; j < use_j + 1 ; j++)
				{
					for (int spawn = 0; spawn < itemToSpawn[j]; spawn++)
					{
						positionY = i;
						RandomRangePosition(ref positionX, ref positionY);
						if (i == 11)
						{
							Instantiate(objectBase[i], new Vector3(positionX, positionY, transform.position.z), Quaternion.identity);
							yield return new WaitForSeconds(2);
							positionY = i;
							RandomRangePosition(ref positionX, ref positionY);
							for (int spawn2 = 0; spawn2 < 1; spawn2++)
							{
								Instantiate(objectBase[i], new Vector3(positionX, positionY, transform.position.z), Quaternion.identity);
								yield return new WaitForSeconds(2);
							}
						}
						else if (i == 7 || i == 13)
						{
							if (i == 7) { numberObjectBase = i + 1; }
							else { numberObjectBase = i - 1; }
							Instantiate(objectBase[i], new Vector3(positionX, positionY, transform.position.z), Quaternion.identity);
							yield return new WaitForSeconds(2);
							positionY = numberObjectBase;
							RandomRangePosition(ref positionX, ref positionY);
							for (int spawn3 = 0; spawn3 < 1; spawn3++)
							{
								Instantiate(objectBase[numberObjectBase], new Vector3(positionX, positionY, transform.position.z), Quaternion.identity);
								yield return new WaitForSeconds(2);
							}
						}
						else
						{
							Instantiate(objectBase[i], new Vector3(positionX, positionY, transform.position.z), Quaternion.identity);
							yield return new WaitForSeconds(2);
						}
					}
				}
				use_j = j;
				numberObjectQueue++;
				i = objectQueue[numberObjectQueue];
			}
		}
	}

	internal void StopSpawn() //Остановка корутина спавна.
	{
		StopCoroutine(SpawnCoroutine);
	}

	float RandomRangePosition(ref float positionX, ref float positionY)
	{
		switch (positionY)
		{
			case 0:
				positionY = Random.Range(4, 10.8f);
				positionX = 11;
				return positionY;
			case 1:
				positionY = fish.transform.position.y;
				positionX = 11;
				return positionY;
			case 2:
				positionY = fish.transform.position.y;
				positionX = 11;
				return positionY;
			case 3:
				positionY = fish.transform.position.y;
				positionX = -11.7f;
				return positionY;
			case 4:
				positionY = -3.5f;
				positionX = 13;
				return positionY;
			case 5:
				positionY = transform.position.y;
				positionX = 11;
				return positionY;
			case 6:
				positionY = Random.Range(4, -3.62f);
				positionX = 11;
				return positionY;
			case 7:
				positionY = -3.1f;
				positionX = 11;
				return positionY;
			case 8:
				positionY = 2.2f;
				positionX = 11;
				return positionY;
			case 9:
				positionY = -3.1f;
				positionX = 11;
				return positionY;
			case 10:
				positionY = 4.7f;
				positionX = 11;
				return positionY;
			case 11:
				if (checkSpikeBox == 1)
				{
					positionY = 0.4f;
					positionX = 11;
					checkSpikeBox = 2;
					return positionY;
				}
				else if (checkSpikeBox == 2)
				{
					positionY = -2.7f;
					positionX = 11;
					checkSpikeBox = 1;
					return positionY;
				}
				break;
			case 12:
				positionY = -4.5f;
				positionX = 12;
				return positionY;
			case 13:
				positionY = 5.7f;
				positionX = 11;
				return positionY;
			case 14:
				positionY = 0;
				positionX = 11;
				return positionY;
			case 15:
				break;
		}
		return positionY;
	}
}
