using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;

namespace OppoCraft
{
    public class UnitDataLoader
    {
        Dictionary<string, DataRow> unitData;
        Game1 theGame;

        public UnitDataLoader(Game1 theGame)
        {
            this.theGame = theGame;
            this.unitData = new Dictionary<string, DataRow>();
            this.init();
        }


        public void init()
        {
            DataTable unitDataTable = this.theGame.db.Query("Select * from Units");
            unitDataTable.Columns.Add("Path",typeof(string));
            foreach (DataRow unitData in unitDataTable.Rows)
            {
                if (unitData["AnimationFileID"] != DBNull.Value)
                {
                    DataTable animationFileDataTable = this.theGame.db.Query("Select Path from AnimationFile where AnimationFileID=" + unitData.Field<int>("AnimationFileID"));
                    if (animationFileDataTable.Rows.Count > 0)
                        unitData["Path"] = animationFileDataTable.Rows[0].Field<string>("Path");
                }

                this.unitData.Add(unitData.Field<string>("UnitType"), unitData);
            }
        }



        public bool Load(Unit theUnit, string type)
        {
            if (!this.unitData.ContainsKey(type)) return false;
            DataRow unitData = this.unitData[type];

            if (unitData["Path"] != DBNull.Value)
            {
                theUnit.animation = theUnit.theGame.graphContent.GetUnitAnimation(theUnit, unitData.Field<string>("Path"));
            }
            if (unitData["Group"] != DBNull.Value)
                theUnit.group = unitData.Field<string>("Group");
            
            theUnit.isObstacle = unitData.Field<bool>("Obstacle");

            theUnit.size = theUnit.theGame.theGrid.getWorldCoords(new GridCoords(unitData.Field<int>("Width"), unitData.Field<int>("Height")));

            if (theUnit.isMy || theUnit.isServed)
            {
                if (unitData["Driver"] != DBNull.Value)
                {
                    Task task = DriverTaskFactory.Create(unitData.Field<string>("Driver"));
                    if (task != null)
                        theUnit.task.Add(task);
                }
            }

            return true;
        }
    }
}
