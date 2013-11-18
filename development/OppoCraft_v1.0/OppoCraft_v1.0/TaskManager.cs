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
            t.unit = this.unit;
            this.tasks.AddLast(t);
            t.onStart();
        }

        public void AddUnique(Task task)
        {
            this.RemoveByType(task.GetType());
            this.Add(task);
        }

        public void RemoveByType(System.Type TypeToRemove)
        {
           
        }

        public void Remove(Task t)
        {
           
        }
    }
}
