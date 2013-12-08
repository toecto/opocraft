using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    class TaskPatrol:Task
    {
        List<WorldCoords> points;

        public TaskPatrol(List<WorldCoords> points)
        {
            this.points = points;
        }

        public override bool Tick()
        {
            if (!this.unit.task.isRunning(typeof(TaskGoTo)))
            {
                WorldCoords nextStep = this.points[Game1.rnd.Next(0, this.points.Count - 1)];
                this.unit.task.Add(new TaskGoTo(nextStep,0));
            }

            return true;
        }

    }
}
