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
    public static Vector3 currentUnitPosition;
    public static Vector3 playerOnePosition;
    public static Vector3 playerTwoPosition;
    public static Vector3 playerThreePosition;
    public static Vector3 playerFourPosition;
    public static int previousUnitAvatar = 0;
    public static int newUnitPosition = 0;
    public static int unitPositionPlayer1 = 0;
    public static int unitPositionPlayer2 = 0;
    public static int unitPositionPlayer3 = 0;
    public static int unitPositionPlayer4 = 0;
    int avatarAtCurrentPosition = 0;
    public static bool playerIsMoving;
    public static Vector3 boardPosition;
    public static List<Vector3> boardPositions = new List<Vector3>();
    public static List<Vector3> localBoardPositions = new List<Vector3>();
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
    //
    public static List<Vector3> boardSlotPositions = new List<Vector3>();
    public static List<Vector3> boardCampPositions = new List<Vector3>();
    public static List<Vector3> boardCrossroads = new List<Vector3>();
    public static Dictionary<int, string> boardClockPosition = new Dictionary<int, string>();
    public static Dictionary<int, string> boardLoopDirection = new Dictionary<int, string>();
    public static Dictionary<int, string> boardStructures = new Dictionary<int, string>();
    public static Dictionary<int, string> boardUnits = new Dictionary<int, string>();
    public static int boardLength;
    bool firstSectionActive;
    bool secondSectionActive;
    bool thirdSectionActive;
    string loopDirection;
    bool leftPassage;
    bool rightPassage;
    bool topPassage;
    bool downPassage;
    public static bool arrowsInCampEnabled = false;
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
    private float counter = 0.5f;
    private float tempCounter = 0f;
    private float tempCounter2 = 0f;
    private float counter2 = 1f;
    // Other //
    public static string currentPlayerColor;
    public static bool playerCurrentlyInCamp;
    bool crossroadsPosition = false;
    int playerInClockPosition = 0;
    public static int movesRemaining;

    void Start()
    {
        CampGenerator();
        TerrainGenerator();
        currentUnitPosition = playerOnePosition;
        boardPosition = currentUnitPosition;
        playerCurrentlyInCamp = true;
        arrowsInCampEnabled = false;
    }

    void Update()
    {
        if (playerIsMoving)
        {
            if (tempCounter2 <= 0f)
            {
                if (GameMain.activePlayers > 0)
                {
                    boardPosition = playerOnePosition;
                    switch (GameMain.playerOneColor)
                    {
                        case "red": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerRed); break;
                        case "blue": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerBlue); break;
                        case "green": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerGreen); break;
                        case "purple": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerPurple); break;
                        case "white": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerWhite); break;
                    }
                }
                if (GameMain.activePlayers > 1)
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
                if (GameMain.activePlayers > 2)
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
                if (GameMain.activePlayers > 3)
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
                if (movesRemaining > 0)
                {
                    CheckForLocalBoardPositions();
                    tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), null);
                    switch (GameMain.currentPlayer)
                    {
                        case 1: playerOnePosition = currentUnitPosition; break;
                        case 2: playerTwoPosition = currentUnitPosition; break;
                        case 3: playerThreePosition = currentUnitPosition; break;
                        case 4: playerFourPosition = currentUnitPosition; break;
                    }
                    movesRemaining -= 1;
                }
                if (movesRemaining == 0)
                {
                    playerIsMoving = false;
                }
                tempCounter2 = counter2;
            }
            else
            {
                tempCounter2 -= Time.deltaTime;
            }
        }
        /*if (playerIsMoving && playerCurrentlyInCamp)
        {
            if (tempCounter <= 0f)
            {
                boardPosition = currentUnitPosition;
                CheckForUnitsAtPosition();
                currentUnitPositionOnBoard += 1;
                tempCounter = counter;
            }
            else
            {
                tempCounter -= Time.deltaTime;
            }
        }*/
        if (playerIsMoving && !playerCurrentlyInCamp)
        {
            /*
            if (tempCounter <= 0f)
            {
                if (currentUnitPositionOnBoard == newUnitPosition)
                {
                    GameMain.playerIsMoving = false;
                    GameMain.playerIsMovingInReverse = false;
                    if (previousUnitAvatar == 0)
                    {
                        // MoveUnitComplete(tilemapStructures, playerRed, playerBlue, playerGreen, playerPurple, playerWhite);
                    }
                    else
                    {
                        Combat.CombatEncounterStart();
                        // MoveUnitComplete(tilemapStructures, player, playerRed, playerBlue, playerGreen, playerPurple, playerWhite);
                    }
                }
                tempCounter = counter;
            }
            else
            {
                tempCounter -= Time.deltaTime;
            }*/
        }
    }

    public static void MoveUnit()
    {
        GameMain.RollDice();
        GameMain.bottomLeftLowerButtonEnabled = false;
        movesRemaining = GameMain.diceOneResult + GameMain.diceTwoResult + GameMain.diceThreeResult;
        CheckForLocalBoardPositions();
    }

        public void CheckForUnitsAtPosition()
    {
        previousUnitAvatar = 0;
        if (currentUnitPosition == playerOnePosition && GameMain.currentPlayer != 1)
        {
            previousUnitAvatar = 1;
            // Combat for landing on another player?
        }
        if (currentUnitPosition == playerTwoPosition && GameMain.currentPlayer != 2)
        {
            previousUnitAvatar = 2;
        }
        if (currentUnitPosition == playerThreePosition && GameMain.currentPlayer != 3)
        {
            previousUnitAvatar = 3;
        }
        if (currentUnitPosition == playerFourPosition && GameMain.currentPlayer != 4)
        {
            previousUnitAvatar = 4;
        }
        if (previousUnitAvatar == 0)
        {
            boardPosition = boardPositions[currentUnitPositionOnBoard];
            tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), null);
        }
        // Add monsters
    }

    void CheckForBoardCrossroads()
    {
        boardPosition = boardPositions[currentUnitPositionOnBoard];
        foreach (Vector3 listVector in boardCrossroads)
        {
            if (listVector == boardPosition)
            {
                crossroadsPosition = true;
            }
        }
    }

    public static void DetermineNextPosition()
    {
        boardPosition = boardPositions[currentUnitPositionOnBoard];
        Vector3 north = new Vector3(boardPosition[0], boardPosition[1] + 1);
        Vector3 east = new Vector3(boardPosition[0] + 1, boardPosition[1]);
        Vector3 south = new Vector3(boardPosition[0], boardPosition[1] - 1);
        Vector3 west = new Vector3(boardPosition[0] - 1, boardPosition[1]);
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
        GUI.enableArrowButtons = true;
    }

    void CheckForLocalBoardSlots()
    {

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

    void LoopGenerator()
    {
        // Determine where the loop will spawn
        if (!firstSectionActive)
        {
            int random = 1;
            int randomLoopDirection = Random.Range(1,4);
            if (randomLoopDirection == 1)
            {
                loopDirection = "left";
            }
            else if (randomLoopDirection == 2)
            {
                loopDirection = "right";
            }
            else if (randomLoopDirection == 3)
            {
                loopDirection = "either";
            }
            int shortSide = Random.Range(3,5);
            int fullSide = (shortSide * 2) + 1;

            string boardStartingPoint = "down";
            if (boardStartingPoint == "down")
            {
                boardLoopDirection.Add(1, loopDirection);
                // Bottom Middle to the Bottom Left Corner
                for (int i = 1; i <= shortSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] - i, (int)boardPosition[1]), bcHorizontal);
                    boardPositions.Add(new Vector3((int)boardPosition[0] - i, (int)boardPosition[1]));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0] - i, (int)boardPosition[1] + 1));
                }
                tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1]), bcBottomLeftCorner);
                boardPositions.Add(new Vector3((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1]));
                // Bottom Left Corner to the Top Left Corner
                for (int i = 1; i <= shortSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1] + i), bcVertical);
                    boardPositions.Add(new Vector3((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1] + i));
                    if (i > 1)
                    {
                        boardSlotPositions.Add(new Vector3((int)boardPosition[0] - shortSide, (int)boardPosition[1] + i));
                    }
                }
                random = Random.Range(1,3);
                if (random == 1)
                {
                    Debug.Log("Three Left");
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1] + shortSide + 1), bcThreeLeft);
                    boardPositions.Add(new Vector3((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1] + shortSide + 1));
                    boardCrossroads.Add(new Vector3((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1] + shortSide + 1));
                    leftPassage = true;
                }
                else if (random == 2)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1] + shortSide + 1), bcVertical);
                    boardPositions.Add(new Vector3((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1] + shortSide + 1));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0] - shortSide, (int)boardPosition[1] + shortSide + 1));
                    leftPassage = false;
                }
                for (int i = shortSide + 2; i <= fullSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1] + i), bcVertical);
                    boardPositions.Add(new Vector3((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1] + i));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0] - shortSide, (int)boardPosition[1] + i));
                }
                tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1] + fullSide + 1), bcTopLeftCorner);
                boardPositions.Add(new Vector3((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1] + fullSide + 1));
                // Top Left Corner to Top Right Corner
                for (int i = 0; i < shortSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] - shortSide + i, (int)boardPosition[1] + fullSide + 1), bcHorizontal);
                    boardPositions.Add(new Vector3((int)boardPosition[0] - shortSide + i, (int)boardPosition[1] + fullSide + 1));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0] - shortSide + i, (int)boardPosition[1] + fullSide));
                }
                random = Random.Range(1,3);
                if (random == 1)
                {
                    Debug.Log("Three Up");
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1] + fullSide + 1), bcThreeUp);
                    boardPositions.Add(new Vector3((int)boardPosition[0], (int)boardPosition[1] + fullSide + 1));
                    boardCrossroads.Add(new Vector3((int)boardPosition[0], (int)boardPosition[1] + fullSide + 1));
                    topPassage = true;
                }
                else if (random == 2)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1] + fullSide + 1), bcHorizontal);
                    boardPositions.Add(new Vector3((int)boardPosition[0], (int)boardPosition[1] + fullSide + 1));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0], (int)boardPosition[1] + fullSide));
                    topPassage = false;
                }
                for (int i = 1; i <= shortSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + i, (int)boardPosition[1] + fullSide + 1), bcHorizontal);
                    boardPositions.Add(new Vector3((int)boardPosition[0] + i, (int)boardPosition[1] + fullSide + 1));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0] + i, (int)boardPosition[1] + fullSide));
                }
                tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + fullSide + 1), bcTopRightCorner);
                boardPositions.Add(new Vector3((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + fullSide + 1));
                // Top Right Corner to Bottom Right Corner
                for (int i = 0; i < shortSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + fullSide - i), bcVertical);
                    boardPositions.Add(new Vector3((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] - i));
                    /*boardSlotPositions.Add(new Vector3((int)boardPosition[0] + shortSide, (int)boardPosition[1] + fullSide - i));*/
                }
                random = Random.Range(1,3);
                if (random == 1)
                {
                    Debug.Log("Three Right");
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + shortSide + 1), bcThreeRight);
                    boardPositions.Add(new Vector3((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + shortSide - 1));
                    boardCrossroads.Add(new Vector3((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + shortSide - 1));
                    rightPassage = true;
                }
                else if (random == 2)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + shortSide + 1), bcVertical);
                    boardPositions.Add(new Vector3((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + shortSide + 1));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0] + shortSide, (int)boardPosition[1] + shortSide + 1));
                    rightPassage = false;
                }
                for (int i = 0; i <= shortSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + shortSide - i), bcVertical);
                    boardPositions.Add(new Vector3((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + shortSide + 1 - i));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0] + shortSide, (int)boardPosition[1] + shortSide - i));
                }
                tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1]), bcBottomRightCorner);
                boardPositions.Add(new Vector3((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1]));
                // Bottom Right Corner to Bottom Middle
                for (int i = 0; i < shortSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + shortSide - i, (int)boardPosition[1]), bcHorizontal);
                    boardPositions.Add(new Vector3((int)boardPosition[0] + shortSide - i, (int)boardPosition[1]));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0] + shortSide - i, (int)boardPosition[1] + 1));
                }
            }
            if (boardStartingPoint == "right")
            {
                boardLoopDirection.Add(1, loopDirection);
                // Middle Left to Top Left Corner
                for (int i = 1; i <= shortSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1] + i), bcVertical);
                    boardPositions.Add(new Vector3((int)boardPosition[0], (int)boardPosition[1] + i));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0], (int)boardPosition[1] + i));
                }
                tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1] + shortSide + 1), bcTopLeftCorner);
                boardPositions.Add(new Vector3((int)boardPosition[0], (int)boardPosition[1] + shortSide + 1));
                /*
                // Top Left Corner to Top Right Corner
                for (int i = 0; i < shortSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] - shortSide + i, (int)boardPosition[1] + fullSide + 1), bcHorizontal);
                    boardPositions.Add(new Vector3((int)boardPosition[0] - shortSide + i, (int)boardPosition[1] + fullSide + 1));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0] - shortSide + i, (int)boardPosition[1] + fullSide));
                }
                random = Random.Range(1,3);
                if (random == 1)
                {
                    Debug.Log("Three Up");
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1] + fullSide + 1), bcThreeUp);
                    boardPositions.Add(new Vector3((int)boardPosition[0], (int)boardPosition[1] + fullSide + 1));
                    boardCrossroads.Add(new Vector3((int)boardPosition[0], (int)boardPosition[1] + fullSide + 1));
                    topPassage = true;
                }
                else if (random == 2)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1] + fullSide + 1), bcHorizontal);
                    boardPositions.Add(new Vector3((int)boardPosition[0], (int)boardPosition[1] + fullSide + 1));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0], (int)boardPosition[1] + fullSide));
                    topPassage = false;
                }
                for (int i = 1; i <= shortSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + i, (int)boardPosition[1] + fullSide + 1), bcHorizontal);
                    boardPositions.Add(new Vector3((int)boardPosition[0] + i, (int)boardPosition[1] + fullSide + 1));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0] + i, (int)boardPosition[1] + fullSide + 1));
                }
                tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + fullSide + 1), bcTopRightCorner);
                boardPositions.Add(new Vector3((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + fullSide + 1));
                // Top Right Corner to Bottom Right Corner
                for (int i = 0; i < shortSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + fullSide - i), bcVertical);
                    boardPositions.Add(new Vector3((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] - i));
                    if (i > 1)
                    {
                        boardSlotPositions.Add(new Vector3((int)boardPosition[0] + shortSide, (int)boardPosition[1] - i));
                    }
                }
                random = Random.Range(1,3);
                if (random == 1)
                {
                    Debug.Log("Three Right");
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + shortSide + 1), bcThreeRight);
                    boardPositions.Add(new Vector3((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + shortSide - 1));
                    boardCrossroads.Add(new Vector3((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + shortSide - 1));
                    rightPassage = true;
                }
                else if (random == 2)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + shortSide - 1), bcVertical);
                    boardPositions.Add(new Vector3((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + shortSide - 1));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0] + shortSide, (int)boardPosition[1] + shortSide - 1));
                    rightPassage = false;
                }
                for (int i = 0; i <= shortSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + shortSide - i), bcVertical);
                    boardPositions.Add(new Vector3((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + shortSide - 1));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1] + shortSide + 1));
                }
                tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1]), bcBottomRightCorner);
                boardPositions.Add(new Vector3((int)boardPosition[0] + shortSide + 1, (int)boardPosition[1]));
                // Bottom Right Corner to Bottom Left Corner
                for (int i = 0; i < shortSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + shortSide - i, (int)boardPosition[1]), bcHorizontal);
                    boardPositions.Add(new Vector3((int)boardPosition[0] + shortSide - i, (int)boardPosition[1]));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0] + shortSide - i, (int)boardPosition[1] + 1));
                }
                // Put IF statement here
                for (int i = 1; i <= shortSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] - i, (int)boardPosition[1]), bcHorizontal);
                    boardPositions.Add(new Vector3((int)boardPosition[0] - i, (int)boardPosition[1]));
                    boardSlotPositions.Add(new Vector3((int)boardPosition[0] - i, (int)boardPosition[1] + 1));
                }
                tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1]), bcBottomLeftCorner);
                boardPositions.Add(new Vector3((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1]));
                // Bottom Left Corner to Left Middle
                for (int i = 1; i <= shortSide; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1] + i), bcVertical);
                    boardPositions.Add(new Vector3((int)boardPosition[0] - shortSide - 1, (int)boardPosition[1] + i));
                    if (i > 1)
                    {
                        boardSlotPositions.Add(new Vector3((int)boardPosition[0] - shortSide, (int)boardPosition[1] + i));
                    }
                }*/
            }
            firstSectionActive = true;
        }
    }

    void GameSetup(string currentBoard, int activePlayers)
    {
        TerrainGenerator();
        /*
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 1), bcVertical);
            boardPositions.Add(new Vector3(0, 1));
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 2), bcVertical);
            onDeckPosition = new Vector3(0,2);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 3), bcThreeRight);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 4), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int(1, 3), bcHorizontal);
            
            tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 2, 3), bcHorizontal);
            Debug.Log("Board Length: " + boardLength);
            // Player Camp Positions and Spawn Active Players
            unitPositionPlayer1 = 0;
            campPositionPlayer2 = 0;
            campPositionPlayer3 = 0;
            campPositionPlayer4 = 0;
            if (GameMain.player_in_camp_one)
            {
                switch (GameMain.playerOneColor)
                {
                    case "red": tilemapStructures.SetTile(new Vector3Int(0, 1), playerRed); break;
                    case "blue": tilemapStructures.SetTile(new Vector3Int(0, 1), playerBlue); break;
                    case "green": tilemapStructures.SetTile(new Vector3Int(0, 1), playerGreen); break;
                    case "purple": tilemapStructures.SetTile(new Vector3Int(0, 1), playerPurple); break;
                    case "white": tilemapStructures.SetTile(new Vector3Int(0, 1), playerWhite); break;
                    default: tilemapStructures.SetTile(new Vector3Int(0, 1), player); break;
                }
            }
            if (GameMain.player_in_camp_two)
            {
                switch (GameMain.playerTwoColor)
                {
                    case "red": tilemapStructures.SetTile(new Vector3Int(1, 0), playerRed); break;
                    case "blue": tilemapStructures.SetTile(new Vector3Int(1, 0), playerBlue); break;
                    case "green": tilemapStructures.SetTile(new Vector3Int(1, 0), playerGreen); break;
                    case "purple": tilemapStructures.SetTile(new Vector3Int(1, 0), playerPurple); break;
                    case "white": tilemapStructures.SetTile(new Vector3Int(1, 0), playerWhite); break;
                    default: tilemapStructures.SetTile(new Vector3Int(1, 0), player); break;
                }
            }
            if (GameMain.player_in_camp_three)
            {
                switch (GameMain.playerThreeColor)
                {
                    case "red": tilemapStructures.SetTile(new Vector3Int(0, -1), playerRed); break;
                    case "blue": tilemapStructures.SetTile(new Vector3Int(0, -1), playerBlue); break;
                    case "green": tilemapStructures.SetTile(new Vector3Int(0, -1), playerGreen); break;
                    case "purple": tilemapStructures.SetTile(new Vector3Int(0, -1), playerPurple); break;
                    case "white": tilemapStructures.SetTile(new Vector3Int(0, -1), playerWhite); break;
                    default: tilemapStructures.SetTile(new Vector3Int(0, -1), player); break;
                }
            }
            if (GameMain.player_in_camp_four)
            {
                switch (GameMain.playerFourColor)
                {
                    case "red": tilemapStructures.SetTile(new Vector3Int(-1, 0), playerRed); break;
                    case "blue": tilemapStructures.SetTile(new Vector3Int(-1, 0), playerBlue); break;
                    case "green": tilemapStructures.SetTile(new Vector3Int(-1, 0), playerGreen); break;
                    case "purple": tilemapStructures.SetTile(new Vector3Int(-1, 0), playerPurple); break;
                    case "white": tilemapStructures.SetTile(new Vector3Int(-1, 0), playerWhite); break;
                    default: tilemapStructures.SetTile(new Vector3Int(-1, 0), player); break;
                }
            }
        }
        else if (currentBoard == "graveyard")
        {
            //
        }
        else if (currentBoard == "ocean")
        {
            //
        }
        else if (currentBoard == "moon")
        {
            //
        }
        else if (currentBoard == "machine")
        {
            //
        }
        // Board Structures
        for (int x = 0; x < boardLength; x++)
        {
            int random = Random.Range(1,101);
            if (random <= 20 && dungeonCount <= dungeonCap)
            {
                dungeonCount += 1;
                if (currentBoard == "grasslands")
                {
                    int randomEnemy = Random.Range(1,3);
                    if (randomEnemy == 1)
                    {
                        dungeonType = "Imp";
                    }
                    else if (randomEnemy == 2)
                    {
                        dungeonType = "Basilisk";
                    }
                }
                boardStructures.Add(x, "dungeon" + dungeonType);
                boardPosition = boardSlotPositions[x];
                tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), dungeon);
            }
            else if (random <= 15 && dungeonCount > dungeonCap)
            {
                boardStructures.Add(x, "empty");
            }
            else if (random < 80)
            {
                boardStructures.Add(x, "empty");
            }
            else if (random >= 80 && random < 99)
            {
                boardStructures.Add(x, "chest");
                boardPosition = boardSlotPositions[x];
                tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), chest);
            }
            else if (random >= 99)
            {
                boardStructures.Add(x, "oddity");
                boardPosition = boardSlotPositions[x];
                tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), oddity);
            }
            GameMain.boardMonsters.Add(x, "empty");
        }
        currentTurn += 1;*/
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
        switch (currentPlayerColor)
        {
            case "red": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerRed); break;
            case "blue": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerBlue); break;
            case "green": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerGreen); break;
            case "purple": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerPurple); break;
            case "white": tilemapUnits.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerWhite); break;
        }
    }

    public void SpawnPlayersAtCamp(int activePlayers)
    {
        int random;
        if (activePlayers > 0)
        {
            random = Random.Range(1, 9);
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
            }
            playerOnePosition = boardPosition;
            switch (GameMain.playerOneColor)
            {
                case "red": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerRed); break;
                case "blue": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerBlue); break;
                case "green": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerGreen); break;
                case "purple": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerPurple); break;
                case "white": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerWhite); break;
            }
        }
        if (activePlayers > 1)
        {
            random = Random.Range(1, 9);
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
            }
            playerTwoPosition = boardPosition;
            switch (GameMain.playerTwoColor)
            {
                case "red": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerRed); break;
                case "blue": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerBlue); break;
                case "green": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerGreen); break;
                case "purple": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerPurple); break;
                case "white": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerWhite); break;
            }
        }
        if (activePlayers > 2)
        {
            random = Random.Range(1, 9);
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
            }
            playerThreePosition = boardPosition;
            switch (GameMain.playerThreeColor)
            {
                case "red": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerRed); break;
                case "blue": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerBlue); break;
                case "green": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerGreen); break;
                case "purple": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerPurple); break;
                case "white": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerWhite); break;
            }
        }
        if (activePlayers > 3)
        {
            random = Random.Range(1, 9);
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
            }
            playerFourPosition = boardPosition;
            switch (GameMain.playerFourColor)
            {
                case "red": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerRed); break;
                case "blue": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerBlue); break;
                case "green": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerGreen); break;
                case "purple": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerPurple); break;
                case "white": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), playerWhite); break;
            }
        }
    }

    void CampGenerator()
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
            }
            else if (random == 2)
            {
                section = "toll";
            }
            if (random == 3)
            {
                section = "empty";
            }
            boardClockPosition.Add(1, section);
            boardClockPosition.Add(2, "empty");
            random = Random.Range(1, 4);
            if (random == 1)
            {
                section = "loop";
            }
            else if (random == 2)
            {
                section = "toll";
            }
            if (random == 3)
            {
                section = "empty";
            }
            boardClockPosition.Add(3, section);
            boardClockPosition.Add(4, "empty");
            random = Random.Range(1, 4);
            if (random == 1)
            {
                section = "loop";
            }
            else if (random == 2)
            {
                section = "toll";
            }
            if (random == 3)
            {
                section = "empty";
            }
            boardClockPosition.Add(5, section);
            boardClockPosition.Add(6, "empty");
            random = Random.Range(1, 4);
            if (random == 1)
            {
                section = "loop";
            }
            else if (random == 2)
            {
                section = "toll";
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
            boardCrossroads.Add(new Vector3Int(0, 4));
            boardCrossroads.Add(new Vector3Int(4, 0));
            boardCrossroads.Add(new Vector3Int(0, -4));
            boardCrossroads.Add(new Vector3Int(-4, 0));
        }
        else if (GameMain.currentBoard == "graveyard")
        {
            //
        }
        else if (GameMain.currentBoard == "moonfield")
        {
            //
        }
        else if (GameMain.currentBoard == "machine")
        {
            //
        }
    }
}