using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Monsters : MonoBehaviour
{
    // monsterID is saved in activeMonsters[0] and correlates to which index in monsterPositions is that specific monster //
    public static List<int[]> activeMonsters = new List<int[]>();
    public static List<Vector3> monsterPositions = new List<Vector3>();
    public static Vector3 monsterPosition;
    public static Tilemap units;
    public static int monsterID;

    // monsterID = position is [?] in monsterPositions and [?] in activeMonsters

    private void Start()
    {
        units = Store.tilemaps[4];
    }

    public static void SpawnMonsters()
    {
        Debug.Log("Spawn Monsters");
        int random = 0;
        Vector3 position = BoardManager.currentUnitPosition;
        List<Vector3> possibleSpawns = new List<Vector3>();
        foreach (Vector3 dungeon in Dungeons.dungeonPositions)
        {
            BoardManager.CheckForBoardPositionsNearLocation(dungeon);
            if (BoardManager.northPositionAvailable) { possibleSpawns.Add(BoardManager.northPosition); }
            if (BoardManager.eastPositionAvailable) { possibleSpawns.Add(BoardManager.eastPosition); }
            if (BoardManager.southPositionAvailable) { possibleSpawns.Add(BoardManager.southPosition); }
            if (BoardManager.westPositionAvailable) { possibleSpawns.Add(BoardManager.westPosition); }
            foreach (Vector3 spawn in possibleSpawns)
            {
                random = Random.Range(1, 101);
                if (random <= 25)
                {
                    if (monsterPositions.Contains(spawn) || spawn == BoardManager.currentUnitPosition) { continue; }
                    else { monsterPositions.Add(spawn); CreateMonster(); }
                }
            }
            possibleSpawns.Clear();
        }
        MonsterMovement.UpdateAvatars();
        TurnManager.continueTurnProgression = true;
    }

    public static void CreateMonster()
    {
        int[] values = new int[4];
        int monster = 0;
        if (GameMain.currentBoard == 1)
        {
            monster = Random.Range(0, 2);
        }
        // Type, Health, Lives, Combat //
        switch (monster)
        {
            case 0:
                // Imp
                values[0] = 0; values[1] = 10; values[2] = 1; values[3] = 1; break;
            case 1:
                // Basilisk
                values[0] = 1; values[1] = 7; values[2] = 1; values[3] = 3; break;
        }
        activeMonsters.Add(values);
    }

    public static string MonsterDescriptions(int number)
    {
        string monsterDescription = "";
        switch (number)
        {
            case 0: monsterDescription = "Wanders aimlessly."; break;
            case 1: monsterDescription = "Stays near its dungeon."; break;
            case 2: monsterDescription = "Travels far and fast. Damages where it lands."; break;
            case 3: monsterDescription = "Hunts villages."; break;
        }
        return monsterDescription;
    }

    public static string MonsterNames(int number)
    {
        string monsterName = "";
        switch (number)
        {
            case 0: monsterName = "Imp"; break;
            case 1: monsterName = "Basilisk"; break;
            case 2: monsterName = "Rampaging Elephant"; break;
            case 3: monsterName = "Golem"; break;
        }
        return monsterName;
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