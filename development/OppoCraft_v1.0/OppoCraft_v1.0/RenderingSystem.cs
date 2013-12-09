using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace OppoCraft
{
   public class RenderSystem
    {
        // Render squeezing Coeffecient: equal width, 60% height
       public float kY=0.6f; //kX=1 always

       public Game1 theGame;
       GraphicsDeviceManager graphics;
       private SpriteBatch spriteBatch;
       public SpriteFont font;
       public SpriteFont fontSmall;
       public Texture2D primRect50;
       public Texture2D primRect70;
       public Texture2D primCircle;
       public Texture2D primCircle100;
       public Texture2D primDot;
       
       public Coordinates scroll = new Coordinates(0, 0);
       public SmoothScroller scroller;
       public Coordinates size;

       SoundEffect soundEngine;
       SoundEffectInstance soundEngineInstance;

       public RenderSystem(Game1 g)
       {
           this.theGame = g;
           this.graphics = new GraphicsDeviceManager(this.theGame);

           this.theGame.Window.ClientSizeChanged += new EventHandler<EventArgs>(this.Window_ClientSizeChanged);

           this.size = new Coordinates(1000, 600);
           
           graphics.PreferredBackBufferWidth = this.size.X;
           graphics.PreferredBackBufferHeight = this.size.Y;
           
           this.Window_ClientSizeChanged();
           //this.theGame.Window.AllowUserResizing = true;
           //ToggleFullScreen();
       }

       void Window_ClientSizeChanged(object sender=null, EventArgs e=null)
       {
           if (sender!=null)
                this.size = new Coordinates(this.theGame.Window.ClientBounds.Width, this.theGame.Window.ClientBounds.Height);
           Coordinates saveScroll = this.scroll;
           Coordinates worldSizeOnScreen = new Coordinates(0, 0);
           worldSizeOnScreen.setVector2(this.getScreenCoordsAbs(this.theGame.worldMapSize));
           this.scroller = new SmoothScroller(this.theGame.userInput, this.scroll, worldSizeOnScreen, this.size);
       }

       public void ToggleFullScreen()
       {
           if (!graphics.IsFullScreen)
               this.size = new Coordinates(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
           else
               this.size = new Coordinates(1000, 600);
           
           graphics.PreferredBackBufferWidth = this.size.X;
           graphics.PreferredBackBufferHeight = this.size.Y;
           graphics.ToggleFullScreen();
       }


       public void LoadContent()
       {
           Debug.WriteLine("LoadContent start");
           // Create a new SpriteBatch, which can be used to draw textures.
           this.spriteBatch = new SpriteBatch(this.theGame.GraphicsDevice);

           this.primDot = this.theGame.Content.Load<Texture2D>("dot");
           this.primRect50 = this.theGame.Content.Load<Texture2D>("Prim_Rect50");
           this.primRect70 = this.theGame.Content.Load<Texture2D>("Prim_Rect70");
           this.primCircle = this.theGame.Content.Load<Texture2D>("Prim_Circle");
           this.primCircle100 = this.theGame.Content.Load<Texture2D>("Prim_Circle100");


           // Sound
           soundEngine = this.theGame.Content.Load<SoundEffect>("Sounds\\background");
           soundEngineInstance = soundEngine.CreateInstance();
           soundEngineInstance.Volume = 1f;
           soundEngineInstance.IsLooped = true;
           soundEngineInstance.Play();

           this.font = this.theGame.Content.Load<SpriteFont>("myFont");
           this.fontSmall = this.theGame.Content.Load<SpriteFont>("smallFont");

           this.theGame.graphContent.LoadContent();
       }

       public Texture2D LoadContent(string name)
       {
           return this.theGame.Content.Load<Texture2D>(name);
       }

       public void Render(GameTime gameTime)
       {
           
           this.scroller.Tick();
           this.theGame.GraphicsDevice.Clear(new Color(160, 160, 160));

           // TODO: Add your drawing code here
           this.spriteBatch.Begin();

           this.theGame.map.Render(this);
           this.theGame.forms.Render(this);
           this.theGame.debugger.RenderMessages();
           this.spriteBatch.End();
       }

       public void DrawText(string msg, Vector2 position, Color color=default(Color))
       {
           if (color == default(Color)) color = Color.White;
           if (position.X < this.size.X && position.Y < this.size.Y && position.X > -100 && position.Y > -100)
                this.spriteBatch.DrawString(font, msg, position, color);
       }

       public void DrawTextSmall(string msg, Vector2 position)
       {
           if (position.X < this.size.X && position.Y < this.size.Y && position.X > -100 && position.Y > -100)
                this.spriteBatch.DrawString(fontSmall, msg, position, new Color(225, 225, 225));
       }

       //paramater is for screen coordinates, used to shift the coordinates
       public Vector2 getScreenCoords(WorldCoords worldCoords)
       {
           return new Vector2(worldCoords.X - scroll.X, (int)(worldCoords.Y * this.kY - scroll.Y));
       }
       //paramater is for screen coordinates, used to shift the coordinates
       public Vector2 getScreenCoordsAbs(WorldCoords worldCoords)
       {
           return new Vector2(worldCoords.X, (int)(worldCoords.Y * this.kY));
       }

        //paramaters are for the screen, and scroll coords, to convert from screen to World
        public WorldCoords getWorldCoords(Vector2 screen)
        {
            return new WorldCoords((int)screen.X + scroll.X, (int)((screen.Y + scroll.Y) / this.kY));
        }
        //paramaters are for the screen, and scroll coords, to convert from screen to World
        public WorldCoords getWorldCoordsAbs(Vector2 screen)
        {
            return new WorldCoords((int)screen.X, (int)((screen.Y) / this.kY));
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle sourceRectangle, Color color)
        {
            if (position.X < this.size.X && position.Y < this.size.Y && position.X > -300 && position.Y > -300)
                this.spriteBatch.Draw(texture, position, sourceRectangle, color);
        }

        public void Draw(Texture2D texture, Rectangle DestinationRectangle, Rectangle sourceRectangle, Color color)
        {
            if (DestinationRectangle.X < this.size.X && DestinationRectangle.Y < this.size.Y && DestinationRectangle.X > -300 && DestinationRectangle.Y > -300)
                this.spriteBatch.Draw(texture, DestinationRectangle, sourceRectangle, color);
        }

    }
}
