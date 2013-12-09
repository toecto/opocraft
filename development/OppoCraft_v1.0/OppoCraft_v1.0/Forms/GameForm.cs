using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class GameForm : GameFormControl
    {
        GameFormButton close;
        public bool onScreen=false;
        public GameForm()
        {
            this.parentForm = this;
            this.location = new WorldCoords(300, 50);
            this.size = new WorldCoords(400, 400);
            this.controls.Add(this.close = new GameFormButton("X"));
        }

        public override void onStart()
        {
            this.onScreen = true;

            
            
            this.close.location.X = this.size.X - this.close.size.X;
            this.close.location.Y = 0;
            this.close.onClick += closeForm;

            foreach (MapEntity item in this.theGame.forms.entities.Values)
            {
                if (item is GameForm && item!=this)
                {
                    this.theGame.forms.Remove(item);
                }
            }
        }

        public void closeForm(GameFormControl obj, Coordinates mouse)
        {
            this.theGame.forms.Remove(this.uid);
        }


        public override void Tick()
        {
            if (this.theGame.userInput.mouseClicked)
            {
                WorldCoords mouse = new WorldCoords(0, 0);
                mouse.setVector2(theGame.userInput.mousePosition);
                if (mouse.isIn(this.ScreenPosition(), this.size))
                {
                    this.onClickEvent(mouse);
                    this.theGame.userInput.mouseClicked = false;
                }
            }
            base.Tick();
            
        }

        public override void Render(RenderSystem render)
        {
            render.Draw(render.primRect70, this.location.getRectangle(this.size), render.primRect70.Bounds, Color.Black);
            base.Render(render);
        }


        public override void onFinish()
        {
            this.onScreen = false;
        }

    }
}
