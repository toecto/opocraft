using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OppoCraft
{
    public class CastleForm: GameForm
    {
        public CastleForm()
        {
            this.location.Y = 50;
            this.size.Y = 500;
            int topShift = 0;
            GameFormLabel label = new GameFormLabel("Castle");
            label.tag = "name";
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
            zones.Add(new GameFormRadioButton("Top Forest", "topforest"));
            zones.Add(new GameFormRadioButton("Central Forest", "centerforest"));
            zones.Add(new GameFormRadioButton("Bottom Forest", "bottomforest"));
            zones.Add(new GameFormRadioButton("Enemy Base", "enemybase"));
            zones.Add(new GameFormRadioButton("My Base", "mybase"));


            label = new GameFormLabel("Targets:");
            label.location.X = 20;
            label.location.Y = 50;
            this.controls.Add(label);

            GameFormCheckGroup targets = new GameFormCheckGroup();
            this.controls.Add(targets);
            targets.location.X = 20;
            targets.location.Y = 75;
            targets.Add(new GameFormCheckButton("Knights", "Knight"));
            targets.Add(new GameFormCheckButton("Archers", "Archer"));
            targets.Add(new GameFormCheckButton("Lumberjacks", "Lumberjack"));
            targets.Add(new GameFormCheckButton("Towers", "Tower"));
            targets.Add(new GameFormCheckButton("Castle", "Castle"));
            targets.tag = "targets";
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
            upDown = new GameFormUpDown(0, 5, 100);
            upDown.tag = "attack";
            upDown.location.X = 130;
            upDown.location.Y = topShift-7;
            this.controls.Add(upDown);

            label = new GameFormLabel("Attack range:");
            label.location.X = 200;
            label.location.Y = topShift;
            this.controls.Add(label);
            upDown = new GameFormUpDown(0, 1, 20);
            upDown.tag = "attackrange";
            upDown.location.X = 330;
            upDown.location.Y = topShift - 7;
            this.controls.Add(upDown);


            topShift += 50;
            label = new GameFormLabel("Attack Speed:");
            label.location.X = 20;
            label.location.Y = topShift;
            this.controls.Add(label);
            upDown = new GameFormUpDown(0, 1, 100);
            upDown.tag="attackspeed";
            upDown.location.X = 130;
            upDown.location.Y = topShift-7;
            this.controls.Add(upDown);

            label = new GameFormLabel("Training speed:");
            label.location.X = 200;
            label.location.Y = topShift;
            this.controls.Add(label);
            upDown = new GameFormUpDown(0, 10, 50);
            upDown.tag = "trainingspeed";
            upDown.location.X = 330;
            upDown.location.Y = topShift - 7;
            this.controls.Add(upDown);


            topShift += 50;
            label = new GameFormLabel("Speed:");
            label.location.X = 20;
            label.location.Y = topShift;
            this.controls.Add(label);
            upDown = new GameFormUpDown(0, 10, 25);
            upDown.tag="speed";
            upDown.location.X = 130;
            upDown.location.Y = topShift-7;
            this.controls.Add(upDown);

            label = new GameFormLabel("Total: ");
            label.location.X = 200;
            label.location.Y = topShift;
            this.controls.Add(label);

            label = new GameFormLabel("");
            label.tag = "cost";
            label.location.X = 250;
            label.location.Y = topShift;
            this.controls.Add(label);


            topShift += 50;
            label = new GameFormLabel("Armour:");
            label.location.X = 20;
            label.location.Y = topShift;
            this.controls.Add(label);
            upDown = new GameFormUpDown(0, 0, 80);
            upDown.tag = "armour";
            upDown.location.X = 130;
            upDown.location.Y = topShift - 10;
            this.controls.Add(upDown);

            GameFormToggleButton tbutton = new GameFormToggleButton("Stop trainning", "Start trainning");
            tbutton.size.X = 130;
            tbutton.location.X = 200;
            tbutton.location.Y = topShift;
            tbutton.tag = "training";
            this.controls.Add(tbutton);


            topShift += 50;
            label = new GameFormLabel("View range:");
            label.location.X = 20;
            label.location.Y = topShift;
            this.controls.Add(label);
            upDown = new GameFormUpDown(0, 15, 30);
            upDown.tag="viewrange";
            upDown.location.X = 130;
            upDown.location.Y = topShift - 10;
            this.controls.Add(upDown);

            button = new GameFormButton("  ALL IN! ");
            button.tag = "allin";
            button.size.X = 130;
            button.location.X = 200;
            button.location.Y = topShift;
            this.controls.Add(button);

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
