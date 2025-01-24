using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int health;
    public static int maxHealth;
    public static int totalPotions;
    public static int avatar;
    public static int village;
    public static int interactionRange;

    private void Start()
    {
        health = 5;
        maxHealth = 5;
        totalPotions = 3;
        interactionRange = 1;
    }
}
