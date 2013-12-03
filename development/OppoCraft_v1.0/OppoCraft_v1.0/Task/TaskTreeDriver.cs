using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;

namespace OppoCraft
{
    class TaskTreeDriver: Task
    {
        enum Status
        {
            Adult,
            Died,
            Collected,
            Grow,
        }

        Status status = Status.Adult;


        public override void onStart()
        {
            this.unit.status = "Adult";
        }

        public override bool Tick()
        {
            if (!this.unit.alive && this.status == Status.Adult)
            {
                OppoMessage msg = new OppoMessage(OppoMessageType.ChangeState);
                msg.Text["startact"] = "Die";
                msg.Text["status"] = "ReadyToCollect";
                this.unit.AddCommand(msg);
                this.status = Status.Died;
            }

            if (this.status == Status.Died && this.unit.status == "ReadyToCollect")
            {
                OppoMessage msg = new OppoMessage(OppoMessageType.ChangeState);
                msg.Text["status"] = "Collected";
                msg.Text["startact"] = "Collected";
                this.unit.AddCommand(msg);
            }

            if (this.status == Status.Died && this.unit.status == "Collected")
            {
                OppoMessage msg = new OppoMessage(OppoMessageType.TreeGrow);
                msg["cooldown"] = 400;
                this.unit.AddCommand(msg);
                this.status = Status.Grow;
            }

            if (this.status == Status.Grow && this.unit.status == "Adult")
            {
                this.status = Status.Adult;
            }


            return true;
        }

        public override void onFinish()
        {

        }
    }
}
