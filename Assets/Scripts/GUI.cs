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
    public GameObject infoGUI;
    public Button primaryButton;
    public GameObject primaryButtonPanel;
    public Button secondaryButton;
    public GameObject secondaryButtonPanel;
    public Button endTurnButton;
    public GameObject endTurnButtonPanel;
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
    public static bool enableArrowButtons = false;
    public static bool arrowButtonsEnabled = false;
    public static bool rightArrowButtonEnabled = false;
    public static bool leftArrowButtonEnabled = false;
    public static bool upArrowButtonEnabled = false;
    public static bool downArrowButtonEnabled = false;
    public static bool updateGUIColor = false;
    public bool GUIColorHasBeenUpdated = false;
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
    [SerializeField] public TMP_Text playerGUI_health;
    [SerializeField] public TMP_Text playerGUI_combat;
    [SerializeField] public TMP_Text playerGUI_gold;
    [SerializeField] public TMP_Text playerGUI_lives;
    [SerializeField] public TMP_Text playerGUI_initiative;
    [SerializeField] public TMP_Text infoGUI_topText;
    [SerializeField] public TMP_Text infoGUI_middleText;
    [SerializeField] public TMP_Text infoGUI_bottomText;

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
        arrowUpButton.gameObject.SetActive(false);
        arrowRightButton.gameObject.SetActive(false);
        arrowDownButton.gameObject.SetActive(false);
        arrowLeftButton.gameObject.SetActive(false);
        infoGUI.gameObject.SetActive(false);
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
            GameMain.EndTurn(tilemapStructures, monsterImp, monsterBasilisk, world);
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
            primaryButtonEnabled = true;
        }
        else if (!enablePrimaryButton && primaryButtonEnabled)
        {
            primaryButtonPanel.gameObject.SetActive(false);
            primaryButton.gameObject.SetActive(false);
            primaryButtonEnabled = false;
        }
        if (enableSecondaryButton && !secondaryButtonEnabled)
        {
            secondaryButtonPanel.gameObject.SetActive(true);
            secondaryButtonEnabled = true;
        }
        else if (!enableSecondaryButton && secondaryButtonEnabled)
        {
            secondaryButtonPanel.gameObject.SetActive(false);
            secondaryButtonEnabled = false;
        }
        if (GameMain.GUIEnabled)
        {
            playerGUI.SetActive(true);
            switch (Player.currentHumanPlayer)
            {
                case 1:
                    playerGUI_health.text = "Health: " + GameMain.playerOneHealth;
                    playerGUI_gold.text = "Gold: " + GameMain.playerOneGold;
                    playerGUI_combat.text = "Combat: " + GameMain.playerOneCombat;
                    playerGUI_lives.text = "Lives: " + GameMain.playerOneLives;
                    break;
                case 2:
                    playerGUI_health.text = "Health: " + GameMain.playerTwoHealth;
                    playerGUI_gold.text = "Gold: " + GameMain.playerTwoGold;
                    playerGUI_combat.text = "Combat: " + GameMain.playerTwoCombat;
                    playerGUI_lives.text = "Lives: " + GameMain.playerTwoLives;
                    break;
            }
            if (UpdatePlayerGUIAvatar.playerGUIAvatarHasBeenUpdated == false) { UpdatePlayerGUIAvatar.updatePlayerGUIAvatar = true; };
            if (World.playerIsMoving && GameMain.currentPlayerInCamp)
            {
                centerText.SetText("");
            }
        }
        if (!GameMain.GUIEnabled)
        {
            playerGUI.SetActive(false);
            infoGUI.SetActive(false);
        }
        if (!World.villageNearby)
        {
            infoGUI.SetActive(false);
        }
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
            GameMain.EndTurn(tilemap, monsterImp, monsterBasilisk, world);
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