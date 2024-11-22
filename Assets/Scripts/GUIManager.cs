using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

public class GUIManager : MonoBehaviour
{
    public Button rollButton;
    public Button secondaryButton;
    public GameObject secondaryButtonPanel;
    public Button endTurnButton;
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
        secondaryButton.gameObject.SetActive(false);
        secondaryButtonPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (GameMain.activePlayers >= 1)
        {
            textGoldPlayer1.text = "Player 1 Gold: " + GameMain.player_gold_one;
        }
        if (GameMain.activePlayers >= 2)
        {
            textGoldPlayer2.text = "Player 2 Gold: " + GameMain.player_gold_two;
        }
        if (GameMain.activePlayers >= 3)
        {
            textGoldPlayer3.text = "Player 3 Gold: " + GameMain.player_gold_three;
        }
        if (GameMain.activePlayers >= 4)
        {
            textGoldPlayer4.text = "Player 4 Gold: " + GameMain.player_gold_four;
        }
        textCurrentPlayer.text = "Current Player: " + GameMain.currentPlayer;

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
            GameMain.MoveUnit(tilemap, player, player_red, player_blue, player_green, player_purple, player_white);
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
            GameMain.EndTurn(tilemap, monsterImp, monsterBasilisk);
        }
        else
        {
            Debug.Log("End turn is currently disabled");
        }
    }
}