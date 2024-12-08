using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

public class ChestManager : MonoBehaviour
{
    [SerializeField] public TMP_Text chestReward;
    [SerializeField] public TMP_Text openChestText;
    [SerializeField] public TMP_Text continueText;
    [SerializeField] public Button openChestButton;
    [SerializeField] public Button continueButton;
    public static int updateRewardText = 0;
    public static bool rewardUpdated = false;

    void Start()
    {
        openChestButton.onClick.AddListener(OnClickOpenChestButton);
        continueButton.onClick.AddListener(OnClickContinueButton);
        continueButton.gameObject.SetActive(false);
        continueText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !rewardUpdated && GameMain.chestScreenEnabled)
        {
            OpenChest();
        }
        else if (Input.GetKeyDown(KeyCode.C) && rewardUpdated && GameMain.chestScreenEnabled)
        {
            OnClickContinueButton();
        }

        if (GameMain.chestScreenEnabled && !rewardUpdated)
        {
            continueButton.gameObject.SetActive(false);
            continueText.gameObject.SetActive(false);
            openChestButton.gameObject.SetActive(true);
            openChestText.gameObject.SetActive(true);
            chestReward.text = "???????";
        }
        else if (GameMain.chestScreenEnabled && rewardUpdated)
        {
            continueButton.gameObject.SetActive(true);
            continueText.gameObject.SetActive(true);
            openChestButton.gameObject.SetActive(false);
            openChestText.gameObject.SetActive(false);
        }

