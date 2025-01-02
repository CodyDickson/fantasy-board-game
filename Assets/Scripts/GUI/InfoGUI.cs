using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoGUI : MonoBehaviour
{
    public static bool updateInfoGUI = false;
    public static bool disableInfoGUI = false;
    public static bool infoGUIHasBeenUpdated = false;
    public TMP_Text top, middle, bottom;
    public Image infoGUI_avatar;
    public GameObject infoGUI;

    void Start()
    {
        infoGUI.gameObject.SetActive(false);
    }

    void Update()
    {
        /*if (updateInfoGUI)
        {
            switch (GameMain.currentPlayerAvatar)
            {
                case 0: gameObject.GetComponent<Image>().sprite = Store.playerSprites[0]; break;
                case 1: gameObject.GetComponent<Image>().sprite = Store.playerSprites[1]; break;
            }
            updateInfoGUI = false;
            infoGUIHasBeenUpdated = true;
        }*/
        if (!GameMain.GUIEnabled)
        {
            infoGUI.SetActive(false);
        }
        if (updateInfoGUI)
        {
            infoGUI.SetActive(true);
            gameObject.GetComponent<Image>().sprite = null;
            if (GameMain.GUIEnabled && BoardManager.villageNearby)
            {
                switch (Villages.villageOwner)
                {
                    case 1: top.text = "Growth: " + Villages.playerOneVillageGrowth[Villages.currentVillage]; middle.text = "Gold Per Turn: " + Villages.playerOneVillageGoldPerTurn[Villages.currentVillage]; bottom.text = "Toll: " + Villages.playerOneVillageTolls[Villages.currentVillage]; break;
                    case 2: top.text = "Growth: " + Villages.playerTwoVillageGrowth[Villages.currentVillage]; middle.text = "Gold Per Turn: " + Villages.playerTwoVillageGoldPerTurn[Villages.currentVillage]; bottom.text = "Toll: " + Villages.playerTwoVillageTolls[Villages.currentVillage]; break;
                    case 3: top.text = "Growth: " + Villages.playerThreeVillageGrowth[Villages.currentVillage]; middle.text = "Gold Per Turn: " + Villages.playerThreeVillageGoldPerTurn[Villages.currentVillage]; bottom.text = "Toll: " + Villages.playerThreeVillageTolls[Villages.currentVillage]; break;
                    case 4: top.text = "Growth: " + Villages.playerFourVillageGrowth[Villages.currentVillage]; middle.text = "Gold Per Turn: " + Villages.playerFourVillageGoldPerTurn[Villages.currentVillage]; bottom.text = "Toll: " + Villages.playerFourVillageTolls[Villages.currentVillage]; break;
                }
                updateInfoGUI = false;
            }
            if (GameMain.GUIEnabled && !BoardManager.villageNearby && !BoardManager.dungeonNearby && !BoardManager.merchantNearby)
            {
                ClearInfoGUI(top, middle, bottom);
                middle.text = "There's nothing here.";
                updateInfoGUI = false;
            }
        }
    }

    public static void ClearInfoGUI(TMP_Text top, TMP_Text middle, TMP_Text bottom)
    {
        top.text = "";
        middle.text = "";
        bottom.text = "";
    }

    public static void ToggleInfoGUI(bool status)
    {
        updateInfoGUI = status;
    }
}