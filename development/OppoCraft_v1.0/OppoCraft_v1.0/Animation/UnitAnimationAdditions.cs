using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    class UnitAnimationAdditions
    {
        public static void Render(Unit unit, RenderSystem render)
        {
            Vector2 position = render.getScreenCoords(unit.location);
            //render.Draw(render.primDot, position, new Rectangle(0, 0, 4, 4), Microsoft.Xna.Framework.Color.White);

            //position = Vector2.Subtract(position, Vector2.Divide(new Vector2(render.primRect.Bounds.Width,render.primRect.Bounds.Height), 2f));

            position.X -= render.primRect50.Bounds.Width / 2;
            if (unit.animation.current.First != null)
                position.Y -= unit.animation.current.First.Value.currentAnimation.file.size.Y / 2 + 5;
            if (unit.currHP > 0 && unit.currHP < unit.maxHP)
            {
                Rectangle bar = new Rectangle(0, 0, render.primRect50.Bounds.Width, 2);
                render.Draw(render.primDot, position, bar, Microsoft.Xna.Framework.Color.Red);
                bar = new Rectangle(0, 0, render.primRect50.Bounds.Width * unit.currHP/unit.maxHP, 2);
                render.Draw(render.primDot, position, bar, Microsoft.Xna.Framework.Color.GreenYellow);
            }
            string text = "";
            //text += unit.currHP + "/" + unit.maxHP + "\n";
            //text += unit.direction.ToString()+"\n";
            /*
            foreach (ActionAnimation anim in unit.animation.current)
            {
                text += anim.name + " " + anim.currentAnimation.currentFrame + "/" + anim.currentAnimation.frames + "\n";

            } /**/

            /*
            if (unit.type == "Tower")
            {
                foreach (KeyValuePair<Type, Task> item in unit.task.getTasks())
                {
                    text += item.Value.GetType().ToString() + "\n";

                }

                if (unit.task.isRunning(typeof(CommandMovement)))
                { 
                    CommandMovement cmd=(CommandMovement)unit.task.getTasks()[typeof(CommandMovement)];
                    text += "Movement " + unit.location.X + ":" + unit.location.Y + "  >  " + cmd.destination.X + ":" + cmd.destination.Y;
                
                }

            }
            /**/
            render.DrawText(text, position);
        }



    }
}
