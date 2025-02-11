using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public class Dungeons : MonoBehaviour
{
    // monsterID is saved in activeMonsters[0] and correlates to which index in monsterPositions is that specific monster //
    public static string dungeonType = "";
    public static int dungeonCount;
    public static int localDungeons;
    public static List<int[]> activeDungeons = new List<int[]>();
    public static List<Vector3> dungeonPositions = new List<Vector3>();
    public static int[] dungeonStats = new int[3];
    public static int dungeonID;

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
                                        dungeonPositions.Add(position);
                                        AddNewDungeon(dungeonCount, 1);
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
                                        dungeonPositions.Add(position);
                                        AddNewDungeon(dungeonCount, 1);
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
                                        dungeonPositions.Add(position);
                                        AddNewDungeon(dungeonCount, 1);
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
                                        dungeonPositions.Add(position);
                                        AddNewDungeon(dungeonCount, 1);
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

    public static void CreateDungeon()
    {
        int[] values = new int[4];
        int dungeon = 0;
        if (GameMain.currentBoard == 1)
        {
            dungeon = Random.Range(0, 2);
        }
        // Type, Health, Status //
        switch (dungeon)
        {
            case 0:
                // Imp
                values[0] = 0; values[1] = 10; values[2] = 1; values[3] = 1; break;
            case 1:
                // Basilisk
                values[0] = 1; values[1] = 7; values[2] = 1; values[3] = 3; break;
        }
        activeDungeons.Add(values);
    }

    public static void AddNewDungeon(int value, int monsters)
    {
        // ID correlates to the dungeon vector position in dungeonPositions
        dungeonStats[0] = value;
        // 1 = Imp, Basilisk
        dungeonStats[1] = monsters;
        // 0 = Ready, 1 = Ruined
        dungeonStats[2] = 0;
    }

    public static bool CheckForDungeons(Vector3 position)
    {
        bool dungeonPresent = false;
        foreach (Vector3 dungeonPosition in dungeonPositions) { if (dungeonPosition == position) { dungeonPresent = true; } }
        return dungeonPresent;
    }

    public static void FindCurrentDungeon(Vector3 position)
    {
        //
    }

    public static void RaidDungeon(Vector3 position)
    {
        //
    }
}