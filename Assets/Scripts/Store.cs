using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Store : MonoBehaviour
{
    [SerializeField] public Color[] playerColorsListInEditor;
    [SerializeField] public Tile[] dungeonsListInEditor;
    public static Color[] playerColors;
    public static Tile[] dungeons;

    private void Awake()
    {
        playerColors = playerColorsListInEditor;
        dungeons = dungeonsListInEditor;
    }
}
