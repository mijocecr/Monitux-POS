using Humanizer;
using Microsoft.EntityFrameworkCore;
using Monitux_POS.Clases;
using Monitux_POS.Controles;
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

        double subTotal = 0.00; // Variable para almacenar el subtotal
        double impuesto = 0.00; // Variable para almacenar el impuesto
        double total = 0.00; // Variable para almacenar el total
        double otrosCargos = 0.00; // Variable para almacenar otros cargos
        double descuento = 0.00; // Variable para almacenar el descuento aplicado



        Dictionary<string, Miniatura_Producto> Lista_de_Items = new Dictionary<string, Miniatura_Producto>();

        public V_Factura_Venta()
        {
            InitializeComponent();
        }



        public void Cargar_Items()
        {


            // Lista_de_Items.Clear();
            // dataGridView1.Rows.Clear();

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

                    Selector_Cantidad selector_Cantidad = new Selector_Cantidad();













                    if (miniatura_Producto1.Seleccionado == true)
                    {
                        //flowLayoutPanel2.Controls.Remove(selector_Cantidad);

                        if (!flowLayoutPanel2.Controls.Contains(selector_Cantidad))
                        {

                            selector_Cantidad.SetCodigo(miniatura_Producto1.Codigo);
                            selector_Cantidad.Tag = miniatura_Producto1.Codigo; // Asigna el código como Tag para referencia futura


                            flowLayoutPanel2.Controls.Add(selector_Cantidad);



                        }



                        if (!Lista_de_Items.ContainsKey(miniatura_Producto1.Codigo))
                        {
                            Lista_de_Items.Add(miniatura_Producto1.Codigo, miniatura_Producto1);
                        }
                        else
                        {                             // Si ya existe, actualiza la cantidad seleccionada

                            flowLayoutPanel2.Controls.Remove(selector_Cantidad);
                            Lista_de_Items.Remove(miniatura_Producto1.Codigo);
                            await Task.Delay(100); // Espera para evitar problemas de concurrencia

                            Lista_de_Items.Add(miniatura_Producto1.Codigo, miniatura_Producto1);





                        }




                    }
                    else
                    {


                        //  listBox1.Items.Remove(miniatura_Producto1.Codigo);
                        //  indices.Remove(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1));
                        //  Lista_de_Items.Remove(miniatura_Producto1.Codigo);


                    }
                };










                flowLayoutPanel1.Controls.Add(miniatura_Producto1);



                var controlesMiniatura = flowLayoutPanel1.Controls.OfType<Miniatura_Producto>();

                foreach (var control in controlesMiniatura)
                {
                    if (Lista_de_Items.ContainsKey(control.Codigo))
                    {
                        control.Item_Seleccionado.Checked = true; // Marca el checkbox si el item ya está en la lista

                    }
                    else
                    {
                        control.Item_Seleccionado.Checked = false; // Desmarca el checkbox si el item no está en la lista
                    }
                }




            }



            // Ojo

            var controlesEspeciales = flowLayoutPanel2.Controls.OfType<Selector_Cantidad>();

            foreach (var control in controlesEspeciales)
            {
                if (Lista_de_Items.ContainsKey(control.label1.Text))
                {
                    // Si el código ya existe, actualiza la cantidad seleccionada
                    Lista_de_Items[control.label1.Text].cantidadSelecccionItem = Convert.ToDouble(control.numericUpDown1.Value);
                    control.numericUpDown1.Value = Convert.ToDecimal(Lista_de_Items[control.label1.Text].cantidadSelecccionItem);
                    //control.checkBox1.Checked = true; // Asegura que el checkbox esté marcado si ya existe el item
                }
                else
                {
                    // Si no existe, lo agrega a la lista de items
                    Lista_de_Items.Add(control.label1.Text, new Miniatura_Producto
                    {
                        Codigo = control.label1.Text,
                        cantidadSelecccionItem = Convert.ToDouble(control.numericUpDown1.Value)
                    });
                }


            }

            //Ojo





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
            dataGridView1.Columns.Add("Secuencial_Producto", "SP");
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

                    Selector_Cantidad selector_Cantidad = new Selector_Cantidad();



                    if (miniatura_Producto1.Seleccionado == true)
                    {
                        flowLayoutPanel2.Controls.Remove(selector_Cantidad);

                        if (!flowLayoutPanel2.Controls.Contains(selector_Cantidad))
                        {
                            //selector_Cantidad.label1.Text = miniatura_Producto1.Codigo;
                            selector_Cantidad.SetCodigo(miniatura_Producto1.Codigo);
                            selector_Cantidad.Tag = miniatura_Producto1.Codigo; // Asigna el código como Tag para referencia futura


                            flowLayoutPanel2.Controls.Add(selector_Cantidad);



                        }



                        if (!Lista_de_Items.ContainsKey(miniatura_Producto1.Codigo))
                        {
                            Lista_de_Items.Add(miniatura_Producto1.Codigo, miniatura_Producto1);
                        }
                        else
                        {                             // Si ya existe, actualiza la cantidad seleccionada

                            flowLayoutPanel2.Controls.Remove(selector_Cantidad);
                            Lista_de_Items.Remove(miniatura_Producto1.Codigo);
                            await Task.Delay(100); // Espera para evitar problemas de concurrencia
                            Lista_de_Items.Add(miniatura_Producto1.Codigo, miniatura_Producto1);



                        }




                    }

                };








                flowLayoutPanel1.Controls.Add(miniatura_Producto1);





                var controlesMiniatura = flowLayoutPanel1.Controls.OfType<Miniatura_Producto>();

                foreach (var control in controlesMiniatura)
                {
                    if (Lista_de_Items.ContainsKey(control.Codigo))
                    {
                        control.Item_Seleccionado.Checked = true; // Marca el checkbox si el item ya está en la lista

                    }
                    else
                    {
                        control.Item_Seleccionado.Checked = false; // Desmarca el checkbox si el item no está en la lista
                    }
                }




            }            //---------------Fin del filtro que uso



            // Ojo

            var controlesEspeciales = flowLayoutPanel2.Controls.OfType<Selector_Cantidad>();

            foreach (var control in controlesEspeciales)
            {
                if (Lista_de_Items.ContainsKey(control.label1.Text))
                {
                    // Si el código ya existe, actualiza la cantidad seleccionada
                    Lista_de_Items[control.label1.Text].cantidadSelecccionItem = Convert.ToDouble(control.numericUpDown1.Value);
                    control.numericUpDown1.Value = Convert.ToDecimal(Lista_de_Items[control.label1.Text].cantidadSelecccionItem);
                    // control.checkBox1.Checked = true; // Asegura que el checkbox esté marcado si ya existe el item
                }
                else
                {
                    // Si no existe, lo agrega a la lista de items
                    Lista_de_Items.Add(control.label1.Text, new Miniatura_Producto
                    {
                        Codigo = control.label1.Text,
                        cantidadSelecccionItem = Convert.ToDouble(control.numericUpDown1.Value)
                    });
                }


            }

            //Ojo




        }




        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            V_Producto producto = new V_Producto();
            producto.Secuencial_Usuario = Secuencial_Usuario;

            producto.ShowDialog();
            Cargar_Items();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            V_Cliente cliente = new V_Cliente();
            cliente.Secuencial_Usuario = Secuencial_Usuario;
            cliente.ShowDialog();
            llenar_Combo_Cliente();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filtrar(comboBox2.SelectedItem.ToString(), textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();
            lbl_sub_Total.Text = "0.00"; // Reiniciar el subtotal a 0.00
            label10.Visible = false; // Ocultar la etiqueta de impuesto
            label11.Visible = false; // Ocultar la etiqueta de descuento    
            label12.Visible = false; // Ocultar la etiqueta de impuesto 
            otrosCargos = 0.00; // Reiniciar otros cargos a 0.00
            impuesto = 0.00; // Reiniciar impuesto a 0.00
            descuento = 0.00; // Reiniciar descuento a 0.00 
            label9.Visible = false; // Ocultar la etiqueta de otros cargos
            lbl_OtrosCargos.Visible = false; // Ocultar la etiqueta de otros cargos
            txt_Impuesto.Visible = false; // Ocultar el campo de impuesto
            txt_Descuento.Visible = false; // Ocultar el campo de descuento
            txt_OtrosCargos.Visible = false; // Ocultar el campo de otros cargos
            lbl_Impuesto.Visible = false; // Ocultar la etiqueta de impuesto
            lbl_Descuento.Visible = false; // Ocultar la etiqueta de descuento

            foreach (Selector_Cantidad selector in flowLayoutPanel2.Controls)
            {
                if (Lista_de_Items.ContainsKey(selector.GetCodigo()))
                {
                    Lista_de_Items[selector.GetCodigo()].cantidadSelecccionItem = Convert.ToDouble(selector.numericUpDown1.Value);
                }
                else
                {
                    // MessageBox.Show("El item " + selector.GetCodigo() + " no está en la lista de items seleccionados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // return;
                }
            }






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
                       item.Precio_Venta * item.cantidadSelecccionItem,
                       item.Secuencial
                    );


                    lbl_sub_Total.Text = (Convert.ToDouble(lbl_sub_Total.Text) + (item.Precio_Venta * item.cantidadSelecccionItem)).ToString("0.00");
                    subTotal = Convert.ToDouble(lbl_sub_Total.Text); // Actualizar el subtotal con el nuevo valor

                }
                else
                {

                    MessageBox.Show("Revise la cantidad que desea agregar. -- " + item.Codigo + " --\n\nDescripcion: " + item.Descripcion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }




            }


            Actualizar_Numeros(); // Actualizar los números en los labels correspondientes

        }



        private void Actualizar_Numeros() {


            // Actualizar los números en los labels correspondientes
            lbl_sub_Total.Text = subTotal.ToString("0.00");
            lbl_Impuesto.Text = impuesto.ToString("0.00");
            lbl_OtrosCargos.Text = otrosCargos.ToString("0.00");
            lbl_Descuento.Text = descuento.ToString("0.00");
            total = subTotal + impuesto + otrosCargos - descuento; // Calcular el total
            lbl_Total.Text = total.ToString("0.00"); // Mostrar el total en el label correspondiente
          //  impuesto = (impuesto / 100) * subTotal; // Convertir el porcentaje a decimal
          //  otrosCargos = double.Parse(txt_OtrosCargos.Text);


            
           // descuento = (descuento / 100) * subTotal; // Convertir el porcentaje a decimal


        }




        private void button7_Click(object sender, EventArgs e)
        {
            var x = MessageBox.Show("¿Está seguro de que desea limpiar la factura?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (x == DialogResult.No)
            {
                return; // Si el usuario selecciona "No", no se limpia la factura
            }
                textBox1.Text = "";
            Lista_de_Items.Clear();

            dataGridView1.Rows.Clear();
            flowLayoutPanel2.Controls.Clear();

            lbl_Impuesto.Text = "0.00";
            lbl_sub_Total.Text = "0.00"; // Reiniciar el subtotal a 0.00
            lbl_Total.Text = "0.00"; // Reiniciar el total a 0.00   
            lbl_OtrosCargos.Text = "0.00"; // Reiniciar otros cargos a 0.00
            lbl_Descuento.Text = "0.00"; // Reiniciar descuento a 0.00
            lbl_Descuento.Visible = false; // Ocultar la etiqueta de descuento
            lbl_Impuesto.Visible = false; // Ocultar la etiqueta de impuesto
            lbl_OtrosCargos.Visible = false; // Ocultar la etiqueta de otros cargos
            txt_Impuesto.Visible = false; // Ocultar el campo de impuesto
            txt_Descuento.Visible = false; // Ocultar el campo de descuento
            txt_OtrosCargos.Visible = false; // Ocultar el campo de otros cargos
            label10.Visible = false; // Ocultar la etiqueta de impuesto
            label11.Visible = false; // Ocultar la etiqueta de descuento
            label12.Visible = false; // Ocultar la etiqueta de impuesto
            label13.Visible = false; // Ocultar la etiqueta de descuento
            label9.Visible = false; // Ocultar la etiqueta de otros cargos
            txt_Impuesto.Text = "0.00"; // Reiniciar el campo de impuesto a 0.00


            otrosCargos = 0.00; // Reiniciar otros cargos a 0.00
            impuesto = 0.00; // Reiniciar impuesto a 0.00
            descuento = 0.00; // Reiniciar descuento a 0.00

            txt_OtrosCargos.Text = "0.00"; // Reiniciar el campo de otros cargos a 0.00
            txt_OtrosCargos.Visible = false; // Ocultar el campo de otros cargos
            lbl_OtrosCargos.Visible = false; // Ocultar la etiqueta de otros cargos
            label9.Visible = false; // Ocultar la etiqueta de otros cargos

            total = 0.00; // Reiniciar el total a 0.00
            subTotal = 0.00; // Reiniciar el subtotal a 0.00

            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            Cargar_Items();
            llenar_Combo_Cliente();
            comboCliente.SelectedIndex = -1; // Limpiar la selección del cliente

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






            var controlesEspeciales1 = flowLayoutPanel2.Controls.OfType<Selector_Cantidad>();

            foreach (var control in controlesEspeciales1)
            {


                if (Lista_de_Items.ContainsKey(control.label1.Text))
                {
                    // Si el código ya existe, actualiza la cantidad seleccionada
                    Lista_de_Items[control.label1.Text].cantidadSelecccionItem = Convert.ToDouble(control.numericUpDown1.Value);
                    control.numericUpDown1.Value = Convert.ToDecimal(Lista_de_Items[control.label1.Text].cantidadSelecccionItem);

                }



            }







            //Doble Ojo



            var controlesEspeciales = flowLayoutPanel2.Controls.OfType<Selector_Cantidad>();

            foreach (var control in controlesEspeciales)
            {

                if (control.checkBox1.Checked == true)
                {

                    if (Lista_de_Items.ContainsKey(control.label1.Text))
                    {

                        MessageBox.Show("El item " + control.label1.Text + " se removio de la factura");
                        flowLayoutPanel2.Controls.Remove(control); // Elimina el control del FlowLayoutPanel
                        Lista_de_Items.Remove(control.label1.Text); // Elimina el item de la lista de items


                        flowLayoutPanel2.Refresh(); // Refresca el FlowLayoutPanel para que se actualice la vista

                    }
                    else
                    {
                        control.BackColor = Color.Red; // Desmarca el checkbox si el item no está en la lista
                    }

                }


            }





            Cargar_Items();

            label5.Text = Lista_de_Items.Count.ToString();
            button2_Click(sender, e); // Actualiza el DataGridView con los items seleccionados

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
            label5.Text = Lista_de_Items.Count.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {






        }

        private void lbl_sub_Total_TextChanged(object sender, EventArgs e)
        {


        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txt_OtrosCargos.Visible = true;
            txt_OtrosCargos.Focus();
            txt_OtrosCargos.Text = "";
        }

        private void txt_OtrosCargos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                otrosCargos = double.Parse(txt_OtrosCargos.Text);

            }
            catch
            {
                otrosCargos = 0.00; // Si hay un error al convertir, se establece en 0.00
            }

        }

        private void txt_OtrosCargos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_OtrosCargos.Visible = false;
                lbl_OtrosCargos.Visible = true;
                label9.Visible = true;

                lbl_OtrosCargos.Text = otrosCargos.ToString("0.00");
            }
        }

        private void groupBox2_Move(object sender, EventArgs e)
        {


        }

        private void groupBox2_MouseHover(object sender, EventArgs e)
        {
            txt_OtrosCargos.Visible = false; // Asegurarse de que el campo de otros cargos esté oculto al mover el grupo
            txt_Descuento.Visible = false; // Asegurarse de que el campo de descuento esté oculto al mover el grupo
            txt_Impuesto.Visible = false;
            label12.Visible = false; // Asegurarse de que la etiqueta de impuesto esté oculta al mover el grupo
            label13.Visible = false; // Asegurarse de que la etiqueta de descuento esté oculta al mover el grupo
            

            Actualizar_Numeros(); // Actualizar los números en los labels correspondientes
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txt_Impuesto.Visible = true;
            txt_Impuesto.Text = "";
            txt_Impuesto.Focus();
            label12.Visible = true; // Mostrar la etiqueta de impuesto al hacer clic en el enlace
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txt_Descuento.Visible = true;
            txt_Descuento.Focus();
            txt_Descuento.Text = "";
            label13.Visible = true; // Mostrar la etiqueta de descuento al hacer clic en el enlace
        }

        private void txt_Impuesto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                impuesto = double.Parse(txt_Impuesto.Text);
                impuesto = (impuesto / 100)*subTotal; // Convertir el porcentaje a decimal

            }
            catch
            {
                impuesto = 0.00; // Si hay un error al convertir, se establece en 0.00
            }
        }

        private void txt_Impuesto_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txt_Impuesto.Visible = false;
                lbl_Impuesto.Visible = true;
                label10.Visible = true;
                label12.Visible = false; // Ocultar la etiqueta de impuesto al presionar Enter
                lbl_Impuesto.Text = impuesto.ToString("0.00");
            }
        }

        private void txt_Descuento_TextChanged(object sender, EventArgs e)
        {
            try
            {
                descuento = double.Parse(txt_Descuento.Text);
                descuento = (descuento / 100)*subTotal; // Convertir el porcentaje a decimal

            }
            catch
            {
                descuento = 0.00; // Si hay un error al convertir, se establece en 0.00
            }
        }

        private void txt_Descuento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Descuento.Visible = false;
                label13.Visible = false; // Ocultar la etiqueta de descuento al presionar Enter 
                label11.Visible = true;

                lbl_Descuento.Text = descuento.ToString("0.00");
                lbl_Descuento.Visible = true; // Mostrar la etiqueta de descuento al presionar Enter
            }
        }

        private void lbl_sub_Total_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
        
            Actualizar_Numeros(); // Actualizar los números en los labels correspondientes

        }
    }
}