using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{

    class Follow : Task
    {
        public Unit currDestination;
        
        public void compareLocation()
        {
            if (this.unit.currTarget.location != currDestination.location)
            {
                this.unit.task.Add(new  TaskGoTo(this.currDestination.location));
            }
        }
        public override bool Tick()
        {
            if (this.unit.currTarget == null)
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
        public Unit CurrDestination
        {
            get
            {
                return currDestination;
            }
            set
            {
                this.currDestination = value;
            }
        }
       

    }
}
