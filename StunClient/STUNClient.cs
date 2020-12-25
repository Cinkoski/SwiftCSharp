using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace STUN
{
    /// <summary>
    /// Implements a RFC3489 STUN client.
    /// </summary>
    public static class STUNClient
    {
        /// <summary>
        /// Period of time in miliseconds to wait for server response.
        /// </summary>
        public static int ReceiveTimeout = 5000;

        /// <param name="server">Server address</param>
        /// <param name="queryType">Query type</param>
        /// <param name="closeSocket">
        /// Set to true if created socket should closed after the query
        /// else <see cref="STUNQueryResult.Socket"/> will leave open and can be used.
        /// </param>
        public static Task<STUNQueryResult> QueryAsync(IPEndPoint server, STUNQueryType queryType, bool closeSocket)
        {
            return Task.Run(() => Query(server, queryType, closeSocket));
        }

        /// <param name="server">Server address</param>
        /// <param name="queryType">Query type</param>
        /// <param name="closeSocket">
        /// Set to true if created socket should closed after the query
        /// else <see cref="STUNQueryResult.Socket"/> will leave open and can be used.
        /// </param>
        public static STUNQueryResult Query(IPEndPoint server, STUNQueryType queryType, bool closeSocket)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint bindEndPoint = new IPEndPoint(IPAddress.Any, 0);
            socket.Bind(bindEndPoint);

            var result = STUNRfc5780.Query(socket, server, queryType, ReceiveTimeout);

            if (closeSocket)
            {
                socket.Dispose();
                result.Socket = null;
            }

            return result;
        }
    }
}