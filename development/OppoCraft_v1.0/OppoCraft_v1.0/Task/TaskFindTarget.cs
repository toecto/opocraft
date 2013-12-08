using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OppoCraft 
{
    class TaskFindTarget : Task
    {
        List<string> type;
        bool anySide;
        List<int> ignore;
        int cooldown = 10;
        int currentCooldown = 0;
        int ignoreCooldown = 10;
        int currentignoreCooldown = 0;

        public TaskFindTarget(List<string> type, bool anySide=false)
        {
            this.type = type;
            this.anySide = anySide;
            
        }

        public override void onStart()
        {
            if (!this.unit.task.checkShared("IgnoreUnits"))
                this.unit.task.setShared("IgnoreUnits", new List<int>(8));
            this.ignore = this.unit.task.getShared<List<int>>("IgnoreUnits");

        }

        public override bool Tick()
        {
            currentCooldown--;
            if (currentCooldown > 0) return true;
            currentCooldown = cooldown;


            double minDistance = 0, checkDistance;
            Unit target=null;
            int range = this.unit.theGame.theGrid.getWorldCoords(new GridCoords(this.unit.viewRange, 0)).X;
            WorldCoords start = new WorldCoords(this.unit.location.X - range / 2, this.unit.location.Y - range / 2);
            WorldCoords end = new WorldCoords(start.X + range, start.Y + range);
            foreach (Unit unit in this.unit.theGame.map.units)
            {
                if (unit.cid == this.unit.cid && !anySide) continue;
                if (unit.uid == this.unit.uid) continue;
                if (!unit.location.isIn(start, end)) continue;
                if (!unit.alive) continue;
                if (!this.type.Contains(unit.type)) continue;
                if (this.ignore.Contains(unit.uid)) continue;

                checkDistance = this.unit.location.DistanceSqr(unit.location.X, unit.location.Y);
                if (checkDistance < minDistance || minDistance == 0)
                {
                    minDistance = checkDistance;
                    target = unit;
                }
            }

            if (target != null)
            {
                this.unit.task.setShared("targetUnit", target);
                return false;
            }

            currentignoreCooldown--;
            if (currentignoreCooldown < 0)
            {
                currentignoreCooldown = ignoreCooldown;
                this.unit.task.setShared("IgnoreUnits", new List<int>(8));
            }



            return true;
        }

    }
}
