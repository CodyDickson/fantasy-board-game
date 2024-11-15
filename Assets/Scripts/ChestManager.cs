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
                chestReward.text = "New Item!";
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
        if (random <= 3)
        {
            updateRewardText = 1;
            if (GameMain.currentPlayer == 1)
            {
                GameMain.goldPlayer1 += 100;
            }
            else if (GameMain.currentPlayer == 2)
            {
                GameMain.goldPlayer2 += 100;
            }
            else if (GameMain.currentPlayer == 3)
            {
                GameMain.goldPlayer3 += 100;
            }
            else if (GameMain.currentPlayer == 4)
            {
                GameMain.goldPlayer4 += 100;
            }
        }
        else if (random == 4)
        {
            updateRewardText = 2;
            if (GameMain.currentPlayer == 1)
            {
                GameMain.goldPlayer1 += 250;
            }
            else if (GameMain.currentPlayer == 2)
            {
                GameMain.goldPlayer2 += 250;
            }
            else if (GameMain.currentPlayer == 3)
            {
                GameMain.goldPlayer3 += 250;
            }
            else if (GameMain.currentPlayer == 4)
            {
                GameMain.goldPlayer4 += 250;
            }
        }
        else if (random == 5)
        {
            updateRewardText = 3;
            if (GameMain.currentPlayer == 1)
            {
                GameMain.goldPlayer1 += 500;
            }
            else if (GameMain.currentPlayer == 2)
            {
                GameMain.goldPlayer2 += 500;
            }
            else if (GameMain.currentPlayer == 3)
            {
                GameMain.goldPlayer3 += 500;
            }
            else if (GameMain.currentPlayer == 4)
            {
                GameMain.goldPlayer4 += 500;
            }
        }
        else if (random == 6)
        {
            updateRewardText = 4;
            string item1 = GameMain.GenerateItem();
            string item2 = GameMain.GenerateItem();
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
