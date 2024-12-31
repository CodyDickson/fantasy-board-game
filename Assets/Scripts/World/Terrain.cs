using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Terrain : MonoBehaviour
{
    public static void Generate_Grasslands_Terrain(Tilemap terrain, Tilemap terrainObjects, Tile ground, Tile tree)
    {
        int xSize = 50;
        int ySize = 50;
        for (int z = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, z++)
            {
                terrain.SetTile(new Vector3Int(x, y), ground);
                terrain.SetTile(new Vector3Int(-x, y), ground);
                terrain.SetTile(new Vector3Int(x, -y), ground);
                terrain.SetTile(new Vector3Int(-x, -y), ground);
            }
        }
        int randomTerrainType = 0;
        for (int z = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, z++)
            {
                randomTerrainType = Random.Range(1, 101);
                if (randomTerrainType <= 4)
                {
                    terrainObjects.SetTile(new Vector3Int(x, y), tree);
                    terrainObjects.SetTile(new Vector3Int(-x, y), tree);
                    terrainObjects.SetTile(new Vector3Int(x, -y), tree);
                    terrainObjects.SetTile(new Vector3Int(-x, -y), tree);
                }
            }
        }
    }
}