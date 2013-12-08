using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    public class GameMap
    {
        public Game1 theGame;

        public List<Unit> units;
        public Dictionary<int, MapEntity> entities;

        public GameMap(Game1 g)
        {
            this.theGame = g;
            this.units = new List<Unit>(2048);
            this.entities = new Dictionary<int, MapEntity>(2048);
        }

        public void Tick()
        {
            foreach (KeyValuePair<int, MapEntity> item in this.entities)
            {
                item.Value.Tick();
            }
        }

        public void Render(RenderSystem render)
        {
            foreach (KeyValuePair<int, MapEntity> item in this.entities.OrderBy(item => item.Value.location.Y))
            {
                item.Value.Render(render);
            }
        }

        public void Add(MapEntity u)
        {
            if (u.uid == 0) u.uid = this.theGame.CreateUID();
            u.theGame = this.theGame;
            this.entities.Add(u.uid,u);
            if (u.GetType() == typeof(Unit))
                this.units.Add((Unit)u);
            u.onStart();
        }

        public List<MapEntity> EntitiesIn(WorldCoords start, WorldCoords stop)
        {
            List<MapEntity> result = new List<MapEntity>(16);
            foreach (KeyValuePair<int, MapEntity> item in this.entities)
            {
                if (item.Value.location.isIn(start, stop))
                    result.Add(item.Value);
            }
            return result;
        }


        public List<Unit> UnitsIn(WorldCoords start, WorldCoords stop)
        {
            List<Unit> result = new List<Unit>(16);
            foreach (Unit item in this.units)
            {
                if (item.location.isIn(start, stop))
                    result.Add(item);
            }
            return result;
        }

        public MapEntity getById(int id)
        {
            if (this.entities.ContainsKey(id))
                return this.entities[id];
            return null;
        }

        public void Remove(int uid)
        {
            MapEntity u = this.getById(uid);
            if (u != null)
            {
                u.onFinish();
                this.entities.Remove(uid);
                if(u.GetType()==typeof(Unit))
                    this.units.Remove((Unit)u);
            }
        }

        public void Remove(MapEntity u)
        {
            this.Remove(u.uid);
        }


    }


}
