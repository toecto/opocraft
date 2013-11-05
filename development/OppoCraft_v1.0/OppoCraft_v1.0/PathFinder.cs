using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    class PathFinder
    {
        Grid theGrid;

        //set of coordinates with the value to apply during path fining algorithm
        private int[,] pathValues = new int[,] 
        {
            {-1, -1, 3}, {1,- 1, 3}, {1, 1, 3}, {-1, 1, 3},
            {0, -1, 2}, {1, 0, 2}, {0, 1, 2}, {-1, 0, 2}
        };


        public PathFinder(Grid g)
        {
            this.theGrid = g;
        }

        public Path GetPath(GridCoords orig, GridCoords dest)
        {
            Path path = new Path();
            path.AddFirst(orig);

            this.theGrid.fillRectValues(orig, new Coordinates(1, 1), 1); //set orginal grid cell value to 1
            
            Path newPath = this.SetValues(path);

            while (newPath.Count > 0)
            {
                newPath = this.SetValues(newPath);
            }

            return this.SetPath(orig, dest);
        }


        //takes a Path (collection of Grid Coords) and applies the values from pathValues to the surrounding cells 
        //for each Grid Coord, using the pathValues array
        public Path SetValues(Path p)
        {
            Path newPath = new Path();

            foreach (GridCoords gridCoords in p)            
            {
                for (int i = 0; i < this.pathValues.Length; i++)
                {
                    int x = gridCoords.X + this.pathValues[i, 0];
                    int y = gridCoords.Y + this.pathValues[i, 1];

                    if (this.theGrid.gridValues[x, y] == 0 || this.theGrid.gridValues[x, y] > this.theGrid.gridValues[gridCoords.X, gridCoords.Y] + this.pathValues[i, 2])
                    {
                        this.theGrid.gridValues[x, y] = this.theGrid.gridValues[gridCoords.X, gridCoords.Y] + this.pathValues[i, 2];
                        newPath.AddFirst(new GridCoords(x, y));
                    }
                }
            }

            return newPath;
        }

        //stage 2 - under construction
        //Using the Grid values populated in SetValues method, determine the lowest value of each surrounding cell, and choose that
        //cell (add to Path collection), use that chosen cell to follow the same routine until getting back to the origin coord.
        public Path SetPath(GridCoords org, GridCoords dest)
        {
            Path path = new Path();


            return path;
        }


    }
}
