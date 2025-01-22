using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoGUI : MonoBehaviour
{
    //
    public static bool enableInfoGUI = false;
    public static bool disableInfoGUI = false;
    public static List<string> infoGUIPool = new List<string>();
    public static List<string> infoGUIPool_directions = new List<string>();
    public TMP_Text mainText, buttonText;
    public Image avatarImage, buttonImage;
    public Button mainButton;
    public GameObject infoGUIGameObject;
    //
    public static bool updateInfoGUI = false;
    public static bool finishUpdatingInfoGUI = false;
    //

    void Start()
    {
        mainButton.onClick.AddListener(OnClickButton);
        infoGUIGameObject.gameObject.SetActive(false);
        avatarImage = avatarImage.gameObject.GetComponent<Image>();
        buttonImage = buttonImage.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (enableInfoGUI)
        {
            // DeterminePoolContents();
            infoGUIGameObject.SetActive(true);
            UpdateInfoGUI(infoGUIPool[0], infoGUIPool_directions[0], mainText, buttonText, avatarImage, buttonImage);
            enableInfoGUI = false;
        }
        /*if (updateInfoGUI)
        {
            DeterminePoolContents();
            if (finishUpdatingInfoGUI)
            {
                infoGUI_bottom.SetActive(true);
                Debug.Log(infoGUIPool.Count);
                UpdateInfoGUI(infoGUIPool[0], infoGUIPool_directions[0], main_bottom, buttonText_bottom, avatar_bottom, buttonAvatar_bottom);
                if (infoGUIPool.Count > 1)
                {
                    infoGUI_top.SetActive(true);
                    UpdateInfoGUI(infoGUIPool[1], infoGUIPool_directions[1], main_top, buttonText_top, avatar_top, buttonAvatar_top);
                }
                updateInfoGUI = false;
                finishUpdatingInfoGUI = false;
            }
        }*/
        if (disableInfoGUI)
        {
            infoGUIGameObject.gameObject.SetActive(false);
            disableInfoGUI = false;
        }
    }
    public static void EnableInfoGUI() { enableInfoGUI = true; }

    public static void DisableInfoGUI() { disableInfoGUI = true; }

    public static void OnClickButton()
    {
        switch (infoGUIPool[0])
        {
            case "empty": Villages.BuildVillage(infoGUIPool_directions[0]); break;
            case "dungeon": Dungeons.RaidDungeon(); break;
            case "village": Villages.UpgradeVillage(); break;
            case "merchant": Merchants.OpenShop(); break;
        }
    }

    public static void DeterminePoolContents()
    {
        BoardManager.CheckForLocalStructures();
        infoGUIPool.Clear();
        infoGUIPool_directions.Clear();
        if (BoardManager.northEmpty) { infoGUIPool.Add("empty"); infoGUIPool_directions.Add("north"); }
        if (BoardManager.eastEmpty) { infoGUIPool.Add("empty"); infoGUIPool_directions.Add("east"); }
        if (BoardManager.southEmpty) { infoGUIPool.Add("empty"); infoGUIPool_directions.Add("south"); }
        if (BoardManager.westEmpty) { infoGUIPool.Add("empty"); infoGUIPool_directions.Add("west"); }
        if (BoardManager.villageNorth) { infoGUIPool.Add("village"); infoGUIPool_directions.Add("north"); }
        if (BoardManager.villageEast) { infoGUIPool.Add("village"); infoGUIPool_directions.Add("east"); }
        if (BoardManager.villageSouth) { infoGUIPool.Add("village"); infoGUIPool_directions.Add("south"); }
        if (BoardManager.villageWest) { infoGUIPool.Add("village"); infoGUIPool_directions.Add("west"); }
        if (BoardManager.dungeonNorth) { infoGUIPool.Add("dungeon"); infoGUIPool_directions.Add("north"); }
        if (BoardManager.dungeonEast) { infoGUIPool.Add("dungeon"); infoGUIPool_directions.Add("east"); }
        if (BoardManager.dungeonSouth) { infoGUIPool.Add("dungeon"); infoGUIPool_directions.Add("south"); }
        if (BoardManager.dungeonWest) { infoGUIPool.Add("dungeon"); infoGUIPool_directions.Add("west"); }
        if (BoardManager.merchantNorth) { infoGUIPool.Add("merchant"); infoGUIPool_directions.Add("north"); }
        if (BoardManager.merchantEast) { infoGUIPool.Add("merchant"); infoGUIPool_directions.Add("east"); }
        if (BoardManager.merchantSouth) { infoGUIPool.Add("merchant"); infoGUIPool_directions.Add("south"); }
        if (BoardManager.merchantWest) { infoGUIPool.Add("merchant"); infoGUIPool_directions.Add("west"); }
        finishUpdatingInfoGUI = true;
    }

    public static void UpdateInfoGUI(string content, string direction, TMP_Text main, TMP_Text buttonText, Image avatar, Image buttonAvatar)
    {
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
}