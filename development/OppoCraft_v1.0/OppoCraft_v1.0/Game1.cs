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
using System.Threading;

namespace OppoCraft
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {        
        public RenderSystem render;
        public GraphContentManager graphContent;

        //Cells, Map, and Coordinate Properties
        public WorldCoords cellSize;
        public WorldCoords worldMapSize;
        public Grid theGrid;
        public PathFinder pathFinder;

        //debug test
        public Debugger debugger;

        //Mouse Movement testing
        public MouseState mouseState;
        public MouseState prevMouseState;
        int scrollValue = 0;

   

        private NetworkModule network;
        public GameMap map;
        public GameMap forms;
        public MessageHandler messageHandler;

        public int cid;
        public int enemyCid;
        int UIDCnt = 0;
        public bool running=false;

        public string loadMap;


        //Testing properties
        public int myFirstUnit;
        
        public Database db;
        public UserInputSystem userInput;
        public UnitSelector unitSelector;

        public UnitDataLoader unitDataLoader;

        public static Random rnd = new Random();
        

        public Game1(NetworkModule net, int cid, int enemyCid,string map)
        {
            this.debugger = new Debugger(this);
            this.loadMap = map;
            this.cid = cid;
            this.enemyCid = enemyCid;
            if (enemyCid == 0) this.enemyCid = this.cid + 1;
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this.network = net;
            this.messageHandler = new MessageHandler(this,this.network);
            this.db = new Database("Data Source=OppoClient.sdf");
            //this.OnExiting+=
            //Mouse Scrolling testing
            this.mouseState = Mouse.GetState();
            this.prevMouseState = mouseState;

            this.cellSize = new WorldCoords(40, 40);
            this.worldMapSize = new WorldCoords(5000, 5000); // set back to 10240/10240

            //this.worldMapSize = new WorldCoords(40*20, 40*20); // set back to 10240/10240


            this.userInput = new UserInputSystem(this);
            this.render = new RenderSystem(this);
            this.graphContent = new GraphContentManager(this);
            this.theGrid = new Grid(this);
            this.pathFinder = new PathFinder(this.theGrid);
            this.map = new GameMap(this);
            this.forms = new GameMap(this);
            this.unitDataLoader = new UnitDataLoader(this);
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
            this.forms.Add(new PathFinderTest());
            this.map.Add(this.unitSelector=new UnitSelector());
            this.forms.Add(new UnitDescription());
            this.map.Add(new Background());
            this.forms.Add(new MiniMap());


            if(this.loadMap!=null)
            {
                this.LoadMap();
                this.network.Flush();
                Thread.Sleep(1000);
                this.AddCommand(new OppoMessage(OppoMessageType.StartGame));
            }
            
        }

        


        public void LoadMap()
        {
            //Testing setting up obstacles
            //this.theGrid.fillRectValues(new GridCoords(1, 3), new GridCoords(10, 1), -1);
            //this.theGrid.fillRectValues(new GridCoords(10, 5), new GridCoords(10, 1), -1);
            //this.theGrid.fillRectValues(new GridCoords(1, 7), new GridCoords(10, 1), -1);


            Random rnd = new Random();
            OppoMessage msg;
            int tmp;


            msg = new OppoMessage(OppoMessageType.CreateEntity);
            msg["uid"] = this.CreateUID();
            msg["ownercid"] = this.cid;
            msg["x"] = 40*30+20;
            msg["y"] = 40 * 30 + 20;
            msg.Text["type"] = "Castle";
            msg.Text["class"] = "UnitCastle";
            this.AddCommand(msg);


            
            msg = new OppoMessage(OppoMessageType.CreateEntity);
            msg["uid"] = this.CreateUID();
            msg["x"] = 100;
            msg["y"] = 100; 
            msg.Text["class"] = "EntityEnvironment";
            this.AddCommand(msg);

            
            msg = new OppoMessage(OppoMessageType.CreateEntity);
            msg["uid"]= this.CreateUID();
            msg["x"] = 200;
            msg["y"] = 3400;
            msg.Text["class"] = "EntityForest";
            this.AddCommand(msg);
            
            msg = new OppoMessage(OppoMessageType.CreateEntity);
            msg["uid"] = this.CreateUID();
            msg["x"] = 1900;
            msg["y"] = 1900;
            msg.Text["class"] = "EntityForest";
            this.AddCommand(msg);

            msg = new OppoMessage(OppoMessageType.CreateEntity);
            msg["uid"] = this.CreateUID();
            msg["x"] = 3400;
            msg["y"] = 200;
            msg.Text["class"] = "EntityForest";
            this.AddCommand(msg);

            
            for (int i = 0; i < 50; i++)
            {
                msg = new OppoMessage(OppoMessageType.CreateEntity);
                msg["uid"] = this.CreateUID();
                msg["ownercid"] = this.enemyCid;
                msg["x"] = 40 * rnd.Next(2, this.theGrid.gridSize.X - 2) + 20;
                msg["y"] = 40 * rnd.Next(2, this.theGrid.gridSize.Y - 2) + 20;
                msg.Text["type"] = "Knight";
                this.AddCommand(msg);
            }
            for (int i = 0; i < 50; i++)
            {
                msg = new OppoMessage(OppoMessageType.CreateEntity);
                msg["uid"] = this.CreateUID();
                msg["ownercid"] = this.cid;
                msg["x"] = 40 * rnd.Next(2, this.theGrid.gridSize.X - 2) + 20;
                msg["y"] = 40 * rnd.Next(2, this.theGrid.gridSize.Y - 2) + 20;
                msg.Text["type"] = "Knight";
                this.AddCommand(msg);
            }
            
            for (int i = 0; i < 50; i++)
            {
                msg = new OppoMessage(OppoMessageType.CreateEntity);
                msg["uid"] = this.CreateUID();
                msg["ownercid"] = this.enemyCid;
                msg["x"] = 40 * rnd.Next(2, this.theGrid.gridSize.X - 2) + 20;
                msg["y"] = 40 * rnd.Next(2, this.theGrid.gridSize.Y - 2) + 20;
                msg.Text["type"] = "Lumberjack";
                this.AddCommand(msg);
            }
            for (int i = 0; i < 50; i++)
            {
                msg = new OppoMessage(OppoMessageType.CreateEntity);
                msg["uid"] = this.CreateUID();
                msg["ownercid"] = this.cid;
                msg["x"] = 40 * rnd.Next(2, this.theGrid.gridSize.X - 2) + 20;
                msg["y"] = 40 * rnd.Next(2, this.theGrid.gridSize.Y - 2) + 20;
                msg.Text["type"] = "Lumberjack";
                this.AddCommand(msg);
            }

            for (int i = 0; i < 50; i++)
            {
                msg = new OppoMessage(OppoMessageType.CreateEntity);
                msg["uid"] = this.CreateUID();
                msg["ownercid"] = this.enemyCid;
                msg["x"] = 40 * rnd.Next(2, this.theGrid.gridSize.X - 2) + 20;
                msg["y"] = 40 * rnd.Next(2, this.theGrid.gridSize.Y - 2) + 20;
                msg.Text["type"] = "Archer";
                this.AddCommand(msg);
            }
            for (int i = 0; i < 50; i++)
            {
                msg = new OppoMessage(OppoMessageType.CreateEntity);
                msg["uid"] = this.CreateUID();
                msg["ownercid"] = this.cid;
                msg["x"] = 40 * rnd.Next(2, this.theGrid.gridSize.X - 2) + 20;
                msg["y"] = 40 * rnd.Next(2, this.theGrid.gridSize.Y - 2) + 20;
                msg.Text["type"] = "Archer";
                this.AddCommand(msg);
            }
            /**/
            for (int i = 0; i < 10; i++)
            {
                msg = new OppoMessage(OppoMessageType.CreateEntity);
                msg["uid"] = this.CreateUID();
                msg["ownercid"] = this.enemyCid;
                msg["x"] = 40 * rnd.Next(5, this.theGrid.gridSize.X - 5) + 20;
                msg["y"] = 40 * rnd.Next(5, this.theGrid.gridSize.Y - 5) + 20;
                msg.Text["type"] = "Tower";
                this.AddCommand(msg);
            }

            for (int i = 0; i < 10; i++)
            {
                msg = new OppoMessage(OppoMessageType.CreateEntity);
                msg["uid"] = this.CreateUID();
                msg["ownercid"] = this.cid;
                msg["x"] = 40 * rnd.Next(5, this.theGrid.gridSize.X - 5) + 20;
                msg["y"] = 40 * rnd.Next(5, this.theGrid.gridSize.Y - 5) + 20;
                msg.Text["type"] = "Tower";
                this.AddCommand(msg);
            }
            /**/
            
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

            //Keys[] keys =this.userInput.keyboard.GetPressedKeys();
            //debugger.AddMessage(String.Join("",keys));

            if (this.userInput.keyboard.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }


            if (this.userInput.isKeyPressed(Keys.F10))
            {
                this.render.ToggleFullScreen();
            }

            // TODO: Add your update logic here

            this.mouseState = Mouse.GetState();
            this.scrollValue += (this.prevMouseState.ScrollWheelValue - this.mouseState.ScrollWheelValue) / 12;
            this.prevMouseState = this.mouseState;
            this.debugger.scrollRow = scrollValue;


            this.userInput.Tick();
            messageHandler.Tick();
            if (this.running)
            {
                this.map.Tick();
                this.map.applyChanges();
                this.forms.Tick();
                this.forms.applyChanges();

            
            }

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
            //msg["cid"] = this.cid;
            this.network.Send(msg);
        }


    }
}
