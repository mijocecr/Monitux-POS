using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{

    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;

    namespace MonituxPOS.Clases
    {
        public class MySQLInstaller
        {
            private const string InstallerUrl = "https://dev.mysql.com/get/mysql-installer-web-community-8.0.33.0.msi";
            private const string RootPassword = "Monitux_POS_MYSQL";

            public static void InstalarMySQL()
            {
                string tempPath = Path.Combine(Path.GetTempPath(), "mysql-installer.msi");

                DescargarInstalador(tempPath);
                EjecutarInstalador(tempPath);
                ConfigurarServidor();
            }

            private static void DescargarInstalador(string destino)
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(InstallerUrl, destino);
                }
            }



            public bool IsMySQLInstalled()
            {
                string path = @"C:\Program Files\MySQL\MySQL Server 8.0\bin\mysqld.exe";
                return File.Exists(path);
            }

            public void DownloadInstaller(string destino)
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(InstallerUrl, destino);
                }
            }

            public void RunSilentInstall(string ruta)
            {
                Process.Start("msiexec.exe", $"/i \"{ruta}\" /quiet");
            }

            public void ConfigureServer()
            {
                string comando = $"MySQLInstallerConsole.exe community install server;8.0.33:*:port=3306;serverid=1;type=developer;username=root;password={RootPassword} -silent";
                Process.Start("cmd.exe", "/c " + comando);
            }







            private static void EjecutarInstalador(string ruta)
            {
                Process.Start("msiexec.exe", $"/i \"{ruta}\" /quiet");
            }

            private static void ConfigurarServidor()
            {
                string comando = $"MySQLInstallerConsole.exe community install server;8.0.33:*:port=3306;serverid=1;type=developer;username=root;password={RootPassword} -silent";
                Process.Start("cmd.exe", "/c " + comando);
            }


            public string GetMySqlConnectionString()
            {
                string server = "localhost"; // o IP del servidor MySQL
                string database = "monitux";
                string user = "root";
                string password = "Monitux_POS_MYSQL";
                string port = "3306";

                return $"Server={server};Port={port};Database={database};Uid={user};Pwd={password};SslMode=none;";
            }



        }





    }



    }
