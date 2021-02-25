using System;
using System.IO;

namespace SwiftCSharp.PPSP.Protocol
{
    public class MhtFunction : IProtocolOption
    {
        public ProtocolTypes Type => ProtocolTypes.MhtFunction;
        private byte _value;

        public byte[] GetValue()
        {
            Console.WriteLine("MhtFunction not implemented!");
            throw new NotImplementedException();
        }

        public void Decode(BinaryReader br)
        {
            Console.WriteLine("MhtFunction not implemented!");
            throw new NotImplementedException();
        }
    }
}
