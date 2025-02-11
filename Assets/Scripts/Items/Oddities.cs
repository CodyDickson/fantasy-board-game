using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oddities : MonoBehaviour
{
    public static List<int[]> odditiesStats = new List<int[]>();

    // ODDITIES
    // Deal double damage when landing on a monster.
    // Increased critical hit chance.
    // Increased critical dodge chance.

    /*
     BRAINSTORMING
    The odds are stacked against you in Oddland.

    The objective of the game is to escape the map. Players choose a starting class and a map to begin. Each map has its own objectives, map design, and monsters. Players main stats are Lives (represented by green gems), Health, Combat Strength, and Travel Dice. Secondary stats are vision (how many areas can be seen at once), critical chance (to double attack value), critical dodge (to avoid death), max lives (how many lives the player can have), interaction range.

    For Chance:
    + Small Chance Increase (25%)
    ++ Medium Chance Increase (50%)
    +++ Large Chance Increase (75%)
    * Guaranteed
    
    For Combat Strength:
    + 1
    + 3
    + 5
    + 7

    For Interaction, Vision, Health, Travel Dice:
    + 1
    + 2
    + 3

    + Minimum Combat Strength
    + Maximum Combat Strength
    + Interaction Range
    + Vision Range
    + Health
    + Lives
    + Travel Dice
    + Chance to Double Total Damage
    + Chance to Double Health Recovered
    + Chance to Double Gold Rewards
    + Chance to Instant Kill When Passing Monsters
    + Chance Tolls Are Waived

    48
     */

    public static void GenerateOddity()
    {
        int random = Random.Range(1,49);
    }

    public static int[] OddityTable(int oddity)
    {
        int[] values = new int[4];
        switch (oddity)
        {
            case 1: Player.minimumCombatStrength += 1; break;
        }
        return values;
    }
}