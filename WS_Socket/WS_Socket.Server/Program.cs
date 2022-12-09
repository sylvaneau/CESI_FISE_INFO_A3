using System.Net;
using System.Net.Sockets;

namespace WS_Socket.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket serverSocker = Connect();
        }

        private static Socket Connect()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 9000);
            
            Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endPoint);
            socket.Listen();

            return socket;
        }

        private static Socket AcceptConnection(Socket serverSocket)
        {
            Socket clientSocket = serverSocket.Accept();

            if (clientSocket.RemoteEndPoint is IPEndPoint clientEndpoint)
            {
                //IPEndPoint clientEndpoint = (IPEndPoint)clientSocket.RemoteEndPoint;
                Console.WriteLine("Client connected:" + clientEndpoint.Address.ToString() + ":" + clientEndpoint.Port);
            }

            return clientSocket;
        }
    }
}