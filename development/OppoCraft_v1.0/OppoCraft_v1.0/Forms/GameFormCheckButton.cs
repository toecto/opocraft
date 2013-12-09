using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class GameFormCheckButton : GameFormControl
    {
        public string text;
        public Object value;
        public Color color;
        bool selectedValue;

        public bool selected { 
            get { return this.selectedValue; }
            set { this.selectedValue = value; this.onChangeEvent(); }
        }

        public GameFormCheckButton(string text, Object value)
        {
            this.text = text;
            this.value = value;
            this.color = Color.White;
            this.size.X = 100;
        }

        public override void Render(RenderSystem render)
        {
            this.Draw(render);
            base.Render(render);
        }

        public virtual void Draw(RenderSystem render)
        {
            Coordinates position = this.ScreenPosition();
            position.Y += 3;
            render.Draw(render.primRect70, position.getRectangle(new WorldCoords(12, 12)), render.primRect70.Bounds, color);
            render.DrawText(this.text, new Vector2(position.X + 20, position.Y - 3), this.color);
            if (this.selected)
            {
                position.X += 3;
                position.Y += 3;
                render.Draw(render.primRect70, position.getRectangle(new WorldCoords(6, 6)), render.primRect70.Bounds, Color.Black);
            }
        }

        public override void onClickEvent(WorldCoords mouse)
        {
            this.selected = !this.selected;
        }

    }
}