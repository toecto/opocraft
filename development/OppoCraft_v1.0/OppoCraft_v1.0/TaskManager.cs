using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    class TaskManager
    {

        Unit unit;
        TaskCollection tasks;

        public TaskManager(Unit u)
        {
            this.unit = u;
            this.tasks = new TaskCollection();
        }

        public void Tick()
        {
            TaskCollection toRemove = new TaskCollection();
            foreach (Task t in this.tasks)
            {
                if (!t.Tick())
                    toRemove.AddLast(t);
            }
            foreach (Task t in toRemove)
            {
                this.Remove(t);
            }
            toRemove.Clear();
        }

        public void Add(Task t)
        {
            
        }

        public void AddUnique(Task task)
        {
           
        }

        public void RemoveByType(System.Type TypeToRemove)
        {
           
        }

        public void Remove(Task t)
        {
           
        }
    }
}
