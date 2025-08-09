using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Monitux_POS.Clases;
using Monitux_POS.Clases.MonituxPOS.Clases;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitux_POS.Ventanas
{
    public partial class V_Config_DB : Form
    {
        public V_Config_DB()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            string proveedor = comboBox1.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(proveedor))
            {
                V_Menu_Principal.MSG.ShowMSG("Seleccione un proveedor de base de datos.", "Advertencia");
                return;
            }

            string servidor = textBox5.Text.Trim();
            string usuario = textBox3.Text.Trim();
            string contraseña = textBox4.Text.Trim();

            string cadena = string.Empty;

            switch (proveedor)
            {
                case "SQLITE":
                    Properties.Settings.Default.DB_PROVIDER = "sqlite";
                    //textBox1.Text = $"Data Source=monitux.db;";
                    break;

                case "MYSQL":
                    if (string.IsNullOrEmpty(servidor) || string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
                    {
                        V_Menu_Principal.MSG.ShowMSG("Por favor, complete todos los campos requeridos para MySQL.", "Advertencia");
                        return;
                    }

                    cadena = $"server={servidor};user={usuario};password={contraseña};database=monitux;";
                    Properties.Settings.Default.DB_PROVIDER = "mysql";
                    break;

                case "SQLSERVER":
                    if (string.IsNullOrEmpty(servidor))
                    {
                        V_Menu_Principal.MSG.ShowMSG("Por favor, indique el servidor SQL.", "Advertencia");
                        return;
                    }

                    cadena = ConstruirCadenaSQLServer(servidor, usuario, contraseña);
                    Properties.Settings.Default.DB_PROVIDER = "sqlserver";
                    break;

                default:
                    V_Menu_Principal.MSG.ShowMSG("Proveedor de base de datos no reconocido.", "Error");
                    return;
            }

            if (!string.IsNullOrEmpty(cadena))
            {
                textBox1.Text = cadena;
            }

            Properties.Settings.Default.Save();

            // Función auxiliar
            string ConstruirCadenaSQLServer(string servidor, string usuario, string contraseña)
            {
                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
                {
                    // Autenticación de Windows
                    return $"Server={servidor};Database=monitux;Trusted_Connection=True;Encrypt=False;";
                }
                else
                {
                    // Autenticación SQL Server
                    return $"Server={servidor};User Id={usuario};Password={contraseña};Database=monitux;Encrypt=False;TrustServerCertificate=True;";
                }
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                string proveedor = comboBox1.SelectedItem?.ToString().ToLower();
                string conexion = textBox1.Text?.Trim();

                if (proveedor != "SQLITE")
                {

                    if (string.IsNullOrWhiteSpace(conexion))
                    {
                        V_Menu_Principal.MSG.ShowMSG("Debe ingresar una cadena de conexión válida.", "Advertencia");
                        return;
                    }

                }

                if (string.IsNullOrWhiteSpace(proveedor))
                {
                    V_Menu_Principal.MSG.ShowMSG("Debe seleccionar un proveedor de base de datos.", "Advertencia");
                    return;
                }

              

                var optionsBuilder = new DbContextOptionsBuilder<Monitux_DB_Context>();

                switch (proveedor)
                {
                    case "sqlite":
                        string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Database", "Sqlite-DB.db");

                        if (!File.Exists(dbPath))
                        {
                            V_Menu_Principal.MSG.ShowMSG($"No se encontró la base de datos SQLite en: {dbPath}", "Error");
                            return;
                        }

                        optionsBuilder.UseSqlite($"Data Source={dbPath}");
                        break;

                    case "mysql":
                        optionsBuilder.UseMySql(conexion, ServerVersion.AutoDetect(conexion));
                        break;

                    case "sqlserver":
                        optionsBuilder.UseSqlServer(conexion);
                        break;

                    default:
                        V_Menu_Principal.MSG.ShowMSG($"Proveedor de base de datos no soportado: {proveedor}", "Error");
                        return;
                }

                using var context = new Monitux_DB_Context();

                if (context.Database.CanConnect())
                {
                    V_Menu_Principal.MSG.ShowMSG("Conexión exitosa.", "Éxito");
                }
                else
                {
                    V_Menu_Principal.MSG.ShowMSG("No se pudo establecer conexión con la base de datos.", "Error");
                }
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Error al probar la conexión:\n{ex.Message}", "Error");
            }



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Visible = comboBox1.SelectedItem.ToString() != "SQLITE";
            label2.Visible = comboBox1.SelectedItem.ToString() != "SQLITE";

        }

        private void V_Config_DB_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0; // Selecciona SQLITE por defecto
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ////button3.PerformClick();

            //string proveedor = comboBox1.SelectedItem.ToString().ToLower();
            //string conexion = textBox1.Text;



            //if (proveedor == "sqlite")
            //{
            //    Properties.Settings.Default.DB_PROVIDER = "sqlite";

            //}
            //else if (proveedor == "mysql")
            //{
            //    Properties.Settings.Default.DB_PROVIDER = "mysql";
            //}
            //else if (proveedor == "sqlserver")
            //{
            //    Properties.Settings.Default.DB_PROVIDER = "sqlserver";
            //}



            //Properties.Settings.Default.DB_CONNECTION = textBox1.Text.Trim();

            //Properties.Settings.Default.Save();




            //if (Properties.Settings.Default.DB_PROVIDER == "mysql")
            //{
            //    string archivo = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Database", "MySQL-DB.sql");

            //    if (!File.Exists(archivo))
            //    {
            //        V_Menu_Principal.MSG.ShowMSG("El archivo de respaldo no fue encontrado.", "Error");
            //        return;
            //    }

            //    string dbName = "monitux";
            //    string serverConnection = $"Server={textBox5.Text.Trim()};Uid={textBox3.Text.Trim()};Pwd={textBox4.Text.Trim()};"; // sin 'Database='

            //    using (var conn = new MySqlConnection(serverConnection))
            //    {
            //        conn.Open();

            //        // ⚠️ Eliminar la base de datos si existe
            //        string dropDbScript = $"DROP DATABASE IF EXISTS `{dbName}`;";
            //        using var dropCmd = new MySqlCommand(dropDbScript, conn);
            //        dropCmd.ExecuteNonQuery();

            //        // ✅ Crear la base de datos
            //        string createDbScript = $"CREATE DATABASE `{dbName}` CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;";
            //        using var createCmd = new MySqlCommand(createDbScript, conn);
            //        createCmd.ExecuteNonQuery();
            //    }

            //    // 🧠 Ejecutar el script del esquema
            //    string script = File.ReadAllText(archivo);
            //    string dbConnection = $"Server=localhost;Database={dbName};Uid={textBox3.Text.Trim()};Pwd={textBox4.Text.Trim()};";

            //    using (var conn = new MySqlConnection(dbConnection))
            //    {
            //        conn.Open();

            //        // ⚠️ MySQL no permite múltiples instrucciones por defecto
            //        using var cmd = new MySqlCommand(script, conn);
            //        cmd.ExecuteNonQuery();
            //    }

            //    V_Menu_Principal.MSG.ShowMSG("Base de datos MySQL configurada exitosamente.", "Exito");
            //}


            //if (Properties.Settings.Default.DB_PROVIDER == "sqlserver")
            //{
            //    string archivo = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Database", "SQL-DB.sql");

            //    if (!File.Exists(archivo))
            //    {
            //        V_Menu_Principal.MSG.ShowMSG("El archivo de respaldo no fue encontrado.", "Error");
            //        return;
            //    }

            //    string dbName = "monitux";
            //    string serverConnection = $"Server={textBox5.Text.Trim()};User Id={textBox3.Text.Trim()};Password={textBox4.Text.Trim()};Encrypt=False;TrustServerCertificate=True;";

            //    try
            //    {
            //        using (var conn = new SqlConnection(serverConnection))
            //        {
            //            conn.Open();

            //            // 🧹 Eliminar conexiones activas
            //            string killConnections = $@"
            //    DECLARE @kill varchar(8000) = '';
            //    SELECT @kill = @kill + 'KILL ' + CONVERT(varchar(5), session_id) + ';'
            //    FROM sys.dm_exec_sessions
            //    WHERE database_id = DB_ID('{dbName}');
            //    EXEC(@kill);";

            //            using (var killCmd = new SqlCommand(killConnections, conn))
            //            {
            //                killCmd.ExecuteNonQuery();
            //            }

            //            // 🗑️ Eliminar la base si existe
            //            string dropDbScript = $"IF EXISTS (SELECT name FROM sys.databases WHERE name = N'{dbName}') DROP DATABASE [{dbName}];";
            //            using (var dropCmd = new SqlCommand(dropDbScript, conn))
            //            {
            //                dropCmd.ExecuteNonQuery();
            //            }

            //            // ✅ Crear la base
            //            string createDbScript = $"CREATE DATABASE [{dbName}];";
            //            using (var createCmd = new SqlCommand(createDbScript, conn))
            //            {
            //                createCmd.ExecuteNonQuery();
            //            }
            //        }

            //        // 🧠 Ejecutar el script del esquema
            //        string script = File.ReadAllText(archivo);
            //        string[] bloques = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            //        string dbConnection = $"Server={textBox5.Text.Trim()};Database={dbName};User Id={textBox3.Text.Trim()};Password={textBox4.Text.Trim()};Encrypt=False;TrustServerCertificate=True;";
            //        using (var conn = new SqlConnection(dbConnection))
            //        {
            //            conn.Open();
            //            foreach (string bloque in bloques)
            //            {
            //                if (string.IsNullOrWhiteSpace(bloque)) continue;

            //                try
            //                {
            //                    using var cmd = new SqlCommand(bloque, conn);
            //                    cmd.ExecuteNonQuery();
            //                }
            //                catch (Exception ex)
            //                {
            //                    V_Menu_Principal.MSG.ShowMSG($"Error en bloque SQL:\n{bloque}\n\nMensaje: {ex.Message}", "Error");
            //                }
            //            }
            //        }

            //        V_Menu_Principal.MSG.ShowMSG("Base de datos SQL configurada correctamente.", "Exito");
            //    }
            //    catch (Exception ex)
            //    {
            //        V_Menu_Principal.MSG.ShowMSG($"Error al configurar la base de datos: {ex.Message}", "Error");
            //    }
            //}



            //V_Menu_Principal.MSG.ShowMSG("Configuración aplicada correctamente.", "Información");


            //this.DialogResult = DialogResult.OK;
            //this.Close();





            try
            {
                string proveedor = comboBox1.SelectedItem?.ToString().ToLower();
                string conexion = textBox1.Text?.Trim();

               
                if (proveedor != "sqlite")
                {
                    if (string.IsNullOrWhiteSpace(conexion))
                    {
                        V_Menu_Principal.MSG.ShowMSG("Debe ingresar una cadena de conexión válida.", "Advertencia");
                        return;
                    }


                    if (string.IsNullOrWhiteSpace(proveedor))
                    {
                        V_Menu_Principal.MSG.ShowMSG("Debe seleccionar un proveedor de base de datos.", "Advertencia");
                        return;
                    }

                }
               

                // Guardar configuración
                Properties.Settings.Default.DB_PROVIDER = proveedor;
                Properties.Settings.Default.DB_CONNECTION = conexion;
                Properties.Settings.Default.Save();

                string archivo = proveedor switch
                {
                    "mysql" => Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Database", "MySQL-DB.sql"),
                    "sqlserver" => Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Database", "SQL-DB.sql"),
                    _ => null
                };

                if (archivo != null && !File.Exists(archivo))
                {
                    V_Menu_Principal.MSG.ShowMSG("El archivo de respaldo no fue encontrado.", "Error");
                    return;
                }

                string dbName = "monitux";
                string server = textBox5.Text.Trim();
                string user = textBox3.Text.Trim();
                string password = textBox4.Text.Trim();

                if (proveedor == "mysql")
                {
                    string serverConnection = $"Server={server};Uid={user};Pwd={password};";

                    using (var conn = new MySqlConnection(serverConnection))
                    {
                        conn.Open();
                        new MySqlCommand($"DROP DATABASE IF EXISTS `{dbName}`;", conn).ExecuteNonQuery();
                        new MySqlCommand($"CREATE DATABASE `{dbName}` CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;", conn).ExecuteNonQuery();
                    }

                    string script = File.ReadAllText(archivo);
                    string dbConnection = $"Server=localhost;Database={dbName};Uid={user};Pwd={password};";

                    using (var conn = new MySqlConnection(dbConnection))
                    {
                        conn.Open();
                        new MySqlCommand(script, conn).ExecuteNonQuery();
                    }

                    V_Menu_Principal.MSG.ShowMSG("Base de datos MySQL configurada exitosamente.", "Éxito");
                }
                else if (proveedor == "sqlserver")
                {
                    string serverConnection = $"Server={server};User Id={user};Password={password};Encrypt=False;TrustServerCertificate=True;";

                    using (var conn = new SqlConnection(serverConnection))
                    {
                        conn.Open();

                        string killConnections = $@"
                DECLARE @kill varchar(8000) = '';
                SELECT @kill = @kill + 'KILL ' + CONVERT(varchar(5), session_id) + ';'
                FROM sys.dm_exec_sessions
                WHERE database_id = DB_ID('{dbName}');
                EXEC(@kill);";

                        new SqlCommand(killConnections, conn).ExecuteNonQuery();
                        new SqlCommand($"IF EXISTS (SELECT name FROM sys.databases WHERE name = N'{dbName}') DROP DATABASE [{dbName}];", conn).ExecuteNonQuery();
                        new SqlCommand($"CREATE DATABASE [{dbName}];", conn).ExecuteNonQuery();
                    }

                    string script = File.ReadAllText(archivo);
                    string[] bloques = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    string dbConnection = $"Server={server};Database={dbName};User Id={user};Password={password};Encrypt=False;TrustServerCertificate=True;";

                    using (var conn = new SqlConnection(dbConnection))
                    {
                        conn.Open();
                        foreach (string bloque in bloques)
                        {
                            if (string.IsNullOrWhiteSpace(bloque)) continue;

                            try
                            {
                                new SqlCommand(bloque, conn).ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                V_Menu_Principal.MSG.ShowMSG($"Error en bloque SQL:\n{bloque}\n\nMensaje: {ex.Message}", "Error");
                            }
                        }
                    }

                    V_Menu_Principal.MSG.ShowMSG("Base de datos SQL configurada correctamente.", "Éxito");
                }

                V_Menu_Principal.MSG.ShowMSG("Configuración aplicada correctamente.", "Información");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Error inesperado: {ex.Message}", "Error");
            }









        }



        private void Cargar_Instalacion_SQL()
        {
            var installer = new SqlServerInstaller();
            var progressForm = new ProgressForm();

            if (!installer.IsSqlServerInstalled())
            {
                var result = MessageBox.Show("No se detectó SQL Server Express. ¿Desea instalarlo automáticamente?", "Instalación", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    progressForm.Show();
                    string ruta = Path.Combine(Path.GetTempPath(), "SQLEXPRESS2022.exe");

                    progressForm.UpdateStatus("Descargando instalador...", 10);
                    installer.DownloadInstaller(ruta);

                    progressForm.UpdateStatus("Ejecutando instalación...", 50);
                    installer.RunSilentInstall(ruta);

                    progressForm.UpdateStatus("Verificando instalación...", 90);
                    System.Threading.Thread.Sleep(10000); // Espera opcional

                    if (installer.IsSqlServerInstalled())
                    {
                        progressForm.UpdateStatus("Instalación completada.", 100);
                        MessageBox.Show("SQL Server Express ha sido instalado correctamente.");
                    }
                    else
                    {
                        progressForm.UpdateStatus("Error en la instalación.", 100);
                        MessageBox.Show("Hubo un problema al instalar SQL Server Express.");
                    }

                    progressForm.Close();
                }
            }
            else
            {
                MessageBox.Show("SQL Server Express ya está instalado.", "Monitux-POS");
            }
        }



        private void Cargar_Instalacion_MySQL()
        {
            var installer = new MySQLInstaller();
            var progressForm = new ProgressForm();

            if (!installer.IsMySQLInstalled())
            {
                var result = MessageBox.Show("No se detectó MySQL Server. ¿Desea instalarlo automáticamente?", "Instalación", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    progressForm.Show();
                    string ruta = Path.Combine(Path.GetTempPath(), "mysql-installer.msi");

                    progressForm.UpdateStatus("Descargando instalador...", 10);
                    installer.DownloadInstaller(ruta);

                    progressForm.UpdateStatus("Ejecutando instalación...", 50);
                    installer.RunSilentInstall(ruta);

                    progressForm.UpdateStatus("Configurando servidor...", 80);
                    installer.ConfigureServer();

                    System.Threading.Thread.Sleep(10000); // Espera opcional

                    if (installer.IsMySQLInstalled())
                    {
                        progressForm.UpdateStatus("Instalación completada.", 100);
                        MessageBox.Show("MySQL Server ha sido instalado correctamente.");
                    }
                    else
                    {
                        progressForm.UpdateStatus("Error en la instalación.", 100);
                        MessageBox.Show("Hubo un problema al instalar MySQL Server.");
                    }

                    progressForm.Close();
                }
            }
            else
            {
                MessageBox.Show("MySQL Server ya está instalado.", "Monitux-POS");
            }
        }




        private void pictureBox1_Click(object sender, EventArgs e)
        {





        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void obtenerCadenaDeConexionDeInstanciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlServerInstaller sqlServerInstaller = new SqlServerInstaller();
            textBox1.Text = sqlServerInstaller.GetConnectionString();
        }

        private void instalarSQLServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cargar_Instalacion_SQL();
        }

        private void instalarMySQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cargar_Instalacion_MySQL();
        }

        private void datosDeConexionAInstanciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySQLInstaller mysqlInstaller = new MySQLInstaller();
            textBox1.Text = mysqlInstaller.GetMySqlConnectionString();

        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = "SQLSERVER";
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = "MYSQL";
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = "SQLITE";
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }
    }
}
