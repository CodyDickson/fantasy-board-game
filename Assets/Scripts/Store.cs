using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Store : MonoBehaviour
{
    [SerializeField] public Color[] playerColorsListInEditor;
    [SerializeField] public Sprite[] GUIElementsInEditor, dungeonSpritesListInEditor, villageSpritesListInEditor, monsterSpritesListInEditor, playerSpritesListInEditor, merchantSpritesListInEditor, objectSpritesListInEditor, diceSpritesInEditor, weaponSpritesListInEditor;
    [SerializeField] public Tile[] dungeonTilesListInEditor, villageTilesListInEditor, monsterTilesListInEditor, playerTilesListInEditor, merchantTilesListInEditor, objectTilesListInEditor, boardConnectorTilesInEditor, fogTilesInEditor, terrainTilesInEditor;
    [SerializeField] public Tilemap[] tilemapsListInEditor;
    public static Color[] playerColors;
    public static Sprite[] GUIElements, dungeonSprites, villageSprites, monsterSprites, playerSprites, merchantSprites, objectSprites, diceSprites, weaponSprites;
    public static Tile[] dungeonTiles, villageTiles, monsterTiles, playerTiles, merchantTiles, objectTiles, boardConnectorTiles, fogTiles, terrainTiles;
    public static Tilemap[] tilemaps;

    private void Awake()
    {
        playerColors = playerColorsListInEditor;
        dungeonSprites = dungeonSpritesListInEditor;
        villageSprites = villageSpritesListInEditor;
        monsterSprites = monsterSpritesListInEditor;
        playerSprites = playerSpritesListInEditor;
        merchantSprites = merchantSpritesListInEditor;
        objectSprites = objectSpritesListInEditor;
        dungeonTiles = dungeonTilesListInEditor;
        villageTiles = villageTilesListInEditor;
        monsterTiles = monsterTilesListInEditor;
        playerTiles = playerTilesListInEditor;
        merchantTiles = merchantTilesListInEditor;
        objectTiles = objectTilesListInEditor;
        boardConnectorTiles = boardConnectorTilesInEditor;
        fogTiles = fogTilesInEditor;
        terrainTiles = terrainTilesInEditor;
        tilemaps = tilemapsListInEditor;
        GUIElements = GUIElementsInEditor;
        diceSprites = diceSpritesInEditor;
        weaponSprites = weaponSpritesListInEditor;
    }
}
