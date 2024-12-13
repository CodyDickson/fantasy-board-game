using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dungeons : MonoBehaviour
{
    public Button closeDungeonWindow;
    public Button raidDungeon;
    public static string dungeonType = "";

    void Start()
    {
        closeDungeonWindow.onClick.AddListener(OnClickCloseDungeonWindow);
        raidDungeon.onClick.AddListener(OnClickRaidDungeon);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R) && GameMain.dungeonScreenEnabled)
        {
            OnClickRaidDungeon();
        }
        if (Input.GetKey(KeyCode.C) && GameMain.dungeonScreenEnabled)
        {
            OnClickCloseDungeonWindow();
        }
    }

    void UpdateDungeonInfo()
    {
        // When landing on a dungeon OR clicking on a dungeon
        // Updates the LowerGUI, image of the dungeon on the left and "x turns to grow", "monster type"
        // Lower button is "Raid" if the player has landed on it
        // This opens the Raid GUI in the middle of the screen
    }

    void OnClickRaidDungeon()
    {
        GameMain.dungeonScreenEnabled = false;
        GameMain.combatScreenEnabled = true;
    }

    void OnClickCloseDungeonWindow()
    {
        GameMain.dungeonScreenEnabled = false;
        GameMain.GUIEnabled = true;
    }

    void RaidDungeon()
    {
        int random = Random.Range(1,101);
        // Chance at gold reward
        // Chance at combat
        // Small chance of spawning an elite
    }
}