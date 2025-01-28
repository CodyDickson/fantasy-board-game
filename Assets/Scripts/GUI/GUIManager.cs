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
    public TMP_Text centerText, moveButtonText;
    public Image image_endTurn, image_move, image_attack, image_heal;
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

    void Start()
    {
        endTurnButton.onClick.AddListener(OnClickEndTurn);
        moveButton.onClick.AddListener(OnClickMove);
        attackButton.onClick.AddListener(OnClickAttack);
        healButton.onClick.AddListener(OnClickHeal);
        image_endTurn = image_endTurn.gameObject.GetComponent<Image>();
        image_move = image_move.gameObject.GetComponent<Image>();
        image_attack = image_attack.gameObject.GetComponent<Image>();
        image_heal = image_heal.gameObject.GetComponent<Image>();
        moveButtonPanel.gameObject.SetActive(true);
        playerGUI_Avatar = GetComponent<Image>();
        centerText.SetText("Choose your path...");
        Invoke("ClearCenterText", 1.5f);
        DisableEndTurnButton();
        DisableAttackButton();
        DisableHealButton();
        EnableMoveButton();
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
            image_attack.sprite = Store.GUIElements[1];
            // attackButtonPanel.gameObject.SetActive(true);
            attackButton.gameObject.SetActive(true);
            enableAttackButton = false;
        }
        if (disableAttackButton)
        {
            image_attack.sprite = Store.GUIElements[0];
            // attackButtonPanel.gameObject.SetActive(false);
            attackButton.gameObject.SetActive(false);
            disableAttackButton = false;
        }
        if (enableHealButton && Player.totalPotions > 0)
        {
            image_heal.sprite = Store.GUIElements[1];
            // healButtonPanel.gameObject.SetActive(true);
            healButton.gameObject.SetActive(true);
            enableHealButton = false;
        }
        if (disableHealButton)
        {
            image_heal.sprite = Store.GUIElements[0];
            // healButtonPanel.gameObject.SetActive(false);
            healButton.gameObject.SetActive(false);
            disableHealButton = false;
        }
        if (enableEndTurnButton)
        {
            image_endTurn.sprite = Store.GUIElements[1];
            // endTurnButtonPanel.gameObject.SetActive(true);
            endTurnButton.gameObject.SetActive(true);
            enableEndTurnButton = false;
        }
        if (disableEndTurnButton)
        {
            image_endTurn.sprite = Store.GUIElements[0];
            // endTurnButtonPanel.gameObject.SetActive(false);
            endTurnButton.gameObject.SetActive(false);
            disableEndTurnButton = false;
        }
        if (enableMoveButton)
        {
            image_move.sprite = Store.GUIElements[1];
            if (GameMain.playerInCamp)
            {
                moveButtonText.text = "Exit Camp";
            }
            else
            {
                moveButtonText.text = "Travel";
            }
            //moveButtonPanel.gameObject.SetActive(true);
            moveButton.gameObject.SetActive(true);
            enableMoveButton = false;
        }
        if (disableMoveButton)
        {
            image_move.sprite = Store.GUIElements[0];
            //moveButtonPanel.gameObject.SetActive(false);
            moveButton.gameObject.SetActive(false);
            disableMoveButton = false;
        }
        if (GameMain.GUIEnabled && !playerGUIHasBeenUpdated)
        {
            playerGUI.SetActive(true);
            playerGUI_health.text = "Health: " + Player.health;
            playerGUI_gold.text = "Gold: " + Player.gold;
            playerGUI_combat.text = "Combat: " + Player.combat;
            playerGUI_lives.text = "Lives: " + Player.lives;
            playerGUI_armor.text = "Armor: " + Player.armor;
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
        UpdatePlayerGUI();
        InfoGUI.DisableInfoGUI();
        CombatManager.ClearPotentialAttacks();
        BoardManager.ClearEmptySlots();
        Player.gold += Villages.totalVillageGoldPerTurn;
        TurnManager.TurnProgressionHandler();
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
        if (!CombatManager.combatEnabled)
        {
            DisableHealButton();
            DisableEndTurnButton();
            CombatManager.StartCombat();
        }
        else if (CombatManager.combatEnabled)
        {
            EnableHealButton();
            EnableEndTurnButton();
            CombatManager.StopCombat();
        }
    }

    void OnClickHeal()
    {
        Player.totalPotions -= 1;
        Player.health += 3;
        if (Player.totalPotions == 0)
        {
            DisableHealButton();
        }
    }
}