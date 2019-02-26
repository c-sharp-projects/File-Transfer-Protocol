using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientFileInformation
{
    class Connection
    {
        private string IP;
        private int Port;
        public TcpClient Tcpclient;
        public Stream stream;
        

        public Connection()
        {
            IP = GetLocalIPAddress();
            Port = 21000;
        }

        public static string GetLocalIPAddress()
        {
            
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public void GetConnection()
        {
            try
            {
                Tcpclient = new TcpClient();
                Tcpclient.Connect(IP, Port);
                stream = Tcpclient.GetStream();
            }
            catch (Exception e)
            {
                throw new ClientException(e.Message);
            }
        }

        public void ReleaseConnection()
        {
            if (Tcpclient != null)
            {
                Tcpclient.Close();
            }
            if (stream != null)
            {
                stream.Close();
            }
        }
    }

   
}
