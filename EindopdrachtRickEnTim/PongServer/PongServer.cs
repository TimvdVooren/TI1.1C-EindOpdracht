using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PongServer
{
    class PongServer
    {
        private int PlayerCount = 0;

        public PongServer()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 1506);
            listener.Start();

            while (PlayerCount < 2)
            {
                TcpClient client = listener.AcceptTcpClient();
                PlayerCount++;

                Thread thread = new Thread(HandleClient(client));
                thread.Start();
            }
        }

        private ThreadStart HandleClient(TcpClient client)
        {
            Console.WriteLine("Client connected");
            while (true)
            {

            }
        }
    }
}
