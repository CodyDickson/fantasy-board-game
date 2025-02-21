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
            bool withinInteractionRange = false;
            Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int pOne = Mathf.FloorToInt(clickedPosition.x);
            int pTwo = Mathf.FloorToInt(clickedPosition.y);
            position = new Vector3(pOne, pTwo);
            var tilemap = Store.tilemaps[4].WorldToCell(position);
            var tile = Store.tilemaps[4].GetTile(tilemap);
            BoardManager.CheckInteractionRange();
            if (GameMain.playerInCamp)
            {
                foreach (Vector3 exitPosition in BoardManager.exitPositions)
                {
                    if (exitPosition == position)
                    {
                        permanentPosition = position;
                        ConfirmationGUI.EnableConfirmationGUI("exitingCamp");
                    }
                }
            }
            if (BoardManager.currentInteractionRange.Contains(position))
            {
                withinInteractionRange = true;
            }
            foreach (Vector3 dungeon in Dungeons.dungeonPositions)
            {
                if (dungeon == position)
                {
                    permanentPosition = position;
                    // ConfirmationGUI.EnableConfirmationGUI("raidDungeon");
                    InfoGUI.EnableInfoGUI(position, "dungeon", withinInteractionRange);
                }
            }
            foreach (Vector3 merchant in Merchants.merchantPositions)
            {
                if (merchant == position)
                {
                    permanentPosition = position;
                    ConfirmationGUI.EnableConfirmationGUI("openShop");
                }
            }
            foreach (Vector3 monster in Monsters.monsterPositions)
            {
                if (monster == position)
                {
                    Debug.Log("Monster");
                    /* if (CombatManager.combatEnabled)
                    {
                        CombatManager.PlayerAttackedMonster();
                    }
                    else
                    {
                        InfoGUI.EnableInfoGUI(monster, "monster");
                    }*/
                    InfoGUI.EnableInfoGUI(position, "monster", withinInteractionRange);
                }
            }
            foreach (Vector3 slot in BoardManager.potentialEmptySlots)
            {
                if (slot == position)
                {
                    Debug.Log("Empty Slot");
                    InfoGUI.EnableInfoGUI(slot, "empty", withinInteractionRange);
                }
            }
            foreach (Vector3 village in Villages.villagePositions)
            {
                if (village == position)
                {
                    Debug.Log("Village");
                    InfoGUI.EnableInfoGUI(village, "village", withinInteractionRange);
                }
            }
            if (BoardManager.currentUnitPosition == position)
            {
                Debug.Log("Player");
                // InfoGUI.EnableInfoGUI(BoardManager.currentUnitPosition, "player");
            }
        }
    }
}