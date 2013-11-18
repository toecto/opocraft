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
    public class Unit
    {
        public enum State
        {
            Halt,
            TakeDamage,
            Dying,
            Walking,
            Running,
            Attacking
        }
        public enum Direction
        {
            North,
            South,
            East,
            West,
            North_East,
            North_West,
            South_East,
            South_West
        }

        public Game1 theGame;
        public TaskManager task;


        public Coordinates size = new Coordinates(1, 1);
        public WorldCoords location = new WorldCoords(1, 1);
        public int id;
        public int playerId = 0;
        public int type;

        public State state;
        public Direction direction;
        public WorldPath worldPath;

        public int currHP;
        public int maxHP;
        public float speed = 2;
        public int damage;
        public int armour;
        public int attackSpeed;

        public Unit(int playerId, int id)
        {
            this.playerId = playerId;
            this.id = id;
        }
        //can specify for each unit
        public virtual void SetGridValue()
        {
            GridCoords gridlocation = this.theGame.theGrid.getGridCoords(this.location);
            this.theGame.theGrid.fillRectValues(gridlocation, size, -1);
        }

        public virtual void Tick()
        {
            this.task.Tick();
        }

        public virtual void Render(RenderSystem render)
        {

           
        }
        //sent the message to the server. 

        public virtual void AddCommand(OppoMessage msg)
        {
            msg["uid"] = this.id;
            this.theGame.AddCommand(msg);
        }

    }
}
