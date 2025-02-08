using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ClickEvents : MonoBehaviour
{
    public static Vector3 position = Vector3.zero;
    public static Vector3 permanentPosition = Vector3.zero;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool somethingClicked = false;
            Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(clickedPosition);
            int pOne = Mathf.RoundToInt(clickedPosition.x);
            int pTwo = Mathf.RoundToInt(clickedPosition.y);
            position = new Vector3(pOne, pTwo);
            Debug.Log("Position: " + position);
            var tilemap = Store.tilemaps[4].WorldToCell(position);
            var tile = Store.tilemaps[4].GetTile(tilemap);
            foreach (Vector3 exitPosition in BoardManager.exitPositions)
            {
                if (exitPosition == position && GameMain.playerInCamp)
                {
                    somethingClicked = true;
                    permanentPosition = position;
                    Debug.Log("Clicked: Camp Exit Position");
                    ConfirmationGUI.EnableConfirmationGUI("exitingCamp");
                }
            }
            foreach (Vector3 dungeon in Dungeons.dungeonPositions)
            {
                if (dungeon == position)
                {
                    Debug.Log("Clicked: Dungeon");
                    somethingClicked = true;
                    InfoGUI.EnableInfoGUI(dungeon, "dungeon");
                }
            }
            foreach (Vector3 merchant in Merchants.merchantPositions)
            {
                if (merchant == position)
                {
                    Debug.Log("Clicked: Merchant");
                    somethingClicked = true;
                    InfoGUI.EnableInfoGUI(merchant, "merchant");
                }
            }
            foreach (Vector3 monster in Monsters.monsterPositions)
            {
                if (monster == position)
                {
                    Debug.Log("Monster");
                    somethingClicked = true;
                    if (CombatManager.combatEnabled)
                    {
                        CombatManager.PlayerAttackedMonster();
                    }
                    else
                    {
                        InfoGUI.EnableInfoGUI(monster, "monster");
                    }
                }
            }
            foreach (Vector3 slot in BoardManager.potentialEmptySlots)
            {
                if (slot == position)
                {
                    Debug.Log("Empty Slot");
                    somethingClicked = true;
                    InfoGUI.EnableInfoGUI(slot, "empty");
                }
            }
            foreach (Vector3 village in Villages.villagePositions)
            {
                if (village == position)
                {
                    Debug.Log("Village");
                    somethingClicked = true;
                    InfoGUI.EnableInfoGUI(village, "village");
                }
            }
            /*foreach (Vector3 boardPosition in BoardManager.boardPositions)
            {
                foreach (Vector3 movingPosition in BoardManager.possibleMove)
                {
                    if (boardPosition.Equals(movingPosition))
                    {
                        if (boardPosition == position && InfoGUI.movesAreShowing)
                        {
                            Debug.Log("Movement");
                            PlayerMovement.playerIsMoving = true;
                            // Need to figure out what to do with player direction
                        }
                    }
                }
            }*/
            if (BoardManager.currentUnitPosition == position)
            {
                Debug.Log("Player");
                somethingClicked = true;
                InfoGUI.EnableInfoGUI(BoardManager.currentUnitPosition, "player");
            }
            if (!somethingClicked)
            {
                Debug.Log("Nothing");
                // InfoGUI.DisableInfoGUI();
            }
            /*if (tile == Store.playerTiles[0])
            {
                Debug.Log("clicked on player");
            }*/
        }
    }
}