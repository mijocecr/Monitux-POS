﻿using Microsoft.EntityFrameworkCore;
using Monitux_POS.Clases;
using Monitux_POS.Controles;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuestPDF.Helpers.Colors;

namespace Monitux_POS.Ventanas
{
    public partial class V_Editar_Factura_Compra : Form
    {



        public static int Secuencial_Usuario { get; set; } = V_Menu_Principal.Secuencial_Usuario;

        double subTotal = 0.00; // Variable para almacenar el subtotal
        double impuesto = 0.00; // Variable para almacenar el impuesto
        double total = 0.00; // Variable para almacenar el total
        double otrosCargos = 0.00; // Variable para almacenar otros cargos
        double descuento = 0.00; // Variable para almacenar el descuento aplicado
                                 //  public static string moneda = "USD"; // Variable para almacenar la moneda utilizada en la factura


        public static Dictionary<string, Miniatura_Producto> Lista_de_Items = new Dictionary<string, Miniatura_Producto>();

        public static Dictionary<string, Miniatura_Producto> Lista_de_Items_Eliminar = new Dictionary<string, Miniatura_Producto>();




        public int Secuencial_Proveedor { get; set; }
        public int Secuencial_Empresa { get; set; }
        public int Secuencial_Compra { get; set; }




        public V_Editar_Factura_Compra()
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




        public void Configurar_DataGridView()
        {

            //dataGridView1.Enabled = false;


            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            // Configurar las columnas del DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Codigo", "Código");
            dataGridView1.Columns.Add("Descripcion", "Descripción");
            dataGridView1.Columns.Add("Cantidad", "Cantidad");
            dataGridView1.Columns.Add("Precio_Costo", "Precio");

            dataGridView1.Columns.Add("Total", "Total");
            dataGridView1.Columns.Add("Secuencial_Producto", "SP");
            dataGridView1.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }


        private void Actualizar_Numeros()
        {




            total = subTotal + impuesto + otrosCargos - descuento; // Calcular el total
            lbl_Total.Text = total.ToString("0.00");
            lbl_sub_Total.Text = subTotal.ToString("0.00");
            lbl_Impuesto.Text = impuesto.ToString("0.00");
            lbl_Descuento.Text = descuento.ToString("0.00");


        }



        //Esta Funcion es reciclada, no se puede cambiar la logica - OJO
        ///////////////////////////////////////////////////////



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
                miniatura_Producto1.Secuencial_Empresa = item.Secuencial_Empresa;
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
                miniatura_Producto1.Origen = "Editar_Factura_Compra"; // Asignar el origen del producto a "Factura_Venta"
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




        ///////////////////////////////////////////////////////






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
                miniatura_Producto1.Secuencial_Empresa = item.Secuencial_Empresa;
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
                miniatura_Producto1.Origen = "Editar_Factura_Compra"; // Asignar el origen del producto a "Factura_Venta"



