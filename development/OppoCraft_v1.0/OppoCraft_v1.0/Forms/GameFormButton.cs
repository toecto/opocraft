using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    class GameFormButton: GameFormControl
    {
        string _text;
        public string Text { get { return _text; } set { this._text = value; this.size.X = this._text.Length * 10 + 10; } }
        
        
        Color color;
        public GameFormButton(string text)
        {
            this.color = Color.White;
            this.size.Y = 25;
            this.Text = text;
        }


        public override void Tick()
        {
            base.Tick();
            if (disabled)
            {
                this.color = Color.Red;
                return;
            }

            if (this.parentForm.theGame.userInput.mouseCoordinates.isIn(this.ScreenPosition(), this.size))
                this.color = Color.LightGreen;
            else
                this.color = Color.White;
        }

        public override void Render(RenderSystem render)
        {
            Coordinates position = this.ScreenPosition();
            render.Draw(render.primRect70, position.getRectangle(this.size), render.primRect70.Bounds, color);
            render.DrawText(this._text, new Vector2(position.X + 5, position.Y + 3), Color.White);
            base.Render(render);
        }
        

    }
}
