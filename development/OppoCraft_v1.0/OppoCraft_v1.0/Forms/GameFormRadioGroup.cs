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
            button.onChange += this.handleChange;
            this.controls.Add(button);
            this.list.Add(button);
            this.size.Y = button.location.Y+25;
        }

        public void handleChange(GameFormControl obj)
        {
            GameFormRadioButton button=(GameFormRadioButton)obj;
            if (button.selected == false) return;
            this.selected = button;

            //Debug.WriteLine("handleSelect"+ this.selected.text);
            foreach (GameFormRadioButton item in this.list)
            {
                if (item != this.selected)
                    item.selected = false;
            }
        }


        internal void setSelectedValue(Object val)
        {
            foreach (GameFormRadioButton item in this.list)
            {
                if (item.value.Equals(val))
                    item.selected = true;
            }
        }
    }
}
