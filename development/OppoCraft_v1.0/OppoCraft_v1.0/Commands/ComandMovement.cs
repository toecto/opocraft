using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class ComandMovement: Task
    {
        Vector2 delta;
        Vector2 location;
        Vector2 destination;
        public ComandMovement(WorldCoords dest)
        {
            this.destination = dest.getVector2();
        }
        public override void onStart()
        {
            this.location = this.unit.location.getVector2();
            float distance = Vector2.Distance(this.location, this.destination);
            this.delta = Vector2.Divide(Vector2.Subtract(this.destination, this.location), distance);
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
            //return base.Tick();
        }
        public void MoveHandler()
        {
            this.location = Vector2.Add(this.location, Vector2.Multiply(this.delta, this.unit.speed));
            this.unit.location.setVector2(this.location);
        }
    }
}
