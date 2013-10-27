using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OppoCraft
{
    public class Debugger
    {
        Game1 theGame;

        LinkedList<String> msgList;
        private int rowSize;

        public int scrollRow;

        public Debugger(Game1 g)
        {
            this.theGame = g;
            this.msgList = new LinkedList<string>();
            this.rowSize = 12;
            this.scrollRow = 0; 
        }

        public void AddMessage(string msg)
        {
            this.msgList.AddLast(msg);
        }

        public void RenderMessages()
        {
            int currMsg = 0;

            foreach (string item in msgList)
            {
                this.theGame.renderSystem.DrawText(item, new Vector2(5, this.rowSize * currMsg - this.scrollRow));
                currMsg++;
            }
        }
    }
}
