using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };

    public GameObject outWall, outCorner, inWall, inCorner, tJunk, pellet, pPellet;
    private Dictionary<int, GameObject> tiles = new Dictionary<int, GameObject>();
    void Start()
    {
        tiles[0] = null;
        tiles[1] = outCorner;
        tiles[2] = outWall;
        tiles[3] = inCorner;
        tiles[4] = inWall;
        tiles[5] = pellet;
        tiles[6] = pPellet;
        tiles[7] = tJunk;
        
        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                if (levelMap[i,j] != 0)
                {
                
                    Instantiate(tiles[levelMap[i,j]], new Vector2(-10 + j,3 - i), Quaternion.identity);
                }
            }
            
        }
        foreach (KeyValuePair<char,GameObject> tile in getNeighbours(levelMap,7,3))
        {
            char key = tile.Key;
            if (tile.Value != null)
            {
                String name = tile.Value.name;
                Debug.Log(key + ": " + name);
            }
            else
            {
                Debug.Log(key + ": Empty.");
            }
        }
    }

    private Dictionary<char,GameObject> getNeighbours(int[,] array, int xPos, int yPos)
    {
        Dictionary<char, GameObject> piece = new Dictionary<char, GameObject>();

        if (isInBounds(array, xPos, yPos + 1))
        {
            piece['N'] = tiles[levelMap[xPos, yPos + 1]];
        }
        if (isInBounds(array, xPos - 1, yPos))
        {
            piece['E'] = tiles[levelMap[xPos - 1, yPos]];
        }
        if (isInBounds(array, xPos, yPos - 1))
        {
            piece['S'] = tiles[levelMap[xPos, yPos - 1]];
        }
        if (isInBounds(array, xPos + 1, yPos))
        {
            piece['W'] = tiles[levelMap[xPos + 1, yPos]];
        }

        return piece;
    }
    
    public static bool isInBounds(int[,] array, int x, int y)
    {
        return x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(1);
    }

   
}
