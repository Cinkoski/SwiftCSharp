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
            Swift swiftClient = new Swift();
            swiftClient.Init();

            // Get info - swarm id and peers
            string swarmId = new ProgramGuide().GetAllChannels().List.First().ChannelList.First().SwarmId; // first category -> first channel; for testing only
            PPSPTrackerProtocol trackerResult = swiftClient.Connect(swarmId).Container;
            PeerAddrModel firstAvailablePeer = trackerResult.SwarmResult.First().PeerGroup.PeerInfo.First().PeerAddr; // result -> swarmresult is always only one -> first reflexive peer from group; for testing only
            IPEndPoint peerAddress = new IPEndPoint(IPAddress.Parse(firstAvailablePeer.IpAddress.Address), firstAvailablePeer.Port);

            // Send messages to peer and disconnect from tracker
            //new PpspClient(swarmId).SendHandshake();
            swiftClient.Disconnect();

            Console.ReadLine();
        }
    }
}
