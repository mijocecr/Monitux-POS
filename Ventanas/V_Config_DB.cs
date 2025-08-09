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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

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
            switch (comboBox1.SelectedItem.ToString())
            {
                case "SQLITE":

                    Properties.Settings.Default.DB_PROVIDER = "sqlite";

                    break;


                case "MYSQL":
                    if (!string.IsNullOrEmpty(textBox5.Text)&&!!string.IsNullOrEmpty(textBox3.Text)&& !string.IsNullOrEmpty(textBox4.Text)) {
                        textBox1.Text = $"server={textBox5.Text.Trim()};user={textBox3.Text.Trim()};password={textBox4.Text};database=monitux;";
                        Properties.Settings.Default.DB_PROVIDER = "mysql";
                    }
                    else {                         V_Menu_Principal.MSG.ShowMSG("Por favor, complete todos los campos requeridos para MySQL.", "Advertencia");
                        return;
                    }


                    break;
                case "SQLSERVER":
                    if(!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text))
                    {
                        textBox1.Text = $"Server={textBox5.Text.Trim()};User Id={textBox3.Text.Trim()};Password={textBox4.Text.Trim()};Database=monitux;Encrypt=False;TrustServerCertificate=True;";
                        Properties.Settings.Default.DB_PROVIDER = "sqlserver";
                    }
                    else
                    {
                        V_Menu_Principal.MSG.ShowMSG("Por favor, complete todos los campos requeridos para SQL Server.", "Advertencia");
                        return;
                    }
                   // textBox1.Text = $"Server={textBox5.Text.Trim()};Database=monitux;Trusted_Connection=True;Encrypt=False;";
                    
                    break;
                default:
                    // MessageBox.Show("Seleccione un tipo de base de datos válido.");
                    return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            string proveedor = comboBox1.SelectedItem.ToString().ToLower();
            string conexion = textBox1.Text;

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<Monitux_DB_Context>();

                if (proveedor == "sqlite")
                {
                    string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Database", "monitux.db");
                    optionsBuilder.UseSqlite($"Data Source={dbPath}");
                }
                else if (proveedor == "mysql")
                {
                    optionsBuilder.UseMySql(conexion, ServerVersion.AutoDetect(conexion));
                }
                else if (proveedor == "sqlserver")
                {
                    optionsBuilder.UseSqlServer(conexion);
                }

                using var context = new Monitux_DB_Context();
                context.Database.CanConnect();

                //lblEstado.Text = "Conexión exitosa.";

                V_Menu_Principal.MSG.ShowMSG("Conexión exitosa.", "Exito");
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Error: {ex.Message}", "Error");
                //lblEstado.Text = $"Error: {ex.Message}";
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Visible = comboBox1.SelectedItem.ToString() != "SQLITE";
        }

        private void V_Config_DB_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0; // Selecciona SQLITE por defecto
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.PerformClick();

            string proveedor = comboBox1.SelectedItem.ToString().ToLower();
            string conexion = textBox1.Text;

           

                if (proveedor == "sqlite")
                {
                   Properties.Settings.Default.DB_PROVIDER = "sqlite";
  
                }
                else if (proveedor == "mysql")
                {
                    Properties.Settings.Default.DB_PROVIDER = "mysql";
                }
                else if (proveedor == "sqlserver")
                {
                    Properties.Settings.Default.DB_PROVIDER = "sqlserver";
                }

             

            Properties.Settings.Default.DB_CONNECTION = textBox1.Text.Trim();

            Properties.Settings.Default.Save();
           
            
            V_Menu_Principal.MSG.ShowMSG("Configuración guardada correctamente.", "Información");





            if (Properties.Settings.Default.DB_PROVIDER == "mysql")
            {
                string archivo = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Database", "MySQL-DB.sql");

                if (!File.Exists(archivo))
                {
                    V_Menu_Principal.MSG.ShowMSG("El archivo de respaldo no fue encontrado.","Error");
                    return;
                }

                string dbName = "monitux";
                string serverConnection = $"Server={textBox5.Text.Trim()};Uid={textBox3.Text.Trim()};Pwd={textBox4.Text.Trim()};"; // sin 'Database='

                using (var conn = new MySqlConnection(serverConnection))
                {
                    conn.Open();

                    // ⚠️ Eliminar la base de datos si existe
                    string dropDbScript = $"DROP DATABASE IF EXISTS `{dbName}`;";
                    using var dropCmd = new MySqlCommand(dropDbScript, conn);
                    dropCmd.ExecuteNonQuery();

                    // ✅ Crear la base de datos
                    string createDbScript = $"CREATE DATABASE `{dbName}` CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;";
                    using var createCmd = new MySqlCommand(createDbScript, conn);
                    createCmd.ExecuteNonQuery();
                }

                // 🧠 Ejecutar el script del esquema
                string script = File.ReadAllText(archivo);
                string dbConnection = $"Server=localhost;Database={dbName};Uid={textBox3.Text.Trim()};Pwd={textBox4.Text.Trim()};";

                using (var conn = new MySqlConnection(dbConnection))
                {
                    conn.Open();

                    // ⚠️ MySQL no permite múltiples instrucciones por defecto
                    using var cmd = new MySqlCommand(script, conn);
                    cmd.ExecuteNonQuery();
                }

                V_Menu_Principal.MSG.ShowMSG("Base de datos MySQL configurada exitosamente.","Exito");
            }


            if (Properties.Settings.Default.DB_PROVIDER == "sqlserver")
            {
                string archivo = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Database", "SQL-DB.sql");

                if (!File.Exists(archivo))
                {
                    V_Menu_Principal.MSG.ShowMSG("El archivo de respaldo no fue encontrado.", "Error");
                    return;
                }

                string dbName = "monitux";
                string serverConnection = $"Server={textBox5.Text.Trim()};User Id={textBox3.Text.Trim()};Password={textBox4.Text.Trim()};Encrypt=False;TrustServerCertificate=True;";

                try
                {
                    using (var conn = new SqlConnection(serverConnection))
                    {
                        conn.Open();

                        // 🧹 Eliminar conexiones activas
                        string killConnections = $@"
                DECLARE @kill varchar(8000) = '';
                SELECT @kill = @kill + 'KILL ' + CONVERT(varchar(5), session_id) + ';'
                FROM sys.dm_exec_sessions
                WHERE database_id = DB_ID('{dbName}');
                EXEC(@kill);";

                        using (var killCmd = new SqlCommand(killConnections, conn))
                        {
                            killCmd.ExecuteNonQuery();
                        }

                        // 🗑️ Eliminar la base si existe
                        string dropDbScript = $"IF EXISTS (SELECT name FROM sys.databases WHERE name = N'{dbName}') DROP DATABASE [{dbName}];";
                        using (var dropCmd = new SqlCommand(dropDbScript, conn))
                        {
                            dropCmd.ExecuteNonQuery();
                        }

                        // ✅ Crear la base
                        string createDbScript = $"CREATE DATABASE [{dbName}];";
                        using (var createCmd = new SqlCommand(createDbScript, conn))
                        {
                            createCmd.ExecuteNonQuery();
                        }
                    }

                    // 🧠 Ejecutar el script del esquema
                    string script = File.ReadAllText(archivo);
                    string[] bloques = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                    string dbConnection = $"Server={textBox5.Text.Trim()};Database={dbName};User Id={textBox3.Text.Trim()};Password={textBox4.Text.Trim()};Encrypt=False;TrustServerCertificate=True;";
                    using (var conn = new SqlConnection(dbConnection))
                    {
                        conn.Open();
                        foreach (string bloque in bloques)
                        {
                            if (string.IsNullOrWhiteSpace(bloque)) continue;

                            try
                            {
                                using var cmd = new SqlCommand(bloque, conn);
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                V_Menu_Principal.MSG.ShowMSG($"Error en bloque SQL:\n{bloque}\n\nMensaje: {ex.Message}","Error");
                            }
                        }
                    }

                    V_Menu_Principal.MSG.ShowMSG("Base de datos SQL configurada correctamente.","Exito");
                }
                catch (Exception ex)
                {
                    V_Menu_Principal.MSG.ShowMSG($"Error al configurar la base de datos: {ex.Message}", "Error");
                }
            }






            this.DialogResult = DialogResult.OK;
            this.Close();


        }
    }
}
