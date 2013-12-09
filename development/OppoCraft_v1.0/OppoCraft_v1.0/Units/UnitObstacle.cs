using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testClient;

namespace OppoCraft
{
    class UnitObstacle : Unit
    {
        public UnitObstacle(Game1 theGame, OppoMessage message)
            : base(theGame, message)
        {
        }

        public override void onStart()
        {
            base.onStart();
            this.animation.Clear();
            this.animation.startAction(this.type);
        }
    }
}
