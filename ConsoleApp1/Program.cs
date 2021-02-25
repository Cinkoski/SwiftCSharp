using SwiftCSharp.PPSP;
using SwiftCSharp.PPSP.Message;
using SwiftCSharp.PPSP.Message.Properties;
using SwiftCSharp.PPSP.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace SwiftCSharp
{
    class Program
    {
        private static UdpClient _client;
        private static Dictionary<IPEndPoint, Peer> _peers;

        static void Main(string[] args)
        {
            _peers = new Dictionary<IPEndPoint, Peer>();

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
            string swarmId = "0d4734f37644758ea1803caaa4152ffed3fa58da6ab252e13c0d39be95a358c2d0e0622b0a17fb669c2eda3d783cb3d9e7768c974b3fdcca07e346548111d7d4f9"; // for testing only
            Console.WriteLine($"SwarmID: {swarmId}");

            // Connect to Tracker
            Console.WriteLine("Tracker::Announce");

            IEnumerable<PeerInfoModel> peersList = swiftClient.Connect(swarmId).Container.SwarmResult.First().PeerGroup.PeerInfo
                .Where(p => p.PeerAddr.Type == PeerAddrType.REFLEXIVE && p.PeerAddr.IpAddress.Address != swiftClient.SwiftTracker.RemoteAddress.Address.ToString());

            //swiftClient.Disconnect();
            //return;

            // Send HANDSHAKE to peers

            _client = new UdpClient(swiftClient.SwiftTracker.LocalAddress.Port);

            foreach (var peerInfo in peersList)
            {
                IPEndPoint peerAddress = new IPEndPoint(IPAddress.Parse(peerInfo.PeerAddr.IpAddress.Address), peerInfo.PeerAddr.Port);
                Console.WriteLine($"Sending to PeerAddr: {peerAddress}");

                // TODO: Move to SomeClient.cs
                var handshake = new Handshake(swarmId);
                var handshakeBytes = handshake.ToByteArray();
                var peer = new Peer()
                {
                    ReceivingChannel = handshake.SrcChannelId
                };
                _peers.Add(peerAddress, peer);
                _client.Send(handshakeBytes, handshakeBytes.Length, peerAddress);
                _client.BeginReceive(receiveCallback, peerAddress);
            }

            // TODO: Wait for initialization to complete, then receive video chunks and send to media player

            while (true)
            {
                // TODO: Check if keyboard key is clicked to stop or run in other thread
                //Console.WriteLine("Receiving...");
                //Thread.Sleep(200);
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
                handleIncomingMessage(bytes, ip);
            }
            catch
            {
                //Console.WriteLine("Peer {0} closed connection", ip);
            }
        }

        private static void handleIncomingMessage(byte[] data, IPEndPoint peerAddress)
        {
            if (!_peers.ContainsKey(peerAddress))
            {
                //Console.WriteLine("Peer not found in list");
                return;
            }

            //Console.WriteLine("Received {0} bytes", data.Length);

            if (_peers[peerAddress].ReceivingChannel.ToInt() != BitConverter.ToInt32(data.Take(4).ToArray(), 0))
            {
                //Console.WriteLine("Channels are not equal, removing peer from list");
                _peers.Remove(peerAddress);
                return;
            }

            // TODO: One datagram may contain more than one message type!
            parseIncomingData(data, peerAddress);
        }

        private static void parseIncomingData(byte[] data, IPEndPoint peerAddress)
        {
            using (MemoryStream ms = new MemoryStream(data))
            using (BinaryReader br = new BinaryReader(ms))
            {
                var destChannelId = new ChannelId(br.ReadBytes(4));

                while (ms.Length > ms.Position)
                {
                    var msgType = (MessageTypes)br.ReadByte();

                    Console.WriteLine("Incoming {0} from: {1}", msgType, peerAddress);

                    switch (msgType)
                    {
                        case MessageTypes.HANDSHAKE:
                            handleIncomingHandshake(br, peerAddress);
                            break;
                        case MessageTypes.HAVE:
                            handleIncomingHave(br, peerAddress, destChannelId);
                            break;
                        /*case MessageTypes.DATA:
                            handleIncomingData(br, peerAddress);
                            break;
                        case MessageTypes.PEX_RES4:
                            new PexRes4().Decode(br);
                            break;
                        case MessageTypes.INTEGRITY:
                            new Integrity().Decode(br);
                            break;
                        case MessageTypes.SIGNED_INTEGRITY:
                            new SignedIntegrity().Decode(br);
                            break;*/
                        default:
                            Console.WriteLine("Unhandled msg type: {0} !!!", msgType);
                            break;
                    }
                }
            }
        }

        private static int _liveDiscardWindow;

        private static void handleIncomingHandshake(BinaryReader br, IPEndPoint peerAddress)
        {
            var handshake = new Handshake();
            handshake.Decode(br);

            _liveDiscardWindow = handshake.GetOption<LiveDiscardWindow>().GetRawValue();

            _peers[peerAddress].SendingChannel = handshake.SrcChannelId;

            var returnMessage = new PexReq(_peers[peerAddress].SendingChannel).ToByteArray();
            _client.Send(returnMessage, returnMessage.Length, peerAddress);
        }


        private static ChannelId _sendingChannel;
        private static ChannelId _incomingChannel;

        private static void handleIncomingHave(BinaryReader br, IPEndPoint peerAddress, ChannelId incomingChannel)
        {
            //successfull connection

            var message = new Have();
            message.Decode(br);

            Console.WriteLine($"HAVE Chunk Bin Value: {message.BinValue}");

            if (_incomingChannel == null)
                _incomingChannel = incomingChannel;
            else if (_incomingChannel != incomingChannel)
                return;

            long baseRight = BinUtils.BaseRight(message.BinValue);

            if (baseRight < _liveDiscardWindow)
            {
                Console.WriteLine("Bin smaller than live discard window");
                return;
            }
            else
            {
                long binOffset = BinUtils.LayerOffset(baseRight);

                Console.WriteLine($"HAVE Chunk Most Right: {baseRight}");
                Console.WriteLine($"HAVE Chunk Layer Offset: {binOffset}");

                long temp = binOffset - _liveDiscardWindow + 1;
                long newBin = BinUtils.OffsetToBin(0, temp);
                long newChunk = BinUtils.LayerOffset(newBin);

                Console.WriteLine($"NEW Computed BIN: {newBin}");
                Console.WriteLine($"NEW Computed CHUNK: {newChunk}");
            }

            Environment.Exit(0);

            var returnMessage = new Request();
            returnMessage.DestChannelId = _sendingChannel = _peers[peerAddress].SendingChannel;
            returnMessage.BinValue = message.BinValue;

            var returnMessageBytes = returnMessage.ToByteArray();
            _client.Send(returnMessageBytes, returnMessageBytes.Length, peerAddress);
        }

        private static void handleIncomingData(BinaryReader br, IPEndPoint peerAddress)
        {
            var data = new Data();
            data.Decode(br);

            Console.WriteLine("DATA Size: {0} Chunk: {1}", data.DataBytes.Length, data.BinValue);

            var haveMessage = new Have();
            haveMessage.DestChannelId = _sendingChannel;
            haveMessage.BinValue = data.BinValue;

            var ackMessage = new Ack();
            ackMessage.BinValue = data.BinValue;

            var requestMessage = new Request();
            requestMessage.BinValue = data.BinValue + 40;

            var returnBytes = new List<byte>();
            returnBytes.AddRange(haveMessage.ToByteArray());
            returnBytes.AddRange(requestMessage.ToByteArray());

            _client.Send(returnBytes.ToArray(), returnBytes.Count, peerAddress);
        }
    }
}
