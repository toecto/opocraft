using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    class Task
    {
        public Unit unit;

        public virtual bool Tick() 
        {
            return false;
        }
        public virtual void onStart() 
        {
        }

        public virtual void onFinish() 
        {
        }
    }
}
