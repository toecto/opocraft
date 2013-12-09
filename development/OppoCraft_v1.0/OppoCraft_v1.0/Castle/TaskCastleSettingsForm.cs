using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OppoCraft
{
    class TaskCastleSettingsForm : Task
    {

        CastleForm form = null;
        UnitCastle castle;

        public override bool Tick()
        {
            if (form == null)
            {
                if (this.unit.theGame.unitSelector.selected == this.unit)
                {
                    this.initForm();
                }
            }
            else
            {
                if (!this.form.onScreen)
                {
                    this.form = null;
                    if (this.unit.theGame.unitSelector.selected == this.castle)
                        this.unit.theGame.unitSelector.selected = null;
                }
            }

            return true;
        }

        public override void onStart()
        {
            this.castle = (UnitCastle)this.unit;
            
        }

        public void initForm()
        {
            this.unit.theGame.forms.Add(this.form = new CastleForm());
            this.form.onClick += handleForm;
            this.form.findByTag("allin").onClick += this.handleAllIn;
            this.updateForm(this.form);
            if (this.castle.factorySettings["attackrange"] == 1)
                this.form.findByTag("attackrange").disabled = true;
        
        }

        public void handleAllIn(GameFormControl obj, WorldCoords mouse)
        {
            string goodType=this.castle.factorySettings.Text["unittype"];
            foreach(Unit unit in this.unit.theGame.map.units)
            {
                if (unit.isMy&& unit.type.Equals(goodType))
                { 
                    unit.task.Remove(typeof(TaskFight));
                    unit.task.Remove(typeof(TaskFightArcher));
                    unit.task.Remove(typeof(TaskGoTo));
                    unit.task.setShared("reset", true);
                    unit.settings.Text["zone"]="enemybase";
                }
            }
        }

        public void handleAllback(GameFormControl obj, WorldCoords mouse)
        {
            string goodType = this.castle.factorySettings.Text["unittype"];
            foreach (Unit unit in this.unit.theGame.map.units)
            {
                if (unit.isMy && unit.type.Equals(goodType))
                {
                    unit.task.Remove(typeof(TaskFight));
                    unit.task.Remove(typeof(TaskFightArcher));
                    unit.task.Remove(typeof(TaskGoTo));
                    unit.task.setShared("reset", true);
                    unit.settings.Text["zone"] = "mybase";
                }
            }
        }
        public void handleForm(GameFormControl obj, WorldCoords mouse)
        {

            foreach (GameFormControl item in obj.parentForm.controls)
            {
                if (item.tag != "")
                {
                    switch (item.GetType().Name)
                    {
                        case "GameFormUpDown":
                            this.castle.factorySettings[item.tag] = ((GameFormUpDown)item).value;
                            break;

                        case "GameFormCheckGroup":
                            string[] strs = ((GameFormCheckGroup)item).getSelectedValues<string>();
                            this.castle.factorySettings.Text[item.tag] = String.Join(",", strs);
                            break;
                        case "GameFormRadioGroup":
                            if (((GameFormRadioGroup)item).selected != null)
                                this.castle.factorySettings.Text[item.tag] = (string)((GameFormRadioGroup)item).selected.value;
                            break;
                        case "GameFormToggleButton":
                            this.castle.factorySettings[item.tag] = ((GameFormToggleButton)item).isOn ? 1 : 0;
                            break;
                    }
                }

                ((GameFormLabel)obj.parentForm.findByTag("cost")).Text = this.castle.countCost().ToString();
            }
            
            //Debug.WriteLine(this.castle.factorySettings.ToString());
        }

        public void updateForm(GameForm form)
        {

            GameFormControl control;
            foreach (string tag in this.castle.factorySettings.Keys)
            {
                control = form.findByTag(tag);
                if (control == null) continue;
                switch (control.GetType().Name)
                {
                    case "GameFormUpDown":
                        ((GameFormUpDown)control).value = this.castle.factorySettings[tag];
                        break;
                    case "GameFormToggleButton":
                        ((GameFormToggleButton)control).isOn = this.castle.factorySettings[tag]==1;
                        break;
                }
            }

            foreach (string tag in this.castle.factorySettings.Text.Keys)
            {
                control = form.findByTag(tag);
                if (control == null) continue;
                switch (control.GetType().Name)
                {
                    case "GameFormCheckGroup":
                        ((GameFormCheckGroup)control).setSelectedValues(this.castle.factorySettings.Text[tag].Split(','));
                        break;

                    case "GameFormRadioGroup":
                        ((GameFormRadioGroup)control).setSelectedValue(this.castle.factorySettings.Text[tag]);
                        break;
                    case "GameFormLabel":
                        ((GameFormLabel)control).Text=this.castle.factorySettings.Text[tag];
                        break;
                }
            }

            ((GameFormLabel)form.findByTag("cost")).Text = this.castle.countCost().ToString();
        }

    }
}
