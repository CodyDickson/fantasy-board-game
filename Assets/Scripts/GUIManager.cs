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
    [SerializeField] public Tile village;
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
            textGoldPlayer1.text = "Player 1 Gold: " + GameMain.goldPlayer1;
        }
        if (GameMain.activePlayers >= 2)
        {
            textGoldPlayer2.text = "Player 2 Gold: " + GameMain.goldPlayer2;
        }
        if (GameMain.activePlayers >= 3)
        {
            textGoldPlayer3.text = "Player 3 Gold: " + GameMain.goldPlayer3;
        }
        if (GameMain.activePlayers >= 4)
        {
            textGoldPlayer4.text = "Player 4 Gold: " + GameMain.goldPlayer4;
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
            GameMain.MoveUnit(tilemap, player);
        }
        else
        {
            Debug.Log("Move is currently disabled");
        }
    }

    void OnClickSecondaryButton()
    {
        GameMain.BuildVillage(tilemap, village);
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