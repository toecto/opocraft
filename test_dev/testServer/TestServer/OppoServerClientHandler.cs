using System;
using testClient;
using System.Net.Sockets;
using System.Diagnostics;
using System.Collections;

namespace TestServer
{
    class OppoServerClientHandler
    {
        public TcpMessageClient Net;
        public int ID;
        private OppoServer server;

        public OppoServerClientHandler(OppoServer server, TcpClient inClientSocket, int ID)
        {
            this.Net = new TcpMessageClient(inClientSocket);
            this.Net.onMessage += this.gotMessage;
            this.ID = ID;
            this.server = server;
        }

        public void Stop()
        {
            this.Net.Stop();
        }

        private void gotMessage(TcpMessageClient client)
        {
            if (client.Available > 0)
            {
                byte[] buffer = client.getMessage();
                OppoMessage message = OppoMessage.fromBin(buffer);
                message["cid"] = this.ID;
                Console.Write("From client " + ID + ": " + buffer.Length + " Bytes ");
                Console.Write(message.ToString());
                if(message.Type==OppoMessageType.GetClientID)
                {
                    client.sendMessage(message.toBin(), true);
                }
                else if (message.Type == OppoMessageType.GetClientList)
                {
                    OppoMessage msg = new OppoMessage(OppoMessageType.GetClientList);
                    foreach (DictionaryEntry Item in this.server.clientsList)
                    {
                        msg[Item.Key.ToString()] = (int)Item.Key;
                    }
                    Console.Write(msg.ToString());
                    client.sendMessage(msg.toBin(), true);
                }
                else
                    this.server.broadcast(buffer, this.ID);
            }
        }

    } //end class handleClinet
}
