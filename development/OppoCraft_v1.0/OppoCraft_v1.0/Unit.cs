using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        Game1 theGame;
        Coordinates size;
        WorldCoords location;
        WorldCoords destination;
        int id;
        int type;
        
        public State state;
        public Direction direction;
        public WorldPath worldPath;
        double dX, dY;
        double tempX, tempY;
        

        int currHP;
        int maxHP;
        double speed;
        int damage;
        int armour;
        int attackSpeed;

        public Unit(Game1 g, int id)
        {
            this.theGame = g;
            this.id = id;
            this.size = new Coordinates(1, 1);            

            this.speed = 5;
        }

        public void SetPath(WorldCoords orig, WorldCoords dest)
        {
           this.worldPath = this.theGame.theGrid.thePathFinder.GetPath(orig, dest);
        }

        public WorldCoords GetNextStep()
        {
            WorldCoords nextStep = this.worldPath.ElementAt(1);  //The second World Coord in the collection

            return nextStep;
        }

        //Initial Move method, which takes the next WorldCoord in the Path as the destination coordinate
        //it then calculates delta of X and Y, divided by the distance between the location, and the nextStep
        public void Move(WorldCoords destination)
        {
            double distance = this.location.Distance(destination);
            this.dX = (destination.X - this.location.X) / distance;
            this.dY = (this.location.Y - destination.Y) / distance;
        }

        //Handler of the movement of the unit, as it receives the delta of X and Y, multiplied by speed, and applied to the location
        //should this method return the values to increment X/Y, and allow the task from the server to pass it back to the unit?
        public void MoveHandler()
        {
            this.tempX += (this.dX * this.speed);
            this.tempY += (this.dY * this.speed);

            this.location.X = (int)this.tempX;
            this.location.Y = (int)this.tempY;
        }

        public void SetGridValue()
        {
            GridCoords gridlocation = this.theGame.theGrid.getGridCoords(this.location);
            this.theGame.theGrid.fillRectValues(gridlocation, size, -1);
        }

    }
}