                miniatura_Producto1.Item_Imagen.Click += async (s, ev) =>
                {

                    //Prueba

                    button8.Enabled = false;

                    //Prueba


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




        //Esta Funcion hace la magia - Cuidado
        ///////////////////////////////////////////////////////




        public async Task Importar_Factura(Dictionary<string, double> Lista, string proveedor)
        {

            Cargar_Otros_Datos();

            comboProveedor.SelectedItem = proveedor; // Seleccionar el cliente en el comboBox

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe



            foreach (var item_C in Lista)
            {
                ////////////////////////////////////////////////////////



                var productos = context.Productos
                        .Where(c => EF.Property<string>(c, "Codigo").Equals(item_C.Key) && c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
                        .ToList();

                foreach (var item in productos)
                {
                    Miniatura_Producto miniatura_Producto = new Miniatura_Producto();



                    miniatura_Producto.Cantidad = item.Cantidad;
                    miniatura_Producto.Imagen = item.Imagen;
                    miniatura_Producto.Secuencial_Empresa = item.Secuencial_Empresa;
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
                    miniatura_Producto.Origen = "Editar_Factura_Compra"; // Asignar el origen del producto a "Factura_Venta"
                    Selector_Cantidad selector_Cantidad = new Selector_Cantidad();
                    selector_Cantidad.SetCodigo(miniatura_Producto.Codigo);
                    selector_Cantidad.numericUpDown1.Value = Convert.ToDecimal(miniatura_Producto.cantidadSelecccionItem);
                    // Lista_de_Items_Historica.Add(miniatura_Producto.Codigo, miniatura_Producto);
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

            label5.Text = Lista_de_Items.Count.ToString();




            button2.PerformClick();

        }


        private void Cargar_Otros_Datos()
        {


            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Solo si usas SQLite

            var datos = context.Compras
                .Where(v => v.Secuencial == Secuencial_Compra &&
                            v.Secuencial_Proveedor == Secuencial_Proveedor &&
                            v.Secuencial_Empresa == Secuencial_Empresa)
                .Select(v => new
                {
                    Otros_Cargos = Math.Round((double)v.Otros_Cargos, 2),
                    Impuesto = Math.Round((double)v.Impuesto, 2),
                    Descuento = Math.Round((double)v.Descuento, 2),
                    Tipo = v.Tipo
                })
                .FirstOrDefault();

            if (datos != null)
            {
                otrosCargos = datos.Otros_Cargos;
                impuesto = datos.Impuesto;
                descuento = datos.Descuento;
                comboBox3.SelectedItem = datos.Tipo;


            }

            lbl_Descuento.Text = descuento.ToString();
            lbl_Impuesto.Text = impuesto.ToString();
            lbl_OtrosCargos.Text = otrosCargos.ToString();




        }







        private void V_Editar_Factura_Compra_Load(object sender, EventArgs e)
        {
            this.Text = "Factura No: " + Secuencial_Compra;

            llenar_Combo_Proveedor();
            lbl_Descuento.Text = $"{lbl_Descuento:0.00}";
            lbl_Impuesto.Text = $"{lbl_Impuesto:0.00}";
            lbl_OtrosCargos.Text = $"{lbl_OtrosCargos:0.00}";
            lbl_sub_Total.Text = $"{lbl_sub_Total:0.00}";
            lbl_Total.Text = $"{lbl_Total:0.00}";
            Cargar_Items();

            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            Configurar_DataGridView();

            Importar_Factura(V_Compras_Ventas.Lista, V_Compras_Ventas.cliente_seleccionado);
            Cargar_Items(); // Recargar los items en el FlowLayoutPanel
            Actualizar_Numeros(); // Actualizar los números al cargar la ventana

            comboProveedor.SelectedItem = V_Compras_Ventas.proveedor_seleccionado;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filtrar(comboBox2.SelectedItem.ToString(), textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            V_Producto producto = new V_Producto();
            producto.Secuencial_Usuario = Secuencial_Usuario;

            producto.ShowDialog();
            Cargar_Items();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            dataGridView1.Rows.Clear();
            subTotal = 0.0;
            lbl_sub_Total.Text = "0.00";
            lbl_Total.Text = "0.00";
            button8.Enabled = true;


            // ✅ Actualiza cantidades seleccionadas desde el panel
            foreach (Selector_Cantidad selector in flowLayoutPanel2.Controls)
            {

                //Prueba
                //////////////////////////////////
                if (Lista_de_Items_Eliminar.ContainsKey(selector.GetCodigo()))
                {
                    Lista_de_Items_Eliminar.Remove(selector.GetCodigo());


                }
                /////////////////////////////////
                //Prueba


                if (Lista_de_Items.TryGetValue(selector.GetCodigo(), out Miniatura_Producto item))
                {
                    item.cantidadSelecccionItem = Convert.ToDouble(selector.numericUpDown1.Value);
                }
            }

            // ✅ Procesa cada ítem
            foreach (var clave in Lista_de_Items.Keys)
            {



                if (!Lista_de_Items.TryGetValue(clave, out Miniatura_Producto item))
                    continue;

                if (item.cantidadSelecccionItem <= 0)
                {
                    V_Menu_Principal.MSG.ShowMSG($"Revise la cantidad que desea agregar.\n -- {item.Codigo} --\n{item.Descripcion}", "Error");
                    return;
                }

                double importe = item.Precio_Costo * item.cantidadSelecccionItem;

                dataGridView1.Rows.Add(
                    item.Codigo,
                    item.Descripcion,
                    item.cantidadSelecccionItem,
                    item.Precio_Costo,
                    importe,
                    item.Secuencial
                );

                subTotal += importe;
            }

            // ✅ Actualiza subtotal y total visualmente
            lbl_sub_Total.Text = subTotal.ToString();

            // ✅ Actualiza cálculos financieros
            Actualizar_Numeros();

            // ✅ Actualiza contador
            label5.Text = Lista_de_Items.Count.ToString();
            


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
                        Precio = (double)Convert.ToDecimal(row.Cells["Precio_Costo"].Value)
                    });
                }
            }

            return lista;
        }





        private void button8_Click(object sender, EventArgs e)
        {
            /* ////////////////////////////////



             using var context = new Monitux_DB_Context();
             SQLitePCL.Batteries.Init();
             context.Database.EnsureCreated();

             var controles = flowLayoutPanel2.Controls.OfType<Selector_Cantidad>().ToList();

             foreach (var control in controles)
             {
                 string codigo = control.label1.Text;

                 if (Lista_de_Items.ContainsKey(codigo))
                 {
                     Lista_de_Items[codigo].cantidadSelecccionItem = Convert.ToDouble(control.numericUpDown1.Value);
                     control.numericUpDown1.Value = Convert.ToDecimal(Lista_de_Items[codigo].cantidadSelecccionItem);
                 }

                 if (!control.checkBox1.Checked || !Lista_de_Items.ContainsKey(codigo))
                 {
                     if (!Lista_de_Items.ContainsKey(codigo))
                         control.BackColor = Color.Red;

                     continue;
                 }

                 var original = Lista_de_Items[codigo];
                 var copia = Util.Clonar_Control(original);

                 copia.Precio_Venta = original.Precio_Venta;
                 copia.Precio_Costo = original.Precio_Costo;
                 copia.Cantidad = original.Cantidad;
                 copia.cantidadSelecccionItem = original.cantidadSelecccionItem;
                 copia.unidadesAgregar = copia.cantidadSelecccionItem;

                 Lista_de_Items_Eliminar[codigo] = copia;
                 Lista_de_Items.Remove(codigo);
                 flowLayoutPanel2.Controls.Remove(control);
                 button1.Enabled = true;

                 V_Menu_Principal.MSG.ShowMSG($"El item {codigo} se removió de la factura", "Compras");

                 ActualizarCuentas_Pagar(context, copia.Precio_Costo);

                 ActualizarCompra(context, copia.Precio_Costo);

                 bool existeDetalle = context.Compras_Detalles.Any(cd => cd.Codigo == copia.Codigo && cd.Secuencial_Factura == Secuencial_Compra);

                 if (existeDetalle)
                 {
                     if (copia.Tipo != "Servicio")
                     {
                         RegistrarEntradaKardex(context, copia);
                         Actualizar_Inventario(context, copia);
                     }

                     Util.Registrar_Actividad(Secuencial_Usuario,
                         $"Eliminó el Item: {codigo} de la Factura No. {Secuencial_Compra}\n" +
                         $" Registrado a: {copia.Precio_Costo} {V_Menu_Principal.moneda}, cantidad: {copia.cantidadSelecccionItem}\n " +
                         $"Total: {copia.cantidadSelecccionItem * copia.Precio_Costo}",
                         Secuencial_Empresa);
                 }
                 else
                 {
                     V_Menu_Principal.MSG.ShowMSG($"El item {codigo} no se encontró en detalles de compras para esta factura. No se actualizó Kardex ni se registró actividad.", "Info");
                 }

                 EliminarDetalleCompra(context, copia.Codigo);
             }

             Cargar_Items();
             label5.Text = Lista_de_Items.Count.ToString();
             button2_Click(sender, e);



             ///////////////////////////////


             */




            using var context = new Monitux_DB_Context();
            SQLitePCL.Batteries.Init();
            context.Database.EnsureCreated();

            var controles = flowLayoutPanel2.Controls.OfType<Selector_Cantidad>().ToList();




            SQLitePCL.Batteries.Init();
            context.Database.EnsureCreated();



            foreach (var control in controles)
            {
                string codigo = control.label1.Text;

                if (Lista_de_Items.ContainsKey(codigo))
                {
                    Lista_de_Items[codigo].cantidadSelecccionItem = Convert.ToDouble(control.numericUpDown1.Value);
                    control.numericUpDown1.Value = Convert.ToDecimal(Lista_de_Items[codigo].cantidadSelecccionItem);
                }

                if (!control.checkBox1.Checked || !Lista_de_Items.ContainsKey(codigo))
                {
                    if (!Lista_de_Items.ContainsKey(codigo))
                        control.BackColor = Color.Red;

                    continue;
                }

                var original = Lista_de_Items[codigo];
                var copia = Util.Clonar_Control(original);

                // Copia correctamente las propiedades necesarias
                copia.Precio_Venta = original.Precio_Venta;
                copia.Precio_Costo = original.Precio_Costo;
                copia.Cantidad = original.Cantidad;
                copia.cantidadSelecccionItem = original.cantidadSelecccionItem;
                copia.unidadesAgregar = copia.cantidadSelecccionItem;

                Lista_de_Items_Eliminar[codigo] = copia;
                Lista_de_Items.Remove(codigo);
                flowLayoutPanel2.Controls.Remove(control);
                button1.Enabled = true;

                V_Menu_Principal.MSG.ShowMSG($"El item {codigo} se removió de la factura,  no olvide guardar los cambios efectuados al finalizar.", "Compras");

                bool existeDetalle = context.Compras_Detalles.Any(cd =>
                    cd.Codigo == copia.Codigo &&
                    cd.Secuencial_Factura == Secuencial_Compra);

                if (existeDetalle)
                {
                    ActualizarCuentas_Pagar(context, copia.Precio_Costo);
                    ActualizarCompra(context, copia.Precio_Costo);

                    if (copia.Tipo != "Servicio")
                    {
                        RegistrarEntradaKardex(context, copia);
                        Actualizar_Inventario(context, copia);
                    }

                    Util.Registrar_Actividad(Secuencial_Usuario,
                        $"Eliminó el Item: {codigo} de la Factura No. {Secuencial_Compra}\n" +
                        $" Registrado a: {copia.Precio_Costo} {V_Menu_Principal.moneda}, cantidad: {copia.cantidadSelecccionItem}\n " +
                        $"Total: {copia.cantidadSelecccionItem * copia.Precio_Costo}",
                        Secuencial_Empresa);
                }
                else
                {
                    V_Menu_Principal.MSG.ShowMSG($"El item {codigo} no se encontró en detalles de compras para esta factura. No se actualizó Kardex ni se registró actividad.", "Info");
                }

                EliminarDetalleCompra(context, codigo);
            }

            Cargar_Items();
            label5.Text = Lista_de_Items.Count.ToString();
            button2_Click(sender, e);





            Cargar_Items();
            label5.Text = Lista_de_Items.Count.ToString();
            button2_Click(sender, e);







        }


        private void Actualizar_Inventario(Monitux_DB_Context context, Miniatura_Producto copia)
        {
            if (copia.unidadesAgregar <= 0) return;

            var producto = context.Productos.FirstOrDefault(p =>
                p.Secuencial == copia.Secuencial &&
                p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

            if (producto != null)
            {
                producto.Cantidad += copia.unidadesAgregar;
                context.Entry(producto).Property(p => p.Cantidad).IsModified = true;
                context.SaveChanges();

                Util.Registrar_Actividad(Secuencial_Usuario,
                    $"Agregó {copia.unidadesAgregar} unidades al producto: {copia.Codigo}",
                    V_Menu_Principal.Secuencial_Empresa);
            }
        }


        private void ActualizarCompra(Monitux_DB_Context context, double monto)
        {
            var compra = context.Compras.FirstOrDefault(v =>
                v.Secuencial == Secuencial_Compra &&
                v.Secuencial_Proveedor == Secuencial_Proveedor &&
                v.Secuencial_Empresa == Secuencial_Empresa);

            if (compra == null || monto <= 0) return;

            compra.Gran_Total = Math.Round((double)(compra.Gran_Total - monto), 2);
            compra.Total = Math.Round((double)(compra.Total - monto), 2);

            context.SaveChanges();
        }

        private void ActualizarCuentas_Pagar(Monitux_DB_Context context, double monto)
        {
            try
            {
                var cuenta = context.Cuentas_Pagar.FirstOrDefault(c =>
                    c.Secuencial_Factura == Secuencial_Compra &&
                    c.Secuencial_Proveedor == Secuencial_Proveedor &&
                    c.Secuencial_Empresa == Secuencial_Empresa);

                if (cuenta == null)
                {
                    V_Menu_Principal.MSG.ShowMSG($"❌ No se encontró cuenta por pagar (Factura: {Secuencial_Compra}, Proveedor: {Secuencial_Proveedor})", "Debug");
                    return;
                }

                if (monto <= 0)
                {
                    V_Menu_Principal.MSG.ShowMSG($"⚠️ Monto inválido: {monto}", "Debug");
                    return;
                }

                double saldoAntes = (double)cuenta.Saldo;
                double granTotalAntes = (double)cuenta.Gran_Total;
                double totalAntes = (double)cuenta.Total;

                cuenta.Saldo -= monto;
                cuenta.Gran_Total -= monto;
                cuenta.Total -= monto;

                context.SaveChanges();

                V_Menu_Principal.MSG.ShowMSG(
                    $"✅ Cuenta actualizada: -{monto}\n" +
                    $"Saldo: {saldoAntes} → {cuenta.Saldo}\n" +
                    $"Gran_Total: {granTotalAntes} → {cuenta.Gran_Total}\n" +
                    $"Total: {totalAntes} → {cuenta.Total}",
                    "Cuentas Pagar"
                );
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"🚨 Error al actualizar cuentas pagar:\n{ex.Message}", "Error");
            }
        }




        private void EliminarDetalleCompra(Monitux_DB_Context context, string codigo)
        {
            var detalles = context.Compras_Detalles.Where(d =>
                d.Secuencial_Factura == Secuencial_Compra &&
                d.Codigo == codigo &&
                d.Secuencial_Empresa == Secuencial_Empresa).ToList();

            if (detalles.Any())
            {
                context.Compras_Detalles.RemoveRange(detalles);
                context.SaveChanges();

                V_Menu_Principal.MSG.ShowMSG($"🧹 {detalles.Count} detalle(s) eliminados del documento de compra.", "Limpieza");
            }
        }

        private void RegistrarEntradaKardex(Monitux_DB_Context context, Miniatura_Producto copia)
        {
            if (copia.Precio_Costo <= 0 || copia.Tipo == "Servicio") return;

            Util.Registrar_Movimiento_Kardex(copia.Secuencial, copia.Cantidad, copia.Descripcion,
                copia.cantidadSelecccionItem, copia.Precio_Costo, 0, "Entrada", Secuencial_Empresa);
        }










        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            otrosCargos = 0;
            txt_OtrosCargos.Visible = true;
            txt_OtrosCargos.Focus();
            txt_OtrosCargos.Text = "";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            impuesto = 0;
            txt_Impuesto.Visible = true;
            txt_Impuesto.Text = "";
            txt_Impuesto.Focus();
            label12.Visible = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            descuento = 0;
            txt_Descuento.Visible = true;
            txt_Descuento.Focus();
            txt_Descuento.Text = "";
            label13.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {





            {
                Actualizar_Numeros();

                SQLitePCL.Batteries.Init();
                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var compra = context.Compras.FirstOrDefault(c => c.Secuencial == this.Secuencial_Compra &&
                                                                 c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

                // ✅ Actualizar compra existente
                compra.Secuencial_Proveedor = Secuencial_Proveedor;


                compra.Secuencial_Usuario = Secuencial_Usuario;
                compra.Tipo = comboBox3.SelectedItem?.ToString();
                compra.Forma_Pago = comboBox1.SelectedItem?.ToString();
                compra.Fecha = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                compra.Total = Math.Round(subTotal, 2);
                compra.Gran_Total = Math.Round(total, 2);
                compra.Impuesto = impuesto;
                compra.Otros_Cargos = otrosCargos;
                compra.Descuento = descuento;

                using var context1 = new Monitux_DB_Context();
                context1.Database.EnsureCreated();

                foreach (var pro in Lista_de_Items.Values)
                {
                    var productoBD = context.Productos.FirstOrDefault(p => p.Secuencial == pro.Secuencial);
                    if (productoBD != null)
                    {
                        pro.Precio_Costo = productoBD.Precio_Costo;
                        pro.Precio_Venta = productoBD.Precio_Venta;
                        pro.Cantidad = productoBD.Cantidad;
                    }
                }

                foreach (var pro in Lista_de_Items.Values)
                {
                    var detalleExistente = context.Compras_Detalles.FirstOrDefault(d =>
                        d.Secuencial_Factura == compra.Secuencial &&
                        d.Secuencial_Producto == pro.Secuencial &&
                        d.Secuencial_Empresa == compra.Secuencial_Empresa);

                    int nuevaCantidad = (int)pro.cantidadSelecccionItem;

                    if (detalleExistente != null)
                    {
                        if (pro.Tipo != "Servicio")
                        {
                            int cantidadAnterior = (int)detalleExistente.Cantidad;
                            int diferencia = nuevaCantidad - cantidadAnterior;

                            if (diferencia != 0)
                            {
                                string tipoMovimiento = diferencia > 0 ? "Entrada" : "Salida";
                                int cantidadMovimiento = Math.Abs(diferencia);

                                Util.Registrar_Movimiento_Kardex(pro.Secuencial, pro.Cantidad, pro.Descripcion,
                                    cantidadMovimiento, pro.Precio_Costo, pro.Precio_Venta, tipoMovimiento, Secuencial_Empresa);

                                string accion = diferencia > 0 ? "Agregó" : "Quitó";
                                Util.Registrar_Actividad(Secuencial_Usuario,
                                    $"{accion} {cantidadMovimiento} unidades de {pro.Codigo} en modificación de factura No: {compra.Secuencial}",
                                    Secuencial_Empresa);

                                var producto = context.Productos.FirstOrDefault(p => p.Secuencial == pro.Secuencial);
                                if (producto != null)
                                    producto.Cantidad += diferencia;
                            }
                        }

                        // 🛠️ Actualizar detalle existente
                        detalleExistente.Cantidad = nuevaCantidad;
                        detalleExistente.Precio = Math.Round(pro.Precio_Costo, 2);
                        detalleExistente.Total = Math.Round(nuevaCantidad * pro.Precio_Costo, 2);
                        detalleExistente.Descripcion = pro.Descripcion;
                        detalleExistente.Tipo = pro.Tipo;
                        detalleExistente.Fecha = compra.Fecha;
                        detalleExistente.Secuencial_Usuario = compra.Secuencial_Usuario;
                        detalleExistente.Secuencial_Proveedor = compra.Secuencial_Proveedor;
                    }
                    else
                    {
                        var detalleNuevo = new Compra_Detalle
                        {
                            Secuencial_Empresa = compra.Secuencial_Empresa,
                            Secuencial_Factura = compra.Secuencial,
                            Secuencial_Proveedor = compra.Secuencial_Proveedor,
                            Secuencial_Usuario = compra.Secuencial_Usuario,
                            Fecha = compra.Fecha,
                            Secuencial_Producto = pro.Secuencial,
                            Codigo = pro.Codigo,
                            Descripcion = pro.Descripcion,
                            Cantidad = nuevaCantidad,
                            Precio = Math.Round(pro.Precio_Costo, 2),
                            Total = Math.Round(nuevaCantidad * pro.Precio_Costo, 2),
                            Tipo = pro.Tipo
                        };

                        context.Compras_Detalles.Add(detalleNuevo);

                        Util.Registrar_Actividad(Secuencial_Usuario,
                            $"Agregó la cantidad de: {nuevaCantidad} de: {pro.Codigo} a Factura No: {compra.Secuencial}",
                            Secuencial_Empresa);

                        if (pro.Tipo != "Servicio")
                        {
                            Util.Registrar_Movimiento_Kardex(pro.Secuencial, pro.Cantidad, pro.Descripcion, nuevaCantidad, pro.Precio_Costo, 0, "Entrada", Secuencial_Empresa);

                            var producto = context.Productos.FirstOrDefault(p => p.Secuencial == pro.Secuencial);
                            if (producto != null)
                                producto.Cantidad += nuevaCantidad;
                        }
                    }

                    var resultado1 = context1.Cuentas_Pagar.FirstOrDefault(c =>
                        c.Secuencial_Factura == compra.Secuencial &&
                        c.Secuencial_Proveedor == compra.Secuencial_Proveedor &&
                        c.Secuencial_Empresa == compra.Secuencial_Empresa);

                    if (resultado1 != null)
                    {
                        resultado1.Gran_Total = Math.Round((double)compra.Gran_Total, 2);
                        resultado1.Total = Math.Round((double)compra.Total, 2);
                        resultado1.Saldo = Math.Round((double)(resultado1.Gran_Total - resultado1.Pagado), 2);
                    }
                }


                /////////

                // ... actualizaciones de productos, kardex y detalles de compra ...

                // ✅ Actualizar el ingreso vinculado con esta compra
                var egresoExistente = context.Egresos.FirstOrDefault(i =>
                    i.Secuencial_Factura == compra.Secuencial &&
                    i.Secuencial_Empresa == compra.Secuencial_Empresa);

                if (egresoExistente != null)
                {
                    egresoExistente.Total = (double)compra.Gran_Total;
                    egresoExistente.Descripcion = $"Actualización de egreso por compra No. {compra.Secuencial}";
                    egresoExistente.Fecha = DateTime.Now.ToString();
                    egresoExistente.Tipo_Egreso = compra.Tipo;
                    egresoExistente.Secuencial_Usuario = compra.Secuencial_Usuario;
                }

                // ✅ Guardar todos los cambios, incluyendo el ingreso
                context.SaveChanges();
                context1.SaveChanges();



                /////////



                var factura = new FacturaCompletaPDF_Compra
                {
                    Secuencial = compra.Secuencial,
                    Proveedor = comboProveedor.SelectedItem.ToString()
                        .Substring(comboProveedor.SelectedItem.ToString().IndexOf("- ") + 2)
                        .Trim(),
                    TipoCompra = compra.Tipo,
                    MetodoPago = compra.Forma_Pago,
                    Fecha = compra.Fecha,
                    Items = ObtenerItemsDesdeGrid(dataGridView1),
                    ISV = (double)compra.Impuesto,
                    OtrosCargos = (double)compra.Otros_Cargos,
                    Descuento = (double)compra.Descuento
                };

                string rutaGuardado = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\FAC\\" + V_Menu_Principal.Secuencial_Empresa);
                factura.GeneratePdf($"{rutaGuardado}-{compra.Secuencial}-{factura.Proveedor}.pdf");

                Lista_de_Items.Clear();
                Lista_de_Items_Eliminar.Clear();

                V_Menu_Principal.MSG.ShowMSG($"Factura No. {compra.Secuencial} actualizada correctamente.", "Compras");
                this.Dispose();






            }
        }

        private void V_Editar_Factura_Compra_FormClosing(object sender, FormClosingEventArgs e)
        {
            Lista_de_Items.Clear();
            Lista_de_Items_Eliminar.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                var secuencialCompra = Secuencial_Compra;

                var confirmResult = V_Menu_Principal.MSG.ShowMSG(
                    $"¿Está seguro de eliminar la Compra No. {secuencialCompra}?",
                    "Confirmar Eliminación");

                if (confirmResult != DialogResult.Yes)
                    return;

                using var context = new Monitux_DB_Context();
                SQLitePCL.Batteries.Init();

                var compra = context.Compras.FirstOrDefault(c => c.Secuencial == secuencialCompra);
                if (compra == null)
                {
                    V_Menu_Principal.MSG.ShowMSG("Compra no encontrada.", "Error");
                    return;
                }

                var detalles = context.Compras_Detalles
                    .Where(cd => cd.Secuencial_Factura == secuencialCompra)
                    .ToList();

                foreach (var detalle in detalles)
                {
                    var producto = context.Productos.FirstOrDefault(p => p.Codigo == detalle.Codigo);

                    if (producto != null && producto.Tipo != "Servicio" && detalle.Cantidad != null)
                    {
                        double cantidadRetirada = (double)detalle.Cantidad;

                        // ✅ Registrar salida por eliminación de compra
                        Util.Registrar_Movimiento_Kardex(
                            producto.Secuencial,
                            producto.Cantidad,
                            producto.Descripcion,
                            cantidadRetirada,
                            producto.Precio_Costo,
                            producto.Precio_Venta,
                            "Salida",
                            Secuencial_Empresa
                        );

                        producto.Cantidad -= cantidadRetirada;
                    }
                }

                context.Compras_Detalles.RemoveRange(detalles);
                context.Compras.Remove(compra);

                Util.Registrar_Actividad(
                    Secuencial_Usuario,
                    $"Eliminó la Compra No. {secuencialCompra} con {detalles.Count} productos. Total de {compra.Gran_Total} {V_Menu_Principal.moneda}",
                    Secuencial_Empresa
                );

                // ✅ Eliminar egreso vinculado a la compra
                var egresoAsociado = context.Egresos.FirstOrDefault(i =>
                    i.Secuencial_Factura == secuencialCompra &&
                    i.Secuencial_Empresa == Secuencial_Empresa);

                if (egresoAsociado != null)
                {
                    context.Egresos.Remove(egresoAsociado);
                    Util.Registrar_Actividad(
                        Secuencial_Usuario,
                        $"Se eliminó el egreso relacionado a la Compra No. {secuencialCompra}",
                        Secuencial_Empresa
                    );
                }

                context.SaveChanges();

                V_Menu_Principal.MSG.ShowMSG(
                    $"Compra No. {secuencialCompra} eliminada correctamente.",
                    "Operación Exitosa");

                this.Dispose();
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Error al eliminar compra: {ex.Message}", "Error");
            }


        }
    }
}
