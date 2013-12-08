using System;

namespace testClient
{
    class Program
    {
        static bool EnableEcho = true;

        static void Main(string[] args)
        {
            Console.Write("Input server (Empty for localhost): ");
            string server = "";//Console.ReadLine();
            if (server == "") server = "127.0.0.1";
            TcpMessageClient client = new TcpMessageClient(server,8898);
            client.onMessage += readMessages;

            while (true)
            {
                
                foreach(int val in Enum.GetValues(typeof(OppoMessageType)))
                {
                    Console.WriteLine(val + ") " + ((OppoMessageType)val)+".");
                }
                
                Console.Write("#");
                string userInput = Console.ReadLine();
                if (userInput == "") continue;
                if (userInput == "exit") break;
                if (userInput == "echo") EnableEcho = !EnableEcho;
                else
                {
                    OppoMessage msg = makeMessage(userInput);
                    client.sendMessage(msg.toBin(),true);
                }

               
                //OppoMessage msg = new OppoMessage(OppoMessageType.CommmandUnit);
                //msg.Text["msg"] = userInput;
                //client.sendMessage(msg.toBin());
                
            }
            Console.Write("Done");
            client.Stop();
        }

        public static OppoMessage makeMessage(string type)
        {
            OppoMessage msg = new OppoMessage((OppoMessageType)(int.Parse(type)));
            string key;
            int ival;
            while((key=readStr("Key"))!="")
            {
                ival = readInt("Int Value");
                msg[key] = ival;
            }
            return msg;
        }


        public static void readMessages(TcpMessageClient client)
        {
            if (client.Available > 0)
            { 
                Byte[] RawMessage;
                while ((RawMessage = client.getMessage()) != null)
                {
                    OppoMessage message = OppoMessage.fromBin(RawMessage);
                    //if (EnableEcho) 
                    Console.WriteLine(message.ToString());
                }
            }

        }

        public static int readInt(string msg = "")
        {
            int rez = 0;
            if (msg != "")
                Console.Write(msg + ": ");

            string buffer = Console.ReadLine();
            
            if (buffer != "")
                rez = int.Parse(buffer);
            
            return rez;
        }

        public static string readStr(string msg = "")
        {
            if (msg != "")
                Console.Write(msg + ": ");
        
            return Console.ReadLine();
        }

    }
}
