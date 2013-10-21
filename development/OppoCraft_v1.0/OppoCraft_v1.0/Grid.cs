using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    public class Grid
    {
        Game1 theGame;
        
        public Coordinates gridSize;
        public Coordinates cellSize;
        public int[,] gridValues;

        public Grid(Game1 g, int c, int r)
        {
            this.theGame = g;
            
            this.cellSize = new Coordinates(40, 40); //move to game
            this.gridSize = new Coordinates(c, r); //keep values in game object (create properties in Game1)
            this.gridValues = new int[c, r]; // keep values in game object (create properties in Game1)
            this.ResetGridValues();
        }


        //returns new WorldCoords based on Grid coordinates
        public WorldCoords getWorldCoords(GridCoords gc)
        {
            return new WorldCoords(gc.X * this.cellSize.X, gc.Y * this.cellSize.Y);
        }

        //returns new Grid with coordinates based on WorldCoords parameter
        public GridCoords getGridCoords(WorldCoords worldCoords)
        {
            return new GridCoords((int)(worldCoords.X / this.cellSize.X), (int)(worldCoords.Y / this.cellSize.Y));
        }

        /*
         * To Do: Method to "draw" rectangle onto the Grid
         * three params: position, size, drawValue
         */
        

        //sets the Grid cell values all to zero
        public void ResetGridValues()
        {
            for (int c = 0; c < this.gridSize.X; c++)
            {
                for (int r = 0; r < this.gridSize.Y; r++)
                {
                    this.gridValues[c, r] = 0;
                }
            }
        }

    }
}
