using ServerFileInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

 class Program
 {

    public static void Main(string[] args)
    {
        ServerFTP obj = new ServerFTP();
        obj.Communication();

    }
}

