using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int health;
    public static int maxHealth;
    public static int lives;
    public static int armor;
    public static int gold;
    public static int totalPotions;
    public static int avatar;
    public static int village;
    public static int combat;
    public static int movementDice;
    public static int interactionRange;
    public static int weaponRange;
    public static string playerClass;
    public static List<int> playerWeapons = new List<int>();
    public static List<int> itemsList = new List<int>();

    private void Start()
    {
        playerClass = "nomad";
        PlayerSetup();
    }

    public static void PlayerSetup()
    {
        int[] values = Classes.ClassStartingStats(playerClass);
        avatar = values[0];
        health = values[1];
        maxHealth = values[1];
        lives = values[2];
        Debug.Log(lives);
        armor = values[3];
        movementDice = values[4];
        village = 1;
        gold = 150;
        totalPotions = 3;
        interactionRange = 1;
        int[] weaponValues = Weapons.WeaponsTable(values[5]);
        playerWeapons.Add(values[5]);
        combat = weaponValues[1];
        weaponRange = weaponValues[2];
        itemsList.Add(1);
        itemsList.Add(2);
        itemsList.Add(4);
        ItemGUI.UpdateWeaponAvatar();
        ItemGUI.UpdateItemAvatar();
        ItemGUI.UpdatePotionAvatar();
    }
}
