using System.IO;

namespace SwiftCSharp.PPSP.Message
{
    public class Integrity : AbstractMessage
    {
        public override MessageTypes Type => MessageTypes.INTEGRITY;

        public uint BinValue;
        public byte[] Hash;

        public override byte[] ToByteArray()
        {
            var bytes = ToByteList();
            return bytes.ToArray();
        }

        public override void Decode(BinaryReader br)
        {
            BinValue = br.ReadUInt32BE();
            Hash = br.ReadBytes(20);
        }
    }
}
