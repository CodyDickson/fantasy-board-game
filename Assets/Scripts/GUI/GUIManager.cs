using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

public class GUIManager : MonoBehaviour
{
    public GameObject playerGUI;
    public Image playerGUI_Avatar;
    public Button endTurnButton;
    public GameObject endTurnButtonPanel;
    public Button moveButton;
    public GameObject moveButtonPanel;
    public Sprite playerRed, playerBlue;
    public TMP_Text centerText;
    public static bool enableEndTurnButton = false;
    public static bool endTurnButtonEnabled = false;
    public static bool enableMoveButton = false;
    public static bool moveButtonEnabled = false;
    public bool GUIColorHasBeenUpdated = false;
    public static bool playerGUIHasBeenUpdated = false;
    public static bool clearCenterText = false;
    [SerializeField] public TMP_Text playerGUI_health, playerGUI_combat, playerGUI_gold, playerGUI_lives, playerGUI_armor;
    [SerializeField] public TMP_Text infoGUI_topText, infoGUI_middleText,infoGUI_bottomText;

    void Start()
    {
        endTurnButton.onClick.AddListener(OnClickEndTurn);
        moveButton.onClick.AddListener(OnClickMove);
        endTurnButton.gameObject.SetActive(true);
        endTurnButtonPanel.gameObject.SetActive(true);
        moveButton.gameObject.SetActive(true);
        moveButtonPanel.gameObject.SetActive(true);
        playerGUI_Avatar = GetComponent<Image>();
        centerText.SetText("Choose your path...");
        Invoke("ClearCenterText", 1.5f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && endTurnButtonEnabled)
        {
            TurnManager.TurnProgressionHandler();
        }
        if (Input.GetKeyDown(KeyCode.N) && moveButtonEnabled)
        {
            bool onePath = BoardManager.OnlyOnePathPossible();
            if (!onePath)
            {
                BoardManager.CheckForLocalBoardPositions();
                Arrows.EnableArrowButtons();
            }
            else
            {
                PlayerMovement.playerIsMoving = true;
            }
        }
        if (enableMoveButton && !moveButtonEnabled)
        {
            moveButtonPanel.gameObject.SetActive(true);
            moveButton.gameObject.SetActive(true);
            moveButtonEnabled = true;
        }
        else if (!enableMoveButton && moveButtonEnabled)
        {
            //moveButtonPanel.gameObject.SetActive(false);
            //moveButton.gameObject.SetActive(false);
            moveButtonEnabled = false;
        }
        if (enableEndTurnButton && !endTurnButtonEnabled)
        {
            endTurnButtonPanel.gameObject.SetActive(true);
            endTurnButton.gameObject.SetActive(true);
            endTurnButtonEnabled = true;
        }
        else if (!enableEndTurnButton && endTurnButtonEnabled)
        {
            //endTurnButtonPanel.gameObject.SetActive(false);
            //endTurnButton.gameObject.SetActive(false);
            endTurnButtonEnabled = false;
        }
        if (GameMain.GUIEnabled && !playerGUIHasBeenUpdated)
        {
            playerGUI.SetActive(true);
            playerGUI_health.text = "Health: " + GameMain.playerHealth;
            playerGUI_gold.text = "Gold: " + GameMain.playerGold;
            playerGUI_combat.text = "Combat: " + GameMain.playerCombat;
            playerGUI_lives.text = "Lives: " + GameMain.playerLives;
            playerGUI_armor.text = "Armor: " + GameMain.playerArmor;
            if (UpdatePlayerGUIAvatar.playerGUIAvatarHasBeenUpdated == false) { UpdatePlayerGUIAvatar.updatePlayerGUIAvatar = true; };
            playerGUIHasBeenUpdated = true;
        }
        if (!GameMain.GUIEnabled)
        {
            playerGUI.SetActive(false);
        }
        if (clearCenterText)
        {
            centerText.SetText("");
            clearCenterText = false;
        }
    }

    public static void UpdatePlayerGUI()
    {
        playerGUIHasBeenUpdated = false;
    }

    public void ClearCenterText()
    {
        clearCenterText = true;
    }

    public static void ToggleEndTurnButton(bool enable)
    {
        enableEndTurnButton = enable;
    }

    public static void ToggleMoveButton(bool enable)
    {
        enableMoveButton = enable;
    }

    void OnClickEndTurn()
    {
        if (endTurnButtonEnabled)
        {
            TurnManager.TurnProgressionHandler();
            InfoGUI.ToggleInfoGUI(false);
            ToggleEndTurnButton(false);
            TurnManager.EndPlayerTurn();
        }
    }

    void OnClickMove()
    {
        if (moveButtonEnabled)
        {
            ToggleMoveButton(false);
            PlayerMovement.movesRemaining = Dice.RollDice();
            Dice.EnableDice();
            Arrows.EnableArrowButtons();
        }
    }
}