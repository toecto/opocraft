using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using testClient;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class Unit: MapEntity
    {
        public enum Direction
        {
            East,
            North_East,
            North,
            North_West,
            West,
            South_West,
            South,
            South_East
        }

       
        public TaskManager task;
        public UnitAnimation animation;
        
        public string type;
        public string status;

        public Direction direction;
        public int currHP = 100;
        public bool alive { get { return this.currHP > 0; } }
        public int maxHP = 100;
        public float speed = 1f;
        public int damage = 5;
        public int armour = 1;
        public int attackSpeed = 30;
        public int attackRange = 50;
        public int viewRange = 50;
        public bool isMy;
        public bool isServed;

        public Unit(OppoMessage settings)
        {
            this.settings = settings;
            this.direction = Direction.East;
            this.cid = this.settings["ownercid"];
            this.uid = this.settings["uid"];
            

            this.type = this.settings.Text["type"];
            this.location = new WorldCoords(this.settings["x"], this.settings["y"]);
            this.task = new TaskManager(this);
        }

        public override void onStart()
        {
            this.isMy = this.settings["ownercid"] == this.theGame.cid;
            this.isServed = this.settings["ownercid"] == 0 && this.theGame.loadMap!=null;

            this.animation = this.theGame.graphContent.GetUnitAnimation(this, this.type);
            if (this.isMy || this.isServed)
                this.addDriver();
        }
        public override void onFinish()
        {
            this.SetGridValue(0);
        }

        private void addDriver()
        {
            switch (this.type)
            {
                case "Knight":
                    this.task.Add(new TaskKnightDriver());
                    break;
                case "Archer":
                    this.task.Add(new TaskKnightDriver());
                    break;
                case "Tree":
                    this.task.Add(new TaskTreeDriver());
                    break;
            }
        }


        public virtual void SetGridValue(int val)
        {
            this.theGame.theGrid.fillRectValues(this.location, this.size, val);
        }

        public override void Tick()
        {
            this.SetGridValue(0);
            this.task.Tick();
            this.animation.Tick();
            this.SetGridValue(-this.uid);
        }

        public override void Render(RenderSystem render)
        {
            this.animation.Render(render);
            if(this.type != "System")
            {
                UnitAnimationAdditions.Render(this,render);
            }
        }

        public virtual void AddCommand(OppoMessage msg)
        {
            msg["uid"] = this.uid;
            this.theGame.AddCommand(msg);
        }

        /*
        public void setStatus(string name, string value)
        {
            this.status.Remove(name);
            this.status.Add(name, value);
        }

        public void removeStatus(string name)
        {
            this.status.Remove(name);
        }
        public string getStatus(string name)
        {
            if (!this.status.ContainsKey(name)) return "";
            return this.status[name];
        }
        */
    }
}
