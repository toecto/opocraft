using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;

namespace OppoCraft
{
    class CommandChangeUnitState: Task
    {
        OppoMessage message;
        public CommandChangeUnitState(OppoMessage message)
        {
            this.message = message;
        }

        public override void onStart()
        {
            
            if (this.message.Text.ContainsKey("stopact"))
            {
                this.unit.animation.stopAction(this.message.Text["stopact"]);
            }
            if (this.message.Text.ContainsKey("onlyact"))
            {
                this.unit.animation.Clear();
                this.unit.animation.startAction(this.message.Text["onlyact"]);
            }

            if (this.unit.alive && this.message.Text.ContainsKey("startact"))
            {
                this.unit.animation.startAction(this.message.Text["startact"]);
            }

            if (this.message.Text.ContainsKey("startactforced"))
            {
                this.unit.animation.startAction(this.message.Text["startactforced"]);
            }
            if (this.message.ContainsKey("addhp"))
            {
                this.unit.currHP += this.message["addhp"];
            } 
            
            if (this.message.Text.ContainsKey("status"))
            {
                this.unit.status = this.message.Text["status"];
            }
            if (this.message.ContainsKey("direction"))
            {
                this.unit.direction = (Unit.Direction)this.message["direction"];
            }
            

        }
    }
}
