using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    public class UserPoints
    {
        public int points = 0;
        public bool warning=false;

        public bool canWithdraw(int amount)
        {
            return this.points - amount >= 0;
        }

        public bool tryWithdraw(int amount)
        {
            if (this.points - amount < 0)
            {
                this.warning = true;
                return false;
            }
            this.warning = false;
            this.points -= amount;
            return true;
        }

        public void add(int amount)
        {
            this.points += amount;
        }

    }
}
