using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using testClient;
using System.Diagnostics;

namespace oppocraft
{
	public class TaskRecover:Task
	{
		int currStep;
		int totalSteps;
		Vector2 destination;
		WorldCoords dest;
		HealPath healpath;

		public TaskRecover ()
		{
			this.currStep = 1;
			this.totalSteps = 0;
			this.dest = d;
		}

		public void GetPath(){
			this.HealPath = this.unit.theGame.theGrid.thePathFinder.GetPath(this.unit.location, this.dest);
			this.totalSteps = this.worldPath.Count();
			this.destination = this.worldPath.First.Value.getVector2();

		}

        public void getShortestHealingPath()
        {
            if (minimum(GetPath()))
            {
                this.destination = this.worldPath.ElementAt(this.currStep).getVector2();
                OppoMessage msg = new OppoMessage(OppoMessageType.Movement);
                msg["x"] = (int)this.destination.X;
                msg["y"] = (int)this.destination.Y;
                this.unit.AddCommand(msg);
                this.currStep++;

            }
        }

			public override void onStart()
			{
				base.onStart();
				this.GetPath();
			}

	}
}

