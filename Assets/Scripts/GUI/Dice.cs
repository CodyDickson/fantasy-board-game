using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Dice : MonoBehaviour
{
    // Dice //
    public static bool diceShouldFadeAway = false;
    public static bool diceShouldFadeAwayImmediately = false;
    public static bool diceShouldShow = false;
    public static bool diceOneShow = false;
    public static bool diceTwoShow = false;
    public static bool diceThreeShow = false;
    public static bool diceFourShow = false;
    public static bool diceFiveShow = false;
    public static bool diceSixShow = false;
    public GameObject diceOne;
    public GameObject diceTwo;
    public GameObject diceThree;
    public GameObject diceFour;
    public GameObject diceFive;
    public GameObject diceSix;
    private float tempCounter = 0f;
    private float counter = 0.5f;

    void Start()
    {
        
    }

    void Update()
    {
        if (diceShouldShow)
        {
            if (diceOneShow)
            {
                diceOne.SetActive(true);
                diceOneShow = false;
            }
            else if (diceTwoShow)
            {
                diceTwo.SetActive(true);
                diceTwoShow = false;
            }
            else if (diceThreeShow)
            {
                diceThree.SetActive(true);
                diceThreeShow = false;
            }
            else if (diceFourShow)
            {
                diceFour.SetActive(true);
                diceFourShow = false;
            }
            else if (diceFiveShow)
            {
                diceFive.SetActive(true);
                diceFiveShow = false;
            }
            else if (diceSixShow)
            {
                diceSix.SetActive(true);
                diceSixShow = false;
            }
            diceShouldShow = false;
            diceShouldFadeAway = true;
        }
        if (diceShouldFadeAway)
        {
            if (tempCounter >= counter)
            {
                diceOne.SetActive(false);
                diceTwo.SetActive(false);
                diceThree.SetActive(false);
                diceFour.SetActive(false);
                diceFive.SetActive(false);
                diceSix.SetActive(false);
                diceShouldFadeAway = false;
                tempCounter = 0;
            }
            else
            {
                tempCounter += Time.deltaTime;
            }
        }
        if (diceShouldFadeAwayImmediately)
        {
            diceOne.SetActive(false);
            diceTwo.SetActive(false);
            diceThree.SetActive(false);
            diceFour.SetActive(false);
            diceFive.SetActive(false);
            diceSix.SetActive(false);
            diceShouldFadeAwayImmediately = false;
            diceShouldFadeAway = false;
            diceShouldShow = false;
        }
    }

    public static int RollDice()
    {
        int total = 0;
        int diceOneResult = Random.Range(1, 7);
        total += diceOneResult;
        if (GameMain.playerMovementDice[GameMain.currentPlayer] > 1) { int diceTwoResult = Random.Range(1, 7); total += diceTwoResult; }
        if (GameMain.playerMovementDice[GameMain.currentPlayer] > 2) { int diceThreeResult = Random.Range(1, 7); total += diceThreeResult; }
        return total;
    }

    public static void EnableDice()
    {
        diceShouldShow = true;
    }
}
