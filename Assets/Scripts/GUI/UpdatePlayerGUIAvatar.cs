using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerGUIAvatar : MonoBehaviour
{
    public Sprite playerRed, playerBlue, playerGreen, playerPurple, playerWhite;
    public static bool updatePlayerGUIAvatar = false;
    public static bool playerGUIAvatarHasBeenUpdated = false;

    void Start()
    {
        updatePlayerGUIAvatar = true;
    }

    void Update()
    {
        if (updatePlayerGUIAvatar)
        {
            switch (World.currentPlayerColor)
            {
                case "red": gameObject.GetComponent<Image>().sprite = playerRed; break;
                case "blue": gameObject.GetComponent<Image>().sprite = playerBlue; break;
                case "green": gameObject.GetComponent<Image>().sprite = playerGreen; break;
                case "purple": gameObject.GetComponent<Image>().sprite = playerPurple; break;
                case "white": gameObject.GetComponent<Image>().sprite = playerWhite; break;
            }
            updatePlayerGUIAvatar = false;
            playerGUIAvatarHasBeenUpdated = true;
        }
    }
}