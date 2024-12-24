using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class World : MonoBehaviour
{
    // Map Settings //
    public static bool mapSizeIsRandom = false;
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
    //
    public static Vector3 boardPosition;
    public static List<Vector3> boardPositions = new List<Vector3>();
    public static List<Vector3> localBoardPositions = new List<Vector3>();
    public static List<Vector3> boardCrossroads = new List<Vector3>();
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
    [SerializeField] public Tile villageRed;
    [SerializeField] public Tile villageBlue;
    [SerializeField] public Tile villageGreen;
    [SerializeField] public Tile villagePurple;
    [SerializeField] public Tile villageWhite;
    [SerializeField] public Tile grass;
    [SerializeField] public Tile grassOne;
    [SerializeField] public Tile grassTwo;
    [SerializeField] public Tile grassThree;
    [SerializeField] public Tile graveyardOne;
    [SerializeField] public Tile graveyardTwo;
    [SerializeField] public Tile graveyardThree;
    [SerializeField] public Tile oceanOne;
    [SerializeField] public Tile oceanTwo;
    [SerializeField] public Tile oceanThree;
    [SerializeField] public Tile camp, bcHorizontal, bcThreeDown, bcVertical, bcThreeUp, bcThreeLeft, bcThreeRight, bcTopRightCorner, bcBottomLeftCorner, bcBottomRightCorner, bcTopLeftCorner, emptySlot;
    [SerializeField] public Tilemap tilemapTerrain;
    [SerializeField] public Tilemap tilemapStructures;
    [SerializeField] public Tilemap tilemapBoardConnectors;
    [SerializeField] public Tilemap tilemapUnits;
    // Time //
    private float counter = 0.01f;
    private float tempCounter = 0f;
    private float tempCounter2 = 0f;
    private float counter2 = 0.5f;
    // Other //
    public static string currentPlayerColor = "red";
    public static bool playerCurrentlyInCamp;
    public static bool crossroadsPosition = false;
    public static int movesRemaining;
    public GUI gui;

    void Start()
    {
        Camp.GenerateCamp(tilemapBoardConnectors, camp, bcHorizontal, bcThreeDown, bcVertical, bcThreeUp, bcThreeLeft, bcThreeRight, bcTopRightCorner, bcBottomLeftCorner, bcBottomRightCorner, bcTopLeftCorner);
        Board.GenerateGameBoard(tilemapBoardConnectors, camp, bcHorizontal, bcThreeDown, bcVertical, bcThreeUp, bcThreeLeft, bcThreeRight, bcTopRightCorner, bcBottomLeftCorner, bcBottomRightCorner, bcTopLeftCorner);
        Terrain.GenerateGrasslandsTerrain(tilemapTerrain, grass);
        Camp.SpawnActivePlayerInCamp();
        FillEmptySlots(tilemapBoardConnectors, emptySlot);
        TurnManager.SetInitialTurnOrder();
    }

    void Update()
    {
        if (tempCounter <= 0f)
        {
            if (GameMain.playerOneIsActive)
            {
                switch (GameMain.playerOneColor)
                {
                    case "red": tilemapUnits.SetTile(new Vector3Int((int)playerOnePosition[0], (int)playerOnePosition[1]), playerRed); break;
                    case "blue": tilemapUnits.SetTile(new Vector3Int((int)playerOnePosition[0], (int)playerOnePosition[1]), playerBlue); break;
                    case "green": tilemapUnits.SetTile(new Vector3Int((int)playerOnePosition[0], (int)playerOnePosition[1]), playerGreen); break;
                    case "purple": tilemapUnits.SetTile(new Vector3Int((int)playerOnePosition[0], (int)playerOnePosition[1]), playerPurple); break;
                    case "white": tilemapUnits.SetTile(new Vector3Int((int)playerOnePosition[0], (int)playerOnePosition[1]), playerWhite); break;
                }
            }
            if (GameMain.playerTwoIsActive)
            {
                boardPosition = playerTwoPosition;
                switch (GameMain.playerTwoColor)
                {
                    case "red": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerRed); break;
                    case "blue": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerBlue); break;
                    case "green": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerGreen); break;
                    case "purple": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerPurple); break;
                    case "white": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerWhite); break;
                }
            }
            if (GameMain.playerThreeIsActive)
            {
                boardPosition = playerThreePosition;
                switch (GameMain.playerThreeColor)
                {
                    case "red": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerRed); break;
                    case "blue": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerBlue); break;
                    case "green": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerGreen); break;
                    case "purple": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerPurple); break;
                    case "white": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerWhite); break;
                }
            }
            if (GameMain.playerFourIsActive)
            {
                boardPosition = playerFourPosition;
                switch (GameMain.playerFourColor)
                {
                    case "red": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerRed); break;
                    case "blue": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerBlue); break;
                    case "green": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerGreen); break;
                    case "purple": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerPurple); break;
                    case "white": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerWhite); break;
                }
            }
            tempCounter = counter;
        }
        else
        {
            tempCounter -= Time.deltaTime;
        }
        if (playerIsMoving && GameMain.currentPlayerInCamp)
        {
            tilemapUnits.SetTile(new Vector3Int((int)currentUnitPosition[0], (int)currentUnitPosition[1]), null);
            GameMain.currentPlayerInCamp = false;
            playerIsMoving = false;
            if (currentUnitDirection == "north")
            {
                currentUnitPosition = boardCampPositions[0];
                switch (GameMain.currentPlayer)
                {
                    case 1: playerOnePosition = currentUnitPosition; break;
                    case 2: playerTwoPosition = currentUnitPosition; break;
                    case 3: playerThreePosition = currentUnitPosition; break;
                    case 4: playerFourPosition = currentUnitPosition; break;
                }
            }
            if (currentUnitDirection == "east")
            {
                currentUnitPosition = boardCampPositions[1];
                switch (GameMain.currentPlayer)
                {
                    case 1: playerOnePosition = currentUnitPosition; break;
                    case 2: playerTwoPosition = currentUnitPosition; break;
                    case 3: playerThreePosition = currentUnitPosition; break;
                    case 4: playerFourPosition = currentUnitPosition; break;
                }
            }
            if (currentUnitDirection == "south")
            {
                currentUnitPosition = boardCampPositions[2];
                switch (GameMain.currentPlayer)
                {
                    case 1: playerOnePosition = currentUnitPosition; break;
                    case 2: playerTwoPosition = currentUnitPosition; break;
                    case 3: playerThreePosition = currentUnitPosition; break;
                    case 4: playerFourPosition = currentUnitPosition; break;
                }
            }
            if (currentUnitDirection == "west")
            {
                currentUnitPosition = boardCampPositions[3];
                switch (GameMain.currentPlayer)
                {
                    case 1: playerOnePosition = currentUnitPosition; break;
                    case 2: playerTwoPosition = currentUnitPosition; break;
                    case 3: playerThreePosition = currentUnitPosition; break;
                    case 4: playerFourPosition = currentUnitPosition; break;
                }
            }
            GUI.enablePrimaryButton = true;
        }
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
                        gui.EnableArrows(true);
                        playerIsMoving = false;
                    }
                }
                if (movesRemaining == 0)
                {
                    gui.EnableArrows(false);
                    gui.EnableEndTurnButton(false);
                    playerIsMoving = false;
                    CheckForLocalEmptySlots();
                    if (northEmpty || eastEmpty || southEmpty || westEmpty)
                    {
                        gui.EnableSecondaryButton(true);
                    }
                    else
                    {
                        CheckForLocalVillages();
                        if (villageNearby && GameMain.currentPlayer != Villages.villageOwner)
                        {
                            Villages.PlayerLandedOnOpposingVillage();
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

    void TerrainGenerator()
    {
        int randomTerrainType = 0;
        int xSize = 50;
        int ySize = 50;
        for (int z = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, z++)
            {
                randomTerrainType = Random.Range(1, 101);
                if (GameMain.currentBoard == "grasslands")
                {
                    if (randomTerrainType <= 25)
                    {
                        tilemapTerrain.SetTile(new Vector3Int(x, y), grassOne);
                        tilemapTerrain.SetTile(new Vector3Int(-x, y), grassOne);
                        tilemapTerrain.SetTile(new Vector3Int(x, -y), grassOne);
                        tilemapTerrain.SetTile(new Vector3Int(-x, -y), grassOne);
                    }
                    else if (randomTerrainType > 25 && randomTerrainType <= 50)
                    {
                        tilemapTerrain.SetTile(new Vector3Int(x, y), grassTwo);
                        tilemapTerrain.SetTile(new Vector3Int(-x, y), grassTwo);
                        tilemapTerrain.SetTile(new Vector3Int(x, -y), grassTwo);
                        tilemapTerrain.SetTile(new Vector3Int(-x, -y), grassTwo);
                    }
                    else if (randomTerrainType > 50 && randomTerrainType < 101)
                    {
                        tilemapTerrain.SetTile(new Vector3Int(x, y), grassThree);
                        tilemapTerrain.SetTile(new Vector3Int(-x, y), grassThree);
                        tilemapTerrain.SetTile(new Vector3Int(x, -y), grassThree);
                        tilemapTerrain.SetTile(new Vector3Int(-x, -y), grassThree);
                    }
                }
                if (GameMain.currentBoard == "graveyard")
                {
                    if (randomTerrainType <= 25)
                    {
                        tilemapTerrain.SetTile(new Vector3Int(x, y), graveyardOne);
                        tilemapTerrain.SetTile(new Vector3Int(-x, y), graveyardOne);
                        tilemapTerrain.SetTile(new Vector3Int(x, -y), graveyardOne);
                        tilemapTerrain.SetTile(new Vector3Int(-x, -y), graveyardOne);
                    }
                    else if (randomTerrainType > 25 && randomTerrainType <= 50)
                    {
                        tilemapTerrain.SetTile(new Vector3Int(x, y), graveyardTwo);
                        tilemapTerrain.SetTile(new Vector3Int(-x, y), graveyardTwo);
                        tilemapTerrain.SetTile(new Vector3Int(x, -y), graveyardTwo);
                        tilemapTerrain.SetTile(new Vector3Int(-x, -y), graveyardTwo);
                    }
                    else if (randomTerrainType > 50 && randomTerrainType < 101)
                    {
                        tilemapTerrain.SetTile(new Vector3Int(x, y), graveyardThree);
                        tilemapTerrain.SetTile(new Vector3Int(-x, y), graveyardThree);
                        tilemapTerrain.SetTile(new Vector3Int(x, -y), graveyardThree);
                        tilemapTerrain.SetTile(new Vector3Int(-x, -y), graveyardThree);
                    }
                }
                if (GameMain.currentBoard == "ocean")
                {
                    if (randomTerrainType <= 25)
                    {
                        tilemapTerrain.SetTile(new Vector3Int(x, y), oceanOne);
                        tilemapTerrain.SetTile(new Vector3Int(-x, y), oceanOne);
                        tilemapTerrain.SetTile(new Vector3Int(x, -y), oceanOne);
                        tilemapTerrain.SetTile(new Vector3Int(-x, -y), oceanOne);
                    }
                    else if (randomTerrainType > 25 && randomTerrainType <= 50)
                    {
                        tilemapTerrain.SetTile(new Vector3Int(x, y), oceanTwo);
                        tilemapTerrain.SetTile(new Vector3Int(-x, y), oceanTwo);
                        tilemapTerrain.SetTile(new Vector3Int(x, -y), oceanTwo);
                        tilemapTerrain.SetTile(new Vector3Int(-x, -y), oceanTwo);
                    }
                    else if (randomTerrainType > 50 && randomTerrainType < 101)
                    {
                        tilemapTerrain.SetTile(new Vector3Int(x, y), oceanThree);
                        tilemapTerrain.SetTile(new Vector3Int(-x, y), oceanThree);
                        tilemapTerrain.SetTile(new Vector3Int(x, -y), oceanThree);
                        tilemapTerrain.SetTile(new Vector3Int(-x, -y), oceanThree);
                    }
                }
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