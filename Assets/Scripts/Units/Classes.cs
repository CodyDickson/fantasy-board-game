using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classes : MonoBehaviour
{
    public static List<string> classes = new List<string>();

    public static void UpdateClassList()
    {
        classes.Add("nomad");
    }

    public static void CheckClass()
    {

    }

    public static int[] ClassStartingStats(string className)
    {
        int[] values = new int[6];
        if (className == "nomad")
        {
            // Avatar
            values[0] = 0;
            // Health
            values[1] = 3;
            // Lives
            values[2] = 1;
            // Armor
            values[3] = 0;
            // Movement Dice
            values[4] = 1;
            // Starting Weapon
            values[5] = 1;
        }
        else if (className == "fighter")
        {
            // Avatar
            values[0] = 1;
            // Health
            values[1] = 3;
            // Lives
            values[2] = 1;
            // Armor
            values[3] = 1;
            // Movement Dice
            values[4] = 1;
            // Starting Weapon
            values[5] = 2;
        }
        return values;
    }
}