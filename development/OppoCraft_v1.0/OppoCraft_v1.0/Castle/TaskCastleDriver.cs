using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using testClient;

namespace OppoCraft
{
    class TaskCastleDriver: Task
    {
        UnitCastle castle;

        public override bool Tick()
        {
            if (this.castle.factorySettings["training"] == 0) return true;
            this.castle.trainingCooldown--;
            if (this.castle.trainingCooldown > 0) return true;
            this.castle.trainingCooldown = this.castle.trainingSpeedReal;

            this.castle.tryToSpawn();

            return true;
        }

        public override void onStart()
        {
            this.castle = (UnitCastle)this.unit;
            this.unit.task.Add(new TaskTowerDriver());
            this.unit.task.Add(new TaskCastleSettingsForm());
        }


    }
}
