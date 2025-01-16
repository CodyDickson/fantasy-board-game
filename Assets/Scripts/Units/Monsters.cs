using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Monsters : MonoBehaviour
{
    // monsterID is saved in activeMonsters[0] and correlates to which index in monsterPositions is that specific monster //
    public static List<int[]> activeMonsters;
    public static List<Vector3> monsterPositions;
    public static Vector3 monsterPosition;
    public static Tilemap units;
    public static int monsterID;


    private void Start()
    {
        units = Store.tilemaps[4];
    }

    public static void SpawnMonsters()
    {
        Debug.Log("Spawn Monsters");
        int player = GameMain.currentPlayer;
        int random = 0;
        Vector3 position = BoardManager.currentUnitPosition;
        Tile monster;
        List<Vector3> possibleSpawns = new List<Vector3>();
        foreach (Vector3 dungeon in Dungeons.dungeonPositions)
        {
            random = Random.Range(1, 101);
            if (random <= 25)
            {
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
                if (monsterPositions.Contains(position)) { continue; }
                else { monsterPositions.Add(position); CreateMonster(); }
            }    
        }
        MonsterMovement.UpdateAvatars();
        TurnManager.continueTurnProgression = true;
    }

    public static void CreateMonster()
    {
        int[] values = new int[4];
        int monster = 0;
        // Type, Health, Lives, Combat Dice //
        switch (monster)
        {
            case 0:
                // Imp
                values[0] = 0; values[1] = 10; values[2] = 1; values[3] = 1; break;
            case 1:
                // Basilisk
                values[0] = 1; values[1] = 7; values[2] = 1; values[3] = 1; break;
        }
        activeMonsters.Add(values);
    }

    public static bool CheckMonsterPositions(Vector3 positionToCheck)
    {
        bool positionClear = true;
        foreach (Vector3 monsterPositions in monsterPositions)
        {
            if (monsterPositions == positionToCheck)
            {
                positionClear = false;
            }
        }
        return positionClear;
    }
}