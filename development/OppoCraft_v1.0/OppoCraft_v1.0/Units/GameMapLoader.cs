using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using testClient;

namespace OppoCraft
{
    class GameMapLoader
    {
        static bool Load(GameMap theMap, string name)
        {
            DataTable mapDataTable = theMap.theGame.db.Query("Select * from Map where MapName like '" + name + "'");
            if (mapDataTable.Rows.Count == 0) return false;
            DataRow mapData = mapDataTable.Rows[0];

            DataTable unitsData = theMap.theGame.db.Query("select m.*, u.UnitType from MapUnits as m, Units as u where m.UnitId=u.UnitID and MapID=" + mapData["MapID"]);
            foreach (DataRow unitData in unitsData.Rows)
            {
                OppoMessage msg = new OppoMessage(OppoMessageType.CreateEntity);
                msg["uid"] = theMap.theGame.CreateUID();
                switch (unitData.Field<int>("Owner"))
                { 
                    case 0:
                        msg["ownercid"] = 0;
                    break;
                    case 1:
                        msg["ownercid"] = theMap.theGame.cid;
                    break;
                    case 2:
                        msg["ownercid"] = theMap.theGame.enemyCid;
                    break;
                }
                msg["x"] = unitData.Field<int>("PositionX");
                msg["y"] = unitData.Field<int>("PositionН");
                msg.Text["type"] = unitData.Field<string>("UnitType");
                theMap.theGame.AddCommand(msg);
            }
            
            return true;
        }
    }
}
