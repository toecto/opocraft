using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using testClient;
using System.Diagnostics;

namespace OppoCraft
{
    public class TaskGoTo : Task    
    {
        WorldCoords step;
        WorldPath worldPath;
        WorldCoords dest;
        MessageState messageState = MessageState.Ready;
        int range;
        enum MessageState { 
            Ready,
            Sent,
            InProcess,
        }

        public TaskGoTo(WorldCoords d, int range)
        {
            this.dest = d;
            this.range = range;
        }
        public TaskGoTo(WorldPath worldPath, WorldCoords d, int range)
        {
            this.dest = d;
            this.range = range;
            this.worldPath = worldPath;
            this.messageState = MessageState.Ready;
        }
        
        public void GetPath()
        {
            this.worldPath = this.unit.theGame.pathFinder.GetPath(this.unit.location, this.dest, this.range);
        }

        public override bool Tick()
        {

            if (this.messageState == MessageState.Sent && this.unit.task.isRunning(typeof(CommandMovement)))
            {
                this.messageState = MessageState.InProcess;
                return true;
            }

            if (this.messageState == MessageState.InProcess && !this.unit.task.isRunning(typeof(CommandMovement)))
            {
                this.messageState = MessageState.Ready;
                return true;
            }

            if (this.messageState == MessageState.InProcess)
            {
                if (this.unit.theGame.theGrid.getGridValue(this.step) < 0)
                {
                    GetPath();
                    this.messageState = MessageState.Ready;
                    return true;
                }
            }

            if (this.messageState == MessageState.Ready)
            {
                if (this.worldPath == null) return false;
                if (this.worldPath.Count == 0) return false;

                
                this.step = this.worldPath.First.Value;
                this.worldPath.RemoveFirst();
                

                OppoMessage msg = new OppoMessage(OppoMessageType.Movement);
                msg["x"] = this.step.X;
                msg["y"] = this.step.Y;
                this.unit.AddCommand(msg);
                
                this.messageState = MessageState.Sent;
            }
            return true;
        }

        public override void onStart()
        {
            this.GetPath();
        }

        public override void onFinish()
        {
            OppoMessage msg = new OppoMessage(OppoMessageType.ChangeState);
            msg.Text["stopact"] = "Walk";
            this.unit.AddCommand(msg);
        }
    }
}
