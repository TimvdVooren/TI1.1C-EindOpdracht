using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PongServer
{
    class Server
    {
        private static TcpListener server;
        private static List<Person> people = new List<Person>();
        public static List<string> usernames = new List<string>();
        public static SortedList<Tuple<string, string>, string> chats = new SortedList<Tuple<string, string>, string>();

        public Server()
        {
            ReadChatsFromFile();
            server = new TcpListener(IPAddress.Any, 1506);
            server.Start();
            server.BeginAcceptTcpClient(new AsyncCallback(OnPersonConnect), null);

            Console.ReadKey();
        }

        private static void OnPersonConnect(IAsyncResult ar)
        {
            TcpClient client = server.EndAcceptTcpClient(ar);
            people.Add(new Person(client));
            server.BeginAcceptTcpClient(new AsyncCallback(OnPersonConnect), null);
        }

        public static void SendToPerson(string personName, string data)
        {
            foreach (Person person in people)
                if(person.name == personName)
                    person.Send(data);
        }

        public static void SendToAll(string data)
        {
            foreach (Person person in people)
                person.Send(data);
        }

        public static void SendToOthers(Person user, string data)
        {
            foreach (Person u in people.Where(u => u != user))
                u.Send(data);
        }

        public static void WriteChatsToFile()
        {
            string chatSave = "";
            foreach(string chat in chats.Values)
            {
                int index = chats.IndexOfValue(chat);
                chatSave = chatSave + JsonConvert.SerializeObject(new
                {
                    user1 = chats.Keys[index].Item1,
                    user2 = chats.Keys[index].Item2,
                    chat = chat
                }) + "|";
            }
            File.WriteAllText("ChatSave.txt", chatSave);
        }

        private void ReadChatsFromFile()
        {
            try
            {
                string chatSave = File.ReadAllText("ChatSave.txt");
                string[] savedChats = chatSave.Split('|');
                for (int i = 0; i < savedChats.Length - 1; i++)
                {
                    dynamic data = JsonConvert.DeserializeObject(savedChats[i]);
                    string user1 = data.user1;
                    string user2 = data.user2;
                    Tuple<string, string> users = new Tuple<string, string>(user1, user2);
                    string chat = data.chat;
                    chats.Add(users, chat);
                }
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine("Chat file was not found");
            }
        }
    }
}
