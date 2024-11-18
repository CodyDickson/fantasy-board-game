using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class GameMain : MonoBehaviour
{
   // Determines which screen (and content) is displayed
    public static bool GUIEnabled = true;
    public static bool villageScreenEnabled = false;
    public static bool chestScreenEnabled = false;
    public static bool campScreenEnabled = false;
    public static bool dungeonScreenEnabled = false;
    public static bool opposingPlayerScreenEnabled = false;
    public static bool settingsScreenEnabled = false;
    public static bool combatScreenEnabled = false;
    public static bool secondaryButtonEnabled = false;
    [SerializeField] public TMP_Text centerDisplayText;
    public static string centerDisplayTextContent;
    [SerializeField] public TMP_Text currentTurnText;
    public static bool playerIsMoving = false;
    public static bool playerIsFinishedMoving = false;
    public static bool diceShouldFadeAway = false;
    public static bool diceShouldFadeAwayImmediately = false;
    public static bool diceShouldShow = false;
    public static bool diceOneShow = false;
    public static bool diceTwoShow = false;
    public static bool diceThreeShow = false;
    public static bool diceFourShow = false;
    public static bool diceFiveShow = false;
    public static bool diceSixShow = false;
    public GameObject GUI;
    public GameObject combatScreen;
    public GameObject chestScreen;
    public GameObject villageScreen;
    public GameObject dungeonScreen;
    public GameObject settingsScreen;
    public GameObject diceOne;
    public GameObject diceTwo;
    public GameObject diceThree;
    public GameObject diceFour;
    public GameObject diceFive;
    public GameObject diceSix;

    // Player info
    public static int goldPlayer1;
    public static int goldPlayer2;
    public static int goldPlayer3;
    public static int goldPlayer4;
    public static int combatDicePlayer1 = 1;
    public static int combatDicePlayer2 = 1;
    public static int combatDicePlayer3 = 1;
    public static int combatDicePlayer4 = 1;
    public static int moveDicePlayer1 = 1;
    public static int moveDicePlayer2 = 1;
    public static int moveDicePlayer3 = 1;
    public static int moveDicePlayer4 = 1;

    // Current game info
    public static int currentPlayer = 1;
    public static int activePlayers = 2;
    public static string currentBoard = "grasslands";
    public static int currentTurn = 0;

    // Current move info
    public static int currentUnitPosition = 0;
    public static int newUnitPosition = 0;
    public static int currentPlayerDice = 0;
    public static int currentAvatar = 0;
    
    // Gameplay Settings
    public static bool fastAIMovement = false;
    public static bool fastPlayerMovement = false;

    // Player position tracking
    public static int unitPositionPlayer1;
    public static int unitPositionPlayer2;
    public static int unitPositionPlayer3;
    public static int unitPositionPlayer4;
    public static int campPositionPlayer1;
    public static int campPositionPlayer2;
    public static int campPositionPlayer3;
    public static int campPositionPlayer4;
    public static int[] xy = new int[2];

    // Buttons
    public static bool endTurnButtonEnabled = false;
    public static bool bottomLeftLowerButtonEnabled = true;

    // Settings
    public static bool skipUnitMovement = false;
    public static bool skipAITurn = true;
    public static int dungeonCount = 0;
    public static int dungeonCap = 10;
    public static bool mapSizeRandom = false;
    public  static string mapSize = "medium";

    // Board tracking
    public static List<Vector3> boardPositions = new List<Vector3>();
    public static List<Vector3> boardSlotPositions = new List<Vector3>();
    public static Dictionary<int, string> boardStructures = new Dictionary<int, string>();
    public static Dictionary<int, string> boardUnits = new Dictionary<int, string>();
    public static int boardLength;

    // Tiles and tilemaps
    [SerializeField] public Tile player;
    [SerializeField] public Tile playerGreen;
    [SerializeField] public Tile playerBlue;
    [SerializeField] public Tile monsterImp;
    [SerializeField] public Tile monsterBasilisk;
    [SerializeField] public Tile chest;
    [SerializeField] public Tile oddity;
    [SerializeField] public Tile dungeon;
    [SerializeField] public Tile village;
    [SerializeField] public Tile grassOne;
    [SerializeField] public Tile grassTwo;
    [SerializeField] public Tile grassThree;
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

    // Combat
    [SerializeField] public TMP_Text combatUnitOne_Text;
    [SerializeField] public TMP_Text combatUnitTwo_Text;
    [SerializeField] public GameObject combatUnitOne_Avatar;
    [SerializeField] public GameObject combatUnitTwo_Avatar;
    public Image playerGreenAvatar;
    public Image playerBlueAvatar;
    public Image impAvatar;
    public Image basiliskAvatar;
    public static int combatUnitOne_DiceTotal = 0;
    public static int combatUnitTwo_DiceTotal = 0;
    public static int combatUnitOne;
    public static int combatUnitTwo;
    public static string currentEnemy = "";
    public static string dungeonType = "";
    public static bool combatEncounterHappening = false;

    // Monsters
    public static Dictionary<int, string> boardMonsters = new Dictionary<int, string>();

    // Time tracking
    private float counter = 0.5f;
    private float counter2 = 2f;
    private float tempCounter = 0f;
    private float tempCounter2 = 0f;

    void Start()
    {
        currentBoard = "grasslands";
        activePlayers = 2;
        GameSetup(currentBoard, activePlayers);
        GUI.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && bottomLeftLowerButtonEnabled)
        {
            MoveUnit(tilemapStructures, player);
        }
        if (Input.GetKeyDown(KeyCode.E) && endTurnButtonEnabled)
        {
            EndTurn(tilemapStructures, monsterImp, monsterBasilisk);
        }
        if (Input.GetKeyDown(KeyCode.V) && secondaryButtonEnabled)
        {
            BuildVillage(tilemapStructures, village);
        }

        if (combatScreenEnabled)
        {
            // Display handling
            GUI.SetActive(false);
            if (!dungeonScreenEnabled)
            {
                dungeonScreen.SetActive(false);
            }
            combatScreen.SetActive(true);
            
            if (!combatEncounterHappening)
            {
                CombatEncounter(tilemapStructures, player, monsterImp, monsterBasilisk);
            }

            // Update combat text
            combatUnitOne_Text.text = "Player " + currentPlayer + "\n" + combatUnitOne_DiceTotal;
            if (combatUnitTwo < 5)
            {
                combatUnitTwo_Text.text = "Player " + combatUnitTwo + "\n" + combatUnitTwo_DiceTotal;
            }
            else if (combatUnitTwo == 5)
            {
                combatUnitTwo_Text.text = "Imp\n" + combatUnitTwo_DiceTotal;                
            }
            else if (combatUnitTwo == 6)
            {
                combatUnitTwo_Text.text = "Basilisk\n" + combatUnitTwo_DiceTotal;
            }
        }
        else if (villageScreenEnabled)
        {
            GUI.SetActive(false);
        }
        else if (playerIsMoving)
        {
            if (tempCounter <= 0f)
            {
                // Sets avatar at previous position
                if (currentAvatar == 0)
                {
                    SetCurrentBoardSpace("space" + currentUnitPosition);
                    tilemapStructures.SetTile(new Vector3Int(xy[0], xy[1]), null);
                }
                else if (currentAvatar > 0 && currentAvatar <= 4)
                {
                    SetCurrentBoardSpace("space" + currentUnitPosition);
                    tilemapStructures.SetTile(new Vector3Int(xy[0], xy[1]), player);
                }
                else if (currentAvatar == 5)
                {
                    SetCurrentBoardSpace("space" + currentUnitPosition);
                    tilemapStructures.SetTile(new Vector3Int(xy[0], xy[1]), monsterImp);
                }
                else if (currentAvatar == 6)
                {
                    SetCurrentBoardSpace("space" + currentUnitPosition);
                    tilemapStructures.SetTile(new Vector3Int(xy[0], xy[1]), monsterBasilisk);
                }
                // Add 200 gold if the unit passes their own camp
                currentUnitPosition += 1;
                if (currentPlayer == 1 && campPositionPlayer1 == currentUnitPosition)
                {
                    goldPlayer1 += 200;
                }
                else if (currentPlayer == 2 && campPositionPlayer2 == currentUnitPosition)
                {
                    goldPlayer2 += 200;
                }
                else if (currentPlayer == 3 && campPositionPlayer3 == currentUnitPosition)
                {
                    goldPlayer3 += 200;
                }
                else if (currentPlayer == 4 && campPositionPlayer4 == currentUnitPosition)
                {
                    goldPlayer4 += 200;
                }

                // Loop around the board
                if (currentUnitPosition > boardLength)
                {
                    currentUnitPosition -= boardLength;
                }

                // Save whatever unit is currently at the next position
                if (currentPlayer != 1 && unitPositionPlayer1 == currentUnitPosition)
                {
                    currentAvatar = 1;
                }
                else if (currentPlayer != 2 && unitPositionPlayer2 == currentUnitPosition)
                {
                    currentAvatar = 2;
                }
                else if (currentPlayer != 3 && unitPositionPlayer3 == currentUnitPosition)
                {
                    currentAvatar = 3;
                }
                else if (currentPlayer != 4 && unitPositionPlayer4 == currentUnitPosition)
                {
                    currentAvatar = 4;
                }
                else if (boardMonsters[currentUnitPosition] == "imp")
                {
                    currentAvatar = 5;
                    currentEnemy = "imp";
                }
                else if (boardMonsters[currentUnitPosition] == "basilisk")
                {
                    currentAvatar = 6;
                    currentEnemy = "basilisk";
                }
                else
                {
                    currentAvatar = 0;
                }
                // Sets avatar at next position
                SetCurrentBoardSpace("space" + currentUnitPosition);
                tilemapStructures.SetTile(new Vector3Int(xy[0], xy[1]), player);
                if (currentUnitPosition == newUnitPosition)
                {
                    playerIsMoving = false;
                    if (currentAvatar == 0)
                    {
                        MoveUnitComplete(tilemapStructures, player);
                    }
                    else
                    {
                        CombatEncounter(tilemapStructures, player, monsterImp, monsterBasilisk);
                        MoveUnitComplete(tilemapStructures, player);
                    }
                }
                tempCounter = counter;
            }
            else
            {
                tempCounter -= Time.deltaTime;
            }
        }
        else if (chestScreenEnabled)
        {
            GUI.SetActive(false);
            chestScreen.SetActive(true);
        }
        else if (dungeonScreenEnabled)
        {
            GUI.SetActive(false);
            dungeonScreen.SetActive(true);
        }
        else if (GUIEnabled)
        {
            GUI.SetActive(true);
            currentTurnText.text = "Current Turn: " + currentTurn;
            centerDisplayText.text = centerDisplayTextContent;
            combatScreen.SetActive(false);
            chestScreen.SetActive(false);
            dungeonScreen.SetActive(false);
        }

        if (diceShouldShow)
        {
            if (diceOneShow)
            {
                diceOne.SetActive(true);
                diceOneShow = false;
            }
            else if (diceTwoShow)
            {
                diceTwo.SetActive(true);
                diceTwoShow = false;
            }
            else if (diceThreeShow)
            {
                diceThree.SetActive(true);
                diceThreeShow = false;
            }
            else if (diceFourShow)
            {
                diceFour.SetActive(true);
                diceFourShow = false;
            }
            else if (diceFiveShow)
            {
                diceFive.SetActive(true);
                diceFiveShow = false;
            }
            else if (diceSixShow)
            {
                diceSix.SetActive(true);
                diceSixShow = false;
            }
            diceShouldShow = false;
            diceShouldFadeAway = true;
        }
        if (diceShouldFadeAway)
        {
            if (tempCounter2 >= counter2)
            {
                diceOne.SetActive(false);
                diceTwo.SetActive(false);
                diceThree.SetActive(false);
                diceFour.SetActive(false);
                diceFive.SetActive(false);
                diceSix.SetActive(false);
                diceShouldFadeAway = false;
                tempCounter2 = 0;
            }
            else
            {
                tempCounter2 += Time.deltaTime;
            }
        }
        if (diceShouldFadeAwayImmediately)
        {
            diceOne.SetActive(false);
            diceTwo.SetActive(false);
            diceThree.SetActive(false);
            diceFour.SetActive(false);
            diceFive.SetActive(false);
            diceSix.SetActive(false);
            diceShouldFadeAwayImmediately = false;
            diceShouldFadeAway = false;
            diceShouldShow = false;
        }
    }

    void GameSetup(string currentBoard, int activePlayers)
    {
        // Starting gold for players
        int startingGold = 150;
        if (activePlayers >= 1)
        {
            goldPlayer1 = startingGold;
        }
        if (activePlayers >= 2)
        {
            goldPlayer2 = startingGold;
        }
        if (activePlayers >= 3)
        {
            goldPlayer3 = startingGold;
        }
        if (activePlayers >= 4)
        {
            goldPlayer4 = startingGold;
        }
        
        // Board Settings
        dungeonCap = 10;
        if (currentBoard == "random")
        {
            //
        }
        else if (currentBoard == "grasslands")
        {
            // Terrain Generation
            int randomTerrainType = 0;
            int xSize = 25;
            int ySize = 25;
            for (int z = 0, y = 0; y <= ySize; y++)
            {
                for (int x = 0; x <= xSize; x++, z++)
                {
                    randomTerrainType = Random.Range(1,101);
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
            }
            // Board Connectors
            mapSize = "medium";
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
            int rowLength = 12;
            if (mapSize == "small")
            {
                int randomRowLength = Random.Range(3,7);
                rowLength = randomRowLength;
            }
            else if (mapSize == "medium")
            {
                int randomRowLength = Random.Range(8,13);
                rowLength = randomRowLength;
            }
            else if (mapSize == "large")
            {
                int randomRowLength = Random.Range(15,21);
                rowLength = randomRowLength;
            }
            Debug.Log("Row Length: " + rowLength);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 0), camp);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 1), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 2), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 3), bcThreeRight);
            campPositionPlayer1 = 1;
            tilemapStructures.SetTile(new Vector3Int(0, 4), player);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 4), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int(1, 3), bcHorizontal);
            // Vertical Row One
            for (int i = 1; i <= rowLength; i++)
            {
                tilemapBoardConnectors.SetTile(new Vector3Int(0, (4 + i)), bcVertical);
                boardPositions.Add(new Vector3(0, (4 + i)));
                boardSlotPositions.Add(new Vector3(1, (4 + i)));
                boardLength += 1;
            }    
            // random ThreeRight or TopLeftCorner
            int boardChoice = Random.Range(1,3);
            if (boardChoice == 1)
            {
                tilemapBoardConnectors.SetTile(new Vector3Int(0, rowLength + 5), bcVertical);
                tilemapBoardConnectors.SetTile(new Vector3Int(0, rowLength + 6), bcThreeRight);
                tilemapBoardConnectors.SetTile(new Vector3Int(1, rowLength + 6), bcHorizontal);
                for (int i = 1; i <= rowLength; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((1 + i), rowLength + 6), bcHorizontal);
                    boardPositions.Add(new Vector3((1 + i), rowLength + 6));
                    boardSlotPositions.Add(new Vector3((1 + i), rowLength + 5));
                    boardLength += 1;
                }
                tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 2, rowLength + 6), bcHorizontal);
                tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 3, rowLength + 6), bcTopRightCorner);
            }
            else if (boardChoice == 2)
            {
                tilemapBoardConnectors.SetTile(new Vector3Int(0, rowLength + 5), bcVertical);
                tilemapBoardConnectors.SetTile(new Vector3Int(0, rowLength + 6), bcTopLeftCorner);
                tilemapBoardConnectors.SetTile(new Vector3Int(1, rowLength + 6), bcHorizontal);
                for (int i = 1; i <= rowLength; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((1 + i), rowLength + 6), bcHorizontal);
                    boardPositions.Add(new Vector3((1 + i), rowLength + 6));
                    boardSlotPositions.Add(new Vector3((1 + i), rowLength + 5));
                    boardLength += 1;
                }
                tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 2, rowLength + 6), bcHorizontal);
                tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 3, rowLength + 6), bcTopRightCorner);
            }
            // Horizontal Row One
            for (int i = 1; i <= rowLength; i++)
            {
                tilemapBoardConnectors.SetTile(new Vector3Int((1 + i), 3), bcHorizontal);
                boardPositions.Add(new Vector3((1 + i), 3));
                boardSlotPositions.Add(new Vector3((1 + i), 4));
                boardLength += 1;
            }
            tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 2, 3), bcHorizontal);
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
            // Vertical Row Two
            tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 3, 4), bcVertical);
            for (int i = 1; i <= rowLength; i++)
            {
                tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 3, (4 + i)), bcVertical);
                boardPositions.Add(new Vector3(rowLength + 3, (4 + i)));
                boardSlotPositions.Add(new Vector3(rowLength + 2, (4 + i)));
                boardLength += 1;
            }
            tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 3, rowLength + 5), bcVertical);
        }
        else if (currentBoard == "graveyard")
        {
            //
        }
        else if (currentBoard == "seasons")
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
        for (int x = 1; x <= boardLength; x++)
        {
            Debug.Log(x);
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
                Vector3 position = boardSlotPositions[x - 1];
                tilemapStructures.SetTile(new Vector3Int((int)position[0], (int)position[1]), dungeon);
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
                Vector3 position = boardSlotPositions[x - 1];
                tilemapStructures.SetTile(new Vector3Int((int)position[0], (int)position[1]), chest);
            }
            else if (random >= 99)
            {
                Vector3 position = boardSlotPositions[x - 1];
                tilemapStructures.SetTile(new Vector3Int((int)position[0], (int)position[1]), oddity);
            }
            boardMonsters.Add(x, "empty");
        }
        currentTurn += 1;
    }

    public static void EndTurn(Tilemap tilemap, Tile monsterImp, Tile monsterBasilisk)
    {
        endTurnButtonEnabled = false;
        bottomLeftLowerButtonEnabled = true;
        secondaryButtonEnabled = false;

        // Save current position to the relevant player
        if (currentPlayer == 1)
        {
            unitPositionPlayer1 = currentUnitPosition;
        }
        else if (currentPlayer == 2)
        {
            unitPositionPlayer2 = currentUnitPosition;
        }
        else if (currentPlayer == 3)
        {
            unitPositionPlayer3 = currentUnitPosition;
        }
        else if (currentPlayer == 4)
        {
            unitPositionPlayer4 = currentUnitPosition;
        }
        
        // Determine next player
        currentPlayer += 1;
        if (currentPlayer > activePlayers)
        {
            currentPlayer = 1;
            currentTurn += 1;
            // Spawn monsters
            for (int i = 1; i <= boardLength; i++)
            {
                bool unitPositionClear = true;
                if (unitPositionPlayer1 == i)
                {
                    unitPositionClear = false;
                }
                else if (unitPositionPlayer2 == i)
                {
                    unitPositionClear = false;
                }
                else if (unitPositionPlayer3 == i)
                {
                    unitPositionClear = false;
                }
                else if (unitPositionPlayer4 == i)
                {
                    unitPositionClear = false;
                }
                if (boardStructures[i] == "dungeonImp" && unitPositionClear)
                {
                    int random = Random.Range(1,5);
                    if (random == 1)
                    {
                        if (boardMonsters[i] == "empty")
                        {
                            boardMonsters[i] = "imp";
                            SetCurrentBoardSpace("space" + i);
                            tilemap.SetTile(new Vector3Int(xy[0], xy[1]), monsterImp);
                        }
                    }
                }
                else if (boardStructures[i] == "dungeonBasilisk" && unitPositionClear)
                {
                    int random = Random.Range(1,4);
                    if (random == 1)
                    {
                        if (boardMonsters[i] == "empty")
                        {
                            boardMonsters[i] = "basilisk";
                            SetCurrentBoardSpace("space" + i);
                            tilemap.SetTile(new Vector3Int(xy[0], xy[1]), monsterBasilisk);
                        }
                    }
                }
            }
        }

        // Update current player values to match the current player
        if (currentPlayer == 1)
        {
            currentUnitPosition = unitPositionPlayer1;
            currentPlayerDice = combatDicePlayer1;
        }
        else if (currentPlayer == 2)
        {
            currentUnitPosition = unitPositionPlayer2;
            currentPlayerDice = combatDicePlayer2;
        }
        else if (currentPlayer == 3)
        {
            currentUnitPosition = unitPositionPlayer3;
            currentPlayerDice = combatDicePlayer3;
        }
        else if (currentPlayer == 4)
        {
            currentUnitPosition = unitPositionPlayer4;
            currentPlayerDice = combatDicePlayer4;
        }
    }

    public static void MoveUnit(Tilemap tilemap, Tile player)
    {
        // Determine new unit position
        bottomLeftLowerButtonEnabled = false;
        int diceOneResult = Random.Range(1,7);
        int diceTwo = Random.Range(1,7);
        if (diceOneResult == 1)
        {
            diceShouldShow = true;
            diceOneShow = true;
        }
        else if (diceOneResult == 2)
        {
            diceShouldShow = true;
            diceTwoShow = true;
        }
        else if (diceOneResult == 3)
        {
            diceShouldShow = true;
            diceThreeShow = true;
        }
        else if (diceOneResult == 4)
        {
            diceShouldShow = true;
            diceFourShow = true;
        }
        else if (diceOneResult == 5)
        {
            diceShouldShow = true;
            diceFiveShow = true;
        }
        else if (diceOneResult == 6)
        {
            diceShouldShow = true;
            diceSixShow = true;
        }

        newUnitPosition = currentUnitPosition + diceOneResult;
        string newSpace = "space" + newUnitPosition;
        if (currentBoard == "grasslands")
        {
            if (newUnitPosition > 28)
            {
                newUnitPosition -= 28;
            }
        }
        playerIsMoving = true;
    }

    public static void ClearCurrentSlot(Tilemap tilemap)
    {
        SetCurrentBoardSpace("space" + currentUnitPosition);
        tilemap.SetTile(new Vector3Int(xy[0], xy[1]), null);
    }

    public static void MoveUnitComplete(Tilemap tilemap, Tile player)
    {
        currentUnitPosition = newUnitPosition;
        SetCurrentBoardSpace("space" + currentUnitPosition);
        tilemap.SetTile(new Vector3Int(xy[0], xy[1]), player);

        // Landing Encounters
        if (boardStructures[newUnitPosition] == "chest")
        {
            chestScreenEnabled = true;
            diceShouldFadeAwayImmediately = true;
            string position = "spaceSlot" + currentUnitPosition;
            SetCurrentBoardSpaceSlot(position);
            tilemap.SetTile(new Vector3Int(GameMain.xy[0], GameMain.xy[1]), null);
            boardStructures[newUnitPosition] = "empty";
        }
        else if (boardStructures[newUnitPosition] == "dungeonImp")
        {
            diceShouldFadeAwayImmediately = true;
            DungeonEncounter("imp");
        }
        else if (boardStructures[newUnitPosition] == "dungeonBasilisk")
        {
            diceShouldFadeAwayImmediately = true;
            DungeonEncounter("basilisk");
        }
        else if (boardStructures[newUnitPosition] == "oddity")
        {
            OddityEncounter();
        }
        else if (boardStructures[newUnitPosition] == "villagePlayer1" && currentPlayer != 1)
        {
            OpposingVillageEncounter(1, tilemap, player);
        }
        else if (boardStructures[newUnitPosition] == "villagePlayer2" && currentPlayer != 2)
        {
            OpposingVillageEncounter(2, tilemap, player);
        }
        else if (boardStructures[newUnitPosition] == "villagePlayer3" && currentPlayer != 3)
        {
            OpposingVillageEncounter(3, tilemap, player);
        }
        else if (boardStructures[newUnitPosition] == "villagePlayer4" && currentPlayer != 4)
        {
            OpposingVillageEncounter(4, tilemap, player);
        }
        else if (boardStructures[newUnitPosition] == "empty")
        {
            secondaryButtonEnabled = true;
        }
        endTurnButtonEnabled = true; 
    }

    public static string GenerateItem()
    {
        string itemName = "";

        return itemName;
    }

    public static void DungeonEncounter(string dungeon)
    {
        dungeonScreenEnabled = true;
        if (dungeon == "imp")
        {
            currentEnemy = "imp";
        }
        if (dungeon == "basilisk")
        {
            currentEnemy = "basilisk";
        }
    }

    public static void OddityEncounter()
    {
        Debug.Log("Oddity Encountered!");
    }

    public static void OpposingVillageEncounter(int villageOwner, Tilemap tilemap, Tile player)
    {
        Debug.Log("Landed on an opposing village!");
        int villageCost = 50;
        
        if (currentPlayer == 1 && goldPlayer1 >= villageCost)
        {
            goldPlayer1 -= villageCost;
        }
        else if (currentPlayer == 1 && goldPlayer1 < villageCost)
        {
            goldPlayer1 = 0;
            Debug.Log("Can't afford to pay!");
            ClearCurrentSlot(tilemap);
            ReturnToCamp(1, tilemap, player);
        }
        else if (currentPlayer == 2 && goldPlayer2 >= villageCost)
        {
            goldPlayer2 -= villageCost;
        }
        else if (currentPlayer == 2 && goldPlayer2 < villageCost)
        {
            goldPlayer2 = 0;
            Debug.Log("Can't afford to pay!");
            ClearCurrentSlot(tilemap);
            ReturnToCamp(2, tilemap, player);
        }
        else if (currentPlayer == 3 && goldPlayer3 >= villageCost)
        {
            goldPlayer3 -= villageCost;
        }
        else if (currentPlayer == 3 && goldPlayer3 < villageCost)
        {
            goldPlayer3 = 0;
            Debug.Log("Can't afford to pay!");
        }
        else if (currentPlayer == 4 && goldPlayer4 >= villageCost)
        {
            goldPlayer4 -= villageCost;
        }
        else if (currentPlayer == 4 && goldPlayer4 < villageCost)
        {
            goldPlayer4 = 0;
            Debug.Log("Can't afford to pay!");
        }

        if (villageOwner == 1)
        {
            goldPlayer1 += villageCost;
        }
        else if (villageOwner == 2)
        {
            goldPlayer2 += villageCost;
        }
        else if (villageOwner == 3)
        {
            goldPlayer3 += villageCost;
        }
        else if (villageOwner == 4)
        {
            goldPlayer4 += villageCost;
        }
    }

    public static void BuildVillage(Tilemap tilemap, Tile village)
    {
        int villageCost = 50;
        string position = "spaceSlot" + currentUnitPosition;
        SetCurrentBoardSpaceSlot(position);
        string currentSpaceSlot = boardStructures[currentUnitPosition];
        secondaryButtonEnabled = false;
        if (currentPlayer == 1 && goldPlayer1 >= villageCost && currentSpaceSlot == "empty")
        {
            goldPlayer1 -= 50;
            tilemap.SetTile(new Vector3Int(xy[0], xy[1]), village);
            boardStructures[currentUnitPosition] = "villagePlayer1";
        }
        else if (currentPlayer == 1 && goldPlayer1 < villageCost)
        {
            Debug.Log("Can't afford it");
        }
        else if (currentPlayer == 1 && currentSpaceSlot != "empty")
        {
            Debug.Log("Can't build here");
        }
        else if (currentPlayer == 2 && goldPlayer2 >= villageCost && currentSpaceSlot == "empty")
        {
            goldPlayer2 -= 50;
            tilemap.SetTile(new Vector3Int(xy[0], xy[1]), village);
            boardStructures[currentUnitPosition] = "villagePlayer2";
        }
        else if (currentPlayer == 2 && goldPlayer2 < villageCost)
        {
            Debug.Log("Can't afford it");
        }
    }

    public static void CombatEncounter(Tilemap tilemap, Tile player, Tile monsterImp, Tile monsterBasilisk)
    {
        combatEncounterHappening = true;
        combatScreenEnabled = true;
        GUIEnabled = false;

        // Determines who combat unit one is
        combatUnitOne = currentPlayer;

        // Determines who combat unit two is
        if (currentUnitPosition == unitPositionPlayer1 && currentPlayer != 1)
        {
            combatUnitTwo = 1;
        }
        else if (currentUnitPosition == unitPositionPlayer2 && currentPlayer != 2)
        {
            combatUnitTwo = 2;
        }
        else if (currentUnitPosition == unitPositionPlayer3 && currentPlayer != 3)
        {
            combatUnitTwo = 3;
        }
        else if (currentUnitPosition == unitPositionPlayer4 && currentPlayer != 4)
        {
            combatUnitTwo = 4;
        }
        else if (currentEnemy == "imp")
        {
            combatUnitTwo = 5;
        }
        else if (currentEnemy == "basilisk")
        {
            combatUnitTwo = 6;
        }

        // Determines dice roll for combat unit one
        combatUnitOne_DiceTotal = 0;
        if (combatUnitOne == 1)
        {
            int x = combatDicePlayer1;
            while (x > 0)
            {
                combatUnitOne_DiceTotal += Random.Range(1,7);
                x--;
            }
        }
        else if (combatUnitOne == 2)
        {
            int x = combatDicePlayer2;
            while (x > 0)
            {
                combatUnitOne_DiceTotal += Random.Range(1,7);
                x--;
            }
        }
        else if (combatUnitOne == 3)
        {
            int x = combatDicePlayer3;
            while (x > 0)
            {
                combatUnitOne_DiceTotal += Random.Range(1,7);
                x--;
            }
        }
        else if (combatUnitOne == 4)
        {
            int x = combatDicePlayer4;
            while (x > 0)
            {
                combatUnitOne_DiceTotal += Random.Range(1,7);
                x--;
            }
        }

        // Determines dice roll for combat unit two
        combatUnitTwo_DiceTotal = 0;
        if (combatUnitTwo == 1)
        {
            int x = combatDicePlayer1;
            while (x > 0)
            {
                combatUnitTwo_DiceTotal += Random.Range(1,7);
                x--;
            }
        }
        else if (combatUnitTwo == 2)
        {
            int x = combatDicePlayer2;
            while (x > 0)
            {
                combatUnitTwo_DiceTotal += Random.Range(1,7);
                x--;
            }
        }
        else if (combatUnitTwo == 3)
        {
            int x = combatDicePlayer3;
            while (x > 0)
            {
                combatUnitTwo_DiceTotal += Random.Range(1,7);
                x--;
            }
        }
        else if (combatUnitTwo == 4)
        {
            int x = combatDicePlayer4;
            while (x > 0)
            {
                combatUnitTwo_DiceTotal += Random.Range(1,7);
                x--;
            }
        }
        else if (combatUnitTwo == 5)
        {
            combatUnitTwo_DiceTotal += Random.Range(1,7);
        }
        else if (combatUnitTwo == 6)
        {
            combatUnitTwo_DiceTotal += Random.Range(2,6);
        }

        // Combat Results
        if (combatUnitOne_DiceTotal > combatUnitTwo_DiceTotal)
        {
            Debug.Log("Player " + combatUnitOne + " has won!");

            // Imp Combat Rewards
            if (combatUnitTwo == 5)
            {
                if (combatUnitOne == 1)
                {
                    goldPlayer1 += 500;
                }
                else if (combatUnitOne == 2)
                {
                    goldPlayer2 += 500;
                }
                else if (combatUnitOne == 3)
                {
                    goldPlayer3 += 500;
                }
                else if (combatUnitOne == 4)
                {
                    goldPlayer4 += 500;
                }
            }
            // Basilisk Combat Rewards
            if (combatUnitTwo == 6)
            {
                if (combatUnitOne == 1)
                {
                    goldPlayer1 += 500;
                }
                else if (combatUnitOne == 2)
                {
                    goldPlayer2 += 500;
                }
                else if (combatUnitOne == 3)
                {
                    goldPlayer3 += 500;
                }
                else if (combatUnitOne == 4)
                {
                    goldPlayer4 += 500;
                }
            }

            // Return combatUnitTwo to camp if it is a player
            if (combatUnitTwo == 1)
            {
                SetCurrentBoardSpace("campP1");
                tilemap.SetTile(new Vector3Int(xy[0], xy[1]), player);
                unitPositionPlayer1 = 28;
            }
            else if (combatUnitTwo == 2)
            {
                SetCurrentBoardSpace("campP2");
                tilemap.SetTile(new Vector3Int(xy[0], xy[1]), player);
                if (activePlayers == 2)
                {
                    unitPositionPlayer2 = 14;
                }
            }
        }
        else if (combatUnitOne_DiceTotal < combatUnitTwo_DiceTotal)
        {
            Debug.Log("Player " + combatUnitTwo + " has won!");
            if (combatUnitOne == 1)
            {
                SetCurrentBoardSpace("campP1");
                tilemap.SetTile(new Vector3Int(xy[0], xy[1]), player);
                SetCurrentBoardSpace("space" + currentUnitPosition);
                tilemap.SetTile(new Vector3Int(xy[0], xy[1]), null);
                unitPositionPlayer1 = 28;
                currentUnitPosition = 28;
            }
            else if (combatUnitOne == 2)
            {
                SetCurrentBoardSpace("campP2");
                tilemap.SetTile(new Vector3Int(xy[0], xy[1]), player);
                SetCurrentBoardSpace("space" + currentUnitPosition);
                tilemap.SetTile(new Vector3Int(xy[0], xy[1]), null);
                if (activePlayers == 2)
                {
                    unitPositionPlayer2 = 14;
                    currentUnitPosition = 14;
                }
            }
            // Set monster avatar on the board
            if (combatUnitTwo == 5)
            {
                SetCurrentBoardSpace("space" + currentUnitPosition);
                tilemap.SetTile(new Vector3Int(xy[0], xy[1]), monsterImp);
                Debug.Log(boardMonsters[currentUnitPosition]);
            }
            else if (combatUnitTwo == 6)
            {
                SetCurrentBoardSpace("space" + currentUnitPosition);
                tilemap.SetTile(new Vector3Int(xy[0], xy[1]), monsterBasilisk);
            }
        }
        else if (combatUnitOne_DiceTotal == combatUnitTwo_DiceTotal)
        {
            Debug.Log("Tied combat! SUDDEN DEATH!");
            int rand = Random.Range(1,3);
            if (rand == 1)
            {
                Debug.Log("Player " + currentPlayer + " has won!");
                if (combatUnitTwo == 1)
                {
                    SetCurrentBoardSpace("campP1");
                    tilemap.SetTile(new Vector3Int(xy[0], xy[1]), player);
                    unitPositionPlayer1 = 28;
                }
                else if (combatUnitTwo == 2)
                {
                    SetCurrentBoardSpace("campP2");
                    tilemap.SetTile(new Vector3Int(xy[0], xy[1]), player);
                    if (activePlayers == 2)
                    {
                        unitPositionPlayer2 = 14;
                    }
                }
            }
            else if (rand == 2)
            {
                Debug.Log("Player " + combatUnitTwo + " has won!");
                if (combatUnitOne == 1)
                {
                    SetCurrentBoardSpace("campP1");
                    tilemap.SetTile(new Vector3Int(xy[0], xy[1]), player);
                    SetCurrentBoardSpace("space" + currentUnitPosition);
                    tilemap.SetTile(new Vector3Int(xy[0], xy[1]), null);
                    unitPositionPlayer1 = 28;
                    currentUnitPosition = 28;
                }
                else if (combatUnitOne == 2)
                {
                    SetCurrentBoardSpace("campP2");
                    tilemap.SetTile(new Vector3Int(xy[0], xy[1]), player);
                    SetCurrentBoardSpace("space" + currentUnitPosition);
                    tilemap.SetTile(new Vector3Int(xy[0], xy[1]), null);
                    if (activePlayers == 2)
                    {
                        unitPositionPlayer2 = 14;
                        currentUnitPosition = 28;
                    }
                }
            }
        }
    }

    public static void ReturnToCamp(int unitSelected, Tilemap tilemap, Tile player)
    {
        if (unitSelected == 1)
        {
            SetCurrentBoardSpace("campP1");
            tilemap.SetTile(new Vector3Int(xy[0], xy[1]), player);
            unitPositionPlayer1 = campPositionPlayer1;
        }
        else if (unitSelected == 2)
        {
            SetCurrentBoardSpace("campP2");
            tilemap.SetTile(new Vector3Int(xy[0], xy[1]), player);
            unitPositionPlayer2 = campPositionPlayer2;
        }
        else if (unitSelected == 3)
        {
            SetCurrentBoardSpace("campP3");
            tilemap.SetTile(new Vector3Int(xy[0], xy[1]), player);
            unitPositionPlayer3 = campPositionPlayer3;
        }
        else if (unitSelected == 4)
        {
            SetCurrentBoardSpace("campP4");
            tilemap.SetTile(new Vector3Int(xy[0], xy[1]), player);
            unitPositionPlayer4 = campPositionPlayer4;
        }
    }

    public static void GenerateBoardTerrain(string tileName)
    {

    }

    public static void SetCurrentBoardSpace(string tileName)
    {
        if (tileName == "space1")
        {
            xy[0] = 6;
            xy[1] = -5;
        }
        else if (tileName == "space2")
        {
            xy[0] = 4;
            xy[1] = -5;
        }
        else if (tileName == "space3")
        {
            xy[0] = 2;
            xy[1] = -5;
        }
        else if (tileName == "space4")
        {
            xy[0] = 0;
            xy[1] = -5;
        }
        else if (tileName == "space5")
        {
            xy[0] = -2;
            xy[1] = -5;
        }
        else if (tileName == "space6")
        {
            xy[0] = -4;
            xy[1] = -5;
        }
        else if (tileName == "space7")
        {
            xy[0] = -6;
            xy[1] = -5;
        }
        else if (tileName == "space8")
        {
            xy[0] = -8;
            xy[1] = -5;
        }
        else if (tileName == "space9")
        {
            xy[0] = -10;
            xy[1] = -5;
        }
        else if (tileName == "space10")
        {
            xy[0] = -11;
            xy[1] = -3;
        }
        else if (tileName == "space11")
        {
            xy[0] = -11;
            xy[1] = -1;
        }
        else if (tileName == "space12")
        {
            xy[0] = -11;
            xy[1] = 1;
        }
        else if (tileName == "space13")
        {
            xy[0] = -11;
            xy[1] = 3;
        }
        else if (tileName == "space14" || (tileName == "campP2" && activePlayers == 2))
        {
            xy[0] = -10;
            xy[1] = 5;
        }
        else if (tileName == "space15")
        {
            xy[0] = -8;
            xy[1] = 5;
        }
        else if (tileName == "space16")
        {
            xy[0] = -6;
            xy[1] = 5;
        }
        else if (tileName == "space17")
        {
            xy[0] = -4;
            xy[1] = 5;
        }
        else if (tileName == "space18")
        {
            xy[0] = -2;
            xy[1] = 5;
        }
        else if (tileName == "space19")
        {
            xy[0] = 0;
            xy[1] = 5;
        }
        else if (tileName == "space20")
        {
            xy[0] = 2;
            xy[1] = 5;
        }
        else if (tileName == "space21")
        {
            xy[0] = 4;
            xy[1] = 5;
        }
        else if (tileName == "space22")
        {
            xy[0] = 6;
            xy[1] = 5;
        }
        else if (tileName == "space23")
        {
            xy[0] = 8;
            xy[1] = 5;
        }
        else if (tileName == "space24")
        {
            xy[0] = 9;
            xy[1] = 3;
        }
        else if (tileName == "space25")
        {
            xy[0] = 9;
            xy[1] = 1;
        }
        else if (tileName == "space26")
        {
            xy[0] = 9;
            xy[1] = -1;
        }
        else if (tileName == "space27")
        {
            xy[0] = 9;
            xy[1] = -3;
        }
        else if (tileName == "space28" || tileName == "campP1")
        {
            xy[0] = 8;
            xy[1] = -5;
        }
    }

    public static void SetCurrentBoardSpaceSlot(string tileName)
    {
        if (tileName == "spaceSlot1")
        {
            xy[0] = 6;
            xy[1] = -4;
        }
        else if (tileName == "spaceSlot2")
        {
            xy[0] = 4;
            xy[1] = -4;
        }
        else if (tileName == "spaceSlot3")
        {
            xy[0] = 2;
            xy[1] = -4;
        }
        else if (tileName == "spaceSlot4")
        {
            xy[0] = 0;
            xy[1] = -4;
        }
        else if (tileName == "spaceSlot5")
        {
            xy[0] = -2;
            xy[1] = -4;
        }
        else if (tileName == "spaceSlot6")
        {
            xy[0] = -4;
            xy[1] = -4;
        }
        else if (tileName == "spaceSlot7")
        {
            xy[0] = -6;
            xy[1] = -4;
        }
        else if (tileName == "spaceSlot8")
        {
            xy[0] = -8;
            xy[1] = -4;
        }
        else if (tileName == "spaceSlot9")
        {
            xy[0] = -10;
            xy[1] = -4;
        }
        else if (tileName == "spaceSlot10")
        {
            xy[0] = -10;
            xy[1] = -3;
        }
        else if (tileName == "spaceSlot11")
        {
            xy[0] = -10;
            xy[1] = -1;
        }
        else if (tileName == "spaceSlot12")
        {
            xy[0] = -10;
            xy[1] = 1;
        }
        else if (tileName == "spaceSlot13")
        {
            xy[0] = -10;
            xy[1] = 3;
        }
        else if (tileName == "spaceSlot14")
        {
            xy[0] = -10;
            xy[1] = 4;
        }
        else if (tileName == "spaceSlot15")
        {
            xy[0] = -8;
            xy[1] = 4;
        }
        else if (tileName == "spaceSlot16")
        {
            xy[0] = -6;
            xy[1] = 4;
        }
        else if (tileName == "spaceSlot17")
        {
            xy[0] = -4;
            xy[1] = 4;
        }
        else if (tileName == "spaceSlot18")
        {
            xy[0] = -2;
            xy[1] = 4;
        }
        else if (tileName == "spaceSlot19")
        {
            xy[0] = 0;
            xy[1] = 4;
        }
        else if (tileName == "spaceSlot20")
        {
            xy[0] = 2;
            xy[1] = 4;
        }
        else if (tileName == "spaceSlot21")
        {
            xy[0] = 4;
            xy[1] = 4;
        }
        else if (tileName == "spaceSlot22")
        {
            xy[0] = 6;
            xy[1] = 4;
        }
        else if (tileName == "spaceSlot23")
        {
            xy[0] = 8;
            xy[1] = 4;
        }
        else if (tileName == "spaceSlot24")
        {
            xy[0] = 8;
            xy[1] = 3;
        }
        else if (tileName == "spaceSlot25")
        {
            xy[0] = 8;
            xy[1] = 1;
        }
        else if (tileName == "spaceSlot26")
        {
            xy[0] = 8;
            xy[1] = -1;
        }
        else if (tileName == "spaceSlot27")
        {
            xy[0] = 8;
            xy[1] = -3;
        }
        else if (tileName == "spaceSlot28")
        {
            xy[0] = 8;
            xy[1] = -4;
        }
    }
}