using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    class WinnerForm : GameForm
    {
        bool isYou;

        public WinnerForm(bool isYou)
        {
            this.isYou = isYou;
        }



        public override void onStart()
        {
            base.onStart();
            this.controls.Clear();
            this.location = new WorldCoords(100, 100);
            this.size= new WorldCoords(this.theGame.render.size.X - 200,this.theGame.render.size.Y-200);
            
            GameFormLabel label;
            if(isYou)
                label= new GameFormLabel("You won!");
            else
                label= new GameFormLabel("You lost!");

            label.location = new WorldCoords(this.size.X / 2 - 50, this.size.Y / 2);

            this.controls.Add(label);
        }

    }
}
