using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dungeons : MonoBehaviour
{
    public Button closeButton;
    public Button raidButton;

    void Start()
    {
        closeButton.onClick.AddListener(OnClickCloseButton);
        raidButton.onClick.AddListener(OnClickRaidButton);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R) && GameMain.dungeonScreenEnabled)
        {
            OnClickRaidButton();
        }
        if (Input.GetKey(KeyCode.C) && GameMain.dungeonScreenEnabled)
        {
            OnClickCloseButton();
        }
    }

    void OnClickRaidButton()
    {
        GameMain.dungeonScreenEnabled = false;
        GameMain.combatScreenEnabled = true;
    }

    void OnClickCloseButton()
    {
        GameMain.dungeonScreenEnabled = false;
        GameMain.GUIEnabled = true;
    }

    void RaidDungeon()
    {
        int random = Random.Range(1,7);
        // Chance at gold reward
        // Chance at combat
        // Small chance of spawning an elite
    }
}