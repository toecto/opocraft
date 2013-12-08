using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;

namespace OppoCraft
{
    class CommandTreeGrow : Task
    {

        enum Status
        {
            Adult,
            Died,
            Collected,
            Grow,
        }

        Status status;

        int restCooldown;

        public CommandTreeGrow(OppoMessage settings)
        {
            this.restCooldown = settings["cooldown"];
            this.status = Status.Collected;
        }

        public override bool Tick()
        {
            if (this.status == Status.Collected)
            {
                this.restCooldown--;
                if (this.restCooldown < 1)
                {
                    this.status = Status.Grow;
                    this.unit.animation.Clear();
                    this.unit.animation.startAction("Adult");
                    this.unit.animation.startAction("Grow");
                }
            }

            if (this.status == Status.Grow && this.unit.animation.current.First.Value.name == "Adult")
            {
                this.unit.status = "Adult";
                this.unit.currHP = this.unit.maxHP;
                return false;
            }
            return true;
        }
    }
}
