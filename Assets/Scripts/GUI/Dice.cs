using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    // Dice //
    public static bool diceShouldFadeAway = false;
    public static bool diceShouldFadeAwayImmediately = false;
    public static bool diceShouldShow = false;
    public static int diceOneResult;
    public static int diceTwoResult;
    public static int diceThreeResult;
    public static bool diceOneShow = false;
    public static bool diceTwoShow = false;
    public static bool diceThreeShow = false;
    public static bool diceFourShow = false;
    public static bool diceFiveShow = false;
    public static bool diceSixShow = false;
    public GameObject diceOneObject, diceTwoObject, diceThreeObject;
    public Image diceOne, diceTwo, diceThree;
    private float tempCounter = 0f;
    private float counter = 1f;

    void Start()
    {
        diceOne = diceOne.gameObject.GetComponent<Image>();
        diceTwo = diceTwo.gameObject.GetComponent<Image>();
        diceThree = diceThree.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (diceShouldShow)
        {
            diceOneObject.SetActive(true);
            diceOne.sprite = Store.diceSprites[diceOneResult];
            if (GameMain.playerMovementDice > 1)
            {
                diceTwoObject.SetActive(true);
                diceTwo.sprite = Store.diceSprites[diceTwoResult];
            }
            if (GameMain.playerMovementDice > 2)
            {
                diceThreeObject.SetActive(true);
                diceThree.sprite = Store.diceSprites[diceThreeResult];
            }
            diceShouldShow = false;
        }
        if (diceShouldFadeAway)
        {
            if (tempCounter >= counter)
            {
                diceOneObject.SetActive(false);
                diceTwoObject.SetActive(false);
                diceThreeObject.SetActive(false);
                tempCounter = 0;
            }
            else
            {
                tempCounter += Time.deltaTime;
            }
        }
        if (diceShouldFadeAwayImmediately)
        {
            diceOneObject.SetActive(false);
            diceTwoObject.SetActive(false);
            diceThreeObject.SetActive(false);
            diceShouldFadeAwayImmediately = false;
            diceShouldFadeAway = false;
            diceShouldShow = false;
        }
    }

    public static int RollDice()
    {
        int total = 0;
        diceOneResult = Random.Range(1, 7);
        total += diceOneResult;
        if (GameMain.playerMovementDice > 1) { diceTwoResult = Random.Range(1, 7); total += diceTwoResult; }
        if (GameMain.playerMovementDice > 2) { diceThreeResult = Random.Range(1, 7); total += diceThreeResult; }
        return total;
    }

    public static void EnableDice()
    {
        diceShouldShow = true;
    }

    public static void DisableDice() { diceShouldFadeAway = true; }
    
    public static void DisableDiceImmediately() { diceShouldFadeAwayImmediately = true; }
}