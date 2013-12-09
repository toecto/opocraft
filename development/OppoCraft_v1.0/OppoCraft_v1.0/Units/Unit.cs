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
        public string group;
        public string status;

        public Direction direction;
        public int currHP = 100;
        public bool alive { get { return this.currHP > 0; } }
        public int maxHP = 100;
        public float speedReal = 2f;//  real
        public int speed { get { return (int)(this.speedReal * 10); } set { this.speedReal = value / 10f; } }
        public int damage = 5; // real
        public int armour = 0; // procent of damage
        public int attackSpeedReal=60;
        public int attackSpeed { get { return 3600 / this.attackSpeedReal; } set { this.attackSpeedReal = 3600 / value; } } // per minute 3600/attackSpeed
        public int attackRange = 1; // cells
        public int viewRange = 15;  //cells
        public int viewRangeSqr { get {return this.viewRange*this.viewRange;}}
        public int attackRangeSqr { get { return this.attackRange * this.attackRange; } }
        public double speedSqr { get { return this.speedReal * this.speedReal; } }

        public bool isObstacle;

        public Unit() { } // Default constructor

        public Unit(Game1 theGame, OppoMessage settings)
            : base(theGame,settings)
        {
            this.direction = Direction.East;

            this.type = this.settings.Text["type"];
            if(this.settings.Text.ContainsKey("status"))
                this.status = this.settings.Text["status"];
            this.task = new TaskManager(this);

            if (this.type == "Archer")
            {
                this.attackRange = 10;
                this.attackSpeedReal = 100;
                this.damage = 10;
            }
            this.theGame.unitDataLoader.Load(this, this.type);
            this.applyFactorySettings(this.settings);
            if (!this.settings.Text.ContainsKey("targets"))
                this.settings.Text["targets"] = "Knight,Archer,Lumberjack,Tower,Castle";
        }

        

        public override void onFinish()
        {
            this.CleanGridValue();
        }

        public override void onStart()
        {
             this.SetGridValue();
        }


        public virtual void SetGridValue(int val)
        {

                GridCoords start = this.locationGrid;
                GridCoords size = this.sizeGrid;
                start.X -= size.X / 2;
                start.Y -= size.Y - 1;
                this.theGame.theGrid.fillRectValues(start, size, val);

        }

        public virtual void SetGridValue()
        {
            if (this.isObstacle)
            {
                this.SetGridValue(-this.uid);
            }
        }

        public virtual void CleanGridValue()
        {
            if (this.isObstacle)
            {
                this.SetGridValue(0);
            }
        }

        public override void Tick()
        {

            this.CleanGridValue();
            
            this.task.Tick();
            
            if (this.animation != null)
                this.animation.Tick();

            this.SetGridValue();
        }

        public override void Render(RenderSystem render)
        {
            if (animation != null)
            {
                this.animation.Render(render);
                UnitAnimationAdditions.Render(this, render);
            }
        }

        public virtual void AddCommand(OppoMessage msg)
        {
            msg["uid"] = this.uid;
            this.theGame.AddCommand(msg);
        }

        public void applyFactorySettings(OppoMessage settings)
        {
            if (settings.ContainsKey("factory"))
            {
                this.damage = settings["attack"];
                this.attackSpeed = settings["attackspeed"];
                this.attackRange = settings["attackrange"];
                this.viewRange = settings["viewrange"];
                this.speed = settings["speed"];
                this.armour = settings["armour"];
                this.viewRange = settings["viewrange"];
            }
        }
        
    }
}
