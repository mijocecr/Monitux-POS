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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Monitux_POS.Ventanas
{
    public partial class V_CTAS_Cobrar : Form
    {




        public int Secuencial_Cliente { get; set; }
        public int Secuencial { get; set; }
        public string Nombre { get; set; }
        public int Secuencial_Factura { get; set; }

        public double Gran_Total { get; set; }




        public V_CTAS_Cobrar()
        {
            InitializeComponent();
        }






        private void Cargar_Datos_CTAS()
        {
            double total_facturas = 0;
            double saldo_pendiente = 0;
            // label4.Text = "0";
            //label5.Text = "0";
            dataGridView1.Rows.Clear();
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Asegura la existencia de la base de datos

            var resultado = (from cc in context.Cuentas_Cobrar
                             join v in context.Ventas on cc.Secuencial_Factura equals v.Secuencial
                             join c in context.Clientes on cc.Secuencial_Cliente equals c.Secuencial
                             where cc.Saldo > 0
                             select new
                             {
                                 cc.Secuencial,
                                 SecuencialVenta = v.Secuencial,
                                 NombreCliente = c.Nombre,
                                 cc.Fecha,
                                 cc.Fecha_Vencimiento,
                                 cc.Gran_Total,
                                 cc.Saldo,
                                 cc.Pagado,
                                 cc.Secuencial_Cliente
                             }).ToList();

            dataGridView1.Rows.Clear();



            foreach (var item in resultado)
            {
                int rowIndex = dataGridView1.Rows.Add(
                    item.Secuencial.ToString(),
                    item.SecuencialVenta,
                    item.NombreCliente,
                    item.Fecha.ToString(),
                    item.Fecha_Vencimiento.ToString(),
                    item.Gran_Total.ToString(),
                    item.Saldo,
                    item.Pagado,
                    item.Secuencial_Cliente

                );
                total_facturas += (double)item.Gran_Total;
                saldo_pendiente += (double)item.Saldo;
                // Obtener la fila recién agregada
                var row = dataGridView1.Rows[rowIndex];

                // Aplicar color si está vencida y con saldo pendiente
                if (DateTime.TryParse(row.Cells["Fecha_Vencimiento"].Value?.ToString(), out DateTime venc) &&
                    decimal.TryParse(row.Cells["Saldo"].Value?.ToString(), out decimal saldo) &&
                    venc < DateTime.Today && saldo > 0)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }


            label10.Text = Math.Round(total_facturas,2).ToString() + " " + V_Menu_Principal.moneda;
            label8.Text = Math.Round(saldo_pendiente,2).ToString() + " " + V_Menu_Principal.moneda;

        }




        private void Cargar_Datos_CTAS_Cliente(int secuencial)
        {
            double total_facturas = 0;
            double saldo_pendiente = 0;
            // label4.Text = "0";
            //label5.Text = "0";
            dataGridView1.Rows.Clear();
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Asegura la existencia de la base de datos

            var resultado = (from cc in context.Cuentas_Cobrar
                             join v in context.Ventas on cc.Secuencial_Factura equals v.Secuencial
                             join c in context.Clientes on cc.Secuencial_Cliente equals c.Secuencial
                             where cc.Saldo > 0 && cc.Secuencial_Cliente == secuencial
                             select new
                             {
                                 cc.Secuencial,
                                 SecuencialVenta = v.Secuencial,
                                 NombreCliente = c.Nombre,
                                 cc.Fecha,
                                 cc.Fecha_Vencimiento,
                                 cc.Gran_Total,
                                 cc.Saldo,
                                 cc.Pagado,
                                 cc.Secuencial_Cliente
                             }).ToList();

            dataGridView1.Rows.Clear();



            foreach (var item in resultado)
            {
                int rowIndex = dataGridView1.Rows.Add(
                    item.Secuencial.ToString(),
                    item.SecuencialVenta,
                    item.NombreCliente,
                    item.Fecha.ToString(),
                    item.Fecha_Vencimiento.ToString(),
                    item.Gran_Total.ToString(),
                    item.Saldo,
                    item.Pagado,
                    item.Secuencial_Cliente

                );
                total_facturas += (double)item.Gran_Total;
                saldo_pendiente += (double)item.Saldo;
                // Obtener la fila recién agregada
                var row = dataGridView1.Rows[rowIndex];

                // Aplicar color si está vencida y con saldo pendiente
                if (DateTime.TryParse(row.Cells["Fecha_Vencimiento"].Value?.ToString(), out DateTime venc) &&
                    decimal.TryParse(row.Cells["Saldo"].Value?.ToString(), out decimal saldo) &&
                    venc < DateTime.Today && saldo > 0)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }


            label10.Text = total_facturas.ToString() + " " + V_Menu_Principal.moneda;
            label8.Text = saldo_pendiente.ToString() + " " + V_Menu_Principal.moneda;

        }






        private void Cargar_Datos_CTAS_Fechas(string f1, string f2)
        {
            double total_facturas = 0;
            double saldo_pendiente = 0;

            DateTime.TryParse(f1, out var fd1);
            DateTime.TryParse(f2, out var fd2);

            dataGridView1.Rows.Clear();
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Asegura la existencia de la base de datos

            var resultado = (from cc in context.Cuentas_Cobrar
                             join v in context.Ventas on cc.Secuencial_Factura equals v.Secuencial
                             join c in context.Clientes on cc.Secuencial_Cliente equals c.Secuencial
                             select new
                             {
                                 cc.Secuencial,
                                 SecuencialVenta = v.Secuencial,
                                 NombreCliente = c.Nombre,
                                 cc.Fecha,
                                 cc.Fecha_Vencimiento,
                                 cc.Gran_Total,
                                 cc.Saldo,
                                 cc.Pagado,
                                 cc.Secuencial_Cliente
                             }).ToList()
                             .Where(x =>
                                 DateTime.TryParse(x.Fecha, out var fecha) &&
                                 DateTime.TryParse(x.Fecha_Vencimiento, out var vencimiento) &&
                                 fecha >= fd1.Date && vencimiento <= fd2.Date &&
                                 x.Saldo > 0)
                             .ToList();


            dataGridView1.Rows.Clear();



            foreach (var item in resultado)
            {
                int rowIndex = dataGridView1.Rows.Add(
                    item.Secuencial.ToString(),
                    item.SecuencialVenta,
                    item.NombreCliente,
                    item.Fecha.ToString(),
                    item.Fecha_Vencimiento.ToString(),
                    item.Gran_Total.ToString(),
                    item.Saldo,
                    item.Pagado,
                    item.Secuencial_Cliente
                );
                total_facturas += (double)item.Gran_Total;
                saldo_pendiente += (double)item.Saldo;

                // Obtener la fila recién agregada
                var row = dataGridView1.Rows[rowIndex];

                // Aplicar color si está vencida y con saldo pendiente
                if (DateTime.TryParse(row.Cells["Fecha_Vencimiento"].Value?.ToString(), out DateTime venc) &&
                    decimal.TryParse(row.Cells["Saldo"].Value?.ToString(), out decimal saldo) &&
                    venc < DateTime.Today && saldo > 0)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }


            label10.Text = total_facturas.ToString() + " " + V_Menu_Principal.moneda;
            label8.Text = saldo_pendiente.ToString() + " " + V_Menu_Principal.moneda;


        }






        private void Cargar_Datos_CTAS_Vencidas()
        {
            double total_facturas = 0;
            double saldo_pendiente = 0;


            dataGridView1.Rows.Clear();
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Asegura la existencia de la base de datos

            var resultado = (from cc in context.Cuentas_Cobrar
                             join v in context.Ventas on cc.Secuencial_Factura equals v.Secuencial
                             join c in context.Clientes on cc.Secuencial_Cliente equals c.Secuencial
                             select new
                             {
                                 cc.Secuencial,
                                 SecuencialVenta = v.Secuencial,
                                 NombreCliente = c.Nombre,
                                 cc.Fecha,
                                 cc.Fecha_Vencimiento,
                                 cc.Gran_Total,
                                 cc.Saldo,
                                 cc.Pagado,
                                 cc.Secuencial_Cliente
                             }).ToList()
                             .Where(x =>
                                 DateTime.TryParse(x.Fecha_Vencimiento, out var vencimiento) &&
                                 vencimiento < DateTime.Today &&
                                 x.Saldo > 0)
                             .ToList();


            dataGridView1.Rows.Clear();



            foreach (var item in resultado)
            {
                int rowIndex = dataGridView1.Rows.Add(
                    item.Secuencial.ToString(),
                    item.SecuencialVenta,
                    item.NombreCliente,
                    item.Fecha.ToString(),
                    item.Fecha_Vencimiento.ToString(),
                    item.Gran_Total.ToString(),
                    item.Saldo,
                    item.Pagado,
                    item.Secuencial_Cliente
                );

                total_facturas += (double)item.Gran_Total;
                saldo_pendiente += (double)item.Saldo;

                // Obtener la fila recién agregada
                var row = dataGridView1.Rows[rowIndex];

                // Aplicar color si está vencida y con saldo pendiente
                if (DateTime.TryParse(row.Cells["Fecha_Vencimiento"].Value?.ToString(), out DateTime venc) &&
                    decimal.TryParse(row.Cells["Saldo"].Value?.ToString(), out decimal saldo) &&
                    venc < DateTime.Today && saldo > 0)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }


            label10.Text = total_facturas.ToString() + " " + V_Menu_Principal.moneda;
            label8.Text = saldo_pendiente.ToString() + " " + V_Menu_Principal.moneda;



        }





        public void Configurar_DataGridView()
        {
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Configurar las columnas del DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Secuencial", "S");

            dataGridView1.Columns.Add("SecuencialVenta", "No. Factura");
            dataGridView1.Columns.Add("Cliente", "Cliente");
            dataGridView1.Columns.Add("Fecha", "Emitida");
            dataGridView1.Columns.Add("Fecha_Vencimiento", "Vence");
            dataGridView1.Columns.Add("Gran_Total", "Total");
            dataGridView1.Columns[5].DefaultCellStyle.ForeColor = Color.FromArgb(0, 192, 192);
            dataGridView1.Columns.Add("Saldo", "Saldo");
            dataGridView1.Columns[6].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns.Add("Pagado", "Pagado");
            dataGridView1.Columns[7].DefaultCellStyle.ForeColor = Color.DarkGreen;
            dataGridView1.Columns.Add("Secuencial_Cliente", "SC");




            dataGridView1.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }







        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void V_CTAS_Cobrar_Load(object sender, EventArgs e)
        {
            llenar_Combo_Cliente();
            Configurar_DataGridView();
            Cargar_Datos_CTAS();

        }




        public void llenar_Combo_Cliente()
        {


            comboCliente.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar solo clientes activos
            var clientesActivos = context.Clientes.Where(c => (bool)c.Activo && c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa).ToList();

            foreach (var item in clientesActivos)
            {
                comboCliente.Items.Add(item.Secuencial + " - " + item.Nombre);
            }





        }




        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Configurar_DataGridView();
            Cargar_Datos_CTAS();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Cargar_Datos_CTAS_Fechas(fecha_inicio.Value.ToString("dd/MM/yyyy"), fecha_fin.Value.ToString("dd/MM/yyyy"));

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Cargar_Datos_CTAS_Vencidas();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {




            Secuencial_Cliente = int.Parse(comboCliente.SelectedItem.ToString().Split('-')[0].Trim());


            Cargar_Datos_CTAS_Cliente(Secuencial_Cliente);



        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {




            try
            {
                var row = dataGridView1.CurrentRow;

                if (row == null)
                {
                    MessageBox.Show("No hay ninguna fila seleccionada.");
                    return;
                }

                if (row.Cells["Gran_Total"]?.Value != null)
                {
                    this.Gran_Total = (double)Convert.ToDecimal(row.Cells["Gran_Total"].Value);
                }

                if (row.Cells["Secuencial"]?.Value != null)
                {
                    this.Secuencial = Convert.ToInt32(row.Cells["Secuencial"].Value);
                }

                if (row.Cells["Cliente"]?.Value != null)
                {
                    this.Nombre = row.Cells["Cliente"].Value?.ToString();
                }

                if (int.TryParse(row.Cells["Secuencial_Cliente"].Value?.ToString(), out int secuencialCliente))
                {
                    this.Secuencial_Cliente = secuencialCliente;
                }


                // Asegúrate de que Secuencial_Cliente esté definido antes de usarlo
                V_Abono_Cliente v_Abono_Cliente = new V_Abono_Cliente(Secuencial, Secuencial_Cliente, this.Nombre, this.Gran_Total);

                v_Abono_Cliente.Cliente_Nombre = this.Nombre;
                v_Abono_Cliente.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


            Cargar_Datos_CTAS();


        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
