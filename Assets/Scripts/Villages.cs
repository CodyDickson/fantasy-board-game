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
    private bool villagesGUIEnabled = false;
    public static int currentVillage;
    // Settings //
    public static int villageGrowth = 3;
    public static int villageGoldPerTurnLevelOne = 10;
    public static int villageGoldPerTurnLevelTwo = 25;
    public static int villageGoldPerTurnLevelThree = 50;
    public static int villageTollLevelOne = 25;
    public static int villageTollLevelTwo = 50;
    public static int villageTollLevelThree = 100;
    // Village Tracking //
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

    void Start()
    {
        upgradeVillage.onClick.AddListener(OnClickUpgradeVillage);
        closeVillageWindow.onClick.AddListener(OnClickCloseVillageWindow);
        upgradeVillage.gameObject.SetActive(false);
        closeVillageWindow.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && GameMain.secondaryButtonEnabled)
        {
            Villages.BuildVillage(tilemapStructures, villageRed, villageBlue, villageGreen, villagePurple, villageWhite);
        }
        // button to cycle through all villages of the active player
        if (villagesGUIEnabled)
        {
            closeVillageWindow.gameObject.SetActive(true);
        }
        // On Mouse Click of a Village
        if (GameMain.GUIEnabled)
        {
            // UpdateVillageInfo();
        }
    }
    
    void UpdateVillageInfo()
    {
        // When landing on a village OR clicking on a village
        // Updates the LowerGUI, image of the color village on the left and "x turns to growth", "x gold per turn", "x gold per toll"
        // Lower button is "Upgrade" (if available), for a reduced fee if the player has landed on it
        // Lower button is "Pay Toll" if the player landed on an opposing village
    }

    void OnClickUpgradeVillage()
    {
        UpgradeVillageWhenNearIt();
    }

    void OnClickCloseVillageWindow()
    {
        villagesGUIEnabled = false;
        GameMain.GUIEnabled = true;
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
            case "red": tilemap.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), villageRed); break;
            case "blue": tilemap.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), villageBlue); break;
            case "green": tilemap.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), villageGreen); break;
            case "purple": tilemap.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), villagePurple); break;
            case "white": tilemap.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), villageWhite); break;
        }
        for (int i = 0; i < World.boardEmptySlotPositions.Count; i++)
        {
            if (World.boardPosition == World.boardEmptySlotPositions[i])
            {
                World.boardEmptySlotPositions.RemoveAt(i);
            }
        }
        switch (GameMain.currentPlayer)
        {
            case 1: playerOneTotalVillages += 1; playerOneVillageGrowth.Add(playerOneTotalVillages, villageGrowth); playerOneVillageGoldPerTurn.Add(playerOneTotalVillages, villageGoldPerTurnLevelOne); playerOneVillageTolls.Add(playerOneTotalVillages, villageTollLevelOne); World.boardPlayerOneVillagePositions.Add(World.boardPosition, playerOneTotalVillages); break;
            case 2: playerTwoTotalVillages += 1; playerTwoVillageGrowth.Add(playerTwoTotalVillages, villageGrowth); playerTwoVillageGoldPerTurn.Add(playerTwoTotalVillages, villageGoldPerTurnLevelOne); playerTwoVillageTolls.Add(playerTwoTotalVillages, villageTollLevelOne); World.boardPlayerTwoVillagePositions.Add(World.boardPosition, playerTwoTotalVillages); break;
            case 3: playerThreeTotalVillages += 1; playerThreeVillageGrowth.Add(playerThreeTotalVillages, villageGrowth); playerThreeVillageGoldPerTurn.Add(playerThreeTotalVillages, villageGoldPerTurnLevelOne); playerThreeVillageTolls.Add(playerThreeTotalVillages, villageTollLevelOne); World.boardPlayerThreeVillagePositions.Add(World.boardPosition, playerThreeTotalVillages); break;
            case 4: playerFourTotalVillages += 1; playerFourVillageGrowth.Add(playerFourTotalVillages, villageGrowth); playerFourVillageGoldPerTurn.Add(playerFourTotalVillages, villageGoldPerTurnLevelOne); playerFourVillageTolls.Add(playerFourTotalVillages, villageTollLevelOne); World.boardPlayerFourVillagePositions.Add(World.boardPosition, playerFourTotalVillages); break;
        }
    }

    public static void PlayerLandedOnOpposingVillage()
    {
        Vector3 north = new Vector3(World.currentUnitPosition[0], World.currentUnitPosition[1] + 1);
        Vector3 east = new Vector3(World.currentUnitPosition[0] + 1, World.currentUnitPosition[1]);
        Vector3 south = new Vector3(World.currentUnitPosition[0], World.currentUnitPosition[1] - 1);
        Vector3 west = new Vector3(World.currentUnitPosition[0] - 1, World.currentUnitPosition[1]);
        int villageNumber = 1;
        int villageCost = 50;
        foreach (Vector3 listVector in World.boardPlayerOneVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                villageNumber = World.boardPlayerOneVillagePositions[listVector];
                villageCost = playerOneVillageTolls[villageNumber];
            }
        }
        foreach (Vector3 listVector in World.boardPlayerTwoVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                villageNumber = World.boardPlayerTwoVillagePositions[listVector];
                villageCost = playerTwoVillageTolls[villageNumber];
            }
        }
        foreach (Vector3 listVector in World.boardPlayerThreeVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                villageNumber = World.boardPlayerThreeVillagePositions[listVector];
                villageCost = playerThreeVillageTolls[villageNumber];
            }
        }
        foreach (Vector3 listVector in World.boardPlayerFourVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                villageNumber = World.boardPlayerFourVillagePositions[listVector];
                villageCost = playerFourVillageTolls[villageNumber];
            }
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

    public static void GrowVillage(int currentVillage)
    {
        //
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

    public static void UpgradeVillage()
    {
        //
    }
}