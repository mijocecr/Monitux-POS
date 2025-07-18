using Humanizer;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Monitux_POS.Clases;
using Monitux_POS.Controles;
using PdfiumViewer;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Monitux_POS.Clases.Util;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Monitux_POS.Ventanas
{


    public partial class V_Factura_Venta : Form
    {

        public static int Secuencial_Usuario { get; set; } = V_Menu_Principal.Secuencial_Usuario;

        double subTotal = 0.00; // Variable para almacenar el subtotal
        double impuesto = 0.00; // Variable para almacenar el impuesto
        double total = 0.00; // Variable para almacenar el total
        double otrosCargos = 0.00; // Variable para almacenar otros cargos
        double descuento = 0.00; // Variable para almacenar el descuento aplicado
                                 //  public static string moneda = "USD"; // Variable para almacenar la moneda utilizada en la factura


        public static Dictionary<string, Miniatura_Producto> Lista_de_Items = new Dictionary<string, Miniatura_Producto>();


        public V_Factura_Venta()
        {
            InitializeComponent();
        }



        public void Cargar_Items()
        {




            flowLayoutPanel1.Controls.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();


            context.Database.EnsureCreated(); // Crea la base de datos si no existe




            // **READ**

            var productos = context.Productos
    .Where(p => p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
    .ToList();



            int i = 0;
            Console.WriteLine("Lista de productos:");

            foreach (var item in productos)
            {




                Miniatura_Producto miniatura_Producto1 = new Miniatura_Producto();

                miniatura_Producto1.Cantidad = item.Cantidad;
                miniatura_Producto1.Imagen = item.Imagen;
                miniatura_Producto1.Secuencial_Empresa=item.Secuencial_Empresa;
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
                miniatura_Producto1.Secuencial_Usuario = Secuencial_Usuario;
                miniatura_Producto1.Fecha_Caducidad = item.Fecha_Caducidad;
                miniatura_Producto1.Expira = Convert.ToBoolean(item.Expira);
                miniatura_Producto1.moneda = V_Menu_Principal.moneda; // Asignar la moneda a la miniatura del producto
                miniatura_Producto1.Tipo = item.Tipo; // Asignar el tipo de producto (si es necesario)
                miniatura_Producto1.Origen = "Factura_Venta"; // Asignar el origen del producto a "Factura_Venta"



                miniatura_Producto1.Item_Imagen.Click += async (s, ev) =>
                {


                    Selector_Cantidad selector_Cantidad = new Selector_Cantidad();













                    if (miniatura_Producto1.Seleccionado == true)
                    {
                        //flowLayoutPanel2.Controls.Remove(selector_Cantidad);

                        if (!flowLayoutPanel2.Controls.Contains(selector_Cantidad))
                        {
                            if (miniatura_Producto1.Tipo == "Servicio")
                            {
                                selector_Cantidad.BackColor = Color.LightGray;

                            }

                            selector_Cantidad.SetCodigo(miniatura_Producto1.Codigo);
                            selector_Cantidad.Tag = miniatura_Producto1.Codigo; // Asigna el código como Tag para referencia futura


                            flowLayoutPanel2.Controls.Add(selector_Cantidad);

                            // Hacer scroll automáticamente al último control agregado
                            flowLayoutPanel2.AutoScrollPosition = new Point(0, flowLayoutPanel2.DisplayRectangle.Height);



                        }



                        if (!Lista_de_Items.ContainsKey(miniatura_Producto1.Codigo))
                        {
                            Lista_de_Items.Add(miniatura_Producto1.Codigo, miniatura_Producto1);
                        }
                        else
                        {                             // Si ya existe, actualiza la cantidad seleccionada

                            flowLayoutPanel2.Controls.Remove(selector_Cantidad);


                            var item = Lista_de_Items.Values.FirstOrDefault(x => x.Codigo == miniatura_Producto1.Codigo);
                            if (item != null)
                            {
                                item.Precio_Venta = miniatura_Producto1.Precio_Venta;
                            }


                            // Lista_de_Items.Remove(miniatura_Producto1.Codigo);

                            await Task.Delay(100); // Espera para evitar problemas de concurrencia

                            //  Lista_de_Items.Add(miniatura_Producto1.Codigo, miniatura_Producto1);





                        }




                    }

                };







                // flowLayoutPanel1.Controls.Add(miniatura_Producto1);//118; 156
                // AnimacionesUI.AnimarCrecimiento(miniatura_Producto1, new Size(118, 156));

                //Original
                flowLayoutPanel1.Controls.Add(miniatura_Producto1);
                //Original


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

            // Filtrar solo clientes activos
            var clientesActivos = context.Clientes.Where(c => (bool)c.Activo && c.Secuencial_Empresa==V_Menu_Principal.Secuencial_Empresa).ToList();

            foreach (var item in clientesActivos)
            {
                comboCliente.Items.Add(item.Secuencial + " - " + item.Nombre);
            }





        }


        /* La idea aqui es importar una lista de productos desde una cotizacion y agregarla a la factura de venta.
         * sin modificar el funcionamiento actual de la creacion de la factura. lo que pretendo es con la clave del diccionario
         * se consulte el producto y se agregue a la lista de items de la factura. y con el valor del diccionario se agregue la cantidad de seleccion
         * del producto a la factura. mientras se carga cada poducto se agregara un selector de cantidad para que el usuario pueda modificar la cantidad
        falta darle un par de vueltas a la logica de como se agregara el producto a la factura, pero creo que es un buen comienzo.

         */
        public async Task Importar_Cotizacion(Dictionary<string, double> Lista, string cliente)
        {

            comboCliente.SelectedItem = cliente; // Seleccionar el cliente en el comboBox

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe



            foreach (var item_C in Lista)
            {
                ////////////////////////////////////////////////////////



                var productos = context.Productos
                        .Where(c => EF.Property<string>(c, "Codigo").Equals(item_C.Key) && c.Secuencial_Empresa==V_Menu_Principal.Secuencial_Empresa)
                        .ToList();

                foreach (var item in productos)
                {
                    Miniatura_Producto miniatura_Producto = new Miniatura_Producto();



                    miniatura_Producto.Cantidad = item.Cantidad;
                    miniatura_Producto.Imagen = item.Imagen;
                    miniatura_Producto.Secuencial_Empresa=item.Secuencial_Empresa;
                    miniatura_Producto.Secuencial = item.Secuencial;
                    miniatura_Producto.Codigo = item.Codigo;
                    miniatura_Producto.Marca = item.Marca;
                    miniatura_Producto.Descripcion = item.Descripcion;
                    miniatura_Producto.Precio_Venta = item.Precio_Venta;
                    miniatura_Producto.Precio_Costo = item.Precio_Costo;
                    miniatura_Producto.Existencia_Minima = item.Existencia_Minima;
                    miniatura_Producto.Codigo_Barra = item.Codigo_Barra;
                    miniatura_Producto.Codigo_Fabricante = item.Codigo_Fabricante;
                    miniatura_Producto.Codigo_QR = item.Codigo_QR;
                    miniatura_Producto.Secuencial_Proveedor = item.Secuencial_Proveedor;
                    miniatura_Producto.Secuencial_Categoria = item.Secuencial_Categoria;
                    miniatura_Producto.Secuencial_Usuario = Secuencial_Usuario;
                    miniatura_Producto.Fecha_Caducidad = item.Fecha_Caducidad;
                    miniatura_Producto.Item_Seleccionado.Checked = true;
                    // miniatura_Producto.moneda = moneda; // Asignar la moneda a la miniatura del producto
                    miniatura_Producto.Expira = item.Expira; // Asignar si el producto expira o no
                    miniatura_Producto.cantidadSelecccionItem = Lista.TryGetValue(item_C.Key, out double cantidad) ? cantidad : 0.0; // Asignar la cantidad seleccionada desde el diccionario, o 0.0 si no se encuentra
                    miniatura_Producto.Tipo = item.Tipo;
                    miniatura_Producto.Origen = "Factura_Venta"; // Asignar el origen del producto a "Factura_Venta"
                    Selector_Cantidad selector_Cantidad = new Selector_Cantidad();
                    selector_Cantidad.SetCodigo(miniatura_Producto.Codigo);
                    selector_Cantidad.numericUpDown1.Value = Convert.ToDecimal(miniatura_Producto.cantidadSelecccionItem);
                    if (Lista_de_Items.ContainsKey(miniatura_Producto.Codigo))
                    {
                        // Si el código ya existe, actualiza la cantidad seleccionada
                        // Lista_de_Items[miniatura_Producto.Codigo].cantidadSelecccionItem = miniatura_Producto.cantidadSelecccionItem;
                        Lista_de_Items.Remove(miniatura_Producto.Codigo);
                        Lista_de_Items.Add(miniatura_Producto.Codigo, miniatura_Producto);//Ojo Aquii
                        selector_Cantidad.numericUpDown1.Value = Convert.ToDecimal(Lista_de_Items[selector_Cantidad.label1.Text].cantidadSelecccionItem);

                    }



                    else
                    {


                        // Si no existe, lo agrega a la lista de items
                        Lista_de_Items.Add(miniatura_Producto.Codigo, miniatura_Producto); // Agregar el producto a la lista de items usando su código como clave
                        flowLayoutPanel2.Controls.Add(selector_Cantidad); // Agregar el selector de cantidad al FlowLayoutPanel

                    }




                }


            }



            button2.PerformClick();

        }



        private void V_Factura_Venta_Load(object sender, EventArgs e)
        {

            lbl_Descuento.Text = $"{lbl_Descuento:0.00}";
            lbl_Impuesto.Text = $"{lbl_Impuesto:0.00}";
            lbl_OtrosCargos.Text = $"{lbl_OtrosCargos:0.00}";
            lbl_sub_Total.Text = $"{lbl_sub_Total:0.00}";
            lbl_Total.Text = $"{lbl_Total:0.00}";
            Cargar_Items();
            llenar_Combo_Cliente();
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            Configurar_DataGridView();
            Actualizar_Numeros(); // Actualizar los números al cargar la ventana



        }

        public void Configurar_DataGridView()
        {

            //dataGridView1.Enabled = false;


            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


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
                    .Where(c => EF.Property<string>(c, campo).Contains(valor) && c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
                    .ToList();

            flowLayoutPanel1.Controls.Clear();


            foreach (var item in productos)
            {




                Miniatura_Producto miniatura_Producto1 = new Miniatura_Producto();

                miniatura_Producto1.Cantidad = item.Cantidad;
                miniatura_Producto1.Imagen = item.Imagen;
                miniatura_Producto1.Secuencial_Empresa=item.Secuencial_Empresa;
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
                miniatura_Producto1.Secuencial_Usuario = Secuencial_Usuario;
                miniatura_Producto1.Fecha_Caducidad = item.Fecha_Caducidad;
                miniatura_Producto1.Expira = Convert.ToBoolean(item.Expira);
                miniatura_Producto1.moneda = V_Menu_Principal.moneda; // Asignar la moneda a la miniatura del producto
                miniatura_Producto1.Tipo = item.Tipo; // Asignar el tipo de producto (si es necesario)
                miniatura_Producto1.Origen = "Factura_Venta"; // Asignar el origen del producto a "Factura_Venta"
                /* miniatura_Producto1.Item_Imagen.Click += (s, ev) =>
                {
                    
                };*/



                miniatura_Producto1.Item_Imagen.Click += async (s, ev) =>
                {


                    Selector_Cantidad selector_Cantidad = new Selector_Cantidad();



                    if (miniatura_Producto1.Seleccionado == true)
                    {
                        flowLayoutPanel2.Controls.Remove(selector_Cantidad);

                        if (!flowLayoutPanel2.Controls.Contains(selector_Cantidad))
                        {
                            if (miniatura_Producto1.Tipo == "Servicio")
                            {
                                selector_Cantidad.BackColor = Color.LightGray;

                            }

                            //selector_Cantidad.label1.Text = miniatura_Producto1.Codigo;
                            selector_Cantidad.SetCodigo(miniatura_Producto1.Codigo);
                            selector_Cantidad.Tag = miniatura_Producto1.Codigo; // Asigna el código como Tag para referencia futura


                            flowLayoutPanel2.Controls.Add(selector_Cantidad);
                            flowLayoutPanel2.AutoScrollPosition = new Point(0, flowLayoutPanel2.DisplayRectangle.Height);


                        }



                        if (!Lista_de_Items.ContainsKey(miniatura_Producto1.Codigo))
                        {

                            Lista_de_Items.Add(miniatura_Producto1.Codigo, miniatura_Producto1);
                        }
                        else
                        {                             // Si ya existe, actualiza la cantidad seleccionada



                            var item = Lista_de_Items.Values.FirstOrDefault(x => x.Codigo == miniatura_Producto1.Codigo);
                            if (item != null)
                            {
                                item.Precio_Venta = miniatura_Producto1.Precio_Venta;
                            }




                            flowLayoutPanel2.Controls.Remove(selector_Cantidad);
                            // Lista_de_Items.Remove(miniatura_Producto1.Codigo);
                            await Task.Delay(100); // Espera para evitar problemas de concurrencia
                                                   // Lista_de_Items.Add(miniatura_Producto1.Codigo, miniatura_Producto1);



                        }




                    }

                };


                //  flowLayoutPanel1.Controls.Add(miniatura_Producto1);//118; 156
                //  AnimacionesUI.AnimarCrecimiento(miniatura_Producto1, new Size(118, 156));





                //Original
                flowLayoutPanel1.Controls.Add(miniatura_Producto1);
                //Original




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

            subTotal = 0;
            total = 0;
            dataGridView1.Rows.Clear();
            lbl_sub_Total.Text = "0.00"; // Reiniciar el subtotal a 0.00
            lbl_Total.Text = "0.00";
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
                    Actualizar_Numeros(); // Actualizar los números en los labels correspondientes
                }
                else
                {

                    V_Menu_Principal.MSG.ShowMSG("Revise la cantidad que desea agregar.\n -- " + item.Codigo + " --\n" + item.Descripcion, "Error");
                    return;
                }




            }




        }



        private void Actualizar_Numeros()
        {


            // Actualizar los números en los labels correspondientes

            total = subTotal + impuesto + otrosCargos - descuento; // Calcular el total
            lbl_Total.Text = total.ToString("0.00");
            lbl_sub_Total.Text = subTotal.ToString("0.00");




        }




        private void button7_Click(object sender, EventArgs e)
        {
            var x = V_Menu_Principal.MSG.ShowMSG("¿Está seguro de que desea limpiar la factura?", "Confirmación");
            if (x == DialogResult.No)
            {
                return; // Si el usuario selecciona "No", no se limpia la factura
            }

            Limpiar_Factura(); // Llama al método para limpiar la factura

        }


        public void Limpiar_Factura()
        {

            V_Importar_Cotizacion.Lista.Clear();
            button5.Enabled = true;
            dateTimePicker1.Value = DateTime.Now; // Reiniciar la fecha al valor actual
            textBox1.Text = "";
            Lista_de_Items.Clear();

            dataGridView1.Rows.Clear();
            flowLayoutPanel2.Controls.Clear();
            lbl_Total.Text = total.ToString("0.00"); // Mostrar el total en el label correspondiente

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

            lbl_sub_Total.Text = subTotal.ToString("0.00");
            lbl_Impuesto.Text = impuesto.ToString("0.00");
            lbl_OtrosCargos.Text = otrosCargos.ToString("0.00");
            lbl_Descuento.Text = descuento.ToString("0.00");

            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            Cargar_Items();
            llenar_Combo_Cliente();
            comboCliente.SelectedIndex = -1; // Limpiar la selección del cliente
            Util.Limpiar_Cache(V_Menu_Principal.Secuencial_Empresa); // Limpiar la caché de la aplicación

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
                comboBox1.SelectedItem = "Efectivo";
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

            // Filtrar solo clientes activos cuyo teléfono contiene el texto ingresado
            var clientes = context.Clientes
                .Where(c => (bool)c.Activo && c.Secuencial_Empresa==V_Menu_Principal.Secuencial_Empresa &&EF.Property<string>(c, "Telefono").Contains(textBox2.Text))
                .ToList();

            foreach (var item in clientes)
            {
                comboCliente.Items.Add(item.Secuencial.ToString() + " - " + item.Nombre);
                comboCliente.SelectedItem = item.Secuencial.ToString() + " - " + item.Nombre;
            }


        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.Limpiar_Cache(V_Menu_Principal.Secuencial_Empresa);
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

                        V_Menu_Principal.MSG.ShowMSG("El item " + control.label1.Text + " se removio de la factura", "Ventas");
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
            V_Importar_Cotizacion.Lista.Clear();
            button5.Enabled = false; // Deshabilitar el botón para evitar múltiples clics
            Lista_de_Items.Clear(); // Limpiar la lista de items seleccionados
            flowLayoutPanel2.Controls.Clear(); // Limpiar el FlowLayoutPanel de selectores de cantidad
            V_Importar_Cotizacion importar_Cotizacion = new V_Importar_Cotizacion();
            importar_Cotizacion.ShowDialog();
            Importar_Cotizacion(V_Importar_Cotizacion.Lista, V_Importar_Cotizacion.cliente_seleccionado);
            label5.Text = Lista_de_Items.Count.ToString(); // Actualizar el contador de items seleccionados
            Cargar_Items(); // Recargar los items en el FlowLayoutPanel

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
                Actualizar_Numeros();
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
                impuesto = (impuesto / 100) * subTotal; // Convertir el porcentaje a decimal

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
                Actualizar_Numeros();
            }
        }

        private void txt_Descuento_TextChanged(object sender, EventArgs e)
        {
            try
            {
                descuento = double.Parse(txt_Descuento.Text);
                descuento = (descuento / 100) * subTotal; // Convertir el porcentaje a decimal

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
                Actualizar_Numeros();
            }
        }

        private void lbl_sub_Total_Click(object sender, EventArgs e)
        {

        }



        public List<Item_Factura> ObtenerItemsDesdeGrid(DataGridView dgv)
        {
            var lista = new List<Item_Factura>();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    lista.Add(new Item_Factura
                    {
                        Codigo = row.Cells["Codigo"].Value?.ToString(),
                        Descripcion = row.Cells["Descripcion"].Value?.ToString(),
                        Cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value),
                        Precio = (double)Convert.ToDecimal(row.Cells["Precio_Venta"].Value)
                    });
                }
            }

            return lista;
        }



        private void button6_Click(object sender, EventArgs e)
        {

            Actualizar_Numeros(); // Actualizar los números en los labels correspondientes


            if (comboBox3.SelectedItem == "Credito")
            {
                comboBox1.SelectedItem = "Ninguno";
                if (dateTimePicker1.Value <= DateTime.Now)
                {
                    V_Menu_Principal.MSG.ShowMSG("La fecha de vencimiento no es valida.", "Error");
                    return; // Si la fecha de vencimiento es anterior a la fecha actual, no se puede registrar la venta
                }
            }


            if (Lista_de_Items.Count == 0)
            {
                V_Menu_Principal.MSG.ShowMSG("No hay items seleccionados para registrar la venta.", "Error");
                return; // Si no hay items seleccionados, no se puede registrar la venta
            }

            if (comboCliente.SelectedIndex == -1)
            {
                V_Menu_Principal.MSG.ShowMSG("Debe seleccionar un cliente para registrar la venta.", "Error");
                return; // Si no se ha seleccionado un cliente, no se puede registrar la venta
            }

            if (comboBox3.SelectedIndex == -1)
            {
                V_Menu_Principal.MSG.ShowMSG("Debe seleccionar un tipo de venta.", "Error");
                return; // Si no se ha seleccionado un tipo de venta, no se puede registrar la venta
            }

            if (comboBox1.SelectedIndex == -1)
            {
                V_Menu_Principal.MSG.ShowMSG("Debe seleccionar una forma de pago.", "Error");
                return; // Si no se ha seleccionado una forma de pago, no se puede registrar la venta
            }
            if (total <= 0)
            {
                V_Menu_Principal.MSG.ShowMSG("El total de la venta debe ser mayor a cero.", "Error");
                return; // Si el total es menor o igual a cero, no se puede registrar la venta
            }



            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencial = context.Ventas.Any() ? context.Ventas.Max(p => p.Secuencial) + 1 : 1;

            // Crear y guardar la venta
            var venta = new Venta
            {
               Secuencial_Empresa=V_Menu_Principal.Secuencial_Empresa,
                Secuencial = secuencial,
                Secuencial_Cliente = comboCliente.SelectedIndex != -1
                    ? int.Parse(comboCliente.SelectedItem.ToString().Split('-')[0].Trim())
                    : 0,
                Secuencial_Usuario = Secuencial_Usuario,
                Fecha = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"),
                Tipo = comboBox3.SelectedItem.ToString(),
                Forma_Pago = comboBox1.SelectedItem.ToString(),
                Total = Math.Round(subTotal, 2),
                Gran_Total = Math.Round(total, 2),
                Impuesto = impuesto,
                Otros_Cargos = otrosCargos,
                Descuento = descuento
            };
            context.Ventas.Add(venta);
            context.SaveChanges();

            // Agregar detalles de venta y actualizar stock
            foreach (var pro in Lista_de_Items.Values)
            {
                var detalle = new Venta_Detalle
                {
                    Secuencial_Empresa= venta.Secuencial_Empresa,
                    Secuencial_Factura = venta.Secuencial,
                    Secuencial_Cliente = venta.Secuencial_Cliente,
                    Secuencial_Usuario = venta.Secuencial_Usuario,
                    Fecha = venta.Fecha,
                    Secuencial_Producto = pro.Secuencial,
                    Codigo = pro.Codigo,
                    Descripcion = pro.Descripcion,
                    Cantidad = pro.cantidadSelecccionItem,
                    Precio = Math.Round(pro.Precio_Venta, 2),
                    Total = Math.Round(pro.cantidadSelecccionItem * pro.Precio_Venta, 2),
                    Tipo = pro.Tipo
                };

                context.Ventas_Detalles.Add(detalle);

                if (detalle.Tipo != "Servicio")
                {
                    Util.Registrar_Movimiento_Kardex(pro.Secuencial, pro.Cantidad, pro.Descripcion,
                        pro.cantidadSelecccionItem, pro.Precio_Costo, pro.Precio_Venta, "Salida", V_Menu_Principal.Secuencial_Empresa);

                    var producto = context.Productos.FirstOrDefault(p => p.Secuencial == pro.Secuencial);
                    if (producto != null)
                        producto.Cantidad = pro.Cantidad - pro.cantidadSelecccionItem;
                }
            }
            context.SaveChanges();

            // Registrar cuentas por cobrar si aplica
            if (venta.Tipo == "Credito")
            {
                var cuenta = new Cuentas_Cobrar
                {
                    Secuencial_Empresa=venta.Secuencial_Empresa,
                    Secuencial_Factura = venta.Secuencial,
                    Secuencial_Cliente = venta.Secuencial_Cliente,
                    Secuencial_Usuario = venta.Secuencial_Usuario,
                    Fecha = venta.Fecha,
                    Total = Math.Round(total, 2),
                    Saldo = Math.Round(total, 2),
                    Pagado = 0.00,
                    Fecha_Vencimiento = dateTimePicker1.Value.ToString("dd/MM/yyyy"),
                    Descuento = venta.Descuento,
                    Otros_Cargos = venta.Otros_Cargos,
                    Impuesto = venta.Impuesto,
                    Gran_Total = venta.Gran_Total
                };

                context.Cuentas_Cobrar.Add(cuenta);
              
            }
            else
            {
                var ingreso = new Ingreso
                {
                    Secuencial_Empresa=venta.Secuencial_Empresa,
                    Secuencial_Factura = venta.Secuencial,
                    Secuencial_Usuario = venta.Secuencial_Usuario,
                    Fecha = venta.Fecha,
                    Total = Math.Round(total, 2),
                    Tipo_Ingreso = venta.Forma_Pago,
                    Descripcion = $"Venta según Factura: {venta.Secuencial}"
                };

                context.Ingresos.Add(ingreso);
            }
            context.SaveChanges();

            // Si se solicita calcular cambio
            if (checkBox1.Checked)
            {
                if (V_Menu_Principal.IPB.Show("Escriba la cantidad en números del dinero recibido por esta venta.",
                    "Cálculo del Cambio", out string recibido) == DialogResult.OK)
                {
                    if (double.TryParse(recibido?.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double monto))
                    {
                        double cambio = monto - total;
                        string mensaje = cambio >= 0
                            ? $"El Cambio a favor del Cliente es: {cambio}\n\n{Util.Convertir_Numeros_Palabras(cambio.ToString())} {V_Menu_Principal.moneda}"
                            : $"Falta Dinero: {Math.Abs(cambio)}\n\n{Util.Convertir_Numeros_Palabras(Math.Abs(cambio).ToString())} {V_Menu_Principal.moneda}";

                        V_Menu_Principal.MSG.ShowMSG(mensaje, "Ventas");
                    }
                    else
                    {
                        V_Menu_Principal.MSG.ShowMSG("Error: Solo se permiten números.", "Ventas");
                    }
                }
            }

            // Confirmar venta
            
           

            var factura = new FacturaCompletaPDF_Venta
            {
                Secuencial=venta.Secuencial,
                Cliente = comboCliente.SelectedItem.ToString()
    .Substring(comboCliente.SelectedItem.ToString().IndexOf("- ") + 2)
    .Trim(),

                TipoVenta = venta.Tipo,
                MetodoPago = venta.Forma_Pago,
                Fecha = venta.Fecha,
                Items = ObtenerItemsDesdeGrid(dataGridView1),
                ISV = (double)venta.Impuesto,
                OtrosCargos = (double)venta.Otros_Cargos,
                Descuento = (double)venta.Otros_Cargos
            };


            string rutaGuardado = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\FAV\\" + V_Menu_Principal.Secuencial_Empresa);

            factura.GeneratePdf($"{rutaGuardado}-{venta.Secuencial}-{factura.Cliente}.pdf");


            /////////////


            string rutaPdf = $"{rutaGuardado}-{venta.Secuencial}-{factura.Cliente}.pdf";
            //monitux.pos@gmail.com
            //ffeg qqnx zaij otmb

            var destinatariocliente = context.Clientes.FirstOrDefault(c => c.Secuencial == venta.Secuencial_Cliente);
            string ?destinatario="";
            if (destinatariocliente != null)
            {
               destinatario= destinatariocliente.Email;
            }

            Util.EnviarCorreoConPdf("monitux.pos@gmail.com",
                destinatario,
                V_Menu_Principal.Nombre_Empresa + " - " + "Comprobante",
                "Gracias por su compra. Adjunto tiene su comprobante.", 
                rutaPdf,
                "smtp.gmail.com", 
                587, 
                "monitux.pos", "ffeg qqnx zaij otmb");



          
            try { 

            // Comprobar si el archivo existe antes de abrirlo
            if (!File.Exists(rutaPdf))
            {
                V_Menu_Principal.MSG.ShowMSG($"El archivo no fue encontrado:\n{rutaPdf}", "Archivo no encontrado");
                return;
            }

            // Instanciar visor de factura
            V_Visor_Factura v_Visor_Factura = new V_Visor_Factura
            {
                rutaArchivo = rutaPdf,
                titulo = $"Factura de Venta No. {venta.Secuencial}"
            };
            v_Visor_Factura.ShowDialog();
        }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Se produjo un error inesperado:\n{ex.Message}", "Error");
            }



        


            /////////////

            if (venta.Tipo == "Credito")
            {
                V_Menu_Principal.MSG.ShowMSG("Venta al crédito registrada correctamente.\n\nRecuerde que debe cobrar la cuenta pendiente antes de la fecha de vencimiento.", "Éxito");
                Util.Registrar_Actividad(Secuencial_Usuario, $"Ha registrado una venta al crédito, factura: {venta.Secuencial}, por un valor de: {venta.Total} {V_Menu_Principal.moneda}", V_Menu_Principal.Secuencial_Empresa);
            }
            else
            {
                V_Menu_Principal.MSG.ShowMSG("Venta registrada correctamente.", "Éxito");
                Util.Registrar_Actividad(Secuencial_Usuario, $"Ha registrado una venta, factura: {venta.Secuencial}, por un valor de:  {Math.Round(total, 2)}   {V_Menu_Principal.moneda}", V_Menu_Principal.Secuencial_Empresa);
            }

            Limpiar_Factura();



            /*

            Venta venta = new Venta();
           

            V_Kardex kardex = new V_Kardex(); // Crear una instancia de Kardex para registrar el movimiento de inventario
            Ingreso ingreso = new Ingreso(); // Crear una instancia de Ingreso para registrar el ingreso de productos


            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            int secuencial = context.Ventas.Any() ? context.Ventas.Max(p => p.Secuencial) + 1 : 1;

            venta.Secuencial = secuencial;
            venta.Secuencial_Cliente = comboCliente.SelectedIndex != -1 ? int.Parse(comboCliente.SelectedItem.ToString().Split('-')[0].Trim()) : 0; // Obtener el secuencial del cliente seleccionado

            venta.Secuencial_Usuario = Secuencial_Usuario; // Asignar el secuencial del usuario que está realizando la venta
            venta.Fecha = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"); // Asignar la fecha y hora actual de la venta
            venta.Tipo = comboBox3.SelectedItem.ToString(); // Obtener el tipo de venta seleccionado
            venta.Total = Math.Round(subTotal, 2); // Asignar el total de la venta
            venta.Forma_Pago = comboBox1.SelectedItem.ToString(); // Obtener la forma de pago seleccionada
            venta.Gran_Total = Math.Round(total, 2); // Asignar el gran total de la venta

            venta.Impuesto = impuesto; // Asignar el impuesto de la venta
            venta.Otros_Cargos = otrosCargos; // Asignar los otros cargos de la venta
            venta.Descuento = descuento; // Asignar el descuento de la venta    
            context.Add(venta);
            context.SaveChanges(); // Guardar los cambios en la base de datos


            foreach (var pro in Lista_de_Items.Values)
            {

                SQLitePCL.Batteries.Init();

                using var context1 = new Monitux_DB_Context();
                context1.Database.EnsureCreated(); // Crea la base de datos si no existe

                Venta_Detalle venta_detalle = new Venta_Detalle(); // Crear una nueva instancia de Ventas_Detalles para cada producto en la venta


                venta_detalle.Secuencial_Factura = venta.Secuencial; // Asignar el secuencial de la venta al detalle de la venta
                venta_detalle.Secuencial_Cliente = venta.Secuencial_Cliente; // Asignar el secuencial del cliente al detalle de la venta
                venta_detalle.Secuencial_Usuario = venta.Secuencial_Usuario; // Asignar el secuencial del usuario al detalle de la venta

                venta_detalle.Fecha = venta.Fecha; // Asignar la fecha de la venta al detalle de la venta

                venta_detalle.Secuencial_Producto = pro.Secuencial; // Asignar el secuencial del producto al detalle de la venta
                venta_detalle.Codigo = pro.Codigo; // Asignar el código del producto al detalle de la venta
                venta_detalle.Descripcion = pro.Descripcion; // Asignar la descripción del producto al detalle de la venta
                venta_detalle.Cantidad = pro.cantidadSelecccionItem; // Asignar la cantidad del producto al detalle de la venta
                venta_detalle.Precio = Math.Round(pro.Precio_Venta, 2); // Asignar el precio de venta del producto al detalle de la venta
                venta_detalle.Total = Math.Round(pro.cantidadSelecccionItem * pro.Precio_Venta, 2); // Calcular el total del detalle de la venta
                venta_detalle.Tipo = pro.Tipo; // Asignar el tipo de producto al detalle de la venta
                context1.Add(venta_detalle); // Agregar el detalle de la venta al contexto
                context1.SaveChanges(); // Guardar los cambios en la base de datos

                if (venta_detalle.Tipo != "Servicio")
                {
                    Util.Registrar_Movimiento_Kardex(pro.Secuencial, pro.Cantidad, pro.Descripcion, pro.cantidadSelecccionItem, pro.Precio_Costo, pro.Precio_Venta, "Salida");

                    SQLitePCL.Batteries.Init();

                    using var context2 = new Monitux_DB_Context();
                    context2.Database.EnsureCreated(); // Crea la base de datos si no existe


                    var producto = context2.Productos.FirstOrDefault(p => p.Secuencial == pro.Secuencial);
                    if (producto != null)
                    {



                        producto.Cantidad = pro.Cantidad - pro.cantidadSelecccionItem;
                        context2.SaveChanges();

                    }


                }

            }


            if (comboBox3.SelectedItem == "Credito")
            {


                SQLitePCL.Batteries.Init();

                using var context3 = new Monitux_DB_Context();
                context3.Database.EnsureCreated(); // Crea la base de datos si no existe


                Cuentas_Cobrar cta_cobrar = new Cuentas_Cobrar();

                cta_cobrar.Secuencial_Factura = secuencial;
                cta_cobrar.Secuencial_Cliente = venta.Secuencial_Cliente; // Asignar el secuencial del cliente al detalle de la venta
                cta_cobrar.Secuencial_Usuario = venta.Secuencial_Usuario; // Asignar el secuencial del usuario al detalle de la venta
                cta_cobrar.Fecha = venta.Fecha; // Asignar la fecha de la venta al detalle de la venta
                cta_cobrar.Total = Math.Round(total, 2); // Asignar el total de la venta
                cta_cobrar.Saldo = Math.Round(total, 2); // Asignar el saldo de la cuenta por cobrar
                cta_cobrar.Fecha_Vencimiento = dateTimePicker1.Value.ToString("dd/MM/yyyy"); // Asignar la fecha de vencimiento de la cuenta por cobrar
                cta_cobrar.Pagado = 0.00; // Asignar el pagado de la cuenta por cobrar
                cta_cobrar.Otros_Cargos = venta.Otros_Cargos;
                cta_cobrar.Descuento = venta.Descuento;
                cta_cobrar.Impuesto = venta.Impuesto;
                cta_cobrar.Gran_Total = venta.Gran_Total;
                context3.Add(cta_cobrar);
                context3.SaveChanges();


                Util.Registrar_Actividad(Secuencial_Usuario, "Ha registrado una venta al credito segun factura: " + secuencial + "\nPor valor de: " + Math.Round(total, 2));


            }


            if (comboBox3.SelectedItem != "Credito")
            {
                ingreso.Secuencial_Factura = secuencial; // Asignar el secuencial de la factura al ingreso
                ingreso.Secuencial_Usuario = venta.Secuencial_Usuario; // Asignar el secuencial del usuario al ingreso
                ingreso.Fecha = venta.Fecha; // Asignar la fecha de la venta al ingreso 
                ingreso.Total = Math.Round(total, 2); // Asignar el total de la venta al ingreso
                ingreso.Tipo_Ingreso = comboBox1.SelectedItem.ToString(); // Asignar la forma de pago al ingreso
                ingreso.Descripcion = "Venta segun Factura: "+secuencial; // Asignar el tipo de venta al ingreso

                SQLitePCL.Batteries.Init();

                using var context4 = new Monitux_DB_Context();
                context4.Database.EnsureCreated(); // Crea la base de datos si no existe

                context4.Add(ingreso); // Agregar el ingreso al contexto
                context4.SaveChanges(); // Guardar los cambios en la base de datos

            }


            if (checkBox1.Checked == true)
            {

                string dinero_recibido = null;
                double cambio = 0.00; // Inicializar el cambio a 0.00

                //Codigo Prueba


                if (V_Menu_Principal.IPB.Show("Escriba la cantidad en números del dinero recibido por esta venta.", "Cálculo del Cambio", out dinero_recibido) == DialogResult.OK)
                {
                    dinero_recibido = dinero_recibido?.Trim();

                    if (Double.TryParse(dinero_recibido, NumberStyles.Any, CultureInfo.InvariantCulture, out double numero))
                    {
                        cambio = numero - total;

                        string mensaje = cambio >= 0
                            ? $"El Cambio a favor del Cliente es: {cambio}\n\n{Util.Convertir_Numeros_Palabras(cambio.ToString())} {V_Menu_Principal.moneda}"
                            : $"Falta Dinero: {Math.Abs(cambio)}\n\n{Util.Convertir_Numeros_Palabras(Math.Abs(cambio).ToString())} {V_Menu_Principal.moneda}";

                        V_Menu_Principal.MSG.ShowMSG(mensaje, "Ventas");
                    }
                    else
                    {
                        V_Menu_Principal.MSG.ShowMSG("Error: Solo se permiten números.", "Ventas");
                    }
                }


            }


            V_Menu_Principal.MSG.ShowMSG("Venta registrada correctamente.", "Éxito");
            // Limpiar los campos y controles después de registrar la venta
            Util.Registrar_Actividad(Secuencial_Usuario, "Ha registrado una venta segun factura: " + secuencial + "\nPor valor de: " + Math.Round(total, 2));
            Limpiar_Factura(); // Llama al método para limpiar la factura después de registrar la venta
            */
        }

        private void button1_Click_1(object sender, EventArgs e)
        {









            Actualizar_Numeros(); // Actualizar los números en los labels correspondientes



            if (Lista_de_Items.Count == 0)
            {
                V_Menu_Principal.MSG.ShowMSG("No hay items seleccionados para registrar la cotizacion.", "Error");
                return; // Si no hay items seleccionados, no se puede registrar la venta
            }

            if (comboCliente.SelectedIndex == -1)
            {
                V_Menu_Principal.MSG.ShowMSG("Debe seleccionar un cliente para registrar la cotizacion.", "Error");
                return; // Si no se ha seleccionado un cliente, no se puede registrar la venta
            }



            if (total <= 0)
            {
                V_Menu_Principal.MSG.ShowMSG("El total de la cotizacion debe ser mayor a cero.", "Error");
                return; // Si el total es menor o igual a cero, no se puede registrar la venta
            }




            Cotizacion cotizacion = new Cotizacion();


            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe



            int secuencial = context.Cotizaciones.Any() ? context.Cotizaciones.Max(p => p.Secuencial) + 1 : 1;
            cotizacion.Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa;
            cotizacion.Secuencial = secuencial;
            cotizacion.Secuencial_Cliente = comboCliente.SelectedIndex != -1 ? int.Parse(comboCliente.SelectedItem.ToString().Split('-')[0].Trim()) : 0; // Obtener el secuencial del cliente seleccionado

            cotizacion.Secuencial_Usuario = Secuencial_Usuario; // Asignar el secuencial del usuario que está realizando la venta
            cotizacion.Fecha = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"); // Asignar la fecha y hora actual de la venta

            cotizacion.Total = subTotal; // Asignar el total de la venta

            cotizacion.Gran_Total = total; // Asignar el gran total de la venta
            cotizacion.Descuento = descuento; // Asignar el descuento de la cotizacion
            cotizacion.Impuesto = impuesto; // Asignar el impuesto de la cotizacion
            cotizacion.Otros_Cargos = otrosCargos; // Asignar los otros cargos de la cotizacion
            context.Add(cotizacion);
            context.SaveChanges(); // Guardar los cambios en la base de datos


            foreach (var pro in Lista_de_Items.Values)
            {

                SQLitePCL.Batteries.Init();

                using var context1 = new Monitux_DB_Context();
                context1.Database.EnsureCreated(); // Crea la base de datos si no existe

                Cotizacion_Detalle cotizacion_detalle = new Cotizacion_Detalle();

                cotizacion_detalle.Secuencial_Empresa=V_Menu_Principal.Secuencial_Empresa;
                cotizacion_detalle.Secuencial_Cotizacion = cotizacion.Secuencial;
                cotizacion_detalle.Secuencial_Cliente = cotizacion.Secuencial_Cliente;
                cotizacion_detalle.Secuencial_Usuario = cotizacion.Secuencial_Usuario;

                cotizacion_detalle.Fecha = cotizacion.Fecha;

                cotizacion_detalle.Secuencial_Producto = pro.Secuencial;
                cotizacion_detalle.Codigo = pro.Codigo;
                cotizacion_detalle.Descripcion = pro.Descripcion;
                cotizacion_detalle.Cantidad = pro.cantidadSelecccionItem;
                cotizacion_detalle.Precio = Math.Round(pro.Precio_Venta, 2);
                cotizacion_detalle.Total = Math.Round(pro.cantidadSelecccionItem * pro.Precio_Venta, 2);
                cotizacion_detalle.Tipo = pro.Tipo; // Asignar el tipo de producto al detalle de la cotizacion
                context1.Add(cotizacion_detalle);
                context1.SaveChanges(); // Guardar los cambios en la base de datos



            }




            V_Menu_Principal.MSG.ShowMSG("Cotizacion registrada correctamente.", "Éxito");
            // Limpiar los campos y controles después de registrar la venta
            Util.Registrar_Actividad(Secuencial_Usuario, "Ha registrado una cotizacion segun Numero: " + secuencial + "\nPor valor de: " + Math.Round(total, 2), V_Menu_Principal.Secuencial_Empresa);
            Limpiar_Factura(); // Llama al método para limpiar la factura después de registrar la venta



        }

        private void V_Factura_Venta_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() != "Efectivo")
            {
                checkBox1.Checked = false; // Desmarcar el checkbox si la forma de pago no es "Efectivo"
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() != "Efectivo" && comboBox1.SelectedItem.ToString() != null)
            {
                checkBox1.Checked = false; // Desmarcar el checkbox si la forma de pago no es "Efectivo"
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}