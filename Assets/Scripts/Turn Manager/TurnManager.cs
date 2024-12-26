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

    public static void SetInitialTurnOrder()
    {
        turnOrder.Add("spawnDungeons");
        turnOrder.Add("spawnMerchants");
        for (int i = 1; i <= GameMain.activePlayers; i++)
        {
            turnOrder.Add("player");
        }
        turnOrder.Add("spawnMonsters");
        turnOrder.Add("endTurn");
    }

    public static void TurnProgressionHandler(Tilemap tilemapStructures)
    {
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
        }
        turnOrder.Remove(turnOrder[currentTurnItem]);
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
            turnPool.Add("spawnMerchants");
            currentTurnItem = 0;
        }
        else
        {
            turnOrder.Add(turnPool.First());
            turnPool.RemoveAt(0);
            currentTurnItem++;
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

    public static void SpawnDungeons(Tilemap tilemapStructures)
    {
        Debug.Log("Spawn Dungeons");
        foreach (Vector3 listVector in World.boardSlotPositions)
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
                        vector3[0] = -1;
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
        TurnProgressionHandler(tilemapStructures);
    }

    public static void SpawnMerchants(Tilemap tilemapStructures)
    {
        Debug.Log("Spawn Merchants");
        /*foreach (Vector3 listVector in World.boardEmptySlotPositions)
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
                    random = Random.Range(1,3);
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
        }*/
        TurnProgressionHandler(tilemapStructures);
    }
}
