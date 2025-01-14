using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Monsters : MonoBehaviour
{
    // Tracking //
    public static Dictionary<Vector3, int> monsterPositions = new Dictionary<Vector3, int>();
    public static Dictionary<int, int> monsterStats = new Dictionary<int, int>();
    //
    public static bool monsterIsMoving = false;
    public static int movesRemaining;
    public static Tilemap units;
    public static List<Vector3> allMonsterPositions;
    public static Vector3 monsterPosition;
    public static int totalMonsterIDs;
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
        // Player avatar on the board
        if (avatar_tempCounter <= 0f)
        {
            if (GameMain.playerLives > 0)
            {
                Store.tilemaps[4].SetTile(new Vector3Int((int)BoardManager.currentUnitPosition[0], (int)BoardManager.currentUnitPosition[1]), Store.playerTiles[GameMain.playerAvatar]);
            }
            avatar_tempCounter = avatar_counter;
        }
        else
        {
            avatar_tempCounter -= Time.deltaTime;
        }
        // Player moving on the board
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
        BoardManager.CheckForLocalBoardPositions();
        BoardManager.DetermineNextBoardPosition();
        units.SetTile(new Vector3Int((int)monsterPosition[0], (int)monsterPosition[1]), Store.playerTiles[GameMain.playerAvatar]);
    }

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
        foreach (Vector3 dungeon in Dungeons.dungeonPositions)
        {
            // BoardManager.currentUnitPosition = dungeon;
            random = Random.Range(1,4);
            if (random == 1)
            {
                monsterChoice = 1;
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
                if (monsterPositions.ContainsKey(position)) { continue; }
                else { monsterPositions.Add(position, monsterChoice); }
            }    
        }
        TurnManager.continueTurnProgression = true;
    }

    public static int[] MonsterStartingStats(int monster)
    {
        int[] values = new int[6];
        values[0] = totalMonsterIDs;
        totalMonsterIDs++;
        // ID, Health, Combat Dice, Armor, Lives, Status Effect Infliction, Status Effect Immunity
        switch (monster)
        {
            case 0: values[1] = 3; values[2] = 3; values[3] = 0; values[4] = 1; values[5] = 0; values[6] = 0; break;
            case 1: values[1] = 3; values[2] = 3; values[3] = 0; values[4] = 1; values[5] = 1; values[6] = 1; break;
        }
        return values;
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