using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
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
            //string dbPath = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Database\\monitux.db");

            string sourceDatabase = Path.Combine(Application.StartupPath, "Resources", "Database", "monitux.db");

            string backupDatabase = Path.Combine(textBox2.Text, $"{DateTime.Today.Date.ToString("dd-MM-yyyy")}-Respaldo-Monitux.db");


            // Copiar el archivo de la base de datos a la ubicación de respaldo
            File.Copy(sourceDatabase, backupDatabase, true);
            V_Menu_Principal.MSG.ShowMSG("Respaldo realizado correctamente.", "Éxito");

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
    }
}
