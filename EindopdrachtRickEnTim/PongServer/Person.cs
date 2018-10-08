using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PongServer
{
    class Person
    {
        private byte[] buffer = new byte[1024];
        private string totalBuffer = "";
        public TcpClient client { get; set; }
        public string name;
        public Person(TcpClient client)
        {
            this.client = client;

            client.GetStream().BeginRead(buffer, 0, 1024, new AsyncCallback(OnPersonRead), this);

        }

        private void OnPersonRead(IAsyncResult ar)
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
                    setUsername(packet[1]);
                }
                else if (packet[0] == "data")
                    handleData(packet);
                else
                    Console.WriteLine("Unknown packet: " + packet[0]);
            }


            client.GetStream().BeginRead(buffer, 0, 1024, new AsyncCallback(OnPersonRead), this);
        }

        private void setUsername(string username)
        {
            if(username.Length > 4)
            {
                name = username;
                Console.WriteLine("new Person:" + name);
                Send("username\r\nOK\r\n\r\n");
            }
            else
            {
                Send("username\r\nError\r\n\r\n");
            }
        }

        private void handleData(string[] data)
        {
            
            Console.WriteLine("data received");
            PongServer.BroadcastExcept(this, String.Join("\r\n", data) + "\r\n\r\n");
        }

        public void Send(String data)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(data);
            client.GetStream().Write(byteData, 0, byteData.Length);
        }
    }
}
