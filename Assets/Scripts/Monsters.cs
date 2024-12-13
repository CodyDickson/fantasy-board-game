using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void UpdateMonsterInfo()
    {
        // When clicking on a monster or landing on a monster
        // Updates the LowerGUI, image of the monster on the left and "monster type", "monster health"
        // Lower button is "Fight" if player has landed on the monster, this opens the combat GUI in the middle of the screen
    }

    public static void SpawnMonsters()
    {
        for (int i = 0; i < World.boardDungeonPositions.Count; i++)
        {
            // Access Vector3 of boardDungeonPositions
            // boardPosition = boardDungeonPositions[i];
            // World.CheckForLocalBoardPositions();
            // tilemapStructures.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition.[1]), imp);
        }
    }
}
