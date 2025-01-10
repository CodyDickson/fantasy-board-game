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
    public static List<string> infoGUIPool_directions = new List<string>();
    // 
    public TMP_Text main_top, buttonText_top, buttonText_bottom, main_bottom;
    public Image avatar_top, avatar_bottom, buttonAvatar_top, buttonAvatar_bottom;
    public Button button_top, button_bottom;
    public GameObject infoGUI_top, infoGUI_bottom, button_top_object, button_bottom_object;

    void Start()
    {
        button_top.onClick.AddListener(OnClickTopButton);
        button_bottom.onClick.AddListener(OnClickBottomButton);
        infoGUI_top.gameObject.SetActive(false);
        infoGUI_bottom.gameObject.SetActive(false);
        avatar_top = avatar_top.gameObject.GetComponent<Image>();
        avatar_bottom = avatar_bottom.gameObject.GetComponent<Image>();
        buttonAvatar_top = button_top_object.gameObject.GetComponent<Image>();
        buttonAvatar_bottom = button_bottom_object.GetComponent<Image>();
    }

    void Update()
    {
        if (updateInfoGUI)
        {
            DeterminePoolContents();
            Debug.Log("Info GUI Pool: " + infoGUIPool[0] + " " + infoGUIPool[1]);
            infoGUI_bottom.SetActive(true);
            UpdateInfoGUI(infoGUIPool[0], infoGUIPool_directions[0], main_bottom, buttonText_bottom, avatar_bottom, buttonAvatar_bottom);
            if (infoGUIPool.Count > 1)
            {
                infoGUI_top.SetActive(true);
                UpdateInfoGUI(infoGUIPool[1], infoGUIPool_directions[1], main_top, buttonText_top, avatar_top, buttonAvatar_top);
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

    public static void OnClickTopButton()
    {
        PullFromInfoGUIPool(1);
    }

    public static void OnClickBottomButton()
    {
        PullFromInfoGUIPool(0);
    }

    public static void PullFromInfoGUIPool(int buttonClicked)
    {
        if (buttonClicked == 0)
        {
            switch (infoGUIPool[0])
            {
                case "empty": Villages.BuildVillage(infoGUIPool_directions[0]); infoGUIPool[0] = "village"; break;
                case "dungeon": Dungeons.RaidDungeon(); break;
                case "village": Villages.UpgradeVillage(); break;
                case "merchant": Merchants.OpenShop(); break;
            }
            updateInfoGUI = true;
        }
        if (buttonClicked == 1)
        {
            switch (infoGUIPool[1])
            {
                case "empty": Villages.BuildVillage(infoGUIPool_directions[1]); infoGUIPool[1] = "village"; break;
                case "dungeon": Dungeons.RaidDungeon(); break;
                case "village": Villages.UpgradeVillage(); break;
                case "merchant": Merchants.OpenShop(); break;
            }
            updateInfoGUI = true; 
        }
    }

    public static void DeterminePoolContents()
    {
        BoardManager.CheckForLocalStructures();
        if (BoardManager.northEmpty) { infoGUIPool.Add("empty"); infoGUIPool_directions.Add("north"); }
        if (BoardManager.eastEmpty) { infoGUIPool.Add("empty"); infoGUIPool_directions.Add("east"); }
        if (BoardManager.southEmpty) { infoGUIPool.Add("empty"); infoGUIPool_directions.Add("south"); }
        if (BoardManager.westEmpty) { infoGUIPool.Add("empty"); infoGUIPool_directions.Add("west"); }
        if (BoardManager.dungeonNorth) { infoGUIPool.Add("dungeon"); infoGUIPool_directions.Add("north"); Debug.Log("pass"); }
        if (BoardManager.dungeonEast) { infoGUIPool.Add("dungeon"); infoGUIPool_directions.Add("east"); Debug.Log("pass"); }
        if (BoardManager.dungeonSouth) { infoGUIPool.Add("dungeon"); infoGUIPool_directions.Add("south"); Debug.Log("pass"); }
        if (BoardManager.dungeonWest) { infoGUIPool.Add("dungeon"); infoGUIPool_directions.Add("west"); Debug.Log("pass"); }
        if (BoardManager.villageNorth) { infoGUIPool.Add("village"); infoGUIPool_directions.Add("north"); }
        if (BoardManager.villageEast) { infoGUIPool.Add("village"); infoGUIPool_directions.Add("east"); }
        if (BoardManager.villageSouth) { infoGUIPool.Add("village"); infoGUIPool_directions.Add("south"); }
        if (BoardManager.villageWest) { infoGUIPool.Add("village"); infoGUIPool_directions.Add("west"); }
        if (BoardManager.merchantNorth) { infoGUIPool.Add("merchant"); infoGUIPool_directions.Add("north"); }
        if (BoardManager.merchantEast) { infoGUIPool.Add("merchant"); infoGUIPool_directions.Add("east"); }
        if (BoardManager.merchantSouth) { infoGUIPool.Add("merchant"); infoGUIPool_directions.Add("south"); }
        if (BoardManager.merchantWest) { infoGUIPool.Add("merchant"); infoGUIPool_directions.Add("west"); }
    }

    public static void UpdateInfoGUI(string content, string direction, TMP_Text main, TMP_Text buttonText, Image avatar, Image buttonAvatar)
    {
        Debug.Log(content);
        if (content == "empty")
        {
            main.text = direction + "\nempty";
            avatar.sprite = Store.GUIElements[2];
            if (GameMain.playerGold >= Villages.villageBuildCost)
            {
                buttonText.text = "build";
                buttonAvatar.sprite = Store.GUIElements[1];
            }
            else
            {
                buttonText.text = "";
                buttonAvatar.sprite = Store.GUIElements[0];
            }
        }
        if (content == "dungeon")
        {
            main.text = direction + "\ndungeon";
            avatar.sprite = Store.dungeonSprites[0];
            buttonText.text = "Raid";
            buttonAvatar.sprite = Store.GUIElements[1];
        }
        if (content == "village")
        {
            main.text = direction + "\nvillage";
            avatar.sprite = Store.villageSprites[GameMain.playerVillage];
            if (GameMain.playerGold >= Villages.villageUpgradeCost)
            {
                buttonText.text = "Upgrade";
                buttonAvatar.sprite = Store.GUIElements[1];
            }
            else
            {
                buttonText.text = "";
                buttonAvatar.sprite = Store.GUIElements[0];
            }
        }
        if (content == "merchant")
        {
            main.text = direction + "\nmerchant";
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