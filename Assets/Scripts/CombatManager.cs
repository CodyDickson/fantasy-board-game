using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public Button continueButton;

    void Start()
    {
        continueButton.onClick.AddListener(OnClickContinueButton);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.C) && GameMain.combatScreenEnabled)
        {
            OnClickContinueButton();
        }
    }

    void OnClickContinueButton()
    {
        GameMain.combatScreenEnabled = false;
        GameMain.combatEncounterHappening = false;
        GameMain.GUIEnabled = true;
        GameMain.endTurnButtonEnabled = true;
    }

    public static int OpposingPlayerCombat(int opposingPlayer)
    {
        GameMain.combatScreenEnabled = true;
        GameMain.GUIEnabled = false;

        int victor = 0;
        int opposingPlayerDice = 1;
        if (opposingPlayer == 1)
        {
            opposingPlayerDice = GameMain.combatDicePlayer1;
        }
        else if (opposingPlayer == 2)
        {
            opposingPlayerDice = GameMain.combatDicePlayer2;
        }
        else if (opposingPlayer == 3)
        {
            opposingPlayerDice = GameMain.combatDicePlayer3;
        }
        else if (opposingPlayer == 4)
        {
            opposingPlayerDice = GameMain.combatDicePlayer4;
        }

        int opposingPlayerAmount = 0;
        int currentPlayerAmount = 0;

        for (int x = 1; x <= opposingPlayerDice; x++)
        {
            opposingPlayerAmount += Random.Range(1,6);
        }
        for (int y = 1; y <= GameMain.currentPlayerDice; y++)
        {
            currentPlayerAmount += Random.Range(1,6);
        }

        if (currentPlayerAmount > opposingPlayerAmount)
        {
            victor = 1;
        }
        else if (opposingPlayerAmount > currentPlayerAmount)
        {
            victor = 2;
        }
        else if (currentPlayerAmount == opposingPlayerAmount)
        {
            int random = Random.Range(1,2);
            if (random == 1)
            {
                victor = 1;
            }
            else if (random == 2)
            {
                victor = 2;
            }
        }
        return victor;
    }
}
