using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoGUI : MonoBehaviour
{
    //
    public static bool updateInfoGUI = false;
    public static bool disableInfoGUI = false;
    //
    public static List<string> infoGUIPool = new List<string>();
    // 
    public TMP_Text main_top, buttonText_top, buttonText_bottom, main_bottom;
    public Image avatar_top, avatar_bottom, buttonAvatar_top, buttonAvatar_bottom;
    public Button button_top, button_bottom;
    public GameObject infoGUI_top, infoGUI_bottom;

    void Start()
    {
        infoGUI_top.gameObject.SetActive(false);
        infoGUI_bottom.gameObject.SetActive(false);
        Image avatar_top = GameObject.Find("AvatarTop").GetComponent<Image>();
        avatar_bottom = GameObject.Find("AvatarBottom").GetComponent<Image>();
        buttonAvatar_top = GameObject.Find("buttonAvatar_top").GetComponent<Image>();
        buttonAvatar_bottom = GameObject.Find("buttonAvatar_bottom").GetComponent<Image>();
    }

    void Update()
    {
        if (updateInfoGUI && GameMain.GUIEnabled)
        {
            DeterminePoolContents();
            infoGUI_bottom.SetActive(true);
            UpdateBottomInfoGUI(infoGUIPool[0], main_bottom, buttonText_bottom, avatar_bottom, buttonAvatar_bottom);
            if (infoGUIPool.Count > 1)
            {
                infoGUI_top.SetActive(true);
                UpdateTopInfoGUI(infoGUIPool[1], main_top, buttonText_top, avatar_top, buttonAvatar_top);
            }
            updateInfoGUI = false;
        }
        if (disableInfoGUI)
        {
            infoGUI_top.gameObject.SetActive(false);
            infoGUI_bottom.gameObject.SetActive(false);
            disableInfoGUI = false;
        }
    }

    public static void DeterminePoolContents()
    {
        BoardManager.CheckForLocalStructures();
        if (BoardManager.northEmpty) { infoGUIPool.Add("empty"); }
        if (BoardManager.eastEmpty) { infoGUIPool.Add("empty"); }
        if (BoardManager.southEmpty) { infoGUIPool.Add("empty"); }
        if (BoardManager.westEmpty) { infoGUIPool.Add("empty"); }
        if (BoardManager.dungeonNorth) { infoGUIPool.Add("dungeon"); }
        if (BoardManager.dungeonEast) { infoGUIPool.Add("dungeon"); }
        if (BoardManager.dungeonSouth) { infoGUIPool.Add("dungeon"); }
        if (BoardManager.dungeonWest) { infoGUIPool.Add("dungeon"); }
        if (BoardManager.villageNorth) { infoGUIPool.Add("village"); }
        if (BoardManager.villageEast) { infoGUIPool.Add("village"); }
        if (BoardManager.villageSouth) { infoGUIPool.Add("village"); }
        if (BoardManager.villageWest) { infoGUIPool.Add("village"); }
        if (BoardManager.merchantNorth) { infoGUIPool.Add("merchant"); }
        if (BoardManager.merchantEast) { infoGUIPool.Add("merchant"); }
        if (BoardManager.merchantSouth) { infoGUIPool.Add("merchant"); }
        if (BoardManager.merchantWest) { infoGUIPool.Add("merchant"); }
    }

    public static void UpdateTopInfoGUI(string content, TMP_Text main, TMP_Text buttonText, Image avatar, Image buttonAvatar)
    {
        if (content == "empty")
        {
            main.text = "Empty\nTile";
            avatar.sprite = Store.GUIElements[2];
            if (GameMain.playerGold[GameMain.currentPlayer] >= Villages.villageBuildCost)
            {
                buttonText.text = "Build";
                buttonAvatar.sprite = Store.GUIElements[1];
            }
            else
            {
                buttonAvatar.sprite = Store.GUIElements[0];
            }
        }
        if (content == "dungeon")
        {
            main.text = "Dungeon";
            avatar.sprite = Store.dungeonSprites[0];
            buttonText.text = "Raid";
            buttonAvatar.sprite = Store.GUIElements[1];
        }
        if (content == "village")
        {
            main.text = "Village";
            avatar.sprite = Store.villageSprites[0];
            buttonText.text = "Pay Toll";
            buttonAvatar.sprite = Store.GUIElements[1];
        }
        if (content == "merchant")
        {
            main.text = "Merchant";
            avatar.sprite = Store.merchantSprites[0];
            buttonText.text = "Shop";
            buttonAvatar.sprite = Store.GUIElements[1];
        }
    }

    public static void UpdateBottomInfoGUI(string content, TMP_Text main, TMP_Text buttonText, Image avatar, Image buttonAvatar)
    {
        if (content == "empty")
        {
            main.text = "Empty\nTile";
            avatar.sprite = Store.GUIElements[2];
            if (GameMain.playerGold[GameMain.currentPlayer] >= Villages.villageBuildCost)
            {
                buttonText.text = "Build";
                buttonAvatar.sprite = Store.GUIElements[1];
            }
            else
            {
                buttonAvatar.sprite = Store.GUIElements[0];
            }
        }
        if (content == "dungeon")
        {
            main.text = "Dungeon";
            avatar.sprite = Store.dungeonSprites[0];
            buttonText.text = "Raid";
            buttonAvatar.sprite = Store.GUIElements[1];
        }
        if (content == "village")
        {
            main.text = "Village";
            avatar.sprite = Store.villageSprites[0];
            buttonText.text = "Pay Toll";
            buttonAvatar.sprite = Store.GUIElements[1];
        }
        if (content == "merchant")
        {
            main.text = "Merchant";
            avatar.sprite = Store.merchantSprites[0];
            buttonText.text = "Shop";
            buttonAvatar.sprite = Store.GUIElements[1];
        }
    }

    public static void ToggleInfoGUI(bool status)
    {
        if (status == true)
        {
            updateInfoGUI = true;
        }
        else
        {
            disableInfoGUI = true;
        }
    }
}