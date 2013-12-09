using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    class TaskArcherDriver: Task
    {
        enum Status
        {

            Main,
            Searching,
            Fighting

        }

        Status status;

        public override bool Tick()
        {
            if (this.status == Status.Main)
            {
                this.status = Status.Searching;
                this.unit.task.Add(new TaskFindTarget(new List<string>(4) { "Knight", "Archer", "Tower", "Castle" }));
                this.unit.task.Add(new TaskPatrolArea(new WorldCoords(0, 0), this.unit.theGame.worldMapSize));
                return true;
            }

            if (this.status == Status.Searching && this.unit.task.checkShared("targetUnit"))
            {
                this.status = Status.Fighting;
                this.unit.task.Add(new TaskFightArcher(this.unit.task.removeShared<Unit>("targetUnit")));
                this.unit.task.Remove(typeof(TaskPatrolArea));
                return true;
            }

            if (this.status == Status.Fighting && !this.unit.task.isRunning(typeof(TaskFightArcher)))
            {
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
