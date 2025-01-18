using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int health;
    public static int maxHealth;
    public static int totalPotions;

    private void Start()
    {
        health = 5;
        maxHealth = 5;
        totalPotions = 3;
    }
}
