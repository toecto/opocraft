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



            return path;
        }


        //takes a Path (collection of Grid Coords) and applies the values from pathValues to the surrounding cells
        public void setValues(Path p)
        {

            foreach (GridCoords gridCoords in p)            
            {
                for (int i = 0; i < this.pathValues.Length; i++)
                {
                    this.theGrid.gridValues[gridCoords.X + this.pathValues[i, 0], gridCoords.Y + this.pathValues[i, 1]] = this.theGrid.gridValues[gridCoords.X, gridCoords.Y] + this.pathValues[i, 2];
                }
            }

        }



    }
}
