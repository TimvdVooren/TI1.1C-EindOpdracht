using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Timers;

namespace PongClient
{
    class PongClient
    {
        static TcpClient client { get; set; }
        private static byte[] buffer = new byte[1024];
        private static string receive = "";
        private static int player;
        public PongClient()
        {
            client = new TcpClient();
            client.Connect("145.49.12.212", 1506);
            client.GetStream().BeginRead(buffer, 0, 1024, new AsyncCallback(OnRead), null);
            while (true)
            {
                SendPosition();
            }
        }

        private static void OnRead(IAsyncResult ar)
        {
            int rc = client.GetStream().EndRead(ar);
            receive += Encoding.UTF8.GetString(buffer, 0, rc);
            Console.WriteLine(receive + " receive");

            string[] packet = Regex.Split(receive, ",");

            if (packet.Length <= 0)
            {
                Console.WriteLine("error");
            }
            else if (packet[0] == "position")
            {
                UpdatePosition(packet);
            }
            else if(packet[0] == "player")
            {
                player = int.Parse(packet[1]);
            }
            else
                Console.WriteLine("Unknown packet: " + packet[0]);

        }

        private static void UpdatePosition(string[] data)
        {
            
            Console.WriteLine("new position" + data[1] + "is" + data[2] + "," + data[3] );            

        }

        private static void SendPosition()
        {
            string postition = Console.ReadLine();
            send("position," + player + "," + postition + "," + postition);
        }

        public static void send(String data)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(data);
            client.GetStream().Write(byteData, 0, byteData.Length);

        }
    }
}