        if (GameMain.chestScreenEnabled && updateRewardText > 0)
        {
            if (updateRewardText == 1)
            {
                chestReward.text = "100 Gold!";
            }
            else if (updateRewardText == 2)
            {
                chestReward.text = "250 Gold!";
            }
            else if (updateRewardText == 3)
            {
                chestReward.text = "500 Gold!";
            }
            else if (updateRewardText == 4)
            {
                chestReward.text = "Extra Combat Dice!";
            }
            else if (updateRewardText == 5)
            {
                chestReward.text = "Extra Movement Dice!";
            }
            else if (updateRewardText == 6)
            {
                chestReward.text = "Extra Life!";
            }
        }
    }

    public static void OnClickOpenChestButton()
    {
        OpenChest();
    }

    public static void OpenChest()
    {
        int random = Random.Range(1,6);
        if (random == 1)
        {
            updateRewardText = 1;
            if (GameMain.currentPlayer == 1)
            {
                GameMain.playerOneGold += 100;
            }
            else if (GameMain.currentPlayer == 2)
            {
                GameMain.playerTwoGold += 100;
            }
            else if (GameMain.currentPlayer == 3)
            {
                GameMain.playerThreeGold += 100;
            }
            else if (GameMain.currentPlayer == 4)
            {
                GameMain.playerFourGold += 100;
            }
        }
        else if (random == 2)
        {
            updateRewardText = 2;
            if (GameMain.currentPlayer == 1)
            {
                GameMain.playerOneGold += 250;
            }
            else if (GameMain.currentPlayer == 2)
            {
                GameMain.playerTwoGold += 250;
            }
            else if (GameMain.currentPlayer == 3)
            {
                GameMain.playerThreeGold += 250;
            }
            else if (GameMain.currentPlayer == 4)
            {
                GameMain.playerFourGold += 250;
            }
        }
        else if (random == 3)
        {
            updateRewardText = 3;
            if (GameMain.currentPlayer == 1)
            {
                GameMain.playerOneGold += 500;
            }
            else if (GameMain.currentPlayer == 2)
            {
                GameMain.playerTwoGold += 500;
            }
            else if (GameMain.currentPlayer == 3)
            {
                GameMain.playerThreeGold += 500;
            }
            else if (GameMain.currentPlayer == 4)
            {
                GameMain.playerFourGold += 500;
            }
        }
        else if (random == 4)
        {
            if (GameMain.currentPlayer == 1 && GameMain.player_combatDice_one < 3)
            {
                updateRewardText = 4; 
                GameMain.player_combatDice_one += 1;
            }
            else if (GameMain.currentPlayer == 1 && GameMain.player_combatDice_one >= 3)
            {
                updateRewardText = 2;
                GameMain.playerOneGold += 250;
            }
            if (GameMain.currentPlayer == 2 && GameMain.player_combatDice_one < 3)
            {
                updateRewardText = 4;
                GameMain.player_combatDice_two += 1;
            }
            else if (GameMain.currentPlayer == 2 && GameMain.player_combatDice_two >= 3)
            {
                updateRewardText = 2;
                GameMain.playerTwoGold += 250;
            }
            if (GameMain.currentPlayer == 3 && GameMain.player_combatDice_three < 3)
            {
                updateRewardText = 4;
                GameMain.player_combatDice_three += 1;
            }
            else if (GameMain.currentPlayer == 3 && GameMain.player_combatDice_three >= 3)
            {
                updateRewardText = 2;
                GameMain.playerThreeGold += 250;
            }
            if (GameMain.currentPlayer == 4 && GameMain.player_combatDice_four < 3)
            {
                updateRewardText = 4;
                GameMain.player_combatDice_four += 1;
            }
            else if (GameMain.currentPlayer == 4 && GameMain.player_combatDice_four >= 3)
            {
                updateRewardText = 2;
                GameMain.playerFourGold += 250;
            }
        }
        else if (random == 5)
        {
            if (GameMain.currentPlayer == 1 && GameMain.player_moveDice_one < 3)
            {
                updateRewardText = 5; 
                GameMain.player_moveDice_one += 1;
            }
            else if (GameMain.currentPlayer == 1 && GameMain.player_moveDice_one >= 3)
            {
                updateRewardText = 2;
                GameMain.playerOneGold += 250;
            }
            if (GameMain.currentPlayer == 2 && GameMain.player_moveDice_one < 3)
            {
                updateRewardText = 5; 
                GameMain.player_moveDice_two += 1;
            }
            else if (GameMain.currentPlayer == 2 && GameMain.player_moveDice_two >= 3)
            {
                updateRewardText = 2;
                GameMain.playerTwoGold += 250;
            }
            if (GameMain.currentPlayer == 3 && GameMain.player_moveDice_three < 3)
            {
                updateRewardText = 5; 
                GameMain.player_moveDice_three += 1;
            }
            else if (GameMain.currentPlayer == 3 && GameMain.player_moveDice_three >= 3)
            {
                updateRewardText = 2;
                GameMain.playerThreeGold += 250;
            }
            if (GameMain.currentPlayer == 4 && GameMain.player_moveDice_four < 3)
            {
                updateRewardText = 5; 
                GameMain.player_moveDice_four += 1;
            }
            else if (GameMain.currentPlayer == 4 && GameMain.player_moveDice_four >= 3)
            {
                updateRewardText = 2;
                GameMain.playerFourGold += 250;
            }
        }
        else if (random == 6)
        {
            updateRewardText = 6;
            if (GameMain.currentPlayer == 1)
            {
                GameMain.livesPlayerOne += 1;
            }
            else if (GameMain.currentPlayer == 2)
            {
                GameMain.livesPlayerTwo += 1;
            }
            else if (GameMain.currentPlayer == 3)
            {
                GameMain.livesPlayerThree += 1;
            }
            else if (GameMain.currentPlayer == 4)
            {
                GameMain.livesPlayerFour += 1;
            }
        }
        rewardUpdated = true;
    }

    public static void OnClickContinueButton()
    {
        rewardUpdated = false;
        updateRewardText = 0;
        GameMain.chestScreenEnabled = false;
        GameMain.GUIEnabled = true;
        GameMain.secondaryButtonEnabled = true;
    }
}
