using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;

namespace Monitux_POS.Clases
{
    public class MySQLInstaller
    {
        private const string InstallerUrl = "https://dev.mysql.com/get/mysql-installer-web-community-8.0.33.0.msi";
        private const string RootPassword = "Monitux_POS_MYSQL";

        public void InstalarMySQL()
        {
            string tempPath = Path.Combine(Path.GetTempPath(), "mysql-installer.msi");

            DownloadInstaller(tempPath);
            RunSilentInstall(tempPath);
            ConfigureServer();
        }

        // ✅ Restaurado como público
        public void DownloadInstaller(string destino)
        {
            using var client = new WebClient();
            client.DownloadFile(InstallerUrl, destino);
        }

        // ✅ Restaurado como público
        public void RunSilentInstall(string ruta)
        {
            Process.Start("msiexec.exe", $"/i \"{ruta}\" /quiet");
        }

        // ✅ Restaurado como público
        public void ConfigureServer()
        {
            string comando = $"MySQLInstallerConsole.exe community install server;8.0.33:*:port=3306;serverid=1;type=developer;username=root;password={RootPassword} -silent";
            Process.Start("cmd.exe", "/c " + comando);
        }

        // ✅ Detección automática mejorada
        public bool IsMySQLInstalled()
        {
            // 1️⃣ Verificar si mysqld está en el PATH
            try
            {
                var versionCheck = new ProcessStartInfo
                {
                    FileName = "mysqld",
                    Arguments = "--version",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(versionCheck);
                string output = process?.StandardOutput.ReadToEnd();
                process?.WaitForExit();

                if (!string.IsNullOrWhiteSpace(output) && output.ToLower().Contains("mysql"))
                    return true;
            }
            catch { }

            // 2️⃣ Buscar en rutas comunes
            try
            {
                string baseDir = @"C:\Program Files\MySQL";
                if (Directory.Exists(baseDir))
                {
                    var binPaths = Directory.GetDirectories(baseDir)
                        .Select(dir => Path.Combine(dir, "bin", "mysqld.exe"))
                        .Where(File.Exists);

                    if (binPaths.Any())
                        return true;
                }
            }
            catch { }

            // 3️⃣ Verificar si el servicio está activo
            try
            {
                bool servicioActivo = ServiceController.GetServices()
                    .Any(s => s.ServiceName.ToLower().Contains("mysql") && s.Status == ServiceControllerStatus.Running);

                if (servicioActivo)
                    return true;
            }
            catch { }

            return false;
        }

        public string GetMySqlConnectionString()
        {
            string server = "localhost";
            string database = "monitux";
            string user = "root";
            string password = RootPassword;
            string port = "3306";

            return $"Server={server};Port={port};Database={database};Uid={user};Pwd={password};SslMode=none;";
        }
    }
}
