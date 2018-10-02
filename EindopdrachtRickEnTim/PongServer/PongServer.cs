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
        private static int PlayerCount = 0;
        private static TcpListener listener;
        private static List<Player> players = new List<Player>();

        public PongServer()
        {
            listener = new TcpListener( IPAddress.Any,1506);
            listener.Start();
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
            while (true) ;

        }

        private static void OnConnect(IAsyncResult ar)
        {
            if (PlayerCount < 2)
            {
                TcpClient client = listener.EndAcceptTcpClient(ar);
                PlayerCount++;

                players.Add(new Player(client, PlayerCount));

                listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);

            }
        }

        public static void SendToAll(string data)
        {
            foreach(Player p in players)
            {
                p.Send(data);
            }
        }

        
    }
}
