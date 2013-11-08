﻿using System;
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
       GraphicsDeviceManager graphics;
       SpriteBatch spriteBatch;
       SpriteFont font;
       Texture2D primRect;

       public RenderSystem(Game1 g)
       {
           this.theGame = g;

           this.graphics = new GraphicsDeviceManager(this.theGame);
           this.graphics.PreferredBackBufferWidth = 1000;
           this.graphics.PreferredBackBufferHeight = 600;

       }

       public void LoadContent()
       {
           // Create a new SpriteBatch, which can be used to draw textures.
           this.spriteBatch = new SpriteBatch(this.theGame.GraphicsDevice);

           primRect = this.theGame.Content.Load<Texture2D>("Prim_Rect");

           // Load the font from xml file
           this.font = this.theGame.Content.Load<SpriteFont>("myFont");
       }


       public void Render(GameTime gameTime)
       {
           this.theGame.GraphicsDevice.Clear(new Color(160, 160, 160));

           // TODO: Add your drawing code here
           this.spriteBatch.Begin();
           
           int maxValue = 0;
           for (int x = 0; x < this.theGame.theGrid.gridValues.GetLength(0); x++)
           {
               for (int y = 0; y < this.theGame.theGrid.gridValues.GetLength(1); y++)
               {
                   if(maxValue < this.theGame.theGrid.getGridValue(new GridCoords(x,y)))
                   {
                       maxValue = this.theGame.theGrid.getGridValue(new GridCoords(x, y));
                   }
               }
           }

           int color;

           for (int x = 0; x < this.theGame.theGrid.gridValues.GetLength(0); x++)
           {
               for (int y = 0; y < this.theGame.theGrid.gridValues.GetLength(1); y++)
               {
                   Vector2 position = this.getScreenCoords(this.theGame.theGrid.getWorldCoords(new GridCoords(x, y)), new Coordinates(0, 0));
                   color = this.theGame.theGrid.getGridValue(new GridCoords(x, y)) * 255 / maxValue;
                   if(color<0)
                       this.spriteBatch.Draw(primRect, position, new Rectangle(0, 0, 40, 24), new Color(255, 0, 0));
                   else
                   this.spriteBatch.Draw(primRect, position, new Rectangle(0, 0, 40, 24),new Color(0, 0, color));
               }
           }
           foreach(WorldCoords coords in this.theGame.aPath)
           {
                Vector2 position = this.getScreenCoords(coords, new Coordinates(0, 0));
                this.spriteBatch.Draw(primRect, position, new Rectangle(0, 0, 40, 24),new Color(0, 255, 0));
           }

           //this.theGame.debugger.RenderMessages();
           this.spriteBatch.End();
       }

       public void DrawText()
       {
           this.spriteBatch.DrawString(font, "Some Stats", new Vector2(20, 45), new Color(225, 225, 225));
       }

       public void DrawText(string msg, Vector2 position)
       {
           this.spriteBatch.DrawString(font, msg, position, new Color(225, 225, 225));
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
