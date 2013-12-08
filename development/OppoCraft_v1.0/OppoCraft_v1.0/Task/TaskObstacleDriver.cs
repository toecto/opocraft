using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OppoCraft
{
    class TaskObstacleDriver: Task
    {
        public override bool Tick()
        {
            return false;
        }

        public override void onStart()
        {
            this.unit.animation.Clear();
            this.unit.animation.startAction(this.unit.type);
        }

        public override void onFinish()
        {
        }
    }
}
