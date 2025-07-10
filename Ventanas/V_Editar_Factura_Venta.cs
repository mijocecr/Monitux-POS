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

            label5.Text = Lista_de_Items.Count.ToString();

          


            button2.PerformClick();

        }

        
        private void Cargar_Otros_Datos()
        {

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            var resultado = context.Ventas
              .Where(v => v.Secuencial == this.Secuencial_Venta &&
                          v.Secuencial_Cliente == Secuencial_Cliente &&
                          v.Secuencial_Empresa == Secuencial_Empresa)
              .FirstOrDefault();

            if (resultado != null)
            {
                descuento = (double)resultado.Descuento;

                if (subTotal > 0)
                {
                    double porcentajeDescuento = Math.Round((descuento / subTotal) * 100.0, 2);
                    double porcentajeImpuesto = Math.Round((impuesto / subTotal) * 100.0, 2);

                    txt_Descuento.Text = porcentajeDescuento.ToString("0.00");
                    txt_Impuesto.Text = porcentajeImpuesto.ToString("0.00");
                }
                else
                {
                    txt_Descuento.Text = "0.00";
                    txt_Impuesto.Text = "0.00";
                }


                
                impuesto = Math.Round((double)resultado.Impuesto,2);
                otrosCargos = Math.Round((double)resultado.Otros_Cargos, 2);
              

           
                lbl_Descuento.Text = $"{Convert.ToDecimal(resultado.Descuento):0.00}";
                lbl_Impuesto.Text = $"{Convert.ToDecimal(resultado.Impuesto):0.00}";
                lbl_OtrosCargos.Text = $"{Convert.ToDecimal(resultado.Otros_Cargos):0.00}";
                comboBox3.SelectedItem = resultado.Tipo;
                comboBox1.SelectedItem = resultado.Forma_Pago;
                if (resultado.Tipo == "Credito")
                {

                    var resultadoo = context.Cuentas_Cobrar
             .Where(v => v.Secuencial_Factura == this.Secuencial_Venta &&
                         v.Secuencial_Cliente == Secuencial_Cliente &&
                         v.Secuencial_Empresa == Secuencial_Empresa)
             .FirstOrDefault();

                    if (resultadoo != null)
                    {


                        if (DateTime.TryParse(resultadoo.Fecha_Vencimiento, out DateTime fechaVencimiento))
                        {
                            dateTimePicker1.Value = fechaVencimiento;
                        }


                    }


                }


            }
            else
            {
                // Opcional: manejar el caso en que no se encuentra una venta
                lbl_Descuento.Text = "0.00";
                lbl_Impuesto.Text = "0.00";
                lbl_OtrosCargos.Text = "0.00";
            }
            Actualizar_Numeros();

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




        ///////////////////////////////////////////////////////










        private void Actualizar_Numeros()
        {


           ActualizarTotalesFinancieros();


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

            

            // ✅ Actualiza cantidades seleccionadas desde el panel
            foreach (Selector_Cantidad selector in flowLayoutPanel2.Controls)
            {
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
                impuesto = double.Parse(txt_Impuesto.Text);
                impuesto = (impuesto / 100) * subTotal; // Convertir el porcentaje a decimal

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
                descuento = double.Parse(txt_Descuento.Text);
                descuento = (descuento / 100) * subTotal; // Convertir el porcentaje a decimal

            }
            catch
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
                Actualizar_Numeros() ;
            }
        }

        private void flowLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            label5.Text = Lista_de_Items.Count.ToString();

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

        private void button1_Click(object sender, EventArgs e)
        {
            txt_OtrosCargos.Text = "5";

        }
    }
}
