using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class World : MonoBehaviour
{
    // Map Settings //
    public static int currentUnitPositionOnBoard = 0;
    public static int previousUnitAvatar = 0;
    public static int newUnitPosition = 0;
    public static bool playerIsMoving;
    //
    public static Vector3 boardPosition;
    public static List<Vector3> boardPositions = new List<Vector3>();
    public static List<Vector3> localBoardPositions = new List<Vector3>();
    public static List<Vector3> boardCrossroads = new List<Vector3>();
    // 1: Weapons, 2: Oddities, 3: Rarities, 4: Consumables
    public static Dictionary<Vector3, int> boardMerchantPositions = new Dictionary<Vector3, int>();
    //
    public static List<Vector3> boardTollPositions = new List<Vector3>();
    public static List<Vector3> boardCampPositions = new List<Vector3>();
    public static List<Vector3> boardImpDungeonPositions = new List<Vector3>();
    public static List<Vector3> boardBasiliskDungeonPositions = new List<Vector3>();
    public static List<Vector3> boardSkeletonDungeonPositions = new List<Vector3>();
    public static List<Vector3> boardGhostDungeonPositions = new List<Vector3>();
    public static Dictionary<Vector3, int> boardPlayerOneVillagePositions = new Dictionary<Vector3, int>();
    public static Dictionary<Vector3, int> boardPlayerTwoVillagePositions = new Dictionary<Vector3, int>();
    public static Dictionary<Vector3, int> boardPlayerThreeVillagePositions = new Dictionary<Vector3, int>();
    public static Dictionary<Vector3, int> boardPlayerFourVillagePositions = new Dictionary<Vector3, int>();
    public static Dictionary<Vector3, int> boardDungeonPositions = new Dictionary<Vector3, int>();
    public static List<Vector3> boardEmptySlotPositions = new List<Vector3>();
    //
    public static List<Vector3> boardSlotPositions = new List<Vector3>();
    public static Vector3 currentUnitPosition;
    public static Vector3 playerOnePosition;
    public static Vector3 playerTwoPosition;
    public static Vector3 playerThreePosition;
    public static Vector3 playerFourPosition;
    public static Dictionary<int, string> boardClockPosition = new Dictionary<int, string>();
    public static Dictionary<int, string> boardLoopDirection = new Dictionary<int, string>();
    public static Dictionary<int, string> boardStructures = new Dictionary<int, string>();
    public static Dictionary<int, string> boardUnits = new Dictionary<int, string>();
    public static int boardLength;
    // Tiles and Tilemaps //
    [SerializeField] public Tile playerRed;
    [SerializeField] public Tile playerBlue;
    [SerializeField] public Tile playerGreen;
    [SerializeField] public Tile playerPurple;
    [SerializeField] public Tile playerWhite;
    [SerializeField] public Tile monsterImp;
    [SerializeField] public Tile monsterBasilisk;
    [SerializeField] public Tile monsterEliteRampagingElephant;
    [SerializeField] public Tile monsterMimic;
    [SerializeField] public Tile monsterSkeleton;
    [SerializeField] public Tile monsterGhost;
    [SerializeField] public Tile monsterEliteSkeletonAmalgamation;
    [SerializeField] public Tile chest;
    [SerializeField] public Tile oddity;
    [SerializeField] public Tile dungeon;
    [SerializeField] public Tile ground, tree;
    [SerializeField] public Tile camp, bcHorizontal, bcThreeDown, bcVertical, bcThreeUp, bcThreeLeft, bcThreeRight, bcTopRightCorner, bcBottomLeftCorner, bcBottomRightCorner, bcTopLeftCorner, emptySlot;
    [SerializeField] public Tilemap tilemapTerrain, tilemapTerrainObjects, tilemapStructures, tilemapUnits, tilemapBoardConnectors;
    [SerializeField] public Tilemap terrain, terrainObjects;
    // Time //
    private float tempCounter2 = 0f;
    private float counter2 = 0.5f;
    // Other //
    public static string currentPlayerColor = "red";
    public static bool crossroadsPosition = false;
    public static int movesRemaining;
    public GUI gui;
    [SerializeField] public Tile fogTile;
    [SerializeField] public Tilemap fog;

    void Start()
    {
        FillEmptySlots(tilemapBoardConnectors, emptySlot);
        TurnManager.TurnProgressionHandler(tilemapStructures);
    }

    void Update()
    {
    }

    public static void MoveUnit()
    {
        GameMain.RollDice();
        GUI.enablePrimaryButton = false;
        GUI.primaryButtonAssignedTo = "";
        movesRemaining = GameMain.diceOneResult + GameMain.diceTwoResult + GameMain.diceThreeResult;
        Debug.Log("Moves Remaining: " + movesRemaining);
        BoardManager.CheckForLocalBoardPositions();
    }

    public static void FillEmptySlots(Tilemap tilemapBoardConnectors, Tile emptySlot)
    {
        foreach (Vector3 listVector in World.boardEmptySlotPositions)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)listVector[0], (int)listVector[1]), emptySlot);
        }
    }
}