﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using testClient;
using System.Diagnostics;

namespace OppoCraft
{
    public class TaskGoto: Task
    {
        int currStep;
        int totalSteps;
        Vector2 destination;
        WorldPath worldPath;
        WorldCoords dest;

        public TaskGoto(WorldCoords d)
        {
            this.currStep = 1;
            this.totalSteps = 0;
            this.dest = d;
        }

        public void GetPath()
        {
            this.worldPath = this.unit.theGame.theGrid.thePathFinder.GetPath(this.unit.location, this.dest);
            if (this.worldPath == null)
                return;
            this.totalSteps = this.worldPath.Count();
            this.destination = this.worldPath.First.Value.getVector2();
        }

        public override bool Tick()
        {

            if (this.currStep < this.totalSteps)
            {
                if (Vector2.Distance(this.unit.location.getVector2(), this.destination) < this.unit.speed || this.currStep == 1)
                {
                    this.destination = this.worldPath.ElementAt(this.currStep).getVector2();
                    OppoMessage msg = new OppoMessage(OppoMessageType.Movement);
                    msg["x"] = (int)this.destination.X;
                    msg["y"] = (int)this.destination.Y;
                    this.unit.AddCommand(msg);
                    this.currStep++;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void onStart()
        {
            base.onStart();
            this.GetPath();
        }
    }
}
