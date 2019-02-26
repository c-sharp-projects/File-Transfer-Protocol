using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerFileInformation
{
    class Connection
    {
        private int Port;
        private string IP;
        public TcpListener myListener;
        public TcpClient Tcpclient;
        public Socket mySocket;


        public Connection()
        {         
            Port = 0;
            IP = null;
            myListener = null;
            Tcpclient = null;
        }

        public static string GetLocalIPAddress()
        {
            Console.WriteLine("Marvellous Web : Host name - {0}", Dns.GetHostName());

            var Marvelloushost = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in Marvelloushost.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Marvellous Web :No network adapters with an IPv4 address in the system!");
        }

        public void GetConnection()
        {
            IP = GetLocalIPAddress();
            Port = 21000;

            IPAddress ipAd = IPAddress.Parse(IP);
            Console.WriteLine(" Server started ... ");

            myListener = new TcpListener(ipAd, Port);
            myListener.Start();
            mySocket = myListener.AcceptSocket();


        }

    }
}
