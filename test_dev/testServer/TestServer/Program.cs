using System;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            OppoServer server=new OppoServer("0.0.0.0", 8898);
            server.Start();
            Console.WriteLine("Server started ...");
        }

     
    }//end Main class

}
