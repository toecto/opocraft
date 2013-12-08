using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    class TaskCastleDriver: Task
    {

        CastleForm form=null;

        public override bool Tick()
        {
            if (form == null)
            {
                if (this.unit.theGame.unitSelector.selected == this.unit)
                    this.unit.theGame.forms.Add(this.form = new CastleForm((UnitCastle)this.unit));
            }
            else
            { 
                if (this.unit.theGame.unitSelector.selected != this.unit)
                   this.form = null;
            }

            return true;
        }

        public override void onStart()
        {
            //this.unit.task.Add(new TaskTowerDriver());
        }
    }
}
