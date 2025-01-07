using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public static int[] WeaponsTable(int weapon)
    {
        int[] values = new int[2];
        // Avatar, Combat Score
        if (weapon == 1)
        {
            // Avatar
            values[0] = 0;
            // Combat Score
            values[1] = 2;
        }
        else if (weapon == 2)
        {
            // Avatar
            values[0] = 1;
            // Combat Score
            values[1] = 4;
        }
        else if (weapon == 3)
        {
            // Avatar
            values[0] = 2;
            // Combat Score
            values[1] = 3;
        }
        return values;
    }
}