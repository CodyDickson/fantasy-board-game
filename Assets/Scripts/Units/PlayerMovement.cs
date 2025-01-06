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
        // Player avatars on the board
        if (avatar_tempCounter <= 0f)
        {
            for (int i = 1; i <= GameMain.totalPlayers; i++)
            {
                if (GameMain.playerLives[i] > 0 && GameMain.playerIsActive[i])
                {
                    BoardManager.boardPosition = BoardManager.playerPositions[i];
                    Store.tilemaps[4].SetTile(new Vector3Int((int)BoardManager.boardPosition[0], (int)BoardManager.boardPosition[1]), Store.playerTiles[GameMain.playerAvatar[i]]);
                }
            }
            avatar_tempCounter = avatar_counter;
        }
        else
        {
            avatar_tempCounter -= Time.deltaTime;
        }
        // Player Exiting Camp
        if (playerIsMoving && GameMain.playerInCamp[GameMain.currentPlayer])
        {
            PlayerExitingCamp();
        }
        // Player moving on the board
        if (playerIsMoving && !GameMain.playerInCamp[GameMain.currentPlayer])
        {
            if (movement_tempCounter <= 0f)
            {
                if (movesRemaining > 0)
                {
                    BoardManager.CheckForLocalBoardPositions();
                    BoardManager.DetermineNextBoardPosition();
                    units.SetTile(new Vector3Int((int)BoardManager.currentUnitPosition[0], (int)BoardManager.currentUnitPosition[1]), Store.playerTiles[GameMain.currentPlayer]);
                    // BoardManager.CheckForBoardCrossroads(gui);
                    Debug.Log("Current Unit Position: " + BoardManager.currentUnitPosition);
                    // BoardManager.playerPositions[GameMain.currentPlayer] = BoardManager.currentUnitPosition;
                    movesRemaining -= 1;
                    /*if (crossroadsPosition == true)
                    {
                        BoardManager.CheckForLocalBoardPositions();
                        GUI.enableArrowButtons = true;

                        playerIsMoving = false;
                    }*/
                }
                if (movesRemaining == 0)
                {
                    GUIManager.enableArrowButtons = false;
                    GUIManager.ToggleEndTurnButton(true);
                    playerIsMoving = false;
                    BoardManager.CheckForLocalEmptySlots();
                    InfoGUI.ToggleInfoGUI(true);
                }
                movement_tempCounter = movement_counter;
            }
            else
            {
                movement_tempCounter -= Time.deltaTime;
            }
        }
    }

    public static void MoveUnit()
    {
        GameMain.RollDice();
        GUIManager.ToggleMoveButton(false);
        movesRemaining = GameMain.diceOneResult + GameMain.diceTwoResult + GameMain.diceThreeResult;
        BoardManager.CheckForLocalBoardPositions();
    }

    public static void PlayerExitingCamp()
    {
        Tilemap units = Store.tilemaps[4];
        BoardManager.currentUnitPosition = BoardManager.playerPositions[GameMain.currentPlayer];
        units.SetTile(new Vector3Int((int)BoardManager.currentUnitPosition[0], (int)BoardManager.currentUnitPosition[1]), null);
        if (BoardManager.currentUnitDirection == "north")
        {
            BoardManager.currentUnitPosition = BoardManager.campExitPositions[0];
            BoardManager.playerPositions[GameMain.currentPlayer] = BoardManager.campExitPositions[0];
        }
        if (BoardManager.currentUnitDirection == "east")
        {
            BoardManager.currentUnitPosition = BoardManager.campExitPositions[1];
            BoardManager.playerPositions[GameMain.currentPlayer] = BoardManager.campExitPositions[1];
        }
        if (BoardManager.currentUnitDirection == "south")
        {
            BoardManager.currentUnitPosition = BoardManager.campExitPositions[2];
            BoardManager.playerPositions[GameMain.currentPlayer] = BoardManager.campExitPositions[2];
        }
        if (BoardManager.currentUnitDirection == "west")
        {
            BoardManager.currentUnitPosition = BoardManager.campExitPositions[3];
            BoardManager.playerPositions[GameMain.currentPlayer] = BoardManager.campExitPositions[3];
        }
        playerIsMoving = false;
        GameMain.playerInCamp[GameMain.currentPlayer] = false;
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
        Merchants.SpawnMerchants(clockwork);
        GUIManager.ToggleMoveButton(true);
    }
}