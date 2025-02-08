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
        switch (context)
        {
            case "exitingCamp": mainText.text = "Leave camp?"; confirmImage.sprite = Store.GUIElements[1]; noImage.sprite = Store.GUIElements[1]; break;
        }
    }

    public static void EnableConfirmationGUI(string context) { confirmationContext = context; enableConfirmationGUI = true; }

    public static void DisableConfirmationGUI() { disableConfirmationGUI = true; }

    void OnClickButtonConfirm()
    {
        switch (confirmationContext)
        {
            case "exitingCamp": PlayerMovement.PlayerExitingCamp(ClickEvents.permanentPosition); DisableConfirmationGUI(); break;
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
