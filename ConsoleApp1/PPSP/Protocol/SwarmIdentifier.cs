using ConsoleApp1;
using System;

namespace SwiftCSharp.PPSP.Protocol
{
    public class SwarmIdentifier : IProtocolOption
    {
        private readonly string _swarmId;
        public SwarmIdentifier(string swarmId)
        {
            _swarmId = swarmId;
        }

        public byte[] ToByteArray()
        {
            byte[] initBytes = { (byte)ProtocolType.SwarmIdentifier, 0, 65 };
            byte[] output = new byte[69];
            var swarmBytes = Helper.StringToByteArray(_swarmId);
            Buffer.BlockCopy(initBytes, 0, output, 0, initBytes.Length);
            Buffer.BlockCopy(swarmBytes, 0, output, 3, swarmBytes.Length);
            return output;
        }
    }
}
