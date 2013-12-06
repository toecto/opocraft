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
        LoadGame,
        StartGame,

        CreateUnit,
        RemoveUnit,
        Movement,
        ChangeState,
        Stop,
        CreateDecale,
        TreeGrow,

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

        public static OppoMessage fromBin(byte[] arrBytes)
        {
            int length, iValue,current;
            string key,sValue;
            OppoMessage rez = new OppoMessage();

            iValue = BitConverter.ToInt16(arrBytes, 0);
            rez.Type = (OppoMessageType)iValue;
            
            current = 2;
            while ((length = BitConverter.ToInt16(arrBytes, current)) != 0)
            {
                current+=2;
                key = Encoding.Default.GetString(arrBytes,current,length);
                current += length;
             
                iValue = BitConverter.ToInt32(arrBytes, current);
                current += 4;
                rez.Add(key, iValue);
            }

            current += 2;
            while ((length = BitConverter.ToInt16(arrBytes, current)) != 0)
            {
                current += 2;
                key = Encoding.Default.GetString(arrBytes, current, length);
                current += length;
                
                length = BitConverter.ToInt16(arrBytes, current);
                current += 2;
                sValue = Encoding.Default.GetString(arrBytes, current, length);
                current += length;
                rez.Text.Add(key, sValue);
            }

            return rez;
        }

        public override string ToString()
        {
            string rez = "";
            rez += "Message "+this.Type.ToString()+"\n";
            foreach (KeyValuePair<string, int> pair in this)
            {
                rez += pair.Key + "=" + pair.Value + " ";
            }
            if (this.Count > 0)
                rez += "\n";
            
            foreach (KeyValuePair<string, string> pair in this.Text)
            {
                rez += pair.Key + "=" + pair.Value + " ";
            }
            if(this.Text.Count>0)
                rez += "\n";

            return rez;
        }
    }
}
