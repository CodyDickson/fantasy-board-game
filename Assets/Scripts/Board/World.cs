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
}