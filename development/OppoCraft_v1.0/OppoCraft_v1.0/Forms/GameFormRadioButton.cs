using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    class GameFormRadioButton : GameFormControl
    {
        public string text;
        public Object value;
        Color color;
        public bool selected;

        public GameFormRadioButton(string text, Object value)
        {
            this.text = text;
            this.value = value;
            this.color = Color.White;
            this.size.X = 100;
        }

        public override void Render(RenderSystem render)
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
            base.Render(render);
        }
        
    }
}
