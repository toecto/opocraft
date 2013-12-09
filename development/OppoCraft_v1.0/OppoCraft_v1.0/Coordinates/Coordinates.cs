using System;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class Coordinates
    {
        public int X;
        public int Y;

        public Coordinates(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public double Distance(Coordinates c)
        {
            return this.Distance(c.X, c.Y);
        }

        public double Distance(int x, int y)
        {
            return Math.Sqrt(this.DistanceSqr(x,y));
        }
        public double DistanceSqr(int x, int y)
        {
            return (x - this.X) * (x - this.X) + (y - this.Y) * (y - this.Y);
        }

        public Vector2 getVector2()
        {
            return new Vector2(this.X, this.Y);
        }

        public void setVector2(Vector2 v)
        {
            this.X = (int)(v.X);
            this.Y = (int)(v.Y);
        }

        public Rectangle getRectangle(Coordinates second)
        {
            return new Rectangle(this.X, this.Y, second.X, second.Y);
        }

        public bool Equals(Coordinates test)
        {
            return test!=null && (this.X == test.X) && (this.Y == test.Y);
        }

        public bool isIn(Coordinates start, Coordinates size)
        {
            return this.X > start.X && this.X < start.X + size.X
                && this.Y > start.Y && this.Y < start.Y + size.Y;
        }
        public bool isInCentered(Coordinates start, Coordinates size)
        {

            return isIn(new Coordinates(start.X - size.X / 2,start.Y - size.Y/2), size);
        }

        
    }
}
