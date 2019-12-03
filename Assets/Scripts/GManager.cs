using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
	public static int point;
    public TextMesh textMesh;
	internal static bool gameOver = false;
	internal static float speedObject = 2.5f;
	public GameObject hookMaxPosition;

	public void TextView()
    {
        textMesh.text = point.ToString();
    }
}
