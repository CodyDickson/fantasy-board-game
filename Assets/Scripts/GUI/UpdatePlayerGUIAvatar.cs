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
            gameObject.GetComponent<Image>().sprite = Store.playerSprites[GameMain.currentPlayer];
            updatePlayerGUIAvatar = false;
            playerGUIAvatarHasBeenUpdated = true;
        }
    }
}