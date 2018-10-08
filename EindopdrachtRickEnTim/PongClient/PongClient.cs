using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;


namespace PongClient
{
    class PongClient
    {
        private static byte[] buffer = new byte[1024];
        private static string totalBuffer = "";
        static TcpClient client;
        string clientName;
        public PongClient()
        {
            client = new TcpClient();
            client.Connect("192.168.1.139", 1506);
            string userName = Console.ReadLine();
            clientName = userName;
            send($"username\r\n{userName}\r\n\r\n");

            client.GetStream().BeginRead(buffer, 0, 1024, new AsyncCallback(OnClientRead), null);
            Console.ReadKey();

        }

        private static void OnClientRead(IAsyncResult ar)
        {
            int rc = client.GetStream().EndRead(ar);
            totalBuffer += Encoding.UTF8.GetString(buffer, 0, rc);

            if (totalBuffer.Contains("\r\n\r\n"))
            {
                String request = totalBuffer.Substring(0, totalBuffer.IndexOf("\r\n\r\n"));
                totalBuffer = totalBuffer.Substring(totalBuffer.IndexOf("\r\n\r\n") + 4);

                string[] packet = Regex.Split(request, "\r\n");

                if (packet.Length <= 0)
                {
                    Console.WriteLine("Protocol error");
                }

                if (packet[0] == "username")
                {
                    setUserName(packet[1]);
                }                
                else
                    Console.WriteLine("Unknown packet: " + packet[0]);


            }


            client.GetStream().BeginRead(buffer, 0, 1024, new AsyncCallback(OnClientRead), null);
        }        

        private static void setUserName(string status)
        {
            if (status == "error")
            {
                Console.WriteLine("Error logging in, wrong username");
                return;
            }
            else if (status == "OK")
            {
                Console.WriteLine("Login ok, sending data...");
                Timer t = new Timer();
                t.Interval = 1000;
                t.Elapsed += (sender, args) =>
                {
                    send("data\r\n0\r\n1\r\n4\r\n\r\n");
                };
                t.Start();
            }
        }



        public static void send(String data)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(data);
            client.GetStream().Write(byteData, 0, byteData.Length);

        }
    }
}
