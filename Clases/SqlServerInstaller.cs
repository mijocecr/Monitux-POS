using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.ServiceProcess;
using System.Linq;

public class SqlServerInstaller
{
    private const string InstallerUrl = "https://go.microsoft.com/fwlink/?linkid=2209510"; // SQL Server Express 2022
    private const string InstallerPath = "SQLEXPRESS2022.exe";
    private const string SqlPassword = "Monitux_POS_SQL";

    public bool IsSqlServerInstalled()
    {
        // 1️⃣ Verificar si el servicio SQLEXPRESS está instalado
        try
        {
            var servicios = ServiceController.GetServices();
            var sqlService = servicios.FirstOrDefault(s => s.ServiceName.ToUpper().Contains("MSSQL$SQLEXPRESS"));

            if (sqlService != null)
                return sqlService.Status == ServiceControllerStatus.Running || sqlService.Status == ServiceControllerStatus.Stopped;
        }
        catch { }

        // 2️⃣ Buscar ejecutable en rutas comunes
        string[] posiblesRutas = new[]
        {
            @"C:\Program Files\Microsoft SQL Server",
            @"C:\Program Files (x86)\Microsoft SQL Server"
        };

        foreach (var baseDir in posiblesRutas)
        {
            if (Directory.Exists(baseDir))
            {
                var exe = Directory.GetFiles(baseDir, "sqlservr.exe", SearchOption.AllDirectories).FirstOrDefault();
                if (!string.IsNullOrEmpty(exe))
                    return true;
            }
        }

        // 3️⃣ Intentar ejecutar sqlcmd para verificar instalación
        try
        {
            var versionCheck = new ProcessStartInfo
            {
                FileName = "sqlcmd",
                Arguments = "-?",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(versionCheck);
            string output = process?.StandardOutput.ReadToEnd();
            process?.WaitForExit();

            if (!string.IsNullOrWhiteSpace(output) && output.ToLower().Contains("usage"))
                return true;
        }
        catch { }

        return false;
    }

    public void DownloadInstaller(string destino)
    {
        using WebClient client = new WebClient();
        client.DownloadFile(InstallerUrl, destino);
    }

    public void RunSilentInstall(string rutaInstalador)
    {
        var args = "/Q /ACTION=Install /FEATURES=SQLEngine /INSTANCENAME=SQLEXPRESS " +
                   "/SQLSVCACCOUNT=\"NT AUTHORITY\\SYSTEM\" /SQLSYSADMINACCOUNTS=\"BUILTIN\\ADMINISTRATORS\" " +
                   "/SECURITYMODE=SQL /SAPWD=\"" + SqlPassword + "\" /IACCEPTSQLSERVERLICENSETERMS";

        Process.Start(new ProcessStartInfo
        {
            FileName = rutaInstalador,
            Arguments = args,
            UseShellExecute = false,
            CreateNoWindow = true
        });
    }

    public bool IsServiceRunning(string nombreServicio)
    {
        try
        {
            ServiceController sc = new ServiceController(nombreServicio);
            return sc.Status == ServiceControllerStatus.Running;
        }
        catch
        {
            return false;
        }
    }

    public string GetConnectionString(string dbName = "monitux", string user = "sa", string password = "Monitux_POS_SQL")
    {
        return $"Server=.\\SQLEXPRESS;Database={dbName};User Id={user};Password={password};Encrypt=False;TrustServerCertificate=True;";
    }
}
