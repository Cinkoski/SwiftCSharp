using System;

namespace ConsoleApp1
{
    public class Swift
    {
        private string _swarmId;
        private string _swarmHash;
        public Tracker SwiftTracker;

        public void Init()
        {
            var stunAddresses = new StunClient().GetAddress();
            SwiftTracker = new Tracker(stunAddresses.PublicEndPoint, stunAddresses.LocalEndPoint);
        }

        public SwiftProtocol Connect(string swarmId)
        {
            if (_swarmId != null && _swarmId != "")
                throw new Exception("Client is already connected!");

            _swarmId = swarmId;
            _swarmHash = Helper.SwarmToHash(swarmId);

            return SwiftTracker.Join(_swarmHash);
        }

        public void Disconnect()
        {
            SwiftTracker.Leave(_swarmHash);
            _swarmId = "";
            _swarmHash = "";
        }
    }
}