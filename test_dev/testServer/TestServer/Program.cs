using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace TestServer
{
    class Program
    {
        public static Hashtable clientsList = new Hashtable();
        public static ArrayList toRemove = new ArrayList(2);
        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Parse("0.0.0.0"), 8888);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine("Server Started ....");
            counter = 0;
            while ((true))
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                handleClinet client = new handleClinet(clientSocket, counter.ToString());
                clientsList.Add(counter.ToString(), client);

                Console.WriteLine("Client " + counter + " joined");
                
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine("exit");
            Console.ReadLine();
        }

        public static void broadcast(byte[] msg, string sourceClientNo, bool toAll)
        {
            handleClinet client=null;
            NetworkStream broadcastStream;
            foreach (DictionaryEntry Item in clientsList)
            {
                client = (handleClinet)Item.Value;
                try
                {
                    if (toAll || sourceClientNo != Item.Key.ToString())
                    {
                        broadcastStream = client.clientSocket.GetStream();
                        broadcastStream.Write(msg, 0, msg.Length);
                        broadcastStream.Flush();
                    }
                }
                catch (Exception ex)
                {
                    client.Stop();
                    toRemove.Add(client.clientNo);
                }
            }

            foreach (string Item in toRemove)
            {
                clientsList.Remove(Item);
                Console.WriteLine(Item + " Removed");
            }
            toRemove.Clear();
        }  //end broadcast function
    }//end Main class


    public class handleClinet
    {
        public TcpClient clientSocket;
        public string clientNo;
        Thread ctThread;
        bool active = true;

        public handleClinet(TcpClient inClientSocket, string clientNo)
        {
            this.clientSocket = inClientSocket;
            this.clientNo = clientNo;
            ctThread = new Thread(this.Listening);
            ctThread.Start();
        }

        public void Stop()
        {
            this.active = false;
            clientSocket.Close();
            ctThread.Join();
        }

        private void Listening()
        {
            int requestCount = 0;
            byte[] bytesFrom;
            
            requestCount = 0;
            int available;
            NetworkStream networkStream = clientSocket.GetStream();
            while (this.active)
            {
                try
                {
                    requestCount = requestCount + 1;
                    
                    available = clientSocket.Available;
                    if (available>0)
                    {
                        available=clientSocket.Available;
                        bytesFrom = new byte[available];
                        networkStream.Read(bytesFrom, 0, available);
                        Console.WriteLine("From client " + clientNo + ": " + bytesFrom.Length +" Bytes");
                        Program.broadcast(bytesFrom, clientNo, true);
                    }
                }
                catch (Exception ex)
                {
                    this.Stop();
                    Console.WriteLine(ex.ToString());
                }
            }//end while
        }//end doChat
    } //end class handleClinet
}
