using System.Collections;
using System.Collections.Generic;
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
    public static bool northDungeon = false;
    public static bool northVillage = false;
    public static bool northEmpty = false;
    public static Vector3 eastSlotPosition;
    public static bool eastDungeon = false;
    public static bool eastVillage = false;
    public static bool eastEmpty = false;
    public static Vector3 southSlotPosition;
    public static bool southDungeon = false;
    public static bool southVillage = false;
    public static bool southEmpty= false;
    public static Vector3 westSlotPosition;
    public static bool westDungeon = false;
    public static bool westVillage = false;
    public static bool westEmpty = false;
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
    [SerializeField] public Tile grassOne;
    [SerializeField] public Tile grassTwo;
    [SerializeField] public Tile grassThree;
    [SerializeField] public Tile graveyardOne;
    [SerializeField] public Tile graveyardTwo;
    [SerializeField] public Tile graveyardThree;
    [SerializeField] public Tile oceanOne;
    [SerializeField] public Tile oceanTwo;
    [SerializeField] public Tile oceanThree;
    [SerializeField] public Tile camp;
    [SerializeField] public Tile bcHorizontal;
    [SerializeField] public Tile bcThreeDown;
    [SerializeField] public Tile bcVertical;
    [SerializeField] public Tile bcThreeUp;
    [SerializeField] public Tile bcThreeLeft;
    [SerializeField] public Tile bcThreeRight;
    [SerializeField] public Tile bcTopRightCorner;
    [SerializeField] public Tile bcBottomLeftCorner;
    [SerializeField] public Tile bcBottomRightCorner;
    [SerializeField] public Tile bcTopLeftCorner;
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
    public static string currentPlayerColor;
    public static bool playerCurrentlyInCamp;
    public static bool crossroadsPosition = false;
    public static int movesRemaining;
    public GUI gui;

    void Start()
    {
        CampGenerator(dungeon);
        TerrainGenerator();
        currentUnitPosition = playerOnePosition;
        GameMain.playerOneIsActive = true;
        boardPosition = currentUnitPosition;
        playerCurrentlyInCamp = true;
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
        if (playerIsMoving)
        {
            if (tempCounter2 <= 0f)
            {
                if (movesRemaining > 0)
                {
                    CheckForLocalBoardPositions();
                    DetermineNextBoardPosition(tilemapUnits);
                    gui.EnableArrows(false);
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
                    GameMain.endTurnButtonEnabled = true;
                    playerIsMoving = false;
                    CheckForLocalEmptySlots();
                    if (northEmpty || eastEmpty || southEmpty || westEmpty)
                    {
                        GameMain.secondaryButtonEnabled = true;
                    }
                    else
                    {
                        CheckForLocalVillages();
                        if (GameMain.currentPlayer != Villages.villageOwner)
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
        GameMain.bottomLeftLowerButtonEnabled = false;
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
                switch(boardDungeonPositions[listVector])
                {
                    case 1: Dungeons.dungeonType = "imp"; break;
                    case 2: Dungeons.dungeonType = "basilisk"; break;
                }
            }
        }
    }

    public static void CheckForLocalVillages()
    {
        Vector3 north = new Vector3(currentUnitPosition[0], currentUnitPosition[1] + 1);
        Vector3 east = new Vector3(currentUnitPosition[0] + 1, currentUnitPosition[1]);
        Vector3 south = new Vector3(currentUnitPosition[0], currentUnitPosition[1] - 1);
        Vector3 west = new Vector3(currentUnitPosition[0] - 1, currentUnitPosition[1]);
        foreach (Vector3 listVector in boardPlayerOneVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                Villages.villageOwner = 1;
            }
        }
        foreach (Vector3 listVector in boardPlayerTwoVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                Villages.villageOwner = 2;
            }
        }
        foreach (Vector3 listVector in boardPlayerThreeVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                Villages.villageOwner = 3;
            }
        }
        foreach (Vector3 listVector in boardPlayerFourVillagePositions.Keys)
        {
            if (listVector == north || listVector == east || listVector == south || listVector == west)
            {
                Villages.villageOwner = 4;
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
                randomTerrainType = Random.Range(1,101);
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

    void LoopGenerator(int clockworkLocation, Tile dungeon)
    {
        Vector3 midPositionOne = new Vector3(0,0);
        Vector3 cornerPositionOne = new Vector3(0,0);
        Vector3 midPositionTwo = new Vector3(0, 0);
        Vector3 cornerPositionTwo = new Vector3(0, 0);
        Vector3 midPositionThree = new Vector3(0, 0);
        Vector3 cornerPositionThree = new Vector3(0, 0);
        Vector3 midPositionFour = new Vector3(0, 0);
        Vector3 cornerPositionFour = new Vector3(0, 0);
        int startingLocation = clockworkLocation;
        int random;
        int shortSide = 2;
        if (startingLocation == 1)
        {
            midPositionThree = new Vector3(0, 4);
            cornerPositionThree = new Vector3(midPositionThree[0] - shortSide - 1, midPositionThree[1]);
            midPositionFour = new Vector3(cornerPositionThree[0], cornerPositionThree[1] + shortSide + 1);
            cornerPositionFour = new Vector3(midPositionFour[0], midPositionFour[1] + shortSide + 1);
            midPositionOne = new Vector3(cornerPositionFour[0] + shortSide + 1, cornerPositionFour[1]);
            cornerPositionOne = new Vector3(midPositionOne[0] + shortSide + 1, midPositionOne[1]);
            midPositionTwo = new Vector3(cornerPositionOne[0], cornerPositionOne[1] - shortSide - 1);
            cornerPositionTwo = new Vector3(midPositionTwo[0], midPositionTwo[1] - shortSide - 1);

        }
        if (startingLocation == 3)
        {
            midPositionFour = new Vector3(4, 0);
            cornerPositionFour = new Vector3(midPositionFour[0], midPositionFour[1] + shortSide + 1);
            midPositionOne = new Vector3(cornerPositionFour[0] + shortSide + 1, cornerPositionFour[1]);
            cornerPositionOne = new Vector3(midPositionOne[0] + shortSide + 1, midPositionOne[1]);
            midPositionTwo = new Vector3(cornerPositionOne[0], cornerPositionOne[1] - shortSide - 1);
            cornerPositionTwo = new Vector3(midPositionTwo[0], midPositionTwo[1] - shortSide - 1);
            midPositionThree = new Vector3(cornerPositionTwo[0] - shortSide - 1, cornerPositionTwo[1]);
            cornerPositionThree = new Vector3(midPositionThree[0] - shortSide - 1, midPositionThree[1]);
        }
        if (startingLocation == 5)
        {
            midPositionOne = new Vector3(0, -4);
            cornerPositionOne = new Vector3(midPositionOne[0] + shortSide + 1, midPositionOne[1]);
            midPositionTwo = new Vector3(cornerPositionOne[0], cornerPositionOne[1] - shortSide - 1);
            cornerPositionTwo = new Vector3(midPositionTwo[0], midPositionTwo[1] - shortSide - 1);
            midPositionThree = new Vector3(cornerPositionTwo[0] - shortSide - 1, cornerPositionTwo[1]);
            cornerPositionThree = new Vector3(midPositionThree[0] - shortSide - 1, midPositionThree[1]);
            midPositionFour = new Vector3(cornerPositionThree[0], cornerPositionThree[1] + shortSide + 1);
            cornerPositionFour = new Vector3(midPositionFour[0], midPositionFour[1] + shortSide + 1);
        }
        if (startingLocation == 7)
        {
            midPositionTwo = new Vector3(-4, 0);
            cornerPositionTwo = new Vector3(midPositionTwo[0], midPositionTwo[1] - shortSide - 1);
            midPositionThree = new Vector3(cornerPositionTwo[0] - shortSide - 1, cornerPositionTwo[1]);
            cornerPositionThree = new Vector3(midPositionThree[0] - shortSide - 1, midPositionThree[1]);
            midPositionFour = new Vector3(cornerPositionThree[0], cornerPositionThree[1] + shortSide + 1);
            cornerPositionFour = new Vector3(midPositionFour[0], midPositionFour[1] + shortSide + 1);
            midPositionOne = new Vector3(cornerPositionFour[0] + shortSide + 1, cornerPositionFour[1]);
            cornerPositionOne = new Vector3(midPositionOne[0] + shortSide + 1, midPositionOne[1]);
        }
        random = Random.Range(1,3);
        if (startingLocation == 5)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]), bcThreeUp);
            boardCrossroads.Add(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]));
        }
        else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]), bcHorizontal);
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1] - 1));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]), bcThreeUp);
            boardCrossroads.Add(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]));
        }
        random = Random.Range(1, 3);
        if (startingLocation == 7)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]), bcThreeRight);
            boardCrossroads.Add(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]));
        }
        else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]), bcVertical);
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionTwo[0] - 1, (int)midPositionTwo[1]));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]), bcThreeRight);
            boardCrossroads.Add(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]));
        }
        random = Random.Range(1, 3);
        if (startingLocation == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]), bcThreeDown);
            boardCrossroads.Add(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]));
        }
        else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]), bcHorizontal);
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1] + 1));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]), bcThreeDown);
            boardCrossroads.Add(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]));
        }
        random = Random.Range(1, 3);
        if (startingLocation == 3)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]), bcThreeLeft);
            boardCrossroads.Add(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]));
        }
        else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]), bcVertical);
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionFour[0] + 1, (int)midPositionFour[1]));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]), bcThreeLeft);
            boardCrossroads.Add(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]));
        }
        for (int i = 1; i <= shortSide; i++)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionThree[0] + i, (int)cornerPositionThree[1]), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionThree[0], (int)cornerPositionThree[1] + i), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionTwo[0], (int)cornerPositionTwo[1] + i), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionFour[0] + i, (int)cornerPositionFour[1]), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionOne[0] + i, (int)midPositionOne[1]), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0] + i, (int)midPositionThree[1]), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1] + i), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1] + i), bcVertical);
            boardPositions.Add(new Vector3((int)cornerPositionThree[0] + i, (int)cornerPositionThree[1]));
            boardPositions.Add(new Vector3Int((int)cornerPositionThree[0], (int)cornerPositionThree[1] + i));
            boardPositions.Add(new Vector3Int((int)cornerPositionTwo[0], (int)cornerPositionTwo[1] + i));
            boardPositions.Add(new Vector3Int((int)cornerPositionFour[0] + i, (int)cornerPositionFour[1]));
            boardPositions.Add(new Vector3Int((int)midPositionOne[0] + i, (int)midPositionOne[1]));
            boardPositions.Add(new Vector3Int((int)midPositionThree[0] + i, (int)midPositionThree[1]));
            boardPositions.Add(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1] + i));
            boardPositions.Add(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1] + i));
            boardEmptySlotPositions.Add(new Vector3Int((int)cornerPositionThree[0] + i, (int)cornerPositionThree[1] + 1));
            boardEmptySlotPositions.Add(new Vector3Int((int)cornerPositionThree[0] + 1, (int)cornerPositionThree[1] + i));
            boardEmptySlotPositions.Add(new Vector3Int((int)cornerPositionTwo[0] - 1, (int)cornerPositionTwo[1] + i));
            boardEmptySlotPositions.Add(new Vector3Int((int)cornerPositionFour[0] + i, (int)cornerPositionFour[1] - 1));
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionOne[0] + i, (int)midPositionOne[1] - 1));
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionThree[0] + i, (int)midPositionThree[1] + 1));
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionTwo[0] - 1, (int)midPositionTwo[1] + i));
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionFour[0] + 1, (int)midPositionFour[1] + i));
        }
        tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionThree[0], (int)cornerPositionThree[1]), bcBottomLeftCorner);
        tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionFour[0], (int)cornerPositionFour[1]), bcTopLeftCorner);
        tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionOne[0], (int)cornerPositionOne[1]), bcTopRightCorner);
        tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionTwo[0], (int)cornerPositionTwo[1]), bcBottomRightCorner);
        boardPositions.Add(new Vector3((int)midPositionOne[0], (int)midPositionOne[1]));
        boardPositions.Add(new Vector3((int)cornerPositionOne[0], (int)cornerPositionOne[1]));
        boardPositions.Add(new Vector3((int)midPositionTwo[0], (int)midPositionTwo[1]));
        boardPositions.Add(new Vector3((int)cornerPositionTwo[0], (int)cornerPositionTwo[1]));
        boardPositions.Add(new Vector3((int)midPositionThree[0], (int)midPositionThree[1]));
        boardPositions.Add(new Vector3((int)cornerPositionThree[0], (int)cornerPositionThree[1]));
        boardPositions.Add(new Vector3((int)midPositionFour[0], (int)midPositionFour[1]));
        boardPositions.Add(new Vector3((int)cornerPositionFour[0], (int)cornerPositionFour[1]));
        for (int i = 0; i < boardEmptySlotPositions.Count; i++)
        {
            int randomDungeon = Random.Range(1,101);
            if (randomDungeon <= 15)
            {
                int randomDungeonType = Random.Range(1,3);
                if (randomDungeonType == 1)
                {
                    boardImpDungeonPositions.Add(boardEmptySlotPositions[i]);
                }
                else if (randomDungeonType == 2)
                {
                    boardBasiliskDungeonPositions.Add(boardEmptySlotPositions[i]);
                }
                boardEmptySlotPositions.RemoveAt(i);
            }
        }
        for (int i = 0; i < boardImpDungeonPositions.Count; i++)
        {
            boardPosition = boardImpDungeonPositions[i];
            tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), dungeon);
        }
        for (int i = 0; i < boardBasiliskDungeonPositions.Count; i++)
        {
            boardPosition = boardBasiliskDungeonPositions[i];
            tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), dungeon);
        }
    }

    void BranchGenerator(int clockworkLocation, Tile dungeon)
    {
        Vector3 midPositionOne = new Vector3(0, 0);
        Vector3 midPositionTwo = new Vector3(0, 0);
        Vector3 midPositionThree = new Vector3(0, 0);
        Vector3 midPositionFour = new Vector3(0, 0);
        int startingLocation = clockworkLocation;
        int random;
        if (startingLocation == 1)
        {
            midPositionThree = new Vector3(0, 4);
            midPositionOne = new Vector3(midPositionThree[0], midPositionThree[1] + 7);
            midPositionFour = new Vector3(midPositionThree[0] - 4, midPositionThree[1] + 4);
            midPositionTwo = new Vector3(midPositionThree[0] + 4, midPositionThree[1] + 4);
        }
        if (startingLocation == 2)
        {
            midPositionThree = new Vector3(7,4);
            if (boardClockPosition[3] == "loop")
            {
                Debug.Log("pass");
                tilemapBoardConnectors.SetTile(new Vector3Int(7,3), bcThreeUp);
            }
            for (int i = 0; i <= 7; i++)
            {
                tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1] + i), bcVertical);
            }
        }
        /*if (startingLocation == 3)
        {
            midPositionFour = new Vector3(4, 0);
            midPositionOne = new Vector3(cornerPositionFour[0] + 3, cornerPositionFour[1]);
            midPositionTwo = new Vector3(cornerPositionOne[0], cornerPositionOne[1] - 3);
            midPositionThree = new Vector3(cornerPositionTwo[0] - 3, cornerPositionTwo[1]);
        }
        if (startingLocation == 5)
        {
            midPositionOne = new Vector3(0, -4);
            midPositionTwo = new Vector3(cornerPositionOne[0], cornerPositionOne[1] - 3);
            midPositionThree = new Vector3(cornerPositionTwo[0] - 3, cornerPositionTwo[1]);
            midPositionFour = new Vector3(cornerPositionThree[0], cornerPositionThree[1] + 3);
        }
        if (startingLocation == 7)
        {
            midPositionTwo = new Vector3(-4, 0);
            midPositionThree = new Vector3(cornerPositionTwo[0] - 3, cornerPositionTwo[1]);
            midPositionFour = new Vector3(cornerPositionThree[0], cornerPositionThree[1] + 3);
            midPositionOne = new Vector3(cornerPositionFour[0] + 3, cornerPositionFour[1]);
        }
        random = Random.Range(1, 3);
        if (startingLocation == 5)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]), bcThreeUp);
            boardCrossroads.Add(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]));
        }
        else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]), bcHorizontal);
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1] - 1));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]), bcThreeUp);
            boardCrossroads.Add(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]));
        }
        random = Random.Range(1, 3);
        if (startingLocation == 7)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]), bcThreeRight);
            boardCrossroads.Add(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]));
        }
        else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]), bcVertical);
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionTwo[0] - 1, (int)midPositionTwo[1]));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]), bcThreeRight);
            boardCrossroads.Add(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]));
        }
        random = Random.Range(1, 3);
        if (startingLocation == 3)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]), bcThreeLeft);
            boardCrossroads.Add(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]));
        }
        else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]), bcVertical);
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionFour[0] + 1, (int)midPositionFour[1]));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]), bcThreeLeft);
            boardCrossroads.Add(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]));
        }*/
        random = Random.Range(1, 3);
        if (startingLocation == 1)
        {
            for (int i = 0; i <= 7; i++)
            {
                tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1] + i), bcVertical);
            }
            // boardCrossroads.Add(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]));
        }
        /*else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]), bcHorizontal);
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1] + 1));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]), bcThreeDown);
            boardCrossroads.Add(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]));
        }*/
    }

    void FillActiveSlots(string currentBoard, int activePlayers)
    {
        // Board Structures
        for (int x = 0; x < boardLength; x++)
        {
            int random = Random.Range(1,101);
            if (random <= 20)
            {
                if (currentBoard == "grasslands")
                {
                    int randomEnemy = Random.Range(1,3);
                    if (randomEnemy == 1)
                    {
                        GameMain.dungeonType = "Imp";
                    }
                    else if (randomEnemy == 2)
                    {
                        GameMain.dungeonType = "Basilisk";
                    }
                }
                tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), dungeon);
            }
            else if (random >= 80 && random < 99)
            {
                boardStructures.Add(x, "chest");
                tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), chest);
            }
            else if (random >= 99)
            {
                boardStructures.Add(x, "oddity");
                tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), oddity);
            }
            GameMain.boardMonsters.Add(x, "empty");
        }
    }

    public void SpawnActivePlayerAtCamp()
    {
        int random;
        random = Random.Range(1, 17);
        switch (random)
        {
            case 1: boardPosition[0] = 0; boardPosition[1] = 1; break;
            case 2: boardPosition[0] = 0; boardPosition[1] = -1; break;
            case 3: boardPosition[0] = 1; boardPosition[1] = 0; break;
            case 4: boardPosition[0] = -1; boardPosition[1] = 0; break;
            case 5: boardPosition[0] = -1; boardPosition[1] = -1; break;
            case 6: boardPosition[0] = 1; boardPosition[1] = 1; break;
            case 7: boardPosition[0] = -1; boardPosition[1] = 1; break;
            case 8: boardPosition[0] = 1; boardPosition[1] = -1; break;
            case 9: boardPosition[0] = 0; boardPosition[1] = 2; break;
            case 10: boardPosition[0] = 0; boardPosition[1] = 3; break;
            case 11: boardPosition[0] = 0; boardPosition[1] = -2; break;
            case 12: boardPosition[0] = 0; boardPosition[1] = -3; break;
            case 13: boardPosition[0] = 2; boardPosition[1] = 0; break;
            case 14: boardPosition[0] = 3; boardPosition[1] = 0; break;
            case 15: boardPosition[0] = -2; boardPosition[1] = 0; break;
            case 16: boardPosition[0] = -3; boardPosition[1] = -0; break;
        }
        currentUnitPosition = boardPosition;
        switch (GameMain.currentPlayer)
        {
            case 1: playerOnePosition = currentUnitPosition; break;
            case 2: playerTwoPosition = currentUnitPosition; break;
            case 3: playerThreePosition = currentUnitPosition; break;
            case 4: playerFourPosition = currentUnitPosition; break;
        }
        switch (currentPlayerColor)
        {
            case "red": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerRed); break;
            case "blue": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerBlue); break;
            case "green": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerGreen); break;
            case "purple": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerPurple); break;
            case "white": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerWhite); break;
        }
        Debug.Log("Starting Unit Position: " + currentUnitPosition);
    }

    void GraveyardCampGenerator(Tile dungeon)
    {
        tilemapBoardConnectors.SetTile(new Vector3Int(0, 0), camp);
        int random = Random.Range(1,3);
        if (random == 1)
        {
            random = Random.Range(1,3);
            if (random == 1)
            {
                for (int i = 1; i <= 9; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int(i, 0), bcHorizontal);
                }
                random = Random.Range(1,3);
                if (random == 1)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int(0, 10), bcTopRightCorner);
                    for (int i = -1; i <= -5; i--)
                    {
                        tilemapBoardConnectors.SetTile(new Vector3Int(i, 10), bcVertical);
                    }
                }
                else if (random == 2)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int(0, 10), bcBottomRightCorner);
                    for (int i = 1; i <= 5; i++)
                    {
                        tilemapBoardConnectors.SetTile(new Vector3Int(i, 10), bcVertical);
                    }
                }
            }
        }
        if (random == 2)
        {
            random = Random.Range(1, 3);
            if (random == 1)
            {
                for (int i = 1; i <= 9; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int(0, i), bcVertical);
                }
                random = Random.Range(1, 3);
                if (random == 1)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int(10, 0), bcTopRightCorner);
                    for (int i = -1; i <= -5; i--)
                    {
                        tilemapBoardConnectors.SetTile(new Vector3Int(10, i), bcHorizontal);
                    }
                }
                else if (random == 2)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int(10, 0), bcTopLeftCorner);
                    for (int i = 1; i <= 5; i++)
                    {
                        tilemapBoardConnectors.SetTile(new Vector3Int(10, i), bcHorizontal);
                    }
                }
            }
        }
    }

    void CampGenerator(Tile dungeon)
    {
        if (GameMain.currentBoard == "grasslands")
        {
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 0), camp);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 1), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, -1), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int(1, 0), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int(-1, 0), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int(-1, 1), bcTopLeftCorner);
            tilemapBoardConnectors.SetTile(new Vector3Int(1, 1), bcTopRightCorner);
            tilemapBoardConnectors.SetTile(new Vector3Int(-1, -1), bcBottomLeftCorner);
            tilemapBoardConnectors.SetTile(new Vector3Int(1, -1), bcBottomRightCorner);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 1), bcThreeUp);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 2), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 3), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 4), bcThreeDown);
            tilemapBoardConnectors.SetTile(new Vector3Int(1, 0), bcThreeRight);
            tilemapBoardConnectors.SetTile(new Vector3Int(2, 0), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int(3, 0), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int(4, 0), bcThreeLeft);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, -1), bcThreeDown);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, -2), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, -3), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, -4), bcThreeUp);
            tilemapBoardConnectors.SetTile(new Vector3Int(-1, 0), bcThreeLeft);
            tilemapBoardConnectors.SetTile(new Vector3Int(-2, 0), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int(-3, 0), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int(-4, 0), bcThreeRight);
            int random;
            string section = "empty";
            random = Random.Range(1,4);
            if (random == 1)
            {
                section = "loop";
                LoopGenerator(1, dungeon);
            }
            else if (random == 2)
            {
                section = "branch";
                // BranchGenerator(1, dungeon);
            }
            if (random == 3)
            {
                section = "empty";
            }
            boardClockPosition.Add(1, section);
            random = 3;
            if (random == 1)
            {
                section = "loop";
                LoopGenerator(2, dungeon);
            }
            else if (random == 2)
            {
                section = "branch";
                BranchGenerator(2, dungeon);
            }
            if (random == 3)
            {
                section = "empty";
            }
            boardClockPosition.Add(2, section);
            random = Random.Range(1,4);
            if (random == 1)
            {
                section = "loop";
                LoopGenerator(3, dungeon);
            }
            else if (random == 2)
            {
                section = "branch";
                // BranchGenerator(3, dungeon);
            }
            if (random == 3)
            {
                section = "empty";
            }
            boardClockPosition.Add(3, section);
            boardClockPosition.Add(4, "empty");
            random = Random.Range(1,4);
            if (random == 1)
            {
                section = "loop";
                LoopGenerator(5, dungeon);
            }
            else if (random == 2)
            {
                section = "branch";
                // BranchGenerator(5, dungeon);
            }
            if (random == 3)
            {
                section = "empty";
            }
            boardClockPosition.Add(5, section);
            boardClockPosition.Add(6, "empty");
            random = Random.Range(1,4);
            if (random == 1)
            {
                section = "loop";
                LoopGenerator(7, dungeon);
            }
            else if (random == 2)
            {
                section = "branch";
                // BranchGenerator(7, dungeon);
            }
            if (random == 3)
            {
                section = "empty";
            }
            boardClockPosition.Add(7, section);
            boardClockPosition.Add(8, "empty");
            boardPositions.Add(new Vector3(0, 1));
            boardPositions.Add(new Vector3(0, -1));
            boardPositions.Add(new Vector3(1, 0));
            boardPositions.Add(new Vector3(-1, 0));
            boardPositions.Add(new Vector3(1, 1));
            boardPositions.Add(new Vector3(-1, -1));
            boardPositions.Add(new Vector3(-1, 1));
            boardPositions.Add(new Vector3(1, -1));
            boardPositions.Add(new Vector3Int(0, 2));
            boardPositions.Add(new Vector3Int(0, 3));
            boardPositions.Add(new Vector3Int(0, 4));
            boardPositions.Add(new Vector3Int(2, 0));
            boardPositions.Add(new Vector3Int(3, 0));
            boardPositions.Add(new Vector3Int(4, 0));
            boardPositions.Add(new Vector3Int(0, -2));
            boardPositions.Add(new Vector3Int(0, -3));
            boardPositions.Add(new Vector3Int(0, -4));
            boardPositions.Add(new Vector3Int(-2, 0));
            boardPositions.Add(new Vector3Int(-3, 0));
            boardPositions.Add(new Vector3Int(-4, 0));
            boardCampPositions.Add(new Vector3(0, 1));
            boardCampPositions.Add(new Vector3(0, -1));
            boardCampPositions.Add(new Vector3(1, 0));
            boardCampPositions.Add(new Vector3(-1, 0));
            boardCampPositions.Add(new Vector3(1, 1));
            boardCampPositions.Add(new Vector3(-1, -1));
            boardCampPositions.Add(new Vector3(-1, 1));
            boardCampPositions.Add(new Vector3(1, -1));
            boardCampPositions.Add(new Vector3Int(0, 2));
            boardCampPositions.Add(new Vector3Int(0, 3));
            boardCampPositions.Add(new Vector3Int(0, 4));
            boardCampPositions.Add(new Vector3Int(2, 0));
            boardCampPositions.Add(new Vector3Int(3, 0));
            boardCampPositions.Add(new Vector3Int(4, 0));
            boardCampPositions.Add(new Vector3Int(0, -2));
            boardCampPositions.Add(new Vector3Int(0, -3));
            boardCampPositions.Add(new Vector3Int(0, -4));
            boardCampPositions.Add(new Vector3Int(-2, 0));
            boardCampPositions.Add(new Vector3Int(-3, 0));
            boardCampPositions.Add(new Vector3Int(-4, 0));
            boardCrossroads.Add(new Vector3Int(0, 1));
            boardCrossroads.Add(new Vector3Int(1, 0));
            boardCrossroads.Add(new Vector3Int(0, -1));
            boardCrossroads.Add(new Vector3Int(-1, 0));
            boardCrossroads.Add(new Vector3Int(0, 4));
            boardCrossroads.Add(new Vector3Int(4, 0));
            boardCrossroads.Add(new Vector3Int(0, -4));
            boardCrossroads.Add(new Vector3Int(-4, 0));
        }
        else if (GameMain.currentBoard == "graveyard")
        {
            //
        }
        else if (GameMain.currentBoard == "moon")
        {
            //
        }
        else if (GameMain.currentBoard == "machine")
        {
            //
        }
    }
}