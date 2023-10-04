using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;
using Debug = UnityEngine.Debug;

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
    public GameObject grid;
    void Start()
    {
        Destroy(grid);
        tiles[0] = null;
        tiles[1] = outCorner;
        tiles[2] = outWall;
        tiles[3] = inCorner;
        tiles[4] = inWall;
        tiles[5] = pellet;
        tiles[6] = pPellet;
        tiles[7] = tJunk;

        Debug.Log(levelMap.GetLength(0));
        Debug.Log(levelMap.GetLength(1));

        
        for (int cols = 0; cols < levelMap.GetLength(0); cols++)
        {
            for (int rows = 0; rows < levelMap.GetLength(1); rows++)
            {
                if (tiles[levelMap[cols,rows]] != null)
                {
                    Instantiate(tiles[levelMap[cols, rows]], new Vector2(rows,-cols),getRotation(levelMap[cols,rows], levelMap,cols,rows));
                }
            }
        }
        Debug.Log("NorthTest: " + tiles[getNorth(levelMap,4,1)].name);
        Debug.Log("EastTest: " + tiles[getEast(levelMap,4,1)].name);
        Debug.Log("NorthTest: " + tiles[getSouth(levelMap,4,1)].name);
        Debug.Log("NorthTest: " + tiles[getWest(levelMap,4,1)].name);
       
    }

    private Quaternion getRotation(int tile, int[,] array, int x, int y)
    {
        Quaternion rotationAngle = Quaternion.Euler(0,0,0);
        
        switch (tile)
        {
            case 1:
                if (getEast(array,x,y) == 2 && getNorth(array,x,y) == 2)
                {
                    rotationAngle = Quaternion.Euler(0,0,90);
                    Debug.Log("90");
                }

                if (getWest(array,x,y) == 2 && getSouth(array,x,y) == 2)
                {
                    rotationAngle = Quaternion.Euler(0,0,-90);
                }
                
                if (getWest(array,x,y) == 2 && getNorth(array,x,y) == 2)
                {
                    rotationAngle = Quaternion.Euler(0,0,180);
                }

                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
        }
        return rotationAngle;
    }

    private int getNorth(int[,] array, int x, int y)
    {
        if (isInBounds(array,x - 1,y))
        {
            return array[x - 1, y];
        }

        return 0;
    }
    
    private int getEast(int[,] array, int x, int y)
    {
        if (isInBounds(array,x,y+1))
        {
            return array[x , y+1];
        }

        return 0;
    }
    
    private int getSouth(int[,] array, int x, int y)
    {
        if (isInBounds(array,x + 1,y))
        {
            return array[x + 1, y];
        }

        return 0;
    }
    
    private int getWest(int[,] array, int x, int y)
    {
        if (isInBounds(array,x,y - 1))
        {
            return array[x, y - 1];
        }

        return 0;
    }
    
    public static bool isInBounds(int[,] array, int x, int y)
    {
        return x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(1);
    }

   
}
