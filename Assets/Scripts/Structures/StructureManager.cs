using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StructureManager : MonoBehaviour
{
    public static void BuildStructure(string direction)
    {
        int player = GameMain.currentPlayer;
        Vector3 position = BoardManager.currentUnitPosition;
        BoardManager.GetLocalSlotPositions();
        if (direction == "north") { position = BoardManager.northPosition; }
        if (direction == "east") { position = BoardManager.eastPosition; }
        if (direction == "south") { position = BoardManager.southPosition; }
        if (direction == "west") { position = BoardManager.westPosition; }
    }
}
