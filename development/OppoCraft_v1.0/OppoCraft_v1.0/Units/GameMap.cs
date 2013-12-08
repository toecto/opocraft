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

        LinkedList<KeyValuePair<bool, MapEntity>> toChange;


        public GameMap(Game1 g)
        {
            this.theGame = g;
            this.units = new List<Unit>(2048);
            this.entities = new Dictionary<int, MapEntity>(2048);
            this.toChange = new LinkedList<KeyValuePair<bool, MapEntity>>();
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



        public List<MapEntity> EntitiesIn(WorldCoords start, WorldCoords size)
        {
            List<MapEntity> result = new List<MapEntity>(16);
            foreach (KeyValuePair<int, MapEntity> item in this.entities)
            {
                if (item.Value.location.isIn(start, size))
                    result.Add(item.Value);
            }
            return result;
        }


        public List<Unit> UnitsIn(WorldCoords start, WorldCoords size)
        {
            List<Unit> result = new List<Unit>(16);
            foreach (Unit item in this.units)
            {
                if (item.location.isIn(start, size))
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

        public void Remove(MapEntity u)
        {
            this.toChange.AddLast(new KeyValuePair<bool, MapEntity>(false, u));
        }


        public void Remove(int uid)
        {
            if (this.entities.ContainsKey(uid))
                this.toChange.AddLast(new KeyValuePair<bool, MapEntity>(false, this.entities[uid]));
        }

        public void Add(MapEntity u)
        {
            this.toChange.AddLast(new KeyValuePair<bool, MapEntity>(true, u));
        }

        //--------------------------

        public void applyChanges()
        {
            LinkedListNode<KeyValuePair<bool, MapEntity>> cursor = this.toChange.First;
            KeyValuePair<bool, MapEntity> item;
            while (cursor != null)
            {
                item = cursor.Value;
                if (item.Key == true)
                {
                    this._actualAdd((MapEntity)item.Value);
                }
                else
                {
                    if (item.Value == null)
                        this._actualClear();
                    else
                        this._actualRemove((MapEntity)item.Value);
                }
                cursor = cursor.Next;
            }

            this.toChange.Clear();
        
        }

         void _actualRemove(MapEntity u)
        {
            u.onFinish();
            this.entities.Remove(u.uid);
            if (u is Unit)
                this.units.Remove((Unit)u);
        }


         void _actualAdd(MapEntity u)
         {
             if (u.uid == 0) u.uid = this.theGame.CreateUID();
             u.theGame = this.theGame;
             this.entities.Add(u.uid, u);
             if (u is Unit)
                 this.units.Add((Unit)u);
             u.onStart();
         }

         void _actualClear()
         {
             this.entities.Clear();
             this.units.Clear();
         }




    }


}
