using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TurnManager : MonoBehaviour
{
    public static List<string> turnOrder = new List<string>();
    public static List<string> turnPool = new List<string>();
    public static int currentTurnItem = 0;
    public static GUI gui;

    void Start()
    {
        SetInitialTurnOrder();
    }

    public static void SetInitialTurnOrder()
    {
        for (int i = 1; i <= GameMain.activePlayers; i++)
        {
            turnOrder.Add("player");
        }
        turnOrder.Add("spawnDungeons");
        turnOrder.Add("spawnMonsters");
        turnOrder.Add("spawnItemShops");
        turnOrder.Add("endTurn");
    }

    public static void TurnProgressionHandler()
    {
        currentTurnItem++;
        switch (turnOrder[currentTurnItem])
        {
            case "player": PlayerTurn(); break;
            case "moveMonsters": break;
            case "spawnDungeons": SpawnDungeons(); break;
            case "spawnMonsters": break;
            case "spawnEliteMonster": break;
            case "spawnOddity": break;
            case "spawnItemShops": break;
            case "endTurn": EndTurn(gui); break;
        }
        turnOrder.Remove(turnOrder[currentTurnItem]);
        turnOrder.Add(turnPool.First());
        turnPool.RemoveAt(0);
        if (turnPool.Count == 0)
        {
            for (int i = 1; i <= GameMain.activePlayers; i++)
            {
                turnPool.Add("player");
            }
            turnPool.Add("moveMonsters");
            turnPool.Add("spawnDungeons");
            turnPool.Add("spawnMonsters");
            turnPool.Add("spawnEliteMonsters");
            turnPool.Add("spawnOddity");
            turnPool.Add("spawnItemShops");
        }
    }

    public static void EndTurn(GUI gui)
    {
        World.villageNearby = false;
        UpdatePlayerGUIAvatar.playerGUIAvatarHasBeenUpdated = false;
        UpdateGUIColor.updateGUIColor = true;
        GUI.enablePrimaryButton = true;
        gui.EnableSecondaryButton(true);
        World.CheckForLocalBoardPositions();
    }

    public static void PlayerTurn()
    {
        GameMain.currentPlayer += 1;
        if (GameMain.currentPlayer > GameMain.activePlayers)
        {
            if (GameMain.playerOneIsActive)
            {
                GameMain.currentPlayer = 1;
            }
            else if (GameMain.playerTwoIsActive)
            {
                GameMain.currentPlayer = 2;
            }
            else if (GameMain.playerThreeIsActive)
            {
                GameMain.currentPlayer = 3;
            }
            else if (GameMain.playerFourIsActive)
            {
                GameMain.currentPlayer = 4;
            }
            GameMain.currentTurn += 1;
        }
        if (GameMain.currentTurn == 1)
        {
            switch (GameMain.currentPlayer)
            {
                case 2: GameMain.playerTwoIsActive = true; World.playerCurrentlyInCamp = true; break;
                case 3: GameMain.playerThreeIsActive = true; World.playerCurrentlyInCamp = true; break;
                case 4: GameMain.playerFourIsActive = true; World.playerCurrentlyInCamp = true; break;
            }
            Camp.SpawnActivePlayerInCamp();
        }
        if (GameMain.currentPlayer == 1)
        {
            GameMain.currentPlayerDice = GameMain.player_combatDice_one;
            World.currentPlayerColor = GameMain.playerOneColor;
            World.currentUnitPosition = World.playerOnePosition;
            for (int i = 1; i <= Villages.playerOneVillageGoldPerTurn.Count; i++)
            {
                GameMain.playerOneGold += Villages.playerOneVillageGoldPerTurn[i];
            }
            for (int i = 1; i <= Villages.playerOneVillageGrowth.Count; i++)
            {
                Villages.playerOneVillageGrowth[i] -= 1;
                if (Villages.playerOneVillageGrowth[i] == 0)
                {
                    Villages.playerOneVillageGrowth[i] = 3;
                    Villages.GrowVillage(i);
                }
            }
        }
        else if (GameMain.currentPlayer == 2)
        {
            GameMain.currentPlayerDice = GameMain.player_combatDice_two;
            World.currentPlayerColor = GameMain.playerTwoColor;
            World.currentUnitPosition = World.playerTwoPosition;
            for (int i = 1; i <= Villages.playerTwoVillageGoldPerTurn.Count; i++)
            {
                GameMain.playerTwoGold += Villages.playerTwoVillageGoldPerTurn[i];
            }
            for (int i = 1; i <= Villages.playerTwoVillageGrowth.Count; i++)
            {
                Villages.playerTwoVillageGrowth[i] -= 1;
                if (Villages.playerTwoVillageGrowth[i] == 0)
                {
                    Villages.playerTwoVillageGrowth[i] = 3;
                    Villages.GrowVillage(i);
                }
            }
        }
        else if (GameMain.currentPlayer == 3)
        {
            GameMain.currentPlayerDice = GameMain.player_combatDice_three;
            World.currentPlayerColor = GameMain.playerThreeColor;
            World.currentUnitPosition = World.playerThreePosition;
            for (int i = 1; i <= Villages.playerThreeVillageGoldPerTurn.Count; i++)
            {
                GameMain.playerThreeGold += Villages.playerThreeVillageGoldPerTurn[i];
            }
            for (int i = 1; i <= Villages.playerThreeVillageGrowth.Count; i++)
            {
                Villages.playerThreeVillageGrowth[i] -= 1;
                if (Villages.playerThreeVillageGrowth[i] == 0)
                {
                    Villages.playerThreeVillageGrowth[i] = 3;
                    Villages.GrowVillage(i);
                }
            }
        }
        else if (GameMain.currentPlayer == 4)
        {
            GameMain.currentPlayerDice = GameMain.player_combatDice_four;
            World.currentPlayerColor = GameMain.playerFourColor;
            World.currentUnitPosition = World.playerFourPosition;
            for (int i = 1; i <= Villages.playerFourVillageGoldPerTurn.Count; i++)
            {
                GameMain.playerFourGold += Villages.playerFourVillageGoldPerTurn[i];
            }
            for (int i = 1; i <= Villages.playerFourVillageGrowth.Count; i++)
            {
                Villages.playerFourVillageGrowth[i] -= 1;
                if (Villages.playerFourVillageGrowth[i] == 0)
                {
                    Villages.playerFourVillageGrowth[i] = 3;
                    Villages.GrowVillage(i);
                }
            }
        }
    }

    public static void SpawnDungeons()
    {
        foreach (Vector3 listVector in World.boardEmptySlotPositions)
        {
            Vector3 vector3 = listVector;
            World.currentUnitPosition = listVector;
            World.CheckForLocalBoardPositions();
            int random;
            if (!World.northPositionAvailable)
            {
                random = Random.Range(1, 10);
                if (random == 1)
                {
                    vector3[1] += 1;
                    World.boardEmptySlotPositions.Add(vector3);
                    vector3 = listVector;
                }
            }
            if (!World.eastPositionAvailable)
            {
                random = Random.Range(1, 10);
                if (random == 1)
                {
                    vector3[0] += 1;
                    World.boardEmptySlotPositions.Add(vector3);
                    vector3 = listVector;
                }
            }
            if (!World.southPositionAvailable)
            {
                random = Random.Range(1, 10);
                if (random == 1)
                {
                    vector3[1] -= 1;
                    World.boardEmptySlotPositions.Add(vector3);
                    vector3 = listVector;
                }
            }
            if (!World.westPositionAvailable)
            {
                random = Random.Range(1, 10);
                if (random == 1)
                {
                    vector3[0] -= 1;
                    World.boardEmptySlotPositions.Add(vector3);
                    vector3 = listVector;
                }
            }
        }
    }
}
