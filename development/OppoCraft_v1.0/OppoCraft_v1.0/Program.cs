using System;
using System.Windows.Forms;
using TestServer;
using testClient;
using System.Threading;

namespace OppoCraft
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static NetworkModule network=null;
        public static OppoServer server=null;
        public static Thread gameThread=null;
        static void Main(string[] args)
        {
            Application.ApplicationExit += Application_ApplicationExit;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartForm());
            /**/
                  
              /*
            NetworkModule net = new NetworkModule("127.0.0.1");
            net.Send(new OppoMessage(OppoMessageType.GetClientID));
            net.Send(new OppoMessage(OppoMessageType.StartGame));
            net.Flush();
            while (net.buffer.Count == 0) ;//wait for client id
            using (Game1 game = new Game1(net,"main"))
            {
                game.Run();
            }
             /**/
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (Program.network != null)
                Program.network.Stop();

            if (Program.server != null)
                Program.server.Stop();
            
            if (Program.gameThread != null)
                Program.gameThread.Abort();

        }
    }
#endif
}

