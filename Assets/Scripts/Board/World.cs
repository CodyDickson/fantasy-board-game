using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class World : MonoBehaviour
{
    public static Vector3 center;
    public static Tilemap tilemapBoardConnectors;
    public static Tile bcVertical, bcHorizontal, bcTopLeftCorner, bcTopRightCorner, bcBottomLeftCorner, bcBottomRightCorner, bcThreeUp, bcThreeDown, bcThreeLeft, bcThreeRight;

    public static void GenerateWorld()
    {
        PullBoardConnectorsFromStore();
        Camp.CampDesign();
        GenerateTerrain();
        center = new Vector3(0, 0);
        GenerateForest(center, 6, 8);
        GenerateZone(center, 5);
        Fog.GenerateFog();
        // Fog.RemoveLocalFog(0);
        BoardManager.UpdateEmptySlotPositions();
    }

    public static void GenerateTerrain()
    {
        Tilemap terrain = Store.tilemaps[1];
        Tilemap terrainObjects = Store.tilemaps[2];
        Tile ground = Store.terrainTiles[0];
        Tile groundObject = Store.objectTiles[1];
        switch (GameMain.currentBoard)
        {
            case 1:
                ground = Store.terrainTiles[0];
                groundObject = Store.objectTiles[1]; break;
        }
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
        /*int randomTerrainType = 0;
        for (int z = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, z++)
            {
                randomTerrainType = Random.Range(1, 101);
                if (randomTerrainType <= 4)
                {
                    terrainObjects.SetTile(new Vector3Int(x, y), groundObject);
                    terrainObjects.SetTile(new Vector3Int(-x, y), groundObject);
                    terrainObjects.SetTile(new Vector3Int(x, -y), groundObject);
                    terrainObjects.SetTile(new Vector3Int(-x, -y), groundObject);
                }
            }
        }*/
    }

    public static void GenerateForest(Vector3 center, int minSize, int maxSize)
    {
        Tilemap terrainObjects = Store.tilemaps[2];
        Tile tree = Store.objectTiles[1];
        int size = minSize;
        while (size <= maxSize)
        {
            Vector3 topLeftCorner = new Vector3(center[0] - size, center[1] + size);
            Vector3 topRightCorner = new Vector3(center[0] + size, center[1] + size);
            Vector3 bottomRightCorner = new Vector3(center[0] + size, center[1] - size);
            Vector3 bottomLeftCorner = new Vector3(center[0] - size, center[1] - size);
            BoardManager.forestPositions.Add(new Vector3((int)bottomLeftCorner[0], (int)bottomLeftCorner[1]));
            BoardManager.forestPositions.Add(new Vector3((int)bottomRightCorner[0], (int)bottomRightCorner[1]));
            BoardManager.forestPositions.Add(new Vector3((int)topRightCorner[0], (int)topRightCorner[1]));
            BoardManager.forestPositions.Add(new Vector3((int)topLeftCorner[0], (int)topLeftCorner[1]));
            int sideLength = (size * 2);
            for (int i = 0; i <= sideLength; i++)
            {
                terrainObjects.SetTile(new Vector3Int((int)bottomLeftCorner[0] + i, (int)bottomLeftCorner[1]), tree);
                terrainObjects.SetTile(new Vector3Int((int)bottomLeftCorner[0], (int)bottomLeftCorner[1] + i), tree);
                terrainObjects.SetTile(new Vector3Int((int)topLeftCorner[0] + i, (int)topLeftCorner[1]), tree);
                terrainObjects.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + i), tree);
                BoardManager.forestPositions.Add(new Vector3((int)bottomLeftCorner[0] + i, (int)bottomLeftCorner[1]));
                BoardManager.forestPositions.Add(new Vector3Int((int)bottomLeftCorner[0], (int)bottomLeftCorner[1] + i));
                BoardManager.forestPositions.Add(new Vector3Int((int)topLeftCorner[0] + i, (int)topLeftCorner[1]));
                BoardManager.forestPositions.Add(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + i));
            }
            size++;
        }
    }

    public static void GenerateZone(Vector3 center, int size)
    {
        Vector3 topLeftCorner = new Vector3(center[0] - size, center[1] + size);
        Vector3 topRightCorner = new Vector3(center[0] + size, center[1] + size);
        Vector3 bottomRightCorner = new Vector3(center[0] + size, center[1] - size);
        Vector3 bottomLeftCorner = new Vector3(center[0] - size, center[1] - size);
        BoardManager.zonePositions.Add(new Vector3((int)bottomLeftCorner[0], (int)bottomLeftCorner[1]));
        BoardManager.zonePositions.Add(new Vector3((int)bottomRightCorner[0], (int)bottomRightCorner[1]));
        BoardManager.zonePositions.Add(new Vector3((int)topRightCorner[0], (int)topRightCorner[1]));
        BoardManager.zonePositions.Add(new Vector3((int)topLeftCorner[0], (int)topLeftCorner[1]));
        int sideLength = (size * 2);
        for (int i = 0; i <= sideLength; i++)
        {
            for (int j = 0; j < sideLength; j++)
            {
                BoardManager.zonePositions.Add(new Vector3((int)bottomLeftCorner[0] + i, (int)bottomLeftCorner[1]));
                BoardManager.zonePositions.Add(new Vector3Int((int)bottomLeftCorner[0] + j, (int)bottomLeftCorner[1] + i));
                BoardManager.zonePositions.Add(new Vector3Int((int)topLeftCorner[0] + i, (int)topLeftCorner[1]));
                BoardManager.zonePositions.Add(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + i));
            }
        }
    }

    public static void DetermineCampExitPositions()
    {
        /*int rand = Random.Range(1,101);
        if (rand <= 10)
        {
        rand = Random.Range(1, 101);
        if (rand <= 25)
        {
        position = new Vector3Int(0 + i, size);
        }
        else if (rand > 25 && rand <= 50)
        {
        position = new Vector3Int(0 - i, size);
        }
        else if (rand > 50 && rand <= 75)
        {
        position = new Vector3Int(size, 0 + i);
        }
        else if (rand > 75)
        {
        position = new Vector3Int(size, 0 - i);
        }
        }*/
        // Camp.UpdateExitPositions(position);
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
