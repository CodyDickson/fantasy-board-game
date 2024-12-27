using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Store : MonoBehaviour
{
    [SerializeField] public Color[] playerColorsListInEditor;
    [SerializeField] public Sprite[] dungeonsListInEditor, villagesListInEditor, monstersListInEditor, playersListInEditor, merchantsListInEditor, objectsListInEditor;
    [SerializeField] public Tilemap[] tilemapsListInEditor;
    public static Color[] playerColors;
    public static Sprite[] dungeons, villages, monsters, players, merchants, objects;

    private void Awake()
    {
        playerColors = playerColorsListInEditor;
        dungeons = dungeonsListInEditor;
        villages = villagesListInEditor;
        monsters = monstersListInEditor;
        players = playersListInEditor;
        merchants = merchantsListInEditor;
        objects = objectsListInEditor;
    }
}
