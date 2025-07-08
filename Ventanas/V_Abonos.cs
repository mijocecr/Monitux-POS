using Monitux_POS.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitux_POS.Ventanas
{
    public partial class V_Abonos : Form
    {

        public int Secuencial_CTASP { get; set; }
        public int Secuencial_CTASC { get; set; }

        public bool ventas=false;

        public string Nombre {  get; set; }
        public int Numero { get; set; }

        public V_Abonos(int secuencial,bool ventas,string nombre,int numero)
        {
            this.Nombre = nombre;
            this.ventas = ventas;
            this.Numero = numero;
            this.Secuencial_CTASC = secuencial;
            this.Secuencial_CTASP = secuencial;

            

            InitializeComponent();
        }



        public void Configurar_DataGridView()
        {
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Configurar las columnas del DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Secuencial", "S");
            dataGridView1.Columns.Add("Fecha", "Fecha");
            dataGridView1.Columns.Add("Monto", "Monto");
            



            dataGridView1.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }



        private void Cargar_Datos_CTASP()
        {
           
            dataGridView1.Rows.Clear();
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Asegura la existencia de la base de datos

            var abonos_compras = context.Abonos_Compras
                .Where(c =>
                    c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa &&
                    c.Secuencial_CTAP== this.Secuencial_CTASP)
                .ToList();

            foreach (var item in abonos_compras)
            {
                
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Fecha,
                    item.Monto
                 

                );



            }

          label6.Text=dataGridView1.RowCount.ToString() + " Pagos";
            
        }





        private void Cargar_Datos_CTASC()
        {

            dataGridView1.Rows.Clear();
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Asegura la existencia de la base de datos

            var abonos_ventas = context.Abonos_Ventas
                .Where(c =>
                    c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa &&
                    c.Secuencial_CTAC == this.Secuencial_CTASC)
                .ToList();

            foreach (var item in abonos_ventas)
            {

                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Fecha,
                    item.Monto


                );



            }

            label6.Text = dataGridView1.RowCount.ToString() + " Abonos";

        }






        private void V_Abonos_Load(object sender, EventArgs e)
        {
            label2.Text = Nombre;
            label1.Text = "Factura: "+Numero.ToString();
            
            this.Text = "Monitux-POS v." + V_Menu_Principal.VER;
            Configurar_DataGridView();

            if (!ventas)
            {
                Cargar_Datos_CTASP();
                
            }
            else
            {
                
                Cargar_Datos_CTASC();
            }
            
        }
    }
}
