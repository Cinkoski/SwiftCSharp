using System;
using System.IO;

namespace SwiftCSharp.PPSP.Message
{
    public class Request : AbstractMessage
    {
        public override MessageTypes Type => MessageTypes.REQUEST;

        public uint BinValue;

        public Request()
        {

        }

        public override byte[] ToByteArray()
        {
            var output = ToByteList();
            output.AddRange(BitConverter.GetBytes(BinValue));
            return output.ToArray();
        }

        public override void Decode(BinaryReader br)
        {
            throw new System.NotImplementedException();
        }
    }
}
