using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Villages : MonoBehaviour
{
    //
    public int villageCost = 100;
    // Tiles and Tilemaps //
    [SerializeField] public Tile villageRed;
    [SerializeField] public Tile villageBlue;
    [SerializeField] public Tile villageGreen;
    [SerializeField] public Tile villagePurple;
    [SerializeField] public Tile villageWhite;
    [SerializeField] public Tilemap tilmapStructures;

    void Start()
    {
    }

    void Update()
    {   
    }

    public static void BuildVillage(Tilemap tilemap, Tile villageRed, Tile villageBlue, Tile villageGreen, Tile villagePurple, Tile villageWhite)
    {
        GameMain.secondaryButtonEnabled = false;
        World.CheckForLocalVillages();
        if (World.northEmpty)
        {
            Debug.Log("north");
            World.boardPosition = World.northSlotPosition;
        }
        else if (World.eastEmpty)
        {
            Debug.Log("east");
            World.boardPosition = World.eastSlotPosition;
        }
        else if (World.southEmpty)
        {
            Debug.Log("south");
            World.boardPosition = World.southSlotPosition;
        }
        else if (World.westEmpty)
        {
            Debug.Log("west");
            World.boardPosition = World.westSlotPosition;
        }
        Debug.Log(World.currentPlayerColor);
        switch (World.currentPlayerColor)
        {
            case "red": tilemap.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), villageRed); break;
            case "blue": tilemap.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), villageBlue); break;
            case "green": tilemap.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), villageGreen); break;
            case "purple": tilemap.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), villagePurple); break;
            case "white": tilemap.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), villageWhite); break;
        }
    }
}
