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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Monitux_POS.Ventanas
{


    public partial class V_Factura_Venta : Form
    {

        public int Secuencial_Usuario { get; set; } = 0;
        List<int> indices = new List<int>();

        public V_Factura_Venta()
        {
            InitializeComponent();
        }



        public void Cargar_Items()
        {

            listBox1.Items.Clear();
            indices.Clear();
            flowLayoutPanel1.Controls.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe




            // **READ**
            var productos = context.Productos.ToList();
            int i = 0;
            Console.WriteLine("Lista de productos:");
            ImageList imageList = new ImageList();
            foreach (var item in productos)
            {




                Miniatura_Producto miniatura_Producto1 = new Miniatura_Producto();

                miniatura_Producto1.Cantidad = item.Cantidad;
                miniatura_Producto1.Imagen = item.Imagen;

                miniatura_Producto1.Secuencial = item.Secuencial;
                miniatura_Producto1.Codigo = item.Codigo;
                miniatura_Producto1.Marca = item.Marca;
                miniatura_Producto1.Descripcion = item.Descripcion;
                miniatura_Producto1.Precio_Venta = item.Precio_Venta;
                miniatura_Producto1.Precio_Costo = item.Precio_Costo;
                miniatura_Producto1.Existencia_Minima = item.Existencia_Minima;
                miniatura_Producto1.Codigo_Barra = item.Codigo_Barra;
                miniatura_Producto1.Codigo_Fabricante = item.Codigo_Fabricante;
                miniatura_Producto1.Codigo_QR = item.Codigo_QR;
                miniatura_Producto1.Secuencial_Proveedor = item.Secuencial_Proveedor;
                miniatura_Producto1.Secuencial_Categoria = item.Secuencial_Categoria;
                miniatura_Producto1.Secuencial_Usuario = 1;//Quitar esto
                miniatura_Producto1.Fecha_Caducidad = item.Fecha_Caducidad;


                miniatura_Producto1.Item_Imagen.Click += (s, ev) =>
                {
                    // MessageBox.Show(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1).ToString());

                    if (miniatura_Producto1.Seleccionado == true)
                    {
                        listBox1.Items.Add(miniatura_Producto1.Codigo);
                        indices.Add(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1));

                    }
                    else
                    {


                        listBox1.Items.Remove(miniatura_Producto1.Codigo);
                        indices.Remove(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1));

                    }
                };



                flowLayoutPanel1.Controls.Add(miniatura_Producto1);


            }


        }








        private void button1_Click(object sender, EventArgs e)
        {

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

            /*
            foreach (var item in comboC.Items)
            {
                if (item.ToString().Contains(this.Secuencial_Categoria.ToString())) // Verifica si hay un número
                {
                    comboCategoria.SelectedItem = item;
                    break;
                }
            }*/










        }




        private void V_Factura_Venta_Load(object sender, EventArgs e)
        {
            Cargar_Items();
            llenar_Combo_Cliente();
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }






        private void Filtrar(string campo, string valor)
        {





            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar categorías antes de agregarlas al DataGridView
            /*  string filtro = "eeee"; // Define el criterio de búsqueda
              var categoriasFiltradas = context.Categorias
                  .Where(c => c.Nombre.Contains(filtro)) // Aplica filtro en la consulta
                  .ToList();*/





            //-------------------Filtro que usare



            // Cambia esto a la columna que desees filtrar

            var productos = context.Productos
                    .Where(c => EF.Property<string>(c, campo).Contains(valor))
                    .ToList();

            flowLayoutPanel1.Controls.Clear();


            foreach (var item in productos)
            {




                Miniatura_Producto miniatura_Producto1 = new Miniatura_Producto();

                miniatura_Producto1.Cantidad = item.Cantidad;
                miniatura_Producto1.Imagen = item.Imagen;

                miniatura_Producto1.Secuencial = item.Secuencial;
                miniatura_Producto1.Codigo = item.Codigo;
                miniatura_Producto1.Marca = item.Marca;
                miniatura_Producto1.Descripcion = item.Descripcion;
                miniatura_Producto1.Precio_Venta = item.Precio_Venta;
                miniatura_Producto1.Precio_Costo = item.Precio_Costo;
                miniatura_Producto1.Existencia_Minima = item.Existencia_Minima;
                miniatura_Producto1.Codigo_Barra = item.Codigo_Barra;
                miniatura_Producto1.Codigo_Fabricante = item.Codigo_Fabricante;
                miniatura_Producto1.Codigo_QR = item.Codigo_QR;
                miniatura_Producto1.Secuencial_Proveedor = item.Secuencial_Proveedor;
                miniatura_Producto1.Secuencial_Categoria = item.Secuencial_Categoria;
                miniatura_Producto1.Secuencial_Usuario = 1;//Quitar esto
                miniatura_Producto1.Fecha_Caducidad = item.Fecha_Caducidad;




                /* miniatura_Producto1.Item_Imagen.Click += (s, ev) =>
                {
                    MessageBox.Show(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1).ToString());
                };*/


                miniatura_Producto1.Item_Imagen.Click += (s, ev) =>
                {
                    // MessageBox.Show(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1).ToString());

                    if (miniatura_Producto1.Seleccionado == true)
                    {
                        if (!indices.Contains(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1)))
                        {
                            listBox1.Items.Add(miniatura_Producto1.Codigo);
                            indices.Add(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1));
                        }
                        else
                        {
                            if (indices.Contains(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1)) && miniatura_Producto1.Seleccionado != true)
                            {

                                listBox1.Items.Remove(miniatura_Producto1.Codigo);
                                indices.Remove(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1));
                            }
                            else
                            {
                                miniatura_Producto1.Item_Seleccionado.Checked = true;
                            }

                        }

                    }
                    else
                    {
                        if (indices.Contains(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1)))

                            listBox1.Items.Remove(miniatura_Producto1.Codigo);
                        indices.Remove(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1));

                    }
                };

                flowLayoutPanel1.Controls.Add(miniatura_Producto1);

                foreach (Miniatura_Producto x in flowLayoutPanel1.Controls)
                {
                    if (indices.Contains(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1)))
                    {
                        miniatura_Producto1.Item_Seleccionado.Checked = true;
                    }
                    else
                    {

                        miniatura_Producto1.Item_Seleccionado.Checked = false;

                    }
                }
            }


            //-------------------Filtro que usare









            // Limpiar filas antes de agregar nuevas
            dataGridView1.Rows.Clear();

            foreach (var item in productos)
            {

            }


        }




        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            V_Producto producto = new V_Producto();
            producto.Secuencial_Usuario = Secuencial_Usuario;

            producto.ShowDialog();



        }

        private void button4_Click(object sender, EventArgs e)
        {
            V_Cliente cliente = new V_Cliente();
            cliente.Secuencial_Usuario = Secuencial_Usuario;
            cliente.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filtrar(comboBox2.SelectedItem.ToString(), textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Convertir la lista a un string separado por saltos de línea
            string mensaje = string.Join("\n", indices);

            // Mostrar en un MessageBox
            MessageBox.Show(mensaje, "Lista de Productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Filtrar(comboBox2.SelectedItem.ToString(), textBox1.Text);
            textBox1.Text = "";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 1)
            {
                label3.Visible = true;
                dateTimePicker1.Visible = true;
            }
            else
            {

                label3.Visible = false;
                dateTimePicker1.Visible = false;
            }

        }

        private void comboCliente_TextChanged(object sender, EventArgs e)
        {



        }

        private void comboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

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
