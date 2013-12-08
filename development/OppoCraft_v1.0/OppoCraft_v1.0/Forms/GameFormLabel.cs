using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    class GameFormLabel : GameFormControl
    {
        public string Text="Label";
        public Color color;

        public GameFormLabel(string text)
        {
            this.Text = text;
            this.color = Color.White;
        }

  
        public override void Render(RenderSystem render)
        {
            Coordinates position = this.ScreenPosition();
            render.DrawText(this.Text, new Vector2(position.X + 5, position.Y + 3), color);
            base.Render(render);
        }
        
    }
}
