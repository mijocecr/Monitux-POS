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
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Monitux_POS.Ventanas
{
    public partial class V_Importar_Cotizacion : Form
    {

        public int Secuencial { get; set; }
       public  static Dictionary<string, double> Lista= new Dictionary<string, double>();
        public static string cliente_seleccionado;
        public V_Importar_Cotizacion()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Util.Limpiar_Cache();
           // V_Factura_Venta.button5.Enabled=true;
            this.Dispose();
        }


        public void llenar_Combo_Cliente()
        {


            comboCliente.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar solo clientes activos
            var clientesActivos = context.Clientes.Where(c => (bool)c.Activo).ToList();

            foreach (var item in clientesActivos)
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
            dataGridView1.Columns.Add("Secuencial_Cliente", "SC");


            dataGridView1.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }



        public void Configurar_DataGridView_Detalle()
        {
            // Configurar las columnas del DataGridView
            dataGridView2.Enabled = true;

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
            dataGridView2.Columns.Add("Tipo", "Tipo");

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
                    item.Gran_Total, 2,
                    item.Secuencial_Cliente


                );


            }


        }






        private void V_Importar_Cotizacion_Load(object sender, EventArgs e)
        {
            llenar_Combo_Cliente();
            Configurar_DataGridView_Detalle();
            Configurar_DataGridView_Cotizacion();
            Cargar_Datos_Cotizacion();
            if (comboCliente.Items.Count > 0)
            {
                // Seleccionar el primer cliente por defecto
                comboCliente.SelectedIndex = 0;
            }
            
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {


            comboCliente.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar solo clientes activos cuyo teléfono contiene el texto ingresado
            var clientes = context.Clientes
                .Where(c => (bool)c.Activo && EF.Property<string>(c, "Telefono").Contains(textBox2.Text))
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

                    if (dataGridView1.Rows[e.RowIndex].Cells["Secuencial_Cliente"].Value != null)
                    {
                        var valorBuscado = dataGridView1.Rows[e.RowIndex].Cells["Secuencial_Cliente"].Value.ToString();
                        comboCliente.SelectedItem = comboCliente.Items.Cast<string>().FirstOrDefault(item => item.Contains(valorBuscado));
                    }
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

                    item.Secuencial_Producto,
                    item.Tipo




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

                   item.Secuencial_Producto,item.Tipo);
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
                    item.Total,
                    item.Gran_Total,
                    item.Secuencial_Cliente



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
                  item.Total,
                  item.Gran_Total,
                  item.Secuencial_Cliente


               );

            }


            if (dataGridView1.Rows.Count == 0)
            {
               dataGridView2.Rows.Clear();
                return;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Lista.Clear();
            V_Factura_Venta.Lista_de_Items.Clear();
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay cotizaciones disponibles para importar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var opt = MessageBox.Show("¿Desea importar la cotización seleccionada?\nAdvertencia: Esta se eliminara de los registros.", "Importar Cotización", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (opt == DialogResult.Yes)
            {
                cliente_seleccionado = comboCliente.SelectedItem.ToString();
                Lista.Clear(); // Limpiar la lista antes de importar
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (!row.IsNewRow && row.Cells["Codigo"].Value != null && row.Cells["Cantidad"].Value != null)
                    {
                        string codigo = row.Cells["Codigo"].Value.ToString();

                        if (!Lista.ContainsKey(codigo) && double.TryParse(row.Cells["Cantidad"].Value.ToString(), out double cantidad))
                        {
                            Lista.Add(codigo, cantidad);
                        }
                    }
                }

                SQLitePCL.Batteries.Init();

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated(); // Crea la base de datos si no existe

                var cotizacion = context.Cotizaciones.FirstOrDefault(p => p.Secuencial == this.Secuencial);

                if (cotizacion != null)
                {
                    context.Cotizaciones.Remove(cotizacion);
                    context.SaveChanges();



                }



                SQLitePCL.Batteries.Init();

                using var context1 = new Monitux_DB_Context();
                context1.Database.EnsureCreated();

                var cotizacion_detalle = context1.Cotizaciones_Detalles.Where(p => p.Secuencial_Cotizacion == this.Secuencial).ToList();

                if (cotizacion_detalle.Any()) // Verifica si hay elementos en la lista
                {
                    context1.Cotizaciones_Detalles.RemoveRange(cotizacion_detalle); // Elimina múltiples registros
                    context1.SaveChanges();
                }




                
               MessageBox.Show("Cotización importada correctamente.", "Importación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                
                this.Dispose();
            }
            
        }
    }
}
