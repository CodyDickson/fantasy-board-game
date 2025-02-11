using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    public static Vector3 currentUnitPosition;
    public static Vector3 boardPosition;
    // Board connector positions, Local is within 1 tile
    public static List<Vector3> boardPositions = new List<Vector3>();
    public static List<Vector3> localBoardPositions = new List<Vector3>();
    // All empty slots
    public static List<Vector3> emptyBoardSlots = new List<Vector3>();
    // Empty slots within interaction range
    public static List<Vector3> potentialEmptySlots = new List<Vector3>();
    // The current player interaction range
    public static List<Vector3> currentInteractionRange = new List<Vector3>();
    // Movement possibilities within interaction range
    public static List<Vector3> possibleMove = new List<Vector3>();
    // Board connector positions that have multiple directions
    public static List<Vector3> crossroadPositions = new List<Vector3>();
    // Forest are the walls between zones
    public static List<Vector3> forestPositions = new List<Vector3>();
    // Everything within the forest (determines the size of the zone)
    public static List<Vector3> zonePositions = new List<Vector3>();
    // Exit positions between zones
    public static List<Vector3> exitPositions = new List<Vector3>();
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
    public static bool campExitPositionNorth = false;
    public static bool campExitPositionEast = false;
    public static bool campExitPositionSouth = false;
    public static bool campExitPositionWest = false;
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
            case 2: Graveyard.GenerateBoard(); break;
            case 3: Forest.GenerateBoard(); break;
        }
    }
    public static void SpawnPlayersInCamp()
    {
        Camp.CampSpawn();
        GameMain.playerInCamp = true;
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

    public static void CheckForBoardPositionsNearLocation(Vector3 position)
    {
        northPositionAvailable = false;
        eastPositionAvailable = false;
        southPositionAvailable = false;
        westPositionAvailable = false;
        Vector3 north = new Vector3(position[0], position[1] + 1);
        Vector3 east = new Vector3(position[0] + 1, position[1]);
        Vector3 south = new Vector3(position[0], position[1] - 1);
        Vector3 west = new Vector3(position[0] - 1, position[1]);
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
        boardPosition = currentUnitPosition;
        if (currentUnitDirection == "north" && northPositionAvailable) { boardPosition[1] += 1; }
        else if (currentUnitDirection == "east" && eastPositionAvailable) { boardPosition[0] += 1; }
        else if (currentUnitDirection == "south" && southPositionAvailable) { boardPosition[1] -= 1; }
        else if (currentUnitDirection == "west" && westPositionAvailable) { boardPosition[0] -= 1; }
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
    }

    public static void CheckForCampExits()
    {
        northPositionAvailable = campExitPositionNorth;
        eastPositionAvailable = campExitPositionEast;
        southPositionAvailable = campExitPositionSouth;
        westPositionAvailable = campExitPositionWest;
    }

    public static bool CheckForZonePositions(Vector3 position)
    {
        bool positionPresent = false;
        foreach (Vector3 zonePosition in zonePositions)
        {
            if (zonePosition == position)
            {
                positionPresent = true;
                continue;
            }
        }
        return positionPresent;
    }

    public static bool CheckForForestPositions(Vector3 position)
    {
        bool positionPresent = false;
        foreach (Vector3 forestPosition in forestPositions)
        {
            if (forestPosition == position)
            {
                positionPresent = true;
                continue;
            }
        }
        return positionPresent;
    }

    public static bool OnlyOnePathPossible()
    {
        CheckForLocalBoardPositions();
        bool onePath = false;
        for (int i = 1; i <= 4; i++)
        {
            int count = 0;
            if (northPositionAvailable == true) { currentUnitDirection = "north"; count++; }
            if (eastPositionAvailable == true) { currentUnitDirection = "east"; count++; }
            if (southPositionAvailable == true) { currentUnitDirection = "south"; count++; }
            if (westPositionAvailable == true) { currentUnitDirection = "west"; count++; }
            if (count < 2) { onePath = true; }
        }
        return onePath;
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
        foreach (Vector3 listVector in Dungeons.dungeonPositions)
        {
            if (listVector == north) { dungeonNorth = true; }
            if (listVector == east) { dungeonEast = true; }
            if (listVector == south) { dungeonSouth = true; }
            if (listVector == west) { dungeonWest = true; }
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
        foreach (Vector3 listVector in Villages.villagePositions)
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
        foreach (Vector3 listVector in Merchants.merchantPositions)
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
            if (listVector == north) { northEmpty = true; }
            if (listVector == east) { eastEmpty = true; }
            if (listVector == south) { southEmpty = true; }
            if (listVector == west) { westEmpty = true; }
        }
    }

    public static void CheckInteractionRange()
    {
        Vector3 position = currentUnitPosition;
        for (int z = 0, y = 0; y <= Player.interactionRange; y++)
        {
            for (int x = 0; x <= Player.interactionRange; x++, z++)
            {
                currentInteractionRange.Add(new Vector3Int((int)position[0] + x, (int)position[1] + y));
                currentInteractionRange.Add(new Vector3Int((int)position[0] - x, (int)position[1] + y));
                currentInteractionRange.Add(new Vector3Int((int)position[0] + x, (int)position[1] - y));
                currentInteractionRange.Add(new Vector3Int((int)position[0] - x, (int)position[1] - y));
            }
        }
    }

    public static void ShowLocalEmptySlots()
    {
        Tilemap tilemap = Store.tilemaps[3];
        Vector3 position = BoardManager.currentUnitPosition;
        for (int z = 0, y = 0; y <= Player.interactionRange; y++)
        {
            for (int x = 0; x <= Player.interactionRange; x++, z++)
            {
                potentialEmptySlots.Add(new Vector3Int((int)position[0] + x, (int)position[1] + y));
                potentialEmptySlots.Add(new Vector3Int((int)position[0] - x, (int)position[1] + y));
                potentialEmptySlots.Add(new Vector3Int((int)position[0] + x, (int)position[1] - y));
                potentialEmptySlots.Add(new Vector3Int((int)position[0] - x, (int)position[1] - y));
            }
        }
        foreach (Vector3 potential in potentialEmptySlots)
        {
            foreach (Vector3 emptySlot in emptyBoardSlots)
            {
                if (potential == emptySlot)
                {
                    tilemap.SetTile(new Vector3Int((int)emptySlot[0], (int)emptySlot[1]), Store.objectTiles[2]);
                }
            }
        }
    }

    public static void ShowMovementPossibilities()
    {
        Tilemap tilemap = Store.tilemaps[3];
        Vector3 position = currentUnitPosition;
        int totalRange = Dice.RollDice();
        foreach (Vector3 boardPosition in boardPositions)
        {
            for (int z = 0, y = 0; y <= totalRange; y++)
            {
                for (int x = 0; x <= totalRange; x++, z++)
                {
                    Vector3 checkPosition = new Vector3Int((int)position[0] + x, (int)position[1] + y);
                    if (checkPosition.Equals(boardPosition))
                    {
                        possibleMove.Add(checkPosition);
                    }
                    checkPosition = new Vector3Int((int)position[0] - x, (int)position[1] + y);
                    if (checkPosition.Equals(boardPosition))
                    {
                        possibleMove.Add(checkPosition);
                    }
                    checkPosition = new Vector3Int((int)position[0] + x, (int)position[1] - y);
                    if (checkPosition.Equals(boardPosition))
                    {
                        possibleMove.Add(checkPosition);
                    }
                    checkPosition = new Vector3Int((int)position[0] - x, (int)position[1] - y);
                    if (checkPosition.Equals(boardPosition))
                    {
                        possibleMove.Add(checkPosition);
                    }
                }
            }
        }
        foreach (Vector3 move in possibleMove)
        {
            tilemap.SetTile(new Vector3Int((int)move[0], (int)move[1]), Store.objectTiles[2]);
        }
    }

    public static void ClearEmptySlots()
    {
        Tilemap tilemap = Store.tilemaps[3];
        foreach (Vector3 emptySlot in emptyBoardSlots)
        {
            tilemap.SetTile(new Vector3Int((int)emptySlot[0], (int)emptySlot[1]), null);
        }
    }

    public static void GetLocalSlotPositions()
    {
        northSlotPosition = new Vector3(currentUnitPosition[0], currentUnitPosition[1] + 1);
        eastSlotPosition = new Vector3(currentUnitPosition[0] + 1, currentUnitPosition[1]);
        southSlotPosition = new Vector3(currentUnitPosition[0], currentUnitPosition[1] - 1);
        westSlotPosition = new Vector3(currentUnitPosition[0] - 1, currentUnitPosition[1]);
    }

    public static void CheckForCrossroads()
    {
        crossroadsPosition = false;
        boardPosition = currentUnitPosition;
        foreach (Vector3 listVector in crossroadPositions)
        {
            if (listVector == boardPosition)
            {
                crossroadsPosition = true;
            }
        }
    }

    public static void UpdateEmptySlotPositions()
    {
        foreach (Vector3 listVector in boardPositions)
        {
            Vector3 vector3 = listVector;
            currentUnitPosition = listVector;
            CheckForLocalBoardPositions();
            bool ignorePosition = false;
            foreach (Vector3 exitVector in exitPositions)
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
        emptyBoardSlots = emptyBoardSlots.Distinct().ToList();
    }

    public static void RemoveEmptySlot(Vector3 position)
    {
        emptyBoardSlots.Remove(position);
    }
}