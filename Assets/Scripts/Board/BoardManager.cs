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

    public static Vector3 CheckClockworkPosition(int clockworkLocation)
    {
        Vector3 center = new Vector3(0,0);
        if (GameMain.currentBoard == 1)
        {
            switch (clockworkLocation)
            {
                case 0: center = new Vector3(0, 0); break;
                case 1: center = new Vector3(0, 13); break;
                case 2: center = new Vector3(13, 13); break;
                case 3: center = new Vector3(13, 0); break;
                case 4: center = new Vector3(13, -13); break;
                case 5: center = new Vector3(0, -13); break;
                case 6: center = new Vector3(-13, -13); break;
                case 7: center = new Vector3(-13, 0); break;
                case 8: center = new Vector3(-13, 13); break;
                case 9: center = new Vector3(-13, 26); break;
                case 10: center = new Vector3(0, 26); break;
                case 11: center = new Vector3(13, 26); break;
                case 12: center = new Vector3(26, 26); break;
                case 13: center = new Vector3(26, 13); break;
                case 14: center = new Vector3(26, 0); break;
                case 15: center = new Vector3(26, -13); break;
                case 16: center = new Vector3(26, -26); break;
                case 17: center = new Vector3(13, -26); break;
                case 18: center = new Vector3(0, -26); break;
                case 19: center = new Vector3(-13, -26); break;
                case 20: center = new Vector3(-26, -26); break;
                case 21: center = new Vector3(-26, -13); break;
                case 22: center = new Vector3(-26, 0); break;
                case 23: center = new Vector3(-26, 13); break;
                case 24: center = new Vector3(-26, 26); break;
            }
        }
        return center;
    }
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