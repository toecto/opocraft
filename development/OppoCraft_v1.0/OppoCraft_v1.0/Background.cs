using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    class Background : MapEntity
    {
        public int repeatX;
        public int repeatY;
        SimpleAnimation animation;
        public Background(int uid)
        {
            this.uid = uid;
        }

        public override void onStart()
        {
            this.size = this.theGame.worldMapSize;
            this.animation = new SimpleAnimation(this.theGame.graphContent.files["Grass"], 0, 0, 1, 1, false);
            this.repeatX = this.size.X / this.animation.file.width + 1;
            this.repeatY = this.size.Y / this.animation.file.height + 1;
            
        }


}
