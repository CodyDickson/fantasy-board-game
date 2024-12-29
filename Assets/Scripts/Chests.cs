using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

public class Chests : MonoBehaviour
{
    [SerializeField] public TMP_Text chestReward;
    [SerializeField] public TMP_Text openChestText;
    [SerializeField] public TMP_Text continueText;
    [SerializeField] public Button openChestButton;
    [SerializeField] public Button continueButton;
    public static int updateRewardText = 0;
    public static bool rewardUpdated = false;
    public GUI gui;

    void Start()
    {
        openChestButton.onClick.AddListener(OnClickOpenChestButton);
        // continueButton.onClick.AddListener(OnClickContinueButton(gui));
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
            OnClickContinueButton(gui);
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
        rewardUpdated = true;
    }

    public static void OnClickContinueButton(GUI gui)
    {
        rewardUpdated = false;
        updateRewardText = 0;
        GameMain.chestScreenEnabled = false;
        GameMain.GUIEnabled = true;
        GUI.enablePrimaryButton = true;
    }
}
