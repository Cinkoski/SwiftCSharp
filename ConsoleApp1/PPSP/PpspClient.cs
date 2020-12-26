using SwiftCSharp.PPSP.Message;
using System.Net;
using System.Net.Sockets;

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

        public byte[] SendHandshake(IPEndPoint endpoint, int localPort)
        {
            var outputArray = new Handshake(_swarmId).ToByteArray();

            var udpClient = new UdpClient(localPort);
            udpClient.Connect(endpoint);
            udpClient.Send(outputArray, outputArray.Length);
            return udpClient.Receive(ref endpoint);
        }
    }
}
