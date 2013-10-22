using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OppoCraft
{
   public class RenderSystem
    {
        // Render squeezing Coeffecient: equal width, 60% height
       public float[] renderCoEff = new float[] { 1.0f, 0.6f };

       public Game1 theGame;

       public RenderSystem(Game1 g)
       {
           this.theGame = g;
       }

        //paramater is for screen coordinates, used to shift the coordinates
        public Vector2 getScreenCoords(WorldCoords worldCoords, Coordinates scroll)
        {
            return new Vector2((int)(worldCoords.X * renderCoEff[0] - scroll.X), (int)(worldCoords.Y * renderCoEff[1] - scroll.Y));
        }

        //paramaters are for the screen, and scroll coords, to convert from screen to World
        public WorldCoords getWorldCoords(Vector2 screen, Coordinates scroll)
        {
            return new WorldCoords((int)((screen.X + scroll.X) / renderCoEff[0]), (int)((screen.Y + scroll.Y) / renderCoEff[1]));
        }             

       

    }
}
