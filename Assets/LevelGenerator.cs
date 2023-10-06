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

    public GameObject outWall, outCorner, inWall, inCorner, tJunk, pellet, pPellet, highlight;
    private Dictionary<int, GameObject> tiles = new Dictionary<int, GameObject>();
    private Dictionary<Vector2, GameObject> mapObjects = new Dictionary<Vector2, GameObject>();
    public GameObject grid;
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

        if (grid != null)
        {
            Destroy(grid);
        }
        genrateLevel(drawFullLevel(levelMap));
    }

    private int[,] drawFullLevel(int[,] levelMap)
    {
        int[,] fullLevel = new int[levelMap.GetLength(0)-1, levelMap.GetLength(1) * 2];
        fullLevel = flipVertical(flipHorizontal(levelMap));
        return fullLevel;
    }
    
    private int[,] flipVertical(int[,] levelMap)
    {
        int[,] fullLevel = new int[(levelMap.GetLength(0)) * 2, levelMap.GetLength(1)];
        
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
                mirroredArray[levelMap.GetLength(0) - i - 1, j] = levelMap[i, j];
            }
        }
        
        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                fullLevel[i  + levelMap.GetLength(0)-1, j] = mirroredArray[i, j];
            }
        }

        return fullLevel;
    }

    private int[,] flipHorizontal(int[,] levelMap)
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
        for (int cols = 0; cols < level.GetLength(0); cols++)
        {
            for (int rows = 0; rows < level.GetLength(1); rows++)
            {
                if (tiles[level[cols,rows]] != null)
                {
                    mapObjects[new Vector2(cols, rows)] = Instantiate(tiles[level[cols, rows]], new Vector2(rows,-cols),getRotation(level[cols,rows], level,cols,rows));
                }
            }
        }
        
        foreach (KeyValuePair<Vector2, GameObject> tile in mapObjects)
        {
            fixRotation(tile.Value, (int)tile.Key.x,(int)tile.Key.y);
        }
    }

    private void fixRotation(GameObject currentTile, int x, int y)
    {
        GameObject northObj = getNorthDic(mapObjects, x, y);
        GameObject eastObj = getEastDic(mapObjects, x, y);
        GameObject southObj = getSouthDic(mapObjects, x, y);
        GameObject westObj = getWestDic(mapObjects, x, y);
        
        switch (currentTile.tag)
        {
            case "outCorner":
                break;
            case "outWall":
                break;
            case "inCorner":
                if (northObj != null && eastObj != null && eastObj.tag.Equals("inCorner") && northObj.tag.Equals("inWall") && northObj.transform.rotation == Quaternion.identity)
                {
                    currentTile.transform.rotation = Quaternion.Euler(0, 0, -90);
                }

                if (northObj != null && eastObj != null && eastObj.tag.Equals("inWall") && eastObj.transform.rotation != Quaternion.identity)
                {
                    currentTile.transform.rotation = Quaternion.Euler(0, 0, -90);
                }
                if (northObj != null && eastObj != null && eastObj.tag.Equals("inWall") && eastObj.transform.rotation != Quaternion.identity && northObj.tag.Equals("inWall") && northObj.transform.rotation != Quaternion.identity)
                {
                    currentTile.transform.rotation = Quaternion.Euler(0, 0,180);
                }

                if ((northObj != null && westObj != null) && (northObj.tag.Equals("inWall") &&
                                                              northObj.transform.rotation != Quaternion.identity) && westObj.tag.Equals("inWall") && westObj.transform.rotation == Quaternion.identity)
                {
                    currentTile.transform.rotation = Quaternion.Euler(0, 0,180);
                }
                if ((northObj != null && eastObj != null) && (northObj.tag.Equals("inWall") &&
                                                              northObj.transform.rotation != Quaternion.identity) && eastObj.tag.Equals("inWall") && eastObj.transform.rotation == Quaternion.identity)
                {
                    currentTile.transform.rotation = Quaternion.Euler(0, 0,90);
                }
                break;
            case "inWall":
                break;
            case "tJunk":
                break;
                default:
                break;
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

                else if (west == 2 && south == 2)
                {
                    rotationAngle = Quaternion.Euler(0,0,-90);
                }
                
                else if (west == 2 && north == 2)
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
                        rotationAngle = Quaternion.Euler(0, 0, 90);
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

                if (north == 7 || south == 7)
                {
                    rotationAngle = Quaternion.Euler(0,0, 90);
                }
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                if (north == 4)
                {
                    rotationAngle = Quaternion.Euler(0,0,180);
                }
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
    
    private GameObject getNorthDic(Dictionary<Vector2, GameObject> dict, int x, int y)
    {
        if (dict.ContainsKey(new Vector2(x -1, y)))
        {
            return dict[new Vector2(x -1, y)];
        }
        return null;
    }
    
    private int getEast(int[,] array, int x, int y)
    {
        if (isInBounds(array,x,y+1))
        {
            return array[x , y+1];
        }

        return 0;
    }
    
    private GameObject getEastDic(Dictionary<Vector2, GameObject> dict, int x, int y)
    {
        if (dict.ContainsKey(new Vector2(x, y+1)))
        {
            return dict[new Vector2(x, y+1)];
        }

        return null;
    }
    
    private int getSouth(int[,] array, int x, int y)
    {
        if (isInBounds(array,x + 1,y))
        {
            return array[x + 1, y];
        }

        return 0;
    }
    
    private GameObject getSouthDic(Dictionary<Vector2, GameObject> dict, int x, int y)
    {
        if (dict.ContainsKey(new Vector2(x + 1, y)))
        {
            return dict[new Vector2(x + 1, y)];
        }

        return null;
    }
    
    private int getWest(int[,] array, int x, int y)
    {
        if (isInBounds(array,x,y - 1))
        {
            return array[x, y - 1];
        }

        return 0;
    }
    
    private GameObject getWestDic(Dictionary<Vector2, GameObject> dict, int x, int y)
    {
        if (dict.ContainsKey(new Vector2(x, y -1)))
        {
            return dict[new Vector2(x, y -1)];
        }

        return null;
    }
    
    public static bool isInBounds(int[,] array, int x, int y)
    {
        return x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(1);
    }
    
    private void highlightObj(float x, float y)
    {
        Vector2 spot = mapObjects[new Vector2(x, y)].transform.position;
        Instantiate(highlight,spot,Quaternion.identity);
    }

    private void highlightObj(Vector2 spot)
    {
        Instantiate(highlight,spot,Quaternion.identity);
    }

   
}
