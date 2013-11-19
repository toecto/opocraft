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
            foreach (KeyValuePair<int, Unit> item in this)
            {
                item.Value.Tick();
            }
        }

        public void Render(RenderSystem render)
        {
            foreach (KeyValuePair<int,Unit> item in this)
            {
                item.Value.Render(render);
            }
        }

        public override void Add(Unit u)
        {
            u.theGame = this.theGame;
            base.Add(u);
        }
    }
}
