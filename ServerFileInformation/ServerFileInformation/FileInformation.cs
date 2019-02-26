using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerFileInformation
{
    class FileInformation
    {
        public FileInformation()
        {
           
        }

        public string ListAllFiles()
        {
            string[] files = null;
            string file = null;

            files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory+@"\Upload","*");

            

            foreach (string f in files)
            {
                file += Path.GetFileName(f);
                file += "_";
            }

            return file;
        }

        public string IsFileExists(string FileName)
        {
            string sRet = null;

            string path = AppDomain.CurrentDomain.BaseDirectory + @"\Upload";
            FileName = path +@"\"+FileName;

            if (File.Exists(FileName))
            {
                sRet = "File is available on server";
            }
            else
            {
                sRet = "File is not available on server";
            }

            return sRet;
        }

        public string GetPath()
        {
            string path1 = @"\\" + Connection.GetLocalIPAddress() +@"\"+ AppDomain.CurrentDomain.BaseDirectory + @"\Upload";
            string path2 = path1.Replace(":", "$");

            return path2; 
        }

        public string SendFile(string filename)
        {

            
            Console.WriteLine(filename);


            if (!File.Exists(GetPath() +"\\"+ Path.GetFileName(filename)))
            {
                return "Invalid FileName";
            }

            File.Copy(GetPath() + "\\" + Path.GetFileName(filename),filename,true);


            return "File successfully Downloaded";
           
    
        }
    }
}
