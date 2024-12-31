using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    //
    public static bool playerIsMoving = false;
    public static string currentUnitDirection;
    // Time //
    private float avatar_counter = 0.01f;
    private float avatar_tempCounter = 0f;
    private float movement_tempCounter = 0f;
    private float movement_counter = 0.5f;

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
            Tilemap units = Store.tilemaps[4];
            units.SetTile(new Vector3Int((int)BoardManager.currentUnitPosition[0], (int)BoardManager.currentUnitPosition[1]), null);
            if (currentUnitDirection == "north")
            {
                BoardManager.currentUnitPosition = BoardManager.campExitPositions[0];
                BoardManager.playerPositions[GameMain.currentPlayer] = BoardManager.campExitPositions[0];
            }
            if (currentUnitDirection == "east")
            {
                BoardManager.currentUnitPosition = BoardManager.campExitPositions[1];
                BoardManager.playerPositions[GameMain.currentPlayer] = BoardManager.campExitPositions[1];
            }
            if (currentUnitDirection == "south")
            {
                BoardManager.currentUnitPosition = BoardManager.campExitPositions[2];
                BoardManager.playerPositions[GameMain.currentPlayer] = BoardManager.campExitPositions[2];
            }
            if (currentUnitDirection == "west")
            {
                BoardManager.currentUnitPosition = BoardManager.campExitPositions[3];
                BoardManager.playerPositions[GameMain.currentPlayer] = BoardManager.campExitPositions[3];
            }
            playerIsMoving = false;
            GameMain.playerInCamp[GameMain.currentPlayer] = false;
            GUI.ToggleMoveButton(true);
        }
    }
}