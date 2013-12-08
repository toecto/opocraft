using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class UnitSelector : MapEntity
    {
        public Unit selected=null;
        public bool enabled = true;

        public UnitSelector()
        { 
            
        }

        public override void Tick()
        {
            if (this.enabled && this.theGame.userInput.mouseClicked)
            {
                this.selected = null;
                string[] selectable = { "Worker","Fighter", "Building"};
                WorldCoords click=this.theGame.render.getWorldCoords(this.theGame.userInput.mousePosition);
                foreach (Unit unit in this.theGame.map.UnitsIn(this.theGame.render.getWorldCoordsAbs(this.theGame.render.scroll.getVector2()), this.theGame.render.getWorldCoordsAbs(this.theGame.render.size.getVector2())))
                {
                    if (!selectable.Contains(unit.group)) continue;
                    if (unit.animation!=null && click.isIn(new WorldCoords(unit.location.X - unit.animation.current.First.Value.currentAnimation.file.size.X / 2, unit.location.Y - unit.animation.current.First.Value.currentAnimation.file.size.Y), unit.animation.current.First.Value.currentAnimation.file.size))
                    {
                        this.selected = unit;
                    }
                }
            }
        }


        public override void onStart()
        {
            this.location = new WorldCoords(0,10);
            this.selected = null;
        }

        public override void Render(RenderSystem render)
        {
            if (this.selected != null)
            {
                Vector2 position = render.getScreenCoords(new WorldCoords(this.selected.location.X - this.selected.size.X / 2-10, this.selected.location.Y - this.selected.size.Y/2));
                Vector2 size = render.getScreenCoordsAbs(this.selected.size);
                
                render.Draw(this.theGame.render.primCircle, new Rectangle((int)position.X, (int)position.Y, (int)size.X + 20, (int)size.Y), this.theGame.render.primCircle.Bounds, new Color(0, 255, 0));

            }
        }


    }
}
