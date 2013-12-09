using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace OppoCraft 
{
    class UnitShell : Unit
    {
        WorldCoords realLocation;

        public UnitShell(Game1 theGame, OppoMessage settings)
            : base(theGame, settings)
        {

            this.type = this.settings.Text["type"];
            if (this.settings.Text.ContainsKey("status"))
                this.status = this.settings.Text["status"];

            this.animation.startAction(this.status);
            this.damage = this.settings["damage"];


            OppoMessage msg = new OppoMessage(OppoMessageType.FlyToEntity);
            msg["target"] = this.settings["target"];
            this.task.Add(new CommandFlyToEntity(msg));
            this.speedReal = 5;
        }

        public override void onStart()
        {
            base.onStart();
            this.realLocation = this.location;
        }

        public override void Tick()
        {
            this.location = this.realLocation;//for rendering above everything;
            base.Tick();
            this.realLocation = this.location;
            this.location = this.theGame.worldMapSize;
        }

        public override void Render(RenderSystem render)
        {
            this.location = this.realLocation;//for rendering above everything;
            base.Render(render);
            
        }

    }
}
