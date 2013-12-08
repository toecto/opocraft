using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;

namespace OppoCraft
{
    public class UnitCastle: Unit
    {
        public OppoMessage factorySettings;
        
        public UnitCastle(Game1 theGame, OppoMessage settings)
            : base(theGame,settings)
        {
            this.factorySettings = new OppoMessage(OppoMessageType.ChangeState);
            this.factorySettings.Text["zone"] = "";
            this.factorySettings["attack"] = 1;
            this.factorySettings["attackspeed"] = 1;
            this.factorySettings["attackrange"] = 1;
            this.factorySettings["viewrange"] = 1;
            this.factorySettings["speed"] = 1;
            this.factorySettings["armor"] = 1;  
            this.factorySettings["viewrange"] = 1;
            this.factorySettings["buildspeed"] = 1;
            this.factorySettings.Text["targets"] = "";
        }
    }
}
