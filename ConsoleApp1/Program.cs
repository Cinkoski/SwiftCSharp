using SwiftCSharp.PPSP.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        private static UdpClient _client;

        static void Main(string[] args)
        {
            // Flow:
            // Initialize
            // Get all channels list
            // Connect to STUN server - obtain external IP and port
            // Connect to Tracker server - obtain all peer list with specific swarm id
            // Send HANDSHAKE via UDP to all peers
            // Receive all messages - find peers with corrent channel id in message
            // Reply to those peers
            // Receive video data
            // Put video data chunks to media player
            // Send disconnect messages to connected peers
            // Send Leave to Tracker

            // Initialize and connect to STUN
            Console.WriteLine("Swift::Initialize");
            Swift swiftClient = new Swift();
            swiftClient.Init();

            // Get swarm id
            //new ProgramGuide().GetAllChannels().List.First().ChannelList.First().SwarmId; // first category -> first channel
            string swarmId = "0d20a474fbdda19bcd6178fb9cba06036a74db311e7c8734072f25a529defa5faebad3f65e6292bc09c9c4c531dbc49cf39084b9633c3722c21616b3016738ab1b"; // for testing only
            Console.WriteLine($"SwarmID: {swarmId}");

            // Connect to Tracker
            Console.WriteLine("Tracker::Announce");

            IEnumerable<PeerInfoModel> peersList = swiftClient.Connect(swarmId).Container.SwarmResult.First().PeerGroup.PeerInfo
                .Where(p => p.PeerAddr.Type == PeerAddrType.REFLEXIVE && p.PeerAddr.IpAddress.Address != swiftClient.SwiftTracker.RemoteAddress.Address.ToString());

            // Send HANDSHAKE to peers

            _client = new UdpClient(swiftClient.SwiftTracker.LocalAddress.Port);

            foreach (var peerInfo in peersList)
            {
                IPEndPoint peerAddress = new IPEndPoint(IPAddress.Parse(peerInfo.PeerAddr.IpAddress.Address), peerInfo.PeerAddr.Port);
                Console.WriteLine($"Sending to PeerAddr: {peerAddress}");

                // TODO: Move to SomeClient.cs
                var handshakeBytes = new Handshake(swarmId).ToByteArray();
                _client.Send(handshakeBytes, handshakeBytes.Length, peerAddress);
                _client.BeginReceive(receiveCallback, peerAddress);
            }

            // TODO: Wait for initialization to complete, then receive video chunks and send to media player

            while (true)
            {
                // TODO: Check if keyboard key is clicked to stop
                Console.WriteLine("Receiving...");
                Thread.Sleep(200);
            }

            // Not used right now:
            Console.WriteLine("Press [ENTER] to disconnect...");
            Console.ReadLine();
            swiftClient.Disconnect();
            Console.WriteLine("Swift::Disconnect");
        }

        private static void receiveCallback(IAsyncResult ar)
        {
            IPEndPoint ip = (IPEndPoint)ar.AsyncState;

            try
            {
                var bytes = _client.EndReceive(ar, ref ip);
                Console.WriteLine("Received {0} bytes", bytes.Length);
                // TODO: Resolve bytes to specific message after receiving data
                //Task.Run(() => MessageResolver.Resolve(result, handshake.Channel.ToByteArray()));
            }
            catch
            {
                Console.WriteLine("Peer {0} closed connection", ip);
            }
        }
    }
}
