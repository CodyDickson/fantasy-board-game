using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CombatManager : MonoBehaviour
{
    public static List<Vector3> potentials = new List<Vector3>();
    public static bool combatEnabled = false;

    public static void StartCombat()
    {
        ShowPotentialAttacks();
        combatEnabled = true;
    }

    public static void StopCombat()
    {
        ClearPotentialAttacks();
        combatEnabled = false;
    }

    public static void ShowPotentialAttacks()
    {
        Tilemap tilemap = Store.tilemaps[3];
        Vector3 position = BoardManager.currentUnitPosition;
        for (int z = 0, y = 0; y <= Player.weaponRange; y++)
        {
            for (int x = 0; x <= Player.weaponRange; x++, z++)
            {
                potentials.Add(new Vector3Int((int)position[0] + x, (int)position[1] + y));
                potentials.Add(new Vector3Int((int)position[0] - x, (int)position[1] + y));
                potentials.Add(new Vector3Int((int)position[0] + x, (int)position[1] - y));
                potentials.Add(new Vector3Int((int)position[0] - x, (int)position[1] - y));
            }
        }
        foreach (Vector3 potential in potentials)
        {
            foreach (Vector3 boardPosition in BoardManager.boardPositions)
            {
                if (potential == boardPosition)
                {
                    tilemap.SetTile(new Vector3Int((int)potential[0], (int)potential[1]), Store.objectTiles[2]);
                }
            }
        }
    }

    public static void ClearPotentialAttacks()
    {
        potentials.Clear();
    }
}