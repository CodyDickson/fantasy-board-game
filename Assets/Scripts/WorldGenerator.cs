using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class WorldGenerator : MonoBehaviour
{
    // Map Settings //
    public static bool mapSizeRandom = false;
    public static string mapSize = "medium";
    public static int mapLoops = 0;
    public static int mapPassages = 0;
    // Board tracking //
    public static List<Vector3> boardPositions = new List<Vector3>();
    public static Vector3 boardPosition;
    public static List<Vector3> boardCampPositions = new List<Vector3>();
    public static List<Vector3> boardCrossroads = new List<Vector3>();
    public static List<Vector3> boardStartAndEndLoops = new List<Vector3>();
    public static Dictionary<int, string> boardStructures = new Dictionary<int, string>();
    public static Dictionary<int, string> boardUnits = new Dictionary<int, string>();
    public static int boardLength;
    public bool firstSectionActive;
    public bool secondSectionActive;
    public bool thirdSectionActive;
    // Tiles and tilemaps //
    [SerializeField] public Tile playeRed;
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

    void Start()
    {
        CampGenerator();
        LoopGenerator();
        TerrainGenerator();
        if (mapSizeRandom)
        {
            int randomMapSize = Random.Range(1,4);
            if (randomMapSize == 1)
            {
                mapSize = "small";
            }
            else if (randomMapSize == 2)
            {
                mapSize = "medium";
            }
            else if (randomMapSize == 3)
            {
                mapSize = "large";
            }
        }
    }

    void Update()
    {
        
    }

    void TerrainGenerator()
    {
        int randomTerrainType = 0;
        int xSize = 25;
        int ySize = 25;
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

    void CampGenerator()
    {
        tilemapBoardConnectors.SetTile(new Vector3Int(0, 0), camp);
        tilemapBoardConnectors.SetTile(new Vector3Int(0, 1), bcHorizontal);
        tilemapBoardConnectors.SetTile(new Vector3Int(0, -1), bcHorizontal);
        tilemapBoardConnectors.SetTile(new Vector3Int(1, 0), bcVertical);
        tilemapBoardConnectors.SetTile(new Vector3Int(-1, 0), bcVertical);
        tilemapBoardConnectors.SetTile(new Vector3Int(-1, 1), bcTopLeftCorner);
        tilemapBoardConnectors.SetTile(new Vector3Int(1, 1), bcTopRightCorner);
        tilemapBoardConnectors.SetTile(new Vector3Int(-1,-1), bcBottomLeftCorner);
        tilemapBoardConnectors.SetTile(new Vector3Int(1, -1), bcBottomRightCorner);
        // boardCampPositions.Add(new Vector3(0, 1));
    }

    void LoopGenerator()
    {
        int rowLength = 10;
        int randomRowLength = 0;
        switch (mapSize)
        {
            case "small": randomRowLength = Random.Range(3,7); rowLength = randomRowLength; break;
            case "medium": randomRowLength = Random.Range(10,15); rowLength = randomRowLength; break;
            case "large": randomRowLength = Random.Range(16,21); rowLength = randomRowLength; break;
        }
        // Determine where the loop will spawn
        if (!firstSectionActive)
        {
            int random = 1;
            // Random.Range(1,5);
            string loopDirection = "";
            if (random == 1)
            {
                tilemapBoardConnectors.SetTile(new Vector3Int(0, 1), bcThreeUp);
                tilemapBoardConnectors.SetTile(new Vector3Int(0, 2), bcVertical);
                boardPositions.Add(new Vector3Int(0,2));
                tilemapBoardConnectors.SetTile(new Vector3Int(0, 3), bcVertical);
                boardPositions.Add(new Vector3Int(0,3));
                tilemapBoardConnectors.SetTile(new Vector3Int(0, 4), bcThreeDown);
                boardPositions.Add(new Vector3Int(0,4));
                boardCrossroads.Add(new Vector3Int(0,4));
                boardStartAndEndLoops.Add(new Vector3Int(0,4));
                loopDirection = "up";
            }
            else if (random == 2)
            {
                tilemapBoardConnectors.SetTile(new Vector3Int(1, 0), bcThreeRight);
                tilemapBoardConnectors.SetTile(new Vector3Int(2, 0), bcHorizontal);
                boardPositions.Add(new Vector3Int(2,0));
                tilemapBoardConnectors.SetTile(new Vector3Int(3, 0), bcHorizontal);
                boardPositions.Add(new Vector3Int(3,0));
                tilemapBoardConnectors.SetTile(new Vector3Int(4, 0), bcThreeLeft);
                boardPositions.Add(new Vector3Int(4,0));
                boardCrossroads.Add(new Vector3Int(4,0));
                boardStartAndEndLoops.Add(new Vector3Int(4,0));
                loopDirection = "right";
            }
            else if (random == 3)
            {
                tilemapBoardConnectors.SetTile(new Vector3Int(0, -1), bcThreeDown);
                tilemapBoardConnectors.SetTile(new Vector3Int(0, -2), bcVertical);
                boardPositions.Add(new Vector3Int(0,-2));
                tilemapBoardConnectors.SetTile(new Vector3Int(0, -3), bcVertical);
                boardPositions.Add(new Vector3Int(0,-3));
                tilemapBoardConnectors.SetTile(new Vector3Int(0, -4), bcThreeUp);
                boardPositions.Add(new Vector3Int(0,-4));
                boardCrossroads.Add(new Vector3Int(0,-4));
                boardStartAndEndLoops.Add(new Vector3Int(0,-4));
                loopDirection = "down";
            }
            else if (random == 4)
            {
                tilemapBoardConnectors.SetTile(new Vector3Int(-1, 0), bcThreeLeft);
                tilemapBoardConnectors.SetTile(new Vector3Int(-2, 0), bcHorizontal);
                boardPositions.Add(new Vector3Int(-2,0));
                tilemapBoardConnectors.SetTile(new Vector3Int(-3, 0), bcHorizontal);
                boardPositions.Add(new Vector3Int(-3,0));
                tilemapBoardConnectors.SetTile(new Vector3Int(-4, 0), bcThreeRight);
                boardPositions.Add(new Vector3Int(-4,0));
                boardCrossroads.Add(new Vector3Int(-4,0));
                boardStartAndEndLoops.Add(new Vector3Int(-4,0));
                loopDirection = "right";
            }
            for (int i = 1; i <= rowLength; i++)
            {
                if (loopDirection == "up")
                {
                    boardPosition = boardStartAndEndLoops[0];
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)boardPosition[0] + i, (int)boardPosition[1]), bcHorizontal);
                    boardPositions.Add(new Vector3((1 + i), rowLength + 6));
                }
            }
            firstSectionActive = true;
        }
        /*
        // Vertical Row One
        for (int i = 1; i <= rowLength; i++)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int(0, (4 + i)), bcVertical);
            boardPositions.Add(new Vector3(0, (4 + i)));
            boardLength += 1;
        }
        // random ThreeRight or TopLeftCorner
        int boardChoice = Random.Range(1,3);
        if (boardChoice == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int(0, rowLength + 5), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, rowLength + 6), bcThreeRight);
            tilemapBoardConnectors.SetTile(new Vector3Int(1, rowLength + 6), bcHorizontal);
        }
        else if (boardChoice == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int(0, rowLength + 5), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, rowLength + 6), bcTopLeftCorner);
            tilemapBoardConnectors.SetTile(new Vector3Int(1, rowLength + 6), bcHorizontal);
        }
        // Horizontal Row Two Going Right
        for (int i = 1; i <= rowLength; i++)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((1 + i), rowLength + 6), bcHorizontal);
            boardPositions.Add(new Vector3((1 + i), rowLength + 6));
            boardLength += 1;
        }
        tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 2, rowLength + 6), bcHorizontal);
        tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 3, rowLength + 6), bcTopRightCorner);
        // Vertical Row Two
        tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 3, 4), bcVertical);
        for (int i = rowLength; i >= 1; i--)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 3, (4 + i)), bcVertical);
            boardPositions.Add(new Vector3(rowLength + 3, (4 + i)));
            boardLength += 1;
        }
        tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 3, rowLength + 5), bcVertical);
        /*boardLength += 1;
        boardPositions.Add(new Vector3(rowLength + 3, rowLength + 5));
        boardSlotPositions.Add(new Vector3(rowLength + 2, rowLength + 5));
        // random ThreeUp or BottomRightCorner
        boardChoice = Random.Range(1,3);
        if (boardChoice == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 3, 3), bcBottomRightCorner);
        }
        else if (boardChoice == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 3, 3), bcThreeUp);
        }
        // Horizontal Row One
        for (int i = rowLength; i >= 1; i--)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((1 + i), 3), bcHorizontal);
            boardPositions.Add(new Vector3((1 + i), 3));
            boardSlotPositions.Add(new Vector3((1 + i), 4));
            boardLength += 1;
        }*/
    }

    void PassageGenerator()
    {

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
            if (player_in_camp_one)
            {
                switch (player_color_one)
                {
                    case "red": tilemapStructures.SetTile(new Vector3Int(0, 1), player_red); break;
                    case "blue": tilemapStructures.SetTile(new Vector3Int(0, 1), player_blue); break;
                    case "green": tilemapStructures.SetTile(new Vector3Int(0, 1), player_green); break;
                    case "purple": tilemapStructures.SetTile(new Vector3Int(0, 1), player_purple); break;
                    case "white": tilemapStructures.SetTile(new Vector3Int(0, 1), player_white); break;
                    default: tilemapStructures.SetTile(new Vector3Int(0, 1), player); break;
                }
            }
            if (player_in_camp_two)
            {
                switch (player_color_two)
                {
                    case "red": tilemapStructures.SetTile(new Vector3Int(1, 0), player_red); break;
                    case "blue": tilemapStructures.SetTile(new Vector3Int(1, 0), player_blue); break;
                    case "green": tilemapStructures.SetTile(new Vector3Int(1, 0), player_green); break;
                    case "purple": tilemapStructures.SetTile(new Vector3Int(1, 0), player_purple); break;
                    case "white": tilemapStructures.SetTile(new Vector3Int(1, 0), player_white); break;
                    default: tilemapStructures.SetTile(new Vector3Int(1, 0), player); break;
                }
            }
            if (player_in_camp_three)
            {
                switch (player_color_three)
                {
                    case "red": tilemapStructures.SetTile(new Vector3Int(0, -1), player_red); break;
                    case "blue": tilemapStructures.SetTile(new Vector3Int(0, -1), player_blue); break;
                    case "green": tilemapStructures.SetTile(new Vector3Int(0, -1), player_green); break;
                    case "purple": tilemapStructures.SetTile(new Vector3Int(0, -1), player_purple); break;
                    case "white": tilemapStructures.SetTile(new Vector3Int(0, -1), player_white); break;
                    default: tilemapStructures.SetTile(new Vector3Int(0, -1), player); break;
                }
            }
            if (player_in_camp_four)
            {
                switch (player_color_four)
                {
                    case "red": tilemapStructures.SetTile(new Vector3Int(-1, 0), player_red); break;
                    case "blue": tilemapStructures.SetTile(new Vector3Int(-1, 0), player_blue); break;
                    case "green": tilemapStructures.SetTile(new Vector3Int(-1, 0), player_green); break;
                    case "purple": tilemapStructures.SetTile(new Vector3Int(-1, 0), player_purple); break;
                    case "white": tilemapStructures.SetTile(new Vector3Int(-1, 0), player_white); break;
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
            boardMonsters.Add(x, "empty");
        }
        currentTurn += 1;*/
    }
}