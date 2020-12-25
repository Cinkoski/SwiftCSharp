using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var swiftClient = new Swift();
            swiftClient.Init();

            var swarmId = new ProgramGuide().GetAllChannels().List.First().ChannelList.First().SwarmId; // first category -> first channel; for testing only
            swiftClient.Connect(swarmId);
            swiftClient.Disconnect();

            Console.ReadLine();
        }
    }
}
