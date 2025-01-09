using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Villages : MonoBehaviour
{
    public static int totalVillageGoldPerTurn = 0;

    //
    public int villageCost = 100;
    public static int villageBuildCost = 100;
    public static int currentVillage;
    // Settings //
    public static int villageGrowth = 3;
    public static int villageGoldPerTurnLevelOne = 10;
    public static int villageGoldPerTurnLevelTwo = 25;
    public static int villageGoldPerTurnLevelThree = 50;
    public static int villageTollLevelOne = 25;
    public static int villageTollLevelTwo = 50;
    public static int villageTollLevelThree = 100;
    // Tracking //
    public static Dictionary<Vector3, int> villagePositions = new Dictionary<Vector3, int>();
    //
    public static int playerOneTotalVillages = 0;
    public static Dictionary<int, int> playerOneVillageGrowth = new Dictionary<int, int>();
    public static Dictionary<int, int> playerOneVillageGoldPerTurn = new Dictionary<int, int>();
    public static Dictionary<int, int> playerOneVillageTolls = new Dictionary<int, int>();
    public static int playerTwoTotalVillages = 0;
    public static Dictionary<int, int> playerTwoVillageGrowth = new Dictionary<int, int>();
    public static Dictionary<int, int> playerTwoVillageGoldPerTurn = new Dictionary<int, int>();
    public static Dictionary<int, int> playerTwoVillageTolls = new Dictionary<int, int>();
    public static int playerThreeTotalVillages = 0;
    public static Dictionary<int, int> playerThreeVillageGrowth = new Dictionary<int, int>();
    public static Dictionary<int, int> playerThreeVillageGoldPerTurn = new Dictionary<int, int>();
    public static Dictionary<int, int> playerThreeVillageTolls = new Dictionary<int, int>();
    public static int playerFourTotalVillages = 0;
    public static Dictionary<int, int> playerFourVillageGrowth = new Dictionary<int, int>();
    public static Dictionary<int, int> playerFourVillageGoldPerTurn = new Dictionary<int, int>();
    public static Dictionary<int, int> playerFourVillageTolls = new Dictionary<int, int>();

    public static void BuildVillage(string direction)
    {
        int player = GameMain.currentPlayer;
        Vector3 position = BoardManager.currentUnitPosition;
        Tilemap structures = Store.tilemaps[3];
        Tile village = Store.villageTiles[player];
        BoardManager.GetLocalSlotPositions();
        if (direction == "north") { position = BoardManager.northSlotPosition; }
        if (direction == "east") { position = BoardManager.eastSlotPosition; }
        if (direction == "south") { position = BoardManager.southSlotPosition; }
        if (direction == "west") { position = BoardManager.westSlotPosition; }
        structures.SetTile(new Vector3Int((int)position[0], (int)position[1]), village);
        villagePositions.Add(position, player);
        GameMain.playerGold[player] -= villageBuildCost;
    }

    public static void UpgradeVillage()
    {
        //
    }

    public static void PayToll()
    {
        //
    }

    public static int FindVillageOwner(string direction)
    {
        int villageOwner = 1;
        Vector3 position = BoardManager.currentUnitPosition;
        BoardManager.GetLocalSlotPositions();
        if (direction == "north") { position = BoardManager.northSlotPosition; }
        if (direction == "east") { position = BoardManager.eastSlotPosition; }
        if (direction == "south") { position = BoardManager.southSlotPosition; }
        if (direction == "west") { position = BoardManager.westSlotPosition; }
        foreach (Vector3 village in villagePositions.Keys)
        {
            if (village == position)
            {
                villageOwner = villagePositions[village];
            }
        }
        return villageOwner;
    }
}