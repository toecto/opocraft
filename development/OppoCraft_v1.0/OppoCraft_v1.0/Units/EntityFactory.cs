using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;
using System.Diagnostics;

namespace OppoCraft
{
    class EntityFactory
    {
        public static MapEntity Create(Game1 theGame, OppoMessage message)
        {
            if (!message.Text.ContainsKey("class"))
                message.Text["class"] = "Unit";

            if (!message.ContainsKey("ownercid"))
                message["ownercid"] = 0;

            switch (message.Text["class"])
            {
                case "Unit":
                    return new Unit(theGame, message);
                
                case "EntityForest":
                    return new EntityForest(theGame, message);

                case "EntityEnvironment":
                    return new EntityEnvironment(theGame, message);

                case "UnitShell":
                    {
                        return new UnitShell(theGame, message);
                    }

                    
            }
            return null;
        }
    }
}
