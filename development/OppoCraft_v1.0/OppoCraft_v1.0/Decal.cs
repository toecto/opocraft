using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    class Decal : MapEntity
    {
        SimpleAnimation animation;

        public Decal(OppoMessage settings)
        {
            this.settings = settings;
        }

        public override void onStart()
        {
            this.animation = this.theGame.graphContent.GetDecaleAnimation(this.settings.Text["name"]);
            this.animation.Random();
        }


        public override void Tick()
        {
            
        }

        public override void Render(RenderSystem render)
        {
            
        }
    }
}
