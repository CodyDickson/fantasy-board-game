using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Stats //
    public static string title;
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
    public static int minimumCombatStrength;
    public static int maximumCombatStrength;
    public static int weaponRange;
    public static int actionsRemaining;
    public static List<int> weaponsList = new List<int>();
    public static List<int> itemsList = new List<int>();

    private void Start()
    {
        title = "Nomad";
        PlayerSetup();
    }

    public static void PlayerSetup()
    {
        int[] values = Classes.ClassStartingStats(title);
        avatar = values[0];
        health = values[1];
        maxHealth = health;
        lives = values[2];
        Debug.Log("Lives Remaining: " + lives);
        armor = values[3];
        movementDice = values[4];
        village = 1;
        gold = 150;
        totalPotions = 3;
        interactionRange = 1;
        actionsRemaining = 3;
        int[] weaponValues = Weapons.WeaponsTable(values[5]);
        weaponsList.Add(values[5]);
        combat = weaponValues[1];
        weaponRange = weaponValues[2];
        ItemGUI.UpdateWeaponAvatar();
        ItemGUI.UpdateItemAvatar();
        ItemGUI.UpdatePotionAvatar();
        AddPlayerToRoster();
    }

    public static void AddPlayerToRoster()
    {
        // 
    }
}