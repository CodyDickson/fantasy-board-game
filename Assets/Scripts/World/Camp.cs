using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    public void SpawnActivePlayerAtCamp()
    {
        int random;
        random = Random.Range(1,3);
        switch (random)
        {
            case 1: World.boardPosition[0] = 1; World.boardPosition[1] = 2; break;
            case 2: World.boardPosition[0] = -2; World.boardPosition[1] = -2; break;
        }
    }
}
