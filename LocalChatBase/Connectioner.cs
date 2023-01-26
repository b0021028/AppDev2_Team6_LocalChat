using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LocalChatBase
{
    public class Connectioner
    {

        public event EventHandler<Session> EvStartSession = (sender, args) => { };

        public static void StartListen()
        {

        }

        public static Session CreateSession(IPAddress ip, int port)
        {
            return new Session(new System.Net.Sockets.TcpClient(ip.ToString(), port));
        }


        public static void StopListen()
        {

        }
    }
}
