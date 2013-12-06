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

        public GridCoords locationGrid { get { return this.theGame.theGrid.getGridCoords(this.location); } }

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
