using Microsoft.EntityFrameworkCore;
using Monitux_POS.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Monitux_POS.Ventanas
{
    public partial class V_Venta_Rapida : Form
    {
        double subtotal = 0.0;
        double total = 0.0;
        double ISV = 0.0;//quitar esto

        public V_Venta_Rapida()
        {
            this.Shown += V_Venta_Rapida_Shown;

            InitializeComponent();
        }

        private void RestaurarFocoEscaner()
        {
            if (checkBox1.Checked && textBox1.Visible && textBox1.Enabled)
            {
                BeginInvoke((MethodInvoker)(() =>
                {
                    textBox1.Focus();
                    textBox1.Select();
                }));
            }
        }



        public void Filtrar(string campo, string valor)
        {

            string cantidad = "0";

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            var productos = context.Productos
                    .Where(c => EF.Property<string>(c, campo).Equals(valor))
                    .ToList();





            foreach (var item in productos)
            {

                if (V_Menu_Principal.IPB.Show("Digite la cantidad en números de este producto que esta agregando", "¿Cuantos: " + item.Codigo + "?", out cantidad) == DialogResult.OK)
                {

                    cantidad = cantidad?.Trim();

                    if (Double.TryParse(cantidad, NumberStyles.Any, CultureInfo.InvariantCulture, out double numero))
                    {


                        dataGridView1.Rows.Add(item.Secuencial,
                        item.Codigo,
                        item.Descripcion,
                        numero,
                        Math.Round((double)item.Precio_Venta, 2),
                        numero * Math.Round((double)item.Precio_Venta, 2));



                    }


                }
                else
                {
                    V_Menu_Principal.MSG.ShowMSG("No se ha ingresado una cantidad válida.", "Error");
                    if (checkBox1.Checked)
                    {
                        textBox1.Select();
                        textBox1.Focus();
                    }

                }



            }



        }



        private void button7_Click(object sender, EventArgs e)
        {


            textBox1.Text = "1";
            textBox1.Focus();

            // Simula la pulsación de Enter para activar el KeyDown
            BeginInvoke((MethodInvoker)(() => SendKeys.Send("{ENTER}")));





            RestaurarFocoEscaner();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {



        }



        public void Configurar_DataGridView()
        {
            // Configurar las columnas del DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns.Add("Secuencial", "S");
            dataGridView1.Columns["Secuencial"].Width = 28; // Ancho de la columna Secuencial
            dataGridView1.Columns.Add("Codigo", "Codigo");
            dataGridView1.Columns["Codigo"].Width = 100; // Ancho de la columna Codigo
            dataGridView1.Columns.Add("Descripcion", "Descripcion");
            dataGridView1.Columns["Descripcion"].Width = 215; // Ancho de la columna Descripcion

            dataGridView1.Columns.Add("Cantidad", "Cantidad");
            dataGridView1.Columns["Cantidad"].Width = 60; // Ancho de la columna Cantidad




            dataGridView1.Columns.Add("Precio_Venta", "Precio");
            dataGridView1.Columns["Precio_Venta"].Width = 80; // Ancho de la columna Precio_Venta
            dataGridView1.Columns.Add("Total", "Total");
            dataGridView1.Columns["Total"].Width = 80; // Ancho de la columna Total





            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }



        private void ActualizarTotal()
        {
            total = 0.0;
            subtotal = 0.0;
            ISV = 0.0; // Reiniciar el ISV antes de calcular
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    double valor = Convert.ToDouble(row.Cells["Total"].Value);
                    subtotal += valor;
                }
            }
            if (checkBox2.Checked)
            {
                ISV = subtotal * 0.15;

            }
            else
            {
                ISV = 0.0; // Si no se aplica el ISV, lo dejamos en 0
            }
            // Asumiendo un ISV del 15%
            total = subtotal + ISV;
            // Actualizar los totales en la interfaz
            lbl_SubTotal.Text = $"{subtotal:0.00}";
            lbl_ISV.Text = $"{ISV:0.00}";
            lbl_Total.Text = $"{total:0.00}";
            RestaurarFocoEscaner();
        }


        public void MostrarTopProductosEnPanel(FlowLayoutPanel panel)
        {
            panel.Controls.Clear(); // Limpiar por si ya hay botones

            var topProductos = Util.ObtenerTopProductosVendidos(5);
            string cantidad = "0";
            foreach (var producto in topProductos)
            {
                var btn = new System.Windows.Forms.Button
                {
                    Width = 155,
                    Height = 60,
                    BackColor = Color.FromArgb(35, 32, 45),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = $"{producto.Descripcion.Substring(0, Math.Min(35, producto.Descripcion.Length))}\n{V_Menu_Principal.moneda}{producto.Venta:0.00}",
                    Tag = producto
                };
                btn.FlatAppearance.BorderColor = Color.FromArgb(0, 168, 107);
                btn.FlatAppearance.BorderSize = 1;

                var tooltip = new System.Windows.Forms.ToolTip();
                tooltip.SetToolTip(btn, producto.Descripcion);

                btn.Click += (s, e) =>
                {
                    var prod = (Producto_Top_VR)((System.Windows.Forms.Button)s).Tag;
                    //AgregarProductoAlCarrito(prod); // ← Implementa esta función según tu lógica

                    if (V_Menu_Principal.IPB.Show("Digite la cantidad en números de este producto que esta agregando", "¿Cuantos: " + producto.Codigo + "?", out cantidad) == DialogResult.OK)
                    {

                        cantidad = cantidad?.Trim();

                        if (Double.TryParse(cantidad, NumberStyles.Any, CultureInfo.InvariantCulture, out double numero))
                        {


                            dataGridView1.Rows.Add(producto.Secuencial_Producto,
                            producto.Codigo,
                            producto.Descripcion,
                            numero,
                            Math.Round((double)producto.Venta, 2),
                            numero * Math.Round((double)producto.Venta, 2));



                        }

                        if (checkBox1.Checked)
                        {
                            textBox1.Select();
                            textBox1.Focus();
                        }

                    }
                    else
                    {

                        V_Menu_Principal.MSG.ShowMSG("No se ha ingresado una cantidad válida.", "Error");
                        if (checkBox1.Checked)
                        {
                            textBox1.Select();
                            textBox1.Focus();
                        }

                    }




                };

                RestaurarFocoEscaner();
                panel.Controls.Add(btn);
            }
        }







        private void V_Venta_Rapida_Load(object sender, EventArgs e)
        {
            textBox1.Select();
            textBox1.Focus();
            MostrarTopProductosEnPanel(flowLayoutPanel1);
            Configurar_DataGridView();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            comboBox1.Items.Add("Codigo");
            comboBox1.Items.Add("Codigo_Barra");
            comboBox1.Items.Add("Codigo_Fabricante");
            comboBox1.Items.Add("Descripcion");
            comboBox1.Items.Add("Fecha_Caducidad");
            comboBox1.Items.Add("Marca");
            comboBox1.SelectedIndex = 0; // Seleccionar el primer elemento por defecto


        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                // Aquí va lo que quieras ejecutar
                string texto = textBox1.Text.Trim();
                Filtrar("Codigo", texto);
                ActualizarTotal();
                e.Handled = true;
                e.SuppressKeyPress = true; // Opcional: evita que suene "ding"
                textBox1.Clear(); // Limpiar el TextBox después de buscar
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {



            if (checkBox1.Checked && this.Visible && this.ContainsFocus)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    if (!textBox1.Focused)
                    {
                        textBox1.Focus();
                        textBox1.Select();
                    }
                });
            }





        }

        private void V_Venta_Rapida_Shown(object sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                textBox1.Focus();
                textBox1.Select();
            }));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            RestaurarFocoEscaner();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RestaurarFocoEscaner();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RestaurarFocoEscaner();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RestaurarFocoEscaner();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RestaurarFocoEscaner();
        }

        private void dataGridView1_MouseHover(object sender, EventArgs e)
        {
            RestaurarFocoEscaner();
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            RestaurarFocoEscaner();
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            RestaurarFocoEscaner();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RestaurarFocoEscaner();
        }

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            RestaurarFocoEscaner();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            ActualizarTotal();

        }
    }
}
