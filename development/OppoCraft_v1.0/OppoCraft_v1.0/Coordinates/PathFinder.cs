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

        //set of coordinates with the value to apply during path finding algorithm
        public int[,] pathValues = new int[,] 
        {
            {0, -1, 2}, {1, 0, 2}, {0, 1, 2}, {-1, 0, 2},
            {-1, -1, 3}, {1,- 1, 3}, {1, 1, 3}, {-1, 1, 3},
        };


        public PathFinder(Grid g)
        {
            this.theGrid = g;
        }

        // orig == Origin coordiantes, dest == Destination coordinates
        public WorldPath GetPath(WorldCoords orig, WorldCoords dest, int range=0)
        {
            
            this.theGrid.resetGridValues();
            GridCoords origGrid = this.theGrid.getGridCoords(orig);
            GridCoords destGrid = this.theGrid.getGridCoords(dest);

            WorldPath result = null;

            if (this.SetValues(origGrid, destGrid, range))
                result = this.SetPath(origGrid, destGrid);

            return result;
        }


        //takes a Path (collection of Grid Coords) and applies the values from pathValues to the surrounding cells 
        //for each Grid Coord, using the pathValues array
        public bool SetValues(GridCoords orig, GridCoords dest, int range)
        {
            range *= range;
            GridPath cellQue = new GridPath(), newCellQue;
            this.theGrid.gridValues[orig.X, orig.Y] = 1; //set orginal grid cell value to 1
            cellQue.AddFirst(orig);
            int pathValLength = this.pathValues.GetLength(0);
            int x, y, i, newValue, currentValue;
            bool isCornerAround = false;
            while (cellQue.Count > 0)
            {
                newCellQue=new GridPath();
                foreach (GridCoords gridCoords in cellQue)
                {
                    currentValue = this.theGrid.gridValues[gridCoords.X, gridCoords.Y];
                    for (i = 0; i < pathValLength; i++)
                    {
                        if (isCornerAround && this.pathValues[i, 2] == 3)
                            break;
                        x = gridCoords.X + this.pathValues[i, 0];
                        y = gridCoords.Y + this.pathValues[i, 1];
                        newValue = currentValue + this.pathValues[i, 2];

                        if (this.theGrid.gridValues[x, y] == 0)
                        {
                            this.theGrid.gridValues[x, y] = newValue;
                            newCellQue.AddLast(new GridCoords(x, y));

                            if ((int)dest.DistanceSqr(x, y) <= range)
                            {
                                dest.X = x;
                                dest.Y = y;
                                return true;
                            }
                        }
                        else
                        {
                            if (this.pathValues[i, 2] == 2)
                            {
                                isCornerAround = true;
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
            GridCoords current = dest;
            int cnt=0;
            //Debug.WriteLine("Start!");
            while (current != null)
            {
                cnt++;
                if (cnt > 1000)
                {
                    Debug.WriteLine("Achtung!");
                }
                path.AddFirst(this.theGrid.getWorldCoordsCenter(current));
                current = this.makeNextStep(current, orig);
                
            }
            if (!this.theGrid.getGridCoords(path.First.Value).Equals(orig)) 
                return null;//did not get to the end

            path.RemoveFirst();
            return path;
        }

        private GridCoords makeNextStep(GridCoords current, GridCoords orig)
        {

            GridCoords minCoord = null;
            int minValue = this.theGrid.getGridValue(current);
            bool isCornerAround = false;
            int pathValLength = this.pathValues.GetLength(0);

            for (int i = 0; i < pathValLength; i++)
            {
                if (isCornerAround && this.pathValues[i, 2] == 3)
                    break;
                GridCoords nextCoord = new GridCoords(current.X + this.pathValues[i, 0], current.Y + this.pathValues[i, 1]);

                int nextVal = this.theGrid.getGridValue(nextCoord);
                if (nextVal < 0 && this.pathValues[i, 2] == 2)
                {
                    isCornerAround = true;
                }
                else
                {
                    if (nextVal < minValue && nextVal > 0)
                    {
                        minCoord = nextCoord;
                        minValue = nextVal;
                    }
                }
            }
            //if (minCoord!=null)
            //Debug.WriteLine("minCoord! " + minCoord.X + " " + minCoord.Y + " " + this.theGrid.getGridValue(minCoord));

            return minCoord;
        }

    }
}
