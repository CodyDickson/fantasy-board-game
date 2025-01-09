using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderGUI : MonoBehaviour
{
    public static bool updateTurnOrderGUI = false;
    public Image slotOne, slotTwo, slotThree;

    void Start()
    {
        slotOne = slotOne.gameObject.GetComponent<Image>();
        slotTwo = slotTwo.gameObject.GetComponent<Image>();
        slotThree = slotThree.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (updateTurnOrderGUI)
        {
            RefreshTurnOrderGUI(slotOne, slotTwo, slotThree);
            updateTurnOrderGUI = false;
        }
    }

    public static void ToggleTurnOrderGUI()
    {
        updateTurnOrderGUI = true;
    }

    public static void RefreshTurnOrderGUI(Image slotOne, Image slotTwo, Image slotThree)
    {
        string itemOne = "";
        string itemTwo = "";
        string itemThree = "";
        for (int items = 1; items <= 3; items += 1)
        {
            switch (items)
            {
                case 1: itemOne = TurnManager.turnOrder[0]; break;
                case 2: itemTwo = TurnManager.turnOrder[1]; break;
                case 3: itemThree = TurnManager.turnOrder[2]; break;
            }
        }
        Debug.Log(itemOne + " pass");
        if (itemOne == "player")
        {
            slotOne.sprite = Store.playerSprites[GameMain.currentPlayerAvatar];
        }
        if (itemOne == "spawnMonsters")
        {
            slotOne.sprite = Store.GUIElements[3];
        }
        if (itemOne == "moveMonsters")
        {
            slotOne.sprite = Store.GUIElements[4];
        }
        if (itemTwo == "player")
        {
            slotTwo.sprite = Store.playerSprites[GameMain.currentPlayerAvatar];
        }
        if (itemTwo == "spawnMonsters")
        {
            slotTwo.sprite = Store.GUIElements[3];
        }
        if (itemTwo == "moveMonsters")
        {
            slotTwo.sprite = Store.GUIElements[4];
        }
        if (itemThree == "player")
        {
            slotThree.sprite = Store.playerSprites[GameMain.currentPlayerAvatar];
        }
        if (itemThree == "spawnMonsters")
        {
            slotThree.sprite = Store.GUIElements[3];
        }
        if (itemThree == "moveMonsters")
        {
            slotThree.sprite = Store.GUIElements[4];
        }
    }
}
