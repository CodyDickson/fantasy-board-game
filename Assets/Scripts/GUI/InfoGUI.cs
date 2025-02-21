using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoGUI : MonoBehaviour
{
    public static bool enableInfoGUI = false;
    public static bool disableInfoGUI = false;
    public static List<string> infoGUIPool = new List<string>();
    public static List<string> infoGUIPool_directions = new List<string>();
    public TMP_Text mainText, buttonText;
    public Image avatarImage, buttonImage;
    public Button mainButton;
    public GameObject infoGUIGameObject;
    public static Vector3 structurePosition = new Vector3();
    public static string objectClicked;
    //
    public static bool updateInfoGUI = false;
    public static bool finishUpdatingInfoGUI = false;
    public static bool movesAreShowing = false;
    public static bool withinInteractionRange = false;
    public static bool buttonEnabled;
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
            UpdateInfoGUI(structurePosition, objectClicked, mainText, buttonText, avatarImage, buttonImage, withinInteractionRange);
            enableInfoGUI = false;
        }
        else if (disableInfoGUI)
        {
            infoGUIGameObject.gameObject.SetActive(false);
            disableInfoGUI = false;
        }
    }

    public static void EnableInfoGUI(Vector3 position, string type, bool interactable) { structurePosition = position; objectClicked = type; withinInteractionRange = interactable; enableInfoGUI = true; }

    public static void DisableInfoGUI() { disableInfoGUI = true; }

    public static void OnClickButton()
    {
        if (buttonEnabled)
        {
            switch (objectClicked)
            {
                case "empty": Debug.Log("Building Village"); Villages.BuildVillage(structurePosition); break;
                case "dungeon": Dungeons.RaidDungeon(structurePosition); break;
                case "village": Villages.UpgradeVillage(); break;
                case "merchant": Merchants.OpenShop(); break;
                case "player": Arrows.EnableArrowButtons(); PlayerMovement.movesRemaining = Dice.RollDice(); Dice.EnableDice(); break;
                case "monster": Monsters.PlayerAttackedMonster(structurePosition); break;
                default: Debug.Log("should never show"); break;
            }
            buttonEnabled = false;
            // EnableInfoGUI();
        }
    }

    public static void UpdateInfoGUI(Vector3 position, string type, TMP_Text main, TMP_Text buttonText, Image avatar, Image buttonAvatar, bool withinInteractionRange)
    {
        if (type == "empty")
        {
            Debug.Log("Empty Slot");
            avatar.sprite = Store.GUIElements[2];
            main.text = "";
            if (Player.gold >= Villages.villageBuildCost && withinInteractionRange)
            {
                buttonText.text = "Build";
                buttonAvatar.sprite = Store.GUIElements[1];
                buttonEnabled = true;
            }
            else if (Player.gold < Villages.villageBuildCost && withinInteractionRange)
            {
                buttonText.text = "Not Enough Gold";
                buttonAvatar.sprite = Store.GUIElements[0];
            }
            else
            {
                buttonText.text = "Out of Range";
                buttonAvatar.sprite = Store.GUIElements[0];
            }
        }
        if (type == "dungeon")
        {
            avatar.sprite = Store.dungeonSprites[0];
            int dungeonID = Dungeons.FindCurrentDungeon(position);
            int[] ints = Dungeons.activeDungeons[dungeonID];
            string monsterName = Monsters.MonsterNames(ints[0]);
            main.text = monsterName + " Dungeon\n Health: " + ints[1] + "\nStatus: " + ints[2];
            if (withinInteractionRange)
            {
                buttonText.text = "Raid";
                buttonAvatar.sprite = Store.GUIElements[1];
                buttonEnabled = true;
            }
            else
            {
                buttonText.text = "Out of Range";
                buttonAvatar.sprite = Store.GUIElements[0];
            }
        }
        if (type == "village")
        {
            avatar.sprite = Store.villageSprites[Player.village];
            // Villages.FindCurrentVillage(position);
            // Text needs to update to show the Village Level, Gold Per Turn
            main.text = "";
            if (Player.gold >= Villages.villageUpgradeCost && withinInteractionRange)
            {
                buttonText.text = "Upgrade";
                buttonAvatar.sprite = Store.GUIElements[1];
                buttonEnabled = true;
            }
            else if (Player.gold < Villages.villageUpgradeCost && withinInteractionRange)
            {
                buttonText.text = "Not Enough Gold";
                buttonAvatar.sprite = Store.GUIElements[0];
            }
            else
            {
                buttonText.text = "Out of Range";
                buttonAvatar.sprite = Store.GUIElements[0];
            }
        }
        if (type == "toll")
        {
            avatar.sprite = Store.objectSprites[0];
            main.text = "Tollmaster";
            if (Player.gold >= Tollmasters.tollCost && withinInteractionRange)
            {
                buttonText.text = "Pay Toll";
                buttonAvatar.sprite = Store.GUIElements[1];
                buttonEnabled = true;
            }
            else if (Player.gold < Tollmasters.tollCost && withinInteractionRange)
            {
                buttonText.text = "Not Enough Gold";
                buttonAvatar.sprite = Store.GUIElements[0];
            }
            else
            {
                buttonText.text = "Out of Range";
                buttonAvatar.sprite = Store.GUIElements[0];
            }
        }
        if (type == "merchant")
        {
            avatar.sprite = Store.merchantSprites[0];
            buttonText.text = "Shop";
            main.text = "";
            buttonAvatar.sprite = Store.GUIElements[1];
        }
        if (type == "monster")
        {
            int[] ints;
            int monsterType = 0;
            string monsterName = "---";
            string monsterDescription = "---";
            int monsterHealth = 1;
            int monsterLives = 1;
            int monsterCombat = 1;
            // get monster type
            foreach (Vector3 monsterPosition in Monsters.monsterPositions)
            {
                if (monsterPosition == position) {
                    int monsterID = Monsters.monsterPositions.IndexOf(monsterPosition);
                    ints = Monsters.activeMonsters[monsterID];
                    monsterType = ints[0]; monsterHealth = ints[1]; monsterLives = ints[2]; monsterCombat = ints[3];}
            }
            avatar.sprite = Store.monsterSprites[monsterType];
            if (withinInteractionRange)
            {
                buttonText.text = "Attack";
                buttonAvatar.sprite = Store.GUIElements[1];
                buttonEnabled = true;
            }
            else
            {
                buttonText.text = "Out of Range";
                buttonAvatar.sprite = Store.GUIElements[0];
            }
            monsterDescription = Monsters.MonsterDescriptions(monsterType);
            monsterName = Monsters.MonsterNames(monsterType);
            main.text = monsterName + "\nHealth: " + monsterHealth + "\nLives: " + monsterLives + "\nCombat: " + monsterCombat + "\n" + monsterDescription;
        }
        if (type == "player")
        {
            avatar.sprite = Store.playerSprites[Player.avatar];
            main.text = "the " + Player.playerClass;
            buttonText.text = "";
            buttonAvatar.sprite = Store.GUIElements[1];
        }
    }
}