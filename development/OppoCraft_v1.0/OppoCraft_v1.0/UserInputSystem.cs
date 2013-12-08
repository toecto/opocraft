using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class UserInputSystem
    {
        Game1 theGame;
        public KeyboardState keyboard;
        public KeyboardState keyboardOld;
        public MouseState mouse;
        public MouseState mouseOld;
        public Vector2 mouseCoordinatesDelta;
        public Vector2 mousePosition;
        public Coordinates mouseCoordinates;
        Vector2 mouseOldCoordinates;
        public bool mouseClicked;

        public int arrowX,arrowY;

        public UserInputSystem(Game1 game)
        {
            this.theGame = game;
            this.mouseOldCoordinates = new Vector2(0, 0);
            this.mouseCoordinates = new Coordinates(0, 0);
            this.Tick();
        }

        public bool isKeyPressed(Keys k)
        {
            return this.keyboard.IsKeyUp(k) && this.keyboardOld.IsKeyDown(k);
        }
        
        public void Tick()
        {
            this.keyboardOld = this.keyboard;
            this.keyboard = Keyboard.GetState();
            
            this.mouseOld = this.mouse;
            this.mouse = Mouse.GetState();

            this.mouseOldCoordinates = this.mousePosition;
            this.mousePosition = new Vector2(this.mouse.X, this.mouse.Y);
            this.mouseCoordinates.setVector2(this.mousePosition);

            this.mouseCoordinatesDelta = Vector2.Subtract(this.mouseOldCoordinates, this.mousePosition);

            this.arrowX = 0;
            this.arrowY = 0;
            if (this.keyboard.IsKeyDown(Keys.Right)) this.arrowX++;
            if (this.keyboard.IsKeyDown(Keys.Left)) this.arrowX--;
            if (this.keyboard.IsKeyDown(Keys.Up)) this.arrowY--;
            if (this.keyboard.IsKeyDown(Keys.Down)) this.arrowY++;

            if (this.mouseOld.LeftButton == ButtonState.Pressed && this.mouse.LeftButton == ButtonState.Released)
                this.mouseClicked = true;
            else
                this.mouseClicked = false;
        }

    }
}
