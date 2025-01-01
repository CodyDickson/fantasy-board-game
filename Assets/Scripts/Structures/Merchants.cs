using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Merchants : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public static void SpawnMerchants(Tilemap tilemapStructures)
    {
        /*foreach (Vector3 listVector in World.boardPositions)
        {
            Vector3 vector3 = listVector;
            World.currentUnitPosition = listVector;
            World.CheckForLocalBoardPositions();
            int random;
            if (!World.northPositionAvailable)
            {
                random = Random.Range(1, 101);
                if (random == 1)
                {
                    vector3[1] += 1;
                    random = Random.Range(1, 4);
                    World.boardMerchantPositions.Add(vector3, random);
                    vector3 = listVector;
                }
            }
            if (!World.eastPositionAvailable)
            {
                random = Random.Range(1, 101);
                if (random == 1)
                {
                    vector3[0] += 1;
                    random = Random.Range(1, 4);
                    World.boardMerchantPositions.Add(vector3, random);
                    vector3 = listVector;
                }
            }
            if (!World.southPositionAvailable)
            {
                random = Random.Range(1, 101);
                if (random == 1)
                {
                    vector3[1] -= 1;
                    random = Random.Range(1, 4);
                    World.boardMerchantPositions.Add(vector3, random);
                }
            }
            if (!World.westPositionAvailable)
            {
                random = Random.Range(1, 101);
                if (random == 1)
                {
                    vector3[0] -= 1;
                    random = Random.Range(1, 4);
                    World.boardMerchantPositions.Add(vector3, random);
                    vector3 = listVector;
                }
            }
        }
        foreach (var merchant in World.boardMerchantPositions) { tilemapStructures.SetTile(new Vector3Int((int)merchant.Key[0], (int)merchant.Key[1]), Store.merchantTiles[merchant.Value]); }
        continueTurnProgression = true;*/
    }
}
