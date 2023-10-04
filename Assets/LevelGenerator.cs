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
        genrateLevel(generateFullLevel(levelMap));
    }

    private int[,] generateFullLevel(int[,] levelMap)
    {
        int[,] fullLevel = new int[levelMap.GetLength(0), levelMap.GetLength(1) * 2];
        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                fullLevel[i, j] = levelMap[i, j];
            }
        }

        int[,] mirroredArray = new int[levelMap.GetLength(0), levelMap.GetLength(1)];
        
        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                mirroredArray[i, levelMap.GetLength(1) - j - 1 ] = levelMap[i, j];
            }
        }
        
        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                fullLevel[i, j + levelMap.GetLength(1)] = mirroredArray[i, j];
            }
        }

        return fullLevel;
    }

    private void genrateLevel(int [,] level)
    {
        tiles[0] = null;
        tiles[1] = outCorner;
        tiles[2] = outWall;
        tiles[3] = inCorner;
        tiles[4] = inWall;
        tiles[5] = pellet;
        tiles[6] = pPellet;
        tiles[7] = tJunk;

        Debug.Log(level.GetLength(0));
        Debug.Log(level.GetLength(1));

        
        for (int cols = 0; cols < level.GetLength(0); cols++)
        {
            for (int rows = 0; rows < level.GetLength(1); rows++)
            {
                if (tiles[level[cols,rows]] != null)
                {
                    Instantiate(tiles[level[cols, rows]], new Vector2(rows,-cols),getRotation(level[cols,rows], level,cols,rows));
                }
            }
        }
    }

    private Quaternion getRotation(int tile, int[,] array, int x, int y)
    {
        Quaternion rotationAngle = Quaternion.Euler(0,0,0);

        int north = getNorth(array, x, y);
        int east = getEast(array, x, y);
        int south = getSouth(array, x, y);
        int west = getWest(array, x, y);
        
        switch (tile)
        {
            case 1:
                if (east == 2 && north == 2)
                {
                    rotationAngle = Quaternion.Euler(0,0,90);
                }

                if (west == 2 && south == 2)
                {
                    rotationAngle = Quaternion.Euler(0,0,-90);
                }
                
                if (west == 2 && north == 2)
                {
                    rotationAngle = Quaternion.Euler(0,0,180);
                }
                break;
            case 2:
                if (north is 2 or 1)
                {
                    rotationAngle = Quaternion.Euler(0,0, 90);
                }
                break;
            case 3:

                if (south == 4 && east == 4)
                {
                    rotationAngle = Quaternion.Euler(0,0,0);
                }
                else if ((east == 4 && north == 4) || east == 4 && north == 3 || east == 3 && north == 3 || north == 4 && east == 3 || east == 4 && north == 3)
                {
                    rotationAngle = Quaternion.Euler(0,0,90);
                    Debug.Log("90");
                }

                else if (west == 4 && south == 4 || west == 4 && south == 3 || west == 3 && south == 3 || west == 4 && south == 3 || south == 4 && west == 3 )
                {
                    rotationAngle = Quaternion.Euler(0,0,-90);
                }
                
                else if (west == 4 && north == 4 || west == 4 && north == 3 || west == 3 && north == 3 || west == 4 && north == 3 || north == 4 && west == 3 )
                {
                    rotationAngle = Quaternion.Euler(0,0,180);
                }
                break;
            case 4:
                if (north is 4 or 3 && south is 4 or 3)
                {
                    rotationAngle = Quaternion.Euler(0,0, 90);
                }
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
