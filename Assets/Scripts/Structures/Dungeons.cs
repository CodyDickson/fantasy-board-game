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
    public static Dictionary<Vector3, int> dungeonPositions = new Dictionary<Vector3, int>();

    public static void RaidDungeon()
    {
        //
    }

    public static void SpawnDungeons(int location)
    {
        Tilemap structures = Store.tilemaps[3];
        Tile dungeon = Store.dungeonTiles[0];
        Vector3 center = new Vector3(0, 0);
        Vector3 position = new Vector3(0, 0);
        center = BoardManager.CheckClockworkPosition(location);
        int random;
        dungeonCount = 0;
        if (GameMain.currentBoard == 1)
        {
            int monsterType = 0;
            int xSize = 6;
            int ySize = 6;
            List<Vector3> positionsToRemove = new List<Vector3>();
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
                            if (slot == positionCheckOne && dungeonCount < 20)
                            {
                                position = positionCheckOne;
                                random = Random.Range(1, 101);
                                if (random <= 10) {
                                    bool dungeonPresent = CheckForDungeons(position);
                                    if (!dungeonPresent)
                                    {
                                        structures.SetTile(new Vector3Int((int)position[0], (int)position[1]), dungeon);
                                        dungeonCount++;
                                        random = Random.Range(1,3);
                                        if (random == 1) { monsterType = 0; }
                                        if (random == 2) { monsterType = 1; }
                                        dungeonPositions.Add(position, monsterType);
                                        positionsToRemove.Add(position);
                                    }
                                }
                            }
                            if (slot == positionCheckTwo && dungeonCount < 20)
                            {
                                position = positionCheckTwo;
                                random = Random.Range(1, 101);
                                if (random <= 10)
                                {
                                    bool dungeonPresent = CheckForDungeons(position);
                                    if (!dungeonPresent)
                                    {
                                        structures.SetTile(new Vector3Int((int)position[0], (int)position[1]), dungeon);
                                        dungeonCount++;
                                        random = Random.Range(1, 3);
                                        if (random == 1) { monsterType = 0; }
                                        if (random == 2) { monsterType = 1; }
                                        dungeonPositions.Add(position, monsterType);
                                        positionsToRemove.Add(position);
                                    }
                                }
                            }
                            if (slot == positionCheckThree && dungeonCount < 20)
                            {
                                position = positionCheckThree;
                                random = Random.Range(1, 101);
                                if (random <= 10)
                                {
                                    bool dungeonPresent = CheckForDungeons(position);
                                    if (!dungeonPresent)
                                    {
                                        structures.SetTile(new Vector3Int((int)position[0], (int)position[1]), dungeon);
                                        dungeonCount++;
                                        random = Random.Range(1, 3);
                                        if (random == 1) { monsterType = 0; }
                                        if (random == 2) { monsterType = 1; }
                                        dungeonPositions.Add(position, monsterType);
                                        positionsToRemove.Add(position);
                                    }
                                }
                            }
                            if (slot == positionCheckFour && dungeonCount < 20)
                            {
                                position = positionCheckFour;
                                random = Random.Range(1, 101);
                                if (random <= 10)
                                {
                                    bool dungeonPresent = CheckForDungeons(position);
                                    if (!dungeonPresent)
                                    {
                                        structures.SetTile(new Vector3Int((int)position[0], (int)position[1]), dungeon);
                                        dungeonCount++;
                                        random = Random.Range(1, 3);
                                        if (random == 1) { monsterType = 0; }
                                        if (random == 2) { monsterType = 1; }
                                        dungeonPositions.Add(position, monsterType);
                                        positionsToRemove.Add(position);
                                    }
                                }
                            }
                        }
                        for (int i = 0; i < positionsToRemove.Count; i++)
                        {
                            BoardManager.RemoveEmptySlot(positionsToRemove[i]);
                        }
                    }
                }
            }
        }
    }

    public static bool CheckForDungeons(Vector3 position)
    {
        bool dungeonPresent = false;
        foreach (Vector3 dungeonPosition in dungeonPositions.Keys) { if (dungeonPosition == position) { dungeonPresent = true; } }
        return dungeonPresent;
    }
}