using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace testClient
{
    
    public class TcpMessageClient
    {
        private TcpClient ClientSocket=new TcpClient();
        private NetworkStream ServerStream;

        private LinkedList<byte[]> IncomeMessages = new LinkedList<byte[]>();
        private LinkedList<byte[]> OutcomeMessages = new LinkedList<byte[]>();
        public delegate void onMessageHandler(TcpMessageClient x);
        public event onMessageHandler onMessage = null;
        private Thread IncomeThread = null;
        private bool Active = true;

        public int Available
        {
            get
            {
                return this.IncomeMessages.Count;
            }
        }

        public TcpMessageClient(string IP, int Port)
        {
            this.ClientSocket.Connect(IP, Port);
            this.Start();
        }
        public TcpMessageClient(TcpClient ClientSocket)
        {
            this.ClientSocket = ClientSocket;
            this.Start();
        }

        private void Start()
        {
            this.ServerStream = this.ClientSocket.GetStream();
            this.IncomeThread = new Thread(this.receiveMessageLoop);
            IncomeThread.Start();
        }

        public void Stop()
        {
            this.Active = false;
            if (this.IncomeThread != null)
            {
                this.IncomeThread.Abort();
            }
            if (this.ClientSocket != null)
                this.ClientSocket.Close();
            this.ClientSocket = null;
            
        }

        private void receiveMessageLoop()
        {
            int buffSize = 0;
            int LeftToRead=0, AvailableToRead;
            byte[] buffer;
            while (this.Active)
            {
                try
                {
                    buffer = new Byte[sizeof(Int32)];
                    ServerStream.Read(buffer, 0, sizeof(Int32));
                    buffSize = BitConverter.ToInt32(buffer, 0);
                    buffer = new Byte[buffSize];
                    LeftToRead = buffSize;

                    while (LeftToRead > 0)
                    {
                        AvailableToRead = ClientSocket.Available;
                        if (AvailableToRead > LeftToRead) AvailableToRead = LeftToRead;

                        ServerStream.Read(buffer, buffSize - LeftToRead, AvailableToRead);
                        LeftToRead -= AvailableToRead;
                    }
                    /**/
                    lock (this.IncomeMessages)
                    {
                        this.IncomeMessages.AddLast(buffer);
                    }

                    if (this.onMessage != null)
                    { 
                        try 
                        {
                            this.onMessage(this);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("TcpMessageClient receiveMessageLoop onMessage: " + ex.Message);
                                this.onMessage=null;
                        }/**/
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("TcpMessageClient receiveMessageLoop: " + ex.Message);
                    return; 
                }/**/
            }
        }

        public byte[] getMessage()
        {
            if (this.IncomeMessages.Count == 0) return null;
            byte[] Message;
            lock (this.IncomeMessages)
            {
                Message = this.IncomeMessages.First.Value;
                this.IncomeMessages.RemoveFirst();
            }
            return Message;
        }

        public bool sendMessage(byte[] msg, bool forceFlush=false)
        {
            byte[] rez = new byte[msg.Length + sizeof(Int32)];
            BitConverter.GetBytes((Int32)msg.Length).CopyTo(rez,0);
            msg.CopyTo(rez, sizeof(Int32));
            this.OutcomeMessages.AddLast(rez);
            if (forceFlush)
                return this.Flush();
            return true;
       }

        public bool Flush()
        {
            int cursor = 0;
            foreach (byte[] msg in this.OutcomeMessages)
            {
                cursor += msg.Length;
            }
            byte[] rez = new byte[cursor];
            
            cursor = 0;
            foreach (byte[] msg in this.OutcomeMessages)
            {
                msg.CopyTo(rez, cursor);
                cursor += msg.Length;
            }
            this.OutcomeMessages.Clear();
            try
            {
                this.ServerStream.Write(rez, 0, rez.Length);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}
