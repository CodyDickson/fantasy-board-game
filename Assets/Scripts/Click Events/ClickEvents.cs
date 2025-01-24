using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ClickEvents : MonoBehaviour
{
    void Start()
    {
        
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
            int pOne = (int)clickedPosition.x;
            int pTwo = (int)clickedPosition.y;
            Vector3 position = new Vector3(pOne, pTwo);
            var tilemap = Store.tilemaps[4].WorldToCell(position);
            var tile = Store.tilemaps[4].GetTile(tilemap);
            foreach (Vector3 dungeon in Dungeons.dungeonPositions)
            {
                if (dungeon == position)
                {
                    dungeonClicked = true;
                    InfoGUI.EnableInfoGUI(dungeon, "dungeon");
                }
            }
            foreach (Vector3 merchant in Merchants.merchantPositions)
            {
                if (merchant == position)
                {
                    merchantClicked = true;
                    InfoGUI.EnableInfoGUI(merchant, "merchant");
                }
            }
            foreach (Vector3 monster in Monsters.monsterPositions)
            {
                if (monster == position)
                {
                    monsterClicked = true;
                    InfoGUI.EnableInfoGUI(monster, "monster");
                }
            }
            foreach (Vector3 slot in BoardManager.emptyBoardSlots)
            {
                if (slot == position)
                {
                    emptySlotClicked = true;
                    InfoGUI.EnableInfoGUI(slot, "empty");
                }
            }
            foreach (Vector3 village in Villages.villagePositions)
            {
                if (village == position)
                {
                    villageClicked = true;
                    InfoGUI.EnableInfoGUI(village, "village");
                }
            }
            if (BoardManager.currentUnitPosition == position)
            {
                playerClicked = true;
                InfoGUI.EnableInfoGUI(BoardManager.currentUnitPosition, "player");
            }
            if (!dungeonClicked && !playerClicked && !merchantClicked && !monsterClicked && !emptySlotClicked && !villageClicked)
            {
                // InfoGUI.DisableInfoGUI();
            }
            /*if (tile == Store.playerTiles[0])
            {
                Debug.Log("clicked on player");
            }*/
        }
    }
}