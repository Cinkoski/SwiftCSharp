using SwiftCSharp.PPSP;
using SwiftCSharp.PPSP.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
            string swarmId = "0d4734f37644758ea1803caaa4152ffed3fa58da6ab252e13c0d39be95a358c2d0e0622b0a17fb669c2eda3d783cb3d9e7768c974b3fdcca07e346548111d7d4f9"; //new ProgramGuide().GetAllChannels().List.First().ChannelList.First().SwarmId; // first category -> first channel; for testing only
            Console.WriteLine($"SwarmID: {swarmId}");

            IEnumerable<PeerInfoModel> peersList = swiftClient.Connect(swarmId).Container.SwarmResult.First().PeerGroup.PeerInfo
                .Where(p => p.PeerAddr.Type == PeerAddrType.REFLEXIVE && p.PeerAddr.IpAddress.Address != swiftClient.SwiftTracker.RemoteAddress.Address.ToString());

            PpspClient ppspClient = new PpspClient();
            ppspClient.Initialize(swarmId, swiftClient.SwiftTracker.LocalAddress.Port);

            foreach (var peerInfo in peersList)
            {
                IPEndPoint peerAddress = new IPEndPoint(IPAddress.Parse(peerInfo.PeerAddr.IpAddress.Address), peerInfo.PeerAddr.Port);
                Console.WriteLine($"Testing PeerAddr: {peerAddress}");

                // TODO: Move to SomeClient.cs
                var handshake = new Handshake(swarmId);
                var result = Task.Run(() => ppspClient.SendHandshake(handshake, peerAddress));
                Task.Run(() => MessageResolver.Resolve(result, handshake.Channel.ToByteArray()));
            }

            // TODO: Wait for initialization to complete, then receive video chunks and send to media player

            Console.WriteLine("Press [ENTER] to disconnect...");
            Console.ReadLine();
            swiftClient.Disconnect();
            Console.WriteLine("Swift::Disconnect");
        }
    }
}
