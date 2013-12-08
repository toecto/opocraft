using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    public class GameFormControlCollection: List<GameFormControl>
    {
        GameFormControl parent;


        public GameFormControlCollection(GameFormControl parent)
        {
            this.parent = parent;
        }

        new public void Add(GameFormControl item)
        {
            
            item.setParent(this.parent);
            base.Add(item);
        }

        new public void Remove(GameFormControl item)
        {
            base.Remove(item);
        }

    }
}
