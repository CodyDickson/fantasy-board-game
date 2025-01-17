using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CombatManager : MonoBehaviour
{
    public static List<Vector3> potentials = new List<Vector3>();
    public static void StartCombat()
    {
        ShowPotentialAttacks();
    }

    public static void ShowPotentialAttacks()
    {
        Tilemap tilemap = Store.tilemaps[3];
        Vector3 position = BoardManager.currentUnitPosition;
        for (int z = (int)position[0], y = 0; y <= GameMain.weaponRange; y++)
        {
            for (int x = (int)position[1]; x <= GameMain.weaponRange; x++, z++)
            {
                potentials.Add(new Vector3Int(x, y));
                potentials.Add(new Vector3Int(-x, y));
                potentials.Add(new Vector3Int(x, -y));
                potentials.Add(new Vector3Int(-x, -y));
            }
        }
        foreach (Vector3 potential in potentials)
        {
            tilemap.SetTile(new Vector3Int((int)potential[0], (int)potential[1]), Store.objectTiles[2]);
        }
    }

    public static void ClearPotentialAttacks()
    {
        potentials.Clear();
    }
}