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
            int pOne = Mathf.RoundToInt(clickedPosition.x);
            int pTwo = Mathf.RoundToInt(clickedPosition.y);
            position = new Vector3(pOne, pTwo);
            var tilemap = Store.tilemaps[4].WorldToCell(position);
            var tile = Store.tilemaps[4].GetTile(tilemap);
            BoardManager.CheckInteractionRange();
            if (BoardManager.currentInteractionRange.Contains(position))
            {
                foreach (Vector3 exitPosition in BoardManager.exitPositions)
                {
                    if (exitPosition == position && GameMain.playerInCamp)
                    {
                        somethingClicked = true;
                        permanentPosition = position;
                        ConfirmationGUI.EnableConfirmationGUI("exitingCamp");
                    }
                }
                foreach (Vector3 dungeon in Dungeons.dungeonPositions)
                {
                    if (dungeon == position)
                    {
                        somethingClicked = true;
                        permanentPosition = position;
                        ConfirmationGUI.EnableConfirmationGUI("raidDungeon");
                    }
                }
                foreach (Vector3 merchant in Merchants.merchantPositions)
                {
                    if (merchant == position)
                    {
                        somethingClicked = true;
                        permanentPosition = position;
                        ConfirmationGUI.EnableConfirmationGUI("openShop");
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
                if (BoardManager.currentUnitPosition == position)
                {
                    Debug.Log("Player");
                    somethingClicked = true;
                    InfoGUI.EnableInfoGUI(BoardManager.currentUnitPosition, "player");
                }
            }
        }
    }
}