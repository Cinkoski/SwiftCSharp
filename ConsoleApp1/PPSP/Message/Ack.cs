using System;
using System.IO;

namespace SwiftCSharp.PPSP.Message
{
    public class Ack : AbstractMessage
    {
        public override MessageTypes Type => MessageTypes.ACK;

        public uint BinValue;
        public ulong OneWayDelay;

        public override byte[] ToByteArray()
        {
            var bytes = ToByteList();
            bytes.AddRange(BitConverter.GetBytes(BinValue));
            bytes.AddRange(BitConverter.GetBytes(OneWayDelay));
            return bytes.ToArray();
        }

        public override void Decode(BinaryReader br)
        {
            throw new NotImplementedException();
        }
    }
}
