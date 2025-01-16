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
    public Button attackButton, healButton;
    public GameObject attackButtonPanel, healButtonPanel;
    public TMP_Text centerText;
    public static bool enableEndTurnButton = false;
    public static bool disableEndTurnButton = false;
    public static bool enableAttackButton = false;
    public static bool disableAttackButton = false;
    public static bool enableHealButton = false;
    public static bool disableHealButton = false;
    public static bool enableMoveButton = false;
    public static bool disableMoveButton = false;
    public bool GUIColorHasBeenUpdated = false;
    public static bool playerGUIHasBeenUpdated = false;
    public static bool clearCenterText = false;
    [SerializeField] public TMP_Text playerGUI_health, playerGUI_combat, playerGUI_gold, playerGUI_lives, playerGUI_armor;
    [SerializeField] public TMP_Text infoGUI_topText, infoGUI_middleText,infoGUI_bottomText;

    void Start()
    {
        endTurnButton.onClick.AddListener(OnClickEndTurn);
        moveButton.onClick.AddListener(OnClickMove);
        attackButton.onClick.AddListener(OnClickAttack);
        healButton.onClick.AddListener(OnClickHeal);
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
        /*if (Input.GetKeyDown(KeyCode.M) && endTurnButtonEnabled)
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
        }*/
        if (enableAttackButton)
        {
            attackButtonPanel.gameObject.SetActive(true);
            attackButton.gameObject.SetActive(true);
            enableAttackButton = false;
        }
        if (disableAttackButton)
        {
            attackButtonPanel.gameObject.SetActive(false);
            attackButton.gameObject.SetActive(false);
            disableAttackButton = false;
        }
        if (enableHealButton)
        {
            healButtonPanel.gameObject.SetActive(true);
            healButton.gameObject.SetActive(true);
            enableHealButton = false;
        }
        if (disableHealButton)
        {
            healButtonPanel.gameObject.SetActive(false);
            healButton.gameObject.SetActive(false);
            disableHealButton = false;
        }
        if (enableEndTurnButton)
        {
            endTurnButtonPanel.gameObject.SetActive(true);
            endTurnButton.gameObject.SetActive(true);
            enableEndTurnButton = false;
        }
        if (disableEndTurnButton)
        {
            endTurnButtonPanel.gameObject.SetActive(false);
            endTurnButton.gameObject.SetActive(false);
            disableEndTurnButton = false;
        }
        if (enableMoveButton)
        {
            moveButtonPanel.gameObject.SetActive(true);
            moveButton.gameObject.SetActive(true);
            enableMoveButton = false;
        }
        if (disableMoveButton)
        {
            moveButtonPanel.gameObject.SetActive(false);
            moveButton.gameObject.SetActive(false);
            disableMoveButton = false;
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

    public static void EnableMoveButton() { enableMoveButton = true; }

    public static void DisableMoveButton() { disableMoveButton = true; }

    public static void EnableEndTurnButton() { enableEndTurnButton = true; }

    public static void DisableEndTurnButton() { disableEndTurnButton = true; }

    public static void EnableAttackButton() { enableAttackButton = true; }

    public static void DisableAttackButton() { disableAttackButton = true; }

    public static void EnableHealButton() { enableHealButton = true; }

    public static void DisableHealButton() { disableHealButton = true; }

    void OnClickEndTurn()
    {
        DisableEndTurnButton();
        TurnManager.TurnProgressionHandler();
        InfoGUI.ToggleInfoGUI(false);
        TurnManager.EndPlayerTurn();
    }

    void OnClickMove()
    {
        DisableMoveButton();
        PlayerMovement.movesRemaining = Dice.RollDice();
        Dice.EnableDice();
        Arrows.EnableArrowButtons();
    }

    void OnClickAttack()
    {
        DisableAttackButton();
        DisableHealButton();
        DisableEndTurnButton();
        CombatManager.StartCombat();
    }

    void OnClickHeal()
    {

    }
}