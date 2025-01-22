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
    public static int totalPlayers = 1;
    public static bool standardMode = true;
    public static bool oddMode = false;
    // Player Info //
    public static string playerClass = "nomad";
    public static bool playerInCamp = true;
    public static bool playerEnteredSecondLoop = false;
    public static bool playerEnteredThirdLoop = false;
    public static int maxHealth;
    public static int playerArmor;
    public static int playerMovementDice;
    public static int playerCombatDice;
    public static int playerCombat;
    public static int playerLives;
    public static int playerGold;
    public static int playerPotions;
    public static int weaponRange;
    public static List<int> playerWeapons = new List<int>();
    public static List<int> playerItems = new List<int>();
    // Tile References
    public static int playerAvatar;
    public static int playerVillage;
    // Current Player Info //
    public static int currentTurn;
    public static int currentPlayer;
    public static int currentPlayerArmor;
    public static int currentPlayerAvatar;
    public static int currentPlayerLives;
    public static int currentPlayerCombat;
    public static int currentPlayerGold;
    public static int currentPlayerColor;
    public static int current_weapon;
    public static int currentPlayerMovementDice = 1;
    public static int currentHumanPlayer = 1;
    public static bool currentPlayerIsHuman = true;
    // Player Info //
    public static List<string> playerTitle = new List<string>();
    public static List<bool> playerIsActive = new List<bool>();
    public static List<bool> playerIsHuman = new List<bool>();
    public static List<int> playerColor = new List<int>();
    //
    public static Dictionary<int, string> player_class = new Dictionary<int, string>();
    public static Dictionary<int, List<int>> player_weapons = new Dictionary<int, List<int>>();
    public static Dictionary<int, List<int>> player_items = new Dictionary<int, List<int>>();
    public static Dictionary<int, int> player_avatar = new Dictionary<int, int>();
    public static Dictionary<int, int> player_health = new Dictionary<int, int>();
    public static Dictionary<int, int> player_lives = new Dictionary<int, int>();
    public static Dictionary<int, int> player_armor = new Dictionary<int, int>();
    public static Dictionary<int, int> player_movementDice = new Dictionary<int, int>();
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

    private void Awake()
    {
        TurnManager.PopulateTurnOrder();
        TurnOrderGUI.UpdateTurnOrderGUI();
    }

    void Start()
    {
        totalPlayers = 1;
        currentBoard = 1;
        weaponRange = 2;
        playerPotions = 3;
        playerClass = "nomad";
        BoardManager.GenerateGameBoard();
        GameSetup();
        GUI.SetActive(true);
        Arrows.EnableArrowButtons();
        BoardManager.SpawnPlayersInCamp();
        TurnManager.StartPlayerTurn();
    }

    public static void GameSetup()
    {
        int[] values = Classes.ClassStartingStats(playerClass);
        playerAvatar = values[0];
        Player.health = values[1];
        Player.health = values[1];
        playerLives = values[2];
        playerArmor = values[3];
        playerMovementDice = values[4];
        playerVillage = 1;
        playerGold = 150;
    }
}