using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public class Dungeons : MonoBehaviour
{
    public static string dungeonType = "";
    public static int dungeonCount;

    public static void SpawnDungeons(int location)
    {
        Tilemap structures = Store.tilemaps[3];
        Vector3 center = new Vector3(0, 0);
        center = BoardManager.CheckClockworkPosition(location);
        int random;
        dungeonCount = 0;
        if (GameMain.currentBoard == 1)
        {
            int xSize = 6;
            int ySize = 6;
            while (dungeonCount < 5)
            {
                for (int z = 0, y = 0; y <= ySize; y++)
                {
                    for (int x = 0; x <= xSize; x++, z++)
                    {
                        Vector3 positionCheckOne = new Vector3Int((int)center[0] + x, (int)center[1] + y);
                        Vector3 positionCheckTwo = new Vector3Int((int)center[0] - x, (int)center[1] + y);
                        Vector3 positionCheckThree = new Vector3Int((int)center[0] + x, (int)center[1] - y);
                        Vector3 positionCheckFour = new Vector3Int((int)center[0] - x, (int)center[1] - y);
                        foreach (Vector3 slot in BoardManager.emptyBoardSlots)
                        {
                            if (slot == positionCheckOne && dungeonCount < 5)
                            {
                                random = Random.Range(1, 101);
                                if (random <= 10) { structures.SetTile(new Vector3Int((int)positionCheckOne[0], (int)positionCheckOne[1]), Store.dungeonTiles[0]); dungeonCount++; }
                            }
                            if (slot == positionCheckTwo && dungeonCount < 5)
                            {
                                random = Random.Range(1, 101);
                                if (random <= 10) { structures.SetTile(new Vector3Int((int)positionCheckTwo[0], (int)positionCheckTwo[1]), Store.dungeonTiles[0]); dungeonCount++; }
                            }
                            if (slot == positionCheckThree && dungeonCount < 5)
                            {
                                random = Random.Range(1, 101);
                                if (random <= 10) { structures.SetTile(new Vector3Int((int)positionCheckThree[0], (int)positionCheckThree[1]), Store.dungeonTiles[0]); dungeonCount++; }
                            }
                            if (slot == positionCheckFour && dungeonCount < 5)
                            {
                                random = Random.Range(1, 101);
                                if (random <= 10) { structures.SetTile(new Vector3Int((int)positionCheckFour[0], (int)positionCheckFour[1]), Store.dungeonTiles[0]); dungeonCount++; }
                            }
                        }
                    }
                }
            }
        }
    }
}