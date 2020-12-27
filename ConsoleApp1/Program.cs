using SwiftCSharp.PPSP;
using System;
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
            PPSPTrackerProtocol trackerResult = swiftClient.Connect(swarmId).Container;
            PeerAddrModel firstAvailablePeer = trackerResult.SwarmResult.First().PeerGroup.PeerInfo.Where(p => p.PeerAddr.IpAddress.Address != swiftClient.SwiftTracker.RemoteAddress.Address.ToString()).First().PeerAddr; // result -> swarmresult is always only one -> first reflexive peer from group; for testing only
            IPEndPoint peerAddress = new IPEndPoint(IPAddress.Parse(firstAvailablePeer.IpAddress.Address), firstAvailablePeer.Port);

            Console.WriteLine($"SwarmID: {swarmId}");
            Console.WriteLine($"PeerAddr: {peerAddress}");

            // Send messages to peer and disconnect from tracker
            byte[] handshakeResult = new PpspClient(swarmId).SendHandshake(peerAddress, swiftClient.SwiftTracker.LocalAddress.Port);
            Console.WriteLine($"HANDSHAKE Result: {Helper.ByteArrayToString(handshakeResult)}");
            Console.WriteLine("Swift::Disconnect");
            swiftClient.Disconnect();

            Console.ReadLine();
        }
    }
}
