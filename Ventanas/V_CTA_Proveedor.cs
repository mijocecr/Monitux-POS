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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Monitux_POS.Ventanas
{
    public partial class V_CTA_Proveedor : Form
    {




        public int Secuencial_Proveedor { get; set; }
        public int Secuencial { get; set; }
        public string Nombre { get; set; }
        public int Secuencial_Factura { get; set; }

        public double Gran_Total { get; set; }




        public V_CTA_Proveedor(int secuencial_proveedor, string nombre)
        {

            InitializeComponent();

            label1.Text = nombre;
            Secuencial_Proveedor = secuencial_proveedor;
            Nombre = nombre;



        }

        private void V_CTA_Proveedor_Load(object sender, EventArgs e)
        {
            label2.Text = label2.Text + " en " + V_Menu_Principal.moneda;
            label3.Text = label3.Text + " en " + V_Menu_Principal.moneda;
            this.Text = "Monitux-POS v." + V_Menu_Principal.VER;
            Configurar_DataGridView();
            Cargar_Datos_CTAS();
        }



        private void Cargar_Datos_CTAS()
        {
            double total_facturas = 0;
            double saldo_pendiente = 0;
            label4.Text = "0";
            label5.Text = "0";
            dataGridView1.Rows.Clear();
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Asegura la existencia de la base de datos

            var ctas_pagar = context.Cuentas_Pagar
                .Where(c =>
                    c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa &&
                    c.Secuencial_Proveedor == this.Secuencial_Proveedor)
                .ToList();

            foreach (var item in ctas_pagar)
            {
                saldo_pendiente += (double)item.Saldo;
                total_facturas += (double)item.Total;
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Fecha,
                    item.Fecha_Vencimiento,
                    item.Gran_Total,
                    item.Saldo,
                    item.Pagado

                );



            }

            label4.Text = Math.Round(saldo_pendiente, 2).ToString();
            label5.Text = Math.Round(total_facturas, 2).ToString();
            // Opcional: marcar las facturas vencidas con saldo
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (DateTime.TryParse(row.Cells["Fecha_Vencimiento"].Value?.ToString(), out DateTime venc) &&
                    decimal.TryParse(row.Cells["Saldo"].Value?.ToString(), out decimal saldo) &&
                    venc < DateTime.Today && saldo > 0)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
        }





        public void Configurar_DataGridView()
        {
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Configurar las columnas del DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Secuencial", "S");
            dataGridView1.Columns.Add("Fecha", "Emitida");
            dataGridView1.Columns.Add("Fecha_Vencimiento", "Vence");
            dataGridView1.Columns.Add("Gran_Total", "Total");
            dataGridView1.Columns[3].DefaultCellStyle.ForeColor = Color.FromArgb(0, 192, 192);
            dataGridView1.Columns.Add("Saldo", "Saldo");
            dataGridView1.Columns[4].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns.Add("Pagado", "Pagado");
            dataGridView1.Columns[5].DefaultCellStyle.ForeColor = Color.DarkGreen;




            dataGridView1.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {




            try
            {




                if (dataGridView1.CurrentRow?.Cells["Gran_Total"]?.Value != null)
                {
                    this.Gran_Total = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Gran_Total"].Value);
                }





                if (dataGridView1.CurrentRow?.Cells["Secuencial"]?.Value != null)
                {
                    this.Secuencial = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Secuencial"].Value);
                }


                if (Secuencial_Proveedor == 0)
                {
                    return;
                }



                V_Abono_Proveedor v_Abono_Proveedor = new V_Abono_Proveedor(Secuencial, Secuencial_Proveedor, label1.Text, this.Gran_Total);



                v_Abono_Proveedor.ShowDialog();
                v_Abono_Proveedor.Proveedor_Nombre = label1.Text;
                

            }
            catch (Exception ex)
            {

                // Nada que decir, solo para evitar que se rompa la aplicación

            }

            Cargar_Datos_CTAS();




        }
    }
}
