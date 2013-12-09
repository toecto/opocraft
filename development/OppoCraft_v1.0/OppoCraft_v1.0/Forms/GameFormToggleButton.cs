using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    class GameFormToggleButton : GameFormControl
    {
        public string textOn;
        public string textOff;
        
        public Color colorOn;
        public Color colorOff;

        Color color;
        string text;
        bool _isOn;

        public GameFormToggleButton(string textOn, string textOff)
        {
            this.colorOn = Color.White;
            this.colorOff = Color.Green;
            this.size.Y = 25;
            this.textOn = textOn;
            this.textOff = textOff;
            this.isOn = true;
        }

        public bool isOn {
            get { return this._isOn;}
            set { this._isOn=value;
                if (this._isOn)
                {
                    this.color = this.colorOn;
                    this.text = this.textOn;
                }
                else
                {
                    this.color = this.colorOff;
                    this.text = this.textOff;
                }
            }
        }



        public override void Tick()
        {
            
            if (disabled)
            {
                this.color = Color.Red;
                return;
            }

            base.Tick();
        }

        public override void Render(RenderSystem render)
        {
            Coordinates position = this.ScreenPosition();
            render.Draw(render.primRect70, position.getRectangle(this.size), render.primRect70.Bounds, color);
            render.DrawText(this.text, new Vector2(position.X + 5, position.Y + 3), Color.White);
            base.Render(render);
        }

        public override void onClickEvent(WorldCoords mouse)
        {
            this.isOn = !this.isOn;
            base.onClickEvent(mouse);

        }

    }
}
