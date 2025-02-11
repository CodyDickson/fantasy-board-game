using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationGUI : MonoBehaviour
{
    public static bool enableConfirmationGUI = false;
    public static bool disableConfirmationGUI = false;
    public static bool disableNoButton = false;
    public TMP_Text mainText, buttonConfirmText, buttonNoText;
    public Button buttonConfirm, buttonNo;
    public Image buttonConfirmImage, buttonNoImage;
    public GameObject confirmationGUIGameObject;
    public static string confirmationContext;

    void Start()
    {
        buttonConfirm.onClick.AddListener(OnClickButtonConfirm);
        buttonNo.onClick.AddListener(OnClickButtonNo);
        confirmationGUIGameObject.gameObject.SetActive(false);
        buttonConfirmImage = buttonConfirmImage.gameObject.GetComponent<Image>();
        buttonNoImage = buttonNoImage.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (enableConfirmationGUI)
        {
            confirmationGUIGameObject.gameObject.SetActive(true);
            UpdateConfirmationGUI(confirmationContext, mainText, buttonConfirmImage, buttonNoImage);
            enableConfirmationGUI = false;
        }
        else if (disableConfirmationGUI)
        {
            confirmationGUIGameObject.gameObject.SetActive(false);
            disableConfirmationGUI = false;
        }
    }

    public static void UpdateConfirmationGUI(string context, TMP_Text mainText, Image confirmImage, Image noImage)
    {
        confirmImage.sprite = Store.GUIElements[1]; noImage.sprite = Store.GUIElements[1];
        switch (context)
        {
            case "exitingCamp": mainText.text = "Leave Camp?";  break;
            case "raidDungeon": mainText.text = "Raid Dungeon?"; break;
            case "payToll": mainText.text = "Pay Toll?"; break;
            case "attackMonster": mainText.text = "Attack Monster?"; break;
            case "usePotion": mainText.text = "Use Potion?"; break;
            case "openShop": mainText.text = "Visit Merchant?"; break;
            case "movePlayer": mainText.text = "Travel?"; break;
            default: Debug.Log("misfire UpdateConfirmationGUI"); break;
        }
    }

    public static void EnableConfirmationGUI(string context) { confirmationContext = context; enableConfirmationGUI = true; }

    public static void DisableConfirmationGUI() { disableConfirmationGUI = true; }

    void OnClickButtonConfirm()
    {
        switch (confirmationContext)
        {
            case "exitingCamp": PlayerMovement.PlayerExitingCamp(ClickEvents.permanentPosition); DisableConfirmationGUI(); break;
            case "raidDungeon": Dungeons.RaidDungeon(ClickEvents.permanentPosition); DisableConfirmationGUI(); break;
            case "payToll": break;
            case "attackMonster": break;
            case "usePotion": break;
            case "openShop": break;
            case "movePlayer": break;
            default: Debug.Log("misfire confirmationContext"); break;
        }
    }

    void OnClickButtonNo()
    {
        if (!disableNoButton)
        {
            DisableConfirmationGUI();
        }
    }
}
