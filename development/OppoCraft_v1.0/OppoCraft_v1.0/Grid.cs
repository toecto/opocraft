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
        public int[,] gridValues;

        public Grid(Game1 g)
        {
            this.theGame = g;

            this.gridSize = new Coordinates(this.theGame.worldMapSize.X / this.theGame.cellSize.X, this.theGame.worldMapSize.Y / this.theGame.cellSize.Y);
            this.gridValues = new int[this.gridSize.X, this.gridSize.X];
            this.ResetGridValues();
        }


        //returns new WorldCoords based on Grid coordinates
        public WorldCoords getWorldCoords(GridCoords gc)
        {
            return new WorldCoords(gc.X * this.theGame.cellSize.X + this.theGame.cellSize.X, gc.Y * this.theGame.cellSize.Y + this.theGame.cellSize.Y);
        }

        //returns new Grid with coordinates based on WorldCoords parameter
        public GridCoords getGridCoords(WorldCoords worldCoords)
        {
            return new GridCoords((int)(worldCoords.X / this.theGame.cellSize.X), (int)(worldCoords.Y / this.theGame.cellSize.Y));
        }

        //returns new Grid with coordinates based on WorldCoords parameter
        public void fillRectValues(GridCoords p, Coordinates s, int v)
        {
            for (int x = p.X; x < s.X + p.X; x++)
            {
                for (int y = p.Y; y < s.Y + p.Y; y++)
                {
                    this.gridValues[x, y] = v;
                }
            }
        }

        //sets the Grid cell values all to zero
        public void ResetGridValues()
        {
            this.fillRectValues(new GridCoords(0, 0), this.gridSize, -1);
            this.fillRectValues(new GridCoords(1, 1), new Coordinates(this.gridSize.X - 2, this.gridSize.Y - 2), 0);
        }
        

    }
}
