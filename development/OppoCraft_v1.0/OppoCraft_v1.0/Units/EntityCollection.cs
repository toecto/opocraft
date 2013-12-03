using System.Collections.Generic;
using System.Diagnostics;


namespace OppoCraft
{
    public class EntityCollection : Dictionary<int, MapEntity>
    {
        public MapEntity getById(int id)
        {
            if(this.ContainsKey(id))
                return this[id];
            return null;
        }

        new public void Remove(int uid)
        {
            MapEntity u = this.getById(uid);
            if (u != null)
            {
                u.onFinish();
                base.Remove(uid);
            }
        }

        public void Remove(MapEntity u)
        {
            this.Remove(u.uid);
        }

        public virtual void Add(MapEntity u)
        {
            this.Add(u.uid,u);
            u.onStart();
        }

    }
}
