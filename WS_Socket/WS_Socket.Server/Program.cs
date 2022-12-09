using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WS_Socket.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using Socket serverSocket = Connect();

            Socket clientSocket = AcceptConnection(serverSocket);
            Listen(clientSocket);

            Disconnect(clientSocket);

            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
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

        private static void Listen(Socket clientSocket)
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                int bytesReceived = clientSocket.Receive(buffer);

                string message = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                Console.WriteLine("Client says: " + message);

                if (message == "EXIT")
                {
                    break;
                }

                Console.WriteLine("Your answer: ");
                string response = Console.ReadLine();

                clientSocket.Send(Encoding.UTF8.GetBytes(response));
            }
        }

        private static void Disconnect(Socket clientSocket)
        {
            clientSocket.Close();
            clientSocket.Dispose();
        }
    }
}