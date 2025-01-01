using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoGUI : MonoBehaviour
{
    public static bool updateInfoGUI = false;
    public static bool infoGUIHasBeenUpdated = false;
    public TMP_Text infoGUI_topText, infoGUI_middleText, infoGUI_bottomText;
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
        if (GameMain.GUIEnabled && BoardManager.villageNearby && !infoGUIHasBeenUpdated)
        {
            infoGUI.SetActive(true);
            switch (Villages.villageOwner)
            {
                case 1: infoGUI_topText.text = "Growth: " + Villages.playerOneVillageGrowth[Villages.currentVillage]; infoGUI_middleText.text = "Gold Per Turn: " + Villages.playerOneVillageGoldPerTurn[Villages.currentVillage]; infoGUI_bottomText.text = "Toll: " + Villages.playerOneVillageTolls[Villages.currentVillage]; break;
                case 2: infoGUI_topText.text = "Growth: " + Villages.playerTwoVillageGrowth[Villages.currentVillage]; infoGUI_middleText.text = "Gold Per Turn: " + Villages.playerTwoVillageGoldPerTurn[Villages.currentVillage]; infoGUI_bottomText.text = "Toll: " + Villages.playerTwoVillageTolls[Villages.currentVillage]; break;
                case 3: infoGUI_topText.text = "Growth: " + Villages.playerThreeVillageGrowth[Villages.currentVillage]; infoGUI_middleText.text = "Gold Per Turn: " + Villages.playerThreeVillageGoldPerTurn[Villages.currentVillage]; infoGUI_bottomText.text = "Toll: " + Villages.playerThreeVillageTolls[Villages.currentVillage]; break;
                case 4: infoGUI_topText.text = "Growth: " + Villages.playerFourVillageGrowth[Villages.currentVillage]; infoGUI_middleText.text = "Gold Per Turn: " + Villages.playerFourVillageGoldPerTurn[Villages.currentVillage]; infoGUI_bottomText.text = "Toll: " + Villages.playerFourVillageTolls[Villages.currentVillage]; break;
            }
            infoGUIHasBeenUpdated = true;
        }
    }
}