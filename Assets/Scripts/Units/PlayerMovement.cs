using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class PlayerMovement : MonoBehaviour
{
    //
    public static bool playerIsMoving = false;
    public static int movesRemaining;
    public Tilemap units;
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
        // Player Exiting Camp
        if (playerIsMoving && GameMain.playerInCamp)
        {
            PlayerExitingCamp();
        }
        // Player moving on the board
        if (playerIsMoving && !GameMain.playerInCamp)
        {
            if (movement_tempCounter <= 0f)
            {
                if (movesRemaining > 0)
                {
                    units.SetTile(new Vector3Int((int)BoardManager.currentUnitPosition[0], (int)BoardManager.currentUnitPosition[1]), null);
                    bool positionClear = Monsters.CheckMonsterPositions(BoardManager.currentUnitPosition);
                    if (positionClear) { MonsterMovement.UpdateAvatars(); }
                    BoardManager.CheckForLocalBoardPositions();
                    BoardManager.DetermineNextBoardPosition();
                    units.SetTile(new Vector3Int((int)BoardManager.currentUnitPosition[0], (int)BoardManager.currentUnitPosition[1]), Store.playerTiles[GameMain.playerAvatar]);
                    BoardManager.CheckForCrossroads();
                    if (BoardManager.crossroadsPosition == true)
                    {
                        BoardManager.CheckForLocalBoardPositions();
                        Arrows.EnableArrowButtons();
                        playerIsMoving = false;
                    }
                    BoardManager.CheckForMidway();
                    if (BoardManager.midwayPosition == true)
                    {
                        BoardManager.CheckForLocalBoardPositions();
                        Arrows.EnableArrowButtons();
                        playerIsMoving = false;
                    }
                    movesRemaining -= 1;
                }
                if (movesRemaining == 0)
                {
                    Arrows.DisableArrowButtons();
                    GUIManager.EnableEndTurnButton();
                    playerIsMoving = false;
                    InfoGUI.DisableInfoGUI();
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

    public static void PlayerExitingCamp()
    {
        Tilemap units = Store.tilemaps[4];
        units.SetTile(new Vector3Int((int)BoardManager.currentUnitPosition[0], (int)BoardManager.currentUnitPosition[1]), null);
        if (BoardManager.currentUnitDirection == "north") { BoardManager.currentUnitPosition = BoardManager.campExitPositions[0]; }
        if (BoardManager.currentUnitDirection == "east") { BoardManager.currentUnitPosition = BoardManager.campExitPositions[1]; }
        if (BoardManager.currentUnitDirection == "south") { BoardManager.currentUnitPosition = BoardManager.campExitPositions[2]; }
        if (BoardManager.currentUnitDirection == "west") { BoardManager.currentUnitPosition = BoardManager.campExitPositions[3]; }
        playerIsMoving = false;
        GameMain.playerInCamp = false;
        int clockwork = 0;
        switch (BoardManager.currentUnitDirection)
        {
            case "north": clockwork = 1; break;
            case "east": clockwork = 3; break;
            case "south": clockwork = 5; break;
            case "west": clockwork = 7; break;
        }
        Fog.RemoveLocalFog(clockwork);
        Dungeons.SpawnDungeons(clockwork);
        GUIManager.EnableMoveButton();
        GUIManager.EnableEndTurnButton();
        GUIManager.EnableAttackButton();
        GUIManager.EnableHealButton();
    }
}