using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dungeons : MonoBehaviour
{
    public Button closeDungeonWindow;
    public Button raidDungeon;

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