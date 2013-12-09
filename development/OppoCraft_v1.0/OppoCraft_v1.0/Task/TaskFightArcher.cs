using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    class TaskFightArcher:Task
    {
        Unit target;
        GridCoords going=null;
        int cooldown;
        List<int> ignore;
        int range;
        int rangeSqr;

        public TaskFightArcher(Unit target)
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

                //if (this.going != null)
                {
                    //OppoMessage msg = new OppoMessage(OppoMessageType.Stop);
                    //this.unit.AddCommand(msg);
                    this.unit.task.Remove(typeof(TaskGoTo));
                    this.going = null;
                }

                if (this.cooldown <= 0)
                {
                    this.cooldown = this.unit.attackSpeed;
                    OppoMessage msg;

                    msg = new OppoMessage(OppoMessageType.CreateEntity);
                    msg["uid"] = this.unit.theGame.CreateUID();
                    msg["ownercid"] = this.unit.theGame.cid;
                    msg["target"] = this.target.uid;
                    msg["owneruid"] = this.unit.uid;
                    msg["x"] = this.unit.location.X;
                    msg["y"] = this.unit.location.Y;
                    msg["damage"] = this.unit.damage;
                    msg["forcecreate"] = 1;
                    msg.Text["type"] = "Shell";
                    msg.Text["status"] = "Arrow";
                    msg.Text["class"] = "UnitShell";

                    this.unit.theGame.AddCommand(msg);

                    msg = new OppoMessage(OppoMessageType.ChangeState);
                    msg.Text["startact"] = "Attack";
                    msg["direction"] = (int)CommandMovement.vectorToDirection(Vector2.Subtract(target.location.getVector2(), this.unit.location.getVector2()));
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
            this.cooldown = Game1.rnd.Next(0,this.unit.attackSpeed);
            this.range = this.unit.attackRange+this.target.sizeGrid.X-1;
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
