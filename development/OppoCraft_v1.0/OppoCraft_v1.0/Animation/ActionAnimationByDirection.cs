using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace OppoCraft
{
    public class ActionAnimationByDirection : ActionAnimation
    {
        int[] directionMap = new int[8];

        public ActionAnimationByDirection(string name, List<SimpleAnimation> animations, int priority)
            :base(name,animations,priority)
        {
            directionMap[(int)Unit.Direction.North] = 0;
            directionMap[(int)Unit.Direction.South] = 1;
            directionMap[(int)Unit.Direction.East] = 2;
            directionMap[(int)Unit.Direction.West] = 3;
            directionMap[(int)Unit.Direction.North_East] = 4;
            directionMap[(int)Unit.Direction.South_West] = 5;
            directionMap[(int)Unit.Direction.North_West] = 6;
            directionMap[(int)Unit.Direction.South_East] = 7;
        }

        override public bool Tick()
        {
            this.currentAnimation = this.animations[directionMap[(int)this.unit.direction]];
            return base.Tick();
        }
    }
}
