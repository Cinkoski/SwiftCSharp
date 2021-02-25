using System;
using System.Net;

namespace SwiftCSharp
{
    public class Swift
    {
        private string _swarmId;
        private string _swarmHash;
        public Tracker SwiftTracker;

        public void Init()
        {
            var stunResult = new StunClient().GetAddress();
            //SwiftTracker = new Tracker(stunResult.PublicEndPoint, stunResult.LocalEndPoint);
            SwiftTracker = new Tracker(new IPEndPoint(IPAddress.Parse("46.151.136.158"), 55658), new IPEndPoint(IPAddress.Parse("192.168.1.123"), 55658));
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