using System;
using System.IO;

namespace SwiftCSharp.PPSP.Protocol
{
    public class SwarmIdentifier : IProtocolOption
    {
        public ProtocolTypes Type => ProtocolTypes.SwarmIdentifier;
        private string _value;

        public SwarmIdentifier() { }

        public SwarmIdentifier(string swarmId)
        {
            _value = swarmId;
        }

        public byte[] GetValue()
        {
            // TODO: Optimize code !!!
            byte[] initBytes = { 0, 65 }; // string length
            byte[] output = new byte[67];
            var swarmBytes = Helper.StringToByteArray(_value);
            Buffer.BlockCopy(initBytes, 0, output, 0, initBytes.Length);
            Buffer.BlockCopy(swarmBytes, 0, output, 2, swarmBytes.Length);
            return output;
        }

        public void Decode(BinaryReader br)
        {
            var swarmLength = br.ReadInt16();
            var stringBytes = br.ReadBytes(swarmLength);
            _value = Helper.ByteArrayToString(stringBytes);
        }
    }
}
