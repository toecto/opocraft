using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;

namespace OppoCraft
{
    class EntityForest : MapEntity
    {
        public EntityForest(Game1 theGame, OppoMessage message):base(theGame,message)
        {
        }

        public override void onStart()
        {
            this.size = new WorldCoords(1400, 1400);
            int trees = (int)(((this.size.X * this.size.Y) / (this.theGame.cellSize.X * this.theGame.cellSize.Y)) * 0.1);
            GridCoords limits = this.theGame.theGrid.getGridCoords(this.size);
            WorldCoords treePosition;

            limits.X -= 2;
            limits.Y -= 2;
            for (int i = 0; i < trees; i++)
            {
                OppoMessage msg = new OppoMessage(OppoMessageType.CreateEntity);
                msg["uid"] = this.theGame.CreateUID();
                msg["ownercid"] = 0;
                treePosition = this.theGame.theGrid.getWorldCoordsRnd(new GridCoords( + Game1.rnd.Next(1, limits.Y),  + Game1.rnd.Next(1, limits.Y)));
                msg["x"] = treePosition.X + this.location.X;
                msg["y"] = treePosition.Y + this.location.Y;
                msg.Text["type"] = "Tree";
                this.theGame.AddCommand(msg);
            }
        }
    }
}
