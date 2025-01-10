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
    public static int villageUpgradeCost = 100;
    public static int currentVillage;
    // Settings //
    public static int villageGrowth = 3;
    // Tracking //
    public static List<Vector3> villagePositions = new List<Vector3>();

    public static void BuildVillage(string direction)
    {
        Vector3 position = BoardManager.currentUnitPosition;
        Tilemap structures = Store.tilemaps[3];
        Tile village = Store.villageTiles[GameMain.playerVillage];
        BoardManager.GetLocalSlotPositions();
        if (direction == "north") { position = BoardManager.northSlotPosition; }
        if (direction == "east") { position = BoardManager.eastSlotPosition; }
        if (direction == "south") { position = BoardManager.southSlotPosition; }
        if (direction == "west") { position = BoardManager.westSlotPosition; }
        structures.SetTile(new Vector3Int((int)position[0], (int)position[1]), village);
        villagePositions.Add(position);
        BoardManager.RemoveEmptySlot(position);
        GameMain.playerGold -= villageBuildCost;
        GUIManager.UpdatePlayerGUI();
        totalVillageGoldPerTurn += 25;
        InfoGUI.updateInfoGUI = true;
    }

    public static void UpgradeVillage()
    {
        GameMain.playerGold -= villageUpgradeCost;
        totalVillageGoldPerTurn += 50;
    }
}