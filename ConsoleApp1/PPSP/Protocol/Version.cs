namespace SwiftCSharp.PPSP.Protocol
{
    public class Version : IProtocolOption
    {
        public byte[] ToByteArray()
        {
            return new byte[] { (byte)ProtocolType.Version, 1 };
        }
    }
}
