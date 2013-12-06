using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;
using System.Diagnostics;

namespace OppoCraft
{
    class TaskMortality : Task
    {

        enum Status { 
            Alive,
            Died
        }

        Status status = Status.Alive;

        int dyingCooldown = 300;

        public override bool Tick()
        {
            if(this.unit.currHP<=0 && this.status == Status.Alive)
            {
                this.status = Status.Died;

                this.unit.task.Clear();
                this.unit.task.Add(this);

                OppoMessage msg = new OppoMessage(OppoMessageType.ChangeState);
                msg.Text["onlyact"] = "Die";
                this.unit.AddCommand(msg);
            }

            if (this.status == Status.Died)
                this.dyingCooldown--;

            if (this.dyingCooldown < 1 && this.status == Status.Died)
            {
                OppoMessage msg = new OppoMessage(OppoMessageType.RemoveUnit);
                this.unit.AddCommand(msg);
                return false;
            }

            return true;
        }

        public override void onStart()
        {
            //this one must be clear;
        }

        public override void onFinish()
        {

        }
    }
}
