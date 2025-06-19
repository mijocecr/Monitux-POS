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
    public partial class V_Kardex : Form
    {

        public static int Secuencial_Producto { get; set; }
        public static string Codigo_Producto { get; set; } 
       


        public V_Kardex()
        {

           
            InitializeComponent();
        }



        public void Configurar_DataGridView(DataGridView dataGrid)
        {


            // Configurar las columnas del DataGridView
            dataGrid.Enabled = true;

            dataGrid.DefaultCellStyle.SelectionBackColor = dataGrid.DefaultCellStyle.BackColor;
            dataGrid.DefaultCellStyle.SelectionForeColor = dataGrid.DefaultCellStyle.ForeColor;



            dataGrid.Columns.Clear();
            dataGrid.Columns.Add("Secuencial", "S");
            dataGrid.Columns.Add("Fecha", "Fecha");
            dataGrid.Columns.Add("Descripcion", "Descripción");
            dataGrid.Columns.Add("Cantidad", "Cantidad");
            dataGrid.Columns.Add("Saldo", "Saldo");



            dataGrid.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGrid.ReadOnly = true;
        }









        private void Filtrar_Kardex(int secuencial_producto,string movimiento, DataGridView dataGrid)
        {





            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();



            

            
                   var kardex = context.Kardex
                    .Where(c => EF.Property<string>(c, "Movimiento").Equals(movimiento)&&c.Secuencial_Producto==secuencial_producto)
                    .ToList();

            dataGrid.Rows.Clear();
            foreach (var item in kardex)
            {
                dataGrid.Rows.Add(item.Secuencial,
                    item.Fecha,
                    item.Descripcion,
                    item.Cantidad,
                    item.Saldo



                );
            }


            //-------------------Filtro que usare






            dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selecciona toda la fila

            // Agregar columnas si no existen
            if (dataGrid.Columns.Count == 0)
            {
                Configurar_DataGridView(dataGrid);


            }

            // Limpiar filas antes de agregar nuevas
            dataGrid.Rows.Clear();

            foreach (var item in kardex)
            {


                dataGrid.Rows.Add(item.Secuencial,
                   item.Fecha,
                  item.Descripcion,
                  item.Cantidad,
                  item.Saldo


               );

            }




        }










        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Kardex_Load(object sender, EventArgs e)
        {
            label6.Text= Codigo_Producto;
            Configurar_DataGridView(dataGridView2);
            Configurar_DataGridView(dataGridView1);
            
            Filtrar_Kardex(Secuencial_Producto,"Entrada", dataGridView1);
            Filtrar_Kardex(Secuencial_Producto, "Salida", dataGridView2);

            label4.Text= dataGridView1.Rows.Count.ToString();
            label5.Text = dataGridView2.Rows.Count.ToString();
        }
    }
}
