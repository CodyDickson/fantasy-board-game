using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

public class GUI : MonoBehaviour
{
    public Button rollButton;
    public Button secondaryButton;
    public Button endTurnButton;
    public GameObject secondaryButtonPanel;
    public Button arrowUpButton;
    public Button arrowRightButton;
    public Button arrowDownButton;
    public Button arrowLeftButton;
    public World world;
    public static bool enableArrowButtons = false;
    [SerializeField] public Tile player;
    [SerializeField] public Tile player_red;
    [SerializeField] public Tile player_blue;
    [SerializeField] public Tile player_green;
    [SerializeField] public Tile player_purple;
    [SerializeField] public Tile player_white;
    [SerializeField] public Tile village;
    [SerializeField] public Tile village_red;
    [SerializeField] public Tile village_blue;
    [SerializeField] public Tile village_green;
    [SerializeField] public Tile village_purple;
    [SerializeField] public Tile village_white;
    [SerializeField] public Tile monster;
    [SerializeField] public Tile monsterImp;
    [SerializeField] public Tile monsterBasilisk;
    [SerializeField] public Tile dungeon;
    [SerializeField] public Tilemap tilemap;
    [SerializeField] public Tilemap tilemapStructures;
    [SerializeField] public Tilemap tilemapUnits;
    [SerializeField] public TMP_Text textGoldPlayer1;
    [SerializeField] public TMP_Text textGoldPlayer2;
    [SerializeField] public TMP_Text textGoldPlayer3;
    [SerializeField] public TMP_Text textGoldPlayer4;
    [SerializeField] public TMP_Text textCurrentPlayer;

    void Start()
    {
        rollButton.onClick.AddListener(OnClickRoll);
        secondaryButton.onClick.AddListener(OnClickSecondaryButton);
        endTurnButton.onClick.AddListener(OnClickEndTurn);
        arrowUpButton.onClick.AddListener(OnClickArrowUpButton);
        arrowRightButton.onClick.AddListener(OnClickArrowRightButton);
        arrowDownButton.onClick.AddListener(OnClickArrowDownButton);
        arrowLeftButton.onClick.AddListener(OnClickArrowLeftButton);
        endTurnButton.gameObject.SetActive(false);
        secondaryButton.gameObject.SetActive(false);
        secondaryButtonPanel.gameObject.SetActive(false);
        arrowUpButton.gameObject.SetActive(false);
        arrowRightButton.gameObject.SetActive(false);
        arrowDownButton.gameObject.SetActive(false);
        arrowLeftButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (enableArrowButtons)
        {
            if (World.northPositionAvailable == true)
            {
                arrowUpButton.gameObject.SetActive(true);
            }
            if (World.eastPositionAvailable == true)
            {
                arrowRightButton.gameObject.SetActive(true);
            }
            if (World.southPositionAvailable == true)
            {
                arrowDownButton.gameObject.SetActive(true);
            }
            if (World.westPositionAvailable == true)
            {
                arrowLeftButton.gameObject.SetActive(true);
            }
        }
        else if (!enableArrowButtons)
        {
            arrowUpButton.gameObject.SetActive(false);
            arrowRightButton.gameObject.SetActive(false);
            arrowDownButton.gameObject.SetActive(false);
            arrowLeftButton.gameObject.SetActive(false);
        }

        if (GameMain.activePlayers >= 1)
        {
            textGoldPlayer1.text = "player " + GameMain.playerOneColor + " - gold: " + GameMain.player_gold_one + "\nlives: " + GameMain.livesPlayerOne + " move: " + GameMain.player_moveDice_one + " combat: " + GameMain.player_combatDice_one;
        }
        if (GameMain.activePlayers >= 2)
        {
            textGoldPlayer2.text = "player " + GameMain.playerTwoColor + " - gold: " + GameMain.player_gold_two + "\nlives: " + GameMain.livesPlayerTwo + " move: " + GameMain.player_moveDice_two + " combat: " + GameMain.player_combatDice_two;
        }
        if (GameMain.activePlayers >= 3)
        {
            textGoldPlayer3.text = "player " + GameMain.playerThreeColor + " - gold: " + GameMain.player_gold_three + "\nlives: " + GameMain.livesPlayerThree + " move: " + GameMain.player_moveDice_three + " combat: " + GameMain.player_combatDice_three;
        }
        if (GameMain.activePlayers >= 4)
        {
            textGoldPlayer4.text = "player " + GameMain.playerFourColor + " - gold: " + GameMain.player_gold_four + "\nlives: " + GameMain.livesPlayerFour + " move: " + GameMain.player_moveDice_four + " combat: " + GameMain.player_combatDice_four;
        }
        textCurrentPlayer.text = "current player: " + GameMain.currentPlayer;
        if (GameMain.secondaryButtonEnabled && GameMain.GUIEnabled)
        {
            secondaryButton.gameObject.SetActive(true);
            secondaryButtonPanel.gameObject.SetActive(true);
        }
        else if (!GameMain.secondaryButtonEnabled)
        {
            secondaryButton.gameObject.SetActive(false);
            secondaryButtonPanel.gameObject.SetActive(false);
        }
    }

    void OnClickRoll()
    {
        if (GameMain.bottomLeftLowerButtonEnabled)
        {
            World.MoveUnit();
        }
        else
        {
            Debug.Log("Move is currently disabled");
        }
    }

    void OnClickSecondaryButton()
    {
        GameMain.BuildVillage(tilemap, village, village_red, village_blue, village_green, village_purple, village_white);
    }

    void OnClickEndTurn()
    {
        if (GameMain.endTurnButtonEnabled)
        {
            GameMain.EndTurn(tilemap, monsterImp, monsterBasilisk, world);
        }
        else
        {
            Debug.Log("End turn is currently disabled");
        }
    }

    void OnClickArrowUpButton()
    {
        World.currentUnitDirection = "north";
        World.playerIsMoving = true;
        enableArrowButtons = false;
    }

    void OnClickArrowRightButton()
    {
        World.currentUnitDirection = "east";
        World.playerIsMoving = true;
        enableArrowButtons = false;
    }

    void OnClickArrowDownButton()
    {
        World.currentUnitDirection = "south";
        World.playerIsMoving = true;
        enableArrowButtons = false;
    }

    void OnClickArrowLeftButton()
    {
        World.currentUnitDirection = "west";
        World.playerIsMoving = true;
        enableArrowButtons = false;
    }
}