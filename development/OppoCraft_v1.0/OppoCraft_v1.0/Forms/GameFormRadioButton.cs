using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class GameFormRadioButton : GameFormCheckButton
    {
        public GameFormRadioButton(string text, Object value): base(text,value)
        { 
        
        }

        public override void Draw(RenderSystem render)
        {
            Coordinates position = this.ScreenPosition();
            position.Y += 3;
            render.Draw(render.primCircle100, position.getRectangle(new WorldCoords(12, 12)), render.primCircle100.Bounds, color);
            render.DrawText(this.text, new Vector2(position.X + 20, position.Y-3), this.color);
            if (this.selected)
            {
                position.X += 3;
                position.Y += 3;
                render.Draw(render.primCircle100, position.getRectangle(new WorldCoords(6, 6)), render.primCircle100.Bounds, Color.Black);
            }
        }

        public override void onClickEvent(WorldCoords mouse)
        {
            this.selected = true;
        }


    }
}
