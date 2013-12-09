using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using testClient;

namespace OppoCraft
{
    class EntityScoreBar : MapEntity
    {

        Vector2 position;

        Dictionary<string, string> data;
        int cooldown=0;
        int cooldownTotal=30;


        public override void onStart()
        {
            position=new Vector2(20,5);
            data = new Dictionary<string, string>();
            Tick();
        }

        public override void Tick()
        {
            cooldown--;
            if (cooldown > 0) return;
            cooldown = cooldownTotal;

            this.data["UnitsOnMap"]=this.theGame.map.units.Count.ToString();
            this.data["EntitiesOnMap"] = this.theGame.map.entities.Count.ToString();
            
            int cntMy=0,cntEn=0,cntNt=0;

            int castleEn = 0;
            int castleMy = 0;

            Type type;
            foreach(Unit unit in this.theGame.map.units)
            {
                type=unit.GetType();
                
                                    
                if (unit.isMy)
                {
                    cntMy++;
                    if (type == typeof(UnitCastle))
                        castleMy++;
                }
                else
                {
                    if (unit.cid == 0)
                        cntNt++;
                    else
                    {
                        cntEn++;
                        if (type == typeof(UnitCastle))
                            castleEn++;
                    }
                }
            }
            this.data["UnitsMy"] = cntMy.ToString();
            this.data["UnitsEn"] = cntEn.ToString();
            this.data["UnitsNt"] = cntNt.ToString();
            this.data["castleEn"] = castleEn.ToString();
            this.data["castleMy"] = castleMy.ToString();

            if (this.data["UnitsOnMap"] == "0") return;

            if (castleEn == 0)
            {
                OppoMessage msg = new OppoMessage(OppoMessageType.Winner);
                msg["winner"] = this.theGame.cid;
                this.theGame.AddCommand(msg);

            }
            if (castleMy == 0)
            {
                OppoMessage msg = new OppoMessage(OppoMessageType.Winner);
                msg["winner"] = this.theGame.enemyCid;
                this.theGame.AddCommand(msg);
            }
        }


        public override void Render(RenderSystem render)
        {
            int sizeX = this.theGame.render.size.X;
            //render.Draw(render.primRect50, new Rectangle(0, 0, sizeX, sizeY), new Rectangle(0, 0, 40, 24), Color.Gray);
            render.DrawText("Points: " + this.theGame.userPoints.points
                + (this.theGame.userPoints.warning?" Warning!":"")
                , this.position);

            render.DrawText("Castles: My/" + this.data["castleEn"]
                + " Enemy/" + this.data["castleEn"]
                , new Vector2(this.position.X + 150, this.position.Y));


            render.DrawText("Units:"
                + " My/" + this.data["UnitsMy"]
                + " Enemy/" + this.data["UnitsEn"]
                + " Neutral/" + this.data["UnitsNt"]
                + " Total/" + this.data["UnitsOnMap"]
                , new Vector2(this.position.X + 350, this.position.Y));

        }
    }
}
