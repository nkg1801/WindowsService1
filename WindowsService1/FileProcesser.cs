using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WindowsService1
{
    public class FileProcesser
    {
        FileSystemWatcher watcher;
        string directoryToWatch;
        public FileProcesser(string path)
        {
            this.watcher = new FileSystemWatcher();
            this.directoryToWatch = path;
            watcher.IncludeSubdirectories = true;
        }
        public void Watch()
        {
            watcher.Path = directoryToWatch;
            watcher.NotifyFilter = NotifyFilters.LastAccess |
                         NotifyFilters.LastWrite |
                         NotifyFilters.FileName |
                         NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            //File.Copy(e.FullPath, ConfigurationManager.AppSettings["ToPath"] + "\\" + Path.GetFileName(e.FullPath), true);
            string sourceFileName = Path.GetFileName(e.FullPath);
            //string 
            //C:\devops\asdf\New Text Document.txt
            string[] token = e.FullPath.Split('\\');
            token[1] = "devops2";
            string dest = token[0];
            //foreach(string str in token)
            for(int i=1; i<token.Length; i++)
            {
                dest = dest + "\\" + token[i];
            }
            Debug.Print("Destination: " + dest);
            Debug.Print("Soure file path: " + sourceFileName);
            

            FileAttributes attr = File.GetAttributes(e.FullPath);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                Directory.CreateDirectory(dest);
            }
            else
            {
                File.Copy(e.FullPath, dest, true);
            }
        }
    }
}
