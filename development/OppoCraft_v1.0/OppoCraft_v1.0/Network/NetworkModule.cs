using System;
using testClient;
using System.Diagnostics;

namespace OppoCraft{
    public class NetworkModule
    {

        TcpMessageClient net;
        public OppoMessageCollection buffer= new OppoMessageCollection();

        public delegate void onMessageHandler(NetworkModule x);
        public event onMessageHandler onMessage=null;

        public NetworkModule(string IP, int port = 8898)
        {
            this.net = new TcpMessageClient(IP, port);
            this.net.onMessage+=readMessage;
        }

        void readMessage(TcpMessageClient client)
        {
            if (client.Available > 0)
            {
                lock (this.buffer)
                {
                    OppoMessage msg=null;
                    try{
                        msg=OppoMessage.fromBin(client.getMessage());
                    }
                    catch(Exception ex)
                    { Debug.WriteLine("NetworkModule readMessage: " + ex.Message); }
                    if (msg != null)
                    {
                        this.buffer.AddLast(msg);

                        if (this.onMessage != null)
                            this.onMessage(this);
                    }
                }
                
            }
        }

        public bool Flush()
        {
            return this.net.Flush();
        }

        public void Stop()
        {
            this.net.Stop();
        }

        public bool Send(OppoMessage msg, bool force=false)
        {
            return this.net.sendMessage(msg.toBin(), force);
        }

        public OppoMessage getMessage()
        {
            OppoMessage msg = null;
            lock (this.buffer)
            {
                if (this.buffer.First != null)
                {
                    msg = this.buffer.First.Value;
                    buffer.RemoveFirst();
                }
            }
            return msg;

        }


    }
}
