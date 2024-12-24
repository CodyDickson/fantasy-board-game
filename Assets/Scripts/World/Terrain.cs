using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Terrain : MonoBehaviour
{
    public static void GenerateGrasslandsTerrain(Tilemap tilemapTerrain, Tilemap tilemapTerrainObjects, Tile grass, Tile grassObject, Tile grassObject2)
    {
        int randomTerrainType = 0;
        int xSize = 50;
        int ySize = 50;
        for (int z = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, z++)
            {
                randomTerrainType = Random.Range(1, 101);
                if (randomTerrainType <= 1)
                {
                    tilemapTerrain.SetTile(new Vector3Int(x, y), grass);
                    tilemapTerrain.SetTile(new Vector3Int(-x, y), grass);
                    tilemapTerrain.SetTile(new Vector3Int(x, -y), grass);
                    tilemapTerrain.SetTile(new Vector3Int(-x, -y), grass);
                }
                else if (randomTerrainType > 2)
                {
                    tilemapTerrain.SetTile(new Vector3Int(x, y), null);
                    tilemapTerrain.SetTile(new Vector3Int(-x, y), null);
                    tilemapTerrain.SetTile(new Vector3Int(x, -y), null);
                    tilemapTerrain.SetTile(new Vector3Int(-x, -y), null);
                }
                randomTerrainType = Random.Range(1,101);
                if (randomTerrainType <= 10)
                {
                    tilemapTerrainObjects.SetTile(new Vector3Int(x, y), grassObject);
                    tilemapTerrainObjects.SetTile(new Vector3Int(-x, y), grassObject);
                    tilemapTerrainObjects.SetTile(new Vector3Int(x, -y), grassObject);
                    tilemapTerrainObjects.SetTile(new Vector3Int(-x, -y), grassObject);
                }
                else if (randomTerrainType <= 20 && randomTerrainType > 10)
                {
                    tilemapTerrainObjects.SetTile(new Vector3Int(x, y), grassObject2);
                    tilemapTerrainObjects.SetTile(new Vector3Int(-x, y), grassObject2);
                    tilemapTerrainObjects.SetTile(new Vector3Int(x, -y), grassObject2);
                    tilemapTerrainObjects.SetTile(new Vector3Int(-x, -y), grassObject2);
                }
            }
        }
    }
}