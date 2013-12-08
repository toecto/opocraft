using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class ActionAnimation
    {
        public List<SimpleAnimation> animations;
        public Unit unit;
        public int priority;
        public SimpleAnimation currentAnimation;
        public string name;

        public ActionAnimation(string name, List<SimpleAnimation> animations, int priority)
        {
            this.name = name;
            this.animations = animations;
            this.priority = priority;
            this.currentAnimation = animations[0];
        }

        virtual public bool Tick()
        {
            return this.currentAnimation.Tick();
        }

        virtual public void Render(RenderSystem render, Vector2 position)
        {
            this.currentAnimation.Render(render, position);
        }
    }
}
