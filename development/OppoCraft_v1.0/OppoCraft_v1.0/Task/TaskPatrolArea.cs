using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    class TaskPatrolArea: Task
    {
        WorldCoords start;
        WorldCoords size;

        public TaskPatrolArea(WorldCoords start, WorldCoords size, bool isCentered=false)
        {
            if (isCentered)
                this.start = new WorldCoords(start.X-size.X/2,start.Y-size.Y/2);
            else
                this.start = start;

            this.size = size;
        }

        public override bool Tick()
        {
            if (!this.unit.task.isRunning(typeof(TaskGoTo)))
            {
                WorldCoords nextStep = new WorldCoords(start.X + Game1.rnd.Next(0, this.size.X), start.Y + Game1.rnd.Next(0, this.size.Y));
                this.unit.task.Add(new TaskGoTo(nextStep,0));
            }

            return true;
        }

        public override void onFinish()
        {
            this.unit.task.Remove(typeof(TaskGoTo));
        }

    }
}
