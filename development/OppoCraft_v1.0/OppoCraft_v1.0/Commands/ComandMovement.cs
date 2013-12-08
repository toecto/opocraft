using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using testClient;

namespace OppoCraft
{
    public class CommandMovement: Task //do not assign directly
    {
        Vector2 delta;//for a tick increment
        Vector2 location;
        public Vector2 destination;

        public CommandMovement(OppoMessage msg)
        {
            this.destination = (new WorldCoords(msg["x"], msg["y"])).getVector2();
        }

        public override void onStart()
        {
            this.location = this.unit.location.getVector2();
            
            float distance = Vector2.Distance(this.location, this.destination);
            if (distance > 200)
            {
                Debug.WriteLine("WTF!!");
            }
            this.delta = Vector2.Divide(Vector2.Subtract(this.destination, this.location),distance);
            this.unit.direction = vectorToDirection(this.delta);
            this.unit.animation.startAction("Walk");
        }

        public override bool Tick()
        {
            if (Vector2.Distance(this.location, this.destination) <= this.unit.speed)
            {
                this.unit.location.setVector2(this.destination);
                return false; 
            }
            else
            {
                this.MoveHandler();
                return true;
            }            
        }

        public void MoveHandler()
        {
            this.location = Vector2.Add(this.location, Vector2.Multiply(this.delta, this.unit.speed));
            this.unit.location.setVector2(this.location);
            
        }

        public static Unit.Direction vectorToDirection(Vector2 delta)
        {
            delta.Normalize();
            if (delta.X > 0)
            {

                if (delta.Y > 0.99)
                {
                    return Unit.Direction.South;
                }
                
                if (delta.Y > 0.4871)
                {
                    return Unit.Direction.South_East;
                }
                
                if (delta.Y > -0.4871)
                {
                    return Unit.Direction.East;
                }

                if (delta.Y > -0.99)
                {
                    return Unit.Direction.North_East;
                }

                return Unit.Direction.North;
            }
            else
            {
                if (delta.Y > 0.99)
                {
                    return Unit.Direction.South;
                }

                if (delta.Y > 0.4871)
                {
                    return Unit.Direction.South_West;
                }

                if (delta.Y > -0.4871)
                {
                    return Unit.Direction.West ;
                }

                if (delta.Y > -0.99)
                {
                    return Unit.Direction.North_West;
                }

                return Unit.Direction.North;
            }
        }

    }
}
