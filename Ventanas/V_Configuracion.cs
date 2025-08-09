using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitux_POS.Ventanas
{
    public partial class V_Configuracion : Form
    {
        public V_Configuracion()
        {
            InitializeComponent();
        }

        private void V_Configuracion_Load(object sender, EventArgs e)
        {
            this.Text = "Configuración del Sistema";
             switch (Properties.Settings.Default.DB_PROVIDER)
             {
                 case "mysql":
                     panel1.Visible = false;
                     panel3.Visible = false;
                     panel2.Visible = true;
                     break;
                 case "sqlite":
                     panel2.Visible = false;
                     panel3.Visible = false;
                     panel1.Visible = true;
                     break;
                 case "sqlserver":
                     panel1.Visible = false;
                     panel2.Visible = false;
                     panel3.Visible = true;
                     break;

                 default:

                     break;
             }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Archivos de base de datos (*.db)|*.db";
            openFileDialog1.Title = "Selecciona un archivo de respaldo .db";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath != "")
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            // Ruta de la base de datos original (SQLite)
            string rutaBaseDatosOriginal = Path.Combine(Application.StartupPath, "Resources", "Database", "monitux.db");

            // Ruta de respaldo para SQLite
            string nombreRespaldoSQLite = $"{DateTime.Today:dd-MM-yyyy}-Monitux.db";
            string rutaRespaldoSQLite = Path.Combine(textBox2.Text, nombreRespaldoSQLite);


            // Copiar el archivo de la base de datos SQLite a la ubicación de respaldo
            File.Copy(rutaBaseDatosOriginal, rutaRespaldoSQLite, overwrite: true);

            // Mostrar mensaje de éxito
            V_Menu_Principal.MSG.ShowMSG($"Respaldo creado correctamente en:  {rutaRespaldoSQLite}", "Éxito");




        }

        private void button3_Click(object sender, EventArgs e)
        {


            string sourceDatabase = textBox1.Text;
            string targetDatabase = Path.Combine(Application.StartupPath, "Resources", "Database", "monitux.db");

            // Copiar el archivo de respaldo a la ubicación de la base de datos original
            File.Copy(sourceDatabase, targetDatabase, true);

            V_Menu_Principal.MSG.ShowMSG("Respaldo importado correctamente.", "Éxito");

            Application.Restart();


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Archivos de base de datos (*.sql)|*.sql";
            openFileDialog1.Title = "Selecciona un archivo de respaldo .sql";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFileDialog1.FileName;
            }


        }

        private void button11_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Archivos de base de datos (*.bak)|*.bak";
            openFileDialog1.Title = "Selecciona un archivo de respaldo .bak";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = openFileDialog1.FileName;
            }


        }

        private void button8_Click(object sender, EventArgs e)
        {

            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath != "")
            {
                textBox4.Text = folderBrowserDialog1.SelectedPath;
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath != "")
            {
                textBox6.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {


            //////////


            string connectionString = Properties.Settings.Default.DB_CONNECTION;




            // Ruta para respaldo de base de datos MySQL
            string nombreArchivoMySQL = $"{DateTime.Now:dd-MM-yyyy}-Monitux-MySQL.sql";
            string rutaRespaldoMySQL = Path.Combine(textBox4.Text, nombreArchivoMySQL);




            using var conn = new MySqlConnection(connectionString);
            using var cmd = new MySqlCommand();
            using var mb = new MySqlBackup(cmd);

            cmd.Connection = conn;
            conn.Open();
            mb.ExportToFile(rutaRespaldoMySQL);
            conn.Close();

            V_Menu_Principal.MSG.ShowMSG("Respaldo creado correctamente en: " + rutaRespaldoMySQL, "Exito");


            /////////




        }

        private void button6_Click(object sender, EventArgs e)
        {


            string connectionString = Properties.Settings.Default.DB_CONNECTION;

            using var conn = new MySqlConnection(connectionString);
            using var cmd = new MySqlCommand();
            using var mb = new MySqlBackup(cmd);

            cmd.Connection = conn;
            conn.Open();
            mb.ImportFromFile(textBox3.Text);
            conn.Close();

            V_Menu_Principal.MSG.ShowMSG("Respaldo importado correctamente.", "Éxito");

            Application.Restart();

        }

        private void button9_Click(object sender, EventArgs e)
        {



            //// string nombreArchivoSQL = $"{DateTime.Now:dd-MM-yyyy}-Respaldo-Monitux-SQLServer.bak";

            //string nombreArchivoSQL = "prueba_de_SQL.bak";

            //string rutaRespaldoSQL = Path.Combine(textBox4.Text.Trim(), nombreArchivoSQL);

            //string comando = $"BACKUP DATABASE [monitux] TO DISK = N'{rutaRespaldoSQL}' WITH FORMAT;";

            //try
            //{
            //    using var conn = new SqlConnection(Properties.Settings.Default.DB_CONNECTION);
            //    using var cmd = new SqlCommand(comando, conn);

            //    conn.Open();
            //    cmd.ExecuteNonQuery();
            //    conn.Close();

            //    V_Menu_Principal.MSG.ShowMSG("Respaldo SQL Server creado correctamente en: " + rutaRespaldoSQL, "Éxito");
            //}
            //catch (Exception ex)
            //{
            //    V_Menu_Principal.MSG.ShowMSG($"Error al crear el respaldo: {ex.Message}", "Error");
            //}

            // string rutaRespaldoSQL = @"C:\Respaldo\monitux.bak"; // Asegúrate de que esta carpeta exista


            string nombreArchivoSQL = $"{DateTime.Now:dd-MM-yyyy}-Monitux-SQLServer.bak";

            string rutaRespaldoSQL = Path.Combine(textBox6.Text.Trim(), nombreArchivoSQL);

            string comando = $"BACKUP DATABASE [monitux] TO DISK = N'{rutaRespaldoSQL}' WITH INIT;";

            try
            {
                using var conn = new SqlConnection(Properties.Settings.Default.DB_CONNECTION);
                using var cmd = new SqlCommand(comando, conn);

                conn.Open();
                cmd.ExecuteNonQuery();

                V_Menu_Principal.MSG.ShowMSG("Respaldo SQL Server creado correctamente en: " + rutaRespaldoSQL, "Éxito");
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Error al crear el respaldo: {ex.Message}", "Error");
            }



        }

        private void button10_Click(object sender, EventArgs e)
        {




            string rutaRespaldoSQL = textBox5.Text; // Ruta del archivo .bak
            string nombreBaseDatos = "monitux";

            string comando = $@"
USE master;
ALTER DATABASE [{nombreBaseDatos}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
RESTORE DATABASE [{nombreBaseDatos}]
FROM DISK = N'{rutaRespaldoSQL}'
WITH REPLACE;
ALTER DATABASE [{nombreBaseDatos}] SET MULTI_USER;
";

            try
            {
                using var conn = new SqlConnection(Properties.Settings.Default.DB_CONNECTION);
                using var cmd = new SqlCommand(comando, conn);

                conn.Open();
                cmd.ExecuteNonQuery();

                V_Menu_Principal.MSG.ShowMSG("Restauración completada correctamente desde: " + rutaRespaldoSQL, "Éxito");

                Application.Restart();
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Error al restaurar la base de datos: {ex.Message}", "Error");
            }




        }
    }
}
