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

    public static void ClassStartingStats(string className)
    { 
        if (className == "nomad")
        {
            // GameMain.player_equipment[0].Add(GameMain.currentPlayer, 1);
            // health, movement dice, avatar, armor, lives
        }
    }
}