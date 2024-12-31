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
        Vector3 center = new Vector3(0, 0);
        switch (clockworkLocation)
        {
            case 0: center = new Vector3(0, 0); break;
            case 1: center = new Vector3(0, 13); break;
            case 2: center = new Vector3(13, 13); break;
            case 3: center = new Vector3(13, 0); break;
            case 4: center = new Vector3(13, -13); break;
            case 5: center = new Vector3(0, -13); break;
            case 6: center = new Vector3(-13, -13); break;
            case 7: center = new Vector3(-13, 0); break;
            case 8: center = new Vector3(-13, 13); break;
            case 9: center = new Vector3(-13, 26); break;
            case 10: center = new Vector3(0, 26); break;
            case 11: center = new Vector3(13, 26); break;
            case 12: center = new Vector3(26, 26); break;
            case 13: center = new Vector3(26, 13); break;
            case 14: center = new Vector3(26, 0); break;
            case 15: center = new Vector3(26, -13); break;
            case 16: center = new Vector3(26, -26); break;
            case 17: center = new Vector3(13, -26); break;
            case 18: center = new Vector3(0, -26); break;
            case 19: center = new Vector3(-13, -26); break;
            case 20: center = new Vector3(-26, -26); break;
            case 21: center = new Vector3(-26, -13); break;
            case 22: center = new Vector3(-26, 0); break;
            case 23: center = new Vector3(-26, 13); break;
            case 24: center = new Vector3(-26, 26); break;
        }
        int xSize = 10;
        int ySize = 10;
        for (int z = (int)center[0], y = (int)center[1]; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, z++)
            {
                fog.SetTile(new Vector3Int(x, y), null);
                fog.SetTile(new Vector3Int(-x, y), null);
                fog.SetTile(new Vector3Int(x, -y), null);
                fog.SetTile(new Vector3Int(-x, -y), null);
            }
        }
    }
}