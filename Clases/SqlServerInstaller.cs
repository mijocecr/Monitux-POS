using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.ServiceProcess;

public class SqlServerInstaller
{
    private const string InstallerUrl = "https://go.microsoft.com/fwlink/?linkid=2209510"; // SQL Server Express 2022
    private const string InstallerPath = "SQLEXPRESS2022.exe";

    public bool IsSqlServerInstalled()
    {
        try
        {
            ServiceController sc = new ServiceController("MSSQL$SQLEXPRESS");
            return sc.Status == ServiceControllerStatus.Running || sc.Status == ServiceControllerStatus.Stopped;
        }
        catch
        {
            return false;
        }
    }

    public void DownloadInstaller(string destino)
    {
        using WebClient client = new WebClient();
        client.DownloadFile(InstallerUrl, destino);
    }

    public void RunSilentInstall(string rutaInstalador)
    {
        // var args = "/Q /ACTION=Install /FEATURES=SQLEngine /INSTANCENAME=SQLEXPRESS /SQLSVCACCOUNT=\"NT AUTHORITY\\SYSTEM\" /SQLSYSADMINACCOUNTS=\"BUILTIN\\ADMINISTRATORS\" /IACCEPTSQLSERVERLICENSETERMS";

        var args = "/Q /ACTION=Install /FEATURES=SQLEngine /INSTANCENAME=SQLEXPRESS " +
           "/SQLSVCACCOUNT=\"NT AUTHORITY\\SYSTEM\" /SQLSYSADMINACCOUNTS=\"BUILTIN\\ADMINISTRATORS\" " +
           "/SECURITYMODE=SQL /SAPWD=\"Monitux_POS_SQL\" /IACCEPTSQLSERVERLICENSETERMS";


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
