using SwiftCSharp.PPSP.Message;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SwiftCSharp.PPSP
{
    public class PpspClient
    {
        private UdpClient _client;

        public void Initialize(string swarmId, int localPort)
        {
            _client = new UdpClient(localPort);
        }

        public async Task<byte[]> SendHandshake(Handshake handshake, IPEndPoint peerAddress)
        {
            byte[] outputArray = handshake.ToByteArray();

            await _client.SendAsync(outputArray, outputArray.Length, peerAddress);

            try
            {
                UdpReceiveResult result = await _client.ReceiveAsync();

                if (result.RemoteEndPoint.Address.ToString() != "179.43.163.154") // Ignore stun.deltamediaplayer.com sending packets. TODO: What are those packets? How to filter them?
                {
                    Console.WriteLine($"Received {result.Buffer.Length} bytes from: {result.RemoteEndPoint}");
                    return result.Buffer;
                }
            }
            catch
            {
                Console.WriteLine($"Couldn't receive from {peerAddress}");
            }

            return null;
        }
    }
}
