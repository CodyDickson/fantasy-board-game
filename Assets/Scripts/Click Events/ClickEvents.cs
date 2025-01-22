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
            Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int pOne = (int)clickedPosition.x;
            int pTwo = (int)clickedPosition.y;
            Vector3 position = new Vector3(pOne, pTwo);
            Debug.Log(position);
            var tilemap = Store.tilemaps[4].WorldToCell(position);
            var tile = Store.tilemaps[4].GetTile(tilemap);
            foreach (Vector3 dungeon in Dungeons.dungeonPositions)
            {
                if (dungeon == position)
                {
                    dungeonClicked = true;
                    Debug.Log(dungeonClicked);
                    InfoGUI.EnableInfoGUI();
                }
            }
            if (tile == Store.playerTiles[0])
            {
                Debug.Log("pass");
            }
            else
            {
                Debug.Log("nothing");
                InfoGUI.DisableInfoGUI();
            }
        }
    }
}