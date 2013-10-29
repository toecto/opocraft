using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace testClient
{
    
    class TcpMessageClient
    {
        private TcpClient ClientSocket = new TcpClient();
        private NetworkStream ServerStream;
        private IPAddress IP;
        private int Port;

        private LinkedList<byte[]> MessageList = new LinkedList<byte[]>();
        public delegate void onMessageHandler(TcpMessageClient x);
        public event onMessageHandler onMessage;

        public int Available
        {
            get
            {
                return this.MessageList.Count;
            }
        }

        public TcpMessageClient(string IP, int Port)
        {
            this.IP = IPAddress.Parse(IP);
            this.Port = Port;
        }

        public void Connect()
        {
            this.ClientSocket.Connect(this.IP, this.Port);
            this.ServerStream = this.ClientSocket.GetStream();
            Thread ctThread = new Thread(this.receiveMessageLoop);
            ctThread.Start();
        }

        private void receiveMessageLoop()
        {
            int buffSize = 0;
            int LeftToRead=0, AvailableToRead;
            byte[] buffer;
            while (true)
            {
                buffer = new Byte[sizeof(Int32)];
                ServerStream.Read(buffer, 0, sizeof(Int32));
                buffSize = BitConverter.ToInt32(buffer,0);
                buffer = new Byte[buffSize];
                LeftToRead = buffSize;

                //ServerStream.Read(buffer, 0, buffSize);
                while (LeftToRead > 0)
                {
                    AvailableToRead = ClientSocket.Available;
                    if (AvailableToRead > LeftToRead) AvailableToRead = LeftToRead;

                    ServerStream.Read(buffer, buffSize - LeftToRead, AvailableToRead);
                    LeftToRead -= AvailableToRead;
                }
                /**/
                lock (this.MessageList)
                {
                    this.MessageList.AddLast(buffer);
                }

                if (this.onMessage != null)
                    this.onMessage(this);
                
            }
        }

        public byte[] getMessage()
        {
            if (this.MessageList.Count == 0) return null;
            byte[] Message;
            lock (this.MessageList)
            {
                Message = this.MessageList.First.Value;
                this.MessageList.RemoveFirst();
            }
            return Message;
        }

        public void sendMessage(byte[] msg)
        {
            byte[] lengthPrefix = BitConverter.GetBytes((Int32)msg.Length);
            ServerStream.Write(lengthPrefix, 0, lengthPrefix.Length);
            ServerStream.Write(msg, 0, msg.Length);
            ServerStream.Flush();
        }


    }
}
