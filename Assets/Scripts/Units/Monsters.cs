using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Monsters : MonoBehaviour
{
    // Tracking //
    public static Dictionary<Vector3, int> monsterPositions = new Dictionary<Vector3, int>();
    public static Dictionary<int, int> monsterStats = new Dictionary<int, int>();

    public static void SpawnMonster()
    {
        Debug.Log("Spawn Monsters");
        int player = GameMain.currentPlayer;
        int random = 0;
        Vector3 position = BoardManager.currentUnitPosition;
        Tilemap units = Store.tilemaps[4];
        int monsterChoice = 0;
        Tile monster;
        List<Vector3> possibleSpawns = new List<Vector3>();
        foreach (KeyValuePair<Vector3, int> dungeon in Dungeons.dungeonPositions)
        {
            BoardManager.currentUnitPosition = dungeon.Key;
            random = Random.Range(1,4);
            if (random == 1)
            {
                monsterChoice = dungeon.Value;
                monster = Store.monsterTiles[monsterChoice];
                BoardManager.CheckForLocalBoardPositions();
                if (BoardManager.northPositionAvailable)
                {
                    bool positionClear = CheckMonsterPositions(BoardManager.northPosition);
                    if (positionClear) { possibleSpawns.Add(BoardManager.northPosition); }
                }
                if (BoardManager.eastPositionAvailable)
                {
                    bool positionClear = CheckMonsterPositions(BoardManager.eastPosition);
                    if (positionClear) { possibleSpawns.Add(BoardManager.eastPosition); }
                }
                if (BoardManager.southPositionAvailable)
                {
                    bool positionClear = CheckMonsterPositions(BoardManager.southPosition);
                    if (positionClear) { possibleSpawns.Add(BoardManager.southPosition); }
                }
                if (BoardManager.westPositionAvailable)
                {
                    bool positionClear = CheckMonsterPositions(BoardManager.westPosition);
                    if (positionClear) { possibleSpawns.Add(BoardManager.westPosition); }
                }
                if (possibleSpawns.Count == 1)
                {
                    position = possibleSpawns[0];
                }
                else
                {
                    bool choiceMade = false;
                    while (!choiceMade)
                    {
                        for (int i = 0; i < possibleSpawns.Count; i++)
                        {
                            random = Random.Range(1, 3);
                            if (random == 1 && !choiceMade)
                            {
                                position = possibleSpawns[i];
                                choiceMade = true;
                            }
                        }
                    }
                }
                units.SetTile(new Vector3Int((int)position[0], (int)position[1]), monster);
                monsterPositions.Add(position, monsterChoice);
            }    
        }
        TurnManager.continueTurnProgression = true;
    }

    public static bool CheckMonsterPositions(Vector3 positionToCheck)
    {
        bool positionClear = true;
        foreach (Vector3 monsterPositions in monsterPositions.Keys)
        {
            if (monsterPositions == positionToCheck)
            {
                positionClear = false;
            }
        }
        return positionClear;
    }
}