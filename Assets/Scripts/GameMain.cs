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
    public static bool playerInCamp = true;
    public static bool playerEnteredSecondLoop = false;
    public static bool playerEnteredThirdLoop = false;
    public static int maxHealth;
    public static int playerArmor;
    public static int playerMovementDice;
    public static int playerCombatDice;
    public static int playerCombat;
    public static int playerGold;
    public static List<int> playerItems = new List<int>();
    // Current Player Info //
    public static int currentTurn;
    public static int currentPlayer;
    public static int currentPlayerArmor;
    public static int currentPlayerLives;
    public static int currentPlayerCombat;
    public static int currentPlayerGold;
    public static int currentPlayerColor;
    public static int current_weapon;
    public static int currentPlayerMovementDice = 1;
    public static int currentHumanPlayer = 1;
    public static bool currentPlayerIsHuman = true;
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
        BoardManager.GenerateGameBoard();
        BoardManager.SpawnPlayersInCamp();
        TurnManager.StartPlayerTurn();
        Arrows.DisableArrowButtons();
        GUI.SetActive(true);
    }
}