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
    public static int player_combatDice_one = 1;
    public static int player_combatDice_two = 1;
    public static int player_combatDice_three = 1;
    public static int player_combatDice_four = 1;
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
    public static string player_color_one = "red";
    public static string player_color_two = "blue";
    public static string player_color_three = "green";
    public static string player_color_four = "purple";
    // Current move info //
    public static int currentUnitPosition = 0;
    public static int newUnitPosition = 0;
    public static int currentPlayerDice = 0;
    public static int currentAvatar = 0;
    // Player position tracking //
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
    [SerializeField] public Tile village;
    [SerializeField] public Tile village_red;
    [SerializeField] public Tile village_blue;
    [SerializeField] public Tile village_green;
    [SerializeField] public Tile village_purple;
    [SerializeField] public Tile village_white;
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
    // Monsters, Dungeons //
    public static Dictionary<int, string> boardMonsters = new Dictionary<int, string>();
    public static int dungeonCount = 0;
    public static bool spawnRampagingElephant = false;
    // Time tracking //
    private float counter = 0.5f;
    private float counter2 = 2f;
    private float tempCounter = 0f;
    private float tempCounter2 = 0f;

    void Start()
    {
        GameSetup(currentBoard, activePlayers);
        GUI.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && bottomLeftLowerButtonEnabled)
        {
            MoveUnit(tilemapStructures, player, player_red, player_blue, player_green, player_purple, player_white);
        }
        if (Input.GetKeyDown(KeyCode.E) && endTurnButtonEnabled)
        {
            EndTurn(tilemapStructures, monsterImp, monsterBasilisk);
        }
        if (Input.GetKeyDown(KeyCode.V) && secondaryButtonEnabled)
        {
            BuildVillage(tilemapStructures, village, village_red, village_blue, village_green, village_purple, village_white);
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
                CombatEncounter(tilemapStructures, player, player_red, player_blue, player_green, player_purple, player_white, monsterImp, monsterBasilisk);
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
                    boardPosition = boardPositions[currentUnitPosition];
                    tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), null);
                }
                else if (currentAvatar == 1)
                {
                    boardPosition = boardPositions[currentUnitPosition];
                    if (player_color_one == "red")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red);
                    }
                    else if (player_color_one == "blue")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue);
                    }
                    else if (player_color_one == "green")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green);
                    }
                    else if (player_color_one == "purple")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple);
                    }
                    else if (player_color_one == "white")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white);
                    }              
                }
                else if (currentAvatar == 2)
                {
                    boardPosition = boardPositions[currentUnitPosition];
                    if (player_color_two == "red")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red);
                    }
                    else if (player_color_two == "blue")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue);
                    }
                    else if (player_color_two == "green")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green);
                    }
                    else if (player_color_two == "purple")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple);
                    }
                    else if (player_color_two == "white")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white);
                    }
                }
                else if (currentAvatar == 3)
                {
                    boardPosition = boardPositions[currentUnitPosition];
                    if (player_color_three == "red")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red);
                    }
                    else if (player_color_three == "blue")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue);
                    }
                    else if (player_color_three == "green")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green);
                    }
                    else if (player_color_three == "purple")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple);
                    }
                    else if (player_color_three == "white")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white);
                    }
                }
                else if (currentAvatar == 4)
                {
                    boardPosition = boardPositions[currentUnitPosition];
                    if (player_color_four == "red")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red);
                    }
                    else if (player_color_four == "blue")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue);
                    }
                    else if (player_color_four == "green")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green);
                    }
                    else if (player_color_four == "purple")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple);
                    }
                    else if (player_color_four == "white")
                    {
                        tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white);
                    }
                }
                else if (currentAvatar == 5)
                {
                    boardPosition = boardPositions[currentUnitPosition];
                    tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), monsterImp);
                }
                else if (currentAvatar == 6)
                {
                    boardPosition = boardPositions[currentUnitPosition];
                    tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), monsterBasilisk);
                }
                if (!playerIsMovingInReverse)
                {
                    currentUnitPosition += 1;
                    // Loop around the board
                    if (currentUnitPosition > (boardLength - 1))
                    {
                        currentUnitPosition = 0;
                    }
                }
                if (playerIsMovingInReverse)
                {
                    currentUnitPosition -= 1;
                    // Loop around the board
                    if (currentUnitPosition == 0)
                    {
                        currentUnitPosition = 1;
                    }
                }
