using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterMovement : MonoBehaviour
{
    public static bool updateMonsterAvatars = false;
    public static bool monsterIsMoving = false;
    public static int movesRemaining;
    private float movement_tempCounter = 0f;
    private float movement_counter = 0.5f;

    void Update()
    {
        // Set monster avatars on the board
        if (updateMonsterAvatars)
        {
            SetAvatarsOnBoard();
            updateMonsterAvatars = false;
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
                    //units.SetTile(new Vector3Int((int)BoardManager.currentUnitPosition[0], (int)BoardManager.currentUnitPosition[1]), Store.playerTiles[GameMain.playerAvatar]);
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
                    GUIManager.EnableEndTurnButton();
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

    public static void SetAvatarsOnBoard()
    {
        foreach (Vector3 monsterPosition in Monsters.monsterPositions)
        {
            Tilemap units = Store.tilemaps[4];
            int monsterID = Monsters.monsterPositions.IndexOf(monsterPosition);
            int[] list = Monsters.activeMonsters[monsterID];
            int choice = list[0];
            units.SetTile(new Vector3Int((int)monsterPosition[0], (int)monsterPosition[1]), Store.monsterTiles[choice]);
        }
    }

    public static void UpdateAvatars()
    {
        updateMonsterAvatars = true;
    }

    public static void EnableMonsterMovement()
    {
        TurnManager.continueTurnProgression = true;
    }
}
