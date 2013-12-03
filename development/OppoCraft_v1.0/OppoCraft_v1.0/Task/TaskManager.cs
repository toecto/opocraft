using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    public class TaskManager 
    {
        Unit unit;
        TaskCollection tasks;
        Dictionary<string, Object> sharedMsg;
        LinkedList<KeyValuePair<bool, Object>> tasksChanges;

        public TaskManager(Unit u)
        {
            this.unit = u;
            this.sharedMsg = new Dictionary<string, object>();
            this.tasks = new TaskCollection();
            this.tasksChanges = new LinkedList<KeyValuePair<bool, Object>>();
        }

        public void Tick()
        {
            foreach (KeyValuePair<Type, Task> item in this.tasks)
            {
                if (!item.Value.Tick())
                    this.Remove(item.Key);
            }
            this.applyChanges();

        }

        public TaskCollection getTasks()
        {
            return this.tasks;
        }

        public void Add(Task t)
        {
            this.tasksChanges.AddLast(new KeyValuePair<bool, Object>(true, t));
        }

        public void Remove(Type t)
        {
            this.tasksChanges.AddLast(new KeyValuePair<bool, Object>(false, t));
        }

        internal void Clear()
        {
            this.tasksChanges.AddLast(new KeyValuePair<bool, Object>(false, null));
        }



        public bool isRunning(Type t)
        {
            return this.tasks.ContainsKey(t);
        }

        public bool checkShared(string name)
        {
            return this.sharedMsg.ContainsKey(name);
        }

        public T removeShared<T>(string name)
        {
            if (!this.checkShared(name)) return default(T);
            Object rez = this.sharedMsg[name];
            this.sharedMsg.Remove(name);
            return (T)rez;
        }

        public T getShared<T>(string name)
        {
            if (!this.checkShared(name)) return default(T);
            return (T)this.sharedMsg[name];
        }

        public void setShared(string name, Object data)
        {
            this.sharedMsg.Remove(name);
            this.sharedMsg.Add(name, data);
        }


        //private---------------------------------------------
        public void applyChanges()
        {
            LinkedListNode<KeyValuePair<bool, Object>> cursor = this.tasksChanges.First;
            KeyValuePair<bool, Object> item;
            while(cursor!=null)
            {
                item = cursor.Value;
                if (item.Key == true)
                {
                    this._actualAdd((Task)item.Value);
                }
                else
                {
                    if (item.Value == null)
                        this._actualClear();
                    else
                        this._actualRemove((Type)item.Value);
                }
                cursor=cursor.Next;
            }

            this.tasksChanges.Clear();
        }

        private void _actualClear()
        {
            LinkedList<Type> toRemove = new LinkedList<Type>();
            foreach (KeyValuePair<Type, Task> item in this.tasks)
            {
                toRemove.AddLast(item.Key);
            }
            foreach (Type item in toRemove)
            {
                this._actualRemove(item);
            }
            this.tasks.Clear();
        }

        private void _actualAdd(Task task)
        {
            task.unit = this.unit;
            this.tasks.Remove(task.GetType());
            this.tasks.Add(task.GetType(), task);
            task.onStart();
        }

        private void _actualRemove(System.Type t)
        {
            if (this.tasks.ContainsKey(t))
                this.tasks[t].onFinish();
            this.tasks.Remove(t);
        }


    }
}
