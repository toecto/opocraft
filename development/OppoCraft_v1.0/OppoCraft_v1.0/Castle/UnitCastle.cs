using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class UnitCastle: Unit
    {
        

        public OppoMessage factorySettings;
        public int trainingCooldown = 0;
        public int trainingSpeedReal { get { return 3600 / this.factorySettings["trainingspeed"]; } }
        
        public UnitCastle(Game1 theGame, OppoMessage settings)
            : base(theGame,settings)
        {
            this.factorySettings = new OppoMessage(OppoMessageType.ChangeState);
            this.factorySettings["attack"] = 25;
            this.factorySettings["attackspeed"] = 40;
            this.factorySettings["attackrange"] = 1;
            this.factorySettings["viewrange"] = 10;
            this.factorySettings["speed"] = 20;
            this.factorySettings["armour"] = 0;
            this.factorySettings["trainingspeed"] = 50;
            this.factorySettings["training"] = 1;
            this.factorySettings.Text["zone"] = "";
            this.factorySettings.Text["unittype"] = "Knight";
            this.factorySettings.Text["targets"] = "Knight";
            this.factorySettings.Text["name"] = "Knight Castle";

            foreach (KeyValuePair<string, int> item in settings)
            {
                if (this.factorySettings.ContainsKey(item.Key))
                    this.factorySettings[item.Key] = item.Value;
            }
            foreach (KeyValuePair<string, string> item in settings.Text)
            {
                if (this.factorySettings.Text.ContainsKey(item.Key))
                    this.factorySettings.Text[item.Key] = item.Value;
            }
        }

        public override void onStart()
        {
            base.onStart();
            GridCoords start = this.locationGrid;
            start.X -= 5;
            start.Y -= 5;
            GridCoords size = this.sizeGrid;
            size.X = 10;
            size.Y = 10;
            List<MapEntity> toRemove=this.theGame.map.EntitiesIn(this.theGame.theGrid.getWorldCoordsCenter(start), this.theGame.theGrid.getWorldCoordsCenter(size));
            foreach(MapEntity item in toRemove)
            {
                if (item.uid != this.uid)
                {
                    OppoMessage msg = new OppoMessage(OppoMessageType.RemoveUnit);
                    msg["uid"] = item.uid;
                    this.theGame.AddCommand(msg);
                }
            }
        }


        public override void Render(RenderSystem render)
        {
            base.Render(render);

            Vector2 position = render.getScreenCoords(this.location);
            position.X -= render.primRect50.Bounds.Width / 2;
            if (this.animation.current.First != null)
                position.Y -= this.animation.current.First.Value.currentAnimation.file.size.Y / 2 + 10;

            if (trainingCooldown > 0)
            {
                Rectangle bar = new Rectangle(0, 0, render.primRect50.Bounds.Width, 2);
                render.Draw(render.primDot, position, bar, Microsoft.Xna.Framework.Color.Blue);
                bar = new Rectangle(0, 0, render.primRect50.Bounds.Width * trainingCooldown / this.trainingSpeedReal, 2);
                render.Draw(render.primDot, position, bar, Microsoft.Xna.Framework.Color.LightSkyBlue);
            }
        }



        public virtual void applySettings(OppoMessage msg, OppoMessage settings)
        {
            msg["factory"] = 1;
            msg["attack"] = settings["attack"];
            msg["attackspeed"] = settings["attackspeed"];
            msg["attackrange"] = settings["attackrange"];
            msg["viewrange"] = settings["viewrange"];
            msg["speed"] = settings["speed"];
            msg["armour"] = settings["armour"];
            msg["viewrange"] = settings["viewrange"];
            msg.Text["type"] = settings.Text["unittype"];
            msg.Text["zone"] = settings.Text["zone"];
            msg.Text["targets"] = settings.Text["targets"];
        }


        public virtual void tryToSpawn()
        {
            if (!this.theGame.userPoints.tryWithdraw(this.countCost()))
            {
                this.factorySettings["training"] = 0;
                return; //not enough minerals
            }
            if (this.factorySettings["training"] == 1)
            {
                OppoMessage msg;
                msg = new OppoMessage(OppoMessageType.CreateEntity);
                msg["uid"] = this.theGame.CreateUID();
                msg["ownercid"] = this.cid;
                msg["x"] = this.location.X - 2*40;
                msg["y"] = this.location.Y + 40 * 1;

                this.applySettings(msg, this.factorySettings);
                this.theGame.AddCommand(msg);
            }
        }

        public int countCost()
        {
            int cnt = 0;
            foreach (int i in this.factorySettings.Values)
            {
                cnt += i;
            }

            cnt+=(this.factorySettings.Text["targets"].Split(',')).Length * 5;


            return cnt;
            
        }

    }
}
