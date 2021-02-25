using System.IO;

namespace SwiftCSharp.PPSP.Message
{
    public class PexRes4 : AbstractMessage
    {
        public override MessageTypes Type => MessageTypes.PEX_RES4;

        public uint Address;
        public ushort Port;

        public override byte[] ToByteArray()
        {
            var bytes = ToByteList();
            return bytes.ToArray();
        }

        public override void Decode(BinaryReader br)
        {
            Address = br.ReadUInt32BE();
            Port = br.ReadUInt16BE();
        }
    }
}
