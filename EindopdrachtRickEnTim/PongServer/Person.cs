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
        public List<Person> Friends;
        public SortedList<List<Person>, string> Chats;

        public Person(TcpClient client)
        {
            this.client = client;
            Friends = new List<Person>();
            Chats = new SortedList<List<Person>, string>();
            client.GetStream().BeginRead(buffer, 0, 1024, new AsyncCallback(OnPersonRead), this);
        }

        public Person(string name)
        {
            this.name = name;
            Friends = new List<Person>();
            Chats = new SortedList<List<Person>, string>();
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
                        setUsername(packet[1]); break;

                    case "addfriend":
                        addFriend(request); break;

                    case "data":
                        handleData(packet); break;

                    case "message":
                        handleMessage(request); break;

                    default:
                        Console.WriteLine("Unknown packet: " + packet[0]); break;
                }               
    
            }


            client.GetStream().BeginRead(buffer, 0, 1024, new AsyncCallback(OnPersonRead), this);
        }

        private void setUsername(string username)
        {
            if(username.Length > 2)
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

        private void addFriend(string friendname)
        {
            bool friendAdded = false;
            foreach (Person friend in PongServer.people) {
                if (friend.name.Equals(friendname))
                {
                    Friends.Add(friend);
                    Console.WriteLine(this.name + " added " + friendname + " as a friend");
                    Send("addfriend\r\nOK\r\n\r\n");
                    friendAdded = true;
                    break;
                }
            }
            if (!friendAdded)
            {
                Console.WriteLine(friendname + " does not exist");
                Send("addfriend\r\nError\r\n\r\n");
            }
        }

        private void handleMessage(string message)
        {
            Console.WriteLine($"message received");
            PongServer.SendToOthers(this, message + "\r\n\r\n");
        }

        private void handleData(string[] data)
        {            
            Console.WriteLine("data received");
            PongServer.SendToOthers(this, String.Join("\r\n", data) + "\r\n\r\n");
        }

        public void Send(String data)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(data);
            client.GetStream().Write(byteData, 0, byteData.Length);
        }
    }
}
