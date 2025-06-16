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

namespace Monitux_POS.Ventanas
{
    public partial class V_Importar_Orden : Form
    {

        public int Secuencial { get; set; }
        public static Dictionary<string, double> Lista = new Dictionary<string, double>();
        public static string proveedor_seleccionado;
        public V_Importar_Orden()
        {
            InitializeComponent();
        }



        public void llenar_Combo_Proveedor()
        {


            comboCliente.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar solo clientes activos
            var clientesActivos = context.Proveedores.Where(c => (bool)c.Activo).ToList();

            foreach (var item in clientesActivos)
            {
                comboCliente.Items.Add(item.Secuencial + " - " + item.Nombre);
            }






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




        public void Configurar_DataGridView_Orden()
        {
            // Configurar las columnas del DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Secuencial", "S");
            dataGridView1.Columns.Add("Fecha", "Fecha");
            dataGridView1.Columns.Add("Total", "Total");
            dataGridView1.Columns.Add("Gran_Total", "Gran Total");
            dataGridView1.Columns.Add("Secuencial_Proveedor", "SP");


            dataGridView1.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }




        private void Cargar_Datos_Orden()
        {
            dataGridView1.Rows.Clear();
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            var cotizacion = context.Ordenes.ToList();


            foreach (var item in cotizacion)
            {
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Fecha,
                    item.Total,
                    item.Gran_Total, 2,
                    item.Secuencial_Proveedor


                );


            }


        }





        private void V_Importar_Orden_Load(object sender, EventArgs e)
        {
            this.Text = "Monitux POS ver." + V_Menu_Principal.VER; // Establece el título del formulario
            llenar_Combo_Proveedor();
            Configurar_DataGridView_Detalle();
            Configurar_DataGridView_Orden();
            Cargar_Datos_Orden();
            if (comboCliente.Items.Count > 0)
            {
                // Seleccionar el primer cliente por defecto
                comboCliente.SelectedIndex = 0;
            }




        }

        private void button2_Click(object sender, EventArgs e)
        {

            Util.Limpiar_Cache();
           // V_Factura_Compra.button5.Enabled = true;
            this.Dispose();

        }

        private void button1_Click(object sender, EventArgs e)
        {



            Lista.Clear();
            V_Factura_Compra.Lista_de_Items.Clear();
            if (dataGridView1.Rows.Count == 0)
            {
                V_Menu_Principal.MSG.ShowMSG("No hay ordenes disponibles para importar.", "Error");
                return;
            }

            var opt = V_Menu_Principal.MSG.ShowMSG("¿Desea importar la orden seleccionada?\nAdvertencia: Esta se eliminara de los registros.", "Importar Orden");

            if (opt == DialogResult.Yes)
            {
                proveedor_seleccionado = comboCliente.SelectedItem.ToString();
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

                var orden = context.Ordenes.FirstOrDefault(p => p.Secuencial == this.Secuencial);

                if (orden != null)
                {
                    context.Ordenes.Remove(orden);
                    context.SaveChanges();



                }



                SQLitePCL.Batteries.Init();

                using var context1 = new Monitux_DB_Context();
                context1.Database.EnsureCreated();

                var orden_detalle = context1.Ordenes_Detalles.Where(p => p.Secuencial_Orden == this.Secuencial).ToList();

                if (orden_detalle.Any()) // Verifica si hay elementos en la lista
                {
                    context1.Ordenes_Detalles.RemoveRange(orden_detalle); // Elimina múltiples registros
                    context1.SaveChanges();
                }





                V_Menu_Principal.MSG.ShowMSG("Orden importada correctamente.", "Importación Exitosa");


                this.Dispose();
            }




        }





        private void Filtrar_Detalle(string campo, string valor)
        {





            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();



            string columnaSeleccionada = campo;

            var cotizacion_detalle = context.Ordenes_Detalles
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

                   item.Secuencial_Producto, item.Tipo);
            }


        }






        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {






            try
            {





                if (dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value != null)
                {
                    this.Secuencial = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value);

                    Filtrar_Detalle("Secuencial_Orden", this.Secuencial.ToString());

                    if (dataGridView1.Rows[e.RowIndex].Cells["Secuencial_Proveedor"].Value != null)
                    {
                        var valorBuscado = dataGridView1.Rows[e.RowIndex].Cells["Secuencial_Proveedor"].Value.ToString();
                        comboCliente.SelectedItem = comboCliente.Items.Cast<string>().FirstOrDefault(item => item.Contains(valorBuscado));
                    }
                }







            }
            catch (Exception ex)
            {

            }




        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {






            try
            {





                if (dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value != null)
                {
                    this.Secuencial = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value);

                    Filtrar_Detalle("Secuencial_Orden", this.Secuencial.ToString());

                }







            }
            catch (Exception ex)
            {

            }





        }



        private void Filtrar_Orden(string campo, string valor)
        {





            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();



            string columnaSeleccionada = campo;

            var orden = context.Ordenes
                    .Where(c => EF.Property<string>(c, columnaSeleccionada).Contains(valor))
                    .ToList();

            dataGridView1.Rows.Clear();
            foreach (var item in orden)
            {
                dataGridView1.Rows.Add(item.Secuencial,
                    item.Fecha,
                    item.Total,
                    item.Gran_Total,
                    item.Secuencial_Proveedor



                );
            }


            //-------------------Filtro que usare






            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selecciona toda la fila

            // Agregar columnas si no existen
            if (dataGridView1.Columns.Count == 0)
            {
                Configurar_DataGridView_Orden();


            }

            // Limpiar filas antes de agregar nuevas
            dataGridView1.Rows.Clear();

            foreach (var item in orden)
            {


                dataGridView1.Rows.Add(item.Secuencial,
                   item.Fecha,
                  item.Total,
                  item.Gran_Total,
                  item.Secuencial_Proveedor


               );

            }


            if (dataGridView1.Rows.Count == 0)
            {
                dataGridView2.Rows.Clear();
                return;
            }


        }





        private void comboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtrar_Orden("Secuencial_Proveedor", comboCliente.SelectedItem.ToString().Split('-')[0].Trim());
        }
    }
}
