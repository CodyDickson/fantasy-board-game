using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ClickEvents : MonoBehaviour
{
    Plane groundPlane;

    void Start()
    {
        groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool dungeonClicked = false;
            bool playerClicked = false;
            bool merchantClicked = false;
            bool monsterClicked = false;
            bool emptySlotClicked = false;
            bool villageClicked = false;
            Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(clickedPosition);
            int pOne = Mathf.RoundToInt(clickedPosition.x);
            int pTwo = Mathf.RoundToInt(clickedPosition.y);
            Vector3 position = new Vector3(pOne, pTwo);
            Debug.Log("Position: " + position);
            var tilemap = Store.tilemaps[4].WorldToCell(position);
            var tile = Store.tilemaps[4].GetTile(tilemap);
            foreach (Vector3 exitPosition in BoardManager.exitPositions)
            {
                if (exitPosition == position)
                {
                    Debug.Log("Clicked: Camp Exit Position");
                    PlayerMovement.PlayerExitingCamp(exitPosition);
                }
            }
            foreach (Vector3 dungeon in Dungeons.dungeonPositions)
            {
                if (dungeon == position)
                {
                    Debug.Log("Clicked: Dungeon");
                    dungeonClicked = true;
                    InfoGUI.EnableInfoGUI(dungeon, "dungeon");
                }
            }
            foreach (Vector3 merchant in Merchants.merchantPositions)
            {
                if (merchant == position)
                {
                    Debug.Log("Clicked: Merchant");
                    merchantClicked = true;
                    InfoGUI.EnableInfoGUI(merchant, "merchant");
                }
            }
            foreach (Vector3 monster in Monsters.monsterPositions)
            {
                if (monster == position)
                {
                    Debug.Log("Monster");
                    monsterClicked = true;
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
                    emptySlotClicked = true;
                    InfoGUI.EnableInfoGUI(slot, "empty");
                }
            }
            foreach (Vector3 village in Villages.villagePositions)
            {
                if (village == position)
                {
                    Debug.Log("Village");
                    villageClicked = true;
                    InfoGUI.EnableInfoGUI(village, "village");
                }
            }
            Debug.Log("Current Unit Position: " + BoardManager.currentUnitPosition);
            if (BoardManager.currentUnitPosition == position)
            {
                Debug.Log("Self");
                playerClicked = true;
                InfoGUI.EnableInfoGUI(BoardManager.currentUnitPosition, "player");
            }
            if (!dungeonClicked && !playerClicked && !merchantClicked && !monsterClicked && !emptySlotClicked && !villageClicked)
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