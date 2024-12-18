using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public static void GenerateGameBoard(Tilemap tilemapBoardConnectors, Tile camp, Tile bcHorizontal, Tile bcThreeDown, Tile bcVertical, Tile bcThreeUp, Tile bcThreeLeft, Tile bcThreeRight, Tile bcTopRightCorner, Tile bcBottomLeftCorner, Tile bcBottomRightCorner, Tile bcTopLeftCorner)
    {
        switch (GameMain.currentMap)
        {
            case 1: GrasslandsBoard.GenerateGrasslandsBoard(tilemapBoardConnectors, camp, bcHorizontal, bcThreeDown, bcVertical, bcThreeUp, bcThreeLeft, bcThreeRight, bcTopRightCorner, bcBottomLeftCorner, bcBottomRightCorner, bcTopLeftCorner); break;
        }
    }
}