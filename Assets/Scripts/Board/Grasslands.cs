using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Grasslands : MonoBehaviour
{
    public static Vector3 connectorPosition;
    public static Tilemap tilemapBoardConnectors;
    public static Tile bcVertical, bcHorizontal, bcTopLeftCorner, bcTopRightCorner, bcBottomLeftCorner, bcBottomRightCorner, bcThreeUp, bcThreeDown, bcThreeLeft, bcThreeRight;

    public static void GenerateBoard()
    {
        // GenerateFog();
        // Fog.RemoveLocalFog(0);
        PullBoardConnectorsFromStore();
        CampDesign();
        GenerateTerrain();
        GenerateForest();
        /*for (int i = 1; i <= 8; i++)
        {
            LoopGenerator(i);
        }*/
        BoardManager.UpdateEmptySlotPositions();
    }

    public static void GenerateForest()
    {
        // new List<Vector3> forest = BoardManager.forestTiles;
        Tilemap terrainObjects = Store.tilemaps[2];
        Tile tree = Store.objectTiles[1];
        Vector3 position = new Vector3(0, 0);
        int size = 6;
        for (int i = 0; i <= size; i++)
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
            terrainObjects.SetTile(new Vector3Int(i, size), tree);
            terrainObjects.SetTile(new Vector3Int(-i, size), tree);
            terrainObjects.SetTile(new Vector3Int(size, i), tree);
            terrainObjects.SetTile(new Vector3Int(size, -i), tree);
            terrainObjects.SetTile(new Vector3Int(i, -size), tree);
            terrainObjects.SetTile(new Vector3Int(-i, -size), tree);
            terrainObjects.SetTile(new Vector3Int(-size, i), tree);
            terrainObjects.SetTile(new Vector3Int(-size, -i), tree);
        }
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

    public static void CampDesign()
    {
        Store.tilemaps[2].SetTile(new Vector3Int(0, 0), Store.objectTiles[0]);
    }

    public static void GenerateTerrain()
    {
        Tilemap terrain = Store.tilemaps[1];
        Tilemap terrainObjects = Store.tilemaps[2];
        Tile ground = Store.terrainTiles[0];
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

    public static void LoopGenerator(int clockworkLocation)
    {
        tilemapBoardConnectors = Store.tilemaps[0];
        Vector3 center = new Vector3(0, 0);
        switch (clockworkLocation)
        {
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
        Vector3 topLeftCorner = new Vector3(center[0] - 3, center[1] + 3);
        Vector3 topRightCorner = new Vector3(center[0] + 3, center[1] + 3);
        Vector3 bottomRightCorner = new Vector3(center[0] + 3, center[1] - 3);
        Vector3 bottomLeftCorner = new Vector3(center[0] - 3, center[1] - 3);
        tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0], (int)bottomLeftCorner[1]), bcBottomLeftCorner);
        tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0], (int)topLeftCorner[1]), bcTopLeftCorner);
        tilemapBoardConnectors.SetTile(new Vector3Int((int)topRightCorner[0], (int)topRightCorner[1]), bcTopRightCorner);
        tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1]), bcBottomRightCorner);
        BoardManager.boardPositions.Add(new Vector3((int)bottomLeftCorner[0], (int)bottomLeftCorner[1]));
        BoardManager.boardPositions.Add(new Vector3((int)bottomRightCorner[0], (int)bottomRightCorner[1]));
        BoardManager.boardPositions.Add(new Vector3((int)topRightCorner[0], (int)topRightCorner[1]));
        BoardManager.boardPositions.Add(new Vector3((int)topLeftCorner[0], (int)topLeftCorner[1]));
        for (int i = 1; i <= 5; i++)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0] + i, (int)bottomLeftCorner[1]), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0], (int)bottomLeftCorner[1] + i), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0] + i, (int)topLeftCorner[1]), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + i), bcVertical);
            BoardManager.boardPositions.Add(new Vector3((int)bottomLeftCorner[0] + i, (int)bottomLeftCorner[1]));
            BoardManager.boardPositions.Add(new Vector3Int((int)bottomLeftCorner[0], (int)bottomLeftCorner[1] + i));
            BoardManager.boardPositions.Add(new Vector3Int((int)topLeftCorner[0] + i, (int)topLeftCorner[1]));
            BoardManager.boardPositions.Add(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + i));
        }
        int random = Random.Range(2, 5);
        switch (clockworkLocation)
        {
            case 1:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0] + random, (int)bottomLeftCorner[1]), bcThreeDown);
                connectorPosition = bottomLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(false, 3, connectorPosition);
                //BoardManager.campExitPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] - 3));
                BoardManager.campExitPositionNorth = true; break;
            case 2:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0], (int)bottomLeftCorner[1] + random), bcThreeLeft);
                connectorPosition = bottomLeftCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(false, 6, connectorPosition); break;
            case 3:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0] + random, (int)topLeftCorner[1]), bcThreeUp);
                connectorPosition = topLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(true, 6, connectorPosition);
                random = Random.Range(2,5);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0], (int)bottomLeftCorner[1] + random), bcThreeLeft);
                connectorPosition = bottomLeftCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(false, 3, connectorPosition);
                //BoardManager.campExitPositions.Add(new Vector3((int)connectorPosition[0] - 3, (int)connectorPosition[1]));
                BoardManager.campExitPositionEast = true; break;
            case 4:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0] + random, (int)topLeftCorner[1]), bcThreeUp);
                connectorPosition = topLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(true, 6, connectorPosition); break;
            case 5:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0] + random, (int)topLeftCorner[1]), bcThreeUp);
                connectorPosition = topLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(true, 3, connectorPosition);
                //BoardManager.campExitPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] + 3));
                BoardManager.campExitPositionSouth = true;
                random = Random.Range(2, 5);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeRight);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(true, 6, connectorPosition); break;
            case 6:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeRight);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(true, 6, connectorPosition); break;
            case 7:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeRight);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(true, 3, connectorPosition);
                //BoardManager.campExitPositions.Add(new Vector3((int)connectorPosition[0] + 3, (int)connectorPosition[1]));
                BoardManager.campExitPositionWest = true;
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0] + random, (int)bottomLeftCorner[1]), bcThreeUp);
                connectorPosition = bottomLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(false, 6, connectorPosition); break;
            case 8:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeRight);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(true, 6, connectorPosition);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0] + random, (int)bottomLeftCorner[1]), bcThreeUp);
                connectorPosition = bottomLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(false, 6, connectorPosition); break;
            case 9:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0] + random, (int)bottomLeftCorner[1]), bcThreeUp);
                connectorPosition = bottomLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(false, 6, connectorPosition); break;
            case 10:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeLeft);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(false, 6, connectorPosition);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0] + random, (int)bottomLeftCorner[1]), bcThreeUp);
                connectorPosition = bottomLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(false, 6, connectorPosition); break;
            case 11:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeLeft);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(false, 6, connectorPosition);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0] + random, (int)bottomLeftCorner[1]), bcThreeUp);
                connectorPosition = bottomLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(false, 6, connectorPosition); break;
            case 12:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeLeft);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(false, 6, connectorPosition); break;
            case 13:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeLeft);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(false, 6, connectorPosition);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0] + random, (int)topLeftCorner[1]), bcThreeUp);
                connectorPosition = topLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(true, 6, connectorPosition); break;
            case 14:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeLeft);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(false, 6, connectorPosition);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0] + random, (int)topLeftCorner[1]), bcThreeUp);
                connectorPosition = topLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(true, 6, connectorPosition); break;
            case 15:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeLeft);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(false, 6, connectorPosition);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0] + random, (int)topLeftCorner[1]), bcThreeUp);
                connectorPosition = topLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(true, 6, connectorPosition); break;
            case 16:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0] + random, (int)topLeftCorner[1]), bcThreeUp);
                connectorPosition = topLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(true, 6, connectorPosition); break;
            case 17:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0] + random, (int)topLeftCorner[1]), bcThreeUp);
                connectorPosition = topLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(true, 6, connectorPosition);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeRight);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(true, 6, connectorPosition); break;
            case 18:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0] + random, (int)topLeftCorner[1]), bcThreeUp);
                connectorPosition = topLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(true, 6, connectorPosition);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeRight);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(true, 6, connectorPosition); break;
            case 19:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0] + random, (int)topLeftCorner[1]), bcThreeUp);
                connectorPosition = topLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(true, 6, connectorPosition);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeRight);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(true, 6, connectorPosition); break;
            case 20:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeRight);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(true, 6, connectorPosition); break;
            case 21:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0] + random, (int)bottomLeftCorner[1]), bcThreeUp);
                connectorPosition = bottomLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(false, 6, connectorPosition);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeRight);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(true, 6, connectorPosition); break;
            case 22:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0] + random, (int)bottomLeftCorner[1]), bcThreeUp);
                connectorPosition = bottomLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(false, 6, connectorPosition);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeRight);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(true, 6, connectorPosition); break;
            case 23:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0] + random, (int)bottomLeftCorner[1]), bcThreeUp);
                connectorPosition = bottomLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(false, 6, connectorPosition);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeRight);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(true, 6, connectorPosition); break;
            case 24:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0] + random, (int)bottomLeftCorner[1]), bcThreeUp);
                connectorPosition = bottomLeftCorner;
                connectorPosition[0] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                VerticalConnector(false, 6, connectorPosition);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeRight);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                BoardManager.crossroadPositions.Add(connectorPosition);
                HorizontalConnector(true, 6, connectorPosition); break;
        }
    }

    public static void VerticalConnector(bool positiveDirection, int length, Vector3 connectorPosition)
    {
        tilemapBoardConnectors = Store.tilemaps[0];
        bool straightConnection = true;
        if (positiveDirection && straightConnection)
        {
            for (int i = 1; i <= length; i++)
            {
                tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] + i), bcVertical);
                BoardManager.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] + i));
            }
            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] + length + 1), bcThreeDown);
            BoardManager.crossroadPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] + length + 1));
        }
        if (!positiveDirection && straightConnection)
        {
            for (int i = 1; i <= length; i++)
            {
                tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] - i), bcVertical);
                BoardManager.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] - i));
            }
            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] - length - 1), bcThreeUp);
            BoardManager.crossroadPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] - length - 1));
        }
    }

    public static void HorizontalConnector(bool positiveDirection, int length, Vector3 connectorPosition)
    {
        tilemapBoardConnectors = Store.tilemaps[0];
        bool straightConnection = true;
        if (positiveDirection && straightConnection)
        {
            for (int i = 1; i <= length; i++)
            {
                tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + i, (int)connectorPosition[1]), bcHorizontal);
                BoardManager.boardPositions.Add(new Vector3((int)connectorPosition[0] + i, (int)connectorPosition[1]));
            }
            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + length + 1, (int)connectorPosition[1]), bcThreeLeft);
            BoardManager.crossroadPositions.Add(new Vector3((int)connectorPosition[0] + length + 1, (int)connectorPosition[1]));
        }
        if (!positiveDirection && straightConnection)
        {
            for (int i = 1; i <= length; i++)
            {
                tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - i, (int)connectorPosition[1]), bcHorizontal);
                BoardManager.boardPositions.Add(new Vector3((int)connectorPosition[0] - i, (int)connectorPosition[1]));
            }
            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - length - 1, (int)connectorPosition[1]), bcThreeRight);
            BoardManager.crossroadPositions.Add(new Vector3((int)connectorPosition[0] - length - 1, (int)connectorPosition[1]));
        }
    }

    public static void BranchGenerator(int clockworkLocation, Tilemap tilemapBoardConnectors, Tile camp, Tile bcHorizontal, Tile bcThreeDown, Tile bcVertical, Tile bcThreeUp, Tile bcThreeLeft, Tile bcThreeRight, Tile bcTopRightCorner, Tile bcBottomLeftCorner, Tile bcBottomRightCorner, Tile bcTopLeftCorner)
    {
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
