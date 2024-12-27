using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrasslandsBoard : MonoBehaviour
{
    public static Vector3 connectorPosition;

    public static void GenerateGrasslandsBoard(Tilemap tilemapBoardConnectors, Tile camp, Tile bcHorizontal, Tile bcThreeDown, Tile bcVertical, Tile bcThreeUp, Tile bcThreeLeft, Tile bcThreeRight, Tile bcTopRightCorner, Tile bcBottomLeftCorner, Tile bcBottomRightCorner, Tile bcTopLeftCorner)
    {
        int random;
        string section = "empty";
        for (int i = 1; i <= 24; i++)
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

    public static void HorizontalConnector(bool positiveDirection, int length, Vector3 connectorPosition, Tilemap tilemapBoardConnectors, Tile bcHorizontal, Tile bcTopLeftCorner, Tile bcTopRightCorner, Tile bcBottomRightCorner, Tile bcBottomLeftCorner, Tile bcThreeRight, Tile bcThreeLeft)
    {
        if (positiveDirection)
        {
            int random = Random.Range(1, 3);
            for (int i = 1; i <= length; i++)
            {
                if (i == 1)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + i, (int)connectorPosition[1]), bcHorizontal);
                    World.boardPositions.Add(new Vector3((int)connectorPosition[0] + i, (int)connectorPosition[1]));
                }
                if (i == 2)
                {
                    switch (random)
                    {
                        case 1:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + i, (int)connectorPosition[1]), bcHorizontal);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] + i, (int)connectorPosition[1])); break;
                        case 2:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + i, (int)connectorPosition[1]), bcTopRightCorner);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] + i, (int)connectorPosition[1]));
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + i, (int)connectorPosition[1] - 1), bcBottomLeftCorner);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] + i, (int)connectorPosition[1] + 1)); break;
                    }
                }
                if (i == 3)
                {
                    switch (random)
                    {
                        case 1:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + i, (int)connectorPosition[1]), bcHorizontal);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] + i, (int)connectorPosition[1]));
                            if (length == 3)
                            {
                                tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + 4, (int)connectorPosition[1]), bcThreeLeft);
                                World.boardCrossroads.Add(new Vector3((int)connectorPosition[0] + 4, (int)connectorPosition[1]));
                                World.boardPositions.Add(new Vector3((int)connectorPosition[0] + 4, (int)connectorPosition[1]));
                                World.boardCampPositions.Add(new Vector3((int)connectorPosition[0] + 4, (int)connectorPosition[1]));
                            }
                            break;
                        case 2:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + i, (int)connectorPosition[1] - 1), bcHorizontal);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] + i, (int)connectorPosition[1] - 1));
                            if (length == 3)
                            {
                                tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + 4, (int)connectorPosition[1] - 1), bcThreeLeft);
                                World.boardCrossroads.Add(new Vector3((int)connectorPosition[0] + 4, (int)connectorPosition[1] - 1));
                                World.boardPositions.Add(new Vector3((int)connectorPosition[0] + 4, (int)connectorPosition[1] - 1));
                                World.boardCampPositions.Add(new Vector3((int)connectorPosition[0] + 4, (int)connectorPosition[1] - 1));
                            }
                            break;
                    }
                }
                if (i >= 4)
                {
                    switch (random)
                    {
                        case 1:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + i, (int)connectorPosition[1]), bcHorizontal);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] + i, (int)connectorPosition[1])); break;
                        case 2:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + i, (int)connectorPosition[1] - 1), bcHorizontal);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] + i, (int)connectorPosition[1] - 1)); break;
                    }
                }
            }
            if (length == 6)
            {
                switch (random)
                {
                    case 1:
                        tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + 7, (int)connectorPosition[1]), bcThreeLeft);
                        World.boardCrossroads.Add(new Vector3((int)connectorPosition[0] + 7, (int)connectorPosition[1]));
                        World.boardPositions.Add(new Vector3((int)connectorPosition[0] + 7, (int)connectorPosition[1])); break;
                    case 2:
                        tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + 7, (int)connectorPosition[1] - 1), bcThreeLeft);
                        World.boardCrossroads.Add(new Vector3((int)connectorPosition[0] + 7, (int)connectorPosition[1] - 1));
                        World.boardPositions.Add(new Vector3((int)connectorPosition[0] + 7, (int)connectorPosition[1] - 1)); break;
                }
            }
        }
        else if (!positiveDirection)
        {
            int random = Random.Range(1, 3);
            for (int i = 1; i <= length; i++)
            {
                if (i == 1)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - i, (int)connectorPosition[1]), bcHorizontal);
                    World.boardPositions.Add(new Vector3((int)connectorPosition[0] - i, (int)connectorPosition[1]));
                }
                if (i == 2)
                {
                    switch (random)
                    {
                        case 1:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - i, (int)connectorPosition[1]), bcHorizontal);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] - i, (int)connectorPosition[1])); break;
                        case 2:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - i, (int)connectorPosition[1]), bcTopLeftCorner);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] - i, (int)connectorPosition[1] - 1));
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - i, (int)connectorPosition[1] - 1), bcBottomRightCorner);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] - i, (int)connectorPosition[1] - 1)); break;
                    }
                }
                if (i == 3)
                {
                    switch (random)
                    {
                        case 1:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - i, (int)connectorPosition[1]), bcHorizontal);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] - i, (int)connectorPosition[1]));
                            if (length == 3)
                            {
                                tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - 4, (int)connectorPosition[1]), bcThreeRight);
                                World.boardCrossroads.Add(new Vector3((int)connectorPosition[0] - 4, (int)connectorPosition[1]));
                                World.boardPositions.Add(new Vector3((int)connectorPosition[0] - 4, (int)connectorPosition[1]));
                                World.boardCampPositions.Add(new Vector3((int)connectorPosition[0] - 4, (int)connectorPosition[1]));
                            }
                             break;
                        case 2:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - i, (int)connectorPosition[1] - 1), bcHorizontal);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] - i, (int)connectorPosition[1] - 1));
                            if (length == 3)
                            {
                                tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - 4, (int)connectorPosition[1] - 1), bcThreeRight);
                                World.boardCrossroads.Add(new Vector3((int)connectorPosition[0] - 4, (int)connectorPosition[1] - 1));
                                World.boardPositions.Add(new Vector3((int)connectorPosition[0] - 4, (int)connectorPosition[1] - 1));
                                World.boardCampPositions.Add(new Vector3((int)connectorPosition[0] - 4, (int)connectorPosition[1] - 1));
                            }
                            break;
                    }
                }
                if (i >= 4)
                {
                    switch (random)
                    {
                        case 1:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - i, (int)connectorPosition[1]), bcHorizontal);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] - i, (int)connectorPosition[1])); break;
                        case 2:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - i, (int)connectorPosition[1] - 1), bcHorizontal);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] - i, (int)connectorPosition[1] - 1)); break;
                    }
                }
            }
            if (length == 6)
            {
                switch (random)
                {
                    case 1:
                        tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - 7, (int)connectorPosition[1]), bcThreeRight);
                        World.boardCrossroads.Add(new Vector3((int)connectorPosition[0] - 7, (int)connectorPosition[1]));
                        World.boardPositions.Add(new Vector3((int)connectorPosition[0] - 7, (int)connectorPosition[1])); break;
                    case 2:
                        tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - 7, (int)connectorPosition[1] - 1), bcThreeRight);
                        World.boardCrossroads.Add(new Vector3((int)connectorPosition[0] - 7, (int)connectorPosition[1] - 1));
                        World.boardPositions.Add(new Vector3((int)connectorPosition[0] - 7, (int)connectorPosition[1] - 1)); break;
                }
            }
        }
    }

    public static void VerticalConnector(bool positiveDirection, int length, Vector3 connectorPosition, Tilemap tilemapBoardConnectors, Tile bcVertical, Tile bcBottomLeftCorner, Tile bcTopRightCorner, Tile bcThreeUp, Tile bcThreeDown)
    {
        if (positiveDirection)
        {
            int random = Random.Range(1, 3);
            for (int i = 1; i <= length; i++)
            {
                if (i == 1)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] + i), bcVertical);
                    World.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] + i));
                }
                if (i == 2)
                {
                    switch (random)
                    {
                        case 1:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] + i), bcVertical);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] + i)); break;
                        case 2:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] + i), bcTopRightCorner);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] + i));
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - 1, (int)connectorPosition[1] + i), bcBottomLeftCorner);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] - 1, (int)connectorPosition[1] + i)); break;
                    }
                }
                if (i == 3)
                {
                    switch (random)
                    {
                        case 1:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] + i), bcVertical);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] + i));
                            if (length == 3)
                            {
                                tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] + 4), bcThreeDown);
                                World.boardCrossroads.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] + 4));
                                World.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] + 4));
                                World.boardCampPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] + 4));
                            }
                            break;
                        case 2:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - 1, (int)connectorPosition[1] + i), bcVertical);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] - 1, (int)connectorPosition[1] + i));
                            if (length == 3)
                            {
                                tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - 1, (int)connectorPosition[1] + 4), bcThreeDown);
                                World.boardCrossroads.Add(new Vector3((int)connectorPosition[0] - 1, (int)connectorPosition[1] + 4));
                                World.boardPositions.Add(new Vector3((int)connectorPosition[0] - 1, (int)connectorPosition[1] + 4));
                                World.boardCampPositions.Add(new Vector3((int)connectorPosition[0] - 1, (int)connectorPosition[1] + 4));
                            }
                            break;
                    }
                }
                if (i >= 4)
                {
                    switch (random)
                    {
                        case 1:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] + i), bcVertical);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] + i)); break;
                        case 2:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - 1, (int)connectorPosition[1] + i), bcVertical);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] - 1, (int)connectorPosition[1] + i)); break;
                    }
                }
            }
            if (length == 6)
            {
                switch (random)
                {
                    case 1:
                        tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] + 7), bcThreeDown);
                        World.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] + 7)); break;
                    case 2:
                        tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] - 1, (int)connectorPosition[1] + 7), bcThreeDown);
                        World.boardCrossroads.Add(new Vector3((int)connectorPosition[0] - 1, (int)connectorPosition[1] + 7));
                        World.boardPositions.Add(new Vector3((int)connectorPosition[0] - 1, (int)connectorPosition[1] + 7)); break;
                }
            }
        }
        else if (!positiveDirection)
        {
            int random = Random.Range(1, 3);
            for (int i = 1; i <= length; i++)
            {
                if (i == 1)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] - i), bcVertical);
                    World.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] - i));
                }
                if (i == 2)
                {
                    switch (random)
                    {
                        case 1:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] - i), bcVertical);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] - i)); break;
                        case 2:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] - i), bcBottomLeftCorner);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] - i));
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + 1, (int)connectorPosition[1] - i), bcTopRightCorner);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] + 1, (int)connectorPosition[1] - i)); break;
                    }
                }
                if (i == 3)
                {
                    switch (random)
                    {
                        case 1:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] - i), bcVertical);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] - i));
                            if (length == 3)
                            {
                                tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] - 4), bcThreeUp);
                                World.boardCrossroads.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] - 4));
                                World.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] - 4));
                                World.boardCampPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] - 4));
                            }
                            break;
                        case 2:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + 1, (int)connectorPosition[1] - i), bcVertical);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0] + 1, (int)connectorPosition[1] - i));
                            if (length == 3)
                            {
                                tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + 1, (int)connectorPosition[1] - 4), bcThreeUp);
                                World.boardCrossroads.Add(new Vector3((int)connectorPosition[0] + 1, (int)connectorPosition[1] - 4));
                                World.boardPositions.Add(new Vector3((int)connectorPosition[0] + 1, (int)connectorPosition[1] - 4));
                                World.boardCampPositions.Add(new Vector3((int)connectorPosition[0] + 1, (int)connectorPosition[1] - 4));
                            }
                            break;
                    }
                }
                if (i >= 4)
                {
                    switch (random)
                    {
                        case 1:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] - i), bcVertical);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] - i)); break;
                        case 2:
                            tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + 1, (int)connectorPosition[1] - i), bcVertical);
                            World.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] - i)); break;
                    }
                }
            }
            if (length == 6)
            {
                switch (random)
                {
                    case 1:
                        tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0], (int)connectorPosition[1] - 7), bcThreeUp);
                         World.boardPositions.Add(new Vector3((int)connectorPosition[0], (int)connectorPosition[1] - 7)); break;
                    case 2:
                        tilemapBoardConnectors.SetTile(new Vector3Int((int)connectorPosition[0] + 1, (int)connectorPosition[1] - 7), bcThreeUp);
                        World.boardCrossroads.Add(new Vector3((int)connectorPosition[0] + 1, (int)connectorPosition[1] - 7)); break;
                }
            }
        }
    }

    public static void LoopGenerator(int clockworkLocation, Tilemap tilemapBoardConnectors, Tile camp, Tile bcHorizontal, Tile bcThreeDown, Tile bcVertical, Tile bcThreeUp, Tile bcThreeLeft, Tile bcThreeRight, Tile bcTopRightCorner, Tile bcBottomLeftCorner, Tile bcBottomRightCorner, Tile bcTopLeftCorner)
    {
        Vector3 center = new Vector3(0, 0);
        switch (clockworkLocation)
        {
            case 1: center = new Vector3(0, 13); break;
            case 2: center = new Vector3(13, 13); break;
            case 3: center = new Vector3(13, 0); break;
            case 4: center = new Vector3(13, -13); break;
            case 5: center = new Vector3(0, -13); break;
            case 6: center = new Vector3(-13, -13); break;
            case 7: center = new Vector3(-13, 0); break;
            case 8: center = new Vector3(-13, 13); break;
            case 9: center = new Vector3(-13, 26); break;
            case 10: center = new Vector3(0, 26); break;
            case 11: center = new Vector3(13, 26); break;
            case 12: center = new Vector3(26, 26); break;
            case 13: center = new Vector3(26, 13); break;
            case 14: center = new Vector3(26, 0); break;
            case 15: center = new Vector3(26, -13); break;
            case 16: center = new Vector3(26, -26); break;
            case 17: center = new Vector3(13, -26); break;
            case 18: center = new Vector3(0, -26); break;
            case 19: center = new Vector3(-13, -26); break;
            case 20: center = new Vector3(-26, -26); break;
            case 21: center = new Vector3(-26, -13); break;
            case 22: center = new Vector3(-26, 0); break;
            case 23: center = new Vector3(-26, 13); break;
            case 24: center = new Vector3(-26, 26); break;
        }
        Vector3 topLeftCorner = new Vector3(center[0] - 3, center[1] + 3);
        Vector3 topRightCorner = new Vector3(center[0] + 3, center[1] + 3);
        Vector3 bottomRightCorner = new Vector3(center[0] + 3, center[1] - 3);
        Vector3 bottomLeftCorner = new Vector3(center[0] - 3, center[1] - 3);
        tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0], (int)bottomLeftCorner[1]), bcBottomLeftCorner);
        tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0], (int)topLeftCorner[1]), bcTopLeftCorner);
        tilemapBoardConnectors.SetTile(new Vector3Int((int)topRightCorner[0], (int)topRightCorner[1]), bcTopRightCorner);
        tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1]), bcBottomRightCorner);
        World.boardPositions.Add(new Vector3((int)bottomLeftCorner[0], (int)bottomLeftCorner[1]));
        World.boardPositions.Add(new Vector3((int)bottomRightCorner[0], (int)bottomRightCorner[1]));
        World.boardPositions.Add(new Vector3((int)topRightCorner[0], (int)topRightCorner[1]));
        World.boardPositions.Add(new Vector3((int)topLeftCorner[0], (int)topLeftCorner[1]));
        for (int i = 1; i <= 5; i++)
        {
            tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0] + i, (int)bottomLeftCorner[1]), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0], (int)bottomLeftCorner[1] + i), bcVertical);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0] + i, (int)topLeftCorner[1]), bcHorizontal);
            tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + i), bcVertical);
            World.boardPositions.Add(new Vector3((int)bottomLeftCorner[0] + i, (int)bottomLeftCorner[1]));
            World.boardPositions.Add(new Vector3Int((int)bottomLeftCorner[0], (int)bottomLeftCorner[1] + i));
            World.boardPositions.Add(new Vector3Int((int)topLeftCorner[0] + i, (int)topLeftCorner[1]));
            World.boardPositions.Add(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + i));
        }
        int random = Random.Range(2,5);
        switch (clockworkLocation)
        {
            case 1:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0] + random, (int)bottomLeftCorner[1]), bcThreeDown);
                connectorPosition = bottomLeftCorner;
                connectorPosition[0] += random;
                VerticalConnector(false, 3, connectorPosition, tilemapBoardConnectors, bcVertical, bcBottomLeftCorner, bcTopRightCorner, bcThreeUp, bcThreeDown); break;
            case 2:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0], (int)bottomLeftCorner[1] + random), bcThreeLeft);
                connectorPosition = bottomLeftCorner;
                connectorPosition[1] += random;
                HorizontalConnector(false, 6, connectorPosition, tilemapBoardConnectors, bcHorizontal, bcTopLeftCorner, bcTopRightCorner, bcBottomRightCorner, bcBottomLeftCorner, bcThreeRight, bcThreeLeft); break;
            case 3:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0] + random, (int)topLeftCorner[1]), bcThreeUp);
                connectorPosition = topLeftCorner;
                connectorPosition[0] += random;
                VerticalConnector(true, 6, connectorPosition, tilemapBoardConnectors, bcVertical, bcBottomLeftCorner, bcTopRightCorner, bcThreeUp, bcThreeDown);
                random = Random.Range(2,5);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0], (int)bottomLeftCorner[1] + random), bcThreeLeft);
                connectorPosition = bottomLeftCorner;
                connectorPosition[1] += random;
                HorizontalConnector(false, 3, connectorPosition, tilemapBoardConnectors, bcHorizontal, bcTopLeftCorner, bcTopRightCorner,  bcBottomRightCorner, bcBottomLeftCorner, bcThreeRight, bcThreeLeft); break;
            case 4:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0] + random, (int)topLeftCorner[1]), bcThreeUp);
                connectorPosition = topLeftCorner;
                connectorPosition[0] += random;
                VerticalConnector(true, 6, connectorPosition, tilemapBoardConnectors, bcVertical, bcBottomLeftCorner, bcTopRightCorner, bcThreeUp, bcThreeDown); break;
            case 5:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)topLeftCorner[0] + random, (int)topLeftCorner[1]), bcThreeUp);
                connectorPosition = topLeftCorner;
                connectorPosition[0] += random;
                VerticalConnector(true, 3, connectorPosition, tilemapBoardConnectors, bcVertical, bcBottomLeftCorner, bcTopRightCorner, bcThreeUp, bcThreeDown);
                random = Random.Range(2, 5);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeRight);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                HorizontalConnector(true, 6, connectorPosition, tilemapBoardConnectors, bcHorizontal, bcTopLeftCorner, bcTopRightCorner, bcBottomRightCorner, bcBottomLeftCorner, bcThreeRight, bcThreeLeft); break;
            case 6:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeRight);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                HorizontalConnector(true, 6, connectorPosition, tilemapBoardConnectors, bcHorizontal, bcTopLeftCorner, bcTopRightCorner, bcBottomRightCorner, bcBottomLeftCorner, bcThreeRight, bcThreeLeft); break;
            case 7:
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomRightCorner[0], (int)bottomRightCorner[1] + random), bcThreeRight);
                connectorPosition = bottomRightCorner;
                connectorPosition[1] += random;
                HorizontalConnector(true, 3, connectorPosition, tilemapBoardConnectors, bcHorizontal, bcTopLeftCorner, bcTopRightCorner, bcBottomRightCorner, bcBottomLeftCorner, bcThreeRight, bcThreeLeft);
                tilemapBoardConnectors.SetTile(new Vector3Int((int)bottomLeftCorner[0] + random, (int)bottomLeftCorner[1]), bcThreeUp);
                connectorPosition = bottomLeftCorner;
                connectorPosition[0] += random;
                VerticalConnector(false, 6, connectorPosition, tilemapBoardConnectors, bcVertical, bcBottomLeftCorner, bcTopRightCorner, bcThreeUp, bcThreeDown); break;
        }
        foreach (Vector3 listVector in World.boardPositions)
        {
            Vector3 vector3 = listVector;
            World.currentUnitPosition = listVector;
            World.CheckForLocalBoardPositions();
            if (!World.northPositionAvailable)
            {
                vector3[1] += 1;
                World.boardEmptySlotPositions.Add(vector3);
                vector3 = listVector;
            }
            if (!World.eastPositionAvailable)
            {
                vector3[0] += 1;
                World.boardEmptySlotPositions.Add(vector3);
                vector3 = listVector;
            }
            if (!World.southPositionAvailable)
            {
                vector3[1] -= 1;
                World.boardEmptySlotPositions.Add(vector3);
                vector3 = listVector;
            }
            if (!World.westPositionAvailable)
            {
                vector3[0] -= 1;
                World.boardEmptySlotPositions.Add(vector3);
                vector3 = listVector;
            }
        }
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
