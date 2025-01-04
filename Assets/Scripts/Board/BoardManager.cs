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
    public static List<Vector3> merchantPositions = new List<Vector3>();
    public static List<Vector3> emptyBoardSlots = new List<Vector3>();
    // Directions
    public static string currentUnitDirection;
    public static Vector3 northPosition;
    public static bool northPositionAvailable = false;
    public static Vector3 eastPosition;
    public static bool eastPositionAvailable = false;
    public static Vector3 southPosition;
    public static bool southPositionAvailable = false;
    public static Vector3 westPosition;
    public static bool westPositionAvailable = false;
    public static Vector3 northSlotPosition;
    public static bool northEmpty = false;
    public static Vector3 eastSlotPosition;
    public static bool eastEmpty = false;
    public static Vector3 southSlotPosition;
    public static bool southEmpty = false;
    public static Vector3 westSlotPosition;
    public static bool westEmpty = false;
    // Nearby
    public static bool villageNearby = false;
    public static bool villageNorth = false;
    public static bool villageEast = false;
    public static bool villageSouth = false;
    public static bool villageWest = false;
    public static bool dungeonNearby = false;
    public static bool dungeonNorth = false;
    public static bool dungeonEast = false;
    public static bool dungeonSouth = false;
    public static bool dungeonWest = false;
    public static bool merchantNearby = false;
    public static bool merchantNorth = false;
    public static bool merchantEast = false;
    public static bool merchantSouth = false;
    public static bool merchantWest = false;
    //
    public static Tilemap structures;
    public static bool crossroadsPosition = false;

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

    public static void FillEmptySlots()
    {
        structures = Store.tilemaps[3];
        foreach (Vector3 listVector in emptyBoardSlots)
        {
            structures.SetTile(new Vector3Int((int)listVector[0], (int)listVector[1]), Store.objectTiles[2]);
        }
    }

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

    public static void CheckForLocalBoardPositions()
    {
        northPositionAvailable = false;
        eastPositionAvailable = false;
        southPositionAvailable = false;
        westPositionAvailable = false;
        Vector3 north = new Vector3(currentUnitPosition[0], currentUnitPosition[1] + 1);
        Vector3 east = new Vector3(currentUnitPosition[0] + 1, currentUnitPosition[1]);
        Vector3 south = new Vector3(currentUnitPosition[0], currentUnitPosition[1] - 1);
        Vector3 west = new Vector3(currentUnitPosition[0] - 1, currentUnitPosition[1]);
        foreach (Vector3 listVector in boardPositions)
        {
            if (listVector == north)
            {
                northPosition = north;
                northPositionAvailable = true;
            }
            if (listVector == east)
            {
                eastPosition = east;
                eastPositionAvailable = true;
            }
            if (listVector == south)
            {
                southPosition = south;
                southPositionAvailable = true;
            }
            if (listVector == west)
            {
                westPosition = west;
                westPositionAvailable = true;
            }
        }
    }

    public static void DetermineNextBoardPosition()
    {
        Tilemap tilemapUnits = Store.tilemaps[4];
        boardPosition = currentUnitPosition;
        Debug.Log("Check Board Position: " + boardPosition);
        tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), null);
        if (currentUnitDirection == "north" && northPositionAvailable)
        {
            boardPosition[1] += 1;
        }
        else if (currentUnitDirection == "east" && eastPositionAvailable)
        {
            boardPosition[0] += 1;
        }
        else if (currentUnitDirection == "south" && southPositionAvailable)
        {
            boardPosition[1] -= 1;
        }
        else if (currentUnitDirection == "west" && westPositionAvailable)
        {
            boardPosition[0] -= 1;
        }
        if (currentUnitDirection == "north" && !northPositionAvailable)
        {
            if (eastPositionAvailable)
            {
                boardPosition[0] += 1;
            }
            else if (westPositionAvailable)
            {
                boardPosition[0] -= 1;
            }
        }
        if (currentUnitDirection == "east" && !eastPositionAvailable)
        {
            if (northPositionAvailable)
            {
                boardPosition[1] += 1;
            }
            else if (southPositionAvailable)
            {
                boardPosition[1] -= 1;
            }
        }
        if (currentUnitDirection == "south" && !southPositionAvailable)
        {
            if (eastPositionAvailable)
            {
                boardPosition[0] += 1;
            }
            else if (westPositionAvailable)
            {
                boardPosition[0] -= 1;
            }
        }
        if (currentUnitDirection == "west" && !westPositionAvailable)
        {
            if (northPositionAvailable)
            {
                boardPosition[1] += 1;
            }
            else if (southPositionAvailable)
            {
                boardPosition[1] -= 1;
            }
        }
        currentUnitPosition = boardPosition;
        playerPositions[GameMain.currentPlayer] = currentUnitPosition;
    }

    public static void CheckForLocalStructures()
    {
        CheckForLocalDungeons();
        CheckForLocalVillages();
        CheckForLocalMerchants();
        CheckForLocalEmptySlots();
    }

    public static void CheckForLocalDungeons()
    {
        dungeonNorth = false;
        dungeonEast = false;
        dungeonSouth = false;
        dungeonWest = false;
        Vector3 north = new Vector3(currentUnitPosition[0], currentUnitPosition[1] + 1);
        Vector3 east = new Vector3(currentUnitPosition[0] + 1, currentUnitPosition[1]);
        Vector3 south = new Vector3(currentUnitPosition[0], currentUnitPosition[1] - 1);
        Vector3 west = new Vector3(currentUnitPosition[0] - 1, currentUnitPosition[1]);
        foreach (Vector3 listVector in Dungeons.dungeonPositions.Keys)
        {
            if (listVector == north) { dungeonNorth = true; Debug.Log("pass"); }
            if (listVector == east) { dungeonEast = true; Debug.Log("pass"); }
            if (listVector == south) { dungeonSouth = true; Debug.Log("pass"); }
            if (listVector == west) { dungeonWest = true; Debug.Log("pass"); }
        }
    }

    public static void CheckForLocalVillages()
    {
        villageNorth = false;
        villageEast = false;
        villageSouth = false;
        villageWest = false;
        Vector3 north = new Vector3(currentUnitPosition[0], currentUnitPosition[1] + 1);
        Vector3 east = new Vector3(currentUnitPosition[0] + 1, currentUnitPosition[1]);
        Vector3 south = new Vector3(currentUnitPosition[0], currentUnitPosition[1] - 1);
        Vector3 west = new Vector3(currentUnitPosition[0] - 1, currentUnitPosition[1]);
        foreach (Vector3 listVector in Villages.villagePositions.Keys)
        {
            if (listVector == north) { villageNorth = true; }
            if (listVector == east) { villageEast = true; }
            if (listVector == south) { villageSouth = true; }
            if (listVector == west) { villageWest = true; }
        }
    }

    public static void CheckForLocalMerchants()
    {
        merchantNorth = false;
        merchantEast = false;
        merchantSouth = false;
        merchantWest = false;
        Vector3 north = new Vector3(currentUnitPosition[0], currentUnitPosition[1] + 1);
        Vector3 east = new Vector3(currentUnitPosition[0] + 1, currentUnitPosition[1]);
        Vector3 south = new Vector3(currentUnitPosition[0], currentUnitPosition[1] - 1);
        Vector3 west = new Vector3(currentUnitPosition[0] - 1, currentUnitPosition[1]);
        foreach (Vector3 listVector in merchantPositions)
        {
            if (listVector == north) { merchantNorth = true; }
            if (listVector == east) { merchantEast = true; }
            if (listVector == south) { merchantSouth = true; }
            if (listVector == west) { merchantWest = true; }
        }
    }

    public static void CheckForLocalEmptySlots()
    {
        northEmpty = false;
        eastEmpty = false;
        southEmpty = false;
        westEmpty = false;
        Vector3 north = new Vector3(currentUnitPosition[0], currentUnitPosition[1] + 1);
        Vector3 east = new Vector3(currentUnitPosition[0] + 1, currentUnitPosition[1]);
        Vector3 south = new Vector3(currentUnitPosition[0], currentUnitPosition[1] - 1);
        Vector3 west = new Vector3(currentUnitPosition[0] - 1, currentUnitPosition[1]);
        foreach (Vector3 listVector in emptyBoardSlots)
        {
            if (listVector == north)
            {
                Debug.Log("North: " + north);
                Debug.Log("Current Unit Position: " + currentUnitPosition);
                northSlotPosition = north;
                northEmpty = true;
            }
            if (listVector == east)
            {
                eastSlotPosition = east;
                eastEmpty = true;
            }
            if (listVector == south)
            {
                southSlotPosition = south;
                southEmpty = true;
            }
            if (listVector == west)
            {
                westSlotPosition = west;
                westEmpty = true;
            }
        }
    }

    public static void GetLocalSlotPositions()
    {
        northSlotPosition = new Vector3(currentUnitPosition[0], currentUnitPosition[1] + 1);
        eastSlotPosition = new Vector3(currentUnitPosition[0] + 1, currentUnitPosition[1]);
        southSlotPosition = new Vector3(currentUnitPosition[0], currentUnitPosition[1] - 1);
        westSlotPosition = new Vector3(currentUnitPosition[0] - 1, currentUnitPosition[1]);
    }

    public static void CheckForBoardCrossroads(GUIManager gui)
    {
        /*crossroadsPosition = false;
        boardPosition = currentUnitPosition;
        foreach (Vector3 listVector in boardCrossroads)
        {
            if (listVector == boardPosition)
            {
                crossroadsPosition = true;
            }
        }*/
    }

    public static void UpdateEmptySlotPositions()
    {
        foreach (Vector3 listVector in boardPositions)
        {
            Vector3 vector3 = listVector;
            currentUnitPosition = listVector;
            CheckForLocalBoardPositions();
            bool ignorePosition = false;
            foreach (Vector3 exitVector in campExitPositions)
            {
                if (listVector == exitVector)
                {
                    ignorePosition = true;
                }
            }
            if (!ignorePosition)
            {
                if (!northPositionAvailable)
                {
                    vector3[1] += 1;
                    emptyBoardSlots.Add(vector3);
                    vector3 = listVector;
                }
                if (!eastPositionAvailable)
                {
                    vector3[0] += 1;
                    emptyBoardSlots.Add(vector3);
                    vector3 = listVector;

                }
                if (!southPositionAvailable)
                {
                    vector3[1] -= 1;
                    emptyBoardSlots.Add(vector3);
                    vector3 = listVector;
                }
                if (!westPositionAvailable)
                {
                    vector3[0] -= 1;
                    emptyBoardSlots.Add(vector3);
                    vector3 = listVector;
                }
            }
        }
    }
}