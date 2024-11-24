using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public Button continueButton;
    public Button fightButton;
    public GameObject diceOne;
    public GameObject diceTwo;
    public GameObject diceThree;
    public GameObject diceFour;
    public GameObject diceFive;
    public GameObject diceSix;
    public static bool fightScreen = true;
    public static float tempCounter = 0f;
    public static float counter = 0.5f;

    void Start()
    {
        continueButton.onClick.AddListener(OnClickContinueButton);
        continueButton.onClick.AddListener(OnClickFightButton);
        continueButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.C) && GameMain.combatScreenEnabled)
        {
            OnClickContinueButton();
        }
        if (Input.GetKey(KeyCode.F) && GameMain.combatScreenEnabled)
        {
            OnClickFightButton();
        }

        if (fightScreen)
        {
            if (tempCounter <= 0f)
            {
                int result = Random.Range(1,7);
                diceOne.SetActive(false);
                diceTwo.SetActive(false);
                diceThree.SetActive(false);
                diceFour.SetActive(false);
                diceFive.SetActive(false);
                diceSix.SetActive(false);
                switch (result)
                {
                    case 1: diceOne.SetActive(true); break;
                    case 2: diceTwo.SetActive(true); break;
                    case 3: diceThree.SetActive(true); break;
                    case 4: diceFour.SetActive(true); break;
                    case 5: diceFive.SetActive(true); break;
                    case 6: diceSix.SetActive(true); break;
                }
                tempCounter = counter;
            }
            else
            {
                tempCounter -= Time.deltaTime;
            }
        }
    }

    void OnClickContinueButton()
    {
        GameMain.combatScreenEnabled = false;
        GameMain.combatEncounterHappening = false;
        GameMain.GUIEnabled = true;
        GameMain.endTurnButtonEnabled = true;
    }

    void OnClickFightButton()
    {
        fightButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
    }
}
