using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fog : MonoBehaviour
{
    public static List<Vector3> fogPositions = new List<Vector3>();

    public static void GenerateFog()
    {
        Tilemap fog = Store.tilemaps[5];
        Tile fogTile = Store.fogTiles[0];
        int xSize = 50;
        int ySize = 50;
        bool zonePresent;
        bool forestPresent;
        for (int z = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, z++)
            {
                Vector3 position = new Vector3(x, y);
                zonePresent = BoardManager.CheckForZonePositions(position);
                forestPresent = BoardManager.CheckForForestPositions(position);
                if (!zonePresent && !forestPresent)
                {
                    fogPositions.Add(position);
                }
                position = new Vector3(-x, y);
                zonePresent = BoardManager.CheckForZonePositions(position);
                forestPresent = BoardManager.CheckForForestPositions(position);
                if (!zonePresent && !forestPresent)
                {
                    fogPositions.Add(position);
                }
                position = new Vector3(x, -y);
                zonePresent = BoardManager.CheckForZonePositions(position);
                forestPresent = BoardManager.CheckForForestPositions(position);
                if (!zonePresent && !forestPresent)
                {
                    fogPositions.Add(position);
                }
                position = new Vector3(-x, -y);
                zonePresent = BoardManager.CheckForZonePositions(position);
                forestPresent = BoardManager.CheckForForestPositions(position);
                if (!zonePresent && !forestPresent)
                {
                    fogPositions.Add(position);
                }
            }
        }
        foreach (Vector3 fogPosition in fogPositions)
        {
            fog.SetTile(new Vector3Int((int)fogPosition[0], (int)fogPosition[1]), fogTile);
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