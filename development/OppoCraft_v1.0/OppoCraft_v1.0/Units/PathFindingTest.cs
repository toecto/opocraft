using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using testClient;
using System.Collections.Generic;

namespace OppoCraft
{
    class PathFinderTest: MapEntity
    {
        WorldPath aPath=null;
        GridCoords lastSpot = new GridCoords(0,0);
        bool enabled = false;
        public override void onStart()
        {
            this.location.Y = this.theGame.worldMapSize.Y;//to render last
        }

        public override void Tick()
        {
            if (this.theGame.userInput.isKeyPressed(Keys.F9)) enabled = !enabled;
            if (!enabled) return;

            WorldCoords x=this.theGame.render.getWorldCoords(this.theGame.userInput.mousePosition);
            GridCoords test = this.theGame.theGrid.getGridCoords(x);
            if (!test.Equals(this.lastSpot))
            {
                this.lastSpot = test;
                this.aPath = this.theGame.pathFinder.GetPath(new WorldCoords(60, 60), this.theGame.theGrid.getWorldCoords(test),0);
                foreach(KeyValuePair<int,MapEntity> item in this.theGame.map.entities)
                {
                    if (item.Value.GetType() != typeof(Unit)) continue;
                    Unit unit = (Unit)item.Value;
                    if (unit.cid != this.theGame.cid) continue;
                    //Unit u = this.theGame.map.getById(this.theGame.myFirstUnit);
                    WorldCoords origCoord = unit.location;
                    WorldCoords destination = this.theGame.theGrid.getWorldCoords(test);

                    
                    //u.task.Add(new TaskGoTo(destination));
                }
            }
            
        }

        public override void Render(RenderSystem render)
        {
            if (!enabled) return;
            int maxValue = 1;
            for (int x = 0; x < this.theGame.theGrid.gridValues.GetLength(0); x++)
            {
                for (int y = 0; y < this.theGame.theGrid.gridValues.GetLength(1); y++)
                {
                    if (maxValue < this.theGame.theGrid.getGridValue(new GridCoords(x, y)))
                    {
                        maxValue = this.theGame.theGrid.getGridValue(new GridCoords(x, y));
                    }
                }
            }

            int color = 1;

            for (int x = 0; x < this.theGame.theGrid.gridValues.GetLength(0); x++)
            {
                for (int y = 0; y < this.theGame.theGrid.gridValues.GetLength(1); y++)
                {
                    Vector2 position = this.theGame.render.getScreenCoords(this.theGame.theGrid.getWorldCoords(new GridCoords(x, y)));
                    color = this.theGame.theGrid.getGridValue(new GridCoords(x, y)) * 255 / maxValue;
                    if (color < 0)
                    {
                        render.Draw(this.theGame.render.primRect50, position, new Rectangle(0, 0, 40, 24), new Color(255, 0, 0));
                        render.DrawText(this.theGame.theGrid.getGridValue(new GridCoords(x, y)).ToString(), position);
                    }
                    else
                    {
                        //render.Draw(this.theGame.render.primRect, position, new Rectangle(0, 0, 40, 24), new Color(0, 0, color));
                        //render.DrawText(this.theGame.theGrid.getGridValue(new GridCoords(x, y)).ToString(), position);
                    }
                }
            }
            
            if (this.aPath != null)
            {
                foreach (WorldCoords coords in this.aPath)
                {
                    Vector2 position = this.theGame.render.getScreenCoords(coords);
                    position.X -= this.theGame.render.primRect50.Width / 2;
                    position.Y -= this.theGame.render.primRect50.Height / 2; 
                    render.Draw(this.theGame.render.primRect50, position, new Rectangle(0, 0, 40, 24), new Color(0, 255, 0));
                }
            }
            else
            {
                //Debug.WriteLine("No path");
            }
        }
    }
}
