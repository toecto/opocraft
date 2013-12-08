using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;

namespace OppoCraft
{
    public class SimpleAnimation
    {
        public AnimationFile file;
        public int frames;
        public int delay=5;
        public int startX;
        public int startY;
        public bool looped;

        public int currentDelay;
        public int currentFrame;


        public SimpleAnimation(AnimationFile file, int startX, int startY, int frames = 1, int delay = 5, bool looped = true)
        {
            this.file = file;
            this.frames = frames;
            this.startX = startX;
            this.startY = startY;
            this.looped = looped;
            this.delay = delay;
        }

        public Rectangle getFrame(int number)
        {

            return new Rectangle((this.startX + number) * this.file.width, this.startY * this.file.height, this.file.width, this.file.height);
        }

        public bool Tick()
        {
            this.currentDelay++;

            if (this.currentDelay >= this.delay)
            {
                
                this.currentFrame++;

                if (this.currentFrame >= this.frames)
                {
                    this.currentFrame = 0;
                    if (!this.looped)
                    {
                        this.currentFrame = this.frames-1;
                        return false;
                    }
                }
                this.currentDelay = 0;
                
            }
            return true;
        }

        public void Render(RenderSystem render, Vector2 position)
        {
            this.Tick();
            position.X -= this.file.width / 2;
            position.Y -= this.file.height - 20;
            render.Draw(this.file.texture, position, getFrame(this.currentFrame), Microsoft.Xna.Framework.Color.White);
        }

        public void Reset()
        {
            this.currentDelay = 0;
            this.currentFrame = 0;
        }
        public void Random()
        {
            this.currentDelay = 0;
            this.currentFrame = (new Random()).Next(0, this.frames - 1);
        }
    }
}
