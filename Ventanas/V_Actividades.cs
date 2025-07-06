using Monitux_POS.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Monitux_POS.Ventanas
{
    public partial class V_Actividades : Form
    {

        public int SU;

        public V_Actividades()
        {
            InitializeComponent();
        }





        public void Configurar_DataGridView()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();  
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Configurar las columnas del DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Secuencial", "S");
            dataGridView1.Columns.Add("Fecha", "Fecha");
            dataGridView1.Columns.Add("Descripcion", "Descripcion");
            //dataGridView1.Columns["Descripcion"].Width = 300; 
            dataGridView1.Columns.Add("Secuencial_Usuario", "SU");
            dataGridView1.Columns.Add("Secuencial_Empresa", "SE");




            dataGridView1.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }





        public void Cargar_Datos()
        {




            dataGridView1.Rows.Clear();



            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe




            // **READ**


            var actividades = context.Actividades
  .Where(p => p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
  .ToList();





            foreach (var item in actividades)
            {





                dataGridView1.Rows.Add(item.Secuencial,

                    item.Fecha,
                        item.Descripcion,
                    item.Secuencial_Usuario,
                    item.Secuencial_Empresa


                );



            }



        }















        private void V_Actividades_Load(object sender, EventArgs e)
        {
            Configurar_DataGridView();
            Cargar_Datos();
            llenar_Combo_Usuario();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Configurar_DataGridView();
            Cargar_Datos();
            llenar_Combo_Usuario();

        }


        public void llenar_Combo_Usuario()
        {

            comboBox1.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe




            var usuarios = context.Usuarios
    .Where(p => (bool)p.Activo && p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
    .ToList();





            foreach (var item in usuarios)
            {
                comboBox1.Items.Add(item.Secuencial + " - " + item.Nombre);



            }

            comboBox1.SelectedIndex = 0;










        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /////////////////////

            int SU = int.Parse(comboBox1.SelectedItem.ToString().Split('-')[0].Trim());

            Configurar_DataGridView();

            DateTime inicio = fecha_inicio.Value.Date;
            DateTime fin = fecha_fin.Value.Date;

            dataGridView1.Rows.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


           

            var actividades = context.Actividades
                .Where(p => p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa
                    && p.Secuencial_Usuario == SU)
                .ToList() // Traemos los datos a memoria
                .Where(p => DateTime.TryParse(p.Fecha, out var fecha)
                    && fecha >= inicio&& fecha<=fin)
                .ToList();




            // Mostrar resultados en el DataGridView
            foreach (var item in actividades)
            {
                DateTime.TryParse(item.Fecha, out var fechaFormateada);

                dataGridView1.Rows.Add(
                    item.Secuencial,
                    fechaFormateada.ToString("dd/MM/yyyy"),
                    item.Descripcion,
                    item.Secuencial_Usuario,
                    item.Secuencial_Empresa
                );
            }



            /////////////////////
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
