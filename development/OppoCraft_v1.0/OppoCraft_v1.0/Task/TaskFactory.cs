using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace OppoCraft
{
    class DriverTaskFactory
    {
        public static Task Create(string driver)
        {
            switch (driver)
            {
                case "Knight":
                    return new TaskKnightDriver();

                case "Archer":
                    return new TaskArcherDriver();

                case "Lumberjack":
                    return new TaskLumberjackDriver();

                case "Tree":
                    return new TaskTreeDriver();

                case "Shell":
                    return new TaskShellDriver();

                case "Tower":
                    return new TaskTowerDriver();
                
                case "Castle":
                    return new TaskCastleDriver();

            }

            return null;
        }
    }
}
