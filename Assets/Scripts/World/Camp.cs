using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    public static void SpawnPlayerInCamp()
    {
        switch(GameMain.currentMap)
        {
            case 1: GrasslandsCamp(); break;
            case 2: GraveyardCamp(); break;
            case 3: VolcanoCamp(); break;
            case 4: MachineCamp(); break;
        }
    }

    public static void GrasslandsCamp()
    {
        int random;
        random = Random.Range(1, 5);
        switch (random)
        {
            case 1: World.boardPosition[0] = 1; World.boardPosition[1] = 2; break;
            case 2: World.boardPosition[0] = -2; World.boardPosition[1] = -2; break;
            case 3: World.boardPosition[0] = -1; World.boardPosition[1] = -2; break;
            case 4: World.boardPosition[0] = -2; World.boardPosition[1] = 2; break;
        }
    }

    public static void GraveyardCamp()
    {
        int random;
        random = Random.Range(1, 3);
        switch (random)
        {
            case 1: World.boardPosition[0] = 1; World.boardPosition[1] = 2; break;
            case 2: World.boardPosition[0] = -2; World.boardPosition[1] = -2; break;
        }
    }

    public static void VolcanoCamp()
    {
        int random;
        random = Random.Range(1, 3);
        switch (random)
        {
            case 1: World.boardPosition[0] = 1; World.boardPosition[1] = 2; break;
            case 2: World.boardPosition[0] = -2; World.boardPosition[1] = -2; break;
        }
    }

    public static void MachineCamp()
    {
        int random;
        random = Random.Range(1, 3);
        switch (random)
        {
            case 1: World.boardPosition[0] = 1; World.boardPosition[1] = 2; break;
            case 2: World.boardPosition[0] = -2; World.boardPosition[1] = -2; break;
        }
    }
}