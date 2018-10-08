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
        static TcpListener server;
        static List<Player> users = new List<Player>();
        static int PlayerCount = 0;

        public PongServer()
        {
            server = new TcpListener(1234);
            server.Start();
            server.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);


            Console.ReadKey();
        }

        private static void OnConnect(IAsyncResult ar)
        {
            PlayerCount++;
            TcpClient client = server.EndAcceptTcpClient(ar);
            users.Add(new Player(client, PlayerCount));

            server.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        public static void Broadcast(string data)
        {
            foreach (Player user in users)
                user.Send(data);
        }
        public static void BroadcastExcept(Player user, string data)
        {
            foreach (Player u in users.Where(u => u != user))
                u.Send(data);
        }


    }
}
