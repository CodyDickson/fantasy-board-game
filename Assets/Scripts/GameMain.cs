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
    // Player Info //
    public static List<string> playerTitle = new List<string>(); 
    public static List<bool> playerIsActive = new List<bool>();
    public static List<bool> playerInCamp = new List<bool>();
    public static List<bool> playerIsHuman = new List<bool>();
    public static List<int> playerAvatar = new List<int>();
    public static List<int> playerColor = new List<int>();
    public static List<int> playerHealth = new List<int>();
    public static List<int> playerArmor = new List<int>();
    public static List<int> playerLives = new List<int>();
    public static List<int> playerGold = new List<int>();
    public static List<int> playerCombat = new List<int>();
    public static List<int> playerMovementDice = new List<int>();
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
    public GameObject GUI;

    void Start()
    {
        totalPlayers = 2;
        currentBoard = 1;
        BoardManager.GenerateGameBoard();
        Fog.GenerateFog();
        Fog.RemoveLocalFog(0);
        GameSetup();
        GUI.SetActive(true);
        Arrows.EnableArrowButtons();
        BoardManager.SpawnPlayersInCamp();
        TurnManager.SetInitialTurnOrder();
        playerTitle.Add("");
        playerTitle.Add("Red Wizard");
    }

    void Update()
    {
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
        playerLives.Add(1);
        playerHealth.Add(5);
        playerGold.Add(100);
        playerCombat.Add(2);
        playerArmor.Add(0);
        playerAvatar.Add(0);
        playerColor.Add(0);
        playerIsHuman.Add(false);
        playerIsActive.Add(false);
        playerInCamp.Add(false);
        playerMovementDice.Add(1);
        for (int i = 1; i <= totalPlayers; i++) {
            playerHealth.Add(playerHealth[0]); 
            playerLives.Add(playerLives[0]);
            playerGold.Add(playerGold[0]);
            playerCombat.Add(playerCombat[0]);
            playerArmor.Add(playerArmor[0]);
            playerAvatar.Add(1);
            playerColor.Add(1);
            playerIsHuman.Add(true);
            playerIsActive.Add(playerIsActive[0]);
            playerInCamp.Add(playerInCamp[0]);
            playerMovementDice.Add(1);
        }
    }
}