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
    public static int currentTurnItem = 0;
    public static GUI gui;
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
                TurnProgressionHandler(tilemapStructures);
                tempCounter = counter;
                continueTurnProgression = false;
            }
            else
            {
                tempCounter -= Time.deltaTime;
            }
        }
    }

    public static void SetInitialTurnOrder()
    {
        turnOrder.Add("");
        turnOrder.Add("spawnDungeons");
        turnOrder.Add("spawnMerchants");
        for (int i = 1; i <= GameMain.activePlayers; i++)
        {
            turnOrder.Add("player");
        }
        turnOrder.Add("spawnMonsters");
        turnOrder.Add("endTurn");
        int random = Random.Range(1, 5);
        for (int i = 1; i <= GameMain.activePlayers; i++) { turnPool.Add("player"); }
        turnPool.Add("moveMonsters");
        if (random == 1) { turnPool.Add("spawnDungeons"); }
        if (random == 3) { turnPool.Add("spawnMonsters"); }
        turnPool.Add("spawnEliteMonsters");
        if (random == 5) { random = Random.Range(1, 3); if (random == 1) { turnPool.Add("spawnOddity"); } }
        if (random == 2) { turnPool.Add("spawnMerchants"); }
    }

    public static void TurnProgressionHandler(Tilemap tilemapStructures)
    {
        currentTurnItem += 1;
        Debug.Log("Current Turn Item Number: " + currentTurnItem);
        Debug.Log("Current Turn Item: " + turnOrder[currentTurnItem]);
        switch (turnOrder[currentTurnItem])
        {
            case "player": PlayerTurn(); break;
            case "moveMonsters": break;
            case "spawnDungeons": SpawnDungeons(tilemapStructures); break;
            case "spawnMonsters": break;
            case "spawnEliteMonster": break;
            case "spawnOddity": break;
            case "spawnMerchants": SpawnMerchants(tilemapStructures); break;
            case "endTurn": EndTurn(gui); break;
            default: Debug.Log("pass"); break;
        }
        turnOrder.Remove(turnOrder[currentTurnItem]);
        if (turnPool.Count == 0)
        {
            int random = Random.Range(1,5);
            for (int i = 1; i <= GameMain.activePlayers; i++) { turnPool.Add("player"); }
            turnPool.Add("moveMonsters");
            if (random == 1) { turnPool.Add("spawnDungeons"); }
            if (random == 3) { turnPool.Add("spawnMonsters"); }
            turnPool.Add("spawnEliteMonsters");
            if (random == 5) { random = Random.Range(1, 3); if (random == 1) { turnPool.Add("spawnOddity"); } }
            if (random == 2) { turnPool.Add("spawnMerchants"); }
        }
        else
        {
            turnOrder.Add(turnPool.First());
            turnPool.RemoveAt(0);
        }
    }

    public static void EndTurn(GUI gui)
    {
        World.villageNearby = false;
        UpdatePlayerGUIAvatar.playerGUIAvatarHasBeenUpdated = false;
        UpdateGUIColor.updateGUIColor = true;
        GUI.enablePrimaryButton = true;
        GUI.primaryButtonAssignedTo = "move";
        World.CheckForLocalBoardPositions();
    }

    public static void PlayerTurn()
    {
        Debug.Log("Player Turn, Current Player is " + GameMain.currentPlayer);
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
        }
    }

    public static void SpawnDungeons(Tilemap tilemapStructures)
    {
        foreach (Vector3 listVector in World.boardPositions)
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
                    random = Random.Range(1, 3);
                    if (random == 1)
                    {
                        vector3[1] += 1;
                        World.boardImpDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                    else if (random == 2)
                    {
                        vector3[1] += 1;
                        World.boardBasiliskDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                }
            }
            if (!World.eastPositionAvailable)
            {
                random = Random.Range(1, 10);
                if (random == 1)
                {
                    random = Random.Range(1, 3);
                    if (random == 1)
                    {
                        vector3[0] += 1;
                        World.boardImpDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                    else if (random == 2)
                    {
                        vector3[0] += 1;
                        World.boardBasiliskDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                }
            }
            if (!World.southPositionAvailable)
            {
                random = Random.Range(1, 10);
                if (random == 1)
                {
                    random = Random.Range(1, 3);
                    if (random == 1)
                    {
                        vector3[1] -= 1;
                        World.boardImpDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                    else if (random == 2)
                    {
                        vector3[1] -= 1;
                        World.boardBasiliskDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                }
            }
            if (!World.westPositionAvailable)
            {
                random = Random.Range(1, 10);
                if (random == 1)
                {
                    random = Random.Range(1, 3);
                    if (random == 1)
                    {
                        vector3[0] -= 1;
                        World.boardImpDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                    else if (random == 2)
                    {
                        vector3[0] -= 1;
                        World.boardBasiliskDungeonPositions.Add(vector3);
                        vector3 = listVector;
                    }
                }
            }
        }
        foreach (Vector3 impDungeon in World.boardImpDungeonPositions)
        {
            tilemapStructures.SetTile(new Vector3Int((int)impDungeon[0], (int)impDungeon[1]), Store.dungeons[0]);
        }
        foreach (Vector3 basiliskDungeon in World.boardBasiliskDungeonPositions)
        {
            tilemapStructures.SetTile(new Vector3Int((int)basiliskDungeon[0], (int)basiliskDungeon[1]), Store.dungeons[1]);
        }
        continueTurnProgression = true;
    }

    public static void SpawnMerchants(Tilemap tilemapStructures)
    {
        foreach (Vector3 listVector in World.boardPositions)
        {
            Vector3 vector3 = listVector;
            World.currentUnitPosition = listVector;
            World.CheckForLocalBoardPositions();
            int random;
            if (!World.northPositionAvailable)
            {
                random = Random.Range(1, 101);
                if (random == 1)
                {
                    vector3[1] += 1;
                    World.boardMerchantPositions.Add(vector3);
                    vector3 = listVector;
                }
            }
            if (!World.eastPositionAvailable)
            {
                random = Random.Range(1, 101);
                if (random == 1)
                {
                    vector3[0] += 1;
                    World.boardMerchantPositions.Add(vector3);
                    vector3 = listVector;
                }
            }
            if (!World.southPositionAvailable)
            {
                random = Random.Range(1, 101);
                if (random == 1)
                {
                    vector3[1] -= 1;
                    World.boardMerchantPositions.Add(vector3);
                    vector3 = listVector;
                }
            }
            if (!World.westPositionAvailable)
            {
                random = Random.Range(1, 101);
                if (random == 1)
                {
                    vector3[0] -= 1;
                    World.boardMerchantPositions.Add(vector3);
                    vector3 = listVector;
                }
            }
        }
        foreach (Vector3 merchant in World.boardMerchantPositions)
        {
            int random = Random.Range(0,5);
            switch (random)
            {
                case 0: tilemapStructures.SetTile(new Vector3Int((int)merchant[0], (int)merchant[1]), Store.merchants[0]); break;
                case 1: tilemapStructures.SetTile(new Vector3Int((int)merchant[0], (int)merchant[1]), Store.merchants[1]); break;
                case 2: tilemapStructures.SetTile(new Vector3Int((int)merchant[0], (int)merchant[1]), Store.merchants[2]); break;
                case 3: tilemapStructures.SetTile(new Vector3Int((int)merchant[0], (int)merchant[1]), Store.merchants[3]); break;
            }
            
        }
        continueTurnProgression = true;
    }
}
