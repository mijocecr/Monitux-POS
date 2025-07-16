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
    public partial class V_Ingresos : Form
    {
        public int Secuencial_Empresa { get; set; } = V_Menu_Principal.Secuencial_Empresa;
        public int Secuencial_Usuario { get; set; } = V_Menu_Principal.Secuencial_Usuario;

        public int Secuencial { get; set; }
        public V_Ingresos()
        {
            InitializeComponent();
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
            dataGridView1.Columns.Add("Tipo_Ingreso", "Tipo");
            dataGridView1.Columns.Add("Total", "Total");
            dataGridView1.Columns.Add("Descripcion", "Descripción");
            // dataGridView1.Columns["Descripcion"].Width = 250; // valor en píxeles









            dataGridView1.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }




        private void Cargar_Datos()
        {
            double total_ingresos = 0;
            double total_otros = 0;

            dataGridView1.Rows.Clear();
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Asegura la existencia de la base de datos


            var resultado = (from i in context.Ingresos
                             join u in context.Usuarios on i.Secuencial_Usuario equals u.Secuencial
                             join v in context.Ventas on i.Secuencial_Factura equals v.Secuencial into ventasJoin
                             from v in ventasJoin.DefaultIfEmpty() // permite que haya ingresos sin venta asociada
                             where i.Secuencial_Empresa == Secuencial_Empresa
                             orderby i.Fecha descending
                             select new
                             {
                                 i.Secuencial,
                                 Fecha = i.Fecha,
                                 Tipo = i.Tipo_Ingreso,
                                 Monto = i.Total,
                                 NombreUsuario = u.Nombre,
                                 Descripcion = i.Descripcion,
                                 SecuencialUsuario = i.Secuencial_Usuario,
                                 FacturaAsociada = v != null ? $"Factura No. {v.Secuencial}" : "0"
                             }).ToList();







            dataGridView1.Rows.Clear();



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
                    total_ingresos += (double)item.Monto;
                }



                if (item.FacturaAsociada == "0")

                {
                    total_otros += (double)item.Monto;
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }

            }


            label10.Text = Math.Round(total_otros, 2).ToString() + " " + V_Menu_Principal.moneda;
            label8.Text = Math.Round(total_ingresos, 2).ToString() + " " + V_Menu_Principal.moneda;

        }






        private void V_Ingresos_Load(object sender, EventArgs e)
        {
            this.Text = "Monitux-POS v." + V_Menu_Principal.VER;
            Configurar_DataGridView();
            Cargar_Datos();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Cargar_Datos();
        }

        private void button2_Click(object sender, EventArgs e)
        {



            var confirmResult = V_Menu_Principal.MSG.ShowMSG(
    $"¿Está seguro de eliminar el ingreso seleccionado?",
    "Confirmar Eliminación");

            if (confirmResult != DialogResult.Yes)
                return;

            using var context = new Monitux_DB_Context();
            SQLitePCL.Batteries.Init();

            var ingreso = context.Ingresos.FirstOrDefault(i =>
                i.Secuencial == this.Secuencial &&
                (i.Secuencial_Factura == 0 || i.Secuencial_Factura == null) &&
                i.Secuencial_Empresa == Secuencial_Empresa);

            if (ingreso != null)
            {
                context.Ingresos.Remove(ingreso);

                Util.Registrar_Actividad(
                    Secuencial_Usuario,
                    $"Eliminó ingreso sin factura asociada. Monto: {ingreso.Total} | Tipo: {ingreso.Tipo_Ingreso} | Fecha: {ingreso.Fecha}",
                    Secuencial_Empresa
                );

                context.SaveChanges();
                V_Menu_Principal.MSG.ShowMSG("Ingreso sin factura asociada eliminado correctamente.", "Éxito");
                Cargar_Datos();
            }
            else
            {
                V_Menu_Principal.MSG.ShowMSG("No es posible eliminar el ingreso seleccionado.", "Error");
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
            double total_ingresos = 0;
            double total_otros = 0;
            try
            {
                DateTime fechaInicio = fecha_inicio.Value.Date;
                DateTime fechaFin = fecha_fin.Value.Date.AddDays(1).AddSeconds(-1); // incluye toda la fecha final

                SQLitePCL.Batteries.Init();
                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var ingresos = context.Ingresos
     .Where(i => i.Secuencial_Empresa == Secuencial_Empresa)
     .ToList(); // ← carga en memoria

                var resultado = (from i in ingresos
                                 join u in context.Usuarios.ToList() on i.Secuencial_Usuario equals u.Secuencial
                                 join v in context.Ventas.ToList() on i.Secuencial_Factura equals v.Secuencial into ventasJoin
                                 from v in ventasJoin.DefaultIfEmpty()
                                 let fechaConvertida = DateTime.TryParse(i.Fecha, out var fecha) ? fecha : DateTime.MinValue
                                 where fechaConvertida >= fechaInicio && fechaConvertida <= fechaFin
                                 orderby fechaConvertida descending
                                 select new
                                 {
                                     i.Secuencial,
                                     Fecha = i.Fecha,
                                     Tipo = i.Tipo_Ingreso,
                                     Monto = i.Total,
                                     NombreUsuario = u.Nombre,
                                     Descripcion = i.Descripcion,
                                     FacturaAsociada = v != null ? $"Factura No. {v.Secuencial}" : "0"
                                 }).ToList();


                dataGridView1.Rows.Clear();



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
                        total_ingresos += (double)item.Monto;
                    }



                    if (item.FacturaAsociada == "0")

                    {
                        total_otros += (double)item.Monto;
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }

                }


                label10.Text = Math.Round(total_otros, 2).ToString() + " " + V_Menu_Principal.moneda;
                label8.Text = Math.Round(total_ingresos, 2).ToString() + " " + V_Menu_Principal.moneda;




            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Error al filtrar ingresos por fecha: {ex.Message}", "Error");
            }







        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
          

            Util.ExportarDataGridViewAExcel(dataGridView1, "Ingresos", "Ingresos");


        }
    }
}
