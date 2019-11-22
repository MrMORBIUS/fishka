using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
	public static int point;
    public TextMesh textMesh;
	internal static bool gameOver = false;
	public GameObject hookMaxPosition;

	public void TextView()
    {
        textMesh.text = point.ToString();
    }
}
