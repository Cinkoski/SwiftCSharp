using System.IO;

namespace SwiftCSharp.PPSP.Protocol
{
    public class CipMethod : IProtocolOption
    {
        public ProtocolTypes Type => ProtocolTypes.CipMethod;
        private byte _value;

        public CipMethod(byte value = 3)
        {
            _value = value;
        }

        public byte[] GetValue()
        {
            return new byte[] { _value }; // default: Unified Merkle Tree
        }

        public void Decode(BinaryReader br)
        {
            _value = br.ReadByte();
        }
    }
}