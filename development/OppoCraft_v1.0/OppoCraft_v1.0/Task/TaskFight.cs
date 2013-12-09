using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace OppoCraft
{
    class TaskFight: Task
    {
        Unit target;
        GridCoords going=null;
        int cooldown;
        List<int> ignore;
        int range;
        int rangeSqr;

        public TaskFight(Unit target)
        {
            this.target = target;
        }

        public override bool Tick()
        {
            if (!target.alive)
            {
                if (this.going!=null)
                    this.unit.task.Remove(typeof(TaskGoTo));
                //this.unit.task.setShared("IgnoreUnits", new List<int>(8));
                return false;
            }

            if ((int)this.unit.locationGrid.DistanceSqr(this.target.locationGrid.X, this.target.locationGrid.Y) <= this.rangeSqr)
            {

                if (this.going != null)
                {
                    OppoMessage msg = new OppoMessage(OppoMessageType.Stop);
                    msg["x"] = this.unit.location.X;
                    msg["y"] = this.unit.location.Y;
                    this.unit.AddCommand(msg);
                    this.going = null;
                }

                if (this.cooldown <= 0)
                {
                    this.cooldown = this.unit.attackSpeed;
                    OppoMessage msg;
                    
                    msg = new OppoMessage(OppoMessageType.ChangeState);
                    msg["uid"] = this.target.uid;
                    msg["addhp"] = -this.unit.damage;
                    msg.Text["startact"] = "TakeDamage";
                    this.unit.theGame.AddCommand(msg);

                    msg = new OppoMessage(OppoMessageType.ChangeState);
                    msg.Text["startact"] = "Attack";
                    msg["direction"] = (int)CommandMovement.vectorToDirection(Vector2.Subtract(target.location.getVector2(),this.unit.location.getVector2()));
                    this.unit.AddCommand(msg);
                }
                this.cooldown--;                

            }
            else
            {
                if (!this.unit.task.isRunning(typeof(TaskGoTo)) || !this.target.locationGrid.Equals(this.going))
                {
                    WorldPath path = this.unit.theGame.pathFinder.GetPath(this.unit.location, target.location, this.range);
                    if (path == null)
                    {
                        this.ignore.Add(target.uid);
                        return false;
                    }

                    this.unit.task.Add(new TaskGoTo(path, target.location, this.range));
                    this.going = target.locationGrid;
                }
            }
            
            return true;
        }

        public override void onStart()
        {
            if (!this.unit.task.checkShared("IgnoreUnits"))
                this.unit.task.setShared("IgnoreUnits", new List<int>(8));
            this.ignore = this.unit.task.getShared<List<int>>("IgnoreUnits");
            this.range = this.unit.attackRange + this.target.sizeGrid.X - 1;
            this.rangeSqr = this.range * this.range;
        }

        public override void onFinish()
        {
            OppoMessage msg = new OppoMessage(OppoMessageType.ChangeState);
            msg.Text["stopact"] = "Attack";
            this.unit.AddCommand(msg);
        }
    }
}
