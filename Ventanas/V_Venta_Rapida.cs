using AForge.Video.DirectShow;
using Microsoft.EntityFrameworkCore;
using Monitux_POS.Clases;
using PdfiumViewer;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.Windows.Compatibility;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Monitux_POS.Ventanas
{
    public partial class V_Venta_Rapida : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        public Dictionary<string, Miniatura_Producto> Lista_de_Items = new Dictionary<string, Miniatura_Producto>();

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



        public void Filtrar(string campo, string valor, int secuencial_empresa)
        {

            string cantidad = "0";

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            var productos = context.Productos
     .Where(c => EF.Property<string>(c, campo).Equals(valor) && c.Secuencial_Empresa == secuencial_empresa)
     .ToList();





            foreach (var item in productos)
            {

                if (V_Menu_Principal.IPB.Show("Digite la cantidad en números de este producto que esta agregando", "¿Cuantos: " + item.Codigo + "?", out cantidad) == DialogResult.OK)
                {

                    cantidad = cantidad?.Trim();

                    if (Double.TryParse(cantidad, NumberStyles.Any, CultureInfo.InvariantCulture, out double numero))
                    {
                        if (!Lista_de_Items.ContainsKey(item.Codigo))
                        {
                            Lista_de_Items.Add(item.Codigo, new Miniatura_Producto
                            {
                                Secuencial = item.Secuencial,
                                Codigo = item.Codigo,
                                Descripcion = item.Descripcion,
                                Precio_Venta = item.Precio_Venta,
                                Cantidad = item.Cantidad,
                                cantidadSelecccionItem = numero,
                                Tipo = item.Tipo
                            });


                            dataGridView1.Rows.Add(item.Secuencial,
                            item.Codigo,
                            item.Descripcion,
                            Lista_de_Items[item.Codigo].cantidadSelecccionItem,
                            Math.Round((double)item.Precio_Venta, 2),
                            numero * Math.Round((double)item.Precio_Venta, 2));


                        }
                        else
                        {
                            Lista_de_Items[item.Codigo].cantidadSelecccionItem = Lista_de_Items[item.Codigo].cantidadSelecccionItem + numero;
                            dataGridView1.Rows.Remove(dataGridView1.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Cells["Codigo"].Value.ToString() == item.Codigo));

                            dataGridView1.Rows.Add(item.Secuencial,
                     item.Codigo,
                     item.Descripcion,

                     Lista_de_Items[item.Codigo].cantidadSelecccionItem,
                     Math.Round((double)item.Precio_Venta, 2),
                     Lista_de_Items[item.Codigo].cantidadSelecccionItem * Math.Round((double)item.Precio_Venta, 2)); //ojo aqui



                        }


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
            comboCliente.SelectedIndex = -1; // Limpiar la selección del cliente
            /*
                        textBox1.Text = "1";
                        textBox1.Focus();

                        // Simula la pulsación de Enter para activar el KeyDown
                        BeginInvoke((MethodInvoker)(() => SendKeys.Send("{ENTER}")));


                        */


            dataGridView1.Rows.Clear();
            subtotal = 0.0;
            total = 0.0;
            ISV = 0.0; // Reiniciar el ISV antes de calcular
            lbl_SubTotal.Text = $"{subtotal:0.00}";
            lbl_ISV.Text = $"{ISV:0.00}";
            lbl_Total.Text = $"{total:0.00}";
            textBox1.Clear();
            Configurar_DataGridView();
            checkBox2.Checked = false; // Desmarcar el checkbox de ISV

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

            var topProductos = Util.ObtenerTopProductosVendidos(7);
            string cantidad = "0";
            foreach (var producto in topProductos)
            {
                var btn = new System.Windows.Forms.Button
                {
                    Width = 155,
                    Height = 60,
                    BackColor = Color.FromArgb(35, 32, 45),
                    ForeColor = Color.Yellow,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = $"{producto.Descripcion.Substring(0, Math.Min(35, producto.Descripcion.Length))}\n{V_Menu_Principal.moneda}{producto.Venta:0.00}",
                    Tag = producto
                };
                btn.FlatAppearance.BorderColor = Color.FromArgb(0, 168, 107);
                btn.FlatAppearance.BorderSize = 1;

                var tooltip = new System.Windows.Forms.ToolTip();
                tooltip.SetToolTip(btn,producto.Codigo +"\n" + producto.Descripcion);

                btn.Click += (s, e) =>
                {
                    var prod = (Producto_Top_VR)((System.Windows.Forms.Button)s).Tag;
                    //AgregarProductoAlCarrito(prod); // ← Implementa esta función según tu lógica



                            Filtrar("Codigo", producto.Codigo, V_Menu_Principal.Secuencial_Empresa);


                        

                        if (checkBox1.Checked)
                        {
                            textBox1.Select();
                            textBox1.Focus();
                        }

                    
                   

                    ActualizarTotal();


                };

                RestaurarFocoEscaner();
                panel.Controls.Add(btn);
            }
        }


        public void llenar_Combo_Cliente()
        {


            comboCliente.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar solo clientes activos
            var clientesActivos = context.Clientes.Where(c => (bool)c.Activo&&c.Secuencial_Empresa==V_Menu_Principal.Secuencial_Empresa).ToList();

            foreach (var item in clientesActivos)
            {
                comboCliente.Items.Add(item.Secuencial + " - " + item.Nombre);
            }





        }





        private void V_Venta_Rapida_Load(object sender, EventArgs e)
        {
            textBox1.Select();
            textBox1.Focus();
            MostrarTopProductosEnPanel(flowLayoutPanel1);
            Configurar_DataGridView();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            llenar_Combo_Cliente();
            comboBox1.Items.Add("Codigo");
            comboBox1.Items.Add("Codigo_Barra");
            comboBox1.Items.Add("Codigo_Fabricante");



            comboBox1.SelectedIndex = 0; // Seleccionar el primer elemento por defecto


        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!textBox1.Text.Equals(string.IsNullOrEmpty) && !textBox1.Text.Equals(string.IsNullOrWhiteSpace))
            {


                if (e.KeyCode == Keys.Enter)
                {
                    // Aquí va lo que quieras ejecutar
                    string texto = textBox1.Text.Trim();
                    Filtrar(comboBox1.SelectedItem.ToString(), texto,V_Menu_Principal.Secuencial_Empresa);
                    ActualizarTotal();
                    e.Handled = true;
                    e.SuppressKeyPress = true; // Opcional: evita que suene "ding"

                    if (checkBox1.Checked)
                    {
                        IniciarEscaneo(); // Reiniciar la cámara para escanear el siguiente código de barras
                    }


                }
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
            if (checkBox1.Checked)
            {
                comboBox1.Enabled = false;
                comboBox1.SelectedIndex = 1;
                cboCamaras.Visible = true;
                pictureBox1.Visible = true;

                filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                foreach (FilterInfo device in filterInfoCollection)
                    cboCamaras.Items.Add(device.Name);
                cboCamaras.SelectedIndex = 0;


                ///////////


                videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboCamaras.SelectedIndex].MonikerString);
                videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
                videoCaptureDevice.Start();


                ///////////



            }
            else
            {
                cboCamaras.Visible = false;
                comboBox1.Enabled = true;
                pictureBox1.Visible = false;
                if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
                {
                    videoCaptureDevice.SignalToStop();
                    videoCaptureDevice.WaitForStop();

                }

                comboBox1.SelectedIndex = 0; // Volver al primer elemento del ComboBox
            }
        }

        private void IniciarEscaneo()
        {
            textBox1.Clear(); // Limpiar el TextBox antes de iniciar el escaneo
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.SignalToStop();
                videoCaptureDevice.WaitForStop();
            }
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboCamaras.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }


        private void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

            BarcodeReader reader = new BarcodeReader();

            try
            {
                if (bitmap != null)
                {

                    var result = reader.Decode(bitmap);

                    //Creo que esta solucionado el problema de que no se decodificaba el codigo de barras


                    if (result != null)
                    {

                        textBox1.Invoke(new MethodInvoker(delegate ()
                        {
                            textBox1.Text = result.ToString();


                            textBox1.Focus();

                            // Simula la pulsación de Enter para activar el KeyDown
                            BeginInvoke((MethodInvoker)(() => SendKeys.Send("{ENTER}")));



                            videoCaptureDevice.SignalToStop();



                        }));
                        // pictureBox1.Image = null;
                    }
                    pictureBox1.Image = bitmap;


                }

            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al decodificar el código de barras
                //               Console.WriteLine("Error al decodificar el código de barras: " + ex.Message);

                //MessageBox.Show("Error al decodificar el código de barras: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




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

            if (dataGridView1.CurrentRow == null)
            {
                V_Menu_Principal.MSG.ShowMSG("No hay filas seleccionadas para eliminar.", "Error");
                return;
            }

            Lista_de_Items.Remove(dataGridView1.CurrentRow.Cells["Codigo"].Value.ToString());

            // Eliminar la fila seleccionada
            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            ActualizarTotal();

            RestaurarFocoEscaner();
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
            if (comboCliente.SelectedIndex == -1)
            {
                V_Menu_Principal.MSG.ShowMSG("Debe seleccionar un cliente para registrar la venta.", "Error");
                return; // Si no se ha seleccionado un cliente, no se puede registrar la venta
            }

            if (dataGridView1.Rows.Count == 0)
            {
                V_Menu_Principal.MSG.ShowMSG("No hay productos en la venta.", "Error");
                return; // Si no hay productos, no se puede registrar la venta
            }




            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            // Generar secuencial
            int secuencial = context.Ventas.Any() ? context.Ventas.Max(p => p.Secuencial) + 1 : 1;

            // Crear venta
            var venta = new Venta
            {
                Secuencial = secuencial,
                Secuencial_Cliente = comboCliente.SelectedIndex != -1
                    ? int.Parse(comboCliente.SelectedItem.ToString().Split('-')[0].Trim()) : 0,
                Secuencial_Usuario = V_Menu_Principal.Secuencial_Usuario,
                Fecha = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"),
                Tipo = "Contado",
                Forma_Pago = "Efectivo",
                Total = Math.Round(subtotal, 2),
                Gran_Total = Math.Round(total, 2),
                Impuesto = ISV,
                Otros_Cargos = 0,
                Descuento = 0,
                Secuencial_Empresa=V_Menu_Principal.Secuencial_Empresa
            };
            context.Ventas.Add(venta);
            context.SaveChanges();

            // Agregar detalles de venta y actualizar inventario si aplica
            foreach (var pro in Lista_de_Items.Values)
            {
                var detalle = new Venta_Detalle
                {
                    Secuencial_Empresa=venta.Secuencial_Empresa,
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

                // Si no es servicio, restar stock y registrar en Kardex
                if (pro.Tipo != "Servicio")
                {
                    Util.Registrar_Movimiento_Kardex(pro.Secuencial, pro.Cantidad, pro.Descripcion,
                        pro.cantidadSelecccionItem, pro.Precio_Costo, pro.Precio_Venta, "Salida", V_Menu_Principal.Secuencial_Empresa);

                    var producto = context.Productos.FirstOrDefault(p => p.Secuencial == pro.Secuencial);
                    if (producto != null)
                        producto.Cantidad = pro.Cantidad - pro.cantidadSelecccionItem;
                }





            }
            context.SaveChanges();

            // Registrar ingreso de efectivo
            var ingreso = new Ingreso
            {
                Secuencial_Factura = venta.Secuencial,
                Secuencial_Usuario = venta.Secuencial_Usuario,
                Fecha = venta.Fecha,
                Total = Math.Round(total, 2),
                Tipo_Ingreso = "Efectivo",
                Descripcion = $"Venta según Factura: {venta.Secuencial}",
                Secuencial_Empresa=venta.Secuencial_Empresa
            };
            context.Ingresos.Add(ingreso);
            context.SaveChanges();

            // Confirmar y limpiar
            V_Menu_Principal.MSG.ShowMSG("Venta registrada correctamente.", "Éxito");
            Util.Registrar_Actividad(venta.Secuencial_Usuario, $"Ha registrado una venta según factura: {venta.Secuencial}, por un valor de: {Math.Round(total, 2):C}", V_Menu_Principal.Secuencial_Empresa);





            var factura = new FacturaCompletaPDF_Venta
            {
                Secuencial = venta.Secuencial,
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



            string rutaPdf = $"{rutaGuardado}-{venta.Secuencial}-{factura.Cliente}.pdf";



            var destinatariocliente = context.Clientes.FirstOrDefault(c => c.Secuencial == venta.Secuencial_Cliente);
            string? destinatario = "";
            if (destinatariocliente != null)
            {
                destinatario = destinatariocliente.Email;
            }

            Util.EnviarCorreoConPdf("monitux.pos@gmail.com",
                destinatario,
                V_Menu_Principal.Nombre_Empresa + " - " + "Comprobante",
                "Gracias por su compra. Adjunto tiene su comprobante.",
                rutaPdf,
                "smtp.gmail.com",
                587,
                "monitux.pos", "ffeg qqnx zaij otmb");







            // Mostrar diálogo para seleccionar impresora
            using (PrintDialog printDialog = new PrintDialog())
            {
                printDialog.AllowSomePages = true;
                printDialog.AllowSelection = true;
                printDialog.UseEXDialog = true;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var documento = PdfDocument.Load(rutaPdf))
                    {
                        using (var printDoc = documento.CreatePrintDocument())
                        {
                            printDoc.PrinterSettings = printDialog.PrinterSettings;
                            printDoc.PrintController = new StandardPrintController(); // Oculta ventana de impresión
                            printDoc.Print();
                            Console.WriteLine("✅ Impresión enviada correctamente.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("❌ Impresión cancelada por el usuario.");
                }
            }





            button7.PerformClick(); // Limpiar
            RestaurarFocoEscaner(); // Enfocar escáner







            ////////////////////////////

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

            venta.Secuencial_Usuario = V_Menu_Principal.Secuencial_Usuario; // Asignar el secuencial del usuario que está realizando la venta
            venta.Fecha = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"); // Asignar la fecha y hora actual de la venta
            venta.Tipo = "Contado"; // Obtener el tipo de venta seleccionado
            venta.Total = Math.Round(subtotal, 2); // Asignar el total de la venta
            venta.Forma_Pago = "Efectivo"; // Obtener la forma de pago seleccionada
            venta.Gran_Total = Math.Round(total, 2); // Asignar el gran total de la venta

            venta.Impuesto = ISV; // Asignar el impuesto de la venta
            venta.Otros_Cargos = 0; // Asignar los otros cargos de la venta
            venta.Descuento = 0; // Asignar el descuento de la venta    
            context.Add(venta);
            context.SaveChanges(); // Guardar los cambios en la base de datos



            /////////////////////////////



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




           
                ingreso.Secuencial_Factura = secuencial; // Asignar el secuencial de la factura al ingreso
                ingreso.Secuencial_Usuario = venta.Secuencial_Usuario; // Asignar el secuencial del usuario al ingreso
                ingreso.Fecha = venta.Fecha; // Asignar la fecha de la venta al ingreso 
                ingreso.Total = Math.Round(total, 2); // Asignar el total de la venta al ingreso
                ingreso.Tipo_Ingreso = "Efectivo"; // Asignar la forma de pago al ingreso
                ingreso.Descripcion = "Venta segun Factura: " + secuencial; // Asignar el tipo de venta al ingreso

                SQLitePCL.Batteries.Init();

                using var context4 = new Monitux_DB_Context();
                context4.Database.EnsureCreated(); // Crea la base de datos si no existe

                context4.Add(ingreso); // Agregar el ingreso al contexto
                context4.SaveChanges(); // Guardar los cambios en la base de datos

            



            ///////////////////////////



            V_Menu_Principal.MSG.ShowMSG("Venta registrada correctamente.", "Éxito");
            // Limpiar los campos y controles después de registrar la venta
            Util.Registrar_Actividad(V_Menu_Principal.Secuencial_Usuario, "Ha registrado una venta segun factura: " + secuencial + "\nPor valor de: " + Math.Round(total, 2));


            //Imprimir_Comprobante();


            button7.PerformClick(); // Limpiar la venta rápida








            RestaurarFocoEscaner();*/
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

        private void cboCamaras_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void V_Venta_Rapida_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning)

                    videoCaptureDevice.SignalToStop();
                videoCaptureDevice.WaitForStop();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboCliente_MouseClick(object sender, MouseEventArgs e)
        {
            llenar_Combo_Cliente();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
