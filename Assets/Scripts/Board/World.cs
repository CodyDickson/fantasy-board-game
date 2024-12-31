using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class World : MonoBehaviour
{
    // Map Settings //
    public static int currentUnitPositionOnBoard = 0;
    public static int previousUnitAvatar = 0;
    public static int newUnitPosition = 0;
    public static bool playerIsMoving;
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
    public static bool villageNearby = false;
    public static bool dungeonNearby = false;
    public static bool merchantNearby = false;
    //
    public static Vector3 boardPosition;
    public static List<Vector3> boardPositions = new List<Vector3>();
    public static List<Vector3> localBoardPositions = new List<Vector3>();
    public static List<Vector3> boardCrossroads = new List<Vector3>();
    // 1: Weapons, 2: Oddities, 3: Rarities, 4: Consumables
    public static Dictionary<Vector3, int> boardMerchantPositions = new Dictionary<Vector3, int>();
    //
    public static List<Vector3> boardTollPositions = new List<Vector3>();
    public static List<Vector3> boardCampPositions = new List<Vector3>();
    public static List<Vector3> boardImpDungeonPositions = new List<Vector3>();
    public static List<Vector3> boardBasiliskDungeonPositions = new List<Vector3>();
    public static List<Vector3> boardSkeletonDungeonPositions = new List<Vector3>();
    public static List<Vector3> boardGhostDungeonPositions = new List<Vector3>();
    public static Dictionary<Vector3, int> boardPlayerOneVillagePositions = new Dictionary<Vector3, int>();
    public static Dictionary<Vector3, int> boardPlayerTwoVillagePositions = new Dictionary<Vector3, int>();
    public static Dictionary<Vector3, int> boardPlayerThreeVillagePositions = new Dictionary<Vector3, int>();
    public static Dictionary<Vector3, int> boardPlayerFourVillagePositions = new Dictionary<Vector3, int>();
    public static Dictionary<Vector3, int> boardDungeonPositions = new Dictionary<Vector3, int>();
    public static List<Vector3> boardEmptySlotPositions = new List<Vector3>();
    //
    public static List<Vector3> boardSlotPositions = new List<Vector3>();
    public static Vector3 currentUnitPosition;
    public static Vector3 playerOnePosition;
    public static Vector3 playerTwoPosition;
    public static Vector3 playerThreePosition;
    public static Vector3 playerFourPosition;
    public static Dictionary<int, string> boardClockPosition = new Dictionary<int, string>();
    public static Dictionary<int, string> boardLoopDirection = new Dictionary<int, string>();
    public static Dictionary<int, string> boardStructures = new Dictionary<int, string>();
    public static Dictionary<int, string> boardUnits = new Dictionary<int, string>();
    public static int boardLength;
    // Tiles and Tilemaps //
    [SerializeField] public Tile playerRed;
    [SerializeField] public Tile playerBlue;
    [SerializeField] public Tile playerGreen;
    [SerializeField] public Tile playerPurple;
    [SerializeField] public Tile playerWhite;
    [SerializeField] public Tile monsterImp;
    [SerializeField] public Tile monsterBasilisk;
    [SerializeField] public Tile monsterEliteRampagingElephant;
    [SerializeField] public Tile monsterMimic;
    [SerializeField] public Tile monsterSkeleton;
    [SerializeField] public Tile monsterGhost;
    [SerializeField] public Tile monsterEliteSkeletonAmalgamation;
    [SerializeField] public Tile chest;
    [SerializeField] public Tile oddity;
    [SerializeField] public Tile dungeon;
    [SerializeField] public Tile ground, tree;
    [SerializeField] public Tile camp, bcHorizontal, bcThreeDown, bcVertical, bcThreeUp, bcThreeLeft, bcThreeRight, bcTopRightCorner, bcBottomLeftCorner, bcBottomRightCorner, bcTopLeftCorner, emptySlot;
    [SerializeField] public Tilemap tilemapTerrain, tilemapTerrainObjects, tilemapStructures, tilemapUnits, tilemapBoardConnectors;
    [SerializeField] public Tilemap terrain, terrainObjects;
    // Time //
    private float tempCounter2 = 0f;
    private float counter2 = 0.5f;
    // Other //
    public static string currentPlayerColor = "red";
    public static bool crossroadsPosition = false;
    public static int movesRemaining;
    public GUI gui;
    [SerializeField] public Tile fogTile;
    [SerializeField] public Tilemap fog;

    void Start()
    {
        FillEmptySlots(tilemapBoardConnectors, emptySlot);
        TurnManager.TurnProgressionHandler(tilemapStructures);
    }

    void Update()
    {

        if (playerIsMoving && !GameMain.currentPlayerInCamp)
        {
            if (tempCounter2 <= 0f)
            {
                if (movesRemaining > 0)
                {
                    CheckForLocalBoardPositions();
                    DetermineNextBoardPosition(tilemapUnits);
                    // gui.EnableArrows(false);
                    CheckForBoardCrossroads(gui);
                    Debug.Log("Current Unit Position: " + currentUnitPosition);
                    switch (GameMain.currentPlayer)
                    {
                        case 1: playerOnePosition = currentUnitPosition; break;
                        case 2: playerTwoPosition = currentUnitPosition; break;
                        case 3: playerThreePosition = currentUnitPosition; break;
                        case 4: playerFourPosition = currentUnitPosition; break;
                    }
                    movesRemaining -= 1;
                    if (crossroadsPosition == true)
                    {
                        CheckForLocalBoardPositions();
                        GUI.enableArrowButtons = true;;
                        playerIsMoving = false;
                    }
                }
                if (movesRemaining == 0)
                {
                    GUI.enableArrowButtons = false;
                    GUI.ToggleEndTurnButton(false);
                    playerIsMoving = false;
                    CheckForLocalEmptySlots();
                    if (northEmpty || eastEmpty || southEmpty || westEmpty)
                    {
                        if (GUI.primaryButtonAssignedTo == "")
                        {
                            GUI.TogglePrimaryButton(true, "build");
                            GUI.ToggleEndTurnButton(true);
                        }
                    }
                    else
                    {
                        CheckForLocalVillages();
                        if (villageNearby && GameMain.currentPlayer != Villages.villageOwner)
                        {
                            if (GUI.primaryButtonAssignedTo == "")
                            {
                                GUI.TogglePrimaryButton(true, "payToll");
                                GUI.ToggleEndTurnButton(false);
                            }
                            else
                            {
                                GUI.ToggleSecondaryButton(true, "payToll");
                                GUI.ToggleEndTurnButton(false);
                            }
                        }
                        else if (villageNearby && GameMain.currentPlayer == Villages.villageOwner)
                        {
                            if (GUI.primaryButtonAssignedTo == "")
                            {
                                GUI.TogglePrimaryButton(true, "upgrade");
                                GUI.ToggleEndTurnButton(true);
                            }
                            else
                            {
                                GUI.ToggleSecondaryButton(true, "upgrade");
                                GUI.ToggleEndTurnButton(true);
                            }
                        }
                            CheckForLocalDungeons();
                        if (Dungeons.dungeonType != "")
                        {
                            switch (Dungeons.dungeonType)
                            {
                                case "imp": break;
                                case "basilisk": break;
                            }
                        }
                    }
                }
                tempCounter2 = counter2;
            }
            else
            {
                tempCounter2 -= Time.deltaTime;
            }
        }
    }

    public static void MoveUnit()
    {
        GameMain.RollDice();
        GUI.enablePrimaryButton = false;
        GUI.primaryButtonAssignedTo = "";
        movesRemaining = GameMain.diceOneResult + GameMain.diceTwoResult + GameMain.diceThreeResult;
        Debug.Log("Moves Remaining: " + movesRemaining);
        CheckForLocalBoardPositions();
    }

    public static void CheckForBoardCrossroads(GUI gui)
    {
        crossroadsPosition = false;
        boardPosition = currentUnitPosition;
        foreach (Vector3 listVector in boardCrossroads)
        {
            if (listVector == boardPosition)
            {
                crossroadsPosition = true;
            }
        }
    }

    public static void DetermineNextBoardPosition(Tilemap tilemapUnits)
    {
        boardPosition = currentUnitPosition;
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

    void CheckForLocalDungeons()
    {
        dungeonNearby = false;
        Vector3 north = new Vector3(currentUnitPosition[0], currentUnitPosition[1] + 1);
        Vector3 east = new Vector3(currentUnitPosition[0] + 1, currentUnitPosition[1]);
        Vector3 south = new Vector3(currentUnitPosition[0], currentUnitPosition[1] - 1);
        Vector3 west = new Vector3(currentUnitPosition[0] - 1, currentUnitPosition[1]);
        foreach (Vector3 listVector in boardDungeonPositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                switch (boardDungeonPositions[listVector])
                {
                    case 1: Dungeons.dungeonType = "imp"; break;
                    case 2: Dungeons.dungeonType = "basilisk"; break;
                }
            }
        }
    }

    public static void CheckForLocalVillages()
    {
        villageNearby = false;
        Vector3 north = new Vector3(currentUnitPosition[0], currentUnitPosition[1] + 1);
        Vector3 east = new Vector3(currentUnitPosition[0] + 1, currentUnitPosition[1]);
        Vector3 south = new Vector3(currentUnitPosition[0], currentUnitPosition[1] - 1);
        Vector3 west = new Vector3(currentUnitPosition[0] - 1, currentUnitPosition[1]);
        foreach (Vector3 listVector in boardPlayerOneVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                Villages.villageOwner = 1;
                villageNearby = true;
                Villages.currentVillage = boardPlayerOneVillagePositions[listVector];
            }
        }
        foreach (Vector3 listVector in boardPlayerTwoVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                Villages.villageOwner = 2;
                villageNearby = true;
                Villages.currentVillage = boardPlayerTwoVillagePositions[listVector];
            }
        }
        foreach (Vector3 listVector in boardPlayerThreeVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                Villages.villageOwner = 3;
                villageNearby = true;
                Villages.currentVillage = boardPlayerThreeVillagePositions[listVector];
            }
        }
        foreach (Vector3 listVector in boardPlayerFourVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                Villages.villageOwner = 4;
                villageNearby = true;
                Villages.currentVillage = boardPlayerFourVillagePositions[listVector];
            }
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
        foreach (Vector3 listVector in boardEmptySlotPositions)
        {
            if (listVector == north)
            {
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

    public static void FillEmptySlots(Tilemap tilemapBoardConnectors, Tile emptySlot)
    {
        foreach (Vector3 listVector in World.boardEmptySlotPositions)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)listVector[0], (int)listVector[1]), emptySlot);
        }
    }
}