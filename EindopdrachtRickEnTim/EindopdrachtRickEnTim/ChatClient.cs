using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;


namespace EindopdrachtRickEnTim
{
    public class ChatClient
    {
        private static byte[] buffer = new byte[1024];
        private static string totalBuffer = "";
        private static TcpClient client;
        private string Username;
        public string lastMessage { get; set; }

        public ChatClient()
        {
            client = new TcpClient();
            client.Connect("localhost", 1506);
            lastMessage = "";
            client.GetStream().BeginRead(buffer, 0, 1024, new AsyncCallback(OnClientRead), null);
        }

        private void OnClientRead(IAsyncResult ar)
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

                switch (packet[0])
                {
                    case "username":
                        AcceptUserName(packet[1]); break;                   

                    case "message":
                        HandleMessage(packet[1], packet[2]); break;

                    default:
                        Console.WriteLine("Unknown packet: " + packet[0]); break;
                }


            }
            client.GetStream().BeginRead(buffer, 0, 1024, new AsyncCallback(OnClientRead), null);
        }        

        private void AcceptUserName(string status)
        {
            if (status == "Error")
            {
                Console.WriteLine("Your username is too short");
                return;
            }
            else if (status == "OK")
            {
                Console.WriteLine("Login ok, sending data...");
                Timer t = new Timer();
                t.Interval = 1000;
                t.Elapsed += (sender, args) =>
                {
                    Send("data\r\n0\r\n1\r\n4\r\n\r\n");
                };
                t.Start();
            }
        }

        public void HandleMessage(string message, string otherUser)
        {
            lastMessage = message + "\r\n" + otherUser;
        }

        public void SendUserName(string Username)
        {
            this.Username = Username;
            Send($"username\r\n{Username}\r\n\r\n");
        }

        public void AddFriend(string FriendName)
        {
            Send($"addfriend\r\n{FriendName}\r\n\r\n");
        }

        public void SendMessage(string message)
        {
            Send($"message\r\n{message}\r\n{Username}\r\n\r\n");
        }

        public static void Send(String data)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(data);
            client.GetStream().Write(byteData, 0, byteData.Length);
        }
    }
}
