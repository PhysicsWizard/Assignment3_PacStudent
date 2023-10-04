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
                    getRotation(levelMap[i,j],new Vector2(-10 + j,3 - i),levelMap);
                }
            }
        }
    }

    private void getRotation(int tileKey, Vector2 position, int[,] array)
    {
        Dictionary<char, GameObject> neighbours = getAdjacent(position,array);
    }

    private Dictionary<char,GameObject> getAdjacent(Vector2 position, int[,] array)
    {
        Dictionary<char, GameObject> cardinals = new Dictionary<char, GameObject>();

        Vector2 north = position - new Vector2(0, 1) ;
        Vector2 east = position + new Vector2(1,0);
        Vector2 south = position + new Vector2(0, 1);
        Vector2 west = position - new Vector2(1, 0);
        
        cardinals['N'] = null;
        
        cardinals['E'] = null;
        
        cardinals['S'] = null;
        
        cardinals['W'] = null;

        if (isInBounds(array,north))
        {
            cardinals['N'] = tiles[array[(int)north.x,(int)north.y]];
        }

        if (isInBounds(array,east))
        {
            cardinals['E'] = tiles[array[(int)east.x,(int)east.y]];
        }
        
        if (isInBounds(array,south))
        {
            cardinals['S'] = tiles[array[(int)south.x,(int)south.y]];
        }
        
        if (isInBounds(array,west))
        {
            cardinals['W'] = tiles[array[(int)west.x,(int)west.y]];
        }
        
        foreach (KeyValuePair<char,GameObject> piece in cardinals)
        {
            char key = piece.Key;
            if (piece.Value != null)
            {
                String name = piece.Value.name;
                Debug.Log(key +": " + name);
            }
            else
            {
                Debug.Log(key +": " + "Empty"); 
            }
        }
        return cardinals;
    }

    private bool isInBounds(int[,] array, Vector2 newPos)
    {
        return newPos.x >= 0 && newPos.x < array.GetLength(0) && newPos.y >= 0 && newPos.y < array.GetLength(1);
    }
}
