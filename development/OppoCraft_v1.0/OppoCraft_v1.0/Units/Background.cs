using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    class Background : MapEntity
    {
        public int repeatX;
        public int repeatY;
        SimpleAnimation animation;

        public override void onStart()
        {
            this.size = this.theGame.worldMapSize;
            this.animation = new SimpleAnimation(this.theGame.graphContent.files["Grass"], 0, 0, 1, 1, false);
            this.repeatX = this.size.X / this.animation.file.size.X + 1;
            this.repeatY = this.size.Y / this.animation.file.size.Y + 1;
            
        }

        public override void Tick()
        {
        }

        public override void Render(RenderSystem render)
        {
            Rectangle rec = new Rectangle(0, 0, this.animation.file.size.X, this.animation.file.size.Y);
            for(int x=0;x<this.repeatX;x++)
            {
                for (int y = 0; y < this.repeatY; y++)
                {
                    Vector2 crd = render.getScreenCoords(new WorldCoords(x * this.animation.file.size.X, (int)(y * this.animation.file.size.Y * 1.6)));
                    render.Draw(this.animation.file.texture, crd, rec, Color.White);
                }

            }
        }

    }
}
