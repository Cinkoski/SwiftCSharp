using System;
using System.IO;

namespace SwiftCSharp.PPSP.Protocol
{
    public class ChunkSize : IProtocolOption
    {
        public ProtocolTypes Type => ProtocolTypes.ChunkSize;
        private int _value;

        public byte[] GetValue()
        {
            var bytes = BitConverter.GetBytes(_value);

            if (BitConverter.IsLittleEndian)
                bytes.Reverse();

            return bytes;
        }

        public void Decode(BinaryReader br)
        {
            _value = br.ReadInt32();
        }
    }
}
