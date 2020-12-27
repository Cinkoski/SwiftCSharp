using SwiftCSharp.PPSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize
            Console.WriteLine("Swift::Initialize");
            Swift swiftClient = new Swift();
            swiftClient.Init();

            // Get info - swarm id and peers
            Console.WriteLine("Tracker::Announce");
            // TODO: When iterating peers exclude own address, it might be on the peers list
            string swarmId = new ProgramGuide().GetAllChannels().List.First().ChannelList.First().SwarmId; // first category -> first channel; for testing only
            Console.WriteLine($"SwarmID: {swarmId}");

            List<PeerInfoModel> peersList = swiftClient.Connect(swarmId).Container.SwarmResult.First().PeerGroup.PeerInfo;

            foreach (var peerInfo in peersList)
            {
                IPEndPoint peerAddress = new IPEndPoint(IPAddress.Parse(peerInfo.PeerAddr.IpAddress.Address), peerInfo.PeerAddr.Port);
                Console.WriteLine($"Testing PeerAddr: {peerAddress}");

                byte[] handshakeResult = new PpspClient(swarmId).SendHandshake(peerAddress, swiftClient.SwiftTracker.LocalAddress.Port);

                if (handshakeResult == null)
                {
                    Console.WriteLine("Peer not connected");
                } else
                {
                    Console.WriteLine("Peer is connected");
                }
            }

            // Send messages to peer and disconnect from tracker
            Console.WriteLine("Swift::Disconnect");
            swiftClient.Disconnect();

            Console.ReadLine();
        }
    }
}
