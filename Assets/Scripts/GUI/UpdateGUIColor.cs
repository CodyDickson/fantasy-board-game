using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGUIColor : MonoBehaviour
{
    public static Image[] panelsToChange;
    public static bool updateGUIColor = false;

    void Start()
    {
        panelsToChange = gameObject.GetComponentsInChildren<Image>();
        ChangeGUIColor();
    }

    public static void ChangeGUIColor()
    {
        foreach (Image panel in panelsToChange)
        {
            panel.color = Store.playerColors[GameMain.currentPlayer];
        }
        updateGUIColor = false;
    }
}