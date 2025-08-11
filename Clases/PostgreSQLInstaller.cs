


    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.ServiceProcess;

    namespace Monitux_POS.Clases
    {
        public class PostgreSQLInstaller
        {
            private const string InstallerUrl = "https://get.enterprisedb.com/postgresql/postgresql-15.5-1-windows-x64.exe";
            private const string SuperUserPassword = "Monitux_POS_POSTGRES";

            public static void InstalarPostgreSQL()
            {
                string tempPath = Path.Combine(Path.GetTempPath(), "postgres-installer.exe");

                DescargarInstalador(tempPath);
                EjecutarInstalador(tempPath);
            }

            public static void DescargarInstalador(string destino)
            {
                using var client = new WebClient();
                client.DownloadFile(InstallerUrl, destino);
            }

            public static void EjecutarInstalador(string ruta)
            {
                string argumentos = $"--mode unattended --unattendedmodeui minimal --superpassword {SuperUserPassword} --servicename postgresql-x64-15 --serverport 5432 --datadir \"C:\\PostgreSQL\\data\"";

                Process.Start(new ProcessStartInfo
                {
                    FileName = ruta,
                    Arguments = argumentos,
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
            }

            // ✅ Detección automática mejorada
            public bool IsPostgreSQLInstalled()
            {
                // 1️⃣ Verificar si postgres está en el PATH
                try
                {
                    var versionCheck = new ProcessStartInfo
                    {
                        FileName = "postgres",
                        Arguments = "--version",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using var process = Process.Start(versionCheck);
                    string output = process?.StandardOutput.ReadToEnd();
                    process?.WaitForExit();

                    if (!string.IsNullOrWhiteSpace(output) && output.ToLower().Contains("postgresql"))
                        return true;
                }
                catch { }

                // 2️⃣ Buscar en rutas comunes
                try
                {
                    string baseDir = @"C:\Program Files\PostgreSQL";
                    if (Directory.Exists(baseDir))
                    {
                        var binPaths = Directory.GetDirectories(baseDir)
                            .Select(dir => Path.Combine(dir, "bin", "postgres.exe"))
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
                        .Any(s => s.ServiceName.ToLower().Contains("postgresql") && s.Status == ServiceControllerStatus.Running);

                    if (servicioActivo)
                        return true;
                }
                catch { }

                return false;
            }

            public string GetPostgresConnectionString()
            {
                string server = "localhost";
                string database = "monitux";
                string user = "postgres";
                string password = SuperUserPassword;
                string port = "5432";

                return $"Host={server};Port={port};Database={database};Username={user};Password={password};";
            }
        }
    }



