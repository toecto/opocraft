using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
   
namespace OppoCraft
{
    class UnitDescription : MapEntity
    {
        public override void Render(RenderSystem render)
        {
            if (this.theGame.unitSelector.selected != null)
            {
                int scale = 20;
                int sizeX = this.theGame.worldMapSize.X / scale;
                int sizeY = this.theGame.worldMapSize.Y / scale;
                int shiftLeft = render.size.X - sizeX;
                int shiftTop = render.size.Y - sizeY;
                render.Draw(render.primRect50, new Rectangle(shiftLeft, shiftTop, sizeX, sizeY), new Rectangle(0, 0, 40, 24), Color.Gray);

                Unit unit = this.theGame.unitSelector.selected;
                
                string text = (unit.isMy?"Friend":"Enemy") + "\n"
                    + "Type: "+unit.type +"\n"
                    + "Group: "+unit.group+"\n"
                    + "HP: " + unit.currHP+"/" +unit.maxHP+ "\n"
                    ;
                

                foreach (KeyValuePair<Type, Task> item in unit.task.getTasks())
                {
                    text += item.Value.GetType().ToString() + "\n";

                }
                render.DrawText(text, new Vector2(shiftLeft+20, shiftTop+20));

            }
        }

        public override void onStart()
        {
            this.location = this.theGame.worldMapSize;
        }

    }
}
