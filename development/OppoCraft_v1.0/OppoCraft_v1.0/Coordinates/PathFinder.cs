using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OppoCraft
{
   public class PathFinder
    {
        Grid theGrid;
        Game1 theGame;

        //set of coordinates with the value to apply during path finding algorithm
        public int[,] pathValues = new int[,] 
        {
            {0, -1, 2}, {1, 0, 2}, {0, 1, 2}, {-1, 0, 2},
            {-1, -1, 3}, {1,- 1, 3}, {1, 1, 3}, {-1, 1, 3},

            
        };


        public PathFinder(Grid g, Game1 game)
        {
            this.theGrid = g;
            this.theGame = game;
        }

        // orig == Origin coordiantes, dest == Destination coordinates
        public WorldPath GetPath(WorldCoords orig, WorldCoords dest)
        {
            this.theGrid.resetGridValues();
            GridCoords origGrid = this.theGrid.getGridCoords(orig);
            GridCoords destGrid = this.theGrid.getGridCoords(dest);

            //if (this.theGrid.gridValues[origGrid.X, origGrid.Y]>=0)
            this.theGrid.gridValues[origGrid.X, origGrid.Y ]=1; //set orginal grid cell value to 1
               // return null;

            if (this.SetValues(origGrid, destGrid))
                return this.SetPath(origGrid, destGrid);
            
            return null;
        }


        //takes a Path (collection of Grid Coords) and applies the values from pathValues to the surrounding cells 
        //for each Grid Coord, using the pathValues array
        public bool SetValues(GridCoords orig, GridCoords dest)
        {
            GridPath cellQue = new GridPath(), newCellQue;
            cellQue.AddFirst(orig);
            int pathValLength = this.pathValues.GetLength(0);
            while (cellQue.Count > 0)
            {
                newCellQue=new GridPath();
                foreach (GridCoords gridCoords in cellQue)
                {
                    int currentValue = this.theGrid.gridValues[gridCoords.X, gridCoords.Y];
                    for (int i = 0; i < pathValLength; i++)
                    {
                        int x = gridCoords.X + this.pathValues[i, 0];
                        int y = gridCoords.Y + this.pathValues[i, 1];
                        int newValue = currentValue + this.pathValues[i, 2];

                        if (this.theGrid.gridValues[x, y] == 0)
                        {
                            this.theGrid.gridValues[x, y] = newValue;
                            newCellQue.AddLast(new GridCoords(x, y));

                            if (x == dest.X && y == dest.Y)
                            {
                                return true;
                            }
                        }
                    }
                }
                cellQue = newCellQue;
            }
            return false;
        }

        // orig == Origin coordiantes, dest == Destination coordinates
        //Using the Grid values populated in SetValues method, determine the lowest value of each surrounding cell, and choose that
        //cell (add to Path collection), use that chosen cell to follow the same routine until getting back to the origin coord.
        public WorldPath SetPath(GridCoords orig, GridCoords dest)
        {
            WorldPath path = new WorldPath();
            path.AddFirst(this.theGrid.getWorldCoords(dest));
            int pathValLength = this.pathValues.GetLength(0);

            GridCoords baseCoord = dest;
            bool isCornerAround;
             
            while (orig.X != baseCoord.X || orig.Y != baseCoord.Y)
            {
                GridCoords minCoord=null;
                int minValue = 0;
                isCornerAround = false;
                for (int i = 0; i < pathValLength; i++)
                {
                    if (isCornerAround && this.pathValues[i, 2] == 3)
                        break;
                    GridCoords nextCoord = new GridCoords(baseCoord.X + this.pathValues[i, 0], baseCoord.Y + this.pathValues[i, 1]);
                    
                    int nextVal = this.theGrid.getGridValue(nextCoord);
                    if (nextVal == -1 && this.pathValues[i, 2] == 2)
                    {
                        isCornerAround = true;
                    }
                    else
                    {
                        if (nextVal < minValue && nextVal > 0 || minValue ==0)
                        {
                            minCoord = nextCoord;
                            minValue = nextVal;
                        }
                        else
                        {
                            if (nextVal == minValue && nextCoord.Distance(orig) < minCoord.Distance(orig))
                            {
                                minCoord = nextCoord;
                                minValue = nextVal;
                            }
                        }
                    }
                }

                path.AddFirst(this.theGrid.getWorldCoords(minCoord));
                baseCoord = minCoord;
            }
            
            return path;
        }
    }
}
