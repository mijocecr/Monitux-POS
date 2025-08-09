using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Monitux_POS.Clases;
using Monitux_POS.Controles;
using Newtonsoft.Json;
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
    public partial class V_Editar_Factura_Venta : Form
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




        public int Secuencial_Cliente { get; set; }
        public int Secuencial_Empresa { get; set; }
        public int Secuencial_Venta { get; set; }



        public V_Editar_Factura_Venta()
        {
            InitializeComponent();
        }


        //Esta Funcion hace la magia - Cuidado
        ///////////////////////////////////////////////////////




        public async Task Importar_Factura(Dictionary<string, double> Lista, string cliente)
        {

            Cargar_Otros_Datos();

            comboCliente.SelectedItem = cliente; // Seleccionar el cliente en el comboBox

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
                    
                    miniatura_Producto.Secuencial_Proveedor = item.Secuencial_Proveedor;
                    miniatura_Producto.Secuencial_Categoria = item.Secuencial_Categoria;
                    miniatura_Producto.Secuencial_Usuario = Secuencial_Usuario;
                    miniatura_Producto.Fecha_Caducidad = item.Fecha_Caducidad;
                    miniatura_Producto.Item_Seleccionado.Checked = true;
                    // miniatura_Producto.moneda = moneda; // Asignar la moneda a la miniatura del producto
                    miniatura_Producto.Expira = item.Expira; // Asignar si el producto expira o no
                    miniatura_Producto.cantidadSelecccionItem = Lista.TryGetValue(item_C.Key, out double cantidad) ? cantidad : 0.0; // Asignar la cantidad seleccionada desde el diccionario, o 0.0 si no se encuentra
                    miniatura_Producto.Tipo = item.Tipo;
                    miniatura_Producto.Origen = "Editar_Factura_Venta"; // Asignar el origen del producto a "Factura_Venta"
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

            var datos = context.Ventas
                .Where(v => v.Secuencial == Secuencial_Venta &&
                            v.Secuencial_Cliente == Secuencial_Cliente &&
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









        //////////////////////////////////////////////////////

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



        private void V_Editar_Factura_Venta_Load(object sender, EventArgs e)
        {
            this.Text= "Factura No: "+Secuencial_Venta;
            llenar_Combo_Cliente();
            lbl_Descuento.Text = $"{lbl_Descuento:0.00}";
            lbl_Impuesto.Text = $"{lbl_Impuesto:0.00}";
            lbl_OtrosCargos.Text = $"{lbl_OtrosCargos:0.00}";
            lbl_sub_Total.Text = $"{lbl_sub_Total:0.00}";
            lbl_Total.Text = $"{lbl_Total:0.00}";
            Cargar_Items();
            // llenar_Combo_Cliente();
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            Configurar_DataGridView();

            Importar_Factura(V_Compras_Ventas.Lista, V_Compras_Ventas.cliente_seleccionado);
            Cargar_Items(); // Recargar los items en el FlowLayoutPanel
            Actualizar_Numeros(); // Actualizar los números al cargar la ventana


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
                
                miniatura_Producto1.Secuencial_Proveedor = item.Secuencial_Proveedor;
                miniatura_Producto1.Secuencial_Categoria = item.Secuencial_Categoria;
                miniatura_Producto1.Secuencial_Usuario = Secuencial_Usuario;
                miniatura_Producto1.Fecha_Caducidad = item.Fecha_Caducidad;
                miniatura_Producto1.Expira = Convert.ToBoolean(item.Expira);
                miniatura_Producto1.moneda = V_Menu_Principal.moneda; // Asignar la moneda a la miniatura del producto
                miniatura_Producto1.Tipo = item.Tipo; // Asignar el tipo de producto (si es necesario)
                miniatura_Producto1.Origen = "Editar_Factura_Venta"; // Asignar el origen del producto a "Factura_Venta"



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
                
                miniatura_Producto1.Secuencial_Proveedor = item.Secuencial_Proveedor;
                miniatura_Producto1.Secuencial_Categoria = item.Secuencial_Categoria;
                miniatura_Producto1.Secuencial_Usuario = Secuencial_Usuario;
                miniatura_Producto1.Fecha_Caducidad = item.Fecha_Caducidad;
                miniatura_Producto1.Expira = Convert.ToBoolean(item.Expira);
                miniatura_Producto1.moneda = V_Menu_Principal.moneda; // Asignar la moneda a la miniatura del producto
                miniatura_Producto1.Tipo = item.Tipo; // Asignar el tipo de producto (si es necesario)
                miniatura_Producto1.Origen = "Editar_Factura_Venta"; // Asignar el origen del producto a "Factura_Venta"
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










        private void Actualizar_Numeros()
        {


            /*
            total = subTotal+otrosCargos+impuesto-descuento;

            lbl_Total.Text = Math.Round(total,2).ToString();
            lbl_Impuesto.Text = Math.Round(impuesto,2).ToString();
            lbl_Descuento.Text=Math.Round(descuento,2).ToString();
            lbl_OtrosCargos.Text=Math.Round(otrosCargos,2).ToString();
            */




            total = subTotal + impuesto + otrosCargos - descuento; // Calcular el total
            lbl_Total.Text = total.ToString("0.00");
            lbl_sub_Total.Text = subTotal.ToString("0.00");
            lbl_Impuesto.Text = impuesto.ToString("0.00");
            lbl_Descuento.Text = descuento.ToString("0.00");


        }











        /*
                private void ActualizarTotalesFinancieros()
                {
                    // ✅ Obtener porcentaje ingresado por el usuario desde los TextBox
                    double porcentajeDescuento = 0.0;
                    double porcentajeImpuesto = 0.0;

                    double.TryParse(txt_Descuento.Text, out porcentajeDescuento);
                    double.TryParse(txt_Impuesto.Text, out porcentajeImpuesto);

                    // ✅ Calcular valores reales
                    descuento = Math.Round((porcentajeDescuento / 100.0) * subTotal, 2);



                    // ✅ Calcular el total final
                    total = Math.Round(subTotal + impuesto + otrosCargos - descuento,2);

                    // ✅ Actualizar los controles visuales
                    lbl_Descuento.Text = descuento.ToString();
                    lbl_Impuesto.Text = impuesto.ToString();
                    lbl_Total.Text = total.ToString();
                }

                */



        private void ActualizarTotalesFinancieros()
        {
            double porcentajeDescuento = 0.0;
            double porcentajeImpuesto = 0.0;

            double.TryParse(txt_Descuento.Text, out porcentajeDescuento);
            double.TryParse(txt_Impuesto.Text, out porcentajeImpuesto);

            descuento = Math.Round((porcentajeDescuento / 100.0) * subTotal, 2);
            impuesto = Math.Round((porcentajeImpuesto / 100.0) * subTotal, 2);
            total = Math.Round(subTotal + impuesto + otrosCargos - descuento, 2);

            lbl_Descuento.Text = descuento.ToString("0.00");
            lbl_Impuesto.Text = impuesto.ToString("0.00");
            lbl_Total.Text = total.ToString("0.00");
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

                double importe = item.Precio_Venta * item.cantidadSelecccionItem;

                dataGridView1.Rows.Add(
                    item.Codigo,
                    item.Descripcion,
                    item.cantidadSelecccionItem,
                    item.Precio_Venta,
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

        private void button7_Click(object sender, EventArgs e)
        {


            try
            {
                var secuencialFactura = Secuencial_Venta;

                var confirmResult = V_Menu_Principal.MSG.ShowMSG(
                    $"¿Está seguro de eliminar la Factura No. {secuencialFactura}?",
                    "Confirmar Eliminación");

                if (confirmResult != DialogResult.Yes)
                    return;

                using var context = new Monitux_DB_Context();
                SQLitePCL.Batteries.Init();

                var factura = context.Ventas.FirstOrDefault(v => v.Secuencial == secuencialFactura);
                if (factura == null)
                {
                    V_Menu_Principal.MSG.ShowMSG("Factura no encontrada.", "Error");
                    return;
                }

                var ctac = context.Cuentas_Cobrar.FirstOrDefault(c =>
                    c.Secuencial_Factura == secuencialFactura &&
                    c.Secuencial_Cliente == Secuencial_Cliente &&
                    c.Secuencial_Empresa == Secuencial_Empresa);

                if (ctac != null)
                    context.Cuentas_Cobrar.Remove(ctac);

                var detalles = context.Ventas_Detalles
                    .Where(vd => vd.Secuencial_Factura == secuencialFactura)
                    .ToList();

                foreach (var detalle in detalles)
                {
                    var producto = context.Productos.FirstOrDefault(p => p.Codigo == detalle.Codigo);

                    if (producto != null && producto.Tipo != "Servicio" && detalle.Cantidad != null)
                    {
                        double cantidadDevuelta = (double)detalle.Cantidad;


                        // ✅ Registrar solo la cantidad devuelta, no el stock total
                        Util.Registrar_Movimiento_Kardex(
                            producto.Secuencial,
                            producto.Cantidad,
                            producto.Descripcion,
                            cantidadDevuelta,
                            producto.Precio_Costo,
                            producto.Precio_Venta,
                            "Entrada",
                            Secuencial_Empresa
                        );
                        producto.Cantidad += cantidadDevuelta;
                    }
                }

                context.Ventas_Detalles.RemoveRange(detalles);
                context.Ventas.Remove(factura);

                Util.Registrar_Actividad(
                    Secuencial_Usuario,
                    $"Eliminó la Factura No. {secuencialFactura} con {detalles.Count} productos. Registrada por un monto de {factura.Gran_Total} {V_Menu_Principal.moneda}",
                    Secuencial_Empresa
                );


                // ✅ Eliminar ingreso vinculado a la factura
                var ingresoAsociado = context.Ingresos.FirstOrDefault(i =>
                    i.Secuencial_Factura == secuencialFactura &&
                    i.Secuencial_Empresa == Secuencial_Empresa);

                if (ingresoAsociado != null)
                {
                    context.Ingresos.Remove(ingresoAsociado);
                    Util.Registrar_Actividad(
                        Secuencial_Usuario,
                        $"Se eliminó el ingreso relacionado a la Factura No. {secuencialFactura}",
                        Secuencial_Empresa
                    );
                }




                context.SaveChanges();

                V_Menu_Principal.MSG.ShowMSG(
                    $"Factura No. {secuencialFactura} eliminada correctamente.",
                    "Operación Exitosa");

                // Limpieza visual opcional
                this.Dispose();
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Error al eliminar factura: {ex.Message}", "Error");
            }


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

        private void txt_Impuesto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                impuesto = double.Parse(txt_Impuesto.Text) / 100 * subTotal;


            }
            catch
            {
                impuesto = 0.00; // Si hay un error al convertir, se establece en 0.00
            }
        }






        private void txt_Descuento_TextChanged(object sender, EventArgs e)
        {
            try
            {
                descuento = double.Parse(txt_Descuento.Text) / 100 * subTotal;


            }
            catch (Exception ex)
            {

                descuento = 0.00; // Si hay un error al convertir, se establece en 0.00
            }
        }

        private void txt_OtrosCargos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_OtrosCargos.Visible = false;
                lbl_OtrosCargos.Visible = true;
                label9.Visible = true;

                lbl_OtrosCargos.Text = otrosCargos.ToString();
                Actualizar_Numeros();

            }
        }

        private void txt_Impuesto_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txt_Impuesto.Visible = false;
                lbl_Impuesto.Visible = true;
                label10.Visible = true;
                label12.Visible = false;

                Actualizar_Numeros();
            }
        }

        private void txt_Descuento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Descuento.Visible = false;
                label13.Visible = false; // Ocultar la etiqueta de descuento al presionar Enter 
                label11.Visible = true;

                lbl_Descuento.Visible = true; // Mostrar la etiqueta de descuento al presionar Enter
                Actualizar_Numeros();
            }
        }

        private void flowLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            label5.Text = Lista_de_Items.Count.ToString();

        }

        private void button8_Click(object sender, EventArgs e)
        {


            //  this.Text = Lista_de_Items_Eliminar.Values.Count.ToString();

            using var context = new Monitux_DB_Context();
            SQLitePCL.Batteries.Init();
            context.Database.EnsureCreated();

            var controles = flowLayoutPanel2.Controls.OfType<Selector_Cantidad>().ToList();



            foreach (var control in controles)
            {
                string codigo = control.label1.Text;

                if (Lista_de_Items.ContainsKey(codigo))
                {
                    // Actualiza la cantidad seleccionada visualmente
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

                // 🔁 Copia correctamente las propiedades necesarias
                copia.Precio_Venta = original.Precio_Venta;
                copia.Precio_Costo = original.Precio_Costo;
                copia.Cantidad = original.Cantidad;
                copia.cantidadSelecccionItem = original.cantidadSelecccionItem;

                // ✅ Asignar correctamente las unidades que deben devolverse al inventario
                copia.unidadesAgregar = copia.cantidadSelecccionItem;

                Lista_de_Items_Eliminar[codigo] = copia;
                Lista_de_Items.Remove(codigo);
                flowLayoutPanel2.Controls.Remove(control);
                button1.Enabled = true;

                V_Menu_Principal.MSG.ShowMSG($"El item {codigo} se removió de la factura, no olvide guardar los cambios efectuados al finalizar.", "Ventas");

                ActualizarCuentas(context, copia.Precio_Venta);
                ActualizarVenta(context, copia.Precio_Venta);



                ////////////////////////////


                bool existeDetalle = context.Ventas_Detalles.Any(vd => vd.Codigo == copia.Codigo && vd.Secuencial_Factura == Secuencial_Venta);

                if (existeDetalle)
                {


                    if (copia.Tipo != "Servicio")
                    {

                        RegistrarSalidaKardex(context, copia);
                        Actualizar_Inventario(context, copia);

                    }



                    Util.Registrar_Actividad(Secuencial_Usuario,
                        $"Eliminó el Item: {codigo} de la Factura No. {Secuencial_Venta}\n" + $" Registrado a: {copia.Precio_Venta} {V_Menu_Principal.moneda}, cantidad: {copia.cantidadSelecccionItem}\n " +
                        $"Total: {copia.cantidadSelecccionItem * copia.Precio_Venta}",
                        Secuencial_Empresa);



                }
                else
                {
                    // Si no existe el detalle → no hace nada
                    V_Menu_Principal.MSG.ShowMSG($"El item {codigo} no se encontró en detalles de ventas para esta factura. No se actualizó Kardex ni se registro actividad.", "Info");
                }


                EliminarDetalle(context, codigo);


                ////////////////////////////







            }//Fin del foreach



            Cargar_Items();
            label5.Text = Lista_de_Items.Count.ToString();
            button2_Click(sender, e);


          




        }

        private void ActualizarVenta(Monitux_DB_Context context, double monto)
        {
            var venta = context.Ventas.FirstOrDefault(v =>
                v.Secuencial == Secuencial_Venta &&
                v.Secuencial_Cliente == Secuencial_Cliente &&
                v.Secuencial_Empresa == Secuencial_Empresa);

            if (venta == null || monto <= 0) return;

            venta.Gran_Total = Math.Round((double)(venta.Gran_Total - monto), 2);
            venta.Total = Math.Round((double)(venta.Total - monto), 2);
        }


        private void ActualizarCuentas(Monitux_DB_Context context, double monto)
        {
            var cuenta = context.Cuentas_Cobrar.FirstOrDefault(v =>
                v.Secuencial_Factura == Secuencial_Venta &&
                v.Secuencial_Cliente == Secuencial_Cliente &&
                v.Secuencial_Empresa == Secuencial_Empresa);

            if (cuenta == null || monto <= 0) return;

            cuenta.Saldo = Math.Round((double)(cuenta.Saldo - monto), 2);
            cuenta.Gran_Total = Math.Round((double)(cuenta.Gran_Total - monto), 2);
            cuenta.Total = Math.Round((double)(cuenta.Total - monto), 2);
        }



        private void EliminarDetalle(Monitux_DB_Context context, string codigo)
        {
            var detalle = context.Ventas_Detalles.FirstOrDefault(v =>
                v.Secuencial_Factura == Secuencial_Venta &&
                v.Codigo == codigo &&
                v.Secuencial_Cliente == Secuencial_Cliente &&
                v.Secuencial_Empresa == Secuencial_Empresa);

            if (detalle != null)
            {
                context.Remove(detalle);
                context.SaveChanges(); // 💾 Esto confirma la eliminación
            }
        }


        private void Actualizar_Inventario(Monitux_DB_Context context, Miniatura_Producto copia)
        {
            if (copia.unidadesAgregar <= 0) return; // Validación rápida

            var producto = context.Productos.FirstOrDefault(p =>
                p.Secuencial == copia.Secuencial &&
                p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

            if (producto != null)
            {
                // 🛠️ Asegura que el valor sea válido y rastreable
                producto.Cantidad += copia.unidadesAgregar;


                // 🔒 Fuerza la detección del cambio en EF si fuera necesario
                context.Entry(producto).Property(p => p.Cantidad).IsModified = true;

                context.SaveChanges();

                // 📝 Registro de actividad, opcional pero útil para trazabilidad
                Util.Registrar_Actividad(Secuencial_Usuario,
                    $"Agregó {copia.unidadesAgregar} unidades al producto: {copia.Codigo}",
                    V_Menu_Principal.Secuencial_Empresa);
            }
        }




        ////////////////////////////



        ///////////////////////////














        private void RegistrarSalidaKardex(Monitux_DB_Context context, Miniatura_Producto copia)
        {
            if (copia.Precio_Venta <= 0 || copia.Precio_Costo <= 0 || copia.Tipo == "Servicio") return;

            Util.Registrar_Movimiento_Kardex(copia.Secuencial, copia.Cantidad, copia.Descripcion,
                copia.cantidadSelecccionItem, copia.Precio_Costo, copia.Precio_Venta, "Entrada", Secuencial_Empresa);
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





        private void button1_Click(object sender, EventArgs e)
        {

            Actualizar_Numeros();

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            // Buscar la venta existente
            var venta = context.Ventas.FirstOrDefault(v =>
                v.Secuencial == this.Secuencial_Venta &&
                v.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

            if (venta == null)
            {
                V_Menu_Principal.MSG.ShowMSG("No se encontró la factura para actualizar.", "Error");
                return;
            }

            // Actualizar datos principales
            venta.Secuencial_Cliente = Secuencial_Cliente;
            venta.Secuencial_Usuario = Secuencial_Usuario;
            venta.Tipo = comboBox3.SelectedItem?.ToString() ?? "Sin tipo";
            venta.Forma_Pago = comboBox1.SelectedItem?.ToString() ?? "Sin forma de pago";
            venta.Fecha = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            venta.Total = Math.Round(Convert.ToDouble(subTotal), 2);
            venta.Gran_Total = Math.Round(Convert.ToDouble(total), 2);
            venta.Impuesto = Convert.ToDouble(impuesto);
            venta.Otros_Cargos = Convert.ToDouble(otrosCargos);
            venta.Descuento = Convert.ToDouble(descuento);

            // Actualizar productos
            var productosCache = context.Productos
                .Where(p => Lista_de_Items.Values.Select(i => i.Secuencial).Contains(p.Secuencial))
                .ToDictionary(p => p.Secuencial);

            foreach (var pro in Lista_de_Items.Values)
            {
                if (productosCache.TryGetValue(pro.Secuencial, out var productoBD))
                {
                    pro.Precio_Costo = productoBD.Precio_Costo;
                    pro.Precio_Venta = productoBD.Precio_Venta;
                    pro.Cantidad = productoBD.Cantidad;
                }
            }

            using var context1 = new Monitux_DB_Context();
            context1.Database.EnsureCreated();

            foreach (var pro in Lista_de_Items.Values)
            {
                double nuevaCantidad = Convert.ToDouble(pro.cantidadSelecccionItem);

                var detalleExistente = context.Ventas_Detalles.FirstOrDefault(d =>
                    d.Secuencial_Factura == venta.Secuencial &&
                    d.Secuencial_Producto == pro.Secuencial &&
                    d.Secuencial_Empresa == venta.Secuencial_Empresa);

                if (detalleExistente != null)
                {
                    if (pro.Tipo != "Servicio")
                    {
                        double cantidadAnterior = Convert.ToDouble(detalleExistente.Cantidad);
                        double diferencia = nuevaCantidad - cantidadAnterior;

                        if (diferencia != 0)
                        {
                            string tipoMovimiento = diferencia > 0 ? "Salida" : "Entrada";
                            double cantidadMovimiento = Math.Abs(diferencia);

                            Util.Registrar_Movimiento_Kardex(pro.Secuencial, pro.Cantidad, pro.Descripcion,
                                cantidadMovimiento, pro.Precio_Costo, pro.Precio_Venta, tipoMovimiento, Secuencial_Empresa);

                            string accion = diferencia > 0 ? "Agregó" : "Quitó";
                            Util.Registrar_Actividad(Secuencial_Usuario,
                                $"{accion} {cantidadMovimiento} unidades de {pro.Codigo} en modificación de factura No: {venta.Secuencial}",
                                Secuencial_Empresa);

                            if (productosCache.TryGetValue(pro.Secuencial, out var producto))
                                producto.Cantidad -= diferencia;
                        }
                    }

                    detalleExistente.Cantidad = nuevaCantidad;
                    detalleExistente.Precio = Math.Round(Convert.ToDouble(pro.Precio_Venta), 2);
                    detalleExistente.Total = Math.Round(nuevaCantidad * Convert.ToDouble(pro.Precio_Venta), 2);
                    detalleExistente.Descripcion = pro.Descripcion;
                    detalleExistente.Tipo = pro.Tipo;
                    detalleExistente.Fecha = venta.Fecha;
                    detalleExistente.Secuencial_Usuario = venta.Secuencial_Usuario;
                    detalleExistente.Secuencial_Cliente = venta.Secuencial_Cliente;
                }
                else
                {
                    var detalleNuevo = new Venta_Detalle
                    {
                        Secuencial_Empresa = venta.Secuencial_Empresa,
                        Secuencial_Factura = venta.Secuencial,
                        Secuencial_Cliente = venta.Secuencial_Cliente,
                        Secuencial_Usuario = venta.Secuencial_Usuario,
                        Fecha = venta.Fecha,
                        Secuencial_Producto = pro.Secuencial,
                        Codigo = pro.Codigo,
                        Descripcion = pro.Descripcion,
                        Cantidad = nuevaCantidad,
                        Precio = Math.Round(Convert.ToDouble(pro.Precio_Venta), 2),
                        Total = Math.Round(nuevaCantidad * Convert.ToDouble(pro.Precio_Venta), 2),
                        Tipo = pro.Tipo
                    };

                    context.Ventas_Detalles.Add(detalleNuevo);

                    Util.Registrar_Actividad(Secuencial_Usuario,
                        $"Agregó la cantidad de: {nuevaCantidad} de: {pro.Codigo} a Factura No: {venta.Secuencial}",
                        Secuencial_Empresa);

                    if (pro.Tipo != "Servicio")
                    {
                        Util.Registrar_Movimiento_Kardex(pro.Secuencial, pro.Cantidad, pro.Descripcion,
                            nuevaCantidad, pro.Precio_Costo, pro.Precio_Venta, "Salida", Secuencial_Empresa);

                        if (productosCache.TryGetValue(pro.Secuencial, out var producto))
                            producto.Cantidad -= nuevaCantidad;
                    }
                }

                var resultado1 = context1.Cuentas_Cobrar.FirstOrDefault(v =>
                    v.Secuencial_Factura == venta.Secuencial &&
                    v.Secuencial_Cliente == venta.Secuencial_Cliente &&
                    v.Secuencial_Empresa == venta.Secuencial_Empresa);

                if (resultado1 != null)
                {
                    resultado1.Gran_Total = Math.Round(Convert.ToDouble(venta.Gran_Total), 2);
                    resultado1.Total = Math.Round(Convert.ToDouble(venta.Total), 2);
                    resultado1.Saldo = Math.Round(Convert.ToDouble(resultado1.Gran_Total - resultado1.Pagado), 2);
                }
            }

            var ingresoExistente = context.Ingresos.FirstOrDefault(i =>
                i.Secuencial_Factura == venta.Secuencial &&
                i.Secuencial_Empresa == venta.Secuencial_Empresa);

            if (ingresoExistente != null)
            {
                ingresoExistente.Total = Convert.ToDouble(venta.Gran_Total);
                ingresoExistente.Descripcion = $"Actualización de ingreso por compra No. {venta.Secuencial}";
                ingresoExistente.Fecha = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                ingresoExistente.Tipo_Ingreso = venta.Tipo;
                ingresoExistente.Secuencial_Usuario = venta.Secuencial_Usuario;
            }

            context.SaveChanges();
            context1.SaveChanges();

            // 🧾 Generar nuevo PDF en memoria
            var factura = new FacturaCompletaPDF_Venta
            {
                Secuencial = venta.Secuencial,
                Cliente = comboCliente.SelectedItem?.ToString()
                    .Substring(comboCliente.SelectedItem.ToString().IndexOf("- ") + 2)
                    .Trim() ?? "Sin cliente",
                TipoVenta = venta.Tipo,
                MetodoPago = venta.Forma_Pago,
                Fecha = venta.Fecha,
                Items = ObtenerItemsDesdeGrid(dataGridView1),
                ISV = Convert.ToDouble(venta.Impuesto),
                OtrosCargos = Convert.ToDouble(venta.Otros_Cargos),
                Descuento = Convert.ToDouble(venta.Descuento)
            };

            venta.Documento = factura.GeneratePdfToBytes();
            context.SaveChanges();

            // 🧹 Limpiar listas
            Lista_de_Items.Clear();
            Lista_de_Items_Eliminar.Clear();

            // ✅ Confirmación
            V_Menu_Principal.MSG.ShowMSG($"Factura No. {venta.Secuencial} actualizada correctamente.", "Ventas");
            this.Dispose();


        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void V_Editar_Factura_Venta_FormClosing(object sender, FormClosingEventArgs e)
        {
            Lista_de_Items.Clear();
            Lista_de_Items_Eliminar.Clear();
        }

        private void comboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
