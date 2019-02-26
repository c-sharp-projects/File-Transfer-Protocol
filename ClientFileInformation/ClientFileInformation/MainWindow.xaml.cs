using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ClientFileInformation
{

    class ClientException : Exception
    {
        public ClientException(String exception)
        {
            MessageBox.Show(exception);
        }
    }


    public partial class MainWindow : Window
    {
        
        private Connection Cobj;
       

        public MainWindow()
        {
            InitializeComponent();
            connect.IsEnabled = true;
            disconnect.IsEnabled = false;
            Cobj = new Connection();
        }
      

        public void SendData(String data)
        {
            ASCIIEncoding asen = new ASCIIEncoding();

            byte[] b = asen.GetBytes(data);

            Cobj.stream.Write(b, 0, b.Length);
        }


        public String GetData()
        {
            string data = null;

            byte[] b = new byte[Cobj.Tcpclient.ReceiveBufferSize];

            int iSize = Cobj.stream.Read(b, 0, Cobj.Tcpclient.ReceiveBufferSize);

            for (int i = 0; i < iSize; i++)
            {
                data += Convert.ToChar(b[i]);
            }

            return data;
        }

        public string GetPath()
        {
            string path1 = @"\\" + Connection.GetLocalIPAddress() + @"\" + AppDomain.CurrentDomain.BaseDirectory + @"\Download\";
            string path2 = path1.Replace(":", "$");

            return path2;
        }


        private void Listener(object sender, RoutedEventArgs e)
        {
           
            Button btn = (Button)sender;
            string code = null;

            if (listview.Items.Count > 0)
            {
                listview.Items.Clear();
            }

            try
            {
                switch(btn.Uid)
                {
                    case "submit":
                        code = "1_";
                        String FileName = code;
                        
                        FileName += FileNameTextBox.Text;

                            if (String.IsNullOrWhiteSpace(FileNameTextBox.Text))
                            {
                               MessageBox.Show("Please Enter FileName !");
                            return;        
                            }
                        FileNameTextBox.Text = "";
                        SendData(FileName);

                            listview.Items.Add(GetData());
                        
                    break;

                    case "listfiles":
                        string data = null;
                        string[] info = null; 
                        code = "2_";
                        SendData(code);
                        
                        data = GetData();
                        info = data.Split('_');

                        foreach (string s in info)
                        {
                            listview.Items.Add(s);
                        }

                        break;

                    case "connect":
                        connect.IsEnabled = false;
                        disconnect.IsEnabled = true;
                        Cobj.GetConnection();
                        break;

                    case "disconnect":
                        code = "3_";
                        SendData(code);
                        connect.IsEnabled = true;
                        disconnect.IsEnabled = false;
                        Cobj.ReleaseConnection();
                        break;

                    case "upload":
                        data = null;
                        code = "4_";
                        SendData(code);
                        data = GetData();

                        if (String.IsNullOrWhiteSpace(FileNameTextBox.Text))
                        {
                            MessageBox.Show("Please Enter FileName !");
                            return;
                        }

                        string fname = FileNameTextBox.Text;
                        FileNameTextBox.Text = "";

                        data += @"\"+fname;

                        File.Copy(fname, data,true);

                        MessageBox.Show("File Uploaded Successfully");

                        break;

                    case "download":
                        data = null;
                        code = "5_";
                        string dir = "Download";
                        if (String.IsNullOrWhiteSpace(FileNameTextBox.Text))
                        {
                            MessageBox.Show("Please Enter FileName !");
                            return;
                        }

                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }

                        fname = FileNameTextBox.Text;
                        FileNameTextBox.Text = "";
                        SendData(code + GetPath() + fname);
                        MessageBox.Show(GetData());
                        
                        break;


                }
            }
            catch (Exception exp)
            {
                throw new ClientException(exp.Message);
            }
        }

       
    }
 }

