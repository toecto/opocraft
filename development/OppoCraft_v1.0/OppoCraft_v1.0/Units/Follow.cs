using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{

    class Follow : Task
    {
        public WorldCoords currDestination;
        
        public void compareLocation()
        {
            if (this.unit.currTarget.location != currDestination)
            {
                this.unit.task.Add(new  TaskGoTo(this.currDestination));
            }
        }
        public override bool Tick()
        {
            if (this.currDestination == null)
            {
                return false;

            }
            else { 
            
                //build second property to compary two location .and resend again
                compareLocation();
               // this.unit.task.Add(new TaskGoTo(this.unit.currTarget.location));
            }
            return true;
        }
        
       

    }
}
