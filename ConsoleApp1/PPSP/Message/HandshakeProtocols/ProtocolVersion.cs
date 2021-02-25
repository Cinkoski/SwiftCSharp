using System.IO;

namespace SwiftCSharp.PPSP.Protocol
{
    public class ProtocolVersion : IProtocolOption
    {
        public ProtocolTypes Type => ProtocolTypes.Version;
        private byte _value;

        public ProtocolVersion(byte value = 1)
        {
            _value = value;
        }

        public byte[] GetValue()
        {
            return new byte[] { _value };
        }

        public void Decode(BinaryReader br)
        {
            _value = br.ReadByte();
        }
    }
}
