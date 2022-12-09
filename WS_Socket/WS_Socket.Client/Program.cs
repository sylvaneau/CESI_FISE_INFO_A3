using System.Net.Sockets;
using System.Net;
using System.Text;

namespace WS_Socket.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket clientSocket = Connect();

            Listen(clientSocket);

            Disconnect(clientSocket);

            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }

        private static Socket Connect()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 9000);

            Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(endPoint);

            return socket;
        }

        private static void Listen(Socket clientSocket)
        {
            while (true)
            {
                Console.WriteLine("Your message: ");
                string message = Console.ReadLine();

                clientSocket.Send(Encoding.UTF8.GetBytes(message));

                if (message == "EXIT")
                {
                    break;
                }

                byte[] buffer = new byte[1024];
                int bytesReceived = clientSocket.Receive(buffer);

                string response = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                Console.WriteLine("Server says: " + response);
            }
        }

        private static void Disconnect(Socket clientSocket)
        {
            clientSocket.Close();
        }
    }
}