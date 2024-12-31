using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Time //
    private float avatar_counter = 0.01f;
    private float avatar_tempCounter = 0f;
    private float movement_tempCounter = 0f;
    private float movement_counter = 0.5f;

    void Update()
    {
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
    }
}
