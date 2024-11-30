using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class WorldGenerator : MonoBehaviour
{
    // Map Settings //
    public static bool mapSizeRandom = false;
    // Board tracking //
    public static List<Vector3> boardPositions = new List<Vector3>();
    public static List<Vector3> localBoardPositions = new List<Vector3>();
    public static Vector3 boardPosition;
    public static List<Vector3> boardSlotPositions = new List<Vector3>();
    public static List<Vector3> boardCampPositions = new List<Vector3>();
    public static List<Vector3> boardCrossroads = new List<Vector3>();
    public static List<Vector3> boardStartAndEndLoops = new List<Vector3>();
    public static Dictionary<int, string> boardClockPosition = new Dictionary<int, string>();
    public static Dictionary<int, string> boardLoopDirection = new Dictionary<int, string>();
    public static Dictionary<int, string> boardStructures = new Dictionary<int, string>();
    public static Dictionary<int, string> boardUnits = new Dictionary<int, string>();
    public static int boardLength;
    bool firstSectionActive;
    bool secondSectionActive;
    bool thirdSectionActive;
    string loopDirection;
    bool leftPassage = false;
    bool rightPassage = false;
    bool downPassage = false;
    bool topPassage = false;
    int loopLength;
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
    }

    void Update()
    {
        
    }

    void CheckForLocalBoardPositions()
    {
        boardPosition = boardPositions[GameMain.currentUnitPosition];
        Vector3 north;
        north = new Vector3((boardPositions[0]), boardPositions[1]);
        Vector3 east = new Vector3((int)boardPositions[0] && ((int)boardPositions[1] + 1));
        Vector3 south = new Vector3(((int)boardPositions[0] - 1) && (int)boardPositions[1]);
        Vector3 west = new Vector3((int)boardPositions[0] && ((int)boardPositions[1] - 1));
        foreach (Vector3 listVector in boardPositions)
        {
            if (listVector == north)
            {
                localBoardPositions.Add(new Vector3(north[0], north[1]));
            }
            else if (listVector == east)
            {
                localBoardPositions.Add(new Vector3(east[0], east[1]));
            }
            else if (listVector == south)
            {
                localBoardPositions.Add(new Vector3(south[0], south[1]));
            }
            else if (listVector == west)
            {
                localBoardPositions.Add(new Vector3(west[0], west[1]));
            }
        }
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
        boardCampPositions.Add(new Vector3(0, 1));
        boardCampPositions.Add(new Vector3(0, -1));
        boardCampPositions.Add(new Vector3(1, 0));
        boardCampPositions.Add(new Vector3(-1, 0));
        boardCampPositions.Add(new Vector3(1, 1));
        boardCampPositions.Add(new Vector3(-1, -1));
        boardCampPositions.Add(new Vector3(-1, 1));
        boardCampPositions.Add(new Vector3(1, -1));
    }

    void LoopGenerator()
    {
        int rowLength = 10;
        int randomRowLength = 0;
        // Determine where the loop will spawn
        if (!firstSectionActive)
        {
            int random = 1;
            // Random.Range(1,5);
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
                boardClockPosition.Add(1, "loop");
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
                boardClockPosition.Add(2, "loop");
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
                boardClockPosition.Add(3, "loop");
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
                boardClockPosition.Add(4, "loop");
            }
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
                boardPosition = boardStartAndEndLoops[0];
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
                boardPosition = boardStartAndEndLoops[0];
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