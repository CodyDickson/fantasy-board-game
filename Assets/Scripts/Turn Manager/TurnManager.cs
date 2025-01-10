using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TurnManager : MonoBehaviour
{
    public static List<string> turnOrder = new List<string>();
    public static bool continueTurnProgression = false;
    private float counter = 2f;
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

    public static void PopulateTurnOrder()
    {
        for (int i = 1; i <= 2; i++)
        {
            turnOrder.Add("player");
            turnOrder.Add("spawnMonsters");
            turnOrder.Add("player");
            turnOrder.Add("spawnMonsters");
            // Move Monsters
            // Spawn Merchants
        }
    }

    public static void TurnProgressionHandler()
    {
        continueTurnProgression = false;
        turnOrder.Remove(turnOrder.First());
        if (turnOrder.Count < 5)
        {
            PopulateTurnOrder();
        }
        switch (turnOrder[0])
        {
            case "player": StartPlayerTurn(); break;
            case "moveMonsters": break;
            case "spawnMonsters": Monsters.SpawnMonster(); break;
            default: Debug.Log("This Should Never Show"); break;
        }
        TurnOrderGUI.UpdateTurnOrderGUI();
        Debug.Log("Current Turn Item: " + turnOrder[0]);
    }

    public static void EndPlayerTurn()
    {
        GameMain.playerGold += Villages.totalVillageGoldPerTurn;
        GUIManager.UpdatePlayerGUI();
    }

    public static void StartPlayerTurn()
    {
        if (GameMain.playerInCamp) { Arrows.EnableArrowButtons(); }
        else { GUIManager.ToggleMoveButton(true); }
    }
}