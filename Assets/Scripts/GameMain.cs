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
    public static bool setCurrentPlayer = false;
    public static int playerMovementDice = 1;
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

    // On turn start, GameMain loads all information for the current player or monster
    // currentPlayer values are set 

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

    private void Update()
    {
        if (setCurrentPlayer)
        {
            UpdateCurrentPlayer();
        }
    }

    public static void UpdateCurrentPlayer()
    {

    }
}