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
        static List<Person> people = new List<Person>();

        public PongServer()
        {
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
    }
}
