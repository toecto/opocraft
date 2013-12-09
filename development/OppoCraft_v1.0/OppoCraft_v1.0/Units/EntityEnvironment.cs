using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;

namespace OppoCraft
{
    public class EntityEnvironment: MapEntity
    {
        public EntityEnvironment(Game1 theGame, OppoMessage message)
            : base(theGame, message)
        {
        }

        public override void onStart()
        {
            this.location = new WorldCoords(0,0);
            this.size = this.theGame.worldMapSize;
            string[] objects = { "House", 
"Barack", 
"Cart", 
"Shelter", 
"Rock1", 
"Rock2", 
"Rock3", 
"Rock4", 
"Rock5", 
"Ruins1", 
"Ruins2", 
"Ruins3", 
"Ruins4", 
"Hay", 
"Stump", 
"Pointer",  };



            int objectsToCreate = (int)(((this.size.X * this.size.Y) / (this.theGame.cellSize.X * this.theGame.cellSize.Y)) * 0.005);
            GridCoords limits = this.theGame.theGrid.getGridCoords(this.size);
            WorldCoords treePosition;

            limits.X -= 6;
            limits.Y -= 6;
            for (int i = 0; i < objectsToCreate; i++)
            {
                OppoMessage msg = new OppoMessage(OppoMessageType.CreateEntity);
                msg["uid"] = this.theGame.CreateUID();
                msg["ownercid"] = 0;
                treePosition = this.theGame.theGrid.getWorldCoordsCenter(new GridCoords( + Game1.rnd.Next(4, limits.Y),  + Game1.rnd.Next(4, limits.Y)));
                msg["x"] = treePosition.X + this.location.X;
                msg["y"] = treePosition.Y + this.location.Y;
                msg.Text["type"] = objects[Game1.rnd.Next(0,objects.Count())];
                msg.Text["class"] = "UnitObstacle";
                this.theGame.AddCommand(msg);
            }
        }
    }
}
