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
                clientsList.Add(counter.ToString(), clientSocket);

                Console.WriteLine("Client " + counter + " joined");
                handleClinet client = new handleClinet();
                client.startClient(clientSocket, counter.ToString());
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine("exit");
            Console.ReadLine();
        }

        public static void broadcast(string msg, string client, bool toAll)
        {
            foreach (DictionaryEntry Item in clientsList)
            {
                if(toAll || client != Item.Key)
                {
                    TcpClient broadcastSocket = (TcpClient)Item.Value;
                    NetworkStream broadcastStream = broadcastSocket.GetStream();
                    Byte[] broadcastBytes = Encoding.ASCII.GetBytes(msg);

                    broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                    broadcastStream.Flush();
                }
            }
        }  //end broadcast function
    }//end Main class


    public class handleClinet
    {
        TcpClient clientSocket;
        string clNo;

        public void startClient(TcpClient inClientSocket, string clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;

            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }

        private void doChat()
        {
            int requestCount = 0;
            byte[] bytesFrom;
            string dataFromClient = null;
            
            requestCount = 0;
            int available;
            NetworkStream networkStream = clientSocket.GetStream();
            while ((true))
            {
                try
                {
                    requestCount = requestCount + 1;
                    
                    available = (int)clientSocket.Available;
                    if (available>0)
                    {
                        bytesFrom = new byte[(int)clientSocket.Available];
                        networkStream.Read(bytesFrom, 0, (int)clientSocket.Available);
                        dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                        Console.WriteLine("From client " + clNo + ": " + dataFromClient);
                        Program.broadcast(dataFromClient, clNo, true);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }//end while
        }//end doChat
    } //end class handleClinet
}
