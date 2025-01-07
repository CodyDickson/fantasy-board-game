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
    public World world;
    public Sprite playerRed, playerBlue;
    public TMP_Text centerText;
    public static bool enableEndTurnButton = false;
    public static bool endTurnButtonEnabled = false;
    public static bool enableMoveButton = false;
    public static bool moveButtonEnabled = false;
    public bool GUIColorHasBeenUpdated = false;
    public bool playerGUIHasBeenUpdated = false;
    public static bool clearCenterText = false;
    [SerializeField] public Tile player;
    [SerializeField] public Tile player_red;
    [SerializeField] public Tile player_blue;
    [SerializeField] public Tile player_green;
    [SerializeField] public Tile player_purple;
    [SerializeField] public Tile player_white;
    [SerializeField] public Tile villageRed;
    [SerializeField] public Tile villageBlue;
    [SerializeField] public Tile villageGreen;
    [SerializeField] public Tile villagePurple;
    [SerializeField] public Tile villageWhite;
    [SerializeField] public Tile monster;
    [SerializeField] public Tile monsterImp;
    [SerializeField] public Tile monsterBasilisk;
    [SerializeField] public Tile dungeon;
    [SerializeField] public Tilemap tilemap;
    [SerializeField] public Tilemap tilemapStructures;
    [SerializeField] public Tilemap tilemapUnits;
    [SerializeField] public TMP_Text playerGUI_health, playerGUI_combat, playerGUI_gold, playerGUI_lives, playerGUI_armor;
    [SerializeField] public TMP_Text infoGUI_topText, infoGUI_middleText,infoGUI_bottomText;

    void Start()
    {
        endTurnButton.onClick.AddListener(OnClickEndTurn);
        moveButton.onClick.AddListener(OnClickMove);
        endTurnButton.gameObject.SetActive(false);
        endTurnButtonPanel.gameObject.SetActive(false);
        moveButton.gameObject.SetActive(false);
        moveButtonPanel.gameObject.SetActive(false);
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
            moveButtonPanel.gameObject.SetActive(false);
            moveButton.gameObject.SetActive(false);
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
            endTurnButtonPanel.gameObject.SetActive(false);
            endTurnButton.gameObject.SetActive(false);
            endTurnButtonEnabled = false;
        }
        if (GameMain.GUIEnabled && !playerGUIHasBeenUpdated)
        {
            playerGUI.SetActive(true);
            playerGUI_health.text = "Health: " + GameMain.player_health[GameMain.currentHumanPlayer];
            playerGUI_gold.text = "Gold: " + GameMain.playerGold[GameMain.currentHumanPlayer];
            playerGUI_combat.text = "Combat: " + GameMain.playerCombat[GameMain.currentHumanPlayer];
            playerGUI_lives.text = "Lives: " + GameMain.player_lives[GameMain.currentHumanPlayer];
            playerGUI_armor.text = "Armor: " + GameMain.player_armor[GameMain.currentHumanPlayer];
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
            enableEndTurnButton = false;
        }
        else
        {
            Debug.Log("End turn is currently disabled");
        }
    }

    void OnClickMove()
    {
        PlayerMovement.movesRemaining = Dice.RollDice();
        Dice.EnableDice();
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
}