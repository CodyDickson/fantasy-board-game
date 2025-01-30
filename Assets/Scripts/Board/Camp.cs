using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    public static void CampSpawn()
    {
        int random = Random.Range(1, 5);
        switch (random)
        {
            case 1: BoardManager.currentUnitPosition[0] = 1; BoardManager.currentUnitPosition[1] = 2; break;
            case 2: BoardManager.currentUnitPosition[0] = -2; BoardManager.currentUnitPosition[1] = -2; break;
            case 3: BoardManager.currentUnitPosition[0] = -1; BoardManager.currentUnitPosition[1] = -2; break;
            case 4: BoardManager.currentUnitPosition[0] = -2; BoardManager.currentUnitPosition[1] = 2; break;
        }
    }

    public static void CampDesign()
    {
        Store.tilemaps[2].SetTile(new Vector3Int(0, 0), Store.objectTiles[0]);
    }

    public static void UpdateExitPositions(Vector3 position)
    {
        // when generating the board, saves the exit point from camp in a list
        BoardManager.exitPositions.Add(position);
    }
}
