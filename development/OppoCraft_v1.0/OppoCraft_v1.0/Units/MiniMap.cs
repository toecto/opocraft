using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    class MiniMap: MapEntity
    {
        public MiniMap(int uid)
        {
            this.uid = uid;
        }

        public override void onStart()
        {
            this.location.Y = this.theGame.worldMapSize.Y;
        }

        public override void Tick()
        {
        }

        public override void Render(RenderSystem render)
        {
            Vector2 position;    
            Rectangle dot = new Rectangle(0,0, 2, 2);
            int scale = 15;
            int sizeX = this.theGame.worldMapSize.X / scale;
            int sizeY = this.theGame.worldMapSize.Y / scale;
            int shiftToBottom= render.size.Y - sizeY;
            render.Draw(render.primRect, new Rectangle(0, render.size.Y-sizeY, sizeX, sizeY), new Rectangle(0, 0, 40, 24), Microsoft.Xna.Framework.Color.White);

            Vector2 frameStart = Vector2.Divide(render.getAbsWorldCoords(render.scroll.getVector2()).getVector2(),new Vector2(scale,scale));
            Vector2 frameSize = Vector2.Divide(render.getAbsWorldCoords(render.size.getVector2()).getVector2(), new Vector2(scale, scale));
            
            render.Draw(render.primRect, new Rectangle((int)frameStart.X, (int)frameStart.Y+shiftToBottom, (int)frameSize.X, (int)frameSize.Y), new Rectangle(0, 0, 40, 24), Microsoft.Xna.Framework.Color.BlueViolet);

            foreach (KeyValuePair<int, MapEntity> item in this.theGame.map)
            {
                if (item.Value.GetType() == typeof(Unit))
                {
                    Unit unit=(Unit)item.Value;
                    position = new Vector2(unit.location.X * sizeX / this.theGame.worldMapSize.X, unit.location.Y * sizeY / this.theGame.worldMapSize.Y + shiftToBottom);
                    if (unit.cid == this.theGame.cid)
                        render.Draw(render.primDot, position, dot, Microsoft.Xna.Framework.Color.Blue);
                    else
                    {
                        if(unit.type=="Tree")
                            render.Draw(render.primDot, position, dot, Microsoft.Xna.Framework.Color.GreenYellow);
                        else
                            render.Draw(render.primDot, position, dot, Microsoft.Xna.Framework.Color.Red);
                    }
                    
                }
            }
        }

    }
}
