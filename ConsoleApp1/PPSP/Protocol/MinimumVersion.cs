namespace SwiftCSharp.PPSP.Protocol
{
    public class MinimumVersion : IProtocolOption
    {
        public byte[] ToByteArray()
        {
            return new byte[] { (byte)ProtocolType.MinimumVersion, 1 };
        }
    }
}
