using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft 
{
    class TaskFindTarget : Task
    {
        string type;
        bool anySide;

        public TaskFindTarget(string type, bool anySide=false)
        {
            this.type = type;
            this.anySide = anySide;    
        }

        public override bool Tick()
        {
            double minDistance = 0, checkDistance;
            Unit target=null, unit;
            foreach (KeyValuePair<int, MapEntity> item in this.unit.theGame.map)
            {
                if (item.Value.GetType() != typeof(Unit)) continue;
                unit = (Unit)item.Value;
                if (unit.cid == this.unit.cid && !anySide) continue;
                if (unit.uid == this.unit.uid) continue;
                if (unit.type != this.type) continue;

                if (!unit.alive) continue;

                checkDistance=this.unit.location.Distance(item.Value.location);
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

            return true;
        }

    }
}
