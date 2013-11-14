using System;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Diagnostics;
using testClient;

namespace TestServer
{
    public class OppoServer
    {
        public Hashtable clientsList = new Hashtable();
        TcpListener serverSocket;
        string IP;
        int Port;
        Thread sThread;
        
        public OppoServer(string IP, int Port)
        {
            this.IP = IP;
            this.Port = Port;
        }

        public bool Start()
        {
            this.serverSocket = new TcpListener(IPAddress.Parse(this.IP), this.Port);
            try
            {
                serverSocket.Start();
            }
            catch (Exception ex)
            {
                return false;
            }

            this.sThread = new Thread(this.ServerLoop);
            sThread.Start();

            return true;
        }

        public void ServerLoop()
        {
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;
            counter = 0;
            while ((true))
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                OppoServerClientHandler client = new OppoServerClientHandler(this, clientSocket, counter);

                Console.WriteLine("Client " + counter + " joined");
                OppoMessage netClientMsg = new OppoMessage(OppoMessageType.Conected);
                netClientMsg["cid"] = client.ID;
                this.broadcast(netClientMsg.toBin(), client.ID, false);

                clientsList.Add(counter, client);

            }
        }

        public void Stop()
        {
            if(this.sThread!=null)
                this.sThread.Abort();
            this.sThread = null;
            foreach (DictionaryEntry Item in clientsList)
            {
                OppoServerClientHandler client = (OppoServerClientHandler)Item.Value;
                try
                {
                    client.Stop();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("OppoServer Stop " + ex.Message);
                }
            }
            this.serverSocket.Stop();
        }

        public void broadcast(byte[] msg, int sourceClientNo, bool toAll=true)
        {
            OppoServerClientHandler client = null;
            ArrayList toRemove = new ArrayList(2);
            foreach (DictionaryEntry Item in clientsList)
            {
                client = (OppoServerClientHandler)Item.Value;

                if (toAll || sourceClientNo != (int)Item.Key)
                {
                    if (!client.Net.sendMessage(msg, true))
                    {
                        client.Stop();
                        toRemove.Add(client.ID);
                    }
                }
            }

            foreach (int Item in toRemove)
            {
                clientsList.Remove(Item);
                OppoMessage discMsg=new OppoMessage(OppoMessageType.Disconected);
                discMsg["cid"] = Item;
                Console.WriteLine(discMsg.ToString());
                this.broadcast(discMsg.toBin(), 0);
                Console.WriteLine("Client "+ Item + " is removed");
            }
            
        }  //end broadcast function
    }
}
