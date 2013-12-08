using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace OppoCraft
{

    public class AnimationFile
    {
        public Texture2D texture;
        public int width;
        public int height;
        public int id;
        public AnimationFile(Texture2D texture, int width, int height, int id)
        {
            this.texture=texture;
            this.width = width;
            this.height = height;
            this.id = id;
        }

        public List<SimpleAnimation> getAnimations(int StartX, int StartY, int frames=1, int delay=5, bool looped=true, int VerX=1, int VerY=1)
        {
            List<SimpleAnimation> animations = new List<SimpleAnimation>();
            int cnt = 0;
            for (int y = 0; y < VerY; y++)
            {
                for (int x = 0; x < VerX; x++)
                {

                    animations.Add(new SimpleAnimation(this, StartX + x * frames, StartY + y, frames, delay, looped));
                    cnt++;
                }
            }
            return animations;
        }

    }
}
