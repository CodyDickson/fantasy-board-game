using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

public class GUI : MonoBehaviour
{
    public GameObject playerGUI;
    public Image playerGUI_Avatar;
    public TMP_Text primaryButtonText;
    public Button primaryButton;
    public GameObject primaryButtonPanel;
    public Button secondaryButton;
    public TMP_Text secondaryButtonText;
    public GameObject secondaryButtonPanel;
    public Button endTurnButton;
    public GameObject endTurnButtonPanel;
    public Button moveButton;
    public GameObject moveButtonPanel;
    public Button arrowUpButton;
    public Button arrowRightButton;
    public Button arrowDownButton;
    public Button arrowLeftButton;
    public World world;
    public Sprite playerRed, playerBlue;
    public TMP_Text centerText;
    public static bool enablePrimaryButton = false;
    public static bool primaryButtonEnabled = false;
    public static string primaryButtonAssignedTo = "";
    public static bool enableSecondaryButton = false;
    public static bool secondaryButtonEnabled = false;
    public static string secondaryButtonAssignedTo = "";
    public static bool enableEndTurnButton = false;
    public static bool endTurnButtonEnabled = false;
    public static bool enableMoveButton = false;
    public static bool moveButtonEnabled = false;
    public static bool enableArrowButtons = false;
    public static bool arrowButtonsEnabled = false;
    public static bool rightArrowButtonEnabled = false;
    public static bool leftArrowButtonEnabled = false;
    public static bool upArrowButtonEnabled = false;
    public static bool downArrowButtonEnabled = false;
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
        primaryButton.onClick.AddListener(OnClickPrimaryButton);
        secondaryButton.onClick.AddListener(OnClickSecondaryButton);
        endTurnButton.onClick.AddListener(OnClickEndTurn);
        arrowUpButton.onClick.AddListener(OnClickUpArrow);
        arrowRightButton.onClick.AddListener(OnClickRightArrow);
        arrowDownButton.onClick.AddListener(OnClickDownArrow);
        arrowLeftButton.onClick.AddListener(OnClickLeftArrow);
        endTurnButton.gameObject.SetActive(false);
        endTurnButtonPanel.gameObject.SetActive(false);
        secondaryButton.gameObject.SetActive(false);
        secondaryButtonPanel.gameObject.SetActive(false);
        primaryButton.gameObject.SetActive(false);
        primaryButtonPanel.gameObject.SetActive(false);
        moveButton.gameObject.SetActive(false);
        moveButtonPanel.gameObject.SetActive(false);
        arrowUpButton.gameObject.SetActive(false);
        arrowRightButton.gameObject.SetActive(false);
        arrowDownButton.gameObject.SetActive(false);
        arrowLeftButton.gameObject.SetActive(false);
        playerGUI_Avatar = GetComponent<Image>();
        centerText.SetText("Choose your path...");
        if (GameMain.currentPlayerInCamp)
        {
            enableArrowButtons = true;
            arrowUpButton.gameObject.SetActive(true);
            arrowRightButton.gameObject.SetActive(true);
            arrowDownButton.gameObject.SetActive(true);
            arrowLeftButton.gameObject.SetActive(true);
        }
        Invoke("ClearCenterText(clearCenterText)", 1.5f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && primaryButtonEnabled)
        {
            if (primaryButtonAssignedTo == "move")
            {
                World.MoveUnit();
                enableArrowButtons = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z) && secondaryButtonEnabled)
        {
            if (secondaryButtonAssignedTo == "build")
            {
                Villages.BuildVillage(tilemapStructures, villageRed, villageBlue, villageGreen, villagePurple, villageWhite);
            }
        }
        if (Input.GetKeyDown(KeyCode.M) && endTurnButtonEnabled)
        {
            TurnManager.TurnProgressionHandler(tilemapStructures);
        }
        if (Input.GetKeyDown(KeyCode.N) && moveButtonEnabled)
        {
            World.MoveUnit();
            enableArrowButtons = true;
        }
        if (arrowButtonsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && rightArrowButtonEnabled)
            {
                OnClickRightArrow();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && upArrowButtonEnabled)
            {
                OnClickUpArrow();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && rightArrowButtonEnabled)
            {
                OnClickDownArrow();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && upArrowButtonEnabled)
            {
                OnClickLeftArrow();
            }
        }
        if (enableArrowButtons && !arrowButtonsEnabled)
        {
            if (World.northPositionAvailable == true)
            {
                arrowUpButton.gameObject.SetActive(true);
                upArrowButtonEnabled = true;
            }
            if (World.eastPositionAvailable == true)
            {
                arrowRightButton.gameObject.SetActive(true);
                rightArrowButtonEnabled = true;
            }
            if (World.southPositionAvailable == true)
            {
                arrowDownButton.gameObject.SetActive(true);
                downArrowButtonEnabled = true;
            }
            if (World.westPositionAvailable == true)
            {
                arrowLeftButton.gameObject.SetActive(true);
                leftArrowButtonEnabled = true;
            }
            arrowButtonsEnabled = true;
        }
        else if (!enableArrowButtons && arrowButtonsEnabled)
        {
            arrowUpButton.gameObject.SetActive(false);
            arrowRightButton.gameObject.SetActive(false);
            arrowDownButton.gameObject.SetActive(false);
            arrowLeftButton.gameObject.SetActive(false);
            arrowButtonsEnabled = false;
        }
        if (enablePrimaryButton && !primaryButtonEnabled)
        {
            primaryButtonPanel.gameObject.SetActive(true);
            primaryButton.gameObject.SetActive(true);
            primaryButtonText.text = primaryButtonAssignedTo;
            primaryButtonEnabled = true;
        }
        else if (!enablePrimaryButton && primaryButtonEnabled)
        {
            primaryButtonPanel.gameObject.SetActive(false);
            primaryButton.gameObject.SetActive(false);
            primaryButtonText.text = "";
            primaryButtonEnabled = false;
        }
        if (enableSecondaryButton && !secondaryButtonEnabled)
        {
            secondaryButtonPanel.gameObject.SetActive(true);
            secondaryButton.gameObject.SetActive(true);
            secondaryButtonText.text = secondaryButtonAssignedTo;
            secondaryButtonEnabled = true;
        }
        else if (!enableSecondaryButton && secondaryButtonEnabled)
        {
            secondaryButtonPanel.gameObject.SetActive(false);
            secondaryButton.gameObject.SetActive(false);
            secondaryButtonText.text = "";
            secondaryButtonEnabled = false;
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
        if (GameMain.GUIEnabled && !playerGUIHasBeenUpdated)
        {
            playerGUI.SetActive(true);
            playerGUI_health.text = "Health: " + GameMain.playerHealth[GameMain.currentHumanPlayer];
            playerGUI_gold.text = "Gold: " + GameMain.playerGold[GameMain.currentHumanPlayer];
            playerGUI_combat.text = "Combat: " + GameMain.playerCombat[GameMain.currentHumanPlayer];
            playerGUI_lives.text = "Lives: " + GameMain.playerLives[GameMain.currentHumanPlayer];
            playerGUI_armor.text = "Armor: " + GameMain.playerArmor[GameMain.currentHumanPlayer];
            if (UpdatePlayerGUIAvatar.playerGUIAvatarHasBeenUpdated == false) { UpdatePlayerGUIAvatar.updatePlayerGUIAvatar = true; };
            playerGUIHasBeenUpdated = true;
        }
        if (!GameMain.GUIEnabled)
        {
            playerGUI.SetActive(false);
        }
        if (World.playerIsMoving && GameMain.currentPlayerInCamp)
        {
            ClearCenterText(clearCenterText);
        }
        if (clearCenterText)
        {
            centerText.SetText("");
            clearCenterText = false;
        }
    }

    public static void ClearCenterText(bool clearCenterText)
    {
        clearCenterText = true;
    }

    public static void TogglePrimaryButton(bool enable, string buttonText)
    {
        primaryButtonAssignedTo = buttonText;
        enablePrimaryButton = enable;
    }

    public static void ToggleSecondaryButton(bool enable, string buttonText)
    {
        secondaryButtonAssignedTo = buttonText;
        enableSecondaryButton = enable;
    }

    public static void ToggleEndTurnButton(bool enable)
    {
        enableEndTurnButton = enable;
    }

    public static void ToggleMoveButton(bool enable)
    {
        enableMoveButton = enable;
    }

    void OnClickPrimaryButton()
    {
        if (primaryButtonEnabled)
        {
            switch (primaryButtonAssignedTo)
            {
                case "move": World.MoveUnit(); enableArrowButtons = true; enablePrimaryButton = false; break;
                case "build": Villages.BuildVillage(tilemap, villageRed, villageBlue, villageGreen, villagePurple, villageWhite); enablePrimaryButton = false; break;
            }
        }
        else
        {
            Debug.Log("Primary is currently disabled");
        }
    }

    void OnClickSecondaryButton()
    {
        if (secondaryButtonEnabled)
        {
            switch (primaryButtonAssignedTo)
            {
                case "build": Villages.BuildVillage(tilemap, villageRed, villageBlue, villageGreen, villagePurple, villageWhite); GUI.enableSecondaryButton = false; break;
            }  
        }
        else
        {
            Debug.Log("Secondary is currently disabled");
        }
    }

    void OnClickEndTurn()
    {
        if (endTurnButtonEnabled)
        {
            TurnManager.TurnProgressionHandler(tilemapStructures);
            GUI.enableEndTurnButton = false;
        }
        else
        {
            Debug.Log("End turn is currently disabled");
        }
    }

    void OnClickUpArrow()
    {
        upArrowButtonEnabled = false;
        rightArrowButtonEnabled = false;
        downArrowButtonEnabled = false;
        leftArrowButtonEnabled = false;
        World.currentUnitDirection = "north";
        World.playerIsMoving = true;
        enableArrowButtons = false;
    }

    void OnClickRightArrow()
    {
        upArrowButtonEnabled = false;
        rightArrowButtonEnabled = false;
        downArrowButtonEnabled = false;
        leftArrowButtonEnabled = false;
        World.currentUnitDirection = "east";
        World.playerIsMoving = true;
        enableArrowButtons = false;
    }

    void OnClickDownArrow()
    {
        upArrowButtonEnabled = false;
        rightArrowButtonEnabled = false;
        downArrowButtonEnabled = false;
        leftArrowButtonEnabled = false;
        World.currentUnitDirection = "south";
        World.playerIsMoving = true;
        enableArrowButtons = false;
    }

    void OnClickLeftArrow()
    {
        upArrowButtonEnabled = false;
        rightArrowButtonEnabled = false;
        downArrowButtonEnabled = false;
        leftArrowButtonEnabled = false;
        World.currentUnitDirection = "west";
        World.playerIsMoving = true;
        enableArrowButtons = false;
    }
}