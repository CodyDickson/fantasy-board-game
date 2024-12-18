using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrasslandsBoard : MonoBehaviour
{
    public static void GenerateGrasslandsBoard(Tilemap tilemapBoardConnectors, Tile camp, Tile bcHorizontal, Tile bcThreeDown, Tile bcVertical, Tile bcThreeUp, Tile bcThreeLeft, Tile bcThreeRight, Tile bcTopRightCorner, Tile bcBottomLeftCorner, Tile bcBottomRightCorner, Tile bcTopLeftCorner)
    {
        int random;
        string section = "empty";
        for (int i = 1; i <= 3; i++)
        {
            random = Random.Range(1, 10);
            random = 6;
            if (random >= 5)
            {
                section = "loop";
                LoopGenerator(i, tilemapBoardConnectors, camp, bcHorizontal, bcThreeDown, bcVertical, bcThreeUp, bcThreeLeft, bcThreeRight, bcTopRightCorner, bcBottomLeftCorner, bcBottomRightCorner, bcTopLeftCorner);
            }
            else if (random > 1 && random <= 4)
            {
                section = "branch";
                BranchGenerator(i, tilemapBoardConnectors, camp, bcHorizontal, bcThreeDown, bcVertical, bcThreeUp, bcThreeLeft, bcThreeRight, bcTopRightCorner, bcBottomLeftCorner, bcBottomRightCorner, bcTopLeftCorner);
            }
            else if (random == 1)
            {
                section = "empty";
                if (i == 1 || i == 3 || i == 5 || i == 7)
                {
                    section = "loop";
                    LoopGenerator(i, tilemapBoardConnectors, camp, bcHorizontal, bcThreeDown, bcVertical, bcThreeUp, bcThreeLeft, bcThreeRight, bcTopRightCorner, bcBottomLeftCorner, bcBottomRightCorner, bcTopLeftCorner);
                }
            }
            World.boardClockPosition.Add(i, section);
        }
    }

    public static void LoopGenerator(int clockworkLocation, Tilemap tilemapBoardConnectors, Tile camp, Tile bcHorizontal, Tile bcThreeDown, Tile bcVertical, Tile bcThreeUp, Tile bcThreeLeft, Tile bcThreeRight, Tile bcTopRightCorner, Tile bcBottomLeftCorner, Tile bcBottomRightCorner, Tile bcTopLeftCorner)
    {
        Vector3 midPositionOne = new Vector3(0, 0);
        Vector3 cornerPositionOne = new Vector3(0, 0);
        Vector3 midPositionTwo = new Vector3(0, 0);
        Vector3 cornerPositionTwo = new Vector3(0, 0);
        Vector3 midPositionThree = new Vector3(0, 0);
        Vector3 cornerPositionThree = new Vector3(0, 0);
        Vector3 midPositionFour = new Vector3(0, 0);
        Vector3 cornerPositionFour = new Vector3(0, 0);
        int startingLocation = clockworkLocation;
        int shortSide = 2;
        switch (startingLocation)
        {
            case 1: midPositionOne = new Vector3(0, 14); break;
            case 2: midPositionOne = new Vector3(11, 14); break;
            case 3: midPositionOne = new Vector3(11, 3); break;
            case 4: midPositionOne = new Vector3(11, -14); break;
            case 5: midPositionOne = new Vector3(0, -14); break;
        }
        cornerPositionOne = new Vector3(midPositionOne[0] + shortSide + 1, midPositionOne[1]);
        midPositionTwo = new Vector3(cornerPositionOne[0], cornerPositionOne[1] - shortSide - 1);
        cornerPositionTwo = new Vector3(midPositionTwo[0], midPositionTwo[1] - shortSide - 1);
        midPositionThree = new Vector3(cornerPositionTwo[0] - shortSide - 1, cornerPositionTwo[1]);
        cornerPositionThree = new Vector3(midPositionThree[0] - shortSide - 1, midPositionThree[1]);
        midPositionFour = new Vector3(cornerPositionThree[0], cornerPositionThree[1] + shortSide + 1);
        cornerPositionFour = new Vector3(midPositionFour[0], midPositionFour[1] + shortSide + 1);
        int random;
        random = Random.Range(1, 3);
        if (startingLocation == 5)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]), bcThreeUp);
            World.boardCrossroads.Add(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]));
        }
        else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]), bcHorizontal);
            World.boardEmptySlotPositions.Add(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1] - 1));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]), bcThreeUp);
            World.boardCrossroads.Add(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]));
        }
        random = Random.Range(1, 3);
        if (startingLocation == 7)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]), bcThreeRight);
            World.boardCrossroads.Add(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]));
        }
        else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]), bcVertical);
            World.boardEmptySlotPositions.Add(new Vector3Int((int)midPositionTwo[0] - 1, (int)midPositionTwo[1]));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]), bcThreeRight);
            World.boardCrossroads.Add(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]));
        }
        random = Random.Range(1, 3);
        if (startingLocation == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]), bcThreeDown);
            World.boardCrossroads.Add(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]));
        }
        else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]), bcHorizontal);
            World.boardEmptySlotPositions.Add(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1] + 1));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]), bcThreeDown);
            World.boardCrossroads.Add(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]));
        }
        random = Random.Range(1, 3);
        if (startingLocation == 3)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]), bcThreeLeft);
            World.boardCrossroads.Add(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]));
        }
        else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]), bcVertical);
            World.boardEmptySlotPositions.Add(new Vector3Int((int)midPositionFour[0] + 1, (int)midPositionFour[1]));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]), bcThreeLeft);
            World.boardCrossroads.Add(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]));
        }
        for (int i = 1; i <= shortSide; i++)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionThree[0] + i, (int)cornerPositionThree[1]), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionThree[0], (int)cornerPositionThree[1] + i), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionTwo[0], (int)cornerPositionTwo[1] + i), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionFour[0] + i, (int)cornerPositionFour[1]), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionOne[0] + i, (int)midPositionOne[1]), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0] + i, (int)midPositionThree[1]), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1] + i), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1] + i), bcVertical);
            World.boardPositions.Add(new Vector3((int)cornerPositionThree[0] + i, (int)cornerPositionThree[1]));
            World.boardPositions.Add(new Vector3Int((int)cornerPositionThree[0], (int)cornerPositionThree[1] + i));
            World.boardPositions.Add(new Vector3Int((int)cornerPositionTwo[0], (int)cornerPositionTwo[1] + i));
            World.boardPositions.Add(new Vector3Int((int)cornerPositionFour[0] + i, (int)cornerPositionFour[1]));
            World.boardPositions.Add(new Vector3Int((int)midPositionOne[0] + i, (int)midPositionOne[1]));
            World.boardPositions.Add(new Vector3Int((int)midPositionThree[0] + i, (int)midPositionThree[1]));
            World.boardPositions.Add(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1] + i));
            World.boardPositions.Add(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1] + i));
            World.boardEmptySlotPositions.Add(new Vector3Int((int)cornerPositionThree[0] + i, (int)cornerPositionThree[1] + 1));
            World.boardEmptySlotPositions.Add(new Vector3Int((int)cornerPositionThree[0] + 1, (int)cornerPositionThree[1] + i));
            World.boardEmptySlotPositions.Add(new Vector3Int((int)cornerPositionTwo[0] - 1, (int)cornerPositionTwo[1] + i));
            World.boardEmptySlotPositions.Add(new Vector3Int((int)cornerPositionFour[0] + i, (int)cornerPositionFour[1] - 1));
            World.boardEmptySlotPositions.Add(new Vector3Int((int)midPositionOne[0] + i, (int)midPositionOne[1] - 1));
            World.boardEmptySlotPositions.Add(new Vector3Int((int)midPositionThree[0] + i, (int)midPositionThree[1] + 1));
            World.boardEmptySlotPositions.Add(new Vector3Int((int)midPositionTwo[0] - 1, (int)midPositionTwo[1] + i));
            World.boardEmptySlotPositions.Add(new Vector3Int((int)midPositionFour[0] + 1, (int)midPositionFour[1] + i));
        }
        tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionThree[0], (int)cornerPositionThree[1]), bcBottomLeftCorner);
        tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionFour[0], (int)cornerPositionFour[1]), bcTopLeftCorner);
        tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionOne[0], (int)cornerPositionOne[1]), bcTopRightCorner);
        tilemapBoardConnectors.SetTile(new Vector3Int((int)cornerPositionTwo[0], (int)cornerPositionTwo[1]), bcBottomRightCorner);
        World.boardPositions.Add(new Vector3((int)midPositionOne[0], (int)midPositionOne[1]));
        World.boardPositions.Add(new Vector3((int)cornerPositionOne[0], (int)cornerPositionOne[1]));
        World.boardPositions.Add(new Vector3((int)midPositionTwo[0], (int)midPositionTwo[1]));
        World.boardPositions.Add(new Vector3((int)cornerPositionTwo[0], (int)cornerPositionTwo[1]));
        World.boardPositions.Add(new Vector3((int)midPositionThree[0], (int)midPositionThree[1]));
        World.boardPositions.Add(new Vector3((int)cornerPositionThree[0], (int)cornerPositionThree[1]));
        World.boardPositions.Add(new Vector3((int)midPositionFour[0], (int)midPositionFour[1]));
        World.boardPositions.Add(new Vector3((int)cornerPositionFour[0], (int)cornerPositionFour[1]));
    }

    public static void BranchGenerator(int clockworkLocation, Tilemap tilemapBoardConnectors, Tile camp, Tile bcHorizontal, Tile bcThreeDown, Tile bcVertical, Tile bcThreeUp, Tile bcThreeLeft, Tile bcThreeRight, Tile bcTopRightCorner, Tile bcBottomLeftCorner, Tile bcBottomRightCorner, Tile bcTopLeftCorner)
    {
        /*Vector3 midPositionOne = new Vector3(0, 0);
        Vector3 midPositionTwo = new Vector3(0, 0);
        Vector3 midPositionThree = new Vector3(0, 0);
        Vector3 midPositionFour = new Vector3(0, 0);
        int startingLocation = clockworkLocation;
        int random;
        if (startingLocation == 1)
        {
            midPositionThree = new Vector3(0, 4);
            midPositionOne = new Vector3(midPositionThree[0], midPositionThree[1] + 7);
            midPositionFour = new Vector3(midPositionThree[0] - 4, midPositionThree[1] + 4);
            midPositionTwo = new Vector3(midPositionThree[0] + 4, midPositionThree[1] + 4);
        }
        if (startingLocation == 2)
        {
            midPositionThree = new Vector3(7, 4);
            if (World.boardClockPosition[3] == "loop")
            {
                Debug.Log("pass");
                World.tilemapBoardConnectors.SetTile(new Vector3Int(7, 3), bcThreeUp);
            }
            for (int i = 0; i <= 7; i++)
            {
                World.tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1] + i), bcVertical);
            }
        }
        if (startingLocation == 3)
        {
            midPositionFour = new Vector3(4, 0);
            midPositionOne = new Vector3(cornerPositionFour[0] + 3, cornerPositionFour[1]);
            midPositionTwo = new Vector3(cornerPositionOne[0], cornerPositionOne[1] - 3);
            midPositionThree = new Vector3(cornerPositionTwo[0] - 3, cornerPositionTwo[1]);
        }
        if (startingLocation == 5)
        {
            midPositionOne = new Vector3(0, -4);
            midPositionTwo = new Vector3(cornerPositionOne[0], cornerPositionOne[1] - 3);
            midPositionThree = new Vector3(cornerPositionTwo[0] - 3, cornerPositionTwo[1]);
            midPositionFour = new Vector3(cornerPositionThree[0], cornerPositionThree[1] + 3);
        }
        if (startingLocation == 7)
        {
            midPositionTwo = new Vector3(-4, 0);
            midPositionThree = new Vector3(cornerPositionTwo[0] - 3, cornerPositionTwo[1]);
            midPositionFour = new Vector3(cornerPositionThree[0], cornerPositionThree[1] + 3);
            midPositionOne = new Vector3(cornerPositionFour[0] + 3, cornerPositionFour[1]);
        }
        random = Random.Range(1, 3);
        if (startingLocation == 5)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]), bcThreeUp);
            boardCrossroads.Add(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]));
        }
        else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]), bcHorizontal);
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1] - 1));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]), bcThreeUp);
            boardCrossroads.Add(new Vector3Int((int)midPositionOne[0], (int)midPositionOne[1]));
        }
        random = Random.Range(1, 3);
        if (startingLocation == 7)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]), bcThreeRight);
            boardCrossroads.Add(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]));
        }
        else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]), bcVertical);
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionTwo[0] - 1, (int)midPositionTwo[1]));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]), bcThreeRight);
            boardCrossroads.Add(new Vector3Int((int)midPositionTwo[0], (int)midPositionTwo[1]));
        }
        random = Random.Range(1, 3);
        if (startingLocation == 3)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]), bcThreeLeft);
            boardCrossroads.Add(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]));
        }
        else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]), bcVertical);
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionFour[0] + 1, (int)midPositionFour[1]));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]), bcThreeLeft);
            boardCrossroads.Add(new Vector3Int((int)midPositionFour[0], (int)midPositionFour[1]));
        }
        random = Random.Range(1, 3);
        if (startingLocation == 1)
        {
            for (int i = 0; i <= 7; i++)
            {
                World.tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1] + i), bcVertical);
            }
            // boardCrossroads.Add(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]));
        }
        /*else if (random == 1)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]), bcHorizontal);
            boardEmptySlotPositions.Add(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1] + 1));
        }
        else if (random == 2)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]), bcThreeDown);
            boardCrossroads.Add(new Vector3Int((int)midPositionThree[0], (int)midPositionThree[1]));
        }*/
    }

    public void SpawnDungeons(Tile dungeon)
    {
        /*for (int i = 0; i < World.boardEmptySlotPositions.Count; i++)
        {
            int randomDungeon = Random.Range(1, 101);
            if (randomDungeon <= 15)
            {
                int randomDungeonType = Random.Range(1, 3);
                if (randomDungeonType == 1)
                {
                    World.boardImpDungeonPositions.Add(World.boardEmptySlotPositions[i]);
                }
                else if (randomDungeonType == 2)
                {
                    World.boardBasiliskDungeonPositions.Add(World.boardEmptySlotPositions[i]);
                }
                World.boardEmptySlotPositions.RemoveAt(i);
            }
        }
        for (int i = 0; i < World.boardImpDungeonPositions.Count; i++)
        {
            World.boardPosition = World.boardImpDungeonPositions[i];
            World.tilemapStructures.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), dungeon);
        }
        for (int i = 0; i < World.boardBasiliskDungeonPositions.Count; i++)
        {
            World.boardPosition = World.boardBasiliskDungeonPositions[i];
            World.tilemapStructures.SetTile(new Vector3Int((int)World.boardPosition[0], (int)World.boardPosition[1]), dungeon);
        }*/
    }
}
