using System.IO;

namespace SwiftCSharp.PPSP.Protocol
{
    public class LiveSignatureAlgorithm : IProtocolOption
    {
        public ProtocolTypes Type => ProtocolTypes.LiveSignatureAlgorithm;
        private byte _value = 13;

        public byte[] GetValue()
        {
            return new byte[] { _value }; //https://tools.ietf.org/html/draft-ietf-ppsp-peer-protocol-12#ref-IANADNSSECALGNUM
        }

        public void Decode(BinaryReader br)
        {
            _value = br.ReadByte();
        }
    }
}
