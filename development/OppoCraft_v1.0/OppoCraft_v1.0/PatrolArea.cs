using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class PatrolArea: Task
    {
        
        WorldPath currPath;
        WorldCoords w;
        
       public TaskGoTo patrol;
        public PatrolArea(WorldPath p)
        {
            this.currPath = p;
           
        }

        public override bool Tick()
        {
            
            return base.Tick();
        }

       

        public override void onStart()
        {
           
        }

        public override void onFinish()
        {
            base.onFinish();
        }
    }
}
