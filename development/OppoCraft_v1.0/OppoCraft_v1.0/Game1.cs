using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace OppoCraft
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {        
        public RenderSystem renderSystem;

        //Cells, Map, and Coordinate Properties
        public Coordinates cellSize;
        public Coordinates worldMapSize;
        public Grid theGrid;

        //debug test
        public Debugger debugger;
        double delayTime = 0;
        int counter = 0;

        //Mouse Movement testing
        public MouseState mouseState;
        public MouseState prevMouseState;
        int scrollValue = 0;

        public Game1()
        {            
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;

            //Mouse Scrolling testing
            this.mouseState = Mouse.GetState();
            this.prevMouseState = mouseState;

            this.cellSize = new Coordinates(40, 40);
            this.worldMapSize = new Coordinates(10240, 10240);

            this.renderSystem = new RenderSystem(this);

            this.theGrid = new Grid(this);

            this.debugger = new Debugger(this);
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {                       
            this.renderSystem.LoadContent();
            
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            this.mouseState = Mouse.GetState();
            this.scrollValue += (this.prevMouseState.ScrollWheelValue - this.mouseState.ScrollWheelValue) / 12;
            this.prevMouseState = this.mouseState;

            this.debugger.scrollRow = scrollValue;


            if (this.delayTime >= 1000)
            {
                this.debugger.AddMessage("game time = " + gameTime.TotalGameTime.ToString());
                this.delayTime = 0;
                counter++;
            }
            else
            {
                this.delayTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {         
            this.renderSystem.Render(gameTime);

            base.Draw(gameTime);
        }       

    }
}
