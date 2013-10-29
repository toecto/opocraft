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
            Console.Write("Input server: ");
            string server=Console.ReadLine();
            Console.Write("Input name: ");
            string name = Console.ReadLine();
            connect(name, server);
            while (true)
            {
                Console.Write("#");
                string msg = Console.ReadLine();
                sendMessage(msg);
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
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(msg + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }


        static void connect(string name, string server)
        {
            Console.WriteLine("Conected to Chat Server ...");
            clientSocket.Connect(server, 8888);
            serverStream = clientSocket.GetStream();

            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(name + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            Thread ctThread = new Thread(Program.getMessage);
            ctThread.Start();
        }

        static void getMessage()
        {
            while (true)
            {
                serverStream = clientSocket.GetStream();
                int buffSize = 0;

                buffSize = clientSocket.Available;
                
                if (buffSize > 0)
                {
                    byte[] inStream = new byte[buffSize];
                    serverStream.Read(inStream, 0, buffSize);
                    string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                    Console.WriteLine(buffSize+">"+returndata+"<");
                }
            }
        }
    }
}
