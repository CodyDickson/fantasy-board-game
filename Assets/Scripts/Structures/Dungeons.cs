using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;

public class Dungeons : MonoBehaviour
{
    public static string dungeonType = "";

    public static void SpawnDungeons()
    {
        /*foreach (Vector3 listVector in BoardManager.)
        {
            Vector3 vector3 = listVector;
            World.currentUnitPosition = listVector;
            World.CheckForLocalBoardPositions();
            int random;
            if (!World.northPositionAvailable)
            {
                random = Random.Range(1, 10);
                if (random == 1)
                {
                    random = Random.Range(1, 3);
                    if (random == 1)
                    {
                        vector3[1] += 1;
                        World.boardImpDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                    else if (random == 2)
                    {
                        vector3[1] += 1;
                        World.boardBasiliskDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                }
            }
            if (!World.eastPositionAvailable)
            {
                random = Random.Range(1, 10);
                if (random == 1)
                {
                    random = Random.Range(1, 3);
                    if (random == 1)
                    {
                        vector3[0] += 1;
                        World.boardImpDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                    else if (random == 2)
                    {
                        vector3[0] += 1;
                        World.boardBasiliskDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                }
            }
            if (!World.southPositionAvailable)
            {
                random = Random.Range(1, 10);
                if (random == 1)
                {
                    random = Random.Range(1, 3);
                    if (random == 1)
                    {
                        vector3[1] -= 1;
                        World.boardImpDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                    else if (random == 2)
                    {
                        vector3[1] -= 1;
                        World.boardBasiliskDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                }
            }
            if (!World.westPositionAvailable)
            {
                random = Random.Range(1, 10);
                if (random == 1)
                {
                    random = Random.Range(1, 3);
                    if (random == 1)
                    {
                        vector3[0] -= 1;
                        World.boardImpDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                    else if (random == 2)
                    {
                        vector3[0] -= 1;
                        World.boardBasiliskDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                }
            }
        }
        foreach (Vector3 impDungeon in World.boardImpDungeonPositions)
        {
            tilemapStructures.SetTile(new Vector3Int((int)impDungeon[0], (int)impDungeon[1]), Store.dungeonTiles[0]);
        }
        foreach (Vector3 basiliskDungeon in World.boardBasiliskDungeonPositions)
        {
            tilemapStructures.SetTile(new Vector3Int((int)basiliskDungeon[0], (int)basiliskDungeon[1]), Store.dungeonTiles[1]);
        }
        continueTurnProgression = true;*/
    }
}