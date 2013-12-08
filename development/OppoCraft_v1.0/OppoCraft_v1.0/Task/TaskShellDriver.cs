using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;

namespace OppoCraft 
{
    class TaskShellDriver : Task
    {
        Unit target;
        int ownerUid;
        
        public TaskShellDriver()
        {

        }

        public override bool Tick()
        {
            if (!this.unit.task.isRunning(typeof(CommandFlyToEntity)) || !this.target.alive)
            {
                return false;
            }
            return true;
        }

        public override void onStart()
        {
            this.target = (Unit)this.unit.theGame.map.getById(this.unit.settings["target"]);
            this.ownerUid = this.unit.settings["owneruid"];
        }

        public override void onFinish()
        {
            OppoMessage msg;
            msg = new OppoMessage(OppoMessageType.RemoveUnit);
            this.unit.AddCommand(msg);


            if (this.target.alive)
            {
                msg = new OppoMessage(OppoMessageType.ChangeState);
                msg["uid"] = this.target.uid;
                msg["addhp"] = -this.unit.damage;
                msg.Text["startact"] = "TakeDamage";
                this.unit.theGame.AddCommand(msg);

                msg = new OppoMessage(OppoMessageType.ChangeState);
                msg["uid"] = ownerUid;
                msg["addxp"] = this.unit.damage;
                this.unit.theGame.AddCommand(msg);
            }

        }
    }
}
