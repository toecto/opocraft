using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    public class GameMap: UnitCollection
    {
        Game1 theGame;
        public GameMap(Game1 g)
        {
            this.theGame = g;
        }

        public void Tick()
        {

        }

        public void Render(RenderSystem render)
        {
            
        }
        // add unitcollection to the map
        public override void Add(Unit u)
        {
           
        }
    }
}
