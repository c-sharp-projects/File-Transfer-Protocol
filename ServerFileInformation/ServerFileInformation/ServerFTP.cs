using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerFileInformation
{
    class ServerFTP
    {
       
        public Connection Cobj;
        public Thread tobj;
        public FileInformation fobj;
        

        public object Tcpclient { get; private set; }

        public ServerFTP()
        {
            Cobj = new Connection();
            Cobj.GetConnection();
            fobj = new FileInformation();
        }

        public void SendData(string data)
        {
            ASCIIEncoding asen = new ASCIIEncoding();
            Cobj.mySocket.Send(asen.GetBytes(data));

        }

        public string GetData()
        {
            string data = null;

            byte[] b = new byte[Cobj.mySocket.ReceiveBufferSize];

            int iSize = Cobj.mySocket.Receive(b);

            for (int i = 0; i < iSize; i++)
            {
                data += Convert.ToChar(b[i]);
            }

            return data;

        }


        public void Communication()
        {
            while (Cobj.mySocket.Connected)
            {
                
                try
                    {
                       Console.WriteLine("Marvellous Web : Connection Established with client....");
               
                        string data = GetData();
                        string[] code = data.Split('_');

                    switch (Convert.ToInt32(code[0]))
                    {
                        case 1:
                            string info = fobj.IsFileExists(code[1]);
                            SendData(info);
                            break;

                        case 2:
                            string files = fobj.ListAllFiles();
                            SendData(files);
                            break;

                        case 3:

                            Cobj.mySocket.Disconnect(true);
                            Console.WriteLine("Connection has been disconnected");
                           
                            break;

                        case 4:
                            string path = fobj.GetPath();
                            SendData(path);
                            break;

                        case 5:

                           SendData( fobj.SendFile(code[1]));


                            break;



                    }

                       

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Marvellous Web : Exception - " + e.StackTrace);
                    }

              

        }
            /*  finally
              {
                  Console.WriteLine("\nMarvellous Web : Deallocating all resources ...");
                  if (Cobj.mySocket != null)
                  {
                  Cobj.mySocket.Close();
                  }
                  if (Cobj.myListener != null)
                  {
                  Cobj.myListener.Stop();
                  }
              }
              */
        }



    }

}
