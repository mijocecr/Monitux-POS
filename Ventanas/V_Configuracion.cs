using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

                case "postgres":
                    panel1.Visible = false;
                    panel2.Visible = false;
                    panel3.Visible = false;
                    panel4.Visible = true;
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

        private async void button4_Click(object sender, EventArgs e)
        {
            /*
                        // Ruta de la base de datos original (SQLite)
                        string rutaBaseDatosOriginal = Path.Combine(Application.StartupPath, "Resources", "Database", "monitux.db");

                        // Ruta de respaldo para SQLite
                        string nombreRespaldoSQLite = $"{DateTime.Today:dd-MM-yyyy}-Monitux.db";
                        string rutaRespaldoSQLite = Path.Combine(textBox2.Text, nombreRespaldoSQLite);


                        // Copiar el archivo de la base de datos SQLite a la ubicación de respaldo
                        File.Copy(rutaBaseDatosOriginal, rutaRespaldoSQLite, overwrite: true);

                        // Mostrar mensaje de éxito
                        V_Menu_Principal.MSG.ShowMSG($"Respaldo creado correctamente en:  {rutaRespaldoSQLite}", "Éxito");

                        */

            button4.Enabled = false;
            lblEstado.Text = "Iniciando respaldo SQLite...";
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.Visible = true;
            lblEstado.Visible = true;

            var stopwatch = Stopwatch.StartNew();

            await Task.Run(() =>
            {
                try
                {
                    string rutaBaseDatosOriginal = Path.Combine(Application.StartupPath, "Resources", "Database", "monitux.db");
                    string nombreRespaldoSQLite = $"{DateTime.Today:dd-MM-yyyy}-Monitux.db";
                    string rutaRespaldoSQLite = Path.Combine(textBox2.Text.Trim(), nombreRespaldoSQLite);

                    if (!File.Exists(rutaBaseDatosOriginal))
                        throw new FileNotFoundException("No se encontró la base de datos original en la ruta esperada.");

                    File.Copy(rutaBaseDatosOriginal, rutaRespaldoSQLite, overwrite: true);

                    stopwatch.Stop();

                    this.Invoke(() =>
                    {
                        V_Menu_Principal.MSG.ShowMSG(
                            $"Respaldo SQLite creado correctamente en:\n{rutaRespaldoSQLite}\n\nTiempo total: {stopwatch.Elapsed.TotalSeconds:F1} segundos",
                            "Éxito");
                    });
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();

                    this.Invoke(() =>
                    {
                        V_Menu_Principal.MSG.ShowMSG(
                            $"Error al crear el respaldo:\n{ex.Message}\n\nTiempo transcurrido: {stopwatch.Elapsed.TotalSeconds:F1} segundos",
                            "Error");
                    });
                }
            });

            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Value = 100;
            lblEstado.Text = "Respaldo SQLite completado ✅";
            button4.Enabled = true;


        }

        private async void button3_Click(object sender, EventArgs e)
        {


            //string sourceDatabase = textBox1.Text;
            //string targetDatabase = Path.Combine(Application.StartupPath, "Resources", "Database", "monitux.db");

            //// Copiar el archivo de respaldo a la ubicación de la base de datos original
            //File.Copy(sourceDatabase, targetDatabase, true);

            //V_Menu_Principal.MSG.ShowMSG("Respaldo importado correctamente.", "Éxito");

            //Application.Restart();



            
                button3.Enabled = false;
                progressBar1.Visible = true;
                lblEstado.Visible = true;
                lblEstado.Text = "Iniciando restauración SQLite...";
                progressBar1.Style = ProgressBarStyle.Marquee;

                var stopwatch = Stopwatch.StartNew();

                await Task.Run(() =>
                {
                    try
                    {
                        string sourceDatabase = textBox1.Text.Trim(); // Ruta del archivo de respaldo
                        string targetDatabase = Path.Combine(Application.StartupPath, "Resources", "Database", "monitux.db");

                        if (!File.Exists(sourceDatabase))
                            throw new FileNotFoundException("El archivo de respaldo no existe. Verifica la ruta.");

                        File.Copy(sourceDatabase, targetDatabase, overwrite: true);

                        stopwatch.Stop();

                        this.Invoke(() =>
                        {
                            V_Menu_Principal.MSG.ShowMSG(
                                $"Respaldo SQLite importado correctamente.\n\nTiempo total: {stopwatch.Elapsed.TotalSeconds:F1} segundos",
                                "Éxito");
                            Application.Restart();
                        });
                    }
                    catch (Exception ex)
                    {
                        stopwatch.Stop();

                        this.Invoke(() =>
                        {
                            MessageBox.Show($"Error al restaurar la base de datos:\n{ex.Message}\n\nTiempo transcurrido: {stopwatch.Elapsed.TotalSeconds:F1} segundos", "Error");
                        });
                    }
                });

                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 100;
                lblEstado.Text = "Restauración SQLite completada ✅";
                button3.Enabled = true;


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

        private async void button5_Click(object sender, EventArgs e)
        {

            /*
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

                        */


            {
                progressBar1.Visible = true;
                lblEstado.Visible = true;
                button5.Enabled = false;
                lblEstado.Text = "Iniciando respaldo MySQL...";
                progressBar1.Style = ProgressBarStyle.Marquee;

                var stopwatch = Stopwatch.StartNew();

                await Task.Run(() =>
                {
                    string connectionString = Properties.Settings.Default.DB_CONNECTION;
                    string nombreArchivoMySQL = $"{DateTime.Now:dd-MM-yyyy}-Monitux-MySQL.sql";
                    string rutaRespaldoMySQL = Path.Combine(textBox4.Text.Trim(), nombreArchivoMySQL);

                    try
                    {
                        using var conn = new MySqlConnection(connectionString);
                        using var cmd = new MySqlCommand
                        {
                            Connection = conn,
                            CommandTimeout = 60 // ⏱️ Tiempo máximo de espera en segundos
                        };
                        using var mb = new MySqlBackup(cmd);

                        conn.Open();
                        mb.ExportToFile(rutaRespaldoMySQL);
                        conn.Close();

                        stopwatch.Stop();

                        this.Invoke(() =>
                        {
                            V_Menu_Principal.MSG.ShowMSG(
                                $"Respaldo MySQL creado correctamente en:\n{rutaRespaldoMySQL}\n\nTiempo total: {stopwatch.Elapsed.TotalSeconds:F1} segundos",
                                "Éxito");
                        });
                    }
                    catch (Exception ex)
                    {
                        stopwatch.Stop();

                        this.Invoke(() =>
                        {
                            V_Menu_Principal.MSG.ShowMSG(
                                $"Error al crear el respaldo:\n{ex.Message}\n\nTiempo transcurrido: {stopwatch.Elapsed.TotalSeconds:F1} segundos",
                                "Error");
                        });
                    }
                });

                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 100;
                lblEstado.Text = "Respaldo MySQL completado ✅";
                button5.Enabled = true;
           
            }

            }

        private async void button6_Click(object sender, EventArgs e)
        {


            //string connectionString = Properties.Settings.Default.DB_CONNECTION;

            //using var conn = new MySqlConnection(connectionString);
            //using var cmd = new MySqlCommand();
            //using var mb = new MySqlBackup(cmd);

            //cmd.Connection = conn;
            //conn.Open();
            //mb.ImportFromFile(textBox3.Text);
            //conn.Close();

            //V_Menu_Principal.MSG.ShowMSG("Respaldo importado correctamente.", "Éxito");

            //Application.Restart();


            
                button6.Enabled = false;
                progressBar1.Visible = true;
                lblEstado.Visible = true;
                lblEstado.Text = "Iniciando restauración MySQL...";
                progressBar1.Style = ProgressBarStyle.Marquee;

                var stopwatch = Stopwatch.StartNew();

                await Task.Run(() =>
                {
                    string connectionString = Properties.Settings.Default.DB_CONNECTION;
                    string rutaRespaldo = textBox3.Text.Trim(); // Ruta del archivo .sql

                    if (!File.Exists(rutaRespaldo))
                    {
                        this.Invoke(() =>
                        {
                            MessageBox.Show("El archivo de respaldo no existe. Verifica la ruta.", "Error");
                        });
                        return;
                    }

                    try
                    {
                        using var conn = new MySqlConnection(connectionString);
                        using var cmd = new MySqlCommand
                        {
                            Connection = conn,
                            CommandTimeout = 120 // ⏱️ Tiempo máximo de espera en segundos
                        };
                        using var mb = new MySqlBackup(cmd);

                        conn.Open();
                        mb.ImportFromFile(rutaRespaldo);
                        conn.Close();

                        stopwatch.Stop();

                        this.Invoke(() =>
                        {
                            V_Menu_Principal.MSG.ShowMSG(
                                $"Respaldo MySQL importado correctamente.\n\nTiempo total: {stopwatch.Elapsed.TotalSeconds:F1} segundos",
                                "Éxito");
                            Application.Restart();
                        });
                    }
                    catch (Exception ex)
                    {
                        stopwatch.Stop();

                        this.Invoke(() =>
                        {
                            MessageBox.Show($"Error al restaurar la base de datos:\n{ex.Message}\n\nTiempo transcurrido: {stopwatch.Elapsed.TotalSeconds:F1} segundos", "Error");
                        });
                    }
                });

                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 100;
                lblEstado.Text = "Restauración MySQL completada ✅";
                button6.Enabled = true;





            }

        private async void button9_Click(object sender, EventArgs e)
        {




            /*

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
            }*/

            button9.Enabled = false;
            progressBar1.Visible = true;
            lblEstado.Visible = true;
            lblEstado.Text = "Iniciando respaldo SQL Server...";
            progressBar1.Style = ProgressBarStyle.Marquee;

            var stopwatch = Stopwatch.StartNew();

            await Task.Run(() =>
            {
                string nombreArchivoSQL = $"{DateTime.Now:dd-MM-yyyy}-Monitux-SQLServer.bak";
                string rutaRespaldoSQL = Path.Combine(textBox6.Text.Trim(), nombreArchivoSQL);
                string comando = $"BACKUP DATABASE [monitux] TO DISK = N'{rutaRespaldoSQL}' WITH INIT;";

                try
                {
                    using var conn = new SqlConnection(Properties.Settings.Default.DB_CONNECTION);
                    using var cmd = new SqlCommand(comando, conn)
                    {
                        CommandTimeout = 60 // ⏱️ Tiempo máximo de espera en segundos
                    };

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    stopwatch.Stop();

                    this.Invoke(() =>
                    {
                        V_Menu_Principal.MSG.ShowMSG(
                            $"Respaldo SQL Server creado correctamente en:\n{rutaRespaldoSQL}\n\nTiempo total: {stopwatch.Elapsed.TotalSeconds:F1} segundos",
                            "Éxito");
                    });
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();

                    this.Invoke(() =>
                    {
                        V_Menu_Principal.MSG.ShowMSG(
                            $"Error al crear el respaldo:\n{ex.Message}\n\nTiempo transcurrido: {stopwatch.Elapsed.TotalSeconds:F1} segundos",
                            "Error");
                    });
                }
            });

            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Value = 100;
            lblEstado.Text = "Respaldo SQL Server completado ✅";
            button9.Enabled = true;

        }

        private async void button10_Click(object sender, EventArgs e)
        {




            //            string rutaRespaldoSQL = textBox5.Text; // Ruta del archivo .bak
            //            string nombreBaseDatos = "monitux";

            //            string comando = $@"
            //USE master;
            //ALTER DATABASE [{nombreBaseDatos}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
            //RESTORE DATABASE [{nombreBaseDatos}]
            //FROM DISK = N'{rutaRespaldoSQL}'
            //WITH REPLACE;
            //ALTER DATABASE [{nombreBaseDatos}] SET MULTI_USER;
            //";

            //            try
            //            {
            //                using var conn = new SqlConnection(Properties.Settings.Default.DB_CONNECTION);
            //                using var cmd = new SqlCommand(comando, conn);

            //                conn.Open();
            //                cmd.ExecuteNonQuery();

            //                V_Menu_Principal.MSG.ShowMSG("Restauración completada correctamente desde: " + rutaRespaldoSQL, "Éxito");

            //                Application.Restart();
            //            }
            //            catch (Exception ex)
            //            {
            //                V_Menu_Principal.MSG.ShowMSG($"Error al restaurar la base de datos: {ex.Message}", "Error");
            //            }





            
                button10.Enabled = false;
                progressBar1.Visible = true;
                lblEstado.Visible = true;
                lblEstado.Text = "Iniciando restauración SQL Server...";
                progressBar1.Style = ProgressBarStyle.Marquee;

                var stopwatch = Stopwatch.StartNew();

                await Task.Run(() =>
                {
                    string rutaRespaldoSQL = textBox5.Text.Trim(); // Ruta del archivo .bak
                    string nombreBaseDatos = "monitux";

                    if (!File.Exists(rutaRespaldoSQL))
                    {
                        this.Invoke(() =>
                        {
                            MessageBox.Show("El archivo de respaldo no existe. Verifica la ruta.", "Error");
                        });
                        return;
                    }

                    string comando = $@"
            USE master;
            ALTER DATABASE [{nombreBaseDatos}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
            RESTORE DATABASE [{nombreBaseDatos}] FROM DISK = N'{rutaRespaldoSQL}' WITH REPLACE;
            ALTER DATABASE [{nombreBaseDatos}] SET MULTI_USER;
        ";

                    try
                    {
                        using var conn = new SqlConnection(Properties.Settings.Default.DB_CONNECTION);
                        using var cmd = new SqlCommand(comando, conn)
                        {
                            CommandTimeout = 120 // ⏱️ Tiempo máximo de espera en segundos
                        };

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        stopwatch.Stop();

                        this.Invoke(() =>
                        {
                            V_Menu_Principal.MSG.ShowMSG(
                                $"Restauración SQL Server completada correctamente.\n\nTiempo total: {stopwatch.Elapsed.TotalSeconds:F1} segundos",
                                "Éxito");
                            Application.Restart();
                        });
                    }
                    catch (Exception ex)
                    {
                        stopwatch.Stop();

                        this.Invoke(() =>
                        {
                            MessageBox.Show($"Error al restaurar la base de datos:\n{ex.Message}\n\nTiempo transcurrido: {stopwatch.Elapsed.TotalSeconds:F1} segundos", "Error");
                        });
                    }
                });

                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 100;
                lblEstado.Text = "Restauración SQL Server completada ✅";
                button10.Enabled = true;

            

            }

        private void button15_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Archivos de base de datos (*.backup)|*.backup";
            openFileDialog1.Title = "Selecciona un archivo de respaldo .backup";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox7.Text = openFileDialog1.FileName;
            }


        }

        private void button16_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath != "")
            {
                textBox8.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private async void button14_Click(object sender, EventArgs e)
        {

            string connectionString = Properties.Settings.Default.DB_CONNECTION;
            string backupFilePath = textBox7.Text.Trim(); // Ruta del archivo .backup

            if (!File.Exists(backupFilePath))
            {
                MessageBox.Show("El archivo de respaldo no existe. Verifica la ruta.", "Error");
                return;
            }

            string pgRestorePath = DetectarPgRestore();

            if (pgRestorePath == null)
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "pg_restore.exe|pg_restore.exe",
                    Title = "Selecciona pg_restore.exe"
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pgRestorePath = dialog.FileName;
                }
                else
                {
                    MessageBox.Show("No se seleccionó pg_restore.exe. No se puede continuar.", "Error");
                    return;
                }
            }

            string dbName = "monitux";
            string user = "postgres";
            string host = "localhost";
            string port = "5432";
            string password = "Monitux_POS_POSTGRES";

            Environment.SetEnvironmentVariable("PGPASSWORD", password);

            // ✅ Se añadió --clean y --if-exists para evitar conflictos con objetos existentes
            string argumentos = $"-h {host} -p {port} -U {user} --clean --if-exists -d {dbName} -v \"{backupFilePath}\"";

            var stopwatch = Stopwatch.StartNew();

            await Task.Run(() =>
            {
                try
                {
                    using var proceso = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = pgRestorePath,
                            Arguments = argumentos,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        }
                    };

                    proceso.Start();

                    var salidaTask = proceso.StandardOutput.ReadToEndAsync();
                    var errorTask = proceso.StandardError.ReadToEndAsync();

                    if (!proceso.WaitForExit(60000))
                    {
                        proceso.Kill();
                        throw new TimeoutException("pg_restore excedió el tiempo límite de 60 segundos.");
                    }

                    var salida = salidaTask.Result;
                    var errores = errorTask.Result;

                    stopwatch.Stop();

                    if (proceso.ExitCode == 0)
                    {
                        this.Invoke(() =>
                        {
                            V_Menu_Principal.MSG.ShowMSG(
                                $"Respaldo importado correctamente.\n\nTiempo total: {stopwatch.Elapsed.TotalSeconds:F1} segundos",
                                "Éxito");
                            Application.Restart();
                        });
                    }
                    else
                    {
                        this.Invoke(() =>
                        {
                            MessageBox.Show($"Error al importar respaldo:\n{errores}", "Error");
                        });
                    }
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    this.Invoke(() =>
                    {
                        MessageBox.Show($"Excepción: {ex.Message}\n\nTiempo transcurrido: {stopwatch.Elapsed.TotalSeconds:F1} segundos", "Error");
                    });
                }
            });


        }


        private string DetectarPgRestore()
        {
            string[] posiblesBases = new[]
            {
        @"C:\Program Files\PostgreSQL",
        @"C:\Program Files (x86)\PostgreSQL"
    };

            foreach (var basePath in posiblesBases)
            {
                if (Directory.Exists(basePath))
                {
                    var versiones = Directory.GetDirectories(basePath);
                    foreach (var version in versiones)
                    {
                        var posibleRuta = Path.Combine(version, "bin", "pg_restore.exe");
                        if (File.Exists(posibleRuta))
                            return posibleRuta;
                    }
                }
            }

            return null;
        }


        private async void button13_Click(object sender, EventArgs e)
        {



            //string pgDumpPath = @"C:\Program Files\PostgreSQL\17\bin\pg_dump.exe";
            //string rutaDestino = Path.Combine(textBox8.Text.Trim(), $"{DateTime.Now:yyyyMMdd_HHmmss}-monitux.sql");
            //string rutaLog = Path.Combine(textBox8.Text.Trim(), $"{DateTime.Now:yyyyMMdd_HHmmss}-respaldo_log.txt");
            //string usuario = "postgres";
            //string contraseña = "Monitux_POS_POSTGRES";
            //string baseDatos = "monitux";

            //progressBar1.Visible = true;
            //lblEstado.Visible = true;
            //button13.Enabled = false;
            //progressBar1.Style = ProgressBarStyle.Marquee;
            //lblEstado.Text = "Generando respaldo...";

            //var stopwatch = Stopwatch.StartNew();

            //await Task.Run(() =>
            //{
            //    try
            //    {
            //        Environment.SetEnvironmentVariable("PGPASSWORD", contraseña);

            //        var proceso = new Process
            //        {
            //            StartInfo = new ProcessStartInfo
            //            {
            //                FileName = pgDumpPath,
            //                Arguments = $"-U {usuario} -F p -b -v -f \"{rutaDestino}\" {baseDatos}",
            //                UseShellExecute = false,
            //                RedirectStandardOutput = true,
            //                RedirectStandardError = true,
            //                CreateNoWindow = true
            //            }
            //        };

            //        proceso.Start();

            //        // Leer salida en paralelo para evitar bloqueo
            //        var salidaTask = proceso.StandardOutput.ReadToEndAsync();
            //        var errorTask = proceso.StandardError.ReadToEndAsync();

            //        // Esperar máximo 60 segundos
            //        if (!proceso.WaitForExit(60000))
            //        {
            //            proceso.Kill();
            //            throw new TimeoutException("El proceso pg_dump excedió el tiempo límite de 60 segundos.");
            //        }

            //        var salida = salidaTask.Result;
            //        var errores = errorTask.Result;

            //        File.WriteAllText(rutaLog, $"SALIDA:\n{salida}\n\nERRORES:\n{errores}");

            //        if (proceso.ExitCode != 0)
            //            throw new Exception($"pg_dump falló:\n{errores}");

            //        if (!File.Exists(rutaDestino) || new FileInfo(rutaDestino).Length == 0)
            //            throw new Exception("El archivo de respaldo no se creó correctamente o está vacío.");

            //        stopwatch.Stop();

            //        this.Invoke(() =>
            //        {
            //            V_Menu_Principal.MSG.ShowMSG(
            //                $"Respaldo creado correctamente en:\n{rutaDestino}\n\nTiempo total: {stopwatch.Elapsed.TotalSeconds:F1} segundos",
            //                "Éxito");
            //        });
            //    }
            //    catch (Exception ex)
            //    {
            //        stopwatch.Stop();
            //        this.Invoke(() =>
            //        {
            //            V_Menu_Principal.MSG.ShowMSG(
            //                $"Error al crear el respaldo:\n{ex.Message}\n\nTiempo transcurrido: {stopwatch.Elapsed.TotalSeconds:F1} segundos",
            //                "Error");
            //        });
            //    }
            //});

            //progressBar1.Style = ProgressBarStyle.Blocks;
            //progressBar1.Value = 100;
            //lblEstado.Text = "Respaldo PostgreSQL completado ✅";
            //button13.Enabled = true;


            


            string pgDumpPath = DetectarPgDump();
            string rutaDestino = Path.Combine(textBox8.Text.Trim(), $"{DateTime.Now:dd-MM-yyyy}-Monitux-POSTGRES.backup");
            //string rutaDestino = Path.Combine(textBox8.Text.Trim(), $"{DateTime.Now:dd-MM-yyyy}-Monitux-POSTGRES.sql");
            string rutaLog = Path.Combine(textBox8.Text.Trim(), $"{DateTime.Now:yyyyMMdd_HHmmss}-respaldo_log.txt");
            string usuario = "postgres";
            string contraseña = "Monitux_POS_POSTGRES";
            string baseDatos = "monitux";

            progressBar1.Visible = true;
            lblEstado.Visible = true;
            button13.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee;
            lblEstado.Text = "Generando respaldo...";

            var stopwatch = Stopwatch.StartNew();

            await Task.Run(() =>
            {
                try
                {
                    Environment.SetEnvironmentVariable("PGPASSWORD", contraseña);

                    var proceso = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = pgDumpPath,
                            Arguments = $"-U {usuario} -F c -b -v -f \"{rutaDestino}\" {baseDatos}",
                            
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        }
                    };

                    proceso.Start();

                    var salidaTask = proceso.StandardOutput.ReadToEndAsync();
                    var errorTask = proceso.StandardError.ReadToEndAsync();

                    if (!proceso.WaitForExit(60000))
                    {
                        proceso.Kill();
                        throw new TimeoutException("El proceso pg_dump excedió el tiempo límite de 60 segundos.");
                    }

                    var salida = salidaTask.Result;
                    var errores = errorTask.Result;

                    File.WriteAllText(rutaLog, $"SALIDA:\n{salida}\n\nERRORES:\n{errores}");

                    if (proceso.ExitCode != 0)
                        throw new Exception($"pg_dump falló:\n{errores}");

                    if (!File.Exists(rutaDestino) || new FileInfo(rutaDestino).Length == 0)
                        throw new Exception("El archivo de respaldo no se creó correctamente o está vacío.");

                    stopwatch.Stop();

                    this.Invoke(() =>
                    {
                        V_Menu_Principal.MSG.ShowMSG(
                            $"Respaldo creado correctamente en:\n{rutaDestino}\n\nTiempo total: {stopwatch.Elapsed.TotalSeconds:F1} segundos",
                            "Éxito");
                    });
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    this.Invoke(() =>
                    {
                        V_Menu_Principal.MSG.ShowMSG(
                            $"Error al crear el respaldo:\n{ex.Message}\n\nTiempo transcurrido: {stopwatch.Elapsed.TotalSeconds:F1} segundos",
                            "Error");
                    });
                }
            });

            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Value = 100;
            lblEstado.Text = "Respaldo PostgreSQL completado ✅";
            button13.Enabled = true;


        }

        private string DetectarPgDump()
        {
            var posiblesBases = new[]
            {
        @"C:\Program Files\PostgreSQL",
        @"C:\Program Files (x86)\PostgreSQL"
    };

            foreach (var basePath in posiblesBases)
            {
                if (Directory.Exists(basePath))
                {
                    var versiones = Directory.GetDirectories(basePath);
                    foreach (var version in versiones)
                    {
                        var posibleRuta = Path.Combine(version, "bin", "pg_dump.exe");
                        if (File.Exists(posibleRuta))
                            return posibleRuta;
                    }
                }
            }

            throw new FileNotFoundException("No se encontró pg_dump.exe en las rutas estándar.");
        }





    }
}
