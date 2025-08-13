using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Monitux_POS.Clases;

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


                case "POSTGRES":
                    if (string.IsNullOrEmpty(servidor))
                    {
                        V_Menu_Principal.MSG.ShowMSG("Por favor, indique el servidor PostgreSQL.", "Advertencia");
                        return;
                    }

                    cadena = ConstruirCadenaPostgres(servidor, usuario, contraseña);
                    Properties.Settings.Default.DB_PROVIDER = "postgres";
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


            string ConstruirCadenaPostgres(string servidor, string usuario, string contraseña)
            {
                string baseDatos = "monitux";
                string puerto = "5432";

                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
                {
                    // Autenticación sin usuario/contraseña (por ejemplo, con ident o peer en localhost)
                    return $"Host={servidor};Port={puerto};Database={baseDatos};Integrated Security=true;";
                }
                else
                {
                    // Autenticación estándar con usuario y contraseña
                    return $"Host={servidor};Port={puerto};Database={baseDatos};Username={usuario};Password={contraseña};SSL Mode=Prefer;Trust Server Certificate=true;";
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

            if (checkBox1.Checked)
            {
             Properties.Settings.Default.Empresa_Creada = true;
                Properties.Settings.Default.Primer_Arranque = false;
            }
            else
            {
                Properties.Settings.Default.Primer_Arranque = true;
                Properties.Settings.Default.Empresa_Creada = false;
            }



            //try
            //{
            //    string proveedor = comboBox1.SelectedItem?.ToString().ToLower();
            //    string conexion = textBox1.Text?.Trim();


            //    if (proveedor != "sqlite")
            //    {
            //        if (string.IsNullOrWhiteSpace(conexion))
            //        {
            //            V_Menu_Principal.MSG.ShowMSG("Debe ingresar una cadena de conexión válida.", "Advertencia");
            //            return;
            //        }


            //        if (string.IsNullOrWhiteSpace(proveedor))
            //        {
            //            V_Menu_Principal.MSG.ShowMSG("Debe seleccionar un proveedor de base de datos.", "Advertencia");
            //            return;
            //        }

            //    }


            //    // Guardar configuración
            //    Properties.Settings.Default.DB_PROVIDER = proveedor;
            //    Properties.Settings.Default.DB_CONNECTION = conexion;
            //    Properties.Settings.Default.Save();

            //    string archivo = proveedor switch
            //    {
            //        "mysql" => Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Database", "MySQL-DB.sql"),
            //        "sqlserver" => Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Database", "SQL-DB.sql"),
            //        _ => null
            //    };

            //    if (archivo != null && !File.Exists(archivo))
            //    {
            //        V_Menu_Principal.MSG.ShowMSG("El archivo de respaldo no fue encontrado.", "Error");
            //        return;
            //    }

            //    string dbName = "monitux";
            //    string server = textBox5.Text.Trim();
            //    string user = textBox3.Text.Trim();
            //    string password = textBox4.Text.Trim();

            //    if (proveedor == "mysql")
            //    {
            //        string serverConnection = $"Server={server};Uid={user};Pwd={password};";

            //        using (var conn = new MySqlConnection(serverConnection))
            //        {
            //            conn.Open();
            //            new MySqlCommand($"DROP DATABASE IF EXISTS `{dbName}`;", conn).ExecuteNonQuery();
            //            new MySqlCommand($"CREATE DATABASE `{dbName}` CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;", conn).ExecuteNonQuery();
            //        }

            //        string script = File.ReadAllText(archivo);
            //        string dbConnection = $"Server=localhost;Database={dbName};Uid={user};Pwd={password};";

            //        using (var conn = new MySqlConnection(dbConnection))
            //        {
            //            conn.Open();
            //            new MySqlCommand(script, conn).ExecuteNonQuery();
            //        }

            //        V_Menu_Principal.MSG.ShowMSG("Base de datos MySQL configurada exitosamente.", "Éxito");
            //    }
            //    else if (proveedor == "sqlserver")
            //    {
            //        string serverConnection = $"Server={server};User Id={user};Password={password};Encrypt=False;TrustServerCertificate=True;";

            //        using (var conn = new SqlConnection(serverConnection))
            //        {
            //            conn.Open();

            //            string killConnections = $@"
            //    DECLARE @kill varchar(8000) = '';
            //    SELECT @kill = @kill + 'KILL ' + CONVERT(varchar(5), session_id) + ';'
            //    FROM sys.dm_exec_sessions
            //    WHERE database_id = DB_ID('{dbName}');
            //    EXEC(@kill);";

            //            new SqlCommand(killConnections, conn).ExecuteNonQuery();
            //            new SqlCommand($"IF EXISTS (SELECT name FROM sys.databases WHERE name = N'{dbName}') DROP DATABASE [{dbName}];", conn).ExecuteNonQuery();
            //            new SqlCommand($"CREATE DATABASE [{dbName}];", conn).ExecuteNonQuery();
            //        }

            //        string script = File.ReadAllText(archivo);
            //        string[] bloques = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            //        string dbConnection = $"Server={server};Database={dbName};User Id={user};Password={password};Encrypt=False;TrustServerCertificate=True;";

            //        using (var conn = new SqlConnection(dbConnection))
            //        {
            //            conn.Open();
            //            foreach (string bloque in bloques)
            //            {
            //                if (string.IsNullOrWhiteSpace(bloque)) continue;

            //                try
            //                {
            //                    new SqlCommand(bloque, conn).ExecuteNonQuery();
            //                }
            //                catch (Exception ex)
            //                {
            //                    V_Menu_Principal.MSG.ShowMSG($"Error en bloque SQL:\n{bloque}\n\nMensaje: {ex.Message}", "Error");
            //                }
            //            }
            //        }

            //        V_Menu_Principal.MSG.ShowMSG("Base de datos SQL configurada correctamente.", "Éxito");
            //    }

            //    V_Menu_Principal.MSG.ShowMSG("Configuración aplicada correctamente.", "Información");
            //    this.DialogResult = DialogResult.OK;
            //    this.Close();
            //}
            //catch (Exception ex)
            //{
            //    V_Menu_Principal.MSG.ShowMSG($"Error inesperado: {ex.Message}", "Error");
            //}

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
                    "postgres" => Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Database", "Postgres-DB.sql"),
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
                    if (!checkBox1.Checked)
                    {
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
                    }
                    V_Menu_Principal.MSG.ShowMSG("Base de datos MySQL configurada exitosamente.", "Éxito");
                }
                else if (proveedor == "sqlserver")
                {

                    string serverConnection = $"Server={server};User Id={user};Password={password};Encrypt=False;TrustServerCertificate=True;";

                    if (!checkBox1.Checked)
                    {

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

                    }

                    V_Menu_Principal.MSG.ShowMSG("Base de datos SQL configurada correctamente.", "Éxito");
                    
                    }

                else if (proveedor == "postgres")
                {
                    string serverConnection = $"Host={server};Port=5432;Username={user};Password={password};Database=postgres";

                    if (!checkBox1.Checked)
                    {


                        //////////

                        using (var conn = new Npgsql.NpgsqlConnection(serverConnection))
                        {
                            conn.Open();

                            // Terminar conexiones activas
                            var terminateCmd = new Npgsql.NpgsqlCommand($@"
        SELECT pg_terminate_backend(pid)
        FROM pg_stat_activity
        WHERE datname = '{dbName}' AND pid <> pg_backend_pid();", conn);
                            terminateCmd.ExecuteNonQuery();

                            // Ahora puedes eliminar y crear la base
                            new Npgsql.NpgsqlCommand($"DROP DATABASE IF EXISTS \"{dbName}\";", conn).ExecuteNonQuery();
                            new Npgsql.NpgsqlCommand($"CREATE DATABASE \"{dbName}\" WITH ENCODING='UTF8';", conn).ExecuteNonQuery();
                        }



                        /////////




                        using (var conn = new Npgsql.NpgsqlConnection(serverConnection))
                        {
                            conn.Open();
                            new Npgsql.NpgsqlCommand($"DROP DATABASE IF EXISTS \"{dbName}\";", conn).ExecuteNonQuery();
                            new Npgsql.NpgsqlCommand($"CREATE DATABASE \"{dbName}\" WITH ENCODING='UTF8';", conn).ExecuteNonQuery();
                        }

                        string script = File.ReadAllText(archivo);
                        string dbConnection = $"Host={server};Port=5432;Username={user};Password={password};Database={dbName};";

                        using (var conn = new Npgsql.NpgsqlConnection(dbConnection))
                        {
                            conn.Open();
                            new Npgsql.NpgsqlCommand(script, conn).ExecuteNonQuery();
                        }
                    }
                    V_Menu_Principal.MSG.ShowMSG("Base de datos PostgreSQL configurada exitosamente.", "Éxito");
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




      


      




        private void pictureBox1_Click(object sender, EventArgs e)
        {





        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void obtenerCadenaDeConexionDeInstanciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

     
       
      

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

       

       

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
