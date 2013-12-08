using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using testClient;
using TestServer;
using System.Diagnostics;
using System.Threading;

namespace OppoCraft
{
    public partial class StartForm : Form
    {
        int cid = 0;
        Dictionary<int, bool> clients = new Dictionary<int, bool>();
        Thread checker;
        public StartForm()
        {
            InitializeComponent();

        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            if (Program.network != null)
            {
                Program.network.Stop();
                ConnectionStatus.Text = "Disconnected";
                ConnectBtn.Text = "Connet";
            }
            else
            {
                if (IPAddr.Text == "")
                    IPAddr.Text = "127.0.0.1";
                if (!this.connect(IPAddr.Text))
                    ConnectionStatus.Text = "Failed to connect";
                else
                    ConnectBtn.Text = "Disconnet";
            }
        }

        private void StartSrvBtn_Click(object sender, EventArgs e)
        {
            if (Program.server != null)
            {
                Program.server.Stop();
                Program.server = null;
                this.serverStatus.Text = "Stopped";
                StartSrvBtn.Text = "Start Server";
            }
            else
            { 
            if (Program.network != null)
            {
                Program.network.Stop();
                ConnectionStatus.Text = "Disconnected";
            }
            Program.server = new OppoServer("0.0.0.0", 8898);
            if (Program.server.Start())
            {
                this.serverStatus.Text = "Started";
                StartSrvBtn.Text = "Stop Server";
            }
            else
                this.serverStatus.Text = "Failed to start";
            }
            
        }

        private void readMessage(NetworkModule network)
        {
            
            OppoMessage msg;
            msg = network.getMessage();
            //Debug.WriteLine("*****"+msg.ToString());

            if (msg.Type == OppoMessageType.GetClientID)
            {
                this.cid = msg["cid"];
                ConnectionStatus.Invoke(new MethodInvoker(delegate { ConnectionStatus.Text = "Connected"; }));
                this.checker = new Thread(this.continiousChecker);
                this.checker.Start();
                
                //this.sendMessage(new OppoMessage(OppoMessageType.ReadyToPlay));
                //ReadyBtn.Invoke(new MethodInvoker(delegate { ReadyBtn.Enabled = true;}));
            }

            if (msg.Type == OppoMessageType.GetClientList)
            {
                clients = new Dictionary<int, bool>();
                foreach (KeyValuePair<string, int> i in msg)
                { 
                    clients[i.Value] = true;
                }
                PrintClients();
                if (checkAllReady() && Program.server != null)
                {
                    this.sendMessage(new OppoMessage(OppoMessageType.LoadGame));
                }
            }

            if (msg.Type == OppoMessageType.LoadGame)
            {
                network.onMessage -= this.readMessage;
                this.checker.Abort();
                Program.gameThread = new Thread(this.LoadGame);
                Program.gameThread.Start();
                
            }

        }

        private void LoadGame()
        {
            string map = null; //client
            if (Program.server != null) //server-client
                map = "main";
            int enemyCid=0;
            
            foreach(KeyValuePair<int,bool> item in this.clients)
            {
                if (item.Key != this.cid)
                {
                    enemyCid = item.Key;
                    break;
                }
            }

            using (Game1 game = new Game1(Program.network, this.cid, enemyCid, map))
            {
                game.Run();
            }
        }


        public bool connect(string IP)
        {
            try
            {
                Program.network = new NetworkModule(IP, 8898);
                Program.network.onMessage += this.readMessage;
                this.sendMessage(new OppoMessage(OppoMessageType.GetClientID));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Start connect: " + ex.Message);
                return false;
            }
            return true;
        }

        public bool sendMessage(OppoMessage msg)
        {
            msg["cid"]=this.cid;
            return Program.network.Send(msg,true);
        }

        public void continiousChecker()
        {
            while ((true))
            {

                if(!this.sendMessage(new OppoMessage(OppoMessageType.Ping))) break;
                this.sendMessage(new OppoMessage(OppoMessageType.GetClientList));
                Thread.Sleep(500);
            }
        }

        public bool checkAllReady()
        {
            int cntReady = 0;
            foreach (KeyValuePair<int, bool> i in this.clients)
            {
                cntReady++;
            }

            int waitPlayers = 2;
            if (onePlayerChk.Checked)
                waitPlayers = 1;

            if (cntReady == waitPlayers)
            {
                return true;
            }
            return false;
        }

        public void PrintClients()
        {
            string rez = "Player list:\n";
            foreach (KeyValuePair<int, bool> i in this.clients)
            {
                rez += "Client" + i.Key + " - Ready!\n";
            }
            ConnectionStatus.Invoke(new MethodInvoker(delegate { ConnectedClients.Text = rez; }));
        }


    }
}
