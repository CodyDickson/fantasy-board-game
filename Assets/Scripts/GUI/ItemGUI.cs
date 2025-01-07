using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGUI : MonoBehaviour
{
    public static bool updateWeaponAvatar = false;
    public static bool clearWeaponAvatar = false;
    public Image weapon;

    void Start()
    {
        weapon = weapon.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (updateWeaponAvatar)
        {
            weapon.sprite = Store.weaponSprites[GameMain.current_weapon];
            updateWeaponAvatar = false;
        }
        if (clearWeaponAvatar)
        {
            weapon.sprite = null;
            clearWeaponAvatar = false;
        }
    }

    public static void UpdateWeaponAvatar()
    {
        updateWeaponAvatar = true;
    }

    public static void ClearWeaponAvatar()
    {
        clearWeaponAvatar = true;
    }
}