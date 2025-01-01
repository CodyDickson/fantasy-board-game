using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fog : MonoBehaviour
{
    public static void GenerateFog()
    {
        Tilemap fog = Store.tilemaps[5];
        Tile fogTile = Store.fogTiles[0];
        int xSize = 50;
        int ySize = 50;
        for (int z = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, z++)
            {
                fog.SetTile(new Vector3Int(x, y), fogTile);
                fog.SetTile(new Vector3Int(-x, y), fogTile);
                fog.SetTile(new Vector3Int(x, -y), fogTile);
                fog.SetTile(new Vector3Int(-x, -y), fogTile);
            }
        }
    }

    public static void RemoveLocalFog(int clockworkLocation)
    {
        Tilemap fog = Store.tilemaps[5];
        Vector3 center = BoardManager.CheckClockworkPosition(clockworkLocation);
        int xSize = 9;
        int ySize = 9;
        for (int z = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, z++)
            {
                fog.SetTile(new Vector3Int((int)center[0] + x, (int)center[1] + y), null);
                fog.SetTile(new Vector3Int((int)center[0] - x, (int)center[1] + y), null);
                fog.SetTile(new Vector3Int((int)center[0] + x, (int)center[1] - y), null);
                fog.SetTile(new Vector3Int((int)center[0] - x, (int)center[1] - y), null);
            }
        }
    }
}