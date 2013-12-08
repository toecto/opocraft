﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    class TaskLumberjackDriver: Task
    {
        enum Status
        {

            Main,
            Search,
            Fighting

        }

        Status status;

        public override bool Tick()
        {
            if (this.status == Status.Main)
            {
                this.status = Status.Search;
                this.unit.task.Add(new TaskFindTarget(new List<string>(4) { "Tree" }, true));
                this.unit.task.Add(new TaskPatrolArea(new WorldCoords(0, 0), this.unit.theGame.worldMapSize));
                return true;
            }

            if (this.status == Status.Search && this.unit.task.checkShared("targetUnit"))
            {
                this.status = Status.Fighting;
                this.unit.task.Add(new TaskFight(this.unit.task.removeShared<Unit>("targetUnit")));
                this.unit.task.Remove(typeof(TaskPatrolArea));
                return true;
            }

            if (this.status == Status.Fighting && !this.unit.task.isRunning(typeof(TaskFight)))
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
