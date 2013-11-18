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
using testClient;
using System.Diagnostics;

namespace OppoCraft
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {        
        public RenderSystem render;

        //Cells, Map, and Coordinate Properties
        public Coordinates cellSize;
        public Coordinates worldMapSize;
        public Grid theGrid;

        //debug test
        public Debugger debugger;

        //Mouse Movement testing
        public MouseState mouseState;
        public MouseState prevMouseState;
        int scrollValue = 0;

   

        private NetworkModule network;
        public GameMap map;
        public MessageHandler messageHandler;
        public TaskGoTo goTo = new TaskGoTo(new WorldCoords(500,500));

        public int cid;
        public int enemyCid;
        int UIDCnt = 0;
        public bool running=false;

        //Testing properties
        public int myFirstUnit;
        

        public Game1(NetworkModule net, int cid, int enemyCid,string Map)
        {
            this.cid = cid;
            this.enemyCid = enemyCid;
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this.network = net;
            this.messageHandler = new MessageHandler(this,this.network);
            //this.OnExiting+=
            //Mouse Scrolling testing
            this.mouseState = Mouse.GetState();
            this.prevMouseState = mouseState;

            this.cellSize = new Coordinates(40, 40);
            this.worldMapSize = new Coordinates(800, 800); // set back to 10240/10240

            this.render = new RenderSystem(this);
            this.theGrid = new Grid(this);
            this.debugger = new Debugger(this);
            this.map = new GameMap(this);

            //Testing setting up obstacles
            this.theGrid.fillRectValues(new GridCoords(1, 3), new Coordinates(10, 1), -1);
            this.theGrid.fillRectValues(new GridCoords(10, 5), new Coordinates(10, 1), -1);
            this.theGrid.fillRectValues(new GridCoords(1, 7), new Coordinates(10, 1), -1);
            this.map.Add(new PathFinderTest(this.cid,this.CreateUID()));

            OppoMessage msg = new OppoMessage(OppoMessageType.CreateUnit);
            msg["uid"] = this.myFirstUnit = this.CreateUID();
            this.AddCommand(msg);
            
            //unit.task.Add(new _Movement(unit, new WorldCoords(500, 500)));

            //Testing setting up obstacles
            //this.theGrid.fillRectValues(new GridCoords(1, 3), new Coordinates(10, 1), -1);
            //this.theGrid.fillRectValues(new GridCoords(10, 5), new Coordinates(10, 1), -1);
            //this.theGrid.fillRectValues(new GridCoords(1, 7), new Coordinates(10, 1), -1);
            //Testing the Path Finder Algorithm     
            //Path finding test
 
		/*
            for (int x = 0; x < this.theGrid.gridValues.GetLength(0); x++)
            {
                for (int y = 0; y < this.theGrid.gridValues.GetLength(1); y++)
                {
                    this.debugger.AddMessage("(" + x + ", " + y + "): " + this.theGrid.gridValues[x, y].ToString());
                }
            }
            /**/
            if(Map!=null)
            this.AddCommand(new OppoMessage(OppoMessageType.StartGame));
        }

        public int CreateUID()
        {
            this.UIDCnt++;
            return int.Parse(this.cid + "" + this.UIDCnt);
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
            this.render.LoadContent();
            
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            this.network.Stop();
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

            messageHandler.Tick();
            if(this.running)
                this.map.Tick();

            if (!this.network.Flush())
                this.debugger.AddMessage("Lost connection to server");
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {         
            this.render.Render(gameTime);

            base.Draw(gameTime);
        }


        public void AddCommand(OppoMessage msg)
        {
            msg["cid"] = this.cid;
            this.network.Send(msg);
        }


    }
}
