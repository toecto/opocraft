using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using testClient;

namespace OppoCraft
{
    class CommandFlyToEntity : Task
    {
        Vector2 delta;//for a tick increment
        Vector2 location;
        Vector2 destination;
        MapEntity target;
        int targetID;

        public CommandFlyToEntity(OppoMessage msg)
        {
            this.targetID = msg["target"];
        }

        public override void onStart()
        {
            this.target = this.unit.theGame.map.getById(this.targetID);

            this.location = this.unit.location.getVector2();
            this.init();
        }

        public void init()
        {
            this.destination = target.location.getVector2();
            float distance = Vector2.Distance(this.location, this.destination);
            this.delta = Vector2.Divide(Vector2.Subtract(this.destination, this.location), distance);
            this.unit.direction = CommandMovement.vectorToDirection(this.delta);
        }

        public override bool Tick()
        {
            if (this.target == null) return false;

            if (!this.destination.Equals(target.location.getVector2()))
            {
                this.init();
            }

            if (Vector2.DistanceSquared(this.location, this.destination) <= this.unit.speedSqr)
            {
                return false; 
            }
         
            this.MoveHandler();
            return true;
        }

        public void MoveHandler()
        {
            this.location = Vector2.Add(this.location, Vector2.Multiply(this.delta, this.unit.speedReal));
            this.unit.location.setVector2(this.location);
        }


    }
}
