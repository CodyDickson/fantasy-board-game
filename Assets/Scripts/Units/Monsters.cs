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
    public static int monsterID;
    public static Vector3 monsterPosition;
    public static Tilemap units;

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
                    int currentDungeon = Dungeons.FindCurrentDungeon(spawn);
                    if (monsterPositions.Contains(spawn) || spawn == BoardManager.currentUnitPosition) { continue; }
                    else { monsterPositions.Add(spawn); CreateMonster(spawn, currentDungeon); }
                }
            }
            possibleSpawns.Clear();
        }
        MonsterMovement.UpdateAvatars();
        TurnManager.continueTurnProgression = true;
    }

    public static void CreateMonster(Vector3 dungeonPosition, int currentDungeon)
    {
        int[] values = new int[4];
        int[] dungeonInts = Dungeons.activeDungeons[currentDungeon];
        int monster = dungeonInts[0];
        // int monsterID = Dungeons.dungeonPositions.IndexOf(dungeonPosition);
        // Type, Health, Lives, Combat Strength //
        switch (monster)
        {
            case 0:
                // Imp
                values[0] = monster; values[1] = 10; values[2] = 1; values[3] = 1; StatusEffects.SetUpNewMonsterStatusEffects(false, false); break;
            case 1:
                // Basilisk
                values[0] = monster; values[1] = 7; values[2] = 1; values[3] = 2; StatusEffects.SetUpNewMonsterStatusEffects(true, false); break;
            case 2:
                // Rampaging Elephant
                values[0] = monster; values[1] = 15; values[2] = 2; values[3] = 5; StatusEffects.SetUpNewMonsterStatusEffects(false, false); break;
            case 3:
                // Golem
                values[0] = monster; values[1] = 10; values[2] = 1; values[3] = 3; StatusEffects.SetUpNewMonsterStatusEffects(false, false); break;
            case 4:
                // Magma Cultists
                values[0] = monster; values[1] = 10; values[2] = 2; values[3] = 3; StatusEffects.SetUpNewMonsterStatusEffects(false, false); break;
            case 5:
                // Magma Lord
                values[0] = monster; values[1] = 50; values[2] = 1; values[3] = 10; StatusEffects.SetUpNewMonsterStatusEffects(false, false); break;
        }
        activeMonsters.Add(values);
    }

    public static void MonsterHasDied(int monsterID)
    {

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
            case 4: monsterDescription = "Gathers at altars to summon the Magma Lord."; break;
            case 5: monsterDescription = "Will not rest until it is all lava."; break;
        }
        return monsterDescription;
    }

    public static string MonsterNames(int number)
    {
        string monsterName = "";
        /*int board = GameMain.currentBoard;
        List<string> monsters = new();
        if (board == 1)
        {
            monsters.Add("Imp"); monsters.Add("Basilisk"); monsters.Add("Eagle"); monsters.Add("Rampaging Elephant");
        }
        monsterName = monsters[number];*/
        switch (number)
        {
            case 0: monsterName = "Imp"; break;
            case 1: monsterName = "Basilisk"; break;
            case 2: monsterName = "Rampaging Elephant"; break;
            case 3: monsterName = "Golem"; break;
            case 4: monsterName = "Magma Cultist"; break;
            case 5: monsterName = "Magma Lord"; break;
            case 6: monsterName = "Eagle"; break;
        }
        return monsterName;
    }

    public static bool CheckMonsterPositions(Vector3 positionToCheck)
    {
        bool positionClear = true;
        foreach (Vector3 monsterPosition in monsterPositions)
        {
            if (monsterPosition == positionToCheck)
            {
                positionClear = false;
            }
        }
        return positionClear;
    }

    public static int FindCurrentMonster(Vector3 position)
    {
        int monsterNumber = monsterPositions.IndexOf(position);
        return monsterNumber;
    }

    public static void PlayerAttackedMonster(Vector3 position)
    {
        int monsterID = FindCurrentMonster(position);
        int[] ints = new int[4];
        int monsterHealth = ints[1];
        int monsterLives = ints[2];
        int playerCombat = Random.Range(Player.minimumCombatStrength, Player.maximumCombatStrength + 1);
        Debug.Log("Monster Health: " + monsterHealth + "\nPlayer Combat: " + playerCombat);
        monsterHealth -= playerCombat;
        if (monsterHealth <= 0)
        {
            monsterLives -= 1;
            if (monsterLives <= 0)
            {
                Debug.Log("Monster Has Died");
                MonsterHasDied(monsterID);
            }
            else
            {

            }
        }
    }
}