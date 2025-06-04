using Humanizer;
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
        //List<int> indices = new List<int>();

        Dictionary<string, int> Lista_de_Indices = new Dictionary<string, int>();
        Dictionary<string, Miniatura_Producto> Lista_de_Items = new Dictionary<string, Miniatura_Producto>();

        public V_Factura_Venta()
        {
            InitializeComponent();
        }



        public void Cargar_Items()
        {

            listBox1.Items.Clear();

            Lista_de_Items.Clear();
            dataGridView1.Rows.Clear();

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


                miniatura_Producto1.Item_Imagen.Click += async (s, ev) =>
                {
                    // MessageBox.Show(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1).ToString());

                    if (miniatura_Producto1.Seleccionado == true)
                    {
                        if (!listBox1.Items.Contains(miniatura_Producto1.Codigo))
                        {

                            listBox1.Items.Add(miniatura_Producto1.Codigo);

                        }

                        if (!Lista_de_Indices.ContainsKey(miniatura_Producto1.Codigo))
                        {
                            Lista_de_Indices.Add(miniatura_Producto1.Codigo, flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1));
                        }

                        if (!Lista_de_Items.ContainsKey(miniatura_Producto1.Codigo))
                        {
                            Lista_de_Items.Add(miniatura_Producto1.Codigo, miniatura_Producto1);
                        }
                        else
                        {                             // Si ya existe, actualiza la cantidad seleccionada

                            Lista_de_Items.Remove(miniatura_Producto1.Codigo);
                            await Task.Delay(100); // Espera para evitar problemas de concurrencia
                            Lista_de_Items.Add(miniatura_Producto1.Codigo, miniatura_Producto1);
                            Lista_de_Items[miniatura_Producto1.Codigo].cantidadSelecccionItem = miniatura_Producto1.cantidadSelecccionItem;

                        }




                    }
                    else
                    {


                        //  listBox1.Items.Remove(miniatura_Producto1.Codigo);
                        //  indices.Remove(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1));
                        //  Lista_de_Items.Remove(miniatura_Producto1.Codigo);


                    }
                };



                foreach (var clave in Lista_de_Indices.Keys)
                {



                    Lista_de_Indices.TryGetValue((clave), out int indices);

                    if (indices == flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1))
                    {
                        miniatura_Producto1.Item_Seleccionado.Checked = true;
                    }
                    else
                    {

                        miniatura_Producto1.Item_Seleccionado.Checked = false;

                    }


                }





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
            comboBox1.SelectedIndex = 0;
            Configurar_DataGridView();




        }

        public void Configurar_DataGridView()
        {
            // Configurar las columnas del DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Codigo", "Código");
            dataGridView1.Columns.Add("Descripcion", "Descripción");
            dataGridView1.Columns.Add("Cantidad", "Cantidad");
            dataGridView1.Columns.Add("Precio_Venta", "Precio");

            dataGridView1.Columns.Add("Total", "Total");

            dataGridView1.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
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


                miniatura_Producto1.Item_Imagen.Click += async (s, ev) =>
                {
                    // MessageBox.Show(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1).ToString());



                    if (miniatura_Producto1.Seleccionado == true)
                    {



                        if (!listBox1.Items.Contains(miniatura_Producto1.Codigo))
                        {

                            listBox1.Items.Add(miniatura_Producto1.Codigo);

                        }

                        if (!Lista_de_Indices.ContainsKey(miniatura_Producto1.Codigo))
                        {
                            Lista_de_Indices.Add(miniatura_Producto1.Codigo, flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1));
                        }

                        if (!Lista_de_Items.ContainsKey(miniatura_Producto1.Codigo))
                        {
                            Lista_de_Items.Add(miniatura_Producto1.Codigo, miniatura_Producto1);
                        }
                        else
                        {                             // Si ya existe, actualiza la cantidad seleccionada

                            Lista_de_Items.Remove(miniatura_Producto1.Codigo);
                            await Task.Delay(100); // Espera para evitar problemas de concurrencia
                            Lista_de_Items.Add(miniatura_Producto1.Codigo, miniatura_Producto1);
                            Lista_de_Items[miniatura_Producto1.Codigo].cantidadSelecccionItem = miniatura_Producto1.cantidadSelecccionItem;

                        }



                        /*  else
                          {
                              if (indices.Contains(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1)) && miniatura_Producto1.Seleccionado != true)
                              {

                                  listBox1.Items.Remove(miniatura_Producto1.Codigo);
                                  indices.Remove(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1));
                                  Lista_de_Items.Remove(miniatura_Producto1.Codigo);
                              }
                              else
                              {
                                  miniatura_Producto1.Item_Seleccionado.Checked = true;
                              }

                          }*/

                    }/*
                    else
                    {
                        if (indices.Contains(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1)))

                            listBox1.Items.Remove(miniatura_Producto1.Codigo);
                        indices.Remove(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1));
                        Lista_de_Items.Remove(miniatura_Producto1.Codigo);
                   
                        
                        }*/

                };

                flowLayoutPanel1.Controls.Add(miniatura_Producto1);







                foreach (Miniatura_Producto x in flowLayoutPanel1.Controls)
                {



                    foreach (var clave in Lista_de_Indices.Keys)
                    {



                        Lista_de_Indices.TryGetValue((clave), out int indices);

                        if (indices == flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1))
                        {
                            miniatura_Producto1.Item_Seleccionado.Checked = true;
                        }
                        else
                        {

                            miniatura_Producto1.Item_Seleccionado.Checked = false;

                        }


                    }





                }
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

            dataGridView1.Rows.Clear();


            foreach (var clave in Lista_de_Items.Keys)
            {



                Lista_de_Items.TryGetValue((clave), out Miniatura_Producto item);


                if (item.cantidadSelecccionItem != 0)
                {


                    dataGridView1.Rows.Add(
                    item.Codigo,
                    item.Descripcion,
                    item.cantidadSelecccionItem,
                       item.Precio_Venta,
                       item.Precio_Venta * item.cantidadSelecccionItem
                    );





                }
                else
                {

                    MessageBox.Show("Revise la cantidad que desea agregar. -- " + item.Codigo + " --\n\nDescripcion: " + item.Descripcion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }




            }




        }

        private void button7_Click(object sender, EventArgs e)
        {

            textBox1.Text = "";
            Lista_de_Items.Clear();
            listBox1.Items.Clear();
            dataGridView1.Rows.Clear();
            Cargar_Items();
            //indices.Clear();

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

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.Limpiar_Cache();
            this.Dispose();
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void listBox1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Escoja un item de la lista de seleccion para eliminarlo de la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {



                if (fila.Cells["Codigo"].Value != null && fila.Cells["Codigo"].Value== listBox1.SelectedItem.ToString())
                {
                    string codigo = fila.Cells["Codigo"].Value.ToString();

                    if (listBox1.Items.Contains(codigo))
                    {
                        listBox1.Items.Remove(codigo);
                        Lista_de_Items.Remove(codigo);
                        Lista_de_Indices.Remove(codigo);
                    }

                    if (fila.Index < dataGridView1.Rows.Count)
                    {
                        dataGridView1.Rows.Remove(fila);
                    }
                    else
                    {
                        MessageBox.Show("Índice fuera de rango.");
                    }
                }




            }



            Cargar_Items();

            label5.Text = listBox1.Items.Count.ToString();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {





        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            label5.Text=listBox1.Items.Count.ToString();
        }
    }
}