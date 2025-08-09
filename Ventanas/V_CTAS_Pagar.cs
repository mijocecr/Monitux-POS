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
    public partial class V_CTAS_Pagar : Form
    {


        public int Secuencial_Proveedor { get; set; }
        public int Secuencial { get; set; }
        public string Nombre { get; set; }
        public int Secuencial_Factura { get; set; }

        public double Gran_Total { get; set; }


        public V_CTAS_Pagar()
        {
            InitializeComponent();
        }




        public void llenar_Combo_Proveedor()
        {


            comboProveedor.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar solo clientes activos
            var proveedoresActivos = context.Proveedores.Where(c => (bool)c.Activo && c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa).ToList();

            foreach (var item in proveedoresActivos)
            {
                comboProveedor.Items.Add(item.Secuencial + " - " + item.Nombre);
            }





        }



        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void V_CTAS_Pagar_Load(object sender, EventArgs e)
        {
            llenar_Combo_Proveedor();
            Configurar_DataGridView();
            Cargar_Datos_CTAS();

        }


        public void Configurar_DataGridView()
        {
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Configurar las columnas del DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Secuencial", "S");

            dataGridView1.Columns.Add("SecuencialCompra", "No. Factura");
            dataGridView1.Columns.Add("Proveedor", "Proveedor");
            dataGridView1.Columns.Add("Fecha", "Emitida");
            dataGridView1.Columns.Add("Fecha_Vencimiento", "Vence");
            dataGridView1.Columns.Add("Gran_Total", "Total");
            dataGridView1.Columns[5].DefaultCellStyle.ForeColor = Color.FromArgb(0, 192, 192);
            dataGridView1.Columns.Add("Saldo", "Saldo");
            dataGridView1.Columns[6].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns.Add("Pagado", "Pagado");
            dataGridView1.Columns[7].DefaultCellStyle.ForeColor = Color.DarkGreen;
            dataGridView1.Columns.Add("Secuencial_Proveedor", "SP");




            dataGridView1.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

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

            var resultado = (from cp in context.Cuentas_Pagar
                             join c in context.Compras on cp.Secuencial_Factura equals c.Secuencial
                             join p in context.Proveedores on cp.Secuencial_Proveedor equals p.Secuencial
                             where cp.Saldo > 0
                             select new
                             {
                                 cp.Secuencial,
                                 SecuencialCompra = c.Secuencial,
                                 NombreProveedor = p.Nombre,
                                 cp.Fecha,
                                 cp.Fecha_Vencimiento,
                                 cp.Gran_Total,
                                 cp.Saldo,
                                 cp.Pagado,
                                 cp.Secuencial_Proveedor
                             }).ToList();

            dataGridView1.Rows.Clear();



            foreach (var item in resultado)
            {
                int rowIndex = dataGridView1.Rows.Add(
                    item.Secuencial.ToString(),
                    item.SecuencialCompra,
                    item.NombreProveedor,
                    item.Fecha.ToString(),
                    item.Fecha_Vencimiento.ToString(),
                    item.Gran_Total.ToString(),
                    item.Saldo,
                    item.Pagado,
                    item.Secuencial_Proveedor

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


            label10.Text = Math.Round(total_facturas, 2).ToString() + " " + V_Menu_Principal.moneda;
            label8.Text = Math.Round(saldo_pendiente, 2).ToString() + " " + V_Menu_Principal.moneda;

        }





        private void Cargar_Datos_CTAS_Proveedor(int secuencial)
        {
            double total_facturas = 0;
            double saldo_pendiente = 0;
            // label4.Text = "0";
            //label5.Text = "0";
            dataGridView1.Rows.Clear();
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Asegura la existencia de la base de datos

            var resultado = (from cp in context.Cuentas_Pagar
                             join c in context.Compras on cp.Secuencial_Factura equals c.Secuencial
                             join p in context.Proveedores on cp.Secuencial_Proveedor equals p.Secuencial
                             where cp.Saldo > 0 && cp.Secuencial_Proveedor == secuencial
                             select new
                             {
                                 cp.Secuencial,
                                 SecuencialCompra = c.Secuencial,
                                 NombreProveedor = p.Nombre,
                                 cp.Fecha,
                                 cp.Fecha_Vencimiento,
                                 cp.Gran_Total,
                                 cp.Saldo,
                                 cp.Pagado,
                                 cp.Secuencial_Proveedor
                             }).ToList();

            dataGridView1.Rows.Clear();



            foreach (var item in resultado)
            {
                int rowIndex = dataGridView1.Rows.Add(
                    item.Secuencial.ToString(),
                    item.SecuencialCompra,
                    item.NombreProveedor,
                    item.Fecha.ToString(),
                    item.Fecha_Vencimiento.ToString(),
                    item.Gran_Total.ToString(),
                    item.Saldo,
                    item.Pagado,
                    item.Secuencial_Proveedor

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





        private void comboProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {

            Secuencial_Proveedor = int.Parse(comboProveedor.SelectedItem.ToString().Split('-')[0].Trim());


            Cargar_Datos_CTAS_Proveedor(Secuencial_Proveedor);

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

            var resultado = (from cp in context.Cuentas_Pagar
                             join c in context.Compras on cp.Secuencial_Factura equals c.Secuencial
                             join p in context.Proveedores on cp.Secuencial_Proveedor equals p.Secuencial
                             select new
                             {
                                 cp.Secuencial,
                                 SecuencialCompra = c.Secuencial,
                                 NombreProveedor = p.Nombre,
                                 cp.Fecha,
                                 cp.Fecha_Vencimiento,
                                 cp.Gran_Total,
                                 cp.Saldo,
                                 cp.Pagado,
                                 cp.Secuencial_Proveedor
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
                    item.SecuencialCompra,
                    item.NombreProveedor,
                    item.Fecha.ToString(),
                    item.Fecha_Vencimiento.ToString(),
                    item.Gran_Total.ToString(),
                    item.Saldo,
                    item.Pagado,
                    item.Secuencial_Proveedor
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




        private void button1_Click(object sender, EventArgs e)
        {

            Cargar_Datos_CTAS_Fechas(fecha_inicio.Value.ToString("dd/MM/yyyy"), fecha_fin.Value.ToString("dd/MM/yyyy"));

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Configurar_DataGridView();
            Cargar_Datos_CTAS();
        }



        private void Cargar_Datos_CTAS_Vencidas()
        {
            double total_facturas = 0;
            double saldo_pendiente = 0;


            dataGridView1.Rows.Clear();
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Asegura la existencia de la base de datos

            var resultado = (from cp in context.Cuentas_Pagar
                             join c in context.Compras on cp.Secuencial_Factura equals c.Secuencial
                             join p in context.Proveedores on cp.Secuencial_Proveedor equals p.Secuencial
                             select new
                             {
                                 cp.Secuencial,
                                 SecuencialCompra = c.Secuencial,
                                 NombreProveedor = p.Nombre,
                                 cp.Fecha,
                                 cp.Fecha_Vencimiento,
                                 cp.Gran_Total,
                                 cp.Saldo,
                                 cp.Pagado,
                                 cp.Secuencial_Proveedor
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
                    item.SecuencialCompra,
                    item.NombreProveedor,
                    item.Fecha.ToString(),
                    item.Fecha_Vencimiento.ToString(),
                    item.Gran_Total.ToString(),
                    item.Saldo,
                    item.Pagado,
                    item.Secuencial_Proveedor
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




        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Cargar_Datos_CTAS_Vencidas();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {







            try
            {
                var row = dataGridView1.CurrentRow;

                if (row == null)
                {
                    V_Menu_Principal.MSG.ShowMSG("No hay ninguna fila seleccionada.", "Error");
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

                if (row.Cells["Proveedor"]?.Value != null)
                {
                    this.Nombre = row.Cells["Proveedor"].Value?.ToString();
                }

               


                if (int.TryParse(row.Cells["Secuencial_Proveedor"].Value?.ToString(), out int secuencialProveedor))
                {
                    this.Secuencial_Proveedor = secuencialProveedor;
                }


                V_Abono_Proveedor v_Abono_Proveedor = new V_Abono_Proveedor(Secuencial, Secuencial_Proveedor, this.Nombre, this.Gran_Total);

                v_Abono_Proveedor.Proveedor_Nombre = this.Nombre;
                v_Abono_Proveedor.ShowDialog();
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG("Error: " + ex.Message, "Error");
            }


            Cargar_Datos_CTAS();






        }
    }
}
