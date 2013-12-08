using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;

namespace OppoCraft
{
    public abstract class MapEntity
    {
        public Game1 theGame;
        public WorldCoords size = new WorldCoords(40, 40);
        public WorldCoords location = new WorldCoords(1, 1);
        public OppoMessage settings;

        public int uid = 0;
        public int cid = 0;
        public bool isMy = true;
        public bool isServed = true;

        public GridCoords locationGrid
        {
            get { return this.theGame.theGrid.getGridCoords(this.location); }
            set { this.location = this.theGame.theGrid.getWorldCoordsCenter(value); }
        }
        public GridCoords sizeGrid
        {
            get { return this.theGame.theGrid.getGridCoords(this.size); }
            set { this.size = this.theGame.theGrid.getWorldCoords(value); }
        }

        public MapEntity()
        {
        }

        public MapEntity(Game1 theGame, OppoMessage settings)
        {
            this.theGame = theGame;
            this.settings = settings;
            this.uid = settings["uid"];
            this.cid = settings["ownercid"];
            this.location = new WorldCoords(settings["x"], settings["y"]);
            this.isMy = (this.cid == this.theGame.cid);
            this.isServed = (this.cid == 0 && this.theGame.loadMap != null);
        }

        public virtual void onStart()
        {
        }


        public virtual void onFinish()
        {
        }


        public virtual void Tick()
        {
        }

        public virtual void Render(RenderSystem render)
        {
        }
    }
}
