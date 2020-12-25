namespace SwiftCSharp.PPSP.Protocol
{
    public class LiveDiscardWindow : IProtocolOption
    {
        public byte[] ToByteArray()
        {
            return new byte[] { (byte)ProtocolType.LiveDiscardWindow, 0, 0, 64, 0 }; // TBD
        }
    }
}
