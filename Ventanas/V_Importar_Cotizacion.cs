using Microsoft.EntityFrameworkCore;
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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Monitux_POS.Ventanas
{
    public partial class V_Importar_Cotizacion : Form
    {

        public int Secuencial { get; set; }
        public V_Importar_Cotizacion()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        public void llenar_Combo_Cliente()
        {

            comboCliente.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            var clientes = context.Clientes.ToList();








            foreach (var item in clientes)
            {
                comboCliente.Items.Add(item.Secuencial + " - " + item.Nombre);



            }




        }




        public void Configurar_DataGridView_Cotizacion()
        {
            // Configurar las columnas del DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Secuencial", "S");
            dataGridView1.Columns.Add("Fecha", "Fecha");
            dataGridView1.Columns.Add("Total", "Total");
            dataGridView1.Columns.Add("Gran_Total", "Gran Total");


            dataGridView1.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }



        public void Configurar_DataGridView_Detalle()
        {
            // Configurar las columnas del DataGridView
            dataGridView2.Enabled=true;

            dataGridView2.DefaultCellStyle.SelectionBackColor = dataGridView2.DefaultCellStyle.BackColor;
            dataGridView2.DefaultCellStyle.SelectionForeColor = dataGridView2.DefaultCellStyle.ForeColor;



            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("Secuencial", "S");
            dataGridView2.Columns.Add("Codigo", "Código");
            dataGridView2.Columns.Add("Descripcion", "Descripción");
            dataGridView2.Columns.Add("Cantidad", "Cantidad");
            dataGridView2.Columns.Add("Precio_Venta", "Precio");

            dataGridView2.Columns.Add("Total", "Total");
            dataGridView2.Columns.Add("Secuencial_Producto", "SP");
            dataGridView2.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.ReadOnly = true;
        }







        private void Cargar_Datos_Cotizacion()
        {
            dataGridView1.Rows.Clear();
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            var cotizacion = context.Cotizaciones.ToList();


            foreach (var item in cotizacion)
            {
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Fecha,
                    item.Total,
                    Math.Round((double)item.Gran_Total, 2)


                );


            }


        }






        private void V_Importar_Cotizacion_Load(object sender, EventArgs e)
        {
            llenar_Combo_Cliente();
            Configurar_DataGridView_Detalle();
            Configurar_DataGridView_Cotizacion();
            Cargar_Datos_Cotizacion();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            comboCliente.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            var clientes = context.Clientes
                   .Where(c => EF.Property<string>(c, "Telefono").Contains(textBox2.Text))
                   .ToList();

            foreach (var item in clientes)
            {
                comboCliente.Items.Add(item.Secuencial.ToString() + " - " + item.Nombre);
                comboCliente.SelectedItem = item.Secuencial.ToString() + " - " + item.Nombre;

            }



        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {






            try
            {





                if (dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value != null)
                {
                    this.Secuencial = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value);

                    Filtrar_Detalle("Secuencial_Cotizacion", this.Secuencial.ToString());

                }







            }
            catch (Exception ex)
            {

            }





        }




        private void Filtrar_Detalle(string campo, string valor)
        {





            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();



            string columnaSeleccionada = campo;

            var cotizacion_detalle = context.Cotizaciones_Detalles
                    .Where(c => EF.Property<string>(c, columnaSeleccionada).Contains(valor))
                    .ToList();

            dataGridView2.Rows.Clear();
            foreach (var item in cotizacion_detalle)
            {
                dataGridView2.Rows.Add(item.Secuencial,
                    item.Codigo,
                    item.Descripcion,
                    item.Cantidad,
                    item.Precio,
                    Math.Round((double)item.Total, 2),

                    item.Secuencial_Producto


                );
            }


            //-------------------Filtro que usare






            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selecciona toda la fila

            // Agregar columnas si no existen
            if (dataGridView2.Columns.Count == 0)
            {
                Configurar_DataGridView_Detalle();


            }

            // Limpiar filas antes de agregar nuevas
            dataGridView2.Rows.Clear();

            foreach (var item in cotizacion_detalle)
            {



                dataGridView2.Rows.Add(item.Secuencial,
                   item.Codigo,
                   item.Descripcion,
                   item.Cantidad,
                   item.Precio,
                   item.Total,

                   item.Secuencial_Producto);
            }


        }




        private void Filtrar_Cotizacion(string campo, string valor)
        {


            


            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();



            string columnaSeleccionada = campo;

            var cotizacion = context.Cotizaciones
                    .Where(c => EF.Property<string>(c, columnaSeleccionada).Contains(valor))
                    .ToList();

            dataGridView1.Rows.Clear();
            foreach (var item in cotizacion)
            {
                dataGridView1.Rows.Add(item.Secuencial,
                    item.Fecha,
                    Math.Round((double)item.Total,2),
                    Math.Round((double)item.Gran_Total,2)
                  

                );
            }


            //-------------------Filtro que usare






            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selecciona toda la fila

            // Agregar columnas si no existen
            if (dataGridView1.Columns.Count == 0)
            {
                Configurar_DataGridView_Cotizacion();


            }

            // Limpiar filas antes de agregar nuevas
            dataGridView1.Rows.Clear();

            foreach (var item in cotizacion)
            {


                dataGridView1.Rows.Add(item.Secuencial,
                   item.Fecha,
                  Math.Round((double) item.Total,2),
                  Math.Round((double) item.Gran_Total,2)


               );

            }


        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {




            try
            {





                if (dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value != null)
                {
                    this.Secuencial = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value);

                    Filtrar_Detalle("Secuencial_Cotizacion", this.Secuencial.ToString());

                }







            }
            catch (Exception ex)
            {

            }




        }

        private void comboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtrar_Cotizacion("Secuencial_Cliente", comboCliente.SelectedItem.ToString().Split('-')[0].Trim());
        }
    }
}
