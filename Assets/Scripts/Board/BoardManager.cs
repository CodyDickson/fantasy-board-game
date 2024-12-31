using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    // Board Positions
    public static Vector3 currentUnitPosition;
    public static Vector3 boardPosition;
    public static List<Vector3> boardPositions = new List<Vector3>();
    public static List<Vector3> localBoardPositions = new List<Vector3>();
    public static List<Vector3> crossroads = new List<Vector3>();
    public static List<Vector3> campExitPositions = new List<Vector3>();
    public static List<Vector3> playerPositions = new List<Vector3>();
    // Tiles
    public static Tile bcVertical, bcHorizontal, bcTopLeftCorner, bcTopRightCorner, bcBottomLeftCorner, bcBottomRightCorner, bcThreeUp, bcThreeDown, bcThreeLeft, bcThreeRight;

    public static void GenerateGameBoard()
    {
        switch (GameMain.currentBoard)
        {
            case 1: Grasslands.GenerateBoard(); break;
            // case 2: Graveyard.GenerateBoard(); break;
            // case 3: Volcano.GenerateBoard(); break;
            // case 4: Machine.GenerateBoard(); break;
        }
    }

    public static void SpawnPlayersInCamp()
    {
        if (playerPositions.Count == 0) { playerPositions.Add(new Vector3Int(0, 0)); }
        for (int i = 1; i <= GameMain.totalPlayers; i++)
        {
            switch (GameMain.currentBoard)
            {
                case 1: Grasslands.CampSpawn(); break;
                    // case 2: Graveyard.CampSpawn(); break;
                    // case 3: Volcano.CampSpawn(); break;
                    // case 4: Machine.CampSpawn(); break;
            }
            GameMain.playerIsActive[i] = true;
            GameMain.playerInCamp[i] = true;
        }
    }
}