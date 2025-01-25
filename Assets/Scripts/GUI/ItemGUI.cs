using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGUI : MonoBehaviour
{
    public static bool updateWeaponAvatar = false;
    public static bool clearWeaponAvatar = false;
    public static int firstPlayerWeapon;
    public static int secondPlayerWeapon;
    public Image weapon, weaponTwo;

    void Start()
    {
        weapon = weapon.gameObject.GetComponent<Image>();
        weaponTwo = weaponTwo.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (updateWeaponAvatar)
        {
            weapon.sprite = Store.weaponSprites[firstPlayerWeapon];
            if (Player.playerWeapons.Count > 1)
            {
                weaponTwo.sprite = Store.weaponSprites[secondPlayerWeapon];
            }
            else
            {
                weaponTwo.sprite = null;
            }
            updateWeaponAvatar = false;
        }
        if (clearWeaponAvatar)
        {
            weapon.sprite = null;
            weaponTwo.sprite = null;
            clearWeaponAvatar = false;
        }
    }

    public static void UpdateWeaponAvatar()
    {
        updateWeaponAvatar = true;
        firstPlayerWeapon = Player.playerWeapons[0];
        if (Player.playerWeapons.Count > 1)
        {
            secondPlayerWeapon = Player.playerWeapons[1];
        }
    }

    public static void ClearWeaponAvatar()
    {
        clearWeaponAvatar = true;
    }
}