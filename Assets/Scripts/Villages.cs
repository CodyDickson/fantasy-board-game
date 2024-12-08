using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Villages : MonoBehaviour
{
    //
    public int villageCost = 100;
    // Tiles and Tilemaps //
    [SerializeField] public Tile villageRed;
    [SerializeField] public Tile villageBlue;
    [SerializeField] public Tile villageGreen;
    [SerializeField] public Tile villagePurple;
    [SerializeField] public Tile villageWhite;
    [SerializeField] public Tilemap tilemapStructures;
    public static int villageOwner;
    public Button upgradeVillage;
    public Button closeVillageWindow;

    void Start()
    {
        upgradeVillage.onClick.AddListener(OnClickUpgradeVillage);
        closeVillageWindow.onClick.AddListener(OnClickCloseVillageWindow);
        upgradeVillage.gameObject.SetActive(false);
        closeVillageWindow.gameObject.SetActive(false);
    }

    void Update()
    {

    }

    void OnClickUpgradeVillage()
    {
        //
    }

    void OnClickCloseVillageWindow()
    {
        //
    }

    public static void BuildVillage(Tilemap tilemap, Tile villageRed, Tile villageBlue, Tile villageGreen, Tile villagePurple, Tile villageWhite)
    {
        GameMain.secondaryButtonEnabled = false;
        World.CheckForLocalEmptySlots();
        if (World.northEmpty)
        {
            World.boardPosition = World.northSlotPosition;
        }
        else if (World.eastEmpty)
        {
            World.boardPosition = World.eastSlotPosition;
        }
        else if (World.southEmpty)
        {
            World.boardPosition = World.southSlotPosition;
        }
        else if (World.westEmpty)
        {
            World.boardPosition = World.westSlotPosition;
        }
        switch (World.currentPlayerColor)
        {
            case "red": tilemap.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), villageRed); Debug.Log(World.boardPosition); break;
            case "blue": tilemap.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), villageBlue); break;
            case "green": tilemap.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), villageGreen); break;
            case "purple": tilemap.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), villagePurple); break;
            case "white": tilemap.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), villageWhite); break;
        }
        switch (GameMain.currentPlayer)
        {
            case 1: World.boardPlayerOneVillagePositions.Add(World.boardPosition, 1); break;
            case 2: World.boardPlayerTwoVillagePositions.Add(World.boardPosition, 1); break;
            case 3: World.boardPlayerThreeVillagePositions.Add(World.boardPosition, 1); break;
            case 4: World.boardPlayerFourVillagePositions.Add(World.boardPosition, 1); break;
        }
    }

    public static void PlayerLandedOnOpposingVillage()
    {
        Vector3 north = new Vector3(World.currentUnitPosition[0], World.currentUnitPosition[1] + 1);
        Vector3 east = new Vector3(World.currentUnitPosition[0] + 1, World.currentUnitPosition[1]);
        Vector3 south = new Vector3(World.currentUnitPosition[0], World.currentUnitPosition[1] - 1);
        Vector3 west = new Vector3(World.currentUnitPosition[0] - 1, World.currentUnitPosition[1]);
        int villageLevel = 1;
        int villageCost = 50;
        foreach (Vector3 listVector in World.boardPlayerOneVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                villageLevel = World.boardPlayerOneVillagePositions[listVector];
            }
        }
        foreach (Vector3 listVector in World.boardPlayerTwoVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                villageLevel = World.boardPlayerTwoVillagePositions[listVector];
            }
        }
        foreach (Vector3 listVector in World.boardPlayerThreeVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                villageLevel = World.boardPlayerThreeVillagePositions[listVector];
            }
        }
        foreach (Vector3 listVector in World.boardPlayerFourVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                villageLevel = World.boardPlayerFourVillagePositions[listVector];
            }
        }
        switch (villageLevel)
        {
            case 1: villageCost = 50; break;
            case 2: villageCost = 100; break;
            case 3: villageCost = 150; break;
        }
        switch (GameMain.currentPlayer)
        {
            case 1: GameMain.playerOneGold -= villageCost; break;
            case 2: GameMain.playerTwoGold -= villageCost; break;
            case 3: GameMain.playerThreeGold -= villageCost; break;
            case 4: GameMain.playerFourGold -= villageCost; break;
        }
        switch (villageOwner)
        {
            case 1: GameMain.playerOneGold += villageCost; break;
            case 2: GameMain.playerTwoGold += villageCost; break;
            case 3: GameMain.playerThreeGold += villageCost; break;
            case 4: GameMain.playerFourGold += villageCost; break;
        }
    }

    public static void UpgradeVillage()
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