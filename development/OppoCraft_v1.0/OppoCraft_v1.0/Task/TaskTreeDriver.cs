using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    class TaskTreeDriver : Task
    {
        /*
         * if hp is 0, change tree state to dead
         * if tree is still growing, lumberjack unit cannot hit it
         * 
         * 
         * 
         */ 
        public override bool Tick()
        {
            if (this.unit.currHP == 0) 
            {
                testClient.OppoMessage msg = new testClient.OppoMessage(testClient.OppoMessageType.ChangeState);
                msg["state"] = (int)Unit.State.Dying;
                this.unit.AddCommand(msg);
            }
            if (this.unit.state)
            {
                testClient.OppoMessage msg = new testClient.OppoMessage(testClient.OppoMessageType.ChangeState);
                msg["state"] = (int)Unit.State.Dying;
                this.unit.AddCommand(msg);
            }
            
        }

    }
}
