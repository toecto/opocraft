using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OppoCraft
{
    public class GameFormUpDown : GameFormControl
    {
        public int value=0;
        public int max = int.MaxValue;
        public int min = int.MinValue;
        GameFormLabel labelValue;
        GameFormButton up;
        GameFormButton down;


        public GameFormUpDown(int val = 0)
        {
            this.value = val;
            this.init();
        }

        public GameFormUpDown(int val, int min, int max)
        {
            this.value = val;
            this.min = min;
            this.max = max;
            this.size = new WorldCoords(20, 40);
            this.init();
        }

        public void init()
        {
            this.labelValue = new GameFormLabel(value.ToString());
            this.labelValue.location.X += 19;
            this.labelValue.location.Y += 8;

            this.up = new GameFormButton("+");
            this.up.size = new WorldCoords(20, 20);
            this.up.onClick += this.UpHandler;

            this.down = new GameFormButton("-");
            this.down.size = new WorldCoords(20, 20);
            this.down.location.Y += 20;
            this.down.onClick += this.DownHandler;

            this.controls.Add(this.labelValue);
            this.controls.Add(this.up);
            this.controls.Add(this.down);
        }



        public override void Tick()
        {
            base.Tick();
            if (this.value > this.max) this.value = this.max;
            if (this.value < this.min) this.value = this.min;

            this.up.disabled = (this.value == this.max);
            this.down.disabled = (this.value == this.min);
            this.labelValue.Text = this.value.ToString();

        }



        public void UpHandler(GameFormControl obj, WorldCoords mouse)
        {
            this.value++;
        }

        public void DownHandler(GameFormControl obj, WorldCoords mouse)
        {
            this.value--;
        }




    }
}
