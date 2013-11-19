using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    public class UnitCollection : Dictionary<int, Unit>
    {
        public Unit getById(int id)
        {
            if (this.ContainsKey(id))
                return this[id];
            return null;
        }

        new public void Remove(int id)
        {
            Unit u = this.getById(id);
            if (u != null)
                base.Remove(id);
        }

        public void Remove(Unit u)
        {
            base.Remove(u.id); 
        }

        public virtual void Add(Unit u)
        {
            this.Add(u.id, u);
        }

    }
    
}
