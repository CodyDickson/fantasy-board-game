using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class TurnManager : MonoBehaviour
{
    public static List<string> turnOrder = new List<string>();
    public static List<string> turnPool = new List<string>();
    public static GUIManager gui;
    public static bool continueTurnProgression = false;
    private float counter = 0.5f;
    private float tempCounter = 0f;
    public Tilemap tilemapStructures;

    void Update()
    {
        if (continueTurnProgression)
        {
            if (tempCounter <= 0f)
            {
                TurnProgressionHandler();
                tempCounter = counter;
            }
            else
            {
                tempCounter -= Time.deltaTime;
            }
        }
    }

    public static void SetInitialTurnOrder()
    {
        for (int i = 1; i <= GameMain.totalPlayers; i++)
        {
            turnOrder.Add("player");
        }
        turnOrder.Add("spawnMonsters");
        for (int i = 1; i <= GameMain.totalPlayers; i++)
        {
            turnOrder.Add("player");
        }
        turnOrder.Add("moveMonsters");
        turnOrder.Add("spawnMonsters");
    }

    public static void PopulateTurnOrder()
    {
        for (int i = 1; i <= GameMain.totalPlayers; i++)
        {
            turnOrder.Add("player");
        }
        turnOrder.Add("moveMonsters");
        turnOrder.Add("spawnMonsters");
    }

    public static void TurnProgressionHandler()
    {
        continueTurnProgression = false;
        Debug.Log("Current Turn Item: " + turnOrder[0]);
        switch (turnOrder[0])
        {
            case "player": StartPlayerTurn(); break;
            case "moveMonsters": break;
            case "spawnMonsters": Monsters.SpawnMonster(); break;
            default: Debug.Log("This Should Never Show"); break;
        }
        turnOrder.Remove(turnOrder.First());
        if (turnOrder.Count < 4)
        {
            PopulateTurnOrder();
        }
    }

    public static void EndPlayerTurn(GUIManager gui)
    {
        InfoGUI.ToggleInfoGUI(true);
        if (GameMain.currentPlayer == 1)
        {
            for (int i = 0; i <= Villages.playerOneVillageGoldPerTurn.Count; i++)
            {
                GameMain.playerGold[1] += Villages.playerOneVillageGoldPerTurn[i];
            }
        }
        if (GameMain.currentPlayer == 2)
        {
            World.currentUnitPosition = World.playerTwoPosition;
            for (int i = 0; i <= Villages.playerTwoVillageGoldPerTurn.Count; i++)
            {
                GameMain.playerGold[2] += Villages.playerTwoVillageGoldPerTurn[i];
            }
        }
        if (GameMain.currentPlayer == 3)
        {
            World.currentUnitPosition = World.playerThreePosition;
            for (int i = 0; i <= Villages.playerThreeVillageGoldPerTurn.Count; i++)
            {
                GameMain.playerGold[3] += Villages.playerThreeVillageGoldPerTurn[i];
            }
        }
        if (GameMain.currentPlayer == 4)
        {
            World.currentUnitPosition = World.playerFourPosition;
            for (int i = 0; i <= Villages.playerFourVillageGoldPerTurn.Count; i++)
            {
                GameMain.playerGold[4] += Villages.playerFourVillageGoldPerTurn[i];
            }
        }
        BoardManager.villageNearby = false;
        BoardManager.dungeonNearby = false;
        BoardManager.merchantNearby = false;
        UpdatePlayerGUIAvatar.playerGUIAvatarHasBeenUpdated = false;
        UpdateGUIColor.ChangeGUIColor();
        GUIManager.ToggleMoveButton(true);
        BoardManager.CheckForLocalBoardPositions();
    }

    public static void StartPlayerTurn()
    {
        UpdatePlayerGUIAvatar.updatePlayerGUIAvatar = true;
        ItemGUI.UpdateWeaponAvatar();
        GameMain.currentPlayer += 1;
        Debug.Log("Player Turn, Current Player is " + GameMain.currentPlayer);
        if (GameMain.currentPlayer > GameMain.totalPlayers)
        {
            if (GameMain.playerLives[1] > 0)
            {
                GameMain.currentPlayer = 1;
            }
            else if (GameMain.playerLives[2] > 0)
            {
                GameMain.currentPlayer = 2;
            }
            else if (GameMain.playerLives[3] > 0)
            {
                GameMain.currentPlayer = 3;
            }
            else if (GameMain.playerLives[4] > 0)
            {
                GameMain.currentPlayer = 4;
            }
        }
        if (GameMain.playerInCamp[GameMain.currentPlayer])
        {
            Arrows.EnableArrowButtons();
            // GUI.playerGUIHasBeenUpdated = false;
        }
        else
        {
            GUIManager.ToggleMoveButton(true);
        }
        GameMain.UpdateCurrentPlayerInfo();
        BoardManager.currentUnitPosition = BoardManager.playerPositions[GameMain.currentPlayer];
        if (GameMain.playerIsHuman[GameMain.currentPlayer] == true) { GameMain.currentPlayerIsHuman = true; }
        if (!GameMain.currentPlayerIsHuman)
        {
            ComputerPlayerTurn();
        }
    }

    public static void ComputerPlayerTurn()
    {
        //
    }
}
