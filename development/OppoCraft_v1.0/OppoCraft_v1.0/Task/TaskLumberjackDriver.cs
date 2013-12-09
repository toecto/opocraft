using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OppoCraft
{
    class TaskLumberjackDriver: Task
    {
        enum Status
        {

            Main,
            Search,
            Fighting,
            Returning,

        }

        Status status;

        Unit target;

        public override bool Tick()
        {
            if (this.status == Status.Main)
            {
                this.status = Status.Search;
                if (this.unit.settings.Text.ContainsKey("targets"))
                {
                    string[] targets = (this.unit.settings.Text["targets"] + ",Tree").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    this.unit.task.Add(new TaskFindTarget(targets));
                }
                else
                    this.unit.task.Add(new TaskFindTarget(new string[] { "Tree" }));

                if (this.unit.settings.Text.ContainsKey("zone"))
                {
                    if (this.unit.theGame.zones.ContainsKey(this.unit.settings.Text["zone"]))
                    {
                        MapEntity zone = this.unit.theGame.zones[this.unit.settings.Text["zone"]];
                        this.unit.task.Add(new TaskPatrolArea(zone.location, zone.size));
                    }
                    else
                        Debug.WriteLine("Where is zone : >"+this.unit.settings.Text["zone"]+"<");
                }
                else
                this.unit.task.Add(new TaskPatrolArea(new WorldCoords(0, 0), this.unit.theGame.worldMapSize));
                return true;
            }

            if (this.status == Status.Search && this.unit.task.checkShared("targetUnit"))
            {
                this.status = Status.Fighting;
                this.unit.task.Add(new TaskFight(this.target=this.unit.task.removeShared<Unit>("targetUnit")));
                this.unit.task.Remove(typeof(TaskPatrolArea));
                return true;
            }

            if (this.status == Status.Fighting && !this.unit.task.isRunning(typeof(TaskFight)))
            {
                if (this.target != null && !this.target.alive && this.target.type=="Tree")
                {
                    if (this.unit.isMy)
                        this.unit.task.Add(new TaskGoTo(this.unit.theGame.myBase, 2));
                    else
                        this.unit.task.Add(new TaskGoTo(this.unit.theGame.enemyBase, 2));

                    this.status = Status.Returning;
                }
                else
                    this.status = Status.Main;
                return true;
            }
            if (this.status == Status.Returning && !this.unit.task.isRunning(typeof(TaskGoTo)))
            {
                this.unit.theGame.userPoints.add(100);
                this.status = Status.Main;
                return true;
            }


            if (this.unit.task.checkShared("reset"))
            {
                this.unit.task.removeShared<bool>("reset");
                this.status = Status.Main;
                return true;
            }


            return true;
        }

        public override void onStart()
        {
            this.unit.task.Add(new TaskMortality());
            this.status = Status.Main;
        }

        public override void onFinish()
        {
        }
    }
}
