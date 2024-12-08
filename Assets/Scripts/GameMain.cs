using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class GameMain : MonoBehaviour
{
    // Game Settings //
    public static string currentBoard = "grasslands";
    public static string mapSize = "medium";
    public static bool mapSizeRandom = false;
    public static int activePlayers = 4;
    public static int startingGold = 100;
    public static int villageCost = 50;
    public static int dungeonCap = 10;
    public static int playerLives = 3;
    public static int deathCost = 250;
    public static int dungeonSpawnRate = 25;
    // Current Turn, Current Player Info //
    public static int currentPlayer = 1;
    public static int currentTurn = 0;
   // Determines which screen (and content) is displayed //
    public static bool GUIEnabled = true;
    public static bool villageScreenEnabled = false;
    public static bool chestScreenEnabled = false;
    public static bool campScreenEnabled = false;
    public static bool dungeonScreenEnabled = false;
    public static bool opposingPlayerScreenEnabled = false;
    public static bool settingsScreenEnabled = false;
    public static bool combatScreenEnabled = false;
    public static bool opposingVillageEncounterHappening = false;
    public static bool opposingVillageEncounterCannotPay = false;
    [SerializeField] public TMP_Text centerDisplayText;
    public static string centerDisplayTextContent;
    [SerializeField] public TMP_Text currentTurnText;
    public static bool playerIsMoving = false;
    public static bool playerIsFinishedMoving = false;
    public static bool playerIsMovingInReverse = false;
    public static bool playerRecentlyDied = false;
    // Dice //
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
    public static int diceOneResult = 0;
    public static int diceTwoResult = 0;
    public static int diceThreeResult = 0;
    // Player info //
    public static int player_gold_one = startingGold;
    public static int player_gold_two = startingGold;
    public static int player_gold_three = startingGold;
    public static int player_gold_four = startingGold;
    public static int player_combatDice_one = 2;
    public static int player_combatDice_two = 2;
    public static int player_combatDice_three = 2;
    public static int player_combatDice_four = 2;
    public static int player_moveDice_one = 1;
    public static int player_moveDice_two = 1;
    public static int player_moveDice_three = 1;
    public static int player_moveDice_four = 1;
    public static int livesPlayerOne = playerLives;
    public static int livesPlayerTwo = playerLives;
    public static int livesPlayerThree = playerLives;
    public static int livesPlayerFour = playerLives;
    public static bool player_alive_one = true;
    public static bool player_alive_two = true;
    public static bool player_alive_three = true;
    public static bool player_alive_four = true;
    public static bool player_in_camp_one = true;
    public static bool player_in_camp_two = true;
    public static bool player_in_camp_three = true;
    public static bool player_in_camp_four = true;
    public static string playerOneColor = "red";
    public static string playerTwoColor = "blue";
    public static string playerThreeColor = "green";
    public static string playerFourColor = "purple";
    // Current move info //
    public static int currentPlayerDice = 0;
    // Player position tracking //
    public static bool playerOneIsActive;
    public static bool playerTwoIsActive;
    public static bool playerThreeIsActive;
    public static bool playerFourIsActive;
    public static int unitPositionPlayer1;
    public static int unitPositionPlayer2;
    public static int unitPositionPlayer3;
    public static int unitPositionPlayer4;
    public static int campPositionPlayer1 = 0;
    public static int campPositionPlayer2 = 0;
    public static int campPositionPlayer3 = 0;
    public static int campPositionPlayer4 = 0;
    public static int[] xy = new int[2];
    // Buttons //
    public static bool endTurnButtonEnabled = false;
    public static bool bottomLeftLowerButtonEnabled = true;
    public static bool secondaryButtonEnabled = false;
    public static bool rightArrowButtonEnabled = false;
    public static bool leftArrowButtonEnabled = false;
    public static bool upArrowButtonEnabled = true;
    public static bool downArrowButtonEnabled = true;
    public GameObject rightArrowButton;
    public GameObject upArrowButton;
    // Board tracking //
    public static List<Vector3> boardPositions = new List<Vector3>();
    public static List<Vector3> boardSlotPositions = new List<Vector3>();
    public static Dictionary<int, string> boardStructures = new Dictionary<int, string>();
    public static Dictionary<int, string> boardUnits = new Dictionary<int, string>();
    public static int boardLength;
    public static Vector3 boardPosition;
    public static Vector3 onDeckPosition;
    // Tiles and tilemaps //
    [SerializeField] public Tile player;
    [SerializeField] public Tile player_red;
    [SerializeField] public Tile player_blue;
    [SerializeField] public Tile player_green;
    [SerializeField] public Tile player_purple;
    [SerializeField] public Tile player_white;
    [SerializeField] public Tile playerGreen;
    [SerializeField] public Tile playerBlue;
    [SerializeField] public Tile monsterImp;
    [SerializeField] public Tile monsterBasilisk;
    [SerializeField] public Tile monsterRampagingElephant;
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
    // Combat //
    public static string currentEnemy = "";
    public static string dungeonType = "";
    public static bool combatEncounterHappening = false;
    // Monsters, Dungeons //
    public static Dictionary<int, string> boardMonsters = new Dictionary<int, string>();
    public static int dungeonCount = 0;
    public static bool spawnRampagingElephant = false;
    // Time tracking //
    private float counter = 0.5f;
    private float counter2 = 2f;
    private float tempCounter = 0f;
    private float tempCounter2 = 0f;
    // Other //
    public World world;
    public GUI gui;

    void Start()
    {
        activePlayers = 4;
        currentTurn = 1;
        currentPlayer = 1;
        playerOneColor = "red";
        World.currentPlayerColor = playerOneColor;
        world.SpawnActivePlayerAtCamp();
        GUI.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && bottomLeftLowerButtonEnabled)
        {
            World.MoveUnit();
            gui.EnableArrows(true);
        }
        if (Input.GetKeyDown(KeyCode.E) && endTurnButtonEnabled)
        {
            EndTurn(tilemapStructures, monsterImp, monsterBasilisk, world);
        }
        if (Input.GetKeyDown(KeyCode.V) && secondaryButtonEnabled)
        {
            Villages.BuildVillage(tilemapStructures, villageRed, villageBlue, villageGreen,villagePurple, villageWhite);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && rightArrowButtonEnabled)
        {
            MoveUnitRight(tilemapStructures);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && upArrowButtonEnabled)
        {
            MoveUnitUp(tilemapStructures);
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
                Combat.CombatEncounter(tilemapStructures, player_red, player_blue, player_green, player_purple, player_white, monsterImp, monsterBasilisk);
            }
        }
        else if (villageScreenEnabled)
        {
            GUI.SetActive(false);
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
        else if (opposingVillageEncounterHappening)
        {
            if (tempCounter2 >= counter2)
            {
                opposingVillageEncounterHappening = false;
                opposingVillageEncounterCannotPay = false;
                centerDisplayText.text = "";
            }
            else
            {
                if (!opposingVillageEncounterCannotPay)
                {
                    centerDisplayText.text = "Opposing Village Encountered";
                }
                else if (opposingVillageEncounterCannotPay)
                {
                    centerDisplayText.text = "Cannot Afford To Pay!";   
                }
                tempCounter2 += Time.deltaTime;
            }
        }
        else if (GUIEnabled)
        {
            GUI.SetActive(true);
            currentTurnText.text = "turn #" + currentTurn;
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
        if (playerRecentlyDied)
        {
            // RED PLAYER has been eliminated from the game
            playerRecentlyDied = false;
        }
    }

    void SpawnDungeons()
    {
        for (int i = 0; i < World.boardSlotPositions.Count; i++)
        {
            Debug.Log("pass");
            // World.boardStructures.Add(i, "dungeon" + dungeonType);
            World.boardPosition = World.boardSlotPositions[i];
            Debug.Log(World.boardSlotPositions[i]);
            tilemapStructures.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), dungeon);
        }
    }

    public static void EndTurn(Tilemap tilemap, Tile monsterImp, Tile monsterBasilisk, World world)
    {
        endTurnButtonEnabled = false;
        currentPlayer += 1;
        if (currentPlayer > activePlayers)
        {
            if (playerOneIsActive)
            {
                currentPlayer = 1;
            }
            else if (playerTwoIsActive)
            {
                currentPlayer = 2;
            }
            else if (playerThreeIsActive)
            {
                currentPlayer = 3;
            }
            else if (playerFourIsActive)
            {
                currentPlayer = 4;
            }
            currentTurn += 1;
        }
        if (currentTurn == 1)
        {
            switch (currentPlayer)
            {
                case 2: playerTwoIsActive = true; break;
                case 3: playerThreeIsActive = true; break;
                case 4: playerFourIsActive = true; break;
            }
            world.SpawnActivePlayerAtCamp();
        }
        if (currentPlayer == 1)
        {
            currentPlayerDice = player_combatDice_one;
            World.currentPlayerColor = playerOneColor;
            World.currentUnitPosition = World.playerOnePosition;
        }
        else if (currentPlayer == 2)
        {
            currentPlayerDice = player_combatDice_two;
            World.currentPlayerColor = playerTwoColor;
            World.currentUnitPosition = World.playerTwoPosition;
        }
        else if (currentPlayer == 3)
        {
            currentPlayerDice = player_combatDice_three;
            World.currentPlayerColor = playerThreeColor;
            World.currentUnitPosition = World.playerThreePosition;
        }
        else if (currentPlayer == 4)
        {
            currentPlayerDice = player_combatDice_four;
            World.currentPlayerColor = playerFourColor;
            World.currentUnitPosition = World.playerFourPosition;
        }
        /*if (currentTurn > 1 && currentPlayer <= activePlayers)
        {
            if (currentPlayer == 2 && !playerTwoIsActive)
            {
                currentPlayer += 1;
            }
            if (currentPlayer == 3 && !playerThreeIsActive)
            {
                currentPlayer += 1;
            }
            if (currentPlayer == 4 && !playerFourIsActive)
            {
                currentPlayer = +1;
            }
            if (currentPlayer == 1 && !playerOneIsActive)
            {
                currentPlayer += 1;
            }
        }
            Spawn monsters
                if (boardStructures[i] == "dungeonImp" && unitPositionClear)
                {
                    int random = Random.Range(1,5);
                    if (random == 1)
                    {
                        if (boardMonsters[i] == "empty")
                        {
                            boardMonsters[i] = "imp";
                            boardPosition = boardPositions[i];
                            tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), monsterImp);
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
                            boardPosition = boardPositions[i];
                            tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), monsterBasilisk);
                        }
                    }
                }
            }
            if (currentTurn >= 5)
            {
                int chanceToSpawn = Random.Range(1,11);
                if (chanceToSpawn <= 3)
                {
                    spawnRampagingElephant = true;
                }
            }*/
        bottomLeftLowerButtonEnabled = true;
        secondaryButtonEnabled = false;
        World.CheckForLocalBoardPositions();
    }

    public static void RollDice()
    {
        int moveDiceAmount = 1;
        diceOneResult = 0;
        diceTwoResult = 0;
        diceThreeResult = 0;
        if (moveDiceAmount > 0)
        {
            diceOneResult = Random.Range(1,7);
        }
        else if (moveDiceAmount > 1)
        {
            diceTwoResult = Random.Range(1,7);
        }
        else if (moveDiceAmount > 2)
        {
            diceThreeResult = Random.Range(1,7);
        }
        // UPDATE THIS SO THAT IT CAN SHOW UP TO THREE DICE AT A TIME
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
        ///////////////////////////////////////////////////////////////
    }

    public static void MoveUnitRight(Tilemap tilemap)
    {
        if (World.currentUnitPositionOnBoard == 0)
        {
            World.currentUnitPositionOnBoard = boardLength;
            playerIsMovingInReverse = true;
            tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), null);
            World.newUnitPosition = World.currentUnitPositionOnBoard - diceOneResult - diceTwoResult - diceThreeResult;
        }
        if (World.newUnitPosition < 0)
        {
            int nextPosition = boardLength - World.newUnitPosition;
            World.newUnitPosition = nextPosition;
        }
        playerIsMoving = true;
    }

    public static void MoveUnitUp(Tilemap tilemap)
    {
        if (World.currentUnitPositionOnBoard == 0)
        {
            tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), null);
        }
        playerIsMoving = true;
    }

    public static void MoveUnitComplete(Tilemap tilemap, Tile player_red, Tile player_blue, Tile player_green, Tile player_purple, Tile player_white)
    {
        World.currentUnitPositionOnBoard = World.newUnitPosition;
        boardPosition = boardPositions[World.currentUnitPositionOnBoard];
        if (currentPlayer == 1)
        {
            switch (playerOneColor)
            {
                case "red": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white); break;
            }
        }
        if (currentPlayer == 2)
        {
            switch (playerTwoColor)
            {
                case "red": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white); break;
            }
        }
        if (currentPlayer == 3)
        {
            switch (playerThreeColor)
            {
                case "red": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white); break;
            }
        }
        if (currentPlayer == 4)
        {
            switch (playerFourColor)
            {
                case "red": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white); break;
            }
        }
        // Landing Encounters
        if (boardStructures[World.newUnitPosition] == "chest")
        {
            chestScreenEnabled = true;
            diceShouldFadeAwayImmediately = true;
            boardPosition = boardSlotPositions[World.currentUnitPositionOnBoard];
            tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), null);
            boardStructures[World.newUnitPosition] = "empty";
        }
        else if (boardStructures[World.newUnitPosition] == "dungeonImp")
        {
            diceShouldFadeAwayImmediately = true;
            DungeonEncounter("imp");
        }
        else if (boardStructures[World.newUnitPosition] == "dungeonBasilisk")
        {
            diceShouldFadeAwayImmediately = true;
            DungeonEncounter("basilisk");
        }
        else if (boardStructures[World.newUnitPosition] == "oddity")
        {
            OddityEncounter();
        }
        else if (boardStructures[World.newUnitPosition] == "village_player_one" && currentPlayer != 1)
        {
            OpposingVillageEncounter(1, tilemap, player_red, player_blue, player_green, player_purple, player_white);
        }
        else if (boardStructures[World.newUnitPosition] == "village_player_two" && currentPlayer != 2)
        {
            OpposingVillageEncounter(2, tilemap, player_red, player_blue, player_green, player_purple, player_white);
        }
        else if (boardStructures[World.newUnitPosition] == "village_player_three" && currentPlayer != 3)
        {
            OpposingVillageEncounter(3, tilemap, player_red, player_blue, player_green, player_purple, player_white);
        }
        else if (boardStructures[World.newUnitPosition] == "village_player_four" && currentPlayer != 4)
        {
            OpposingVillageEncounter(4, tilemap, player_red, player_blue, player_green, player_purple, player_white);
        }
        else if (boardStructures[World.newUnitPosition] == "empty")
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
        combatScreenEnabled = true;
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

    public static void OpposingVillageEncounter(int villageOwner, Tilemap tilemap, Tile player_red, Tile player_blue, Tile player_green, Tile player_purple, Tile player_white)
    {
        opposingVillageEncounterHappening = true;
        if (currentPlayer == 1 && player_gold_one >= villageCost)
        {
            player_gold_one -= villageCost;
        }
        else if (currentPlayer == 1 && player_gold_one < villageCost)
        {
            opposingVillageEncounterCannotPay = true;
            player_in_camp_one = true;
            switch (playerOneColor)
            {
                case "red": tilemap.SetTile(new Vector3Int(0, 1), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int(0, 1), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int(0, 1), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int(0, 1), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int(0, 1), player_white); break;
            }
            boardPosition = boardPositions[World.currentUnitPositionOnBoard];
            tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), null);
            unitPositionPlayer1 = 0;
            World.currentUnitPositionOnBoard = 0;
            player_gold_one -= villageCost;
            livesPlayerOne -= 1;
            if (livesPlayerOne <= 0)
            {
                player_alive_one = false;
                playerRecentlyDied = true;
                activePlayers -= 1;
                tilemap.SetTile(new Vector3Int(0, 1), null);
            }
            if (player_gold_one < 0)
            {
                player_gold_one = 0;
            }
        }
        else if (currentPlayer == 2 && player_gold_two >= villageCost)
        {
            player_gold_two -= villageCost;
        }
        else if (currentPlayer == 2 && player_gold_two < villageCost)
        {
            opposingVillageEncounterCannotPay = true;
            player_in_camp_two = true;
            switch (playerTwoColor)
            {
                case "red": tilemap.SetTile(new Vector3Int(1, 0), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int(1, 0), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int(1, 0), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int(1, 0), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int(1, 0), player_white); break;
            }
            boardPosition = boardPositions[World.currentUnitPositionOnBoard];
            tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), null);
            unitPositionPlayer2 = 0;
            World.currentUnitPositionOnBoard = 0;
            player_gold_two -= villageCost;
            livesPlayerTwo -= 1;
            if (livesPlayerTwo <= 0)
            {
                player_alive_two = false;
                playerRecentlyDied = true;
                activePlayers -= 1;
                tilemap.SetTile(new Vector3Int(1, 0), null);
            }
            if (player_gold_two < 0)
            {
                player_gold_two = 0;
            }
        }
        else if (currentPlayer == 3 && player_gold_three >= villageCost)
        {
            player_gold_three -= villageCost;
        }
        else if (currentPlayer == 3 && player_gold_three < villageCost)
        {
            opposingVillageEncounterCannotPay = true;
            player_in_camp_three = true;
            switch (playerThreeColor)
            {
                case "red": tilemap.SetTile(new Vector3Int(0, -1), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int(0, -1), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int(0, -1), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int(0, -1), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int(0, -1), player_white); break;
            }
            boardPosition = boardPositions[World.currentUnitPositionOnBoard];
            tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), null);
            unitPositionPlayer3 = 0;
            World.currentUnitPositionOnBoard = 0;
            player_gold_three -= villageCost;
            livesPlayerThree -= 1;
            if (livesPlayerThree <= 0)
            {
                player_alive_three = false;
                playerRecentlyDied = true;
                activePlayers -= 1;
                tilemap.SetTile(new Vector3Int(0, -1), null);
            }
            if (player_gold_three < 0)
            {
                player_gold_three = 0;
            }
        }
        else if (currentPlayer == 4 && player_gold_four >= villageCost)
        {
            player_gold_four -= villageCost;
        }
        else if (currentPlayer == 4 && player_gold_four < villageCost)
        {
            opposingVillageEncounterCannotPay = true;
            player_in_camp_four = true;
            switch (playerFourColor)
            {
                case "red": tilemap.SetTile(new Vector3Int(-1, 0), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int(-1, 0), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int(-1, 0), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int(-1, 0), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int(-1, 0), player_white); break;
            }
            boardPosition = boardPositions[World.currentUnitPositionOnBoard];
            tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), null);
            unitPositionPlayer4 = 0;
            World.currentUnitPositionOnBoard = 0;
            player_gold_four -= villageCost;
            livesPlayerFour -= 1;
            if (livesPlayerFour <= 0)
            {
                player_alive_four = false;
                playerRecentlyDied = true;
                activePlayers -= 1;
                tilemap.SetTile(new Vector3Int(-1, 0), null);
            }
            if (player_gold_four < 0)
            {
                player_gold_four = 0;
            }
        }
        if (villageOwner == 1)
        {
            player_gold_one += villageCost;
        }
        else if (villageOwner == 2)
        {
            player_gold_two += villageCost;
        }
        else if (villageOwner == 3)
        {
            player_gold_three += villageCost;
        }
        else if (villageOwner == 4)
        {
            player_gold_four += villageCost;
        }
    }
}