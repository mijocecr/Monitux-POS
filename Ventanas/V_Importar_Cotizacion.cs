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
    public partial class V_Importar_Cotizacion : Form
    {
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

        public void Configurar_DataGridView_Detalle()
        {
            // Configurar las columnas del DataGridView
            dataGridView2.Columns.Clear();
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

        private void V_Importar_Cotizacion_Load(object sender, EventArgs e)
        {
            llenar_Combo_Cliente();
            Configurar_DataGridView_Detalle();  
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
    }
}
