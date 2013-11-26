using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class PatrolArea: Task
    {

        int currStep;
        int totalSteps;
        WorldPath worldPath;
        Vector2 destination;
        WorldCoords area;
        
        public PatrolArea(WorldPath p)
        {   
            this.worldPath = p;
        }
        public void GetPath()
        {
            this.worldPath = this.unit.theGame.theGrid.thePathFinder.GetPath(this.unit.location, this.area);
            if (this.worldPath == null)
                return;
            this.totalSteps = this.worldPath.Count();

            this.destination = this.worldPath.First.Value.getVector2();
        }

        public override bool Tick()
        {
            if (this.worldPath != null)
            {
                for (int i = 0; i <= this.worldPath.Count(); i++)
                {
                    int number = this.worldPath.Count();
                    this.destination = this.worldPath.Count(i) - 1;
                }

                
                return true;
            }
            
            else
            {
                return  false;
            }
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