/*                // Add 200 gold if the unit passes camp
                if (currentPlayer == 1 && currentUnitPosition == 0)
                {
                    player_gold_one += 200;
                }
                else if (currentPlayer == 2 && currentUnitPosition == 0)
                {
                    player_gold_two += 200;
                }
                else if (currentPlayer == 3 && currentUnitPosition == 0)
                {
                    player_gold_three += 200;
                }
                else if (currentPlayer == 4 && currentUnitPosition == 0)
                {
                    player_gold_four += 200;
                }*/
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
                boardPosition = boardPositions[currentUnitPosition];
                if (currentPlayer == 1 && currentUnitPosition != newUnitPosition)
                {
                    switch (player_color_one)
                    {
                        case "red": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red); break;
                        case "blue": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue); break;
                        case "green": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green); break;
                        case "purple": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple); break;
                        case "white": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white); break;
                        default: tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player); break;
                    }          
                }
                else if (currentPlayer == 2 && currentUnitPosition != newUnitPosition)
                {
                    switch (player_color_two)
                    {
                        case "red": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red); break;
                        case "blue": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue); break;
                        case "green": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green); break;
                        case "purple": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple); break;
                        case "white": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white); break;
                        default: tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player); break;
                    }  
                }
                else if (currentPlayer == 3 && currentUnitPosition != newUnitPosition)
                {
                    switch (player_color_three)
                    {
                        case "red": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red); break;
                        case "blue": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue); break;
                        case "green": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green); break;
                        case "purple": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple); break;
                        case "white": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white); break;
                        default: tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player); break;
                    }  
                }
                else if (currentPlayer == 4 && currentUnitPosition != newUnitPosition)
                {
                    switch (player_color_four)
                    {
                        case "red": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red); break;
                        case "blue": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue); break;
                        case "green": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green); break;
                        case "purple": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple); break;
                        case "white": tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white); break;
                        default: tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player); break;
                    }  
                }
                if (currentUnitPosition == newUnitPosition)
                {
                    playerIsMoving = false;
                    playerIsMovingInReverse = false;
                    if (currentAvatar == 0)
                    {
                        MoveUnitComplete(tilemapStructures, player, player_red, player_blue, player_green, player_purple, player_white);
                    }
                    else
                    {
                        CombatEncounter(tilemapStructures, player, player_red, player_blue, player_green, player_purple, player_white, monsterImp, monsterBasilisk);
                        // MoveUnitComplete(tilemapStructures, player, player_red, player_blue, player_green, player_purple, player_white);
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
        else if (spawnRampagingElephant == true)
        {
            // Move the camera to the elite spawn spot //
            GUI.SetActive(false);
            for (int i = 0; i < boardLength; i++)
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
                if ((boardStructures[i] == "dungeonImp" || boardStructures[i] == "dungeonBasilisk") && unitPositionClear && boardMonsters[i] == "empty")
                {
                    // unitOnPosition[i] = "rampagingElephant";
                    boardMonsters[i] = "rampagingElephant";
                    boardPosition = boardPositions[i];
                    tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), monsterRampagingElephant);
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
                            tilemapStructures.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), monsterBasilisk);
                        }
                    }
                }
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

    void GameSetup(string currentBoard, int activePlayers)
    {
        if (currentBoard == "oddMode")
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
            boardPositions.Add(new Vector3(0, 1));
            boardSlotPositions.Add(new Vector3(1, 1));
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 2), bcVertical);
            onDeckPosition = new Vector3(0,2);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 3), bcThreeRight);
            tilemapBoardConnectors.SetTile(new Vector3Int(0, 4), bcVertical);
            /*boardLength += 1;
            boardPositions.Add(new Vector3(0, 4));
            boardSlotPositions.Add(new Vector3(1, 4));*/
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
                boardSlotPositions.Add(new Vector3((1 + i), rowLength + 5));
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
                boardSlotPositions.Add(new Vector3(rowLength + 2, (4 + i)));
                boardLength += 1;
            }
            tilemapBoardConnectors.SetTile(new Vector3Int(rowLength + 3, rowLength + 5), bcVertical);
            /*boardLength += 1;
            boardPositions.Add(new Vector3(rowLength + 3, rowLength + 5));
            boardSlotPositions.Add(new Vector3(rowLength + 2, rowLength + 5));*/
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
            }
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
        else if (currentBoard == "elemental")
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
        // Determine next player //
        currentPlayer += 1;
        if (currentPlayer <= activePlayers)
        {
            if (currentPlayer == 2 && !player_alive_two)
            {
                currentPlayer += 1;
            }
            if (currentPlayer == 3 && !player_alive_three)
            {
                currentPlayer += 1;
            }
            if (currentPlayer == 4 && !player_alive_four)
            {
                currentPlayer = 1;
            }
            if (currentPlayer == 1 && !player_alive_one)
            {
                currentPlayer += 1;
            }
        }
        if (currentPlayer > activePlayers)
        {
            if (player_alive_one)
            {
                currentPlayer = 1;
            }
            else if (player_alive_two)
            {
                currentPlayer = 2;
            }
            else if (player_alive_three)
            {
                currentPlayer = 3;
            }
            else if (player_alive_four)
            {
                currentPlayer = 4;
            }
            currentTurn += 1;
            // Spawn monsters
            for (int i = 0; i < boardLength; i++)
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
            }
        }
        // Update current player values to match the current player
        if (currentPlayer == 1)
        {
            currentUnitPosition = unitPositionPlayer1;
            currentPlayerDice = player_combatDice_one;
        }
        else if (currentPlayer == 2)
        {
            currentUnitPosition = unitPositionPlayer2;
            currentPlayerDice = player_combatDice_two;
        }
        else if (currentPlayer == 3)
        {
            currentUnitPosition = unitPositionPlayer3;
            currentPlayerDice = player_combatDice_three;
        }
        else if (currentPlayer == 4)
        {
            currentUnitPosition = unitPositionPlayer4;
            currentPlayerDice = player_combatDice_four;
        }
    }

    public static void MoveUnit(Tilemap tilemap, Tile player, Tile player_red, Tile player_blue, Tile player_green, Tile player_purple, Tile player_white)
    {
        // Determine new unit position
        bottomLeftLowerButtonEnabled = false;
        int moveDiceAmount = 1;
        playerIsMoving = true;
        if (currentPlayer == 1)
        {
            moveDiceAmount = player_moveDice_one;
            if (player_in_camp_one)
            {
                Debug.Log("playerIsMoving: " + playerIsMoving);
                playerIsMoving = false;
                player_in_camp_one = false;
                tilemap.SetTile(new Vector3Int(0, 1), null);
                switch (player_color_one)
                {
                    case "red": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_white); break;
                    default: tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player); break;
                }
                rightArrowButtonEnabled = true;
                upArrowButtonEnabled = true;
            }
        }
        else if (currentPlayer == 2)
        {
            moveDiceAmount = player_moveDice_two;
            if (player_in_camp_two)
            {
                playerIsMoving = false;
                player_in_camp_two = false;
                tilemap.SetTile(new Vector3Int(1, 0), null);
                switch (player_color_two)
                {
                    case "red": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_white); break;
                    default: tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player); break;
                }
                rightArrowButtonEnabled = true;
                upArrowButtonEnabled = true;
            }
        }
        else if (currentPlayer == 3)
        {
            moveDiceAmount = player_moveDice_three;
            if (player_in_camp_three)
            {
                playerIsMoving = false;
                player_in_camp_three = false;
                tilemap.SetTile(new Vector3Int(0, -1), null);
                switch (player_color_three)
                {
                    case "red": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_white); break;
                    default: tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player); break;
                }
                rightArrowButtonEnabled = true;
                upArrowButtonEnabled = true;
            }
        }
        else if (currentPlayer == 4)
        {
            moveDiceAmount = player_moveDice_four;
            if (player_in_camp_four)
            {
                playerIsMoving = false;
                player_in_camp_four = false;
                tilemap.SetTile(new Vector3Int(-1, 0), null);
                switch (player_color_four)
                {
                    case "red": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player_white); break;
                    default: tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), player); break;
                }
                rightArrowButtonEnabled = true;
                upArrowButtonEnabled = true;
            }
        }
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
        newUnitPosition = currentUnitPosition + diceOneResult + diceTwoResult + diceThreeResult;
    }

    public static void MoveUnitRight(Tilemap tilemap)
    {
        if (currentUnitPosition == 0)
        {
            currentUnitPosition = boardLength;
            playerIsMovingInReverse = true;
            tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), null);
            newUnitPosition = currentUnitPosition - diceOneResult - diceTwoResult - diceThreeResult;
        }
        if (newUnitPosition < 0)
        {
            int nextPosition = boardLength - newUnitPosition;
            newUnitPosition = nextPosition;
        }
        playerIsMoving = true;
    }

    public static void MoveUnitUp(Tilemap tilemap)
    {
        if (currentUnitPosition == 0)
        {
            tilemap.SetTile(new Vector3Int((int)onDeckPosition[0], (int)onDeckPosition[1]), null);
        }
        playerIsMoving = true;
    }

    public static void MoveUnitComplete(Tilemap tilemap, Tile player, Tile player_red, Tile player_blue, Tile player_green, Tile player_purple, Tile player_white)
    {
        currentUnitPosition = newUnitPosition;
        boardPosition = boardPositions[currentUnitPosition];
        if (currentPlayer == 1)
        {
            switch (player_color_one)
            {
                case "red": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white); break;
                default: tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player); break;
            }
        }
        if (currentPlayer == 2)
        {
            switch (player_color_two)
            {
                case "red": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white); break;
                default: tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player); break;
            }
        }
        if (currentPlayer == 3)
        {
            switch (player_color_three)
            {
                case "red": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white); break;
                default: tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player); break;
            }
        }
        if (currentPlayer == 4)
        {
            switch (player_color_four)
            {
                case "red": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player_white); break;
                default: tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), player); break;
            }
        }
        // Landing Encounters
        if (boardStructures[newUnitPosition] == "chest")
        {
            chestScreenEnabled = true;
            diceShouldFadeAwayImmediately = true;
            boardPosition = boardSlotPositions[currentUnitPosition];
            tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), null);
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
        else if (boardStructures[newUnitPosition] == "village_player_one" && currentPlayer != 1)
        {
            OpposingVillageEncounter(1, tilemap, player, player_red, player_blue, player_green, player_purple, player_white);
        }
        else if (boardStructures[newUnitPosition] == "village_player_two" && currentPlayer != 2)
        {
            OpposingVillageEncounter(2, tilemap, player, player_red, player_blue, player_green, player_purple, player_white);
        }
        else if (boardStructures[newUnitPosition] == "village_player_three" && currentPlayer != 3)
        {
            OpposingVillageEncounter(3, tilemap, player, player_red, player_blue, player_green, player_purple, player_white);
        }
        else if (boardStructures[newUnitPosition] == "village_player_four" && currentPlayer != 4)
        {
            OpposingVillageEncounter(4, tilemap, player, player_red, player_blue, player_green, player_purple, player_white);
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

    public static void OpposingVillageEncounter(int villageOwner, Tilemap tilemap, Tile player, Tile player_red, Tile player_blue, Tile player_green, Tile player_purple, Tile player_white)
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
            switch (player_color_one)
            {
                case "red": tilemap.SetTile(new Vector3Int(0, 1), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int(0, 1), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int(0, 1), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int(0, 1), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int(0, 1), player_white); break;
                default: tilemap.SetTile(new Vector3Int(0, 1), player); break;
            }
            boardPosition = boardPositions[currentUnitPosition];
            tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), null);
            unitPositionPlayer1 = 0;
            currentUnitPosition = 0;
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
            switch (player_color_two)
            {
                case "red": tilemap.SetTile(new Vector3Int(1, 0), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int(1, 0), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int(1, 0), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int(1, 0), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int(1, 0), player_white); break;
                default: tilemap.SetTile(new Vector3Int(1, 0), player); break;
            }
            boardPosition = boardPositions[currentUnitPosition];
            tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), null);
            unitPositionPlayer2 = 0;
            currentUnitPosition = 0;
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
            switch (player_color_three)
            {
                case "red": tilemap.SetTile(new Vector3Int(0, -1), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int(0, -1), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int(0, -1), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int(0, -1), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int(0, -1), player_white); break;
                default: tilemap.SetTile(new Vector3Int(0, -1), player); break;
            }
            boardPosition = boardPositions[currentUnitPosition];
            tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), null);
            unitPositionPlayer3 = 0;
            currentUnitPosition = 0;
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
            switch (player_color_four)
            {
                case "red": tilemap.SetTile(new Vector3Int(-1, 0), player_red); break;
                case "blue": tilemap.SetTile(new Vector3Int(-1, 0), player_blue); break;
                case "green": tilemap.SetTile(new Vector3Int(-1, 0), player_green); break;
                case "purple": tilemap.SetTile(new Vector3Int(-1, 0), player_purple); break;
                case "white": tilemap.SetTile(new Vector3Int(-1, 0), player_white); break;
                default: tilemap.SetTile(new Vector3Int(-1, 0), player); break;
            }
            boardPosition = boardPositions[currentUnitPosition];
            tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), null);
            unitPositionPlayer4 = 0;
            currentUnitPosition = 0;
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

    public static void BuildVillage(Tilemap tilemap, Tile village, Tile village_red, Tile village_blue, Tile village_green, Tile village_purple, Tile village_white)
    {
        boardPosition = boardSlotPositions[currentUnitPosition];
        string currentSpaceSlot = boardStructures[currentUnitPosition];
        secondaryButtonEnabled = false;
        if (currentPlayer == 1 && player_gold_one >= villageCost && currentSpaceSlot == "empty")
        {
            player_gold_one -= villageCost;
            boardStructures[currentUnitPosition] = "village_player_one";
            if (player_color_one == "red")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_red);
            }
            else if (player_color_one == "blue")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_blue);
            }
            else if (player_color_one == "green")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_green);
            }
            else if (player_color_one == "purple")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_purple);
            }
            else if (player_color_one == "white")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_white);
            }
        }
        else if (currentPlayer == 1 && player_gold_one < villageCost)
        {
            Debug.Log("Can't afford it");
        }
        else if (currentPlayer == 1 && currentSpaceSlot != "empty")
        {
            Debug.Log("Can't build here");
        }
        else if (currentPlayer == 2 && player_gold_two >= villageCost && currentSpaceSlot == "empty")
        {
            player_gold_two -= villageCost;
            boardStructures[currentUnitPosition] = "village_player_two";
            if (player_color_two == "red")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_red);
            }
            else if (player_color_two == "blue")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_blue);
            }
            else if (player_color_two == "green")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_green);
            }
            else if (player_color_two == "purple")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_purple);
            }
            else if (player_color_two == "white")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_white);
            }
        }
        else if (currentPlayer == 2 && player_gold_two < villageCost)
        {
            Debug.Log("Can't afford it");
        }
        else if (currentPlayer == 2 && currentSpaceSlot != "empty")
        {
            Debug.Log("Can't build here");
        }
        else if (currentPlayer == 3 && player_gold_three >= villageCost && currentSpaceSlot == "empty")
        {
            player_gold_three -= 50;
            boardStructures[currentUnitPosition] = "village_player_three";
            if (player_color_three == "red")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_red);
            }
            else if (player_color_three == "blue")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_blue);
            }
            else if (player_color_three == "green")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_green);
            }
            else if (player_color_three == "purple")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_purple);
            }
            else if (player_color_three == "white")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_white);
            }
        }
        else if (currentPlayer == 3 && player_gold_three < villageCost)
        {
            Debug.Log("Can't afford it");
        }
        else if (currentPlayer == 3 && currentSpaceSlot != "empty")
        {
            Debug.Log("Can't build here");
        }
        else if (currentPlayer == 4 && player_gold_three >= villageCost && currentSpaceSlot == "empty")
        {
            player_gold_four -= 50;
            boardStructures[currentUnitPosition] = "village_player_four";
            if (player_color_four == "red")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_red);
            }
            else if (player_color_four == "blue")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_blue);
            }
            else if (player_color_four == "green")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_green);
            }
            else if (player_color_four == "purple")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_purple);
            }
            else if (player_color_four == "white")
            {
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), village_white);
            }
        }
        else if (currentPlayer == 4 && player_gold_four < villageCost)
        {
            Debug.Log("Can't afford it");
        }
        else if (currentPlayer == 4 && currentSpaceSlot != "empty")
        {
            Debug.Log("Can't build here");
        }
    }

    public static void CombatEncounter(Tilemap tilemap, Tile player, Tile player_red, Tile player_blue, Tile player_green, Tile player_purple, Tile player_white, Tile monsterImp, Tile monsterBasilisk)
    {
        combatEncounterHappening = true;
        combatScreenEnabled = true;
        GUIEnabled = false;
        combatUnitOne = currentPlayer;
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
        else if (currentEnemy == "rampagingElephant")
        {
            combatUnitTwo = 7;
        }
        // Determines dice roll for combat unit one
        combatUnitOne_DiceTotal = 0;
        if (combatUnitOne == 1)
        {
            int x = player_combatDice_one;
            while (x > 0)
            {
                combatUnitOne_DiceTotal += Random.Range(1,7);
                x--;
            }
        }
        else if (combatUnitOne == 2)
        {
            int x = player_combatDice_two;
            while (x > 0)
            {
                combatUnitOne_DiceTotal += Random.Range(1,7);
                x--;
            }
        }
        else if (combatUnitOne == 3)
        {
            int x = player_combatDice_three;
            while (x > 0)
            {
                combatUnitOne_DiceTotal += Random.Range(1,7);
                x--;
            }
        }
        else if (combatUnitOne == 4)
        {
            int x = player_combatDice_four;
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
            int x = player_combatDice_one;
            while (x > 0)
            {
                combatUnitTwo_DiceTotal += Random.Range(1,7);
                x--;
            }
        }
        else if (combatUnitTwo == 2)
        {
            int x = player_combatDice_two;
            while (x > 0)
            {
                combatUnitTwo_DiceTotal += Random.Range(1,7);
                x--;
            }
        }
        else if (combatUnitTwo == 3)
        {
            int x = player_combatDice_three;
            while (x > 0)
            {
                combatUnitTwo_DiceTotal += Random.Range(1,7);
                x--;
            }
        }
        else if (combatUnitTwo == 4)
        {
            int x = player_combatDice_four;
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
            Debug.Log("Combat Unit Two: " + combatUnitTwo);
            // Imp Combat Rewards
            if (combatUnitTwo == 5)
            {
                if (combatUnitOne == 1)
                {
                    player_gold_one += 250;
                }
                else if (combatUnitOne == 2)
                {
                    player_gold_two += 250;
                }
                else if (combatUnitOne == 3)
                {
                    player_gold_three += 250;
                }
                else if (combatUnitOne == 4)
                {
                    player_gold_four += 250;
                }
            }
            // Basilisk Combat Rewards
            if (combatUnitTwo == 6)
            {
                if (combatUnitOne == 1)
                {
                    player_gold_one += 500;
                }
                else if (combatUnitOne == 2)
                {
                    player_gold_two += 500;
                }
                else if (combatUnitOne == 3)
                {
                    player_gold_three += 500;
                }
                else if (combatUnitOne == 4)
                {
                    player_gold_four += 500;
                }
            }
            boardMonsters[currentUnitPosition] = "empty";
            // Return combatUnitTwo to camp if it is a player
            if (combatUnitTwo == 1)
            {
                player_in_camp_one = true;
                switch (player_color_one)
                {
                    case "red": tilemap.SetTile(new Vector3Int(0, 1), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(0, 1), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(0, 1), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(0, 1), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(0, 1), player_white); break;
                    default: tilemap.SetTile(new Vector3Int(0, 1), player); break;
                }
                unitPositionPlayer1 = 0;
                livesPlayerOne -= 1;
                if (livesPlayerOne <= 0)
                {
                    player_alive_one = false;
                }
            }
            if (combatUnitTwo == 2)
            {
                player_in_camp_two = true;
                switch (player_color_two)
                {
                    case "red": tilemap.SetTile(new Vector3Int(1, 0), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(1, 0), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(1, 0), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(1, 0), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(1, 0), player_white); break;
                    default: tilemap.SetTile(new Vector3Int(1, 0), player); break;
                }
                unitPositionPlayer2 = 0;
                livesPlayerTwo -= 1;
                if (livesPlayerTwo <= 0)
                {
                    player_alive_two = false;
                }
            }
            if (combatUnitTwo == 3)
            {
                player_in_camp_three = true;
                switch (player_color_three)
                {
                    case "red": tilemap.SetTile(new Vector3Int(0, -1), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(0, -1), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(0, -1), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(0, -1), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(0, -1), player_white); break;
                    default: tilemap.SetTile(new Vector3Int(0, -1), player); break;
                }
                unitPositionPlayer3 = 0;
                livesPlayerThree -= 1;
                if (livesPlayerThree <= 0)
                {
                    player_alive_three = false;
                }
            }
            if (combatUnitTwo == 4)
            {
                player_in_camp_four = true;
                switch (player_color_four)
                {
                    case "red": tilemap.SetTile(new Vector3Int(-1, 0), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(-1, 0), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(-1, 0), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(-1, 0), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(-1, 0), player_white); break;
                    default: tilemap.SetTile(new Vector3Int(-1, 0), player); break;
                }
                unitPositionPlayer4 = 0;
                livesPlayerFour -= 1;
                if (livesPlayerFour <= 0)
                {
                    player_alive_four = false;
                }
            }
            MoveUnitComplete(tilemap, player, player_red, player_blue, player_green, player_purple, player_white);
        }
        else if (combatUnitOne_DiceTotal < combatUnitTwo_DiceTotal)
        {
            Debug.Log("Player " + combatUnitTwo + " has won!");
            if (combatUnitOne == 1)
            {
                player_in_camp_one = true;
                switch (player_color_one)
                {
                    case "red": tilemap.SetTile(new Vector3Int(0, 1), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(0, 1), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(0, 1), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(0, 1), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(0, 1), player_white); break;
                    default: tilemap.SetTile(new Vector3Int(0, 1), player); break;
                }
                unitPositionPlayer1 = 0;
                livesPlayerOne -= 1;
                if (livesPlayerOne <= 0)
                {
                    player_alive_one = false;
                }
            }
            else if (combatUnitOne == 2)
            {
                player_in_camp_two = true;
                switch (player_color_two)
                {
                    case "red": tilemap.SetTile(new Vector3Int(1, 0), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(1, 0), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(1, 0), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(1, 0), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(1, 0), player_white); break;
                    default: tilemap.SetTile(new Vector3Int(1, 0), player); break;
                }
                unitPositionPlayer2 = 0;
                livesPlayerTwo -= 1;
                if (livesPlayerTwo <= 0)
                {
                    player_alive_two = false;
                }
            }
            else if (combatUnitOne == 3)
            {
                player_in_camp_three = true;
                switch (player_color_three)
                {
                    case "red": tilemap.SetTile(new Vector3Int(0, -1), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(0, -1), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(0, -1), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(0, -1), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(0, -1), player_white); break;
                    default: tilemap.SetTile(new Vector3Int(0, -1), player); break;
                }
                unitPositionPlayer3 = 0;
                livesPlayerThree -= 1;
                if (livesPlayerThree <= 0)
                {
                    player_alive_three = false;
                }
            }
            else if (combatUnitOne == 4)
            {
                player_in_camp_four = true;
                switch (player_color_four)
                {
                    case "red": tilemap.SetTile(new Vector3Int(-1, 0), player_red); break;
                    case "blue": tilemap.SetTile(new Vector3Int(-1, 0), player_blue); break;
                    case "green": tilemap.SetTile(new Vector3Int(-1, 0), player_green); break;
                    case "purple": tilemap.SetTile(new Vector3Int(-1, 0), player_purple); break;
                    case "white": tilemap.SetTile(new Vector3Int(-1, 0), player_white); break;
                    default: tilemap.SetTile(new Vector3Int(-1, 0), player); break;
                }
                unitPositionPlayer4 = 0;
                livesPlayerFour -= 1;
                if (livesPlayerFour <= 0)
                {
                    player_alive_four = false;
                }
            }
            if (combatUnitTwo == 5)
            {
                boardPosition = boardPositions[currentUnitPosition];
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), monsterImp);
            }
            else if (combatUnitTwo == 6)
            {
                boardPosition = boardPositions[currentUnitPosition];
                tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), monsterBasilisk);
            }
        }
        else if (combatUnitOne_DiceTotal == combatUnitTwo_DiceTotal)
        {
            Debug.Log("Tied combat! SUDDEN DEATH!");
            int rand = Random.Range(1,3);
            if (rand == 1)
            {
                Debug.Log("Player " + currentPlayer + " has won!");
                boardMonsters[currentUnitPosition] = "empty";
                // Return combatUnitTwo to camp if it is a player
                if (combatUnitTwo == 1)
                {
                    player_in_camp_one = true;
                    switch (player_color_one)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(0, 1), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(0, 1), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(0, 1), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(0, 1), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(0, 1), player_white); break;
                        default: tilemap.SetTile(new Vector3Int(0, 1), player); break;
                    }
                    unitPositionPlayer1 = 0;
                    livesPlayerOne -= 1;
                    if (livesPlayerOne <= 0)
                    {
                        player_alive_one = false;
                    }
                }
                else if (combatUnitTwo == 2)
                {
                    player_in_camp_two = true;
                    switch (player_color_two)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(1, 0), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(1, 0), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(1, 0), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(1, 0), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(1, 0), player_white); break;
                        default: tilemap.SetTile(new Vector3Int(1, 0), player); break;
                    }
                    unitPositionPlayer2 = 0;
                    livesPlayerTwo -= 1;
                    if (livesPlayerTwo <= 0)
                    {
                        player_alive_two = false;
                    }
                }
                else if (combatUnitTwo == 3)
                {
                    player_in_camp_three = true;
                    switch (player_color_three)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(0, -1), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(0, -1), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(0, -1), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(0, -1), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(0, -1), player_white); break;
                        default: tilemap.SetTile(new Vector3Int(0, -1), player); break;
                    }
                    unitPositionPlayer3 = 0;
                    livesPlayerThree -= 1;
                    if (livesPlayerThree <= 0)
                    {
                        player_alive_three = false;
                    }
                }
                else if (combatUnitTwo == 4)
                {
                    player_in_camp_four = true;
                    switch (player_color_four)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(-1, 0), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(-1, 0), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(-1, 0), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(-1, 0), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(-1, 0), player_white); break;
                        default: tilemap.SetTile(new Vector3Int(-1, 0), player); break;
                    }
                    unitPositionPlayer4 = 0;
                    livesPlayerFour -= 1;
                    if (livesPlayerFour <= 0)
                    {
                        player_alive_four = false;
                    }
                }
            }
            else if (rand == 2)
            {
                Debug.Log("Player " + combatUnitTwo + " has won!");
                if (combatUnitOne == 1)
                {
                    player_in_camp_one = true;
                    switch (player_color_one)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(0, 1), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(0, 1), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(0, 1), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(0, 1), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(0, 1), player_white); break;
                        default: tilemap.SetTile(new Vector3Int(0, 1), player); break;
                    }
                    unitPositionPlayer1 = 0;
                    livesPlayerOne -= 1;
                    if (livesPlayerOne <= 0)
                    {
                        player_alive_one = false;
                    }
                }
                else if (combatUnitOne == 2)
                {
                    player_in_camp_two = true;
                    switch (player_color_two)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(1, 0), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(1, 0), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(1, 0), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(1, 0), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(1, 0), player_white); break;
                        default: tilemap.SetTile(new Vector3Int(1, 0), player); break;
                    }
                    unitPositionPlayer2 = 0;
                    livesPlayerTwo -= 1;
                    if (livesPlayerTwo <= 0)
                    {
                        player_alive_two = false;
                    }
                }
                else if (combatUnitOne == 3)
                {
                    player_in_camp_three = true;
                    switch (player_color_three)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(0, -1), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(0, -1), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(0, -1), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(0, -1), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(0, -1), player_white); break;
                        default: tilemap.SetTile(new Vector3Int(0, -1), player); break;
                    }
                    unitPositionPlayer3 = 0;
                    livesPlayerThree -= 1;
                    if (livesPlayerThree <= 0)
                    {
                        player_alive_three = false;
                    }
                }
                else if (combatUnitOne == 4)
                {
                    player_in_camp_four = true;
                    switch (player_color_four)
                    {
                        case "red": tilemap.SetTile(new Vector3Int(-1, 0), player_red); break;
                        case "blue": tilemap.SetTile(new Vector3Int(-1, 0), player_blue); break;
                        case "green": tilemap.SetTile(new Vector3Int(-1, 0), player_green); break;
                        case "purple": tilemap.SetTile(new Vector3Int(-1, 0), player_purple); break;
                        case "white": tilemap.SetTile(new Vector3Int(-1, 0), player_white); break;
                        default: tilemap.SetTile(new Vector3Int(-1, 0), player); break;
                    }
                    unitPositionPlayer4 = 0;
                    livesPlayerFour -= 1;
                    if (livesPlayerFour <= 0)
                    {
                        player_alive_four = false;
                    }
                }
                // Set monster avatar on the board
                if (combatUnitTwo == 5)
                {
                    boardPosition = boardPositions[currentUnitPosition];
                    tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), monsterImp);
                }
                else if (combatUnitTwo == 6)
                {
                    boardPosition = boardPositions[currentUnitPosition];
                    tilemap.SetTile(new Vector3Int((int)boardPosition[0], (int)boardPosition[1]), monsterBasilisk);
                }
            }
        }
    }
}