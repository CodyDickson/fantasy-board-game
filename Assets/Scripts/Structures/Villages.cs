using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Villages : MonoBehaviour
{
    //
    public int villageCost = 100;
    public static int villageBuildCost = 100;
    public static int villageOwner;
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

    void OnClickUpgradeVillage()
    {
        UpgradeVillageWhenNearIt();
    }

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

    public static void GrowVillage(int currentVillage)
    {
        if (GameMain.currentPlayer == 1)
        {
            foreach (int villageToGrow in World.boardPlayerOneVillagePositions.Values)
            {
                if (villageToGrow == currentVillage)
                {
                    // Vector3 villageLocation = World.boardPlayerOneVillagePositions[villageToGrow];
                }
            }
        }
    }

    public static void UpgradeVillageWhenNearIt()
    {
        Vector3 north = new Vector3(World.currentUnitPosition[0], World.currentUnitPosition[1] + 1);
        Vector3 east = new Vector3(World.currentUnitPosition[0] + 1, World.currentUnitPosition[1]);
        Vector3 south = new Vector3(World.currentUnitPosition[0], World.currentUnitPosition[1] - 1);
        Vector3 west = new Vector3(World.currentUnitPosition[0] - 1, World.currentUnitPosition[1]);
        foreach (Vector3 listVector in World.boardPlayerOneVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                World.boardPlayerOneVillagePositions[listVector] += 1;
            }
        }
        foreach (Vector3 listVector in World.boardPlayerTwoVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                World.boardPlayerTwoVillagePositions[listVector] += 1;
            }
        }
        foreach (Vector3 listVector in World.boardPlayerThreeVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                World.boardPlayerThreeVillagePositions[listVector] += 1;
            }
        }
        foreach (Vector3 listVector in World.boardPlayerFourVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                World.boardPlayerFourVillagePositions[listVector] += 1;
            }
        }
    }
}