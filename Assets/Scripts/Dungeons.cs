using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dungeons : MonoBehaviour
{
    public Button fightButton;
    public Button closeButton;
    public Button raidButton;

    void Start()
    {
        fightButton.onClick.AddListener(OnClickFightButton);
        closeButton.onClick.AddListener(OnClickCloseButton);
        raidButton.onClick.AddListener(OnClickRaidButton);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F) && GameMain.dungeonScreenEnabled)
        {
            OnClickFightButton();
        }
        if (Input.GetKey(KeyCode.C) && GameMain.dungeonScreenEnabled)
        {
            OnClickCloseButton();
        }
    }

    void OnClickFightButton()
    {
        GameMain.dungeonScreenEnabled = false;
        GameMain.combatScreenEnabled = true;
    }

    void OnClickCloseButton()
    {
        GameMain.dungeonScreenEnabled = false;
        GameMain.GUIEnabled = true;
    }

    void OnClickRaidButton()
    {

    }

    void RaidDungeon()
    {
        int random = Random.Range(1,7);
        // Chance at gold reward
        // Chance at combat
        // Small chance of spawning an elite
    }
}