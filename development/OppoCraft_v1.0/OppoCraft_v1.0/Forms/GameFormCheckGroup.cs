﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OppoCraft
{
    class GameFormCheckGroup : GameFormControl
    {
        List<GameFormCheckButton> list;

        public List<GameFormCheckButton> selected;


        public GameFormCheckGroup()
        {
            this.list = new List<GameFormCheckButton>(4);
            this.selected = new List<GameFormCheckButton>(4);
            this.size.X = 100;
        }

        public void Add(GameFormCheckButton button)
        {
            button.location.Y = this.list.Count * 25;
            button.onChange += this.handleChange;
            this.controls.Add(button);
            this.list.Add(button);
            this.size.Y = button.location.Y+25;
        }

        public void handleChange(GameFormControl obj)
        {
            this.selected.Clear();
            Debug.WriteLine("handleSelect");
            foreach (GameFormCheckButton item in this.list)
            {
                if (item.selected)
                    this.selected.Add(item);
            }
        }

        public void setSelectedValue(Object val)
        {
            
            foreach (GameFormCheckButton item in this.list)
            {
                if (item.value.Equals(val))
                    item.selected = true;
            }
        }
        public void setSelectedValues(Object[] vals)
        {
            foreach (Object item in vals)
            {
                this.setSelectedValue(item);
            }
        }

        public T[] getSelectedValues<T>()
        {
            T[] rez = new T[this.selected.Count];

            for (int i = 0; i<this.selected.Count; i++)
            {
                rez[i] = (T)this.selected[i].value;
            }
            return rez;
        }

    }
}
