﻿using Newtonsoft.Json;
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

                switch(packet[0])
                {
                    case "username":
                        SetUsername(packet[1]); break;

                    //case "addfriend":
                    //    addFriend(request); break;

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
                Send("username\r\nError\r\n\r\n");
            }
        }

        //private void addFriend(string friendname)
        //{
        //    bool friendAdded = false;
        //    foreach (Person friend in Server.people) {
        //        if (friend.name.Equals(friendname))
        //        {
        //            Friends.Add(friend);
        //            Console.WriteLine(this.name + " added " + friendname + " as a friend");
        //            Send("addfriend\r\nOK\r\n\r\n");
        //            friendAdded = true;
        //            break;
        //        }
        //    }
        //    if (!friendAdded)
        //    {
        //        Console.WriteLine(friendname + " does not exist");
        //        Send("addfriend\r\nError\r\n\r\n");
        //    }
        //}

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
                string chat = Server.chats[conversation1];
                Server.chats[conversation1] = Server.chats[conversation1] + "\r\n" + message;
                chat = Server.chats[conversation1];
            }
            else if (Server.chats.Keys.Contains(conversation2))
            {
                Server.chats[conversation2] = Server.chats[conversation2] + "\r\n" + message;
            }
            //Server.SendToPerson(receiver, $"message\r\n{message}\r\n\r\n");
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
