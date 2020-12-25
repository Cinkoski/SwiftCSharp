using SwiftCSharp.PPSP.Protocol;
using System;
using System.Collections.Generic;

namespace SwiftCSharp.PPSP
{
    class PpspClient
    {
        // 1. HANDSHAKE to all clients from tracker

        private readonly string _swarmId;

        public PpspClient(string swarmId)
        {
            _swarmId = swarmId;
        }

        public void SendHandshake()
        {
            List<byte> outputBuffer = new List<byte>();
            outputBuffer.Add((byte)MessageType.HANDSHAKE);
            outputBuffer.AddRange(new byte[] { 0, 0, 0, 0 }); // add dst 0 address
            outputBuffer.AddRange(getRandomBytes(4)); // add src random address ??
            outputBuffer.AddRange(new SwarmIdentifier(_swarmId).ToByteArray());
            outputBuffer.AddRange(new CipMethod().ToByteArray());
            outputBuffer.AddRange(new LiveSignatureAlgorithm().ToByteArray());
            outputBuffer.AddRange(new ChunkAdressingMethod().ToByteArray());
            outputBuffer.AddRange(new LiveDiscardWindow().ToByteArray());

            var outputArray = outputBuffer.ToArray();
        }

        public byte[] getRandomBytes(int size)
        {
            var randomBytes = new byte[size];
            new Random().NextBytes(randomBytes);
            return randomBytes;
        }
    }
}
