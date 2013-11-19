using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;
using System.Diagnostics;

namespace OppoCraft
{
    public class MessageHandler
    {
        Game1 theGame;
        NetworkModule network;

        public MessageHandler(Game1 g, NetworkModule network)
        {
            this.theGame = g;
            this.network = network;
        }

        public void Tick()
        {
            
            OppoMessage msg;
            while((msg=this.network.getMessage())!=null)   
            {
                this.handle(msg);
            }
        }

        void handle(OppoMessage msg)
        {
            switch(msg.Type)
            {
                case OppoMessageType.StartGame:
                    {
                        this.theGame.running=true;
                        break;
                    }
                case OppoMessageType.GetClientID:
                    {
                        this.theGame.cid = msg["cid"];
                        break;
                    }
                case OppoMessageType.CreateUnit:
                    {
                        Unit unit = new Unit(msg["cid"], msg["uid"]);
                        unit.location = new WorldCoords(100, 100);
                        this.theGame.map.Add(unit);
                        break;
                    }
                case OppoMessageType.Movement:
                    {
                        Unit u=this.theGame.map.getById(msg["uid"]);
                        if (u == null)
                        {
                            Debug.WriteLine("Message for unexisting unit");
                            break;
                        }
                        u.task.AddUnique(new CommandMovement(new WorldCoords(msg["x"],msg["y"])));
                        break;
                    }
            }
        }

    }
}
