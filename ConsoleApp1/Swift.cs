using System;

namespace ConsoleApp1
{
    public class Swift
    {
        private string _swarmId;
        private string _swarmHash;
        private Tracker _tracker;

        public void Init()
        {
            var stunAddresses = new StunClient().GetAddress();
            _tracker = new Tracker(stunAddresses.PublicEndPoint, stunAddresses.LocalEndPoint);
        }

        public void Connect(string swarmId)
        {
            if (_swarmId != null && _swarmId != "")
                throw new Exception("Client is already connected!");

            _swarmId = swarmId;
            _swarmHash = Helper.SwarmToHash(swarmId);

            _tracker.Join(_swarmHash);
        }

        public void Disconnect()
        {
            _tracker.Leave(_swarmHash);
            _swarmId = "";
            _swarmHash = "";
        }
    }
}