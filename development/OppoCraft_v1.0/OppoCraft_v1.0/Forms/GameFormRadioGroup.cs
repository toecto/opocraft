using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OppoCraft
{
    class GameFormRadioGroup : GameFormControl
    {
        List<GameFormRadioButton> list;

        public GameFormRadioButton selected;


        public GameFormRadioGroup()
        {
            this.list = new List<GameFormRadioButton>(4);
            this.size.X = 100;
        }

        public void Add(GameFormRadioButton button)
        {
            button.location.Y = this.list.Count * 25;
            button.onClick += this.handleSelect;
            this.controls.Add(button);
            this.list.Add(button);
            this.size.Y = button.location.Y+25;
        }

        public void handleSelect(GameFormControl obj, WorldCoords mouse)
        {

            this.selected = (GameFormRadioButton)obj;

            Debug.WriteLine("handleSelect"+ this.selected.text);
            foreach (GameFormRadioButton item in this.list)
            {
                if (item != this.selected)
                    item.selected = false;
                else
                    item.selected = true;
            }
        }

    }
}
