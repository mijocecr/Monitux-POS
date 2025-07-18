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
    public partial class V_Egresos : Form
    {
        public int Secuencial_Empresa { get; set; } = V_Menu_Principal.Secuencial_Empresa;
        public int Secuencial_Usuario { get; set; } = V_Menu_Principal.Secuencial_Usuario;

        public int Secuencial { get; set; }
        public V_Egresos()
        {
            InitializeComponent();
        }



        private void Cargar_Datos()
        {
            double total_egresos = 0;
            double total_otros = 0;

            dataGridView1.Rows.Clear();
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Asegura la existencia de la base de datos


            var resultado = (from e in context.Egresos
                             join u in context.Usuarios on e.Secuencial_Usuario equals u.Secuencial
                             join c in context.Compras on e.Secuencial_Factura equals c.Secuencial into comprasJoin
                             from c in comprasJoin.DefaultIfEmpty() // permite que haya ingresos sin venta asociada
                             where e.Secuencial_Empresa == Secuencial_Empresa
                             orderby e.Fecha descending
                             select new
                             {
                                 e.Secuencial,
                                 Fecha = e.Fecha,
                                 Tipo = e.Tipo_Egreso,
                                 Monto = e.Total,
                                 NombreUsuario = u.Nombre,
                                 Descripcion = e.Descripcion,
                                 SecuencialUsuario = e.Secuencial_Usuario,
                                 FacturaAsociada = c != null ? $"Factura No. {c.Secuencial}" : "0"
                             }).ToList();





            foreach (var item in resultado)
            {
                int rowIndex = dataGridView1.Rows.Add(
                    item.Secuencial.ToString(),
                    item.NombreUsuario,
                    item.Fecha.ToString(),
                    item.Tipo,
                    item.Monto,
                    item.Descripcion


                );

                // Obtener la fila recién agregada
                var row = dataGridView1.Rows[rowIndex];


                if (item.FacturaAsociada != "0")
                {
                    total_egresos += (double)item.Monto;
                }



                if (item.FacturaAsociada == "0")

                {
                    total_otros += (double)item.Monto;
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }

            }


            label10.Text = Math.Round(total_otros, 2).ToString() + " " + V_Menu_Principal.moneda;
            label8.Text = Math.Round(total_egresos, 2).ToString() + " " + V_Menu_Principal.moneda;

        }



        private void button3_Click(object sender, EventArgs e)
        {

            V_Ingresos_Egresos vIngresosEgresos = new V_Ingresos_Egresos();
            vIngresosEgresos.IsEgreso = true; // Indica que es un ingreso
            vIngresosEgresos.Secuencial_Empresa = this.Secuencial_Empresa;
            vIngresosEgresos.Secuencial_Usuario = this.Secuencial_Usuario;
            vIngresosEgresos.ShowDialog();
            Cargar_Datos();
        }



        public void Configurar_DataGridView()
        {

            //dataGridView1.Enabled = false;


            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            // Configurar las columnas del DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Secuencial", "S");


            dataGridView1.Columns.Add("Usuario", "Usuario");


            dataGridView1.Columns.Add("Fecha", "Fecha");


            dataGridView1.Columns.Add("Tipo_Egreso", "Tipo");


            dataGridView1.Columns.Add("Total", "Total");


            dataGridView1.Columns.Add("Descripcion", "Descripción");
            //dataGridView1.Columns["Descripcion"].Width = 250; // valor en píxeles









            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }





        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void V_Egresos_Load(object sender, EventArgs e)
        {
            this.Text = "Monitux-POS v." + V_Menu_Principal.VER;
            Configurar_DataGridView();
            Cargar_Datos();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            SQLitePCL.Batteries.Init();



            var confirmResult = V_Menu_Principal.MSG.ShowMSG(
    $"¿Está seguro de eliminar el egreso seleccionado?",
    "Confirmar Eliminación");

            if (confirmResult != DialogResult.Yes)
                return;

            using var context = new Monitux_DB_Context();

            var egreso = context.Egresos.FirstOrDefault(e =>
                e.Secuencial == this.Secuencial &&
                (e.Secuencial_Factura == 0 || e.Secuencial_Factura == null) &&
                e.Secuencial_Empresa == Secuencial_Empresa);

            if (egreso != null)
            {
                context.Egresos.Remove(egreso);

                Util.Registrar_Actividad(
                    Secuencial_Usuario,
                    $"Eliminó egreso sin factura asociada. Monto: {egreso.Total} | Tipo: {egreso.Tipo_Egreso} | Fecha: {egreso.Fecha}",
                    Secuencial_Empresa
                );

                context.SaveChanges();
                V_Menu_Principal.MSG.ShowMSG("Egreso sin factura asociada eliminado correctamente.", "Éxito");
                Cargar_Datos();
            }
            else
            {
                V_Menu_Principal.MSG.ShowMSG("No es posible eliminar el egreso seleccionado.", "Error");
            }





        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex >= 0) // Asegura que no sea el encabezado
            {
                var row = dataGridView1.Rows[e.RowIndex];

                if (row.Cells["Secuencial"]?.Value != null)
                {
                    this.Secuencial = Convert.ToInt32(row.Cells["Secuencial"].Value);

                }
            }

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex >= 0) // Asegura que no sea el encabezado
            {
                var row = dataGridView1.Rows[e.RowIndex];

                if (row.Cells["Secuencial"]?.Value != null)
                {
                    this.Secuencial = Convert.ToInt32(row.Cells["Secuencial"].Value);

                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            double total_egresos = 0;
            double total_otros = 0;

            try
            {
                DateTime fechaInicio = fecha_inicio.Value.Date;
                DateTime fechaFin = fecha_fin.Value.Date.AddDays(1).AddSeconds(-1); // incluye todo el día final

                SQLitePCL.Batteries.Init();
                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var egresos = context.Egresos
                    .Where(ee => ee.Secuencial_Empresa == Secuencial_Empresa)
                    .ToList(); // carga completa para procesar local

                var resultado = (from ee in egresos
                                 join u in context.Usuarios.ToList() on ee.Secuencial_Usuario equals u.Secuencial
                                 join c in context.Compras.ToList() on ee.Secuencial_Factura equals c.Secuencial into comprasJoin
                                 from c in comprasJoin.DefaultIfEmpty()
                                 let fechaConvertida = DateTime.TryParse(ee.Fecha.ToString(), out var fecha) ? fecha : DateTime.MinValue
                                 where fechaConvertida >= fechaInicio && fechaConvertida <= fechaFin
                                 orderby fechaConvertida descending
                                 select new
                                 {
                                     ee.Secuencial,
                                     Fecha = fechaConvertida,
                                     Tipo = ee.Tipo_Egreso,
                                     Monto = ee.Total,
                                     NombreUsuario = u.Nombre,
                                     Descripcion = ee.Descripcion,
                                     FacturaAsociada = c != null ? $"Factura No. {c.Secuencial}" : "0"
                                 }).ToList();

                dataGridView1.Rows.Clear();

                foreach (var item in resultado)
                {
                    int rowIndex = dataGridView1.Rows.Add(
                        item.Secuencial.ToString(),
                        item.NombreUsuario,
                        item.Fecha.ToString("dd/MM/yyyy"),
                        item.Tipo,
                        item.Monto,
                        item.Descripcion
                    );

                    var row = dataGridView1.Rows[rowIndex];

                    if (item.FacturaAsociada != "0")
                    {
                        total_egresos += item.Monto;
                    }
                    else
                    {
                        total_otros += item.Monto;
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }

                label10.Text = Math.Round(total_otros, 2).ToString() + " " + V_Menu_Principal.moneda;
                label8.Text = Math.Round(total_egresos, 2).ToString() + " " + V_Menu_Principal.moneda;
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Error al filtrar egresos por fecha: {ex.Message}", "Error");
            }





        }

        private void button4_Click(object sender, EventArgs e)
        {

            Util.ExportarDataGridViewAExcel(dataGridView1, "Egresos", "Egresos");

        }
    }
}
