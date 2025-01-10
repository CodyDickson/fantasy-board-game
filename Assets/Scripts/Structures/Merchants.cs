using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Merchants : MonoBehaviour
{
    public static int merchantCount;
    public static int merchantType = 1;
    public static Dictionary<Vector3, int> merchantPositions = new Dictionary<Vector3, int>();

    private void Update()
    {
        
    }

    public static void OpenShop()
    {

    }

    public static void SpawnMerchants(int location)
    {
        Tilemap structures = Store.tilemaps[3];
        Vector3 center = new Vector3(0, 0);
        center = BoardManager.CheckClockworkPosition(location);
        int random;
        merchantCount = 0;
        if (GameMain.currentBoard == 1)
        {
            int xSize = 6;
            int ySize = 6;
            while (merchantCount < 3)
            {
                for (int z = 0, y = 0; y <= ySize; y++)
                {
                    for (int x = 0; x <= xSize; x++, z++)
                    {
                        Vector3 positionCheckOne = new Vector3Int((int)center[0] + x, (int)center[1] + y);
                        Vector3 positionCheckTwo = new Vector3Int((int)center[0] - x, (int)center[1] + y);
                        Vector3 positionCheckThree = new Vector3Int((int)center[0] + x, (int)center[1] - y);
                        Vector3 positionCheckFour = new Vector3Int((int)center[0] - x, (int)center[1] - y);
                        foreach (Vector3 slot in BoardManager.emptyBoardSlots)
                        {
                            if (slot == positionCheckOne && merchantCount < 3)
                            {
                                random = Random.Range(1, 101);
                                if (random <= 10) { structures.SetTile(new Vector3Int((int)positionCheckOne[0], (int)positionCheckOne[1]), Store.merchantTiles[0]); merchantCount++; merchantPositions.Add(positionCheckOne, merchantType); BoardManager.RemoveEmptySlot(positionCheckOne); }
                            }
                            if (slot == positionCheckTwo && merchantCount < 3)
                            {
                                random = Random.Range(1, 101);
                                if (random <= 10) { structures.SetTile(new Vector3Int((int)positionCheckTwo[0], (int)positionCheckTwo[1]), Store.merchantTiles[0]); merchantCount++; merchantPositions.Add(positionCheckTwo, merchantType); BoardManager.RemoveEmptySlot(positionCheckTwo); }
                            }
                            if (slot == positionCheckThree && merchantCount < 3)
                            {
                                random = Random.Range(1, 101);
                                if (random <= 10) { structures.SetTile(new Vector3Int((int)positionCheckThree[0], (int)positionCheckThree[1]), Store.merchantTiles[0]); merchantCount++; merchantPositions.Add(positionCheckThree, merchantType); BoardManager.RemoveEmptySlot(positionCheckThree); }
                            }
                            if (slot == positionCheckFour && merchantCount < 3)
                            {
                                random = Random.Range(1, 101);
                                if (random <= 10) { structures.SetTile(new Vector3Int((int)positionCheckFour[0], (int)positionCheckFour[1]), Store.merchantTiles[0]); merchantCount++; merchantPositions.Add(positionCheckFour, merchantType); BoardManager.RemoveEmptySlot(positionCheckFour); }
                            }
                        }
                    }
                }
            }
        }
    }
}
