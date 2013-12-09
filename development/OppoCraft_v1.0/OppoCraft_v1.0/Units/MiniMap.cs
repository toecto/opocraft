using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    class MiniMap: MapEntity
    {
        public override void onStart()
        {
        }

        public override void Tick()
        {
        }

        public override void Render(RenderSystem render)
        {
            Vector2 position;
            Rectangle dot = new Rectangle(0,0, 3, 3);
            int scale = 20;
            int sizeX = this.theGame.worldMapSize.X / scale;
            int sizeY = this.theGame.worldMapSize.Y / scale;
            int shiftToBottom= render.size.Y - sizeY;
            render.Draw(render.primRect50, new Rectangle(0, shiftToBottom, sizeX, sizeY), new Rectangle(0, 0, 40, 24), Color.Gray);

            Vector2 frameStart = Vector2.Divide(render.getWorldCoordsAbs(render.scroll.getVector2()).getVector2(),new Vector2(scale,scale));
            Vector2 frameSize = Vector2.Divide(render.getWorldCoordsAbs(render.size.getVector2()).getVector2(), new Vector2(scale, scale));
            
            render.Draw(render.primRect50, new Rectangle((int)frameStart.X, (int)frameStart.Y+shiftToBottom, (int)frameSize.X, (int)frameSize.Y), new Rectangle(0, 0, 40, 24), Microsoft.Xna.Framework.Color.BlueViolet);

            foreach (Unit unit in this.theGame.map.units)
            {
                if (unit.animation == null) continue;
                position = new Vector2(unit.location.X * sizeX / this.theGame.worldMapSize.X-1, unit.location.Y * sizeY / this.theGame.worldMapSize.Y + shiftToBottom-1);
                if (unit.isMy)
                    render.Draw(render.primDot, position, dot, Microsoft.Xna.Framework.Color.Blue);
                else
                {
                    if (unit.cid == 0)
                    {
                        if (unit.type == "Tree")
                            render.Draw(render.primDot, position, dot, Microsoft.Xna.Framework.Color.GreenYellow);
                        else
                            render.Draw(render.primDot, position, dot, Microsoft.Xna.Framework.Color.Gray);
                    }
                    else
                        render.Draw(render.primDot, position, dot, Microsoft.Xna.Framework.Color.Red);
                }
            }
        }

    }
}
