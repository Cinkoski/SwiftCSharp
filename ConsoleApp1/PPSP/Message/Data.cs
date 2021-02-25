
using System.IO;

namespace SwiftCSharp.PPSP.Message
{
    public class Data : AbstractMessage
    {
        public override MessageTypes Type => MessageTypes.DATA;

        public uint BinValue;
        public ulong Timestamp;
        public byte[] DataBytes;

        public override byte[] ToByteArray()
        {
            var bytes = ToByteList();
            return bytes.ToArray();
        }

        public override void Decode(BinaryReader br)
        {
            BinValue = br.ReadUInt32BE();
            Timestamp = br.ReadUInt64BE();
            DataBytes = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
        }
    }
}
