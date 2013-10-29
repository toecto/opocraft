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
       GraphicsDeviceManager graphics;
       SpriteBatch spriteBatch;
       SpriteFont font;

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

           // Load the font from xml file
           this.font = this.theGame.Content.Load<SpriteFont>("myFont");
       }


       public void Render(GameTime gameTime)
       {
           this.theGame.GraphicsDevice.Clear(new Color(16, 16, 16));

           // TODO: Add your drawing code here
           this.spriteBatch.Begin();
           this.theGame.debugger.RenderMessages();

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
