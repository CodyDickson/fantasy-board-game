using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class GameMain : MonoBehaviour
{
    // Game Settings //
    [SerializeField] public bool devMode = false;
    public static int currentBoard = 1;
    public static int totalPlayers = 2;
    public static int startingGold = 250;
    public static int startingCombat = 0;
    public static int startingLives = 1;
    public static int startingHealth = 3;
    public static int startingArmor = 0;
    public static bool standardMode = true;
    public static bool oddMode = false;
    public static bool randomizeTurnOrder = false;
    // Current Player Info //
    public static int currentTurn;
    public static int currentPlayer;
    public static int currentPlayerHealth;
    public static int currentPlayerArmor;
    public static int currentPlayerAvatar;
    public static int currentPlayerLives;
    public static int currentPlayerCombat;
    public static int currentPlayerGold;
    public static int currentPlayerColor;
    public static int currentPlayerMovementDice = 1;
    public static int currentHumanPlayer = 1;
    public static bool currentPlayerIsHuman = true;
    public static bool currentPlayerInCamp = true;
    // All Player Info //
    // Active Players are alive but may not be present on the board // 
    public static List<bool> playerIsActive = new List<bool>();
    // Avatar //
    public static List<int> playerAvatar = new List<int>();
    // Color //
    public static List<int> playerColor = new List<int>();
    // Health, Green //
    public static List<int> playerHealth = new List<int>();
    // Armor, Gray //
    public static List<int> playerArmor = new List<int>();
    // Lives, White //
    public static List<int> playerLives = new List<int>();
    // Gold, Yellow //
    public static List<int> playerGold = new List<int>();
    // Combat, Red //
    public static List<int> playerCombat = new List<int>();
    // Human or Computer? //
    public static List<bool> playerIsHuman = new List<bool>();
    // Statuses //
    public static List<bool> playerHasBurn = new List<bool>();
    public static List<int> playerHasFrozen = new List<int>();
    public static List<int> playerHasCurse = new List<int>();
   // Determines which screen (and content) is displayed //
    public static bool GUIEnabled = true;
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
    public static int player_moveDice_one = 1;
    public static int player_moveDice_two = 1;
    public static int player_moveDice_three = 1;
    public static int player_moveDice_four = 1;
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
    public static int[] xy = new int[2];
    // Board tracking //
    public static List<Vector3> boardPositions = new List<Vector3>();
    public static Dictionary<int, string> boardStructures = new Dictionary<int, string>();
    public static Dictionary<int, string> boardUnits = new Dictionary<int, string>();
    public static int boardLength;
    public static Vector3 boardPosition;
    public static Vector3 onDeckPosition;
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
        totalPlayers = 2;
        currentBoard = 1;
        BoardManager.GenerateGameBoard();
        Fog.GenerateFog();
        Fog.RemoveLocalFog(0);
        GameSetup();
        GUI.SetActive(true);
        BoardManager.SpawnPlayersInCamp();
        TurnManager.SetInitialTurnOrder();
    }

    void Update()
    {
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

    public static void UpdateCurrentPlayerInfo()
    {
        currentPlayerLives = playerLives[currentPlayer];
        currentPlayerHealth = playerHealth[currentPlayer];
        currentPlayerArmor = playerArmor[currentPlayer];
        currentPlayerCombat = playerCombat[currentPlayer];
        currentPlayerGold = playerGold[currentPlayer];
        currentPlayerAvatar = playerAvatar[currentPlayer];
        currentPlayerColor = playerColor[currentPlayer];
    }

    public static void GameSetup()
    {
        playerLives.Add(startingLives);
        for (int i = 1; i <= totalPlayers; i++)
        {
            playerLives.Add(playerLives[0]);
        }
        playerHealth.Add(startingHealth);
        for (int i = 1; i <= totalPlayers; i++)
        {
            playerHealth.Add(playerHealth[0]);
        }
        playerGold.Add(startingGold);
        for (int i = 1; i <= totalPlayers; i++)
        {
            playerGold.Add(playerGold[0]);
        }
        playerCombat.Add(startingCombat);
        for (int i = 1; i <= totalPlayers; i++)
        {
            playerCombat.Add(playerCombat[0]);
        }
        playerArmor.Add(startingArmor);
        for (int i = 1; i <= totalPlayers; i++)
        {
            playerArmor.Add(playerArmor[0]);
        }
        // Player Avatar will need to be set from the game set up menu
        playerAvatar.Add(0);
        for (int i = 1; i <= totalPlayers; i++)
        {
            playerAvatar.Add(i);
        }
        // Player Color will need to be set from the game set up menu
        playerColor.Add(0);
        for (int i = 1; i <= totalPlayers; i++)
        {
            playerColor.Add(i);
        }
        // Player Is Human will need to be set from the game set up menu
        playerIsHuman.Add(false);
        for (int i = 1; i <= totalPlayers; i++)
        {
            playerIsHuman.Add(true);
        }
        playerIsActive.Add(false);
        for (int i = 1; i <= totalPlayers; i++)
        {
            playerIsActive.Add(playerIsActive[0]);
        }
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
}