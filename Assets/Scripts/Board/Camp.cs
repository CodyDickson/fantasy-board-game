using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    public static List<Vector3> exitPositions = new List<Vector3>();

    public static void UpdateExitPositions(Vector3 position)
    {
        // when generating the board, saves the exit point from camp in a list
        exitPositions.Add(position);
    }
}
