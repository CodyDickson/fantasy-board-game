using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGUI : MonoBehaviour
{
    public static bool updateWeaponAvatar = false;
    public static bool clearWeaponAvatar = false;
    public static bool updateItemAvatar = false;
    public static bool clearItemAvatar = false;
    public static bool updatePotionAvatar = false;
    public static bool clearPotionAvatar = false;
    public static int firstPlayerWeapon;
    public static int secondPlayerWeapon;
    public static int firstItem;
    public static int secondItem;
    public static int thirdItem;
    public static int fourthItem;
    public static int fifthItem;
    public static int potionItem;
    public Image weapon, weaponTwo, itemOne, itemTwo, itemThree, itemFour, itemFive, potion;

    void Start()
    {
        weapon = weapon.gameObject.GetComponent<Image>();
        weaponTwo = weaponTwo.gameObject.GetComponent<Image>();
        itemOne = itemOne.gameObject.GetComponent<Image>();
        itemTwo = itemTwo.gameObject.GetComponent<Image>();
        itemThree = itemThree.gameObject.GetComponent<Image>();
        itemFour = itemFour.gameObject.GetComponent<Image>();
        itemFive = itemFive.gameObject.GetComponent<Image>();
        potion = potion.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (updateWeaponAvatar)
        {
            weapon.enabled = true;
            weapon.sprite = Store.weaponSprites[firstPlayerWeapon];
            if (Player.weaponsList.Count > 1)
            {
                weaponTwo.enabled = true;
                weaponTwo.sprite = Store.weaponSprites[secondPlayerWeapon];
            }
            else
            {
                weaponTwo.enabled = false;
            }
            updateWeaponAvatar = false;
        }
        if (clearWeaponAvatar)
        {
            weapon.enabled = false;
            weaponTwo.enabled = false;
            clearWeaponAvatar = false;
        }
        if (updateItemAvatar)
        {
            int count = Player.itemsList.Count;
            if (count > 0)
            {
                itemOne.enabled = true;
                itemOne.sprite = Store.itemSprites[firstItem];
            }
            else
            {
                itemOne.enabled = false;
            }
            if (count > 1)
            {
                itemTwo.enabled = true;
                itemTwo.sprite = Store.itemSprites[secondItem];
            }
            else
            {
                itemTwo.enabled = false;
            }
            if (count > 2)
            {
                itemThree.enabled = true;
                itemThree.sprite = Store.itemSprites[thirdItem];
            }
            else
            {
                itemThree.enabled = false;
            }
            if (count > 3)
            {
                itemFour.enabled = true;
                itemFour.sprite = Store.itemSprites[fourthItem];
            }
            else
            {
                itemFour.enabled = false;
            }
            if (count > 4)
            {
                itemFive.enabled = true;
                itemFive.sprite = Store.itemSprites[fifthItem];
            }
            else
            {
                itemFive.enabled = false;
            }
            updateItemAvatar = false;
        }
        if (updatePotionAvatar)
        {
            if (Player.totalPotions > 0)
            {
                potion.enabled = true;
                potion.sprite = Store.itemSprites[0];
            }
            else
            {
                potion.enabled = false;
            }
            updatePotionAvatar = false;
        }
    }

    public static void UpdateWeaponAvatar()
    {
        updateWeaponAvatar = true;
        firstPlayerWeapon = Player.weaponsList[0];
        if (Player.weaponsList.Count > 1)
        {
            secondPlayerWeapon = Player.weaponsList[1];
        }
    }

    public static void ClearWeaponAvatar()
    {
        clearWeaponAvatar = true;
    }

    public static void UpdateItemAvatar()
    {
        updateItemAvatar = true;
        if (Player.itemsList.Count > 0)
        {
            firstItem = Player.itemsList[0];
        }
        if (Player.itemsList.Count > 1)
        {
            secondItem = Player.itemsList[1];
        }
        if (Player.itemsList.Count > 2)
        {
            thirdItem = Player.itemsList[2];
        }
        if (Player.itemsList.Count > 3)
        {
            fourthItem = Player.itemsList[3];
        }
        if (Player.itemsList.Count > 4)
        {
            fifthItem = Player.itemsList[4];
        }
    }

    public static void ClearItemAvatar()
    {
        clearItemAvatar = true;
    }

    public static void UpdatePotionAvatar()
    {
        updatePotionAvatar = true;
    }
}