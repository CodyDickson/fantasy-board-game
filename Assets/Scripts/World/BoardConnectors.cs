using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardConnectors : MonoBehaviour
{
    public static Tilemap tilemapBoardConnectors;
    public static void VerticalBridge(bool positiveDirection)
    {
        if (positiveDirection)
        {
            tilemapBoardConnectors = Store.tilemaps[0];
            int random = Random.Range(0,3);
            if (random == 0)
            {
                tilemapBoardConnectors.SetTile();
            }
        }
    }
}
