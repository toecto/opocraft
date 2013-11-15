using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace testClient
{

    public enum OppoMessageType
    {
        GetClientID,
        GetClientList,
        Disconected,
        Conected,
        Ping,


    };

    public class OppoMessage : Dictionary<string, Int32>
    {

        public OppoMessageType Type;

        public Dictionary<string, string> Text = new Dictionary<string, string>();

        public OppoMessage(OppoMessageType Type)
        {
            this.Type = Type;
        }
        public OppoMessage()
        {
        }

        public byte[] toBin()
        {
            MemoryStream rez = new MemoryStream();
            byte[] pointer;

            rez.Write(BitConverter.GetBytes((Int16)this.Type), 0, sizeof(Int16));

            foreach (KeyValuePair<string, int> pair in this)
            {
                pointer = Encoding.ASCII.GetBytes(pair.Key);
                rez.Write(BitConverter.GetBytes((Int16)pointer.Length), 0, sizeof(Int16));
                rez.Write(pointer, 0, pointer.Length);

                rez.Write(BitConverter.GetBytes((Int32)pair.Value), 0, sizeof(Int32));
            }

            rez.Write(BitConverter.GetBytes((Int16)0), 0, sizeof(Int16));

            foreach (KeyValuePair<string, string> pair in this.Text)
            {
                pointer = Encoding.ASCII.GetBytes(pair.Key);
                rez.Write(BitConverter.GetBytes((Int16)pointer.Length), 0, sizeof(Int16));
                rez.Write(pointer, 0, pointer.Length);

                pointer = Encoding.ASCII.GetBytes(pair.Value);
                rez.Write(BitConverter.GetBytes((Int16)pointer.Length), 0, sizeof(Int16));
                rez.Write(pointer, 0, pointer.Length);
            }
            rez.Write(BitConverter.GetBytes((Int16)0), 0, sizeof(Int16));
            return rez.ToArray();
        }

    }
}