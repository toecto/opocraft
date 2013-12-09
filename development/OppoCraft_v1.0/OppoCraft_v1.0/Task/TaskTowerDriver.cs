using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace OppoCraft
{
    class TaskTowerDriver : Task
    {

        enum Status { 
            Ready,
            Search,
            Fight,
        }

        Unit target;
        int range;
        int rangeSqr;
        Status status;
        int cooldown;

        public override bool Tick()
        {
            if (this.status == Status.Ready)
            {
                this.status = Status.Search;
                this.unit.task.Add(new TaskFindTarget(new string[] { "Knight", "Archer", "Lumberjack" }));
            }


            if (this.status == Status.Search && this.unit.task.checkShared("targetUnit"))
            {
                this.target=this.unit.task.removeShared<Unit>("targetUnit");
                this.range = this.unit.attackRange + this.target.sizeGrid.X - 1;
                this.rangeSqr = this.range * this.range;
                this.status = Status.Fight;
            }

            if(this.status == Status.Fight)
            {
                if ((int)this.unit.locationGrid.DistanceSqr(this.target.locationGrid.X, this.target.locationGrid.Y) <= this.rangeSqr && this.target.alive)
                {
                    if (this.cooldown <= 0)
                    {
                        this.cooldown = this.unit.attackSpeedReal;
                        OppoMessage msg;

                        msg = new OppoMessage(OppoMessageType.CreateEntity);
                        msg["uid"] = this.unit.theGame.CreateUID();
                        msg["ownercid"] = this.unit.theGame.cid;
                        msg["target"] = this.target.uid;
                        msg["owneruid"] = this.unit.uid;
                        msg["x"] = this.unit.location.X;
                        msg["y"] = this.unit.location.Y-250;
                        msg["damage"] = this.unit.damage;
                        msg["forcecreate"] = 1;
                        msg.Text["type"] = "Shell";
                        msg.Text["status"] = "Fireball";
                        msg.Text["class"] = "UnitShell";

                        this.unit.theGame.AddCommand(msg);
                    }
                    this.cooldown--;                
                }
                else
                {
                    this.status = Status.Ready;
                }
            }


            return true;
        }


        public override void onStart()
        {
            this.unit.task.Add(new TaskMortality());
            this.status=Status.Ready;
            this.unit.attackRange = 15;
            this.unit.viewRange = 15;
            this.unit.attackSpeed = 50;
        }

    }
}
