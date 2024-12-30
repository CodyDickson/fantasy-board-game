using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Camp : MonoBehaviour
{
    public static bool playerCurrentlyInCamp;

    public static void GenerateCamp(Tilemap tilemapBoardConnectors, Tile camp, Tile bcHorizontal, Tile bcThreeDown, Tile bcVertical, Tile bcThreeUp, Tile bcThreeLeft, Tile bcThreeRight, Tile bcTopRightCorner, Tile bcBottomLeftCorner, Tile bcBottomRightCorner, Tile bcTopLeftCorner)
    {
        switch(GameMain.currentBoard)
        {
            case 1: GrasslandsCampDesign(tilemapBoardConnectors, camp, bcHorizontal, bcThreeDown, bcVertical, bcThreeUp, bcThreeLeft, bcThreeRight, bcTopRightCorner, bcBottomLeftCorner, bcBottomRightCorner, bcTopLeftCorner); break;
            case 2: GraveyardCampSpawn(); break;
            case 3: VolcanoCampSpawn(); break;
            case 4: MachineCampSpawn(); break;
        }
    }

    public static void SpawnActivePlayerInCamp()
    {
        switch (GameMain.currentBoard)
        {
            case 1: GrasslandsCampSpawn(); break;
            case 2: GraveyardCampSpawn(); break;
            case 3: VolcanoCampSpawn(); break;
            case 4: MachineCampSpawn(); break;
        }
    }

    public static void GrasslandsCampSpawn()
    {
        int random;
        random = Random.Range(1, 5);
        switch (random)
        {
            case 1: World.boardPosition[0] = 1; World.boardPosition[1] = 2; break;
            case 2: World.boardPosition[0] = -2; World.boardPosition[1] = -2; break;
            case 3: World.boardPosition[0] = -1; World.boardPosition[1] = -2; break;
            case 4: World.boardPosition[0] = -2; World.boardPosition[1] = 2; break;
        }
        World.currentUnitPosition = World.boardPosition;
        switch (GameMain.currentPlayer)
        {
            case 1: World.playerOnePosition = World.currentUnitPosition; break;
            case 2: World.playerTwoPosition = World.currentUnitPosition; break;
            case 3: World.playerThreePosition = World.currentUnitPosition; break;
            case 4: World.playerFourPosition = World.currentUnitPosition; break;
        }
        GameMain.playerLives[GameMain.currentPlayer] = GameMain.playerLives[0];
    }

    public static void GrasslandsCampDesign(Tilemap tilemapBoardConnectors, Tile camp, Tile bcHorizontal, Tile bcThreeDown, Tile bcVertical, Tile bcThreeUp, Tile bcThreeLeft, Tile bcThreeRight, Tile bcTopRightCorner, Tile bcBottomLeftCorner, Tile bcBottomRightCorner, Tile bcTopLeftCorner)
    {
        tilemapBoardConnectors.SetTile(new Vector3Int(0, 0), camp);
    }

    public static void GraveyardCampSpawn()
    {
        int random;
        random = Random.Range(1, 3);
        switch (random)
        {
            case 1: World.boardPosition[0] = 1; World.boardPosition[1] = 2; break;
            case 2: World.boardPosition[0] = -2; World.boardPosition[1] = -2; break;
        }
    }

    /*public static void GraveyardCampDesign(Tile dungeon)
    {
        tilemapBoardConnectors.SetTile(new Vector3Int(0, 0), camp);
        int random = Random.Range(1, 3);
        if (random == 1)
        {
            random = Random.Range(1, 3);
            if (random == 1)
            {
                for (int i = 1; i <= 9; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int(i, 0), bcHorizontal);
                }
                random = Random.Range(1, 3);
                if (random == 1)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int(0, 10), bcTopRightCorner);
                    for (int i = -1; i <= -5; i--)
                    {
                        tilemapBoardConnectors.SetTile(new Vector3Int(i, 10), bcVertical);
                    }
                }
                else if (random == 2)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int(0, 10), bcBottomRightCorner);
                    for (int i = 1; i <= 5; i++)
                    {
                        tilemapBoardConnectors.SetTile(new Vector3Int(i, 10), bcVertical);
                    }
                }
            }
        }
        if (random == 2)
        {
            random = Random.Range(1, 3);
            if (random == 1)
            {
                for (int i = 1; i <= 9; i++)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int(0, i), bcVertical);
                }
                random = Random.Range(1, 3);
                if (random == 1)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int(10, 0), bcTopRightCorner);
                    for (int i = -1; i <= -5; i--)
                    {
                        tilemapBoardConnectors.SetTile(new Vector3Int(10, i), bcHorizontal);
                    }
                }
                else if (random == 2)
                {
                    tilemapBoardConnectors.SetTile(new Vector3Int(10, 0), bcTopLeftCorner);
                    for (int i = 1; i <= 5; i++)
                    {
                        tilemapBoardConnectors.SetTile(new Vector3Int(10, i), bcHorizontal);
                    }
                }
            }
        }
    }*/

    public static void VolcanoCampSpawn()
    {
        int random;
        random = Random.Range(1, 3);
        switch (random)
        {
            case 1: World.boardPosition[0] = 1; World.boardPosition[1] = 2; break;
            case 2: World.boardPosition[0] = -2; World.boardPosition[1] = -2; break;
        }
    }

    public static void MachineCampSpawn()
    {
        int random;
        random = Random.Range(1, 3);
        switch (random)
        {
            case 1: World.boardPosition[0] = 1; World.boardPosition[1] = 2; break;
            case 2: World.boardPosition[0] = -2; World.boardPosition[1] = -2; break;
        }
    }
}