using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Renci.SshNet;
using System.IO;
using System.Windows.Forms;

namespace Solution
{
    //Процедуры загрузки файлов по протоколу SFTP
    class sftp
    {
        //Загрузка файла с локальной машины на удаленный сервер по протоколу SFTP
        public static void UploadSFTPFile(string host, string username, string password, string sourcefile, string destinationpath, int port)
        {
            using (SftpClient client = new SftpClient(host, port, username, password)) //Объект соединения с сервером по SFTP
            {
                client.Connect();
                client.ChangeDirectory(destinationpath);
                using (FileStream fs = new FileStream(sourcefile, FileMode.Open)) //Объект потокового чтения файла
                {
                    client.BufferSize = 4 * 1024;
                    client.UploadFile(fs, Path.GetFileName(sourcefile));
                    
                    
                }
            }
        }

        //Загрузка файла с удаленного сервера в локальную папку приложения по протоколу SFTP
        public static void DownloadSFTPFile(string host, string username, string password, string file_name, int port)
        {
            using (SftpClient client = new SftpClient(host, port, username, password)) //Объект соединения с сервером по SFTP
            {
                client.Connect();
                string remote_dir = @"/root/";
                string path_to_file = remote_dir + file_name;
                if (!client.Exists(path_to_file))
                {
                    return;
                }
                client.ChangeDirectory(remote_dir);
                using (Stream fileStream = File.Create(Application.StartupPath + @"\" + file_name)) //Объект потокового чтения файла
                {
                    client.DownloadFile(path_to_file, fileStream);
                }
            }
        }
    }
}
