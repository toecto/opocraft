using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OppoCraft
{
    public class CastleForm: GameForm
    {
        UnitCastle castle;

        public CastleForm(UnitCastle castle)
        {
            this.location.Y = 50;
            this.size.Y = 500;
            int topShift = 0;
            this.castle = castle;
            GameFormLabel label = new GameFormLabel("Castle");
            label.location.X = 20;
            label.location.Y = 20;
            this.controls.Add(label);

            label = new GameFormLabel("Patrol zones:");
            label.location.X = 200;
            label.location.Y = 50;
            this.controls.Add(label);

            GameFormRadioGroup zones = new GameFormRadioGroup();
            this.controls.Add(zones);
            zones.location.X = 200;
            zones.location.Y = 75;
            zones.tag="zone";
            zones.Add(new GameFormRadioButton("Top Forest", "Top Forest"));
            zones.Add(new GameFormRadioButton("Center Forest", "Center Forest"));
            zones.Add(new GameFormRadioButton("Bottom Forest", "Bottom Forest"));
            zones.Add(new GameFormRadioButton("Enemy Base", "Enemy Base"));
            zones.Add(new GameFormRadioButton("My Base", "My Base"));


            label = new GameFormLabel("Targets:");
            label.location.X = 20;
            label.location.Y = 50;
            this.controls.Add(label);

            GameFormCheckGroup targets = new GameFormCheckGroup();
            this.controls.Add(targets);
            targets.location.X = 20;
            targets.location.Y = 75;
            targets.Add(new GameFormCheckButton("Knights", "Knights"));
            targets.Add(new GameFormCheckButton("Archers", "Archers"));
            targets.Add(new GameFormCheckButton("Lumberjacks", "Lumberjacks"));
            targets.Add(new GameFormCheckButton("Towers", "Towers"));
            targets.Add(new GameFormCheckButton("Castle", "Castle"));
            targets.tag="targets";
            label = new GameFormLabel("Stats:");
            label.location.X = 20;
            topShift = label.location.Y = 75 + 5 * 25;
            this.controls.Add(label);


            GameFormButton button;
            GameFormUpDown upDown;

            topShift += 25;
            label = new GameFormLabel("Attack:");
            label.location.X = 20;
            label.location.Y = topShift; 
            this.controls.Add(label);
            upDown = new GameFormUpDown(0, 0, 10);
            upDown.tag = "attack";
            upDown.location.X = 130;
            upDown.location.Y = topShift-7;
            this.controls.Add(upDown);

            label = new GameFormLabel("Attack range:");
            label.location.X = 200;
            label.location.Y = topShift;
            this.controls.Add(label);
            upDown = new GameFormUpDown(0, 0, 10);
            upDown.tag = "attackrange";
            upDown.location.X = 330;
            upDown.location.Y = topShift - 7;
            this.controls.Add(upDown);


            topShift += 50;
            label = new GameFormLabel("Attack Speed:");
            label.location.X = 20;
            label.location.Y = topShift;
            this.controls.Add(label);
            upDown = new GameFormUpDown(0, 0, 10);
            upDown.tag="attackspeed";
            upDown.location.X = 130;
            upDown.location.Y = topShift-7;
            this.controls.Add(upDown);

            label = new GameFormLabel("Training speed:");
            label.location.X = 200;
            label.location.Y = topShift;
            this.controls.Add(label);
            upDown = new GameFormUpDown(0, 0, 10);
            upDown.tag = "trainingspeed";
            upDown.location.X = 330;
            upDown.location.Y = topShift - 7;
            this.controls.Add(upDown);


            topShift += 50;
            label = new GameFormLabel("Speed:");
            label.location.X = 20;
            label.location.Y = topShift;
            this.controls.Add(label);
            upDown = new GameFormUpDown(0, 0, 10);
            upDown.tag="speed";
            upDown.location.X = 130;
            upDown.location.Y = topShift-7;
            this.controls.Add(upDown);

            label = new GameFormLabel("Total: 100");
            label.location.X = 200;
            label.location.Y = topShift;
            this.controls.Add(label);


            topShift += 50;
            label = new GameFormLabel("Armor:");
            label.location.X = 20;
            label.location.Y = topShift;
            this.controls.Add(label);
            upDown = new GameFormUpDown(0, 0, 10);
            upDown.tag = "armor";
            upDown.location.X = 130;
            upDown.location.Y = topShift - 10;
            this.controls.Add(upDown);

            button = new GameFormButton("Stop production");
            button.size.X = 130;
            button.location.X = 200;
            button.location.Y = topShift;
            this.controls.Add(button);


            topShift += 50;
            label = new GameFormLabel("View range:");
            label.location.X = 20;
            label.location.Y = topShift;
            this.controls.Add(label);
            upDown = new GameFormUpDown(0, 0, 10);
            upDown.tag="viewrange";
            upDown.location.X = 130;
            upDown.location.Y = topShift - 10;
            this.controls.Add(upDown);

            button = new GameFormButton("  ALL IN! ");
            button.size.X = 130;
            button.location.X = 200;
            button.location.Y = topShift;
            this.controls.Add(button);

            this.onClick += updateChanges;
        }

        public void updateChanges(GameFormControl obj, WorldCoords mouse)
        {

            foreach (GameFormControl item in this.controls)
            {
                if (item.tag != "")
                {
                    if (item.GetType() == typeof(GameFormUpDown))
                        this.castle.factorySettings[item.tag] = ((GameFormUpDown)item).value;
                    if (item.GetType() == typeof(GameFormCheckGroup))
                    {
                        string[] strs = ((GameFormCheckGroup)item).getSelectedValues<string>();
                        this.castle.factorySettings.Text[item.tag] = String.Join(",", strs);
                    }
                        
                    if (item.GetType() == typeof(GameFormRadioGroup))
                        if (((GameFormRadioGroup)item).selected!=null)
                            this.castle.factorySettings.Text[item.tag] = (string)((GameFormRadioGroup)item).selected.value;
                }
            }

            Debug.WriteLine(this.castle.factorySettings.ToString());
        }


        public override void Tick()
        {
            base.Tick();
        }

        public override void onStart()
        {
            base.onStart();

        }

        public override void onFinish()
        {
            base.onFinish();
        }

    }
}
