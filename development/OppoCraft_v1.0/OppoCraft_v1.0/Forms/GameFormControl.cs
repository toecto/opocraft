using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class GameFormControl : MapEntity
    {
        public GameForm parentForm;
        public GameFormControlCollection controls;
        public GameFormControl parent;
        public string tag="";
        public WorldCoords ScreenPosition()
        {

            if (this.parent != this)
            {
                WorldCoords shift=this.parent.ScreenPosition();
                return new WorldCoords(shift.X + this.location.X, shift.Y + this.location.Y); 
            }
            else
                return new WorldCoords(this.location.X, this.location.Y); 
        
        }
        public bool disabled;

        public GameFormControl()
        {
            this.parentForm = null;
            this.parent = this;
            this.location = new WorldCoords(0, 0);
            this.size = new WorldCoords(20, 20);
            this.controls = new GameFormControlCollection(this);
            this.disabled = false;

        }

        public void setParent(GameFormControl parent)
        {
            this.parent = parent;
            this.setForm(parent.parentForm);
        }

        public void setForm(GameForm form)
        {
            this.parentForm = form;
            foreach (GameFormControl item in this.controls)
            {
                item.setForm(form);
            }
        }

        public override void Tick()
        {
            if (this.disabled) return;
            foreach (GameFormControl item in this.controls)
            {
                item.Tick();
            }
        }

        public override void Render(RenderSystem render)
        {
            foreach (GameFormControl control in this.controls)
            {
                control.Render(render);
            }
            //render.Draw(render.primRect50, this.ScreenPosition().getRectangle(this.size), render.primRect50.Bounds, Color.IndianRed);
        }



        public delegate void onClickHandler(GameFormControl GameFormControl, WorldCoords mouse);

        public event onClickHandler onClick;

        public void onClickEvent(WorldCoords mouse)
        {
            if (this.disabled) return;

            foreach (GameFormControl item in this.controls)
            {
                if (mouse.isIn(item.ScreenPosition(), item.size))
                {
                    item.onClickEvent(mouse);
                    break;
                }
            }

            if (this.onClick != null)
                this.onClick(this, mouse);
        }
    }
}
