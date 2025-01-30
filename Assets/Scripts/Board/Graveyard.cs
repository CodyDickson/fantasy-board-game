using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Graveyard : MonoBehaviour
{
    public static Tile bcVertical, bcHorizontal, bcTopLeftCorner, bcTopRightCorner, bcBottomLeftCorner, bcBottomRightCorner, bcThreeUp, bcThreeDown, bcThreeLeft, bcThreeRight;

    public static void GenerateBoard()
    {
        PullBoardConnectorsFromStore();
        CampDesign();
        GenerateTerrain();
        GenerateZone();
        // GenerateBoardConnectors();
    }

    public static void GenerateBoardConnectors()
    {
        // Board Connectors
        Tilemap tilemap = Store.tilemaps[0];
        int random = Random.Range(2,6);
        Vector3 position = new Vector3(0, random);
        random = Random.Range(12, 26);
        for (int i = 0; i <= random; i++)
        {
            tilemap.SetTile(new Vector3Int((int)position[0], (int)position[1] + i), bcVertical);
            random = Random.Range(1, 8);
            if (random == 1)
            {
                random = Random.Range(1, 3);
                if (random == 1)
                {
                    tilemap.SetTile(new Vector3Int((int)position[0], (int)position[1] + i), bcThreeLeft);
                    GenerateBranch(position, false);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int((int)position[0], (int)position[1] + i), bcThreeRight);
                    GenerateBranch(position, true);
                }
                BoardManager.crossroadPositions.Add(position);
            }
        }
    }

    public static void GenerateZone()
    {
        Tilemap tilemap = Store.tilemaps[0];
        Tile tree = Store.objectTiles[1];
    }

    public static void GenerateBranch(Vector3 position, bool positiveDirection)
    {
        // Board Connectors
        Tilemap tilemap = Store.tilemaps[0];
        if (positiveDirection)
        {
            int random = Random.Range(3, 7);
            for (int i = 1; i <= random; i++)
            {
                tilemap.SetTile(new Vector3Int((int)position[0], (int)position[1] + i), bcHorizontal);
            }
        }
        else
        {
            int random = Random.Range(3, 7);
            for (int i = 1; i <= random; i++)
            {
                tilemap.SetTile(new Vector3Int((int)position[0], (int)position[1] - i), bcHorizontal);
            }
        }
    }

    public static void CampDesign()
    {
        Store.tilemaps[2].SetTile(new Vector3Int(0, 0), Store.objectTiles[0]);
    }

    public static void CampSpawn()
    {
        int random = Random.Range(1, 5);
        switch (random)
        {
            case 1: BoardManager.currentUnitPosition[0] = 1; BoardManager.currentUnitPosition[1] = 2; break;
            case 2: BoardManager.currentUnitPosition[0] = -2; BoardManager.currentUnitPosition[1] = -2; break;
            case 3: BoardManager.currentUnitPosition[0] = -1; BoardManager.currentUnitPosition[1] = -2; break;
            case 4: BoardManager.currentUnitPosition[0] = -2; BoardManager.currentUnitPosition[1] = 2; break;
        }
    }

    public static void GenerateTerrain()
    {
        Tilemap terrain = Store.tilemaps[1];
        Tilemap terrainObjects = Store.tilemaps[2];
        Tile ground = Store.terrainTiles[1];
        Tile tree = Store.objectTiles[1];
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

    public static void PullBoardConnectorsFromStore()
    {
        bcVertical = Store.boardConnectorTiles[0];
        bcHorizontal = Store.boardConnectorTiles[1];
        bcTopLeftCorner = Store.boardConnectorTiles[2];
        bcTopRightCorner = Store.boardConnectorTiles[3];
        bcBottomLeftCorner = Store.boardConnectorTiles[4];
        bcBottomRightCorner = Store.boardConnectorTiles[5];
        bcThreeUp = Store.boardConnectorTiles[6];
        bcThreeDown = Store.boardConnectorTiles[7];
        bcThreeLeft = Store.boardConnectorTiles[8];
        bcThreeRight = Store.boardConnectorTiles[9];
    }
}