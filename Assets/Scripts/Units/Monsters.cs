using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Monsters : MonoBehaviour
{
    public static List<Vector3> monsterPositions;
    public static Vector3 monsterPosition;
    public static List<int[]> monsterStats;
    public static int totalMonsterIDs;
    public static int monsterID;
    public static bool monsterIsMoving = false;
    public static bool monsterAvatarsUpdated = false;
    public static int movesRemaining;
    public static int currentDungeon;
    public static Tilemap units;
    // Time //
    private float avatar_counter = 0.01f;
    private float avatar_tempCounter = 0f;
    private float movement_tempCounter = 0f;
    private float movement_counter = 0.5f;

    private void Start()
    {
        units = Store.tilemaps[4];
    }

    void Update()
    {
        // Set monster avatars on the board
        if (!monsterAvatarsUpdated)
        {
            SetAvatarOnBoard();
            monsterAvatarsUpdated = false;
        }
        // Monster moving on the board
        if (monsterIsMoving)
        {
            if (movement_tempCounter <= 0f)
            {
                if (movesRemaining > 0)
                {
                    BoardManager.CheckForLocalBoardPositions();
                    BoardManager.DetermineNextBoardPosition();
                    units.SetTile(new Vector3Int((int)BoardManager.currentUnitPosition[0], (int)BoardManager.currentUnitPosition[1]), Store.playerTiles[GameMain.playerAvatar]);
                    Debug.Log("Current Unit Position: " + BoardManager.currentUnitPosition);
                    BoardManager.CheckForCrossroads();
                    if (BoardManager.crossroadsPosition == true)
                    {
                        BoardManager.CheckForLocalBoardPositions();
                        Arrows.EnableArrowButtons();
                        monsterIsMoving = false;
                    }
                    movesRemaining -= 1;
                }
                if (movesRemaining == 0)
                {
                    GUIManager.ToggleEndTurnButton(true);
                    monsterIsMoving = false;
                    InfoGUI.ToggleInfoGUI(true);
                    Dice.DisableDice();
                }
                movement_tempCounter = movement_counter;
            }
            else
            {
                movement_tempCounter -= Time.deltaTime;
            }
        }
    }

    public static void SetAvatarOnBoard()
    {
        foreach (Vector3 monsterPosition in monsterPositions)
        {

            units.SetTile(new Vector3Int((int)monsterPosition[0], (int)monsterPosition[1]), Store.monsterTiles[0]);
        }
    }

    public static void SpawnMonster()
    {
        Debug.Log("Spawn Monsters");
        int player = GameMain.currentPlayer;
        int random = 0;
        Vector3 position = BoardManager.currentUnitPosition;
        int monsterChoice = 0;
        Tile monster;
        List<Vector3> possibleSpawns = new List<Vector3>();
        foreach (Vector3 dungeon in Dungeons.dungeonPositions)
        {
            random = Random.Range(1,4);
            if (random == 1)
            {
                UpdateCurrentDungeon(dungeon);
                monsterChoice = Dungeons.dungeonStats[1];
                if (monsterChoice == 1)
                {
                    int randomTwo = Random.Range(1,3);
                    if (randomTwo == 1)
                    {
                        monsterChoice = 1;
                    }
                    else
                    {
                        monsterChoice = 2;
                    }
                }
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
                if (monsterPositions.Contains(position)) { continue; }
                else { monsterPositions.Add(position); MonsterStartingStats(0); }
            }    
        }
        monsterAvatarsUpdated = true;
        TurnManager.continueTurnProgression = true;
    }

    public static void MonsterStartingStats(int monster)
    {
        int[] values = new int[4];
        totalMonsterIDs++;
        // ID, Health, Lives, Combat Dice, Status Effect
        switch (monster)
        {
            case 0:
                // Imp
                values[0] = 1; values[1] = 1; values[2] = 1; values[3] = 0; break;
            case 1:
                // Basilisk
                values[0] = 2; values[1] = 1; values[2] = 1; values[3] = 1; break;
        }
        monsterStats.Add(values);
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

    public static void UpdateCurrentDungeon(Vector3 position)
    {
        foreach (Vector3 dungeon in Dungeons.dungeonPositions)
        {
            if (dungeon == position)
            {
                currentDungeon = Dungeons.dungeonPositions.IndexOf(dungeon);
            }
        }
    }
}