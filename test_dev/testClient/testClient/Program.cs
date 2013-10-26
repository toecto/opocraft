using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace testClient
{
    class Program
    {
        
        static TcpClient clientSocket = new TcpClient();
        static NetworkStream serverStream = default(NetworkStream);
        
        

        static void Main(string[] args)
        {
            //Console.Write("Input server: ");
            string server = "192.168.1.3";// Console.ReadLine();
            Console.Write("Input name2: ");
            string name = Console.ReadLine();
            connect(name, server);
            while (true)
            {
                Console.Write("#");
                string msg = Console.ReadLine();
                for (int i = 0; i < 5; i++)
                {
                    sendMessage(msg+i);
                }
                
                if (msg == "exit")
                {
                    clientSocket.Close();
                    serverStream.Close();
                    break;
                }
            }
            Console.Write("Done");
            Console.ReadLine();
        }

        static void sendMessage(string msg)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(msg);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }


        static void connect(string name, string server)
        {
            Console.WriteLine("Conected to Server ...");
            clientSocket.Connect(server, 8888);
            serverStream = clientSocket.GetStream();
            Console.WriteLine("SendTimeout: " + clientSocket.SendTimeout);
            Console.WriteLine("ReceiveTimeout: " + clientSocket.ReceiveTimeout);
            Console.WriteLine("NoDelay: " + clientSocket.NoDelay);
            //Console.WriteLine("NoDelay: " + clientSocket.);
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(name);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            Thread ctThread = new Thread(Program.getMessage);
            ctThread.Start();
        }

        static void getMessage()
        {
            serverStream = clientSocket.GetStream();
            while (true)
            {
                
                int buffSize = 0;

                buffSize = clientSocket.Available;

                byte[] inStream = new byte[buffSize];
                serverStream.Read(inStream, 0, buffSize);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                Console.WriteLine(buffSize+">"+returndata+"<");

            }
        }
    }
}
