using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PongClient
{
    class PongClient
    {
        public PongClient()
        {
            TcpClient client = new TcpClient();
            client.Connect("145.49.50.116", 1506);

            while (true)
            {

            }
        }
    }
}
