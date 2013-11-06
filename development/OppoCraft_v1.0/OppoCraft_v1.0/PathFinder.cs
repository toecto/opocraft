﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
   public class PathFinder
    {
        Grid theGrid;
        Game1 theGame;

        //set of coordinates with the value to apply during path finding algorithm
        public int[,] pathValues = new int[,] 
        {
            {-1, -1, 3}, {1,- 1, 3}, {1, 1, 3}, {-1, 1, 3},
            {0, -1, 2}, {1, 0, 2}, {0, 1, 2}, {-1, 0, 2}
        };


        public PathFinder(Grid g, Game1 game)
        {
            this.theGrid = g;
            this.theGame = game;
        }

        // orig == Origin coordiantes, dest == Destination coordinates
        public Path GetPath(GridCoords orig, GridCoords dest)
        {
            this.theGrid.fillRectValues(orig, new Coordinates(1, 1), 1); //set orginal grid cell value to 1
            
            if(this.SetValues(orig, dest))
                return this.SetPath(orig, dest);
            
            return null;
        }


        //takes a Path (collection of Grid Coords) and applies the values from pathValues to the surrounding cells 
        //for each Grid Coord, using the pathValues array
        public bool SetValues(GridCoords orig, GridCoords dest)
        {
            Path cellQue = new Path(),newCellQue;
            cellQue.AddFirst(orig);

            while (cellQue.Count > 0)
            {
                newCellQue=new Path();
                foreach (GridCoords gridCoords in cellQue)
                {
                    int currentValue = this.theGrid.gridValues[gridCoords.X, gridCoords.Y];

                    for (int i = 0; i < this.pathValues.GetLength(0); i++)
                    {
                        int x = gridCoords.X + this.pathValues[i, 0];
                        int y = gridCoords.Y + this.pathValues[i, 1];
                        int newValue = currentValue + this.pathValues[i, 2];

                        if (this.theGrid.gridValues[x, y] == 0 || this.theGrid.gridValues[x, y] > newValue)
                        {
                            this.theGrid.gridValues[x, y] = newValue;
                            newCellQue.AddFirst(new GridCoords(x, y));

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
        public Path SetPath(GridCoords orig, GridCoords dest)
        {
            Path path = new Path();
            path.AddFirst(dest);

            GridCoords baseCoord = dest;

            while (orig.X != baseCoord.X || orig.Y != baseCoord.Y)
            {
                GridCoords currCoord = new GridCoords(baseCoord.X + this.pathValues[0, 0], baseCoord.Y + this.pathValues[0, 1]);

                for (int i = 1; i < this.pathValues.GetLength(0); i++)
                {                    
                    GridCoords nextCoord = new GridCoords(baseCoord.X + this.pathValues[i, 0], baseCoord.Y + this.pathValues[i, 1]);
                    
                    int currVal = this.theGrid.getGridValue(currCoord);
                    int nextVal = this.theGrid.getGridValue(nextCoord);
                 
                    if (nextVal < currVal && nextVal > 0 || currVal <= 0)
                    {
                        currCoord = nextCoord;
                    }
                        
                    if (nextVal == currVal && nextCoord.Distance(orig) < currCoord.Distance(orig))
                    {                            
                        currCoord = nextCoord;
                    }
                }
             
                path.AddFirst(currCoord);
                baseCoord = currCoord;
            }
            
            return path;
        }
    }
}
