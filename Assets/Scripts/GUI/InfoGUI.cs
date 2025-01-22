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
    public static Vector3 structurePosition = new Vector3();
    public static string structureType;
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
            infoGUIGameObject.gameObject.SetActive(true);
            UpdateInfoGUI(structurePosition, structureType, mainText, buttonText, avatarImage, buttonImage);
            enableInfoGUI = false;
        }
        else if (disableInfoGUI)
        {
            infoGUIGameObject.gameObject.SetActive(false);
            disableInfoGUI = false;
        }
    }
    public static void EnableInfoGUI(Vector3 position, string type) { structurePosition = position; structureType = type; enableInfoGUI = true; }

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

    public static void UpdateInfoGUI(Vector3 position, string type, TMP_Text main, TMP_Text buttonText, Image avatar, Image buttonAvatar)
    {
        if (type == "empty")
        {
            avatar.sprite = Store.GUIElements[2];
            if (GameMain.playerGold >= Villages.villageBuildCost)
            {
                buttonText.text = "Build";
                buttonAvatar.sprite = Store.GUIElements[1];
            }
            else
            {
                buttonText.text = "";
                buttonAvatar.sprite = Store.GUIElements[0];
            }
        }
        if (type == "dungeon")
        {
            avatar.sprite = Store.dungeonSprites[0];
            buttonText.text = "Raid";
            buttonAvatar.sprite = Store.GUIElements[1];
        }
        if (type == "village")
        {
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
        if (type == "merchant")
        {
            avatar.sprite = Store.merchantSprites[0];
            buttonText.text = "Shop";
            buttonAvatar.sprite = Store.GUIElements[1];
        }
        if (type == "monster")
        {
            avatar.sprite = Store.merchantSprites[0];
            buttonText.text = "Shop";
            buttonAvatar.sprite = Store.GUIElements[1];
        }
        if (type == "player")
        {
            avatar.sprite = Store.merchantSprites[0];
            buttonText.text = "Shop";
            buttonAvatar.sprite = Store.GUIElements[1];
        }
    }
}