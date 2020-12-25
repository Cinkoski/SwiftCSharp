namespace SwiftCSharp.PPSP.Protocol
{
    public class ChunkAdressingMethod : IProtocolOption
    {
        public byte[] ToByteArray()
        {
            return new byte[] { (byte)ProtocolType.ChunkAdressingMethod, 0 }; // default: 32-bit bins
        }
    }
}