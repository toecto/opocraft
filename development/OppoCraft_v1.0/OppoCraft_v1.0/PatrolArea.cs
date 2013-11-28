using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class PatrolArea: Task
    {
        WorldCoords currWorldPath;
        WorldCoords destination;
        
        public PatrolArea(WorldPath p)
        {
           
            Random random = new Random();
            int worldIndex = random.Next(p.Count());
            this.destination = p[worldIndex];
           
            
        }
        

        public override bool Tick()
        {
            if (this.currWorldPath == this.destination)
            {
                return false;
            }
            
            else
            {
                this.unit.task.Add(new TaskGoTo(this.destination));
            }
            return true;
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
