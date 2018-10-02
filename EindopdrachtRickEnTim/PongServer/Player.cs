using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PongServer
{
    class Player
    {
        public TcpClient client { get; set; }
        private byte[] buffer = new byte[1024];
        private string receive = "";
        private int player;


        public Player(TcpClient client, int player)
        {
            this.client = client;
            this.player = player;
            Send("player," + player);
            client.GetStream().BeginRead(buffer, 0, 1024, new AsyncCallback(OnRead), this);
        }

        private void OnRead(IAsyncResult ar)
        {
            int rc = client.GetStream().EndRead(ar);
            receive += Encoding.UTF8.GetString(buffer, 0, rc);
            Console.WriteLine(receive +  " receive");

            string[] packet = Regex.Split(receive, ",");

            if (packet.Length <= 0)
            {
                Console.WriteLine("error");
            }
            else if (packet[0] == "position")
            {
                UpdatePosition(receive);
            }           
            else
                Console.WriteLine("Unknown packet: " + packet[0]);
            
        }

        private void UpdatePosition(string data)
        {
            PongServer.SendToAll(data);
        }

        public void Send(String data)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(data);
            client.GetStream().Write(byteData, 0, byteData.Length);
        }
    }
}
