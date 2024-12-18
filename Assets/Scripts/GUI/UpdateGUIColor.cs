using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGUIColor : MonoBehaviour
{
    public Image[] panelsToChange;
    public Color[] colors;
    public static bool updateGUIColor = false;

    void Start()
    {
        ChangeGUIColor();
    }

    void Update()
    {
        if (updateGUIColor)
        {
            ChangeGUIColor();
        }
    }

    public void ChangeGUIColor()
    {
        foreach (Image panel in panelsToChange)
        {
            switch (World.currentPlayerColor)
            {
                case "red": panel.color = colors[0]; break;
                case "blue": panel.color = colors[1]; break;
            }
        }
        updateGUIColor = false;
    }
}