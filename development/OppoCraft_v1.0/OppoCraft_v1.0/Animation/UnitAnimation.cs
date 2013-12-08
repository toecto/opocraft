using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class UnitAnimation
    {
        private Dictionary<string, ActionAnimation> actions;
        public Unit unit;

        public LinkedList<ActionAnimation> current;

        public UnitAnimation(Unit unit)
        {
            this.unit = unit;
            this.actions = new Dictionary<string, ActionAnimation>();
            this.current = new LinkedList<ActionAnimation>();
        }

        public void Add(string name, ActionAnimation action)
        {
            this.actions.Add(name, action);
            action.unit = this.unit;
            if (this.current.First == null)
                this.startAction(name);
        }

        public void Tick()
        {
            //LinkedList<ActionAnimation> toDelete=new LinkedList<ActionAnimation>();
            LinkedListNode<ActionAnimation> act=this.current.First;
            while(act!=null)
            {
                if (!act.Value.Tick() && this.current.Count > 1)
                {
                    //Debug.WriteLine("Removed act "+act.Value.name);
                    this.current.Remove(act.Value);
                }
                act = act.Next;
            }
        }

        public void Render(RenderSystem render)
        {
            if (this.current.First == null) return;
            
            Vector2 position = render.getScreenCoords(this.unit.location);
            this.current.First.Value.Render(render, position);
        }

        public void startAction(string name)
        {
            //Debug.WriteLine("startAction " + name+ " "+this.unit.uid);
            if (!this.actions.ContainsKey(name)) return;
            ActionAnimation act = this.actions[name];
            LinkedListNode<ActionAnimation> cursor;


            cursor = this.current.First;


            if (!act.currentAnimation.looped)
               act.currentAnimation.Reset();

            if (cursor != null && cursor.Value.name==act.name)
            {
                return;
            }

            if (act.currentAnimation.looped)
                act.currentAnimation.Random();


            //Delete the same actions
            while (cursor != null)
            {
                if (cursor.Value.name == act.name)
                    this.current.Remove(cursor);
                cursor = cursor.Next;
            }

            //Find a proper spot by priority
            cursor = this.current.First;
            while (cursor != null)
            {
                if (cursor.Value.priority <= act.priority)
                {
                    this.current.AddBefore(cursor, act);
                    break;
                }
                cursor = cursor.Next;
            }

            //if could not find just add it
            if (cursor == null)
            {
                this.current.AddLast(act);
            }
        }

        public void stopAction(string name)
        {
            //Debug.WriteLine("stopAction " + name + " " + this.unit.uid);
            if (this.actions.ContainsKey(name))
            {
                ActionAnimation act = this.actions[name];
                this.current.Remove(act);
            }
        }


        internal void Clear()
        {
            this.current.Clear();
        }
    }
}
