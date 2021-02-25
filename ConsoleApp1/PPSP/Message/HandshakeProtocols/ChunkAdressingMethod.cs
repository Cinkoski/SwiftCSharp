using SwiftCSharp.PPSP.Protocol.Enums;
using System.IO;

namespace SwiftCSharp.PPSP.Protocol
{
    public class ChunkAdressingMethod : IProtocolOption
    {
        public ProtocolTypes Type => ProtocolTypes.ChunkAdressingMethod;
        private ChunkAdressingMethods _value;

        public ChunkAdressingMethod(ChunkAdressingMethods value = ChunkAdressingMethods.IntBins)
        {
            _value = value;
        }

        public byte[] GetValue()
        {
            return new byte[] { (byte)_value };
        }

        public void Decode(BinaryReader br)
        {
            _value = (ChunkAdressingMethods)br.ReadByte();
        }
    }
}