using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace Server
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

                switch(packet[0])
                {
                    case "username":
                        SetUsername(packet[1]); break;

                    case "load_chat":
                        LoadChat(packet[1]); break;

                    case "data":
                        HandleData(packet); break;

                    case "message":
                        HandleMessage(packet[1], packet[2]); break;

                    default:
                        Console.WriteLine("Unknown packet: " + packet[0]); break;
                }               
    
            }
            
            client.GetStream().BeginRead(buffer, 0, 1024, new AsyncCallback(OnPersonRead), this);
        }

        private void SetUsername(string username)
        {
            if(username.Length > 2 && !Server.usernames.Contains(username))
            {
                name = username;
                Console.WriteLine("new Person:" + name);
                Send("username\r\nOK\r\n\r\n");
                Server.usernames.Add(name);
                Server.SendToAll($"people\r\n{JsonConvert.SerializeObject(Server.usernames)}\r\n\r\n");
            }
            else
            {
                Send("username\r\nERROR\r\n\r\n");
            }
        }

        private void LoadChat(string friendName)
        {
            Tuple<string, string> conversation1 = new Tuple<string, string>(name, friendName);
            Tuple<string, string> conversation2 = new Tuple<string, string>(friendName, name);
            
            if (Server.chats.Keys.Contains(conversation1))
            {
                Send($"load_chat\r\n{Server.chats[conversation1]}\r\n\r\n");
            }
            else if (Server.chats.Keys.Contains(conversation2))
            {
                Send($"load_chat\r\n{Server.chats[conversation2]}\r\n\r\n");
            }
            else
            {
                Server.chats.Add(conversation1, " ");
                Send($"load_chat\r\nNoChat\r\n\r\n");
            }
        }

        private void HandleMessage(string message, string receiver)
        {
            Console.WriteLine($"message received from {name} to {receiver}");
            Tuple<string, string> conversation1 = new Tuple<string, string>(name, receiver);
            Tuple<string, string> conversation2 = new Tuple<string, string>(receiver, name);

            message = name + ": " + message;
            if (Server.chats.Keys.Contains(conversation1))
            {
                Server.chats[conversation1] = Server.chats[conversation1] + "\r\n" + message;
                Server.SendToPerson(receiver, $"message\r\n{name}\r\n\r\n");
            }
            else if (Server.chats.Keys.Contains(conversation2))
            {
                Server.chats[conversation2] = Server.chats[conversation2] + "\r\n" + message;
                Server.SendToPerson(receiver, $"message\r\n{name}\r\n\r\n");
            }
            Server.WriteChatsToFile();
        }

        private void HandleData(string[] data)
        {            
            Console.WriteLine("data received");
            Server.SendToOthers(this, String.Join("\r\n", data) + "\r\n\r\n");
        }

        public void Send(String data)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(data);
            client.GetStream().Write(byteData, 0, byteData.Length);
        }
    }
}
