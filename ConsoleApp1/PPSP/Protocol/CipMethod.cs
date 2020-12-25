namespace SwiftCSharp.PPSP.Protocol
{
    public class CipMethod : IProtocolOption
    {
        public byte[] ToByteArray()
        {
            return new byte[] { (byte)ProtocolType.CipMethod, 3 }; // default: Unified Merkle Tree
        }
    }
}