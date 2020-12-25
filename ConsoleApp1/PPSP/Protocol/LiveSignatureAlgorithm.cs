namespace SwiftCSharp.PPSP.Protocol
{
    public class LiveSignatureAlgorithm : IProtocolOption
    {
        public byte[] ToByteArray()
        {
            return new byte[] { (byte)ProtocolType.LiveSignatureAlgorithm, 13 }; //default: https://tools.ietf.org/html/draft-ietf-ppsp-peer-protocol-12#ref-IANADNSSECALGNUM
        }
    }
}
