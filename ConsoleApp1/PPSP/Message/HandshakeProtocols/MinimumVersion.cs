using System.IO;

namespace SwiftCSharp.PPSP.Protocol
{
    public class MinimumVersion : IProtocolOption
    {
        public ProtocolTypes Type => ProtocolTypes.MinimumVersion;
        private byte _value;

        public MinimumVersion(byte value = 1)
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
