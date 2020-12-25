namespace SwiftCSharp.PPSP.Protocol
{
    public class EndOption : IProtocolOption
    {
        public byte[] ToByteArray()
        {
            return new byte[] { 255 };
        }
    }
}
