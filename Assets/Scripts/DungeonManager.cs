using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DungeonManager : MonoBehaviour
{
    public Button fightButton;
    public Button closeButton;

    void Start()
    {
        fightButton.onClick.AddListener(OnClickFightButton);
        closeButton.onClick.AddListener(OnClickCloseButton);
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
}