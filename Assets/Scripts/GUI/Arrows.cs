using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Arrows : MonoBehaviour
{
    public Button arrowUpButton;
    public Button arrowRightButton;
    public Button arrowDownButton;
    public Button arrowLeftButton;
    public static bool enableArrowButtons;
    public static bool arrowsEnabled;
    public static bool disableArrowButtons;

    private void Start()
    {
        arrowUpButton.onClick.AddListener(OnClickUpArrow);
        arrowRightButton.onClick.AddListener(OnClickRightArrow);
        arrowDownButton.onClick.AddListener(OnClickDownArrow);
        arrowLeftButton.onClick.AddListener(OnClickLeftArrow);
        arrowUpButton.gameObject.SetActive(false);
        arrowRightButton.gameObject.SetActive(false);
        arrowDownButton.gameObject.SetActive(false);
        arrowLeftButton.gameObject.SetActive(false);
    }
    void Update()
    {
        if (arrowsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                OnClickRightArrow();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                OnClickUpArrow();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                OnClickDownArrow();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                OnClickLeftArrow();
            }
        }
        if (enableArrowButtons)
        {
            if (BoardManager.northPositionAvailable == true)
            {
                arrowUpButton.gameObject.SetActive(true);
            }
            if (BoardManager.eastPositionAvailable == true)
            {
                arrowRightButton.gameObject.SetActive(true);
            }
            if (BoardManager.southPositionAvailable == true)
            {
                arrowDownButton.gameObject.SetActive(true);
            }
            if (BoardManager.westPositionAvailable == true)
            {
                arrowLeftButton.gameObject.SetActive(true);
            }
            enableArrowButtons = false;
        }
        if (disableArrowButtons)
        {
            arrowUpButton.gameObject.SetActive(false);
            arrowRightButton.gameObject.SetActive(false);
            arrowDownButton.gameObject.SetActive(false);
            arrowLeftButton.gameObject.SetActive(false);
            disableArrowButtons = false;
        }
    }

    public static void EnableArrowButtons()
    {
        BoardManager.CheckForLocalBoardPositions();
        if (GameMain.playerInCamp[GameMain.currentPlayer])
        {
            BoardManager.CheckForCampExits();
        }
        enableArrowButtons = true;
    }

    public static void DisableArrowButtons()
    {
        disableArrowButtons = true;
    }

    public static void OnClickUpArrow()
    {
        DisableArrowButtons();
        BoardManager.currentUnitDirection = "north";
        PlayerMovement.playerIsMoving = true;
    }

    void OnClickRightArrow()
    {
        DisableArrowButtons();
        BoardManager.currentUnitDirection = "east";
        PlayerMovement.playerIsMoving = true;
    }

    void OnClickDownArrow()
    {
        DisableArrowButtons();
        BoardManager.currentUnitDirection = "south";
        PlayerMovement.playerIsMoving = true;
    }

    void OnClickLeftArrow()
    {
        DisableArrowButtons();
        BoardManager.currentUnitDirection = "west";
        PlayerMovement.playerIsMoving = true;
    }
}