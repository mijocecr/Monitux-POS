using Microsoft.EntityFrameworkCore;
using Monitux_POS.Clases;
using Monitux_POS.Controles;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitux_POS.Ventanas
{
    public partial class V_Inventario : Form
    {
        public V_Inventario()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, e) =>
            {
                e.Graphics.DrawString("Reporte de Inventario\n", new Font("Arial", 16), Brushes.Black, new PointF(100, 100));
                // Dibujar una línea divisoria
                e.Graphics.DrawLine(Pens.Black, 100, 80, 500, 80);


                // Agregar una lista de productos
                string[] productos = { "Producto A", "Producto B", "Producto C" };
                int y = 100;
                foreach (var producto in productos)
                {
                    e.Graphics.DrawString(producto, new Font("Arial", 12), Brushes.Black, new PointF(100, y));
                    y += 20;
                }

            };
            pd.Print();

        }

        private void button6_Click(object sender, EventArgs e)
        {

            button1.Visible = true;
            button2.Visible = true;
            /*GlobalFontSettings.UseWindowsFontsUnderWindows = true;

            // Crear un nuevo documento PDF
            PdfDocument documento = new PdfDocument();
            documento.Info.Title = "Ejemplo de PDFSharp";

            // Agregar una página al documento
            PdfPage pagina = documento.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(pagina);

            // Definir una fuente
            XFont fuente = new XFont("Times New Roman", 12, XFontStyleEx.Regular);



            // Dibujar texto en el PDF
            gfx.DrawString("¡Hola, Miguel! Este es un PDF generado con PdfSharp.", fuente, XBrushes.Black, new XPoint(100, 100));

            // Dibujar una línea
            gfx.DrawLine(XPens.Black, 100, 120, 300, 120);

            // Guardar el documento
            documento.Save("Ejemplo.pdf");

            MessageBox.Show("PDF generado exitosamente.");*/




        }

        private void button4_Click(object sender, EventArgs e)
        {


            comboBox3.Items.Clear();
            comboBox3.Items.Add("Codigo");
            comboBox3.Items.Add("Codigo_Barra");
            comboBox3.Items.Add("Codigo_Fabricante");
            comboBox3.Items.Add("Descripcion");
            comboBox3.Items.Add("Fecha_Caducidad");
            comboBox3.Items.Add("Marca");
            comboBox3.SelectedIndex = 0;

            button2.Visible = true;



            label8.Text = "Modo: Cuadricula";



            Cargar_Items_Cuadricula();




        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("Codigo");
            comboBox3.Items.Add("Codigo_Barra");
            comboBox3.Items.Add("Codigo_Fabricante");
            comboBox3.Items.Add("Descripcion");
            comboBox3.Items.Add("Fecha_Caducidad");
            comboBox3.Items.Add("Marca");
            comboBox3.SelectedIndex = 0;

            label8.Text = "Modo: Lista";
            button2.Visible = true;

            Configurar_DataGridView_Inventario();

            Cargar_Items_Lista();


        }
        int posX, posY;
        private void label6_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                posX = e.X;
                posY = e.Y;

            }
            else
            {
                label6.Cursor = Cursor.Current = Cursors.SizeAll;
                Left = Left + (e.X - posX);
                Top = Top + (e.Y - posY);

            }
        }





        public List<Miniatura_Producto> Cargar_Items_Cuadricula()
        {
            List<Miniatura_Producto> lista_cargada = new List<Miniatura_Producto>();
            linkLabel1.Visible = true;
            linkLabel2.Visible = true;
            groupBox2.Visible = true;
            groupBox1.Visible = false;
            flowLayoutPanel1.Controls.Clear();
            dataGridView1.Visible = false;
            flowLayoutPanel1.Visible = true;
            dataGridView2.Visible = false;



            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe




            // **READ**
            var productos = context.Productos.ToList();
            int i = 0;



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
                miniatura_Producto1.Secuencial_Usuario = V_Menu_Principal.Secuencial_Usuario;
                miniatura_Producto1.Fecha_Caducidad = item.Fecha_Caducidad;
                miniatura_Producto1.Expira = Convert.ToBoolean(item.Expira);
                miniatura_Producto1.moneda = V_Menu_Principal.moneda; // Asignar la moneda a la miniatura del producto
                miniatura_Producto1.Tipo = item.Tipo; // Asignar el tipo de producto (si es necesario)

                miniatura_Producto1.Item_Seleccionado.Visible = false;



                miniatura_Producto1.Item_Imagen.Click += async (s, ev) =>
                {

                    miniatura_Producto1.Seleccionado = false;
                    miniatura_Producto1.Item_Seleccionado.Checked = false;
                    miniatura_Producto1.Item_Seleccionado.Visible = false;

                };




                miniatura_Producto1.Item_Imagen.DoubleClick += async (s, ev) =>
                {
                    // MessageBox.Show(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1).ToString());

                    V_Kardex v_Kardex = new V_Kardex();

                    V_Kardex.Secuencial_Producto = miniatura_Producto1.Secuencial;
                    V_Kardex.Codigo_Producto = miniatura_Producto1.Codigo;
                    if (miniatura_Producto1.Tipo != "Servicio")
                    {

                        v_Kardex.ShowDialog();

                    }




                };



                flowLayoutPanel1.Controls.Add(miniatura_Producto1);
                lista_cargada.Add(miniatura_Producto1); // Agregar a la lista de miniaturas cargadas


            }


            return lista_cargada;





        }




        public void Cargar_Existencia_Minima_Cuadricula(List<Miniatura_Producto> lista_productos)
        {
            groupBox2.Visible = true;
            groupBox1.Visible = false;
            comboBox3.Items.Clear();
            /* comboBox3.Items.Add("Codigo");
             comboBox3.Items.Add("Codigo_Barra");
             comboBox3.Items.Add("Codigo_Fabricante");
             comboBox3.Items.Add("Descripcion");
             comboBox3.Items.Add("Fecha_Caducidad");
             comboBox3.Items.Add("Marca");
             comboBox3.SelectedIndex = 0;*/
            flowLayoutPanel1.Controls.Clear();
            dataGridView1.Visible = false;
            flowLayoutPanel1.Visible = true;
            dataGridView2.Visible = false;



            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe




            // **READ**
            var productos = context.Productos.ToList();
            int i = 0;



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
                miniatura_Producto1.Secuencial_Usuario = V_Menu_Principal.Secuencial_Usuario;
                miniatura_Producto1.Fecha_Caducidad = item.Fecha_Caducidad;
                miniatura_Producto1.Expira = Convert.ToBoolean(item.Expira);
                miniatura_Producto1.moneda = V_Menu_Principal.moneda; // Asignar la moneda a la miniatura del producto
                miniatura_Producto1.Tipo = item.Tipo; // Asignar el tipo de producto (si es necesario)

                miniatura_Producto1.Item_Seleccionado.Visible = false;



                miniatura_Producto1.Item_Imagen.Click += async (s, ev) =>
                {

                    miniatura_Producto1.Seleccionado = false;
                    miniatura_Producto1.Item_Seleccionado.Checked = false;
                    miniatura_Producto1.Item_Seleccionado.Visible = false;

                };




                miniatura_Producto1.Item_Imagen.DoubleClick += async (s, ev) =>
                {
                    // MessageBox.Show(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1).ToString());



                    Selector_Cantidad selector_Cantidad = new Selector_Cantidad();







                    selector_Cantidad.Tag = miniatura_Producto1.Codigo; // Asigna el código como Tag para referencia futura




                };



                //flowLayoutPanel1.Controls.Add(miniatura_Producto1);



            }

            List<Miniatura_Producto> existencia_minima = new List<Miniatura_Producto>();

            foreach (var item in lista_productos)
            {
                if (item.Cantidad == item.Existencia_Minima && item.Tipo == "Producto")
                {
                    existencia_minima.Add(item);
                    flowLayoutPanel1.Controls.Add(item);
                }
            }





        }




        public void Cargar_Agotados_Cuadricula(List<Miniatura_Producto> lista_productos)
        {
            flowLayoutPanel1.Controls.Clear();
            groupBox2.Visible = true;
            groupBox1.Visible = false;
            /* comboBox3.Items.Clear();
             comboBox3.Items.Add("Codigo");
             comboBox3.Items.Add("Codigo_Barra");
             comboBox3.Items.Add("Codigo_Fabricante");
             comboBox3.Items.Add("Descripcion");
             comboBox3.Items.Add("Fecha_Caducidad");
             comboBox3.Items.Add("Marca");
             comboBox3.SelectedIndex = 0;*/

            dataGridView1.Visible = false;
            flowLayoutPanel1.Visible = true;
            dataGridView2.Visible = false;



            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe




            // **READ**
            var productos = context.Productos.ToList();
            int i = 0;



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
                miniatura_Producto1.Secuencial_Usuario = V_Menu_Principal.Secuencial_Usuario;
                miniatura_Producto1.Fecha_Caducidad = item.Fecha_Caducidad;
                miniatura_Producto1.Expira = Convert.ToBoolean(item.Expira);
                miniatura_Producto1.moneda = V_Menu_Principal.moneda; // Asignar la moneda a la miniatura del producto
                miniatura_Producto1.Tipo = item.Tipo; // Asignar el tipo de producto (si es necesario)

                miniatura_Producto1.Item_Seleccionado.Visible = false;



                miniatura_Producto1.Item_Imagen.Click += async (s, ev) =>
                {

                    miniatura_Producto1.Seleccionado = false;
                    miniatura_Producto1.Item_Seleccionado.Checked = false;
                    miniatura_Producto1.Item_Seleccionado.Visible = false;

                };




                miniatura_Producto1.Item_Imagen.DoubleClick += async (s, ev) =>
                {
                    // MessageBox.Show(flowLayoutPanel1.Controls.IndexOf(miniatura_Producto1).ToString());



                    Selector_Cantidad selector_Cantidad = new Selector_Cantidad();







                    selector_Cantidad.Tag = miniatura_Producto1.Codigo; // Asigna el código como Tag para referencia futura




                };



                //flowLayoutPanel1.Controls.Add(miniatura_Producto1);



            }

            List<Miniatura_Producto> existencia_minima = new List<Miniatura_Producto>();

            foreach (var item in lista_productos)
            {
                if (item.Cantidad < item.Existencia_Minima && item.Tipo == "Producto")
                {
                    existencia_minima.Add(item);
                    flowLayoutPanel1.Controls.Add(item);
                }
            }





        }




        public List<Producto> Cargar_Items_Lista()
        {
            List<Producto> lista_cargada = new List<Producto>();
            groupBox2.Visible = true;
            groupBox1.Visible = false;
            linkLabel1.Visible = true;
            linkLabel2.Visible = true;


            dataGridView1.Rows.Clear();
            flowLayoutPanel1.Controls.Clear();
            dataGridView1.Visible = true;
            Configurar_DataGridView_Inventario();
            flowLayoutPanel1.Visible = false;
            dataGridView2.Visible = false;



            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe




            // **READ**
            var productos = context.Productos.ToList();
            int i = 0;



            foreach (var item in productos)
            {


                Producto producto = new Producto
                {
                    Secuencial = item.Secuencial,
                    Codigo = item.Codigo,
                    Descripcion = item.Descripcion,
                    Marca = item.Marca,
                    Cantidad = item.Cantidad,
                    Existencia_Minima = item.Existencia_Minima,
                    Codigo_Barra = item.Codigo_Barra,
                    Codigo_Fabricante = item.Codigo_Fabricante,
                    Fecha_Caducidad = item.Fecha_Caducidad,
                    Precio_Costo = Math.Round((double)item.Precio_Costo, 2),
                    Precio_Venta = Math.Round((double)item.Precio_Venta, 2),
                    Secuencial_Proveedor = item.Secuencial_Proveedor,
                    Secuencial_Categoria = item.Secuencial_Categoria,
                    Expira = Convert.ToBoolean(item.Expira),
                    Tipo = item.Tipo
                };


                dataGridView1.Rows.Add(item.Secuencial,
                    item.Codigo,
                    item.Descripcion,
                        item.Marca,
                    item.Cantidad,
                    item.Existencia_Minima,
                    item.Codigo_Barra,
                    item.Codigo_Fabricante,
                    item.Fecha_Caducidad,
                    Math.Round((double)item.Precio_Costo, 2),
                    Math.Round((double)item.Precio_Venta, 2)


                );


                lista_cargada.Add(producto); // Agregar a la lista de productos cargados


                ///flowLayoutPanel1.Controls.Add(miniatura_Producto1);



            }

            return lista_cargada; // Retornar la lista de productos cargados

        }







        public void Cargar_Existencia_Minima_Lista(List<Producto> lista_productos)
        {
            List<Producto> lista_cargada = new List<Producto>();
            groupBox2.Visible = true;
            groupBox1.Visible = false;

            comboBox3.Items.Clear();
            comboBox3.Items.Add("Codigo");
            comboBox3.Items.Add("Codigo_Barra");
            comboBox3.Items.Add("Codigo_Fabricante");
            comboBox3.Items.Add("Descripcion");
            comboBox3.Items.Add("Fecha_Caducidad");
            comboBox3.Items.Add("Marca");
            comboBox3.SelectedIndex = 0;


            dataGridView1.Rows.Clear();
            flowLayoutPanel1.Controls.Clear();
            dataGridView1.Visible = true;
            Configurar_DataGridView_Inventario();
            flowLayoutPanel1.Visible = false;
            dataGridView2.Visible = false;



            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe




            // **READ**
            var productos = context.Productos.ToList();
            int i = 0;



            foreach (var item in productos)
            {


                Producto producto = new Producto
                {
                    Secuencial = item.Secuencial,
                    Codigo = item.Codigo,
                    Descripcion = item.Descripcion,
                    Marca = item.Marca,
                    Cantidad = item.Cantidad,
                    Existencia_Minima = item.Existencia_Minima,
                    Codigo_Barra = item.Codigo_Barra,
                    Codigo_Fabricante = item.Codigo_Fabricante,
                    Fecha_Caducidad = item.Fecha_Caducidad,
                    Precio_Costo = Math.Round((double)item.Precio_Costo, 2),
                    Precio_Venta = Math.Round((double)item.Precio_Venta, 2),
                    Secuencial_Proveedor = item.Secuencial_Proveedor,
                    Secuencial_Categoria = item.Secuencial_Categoria,
                    Expira = Convert.ToBoolean(item.Expira),
                    Tipo = item.Tipo
                };





                if (producto.Cantidad == producto.Existencia_Minima && producto.Tipo == "Producto")
                {
                    dataGridView1.Rows.Add(item.Secuencial,
                  item.Codigo,
                  item.Descripcion,
                      item.Marca,
                  item.Cantidad,
                  item.Existencia_Minima,
                  item.Codigo_Barra,
                  item.Codigo_Fabricante,
                  item.Fecha_Caducidad,
                  Math.Round((double)item.Precio_Costo, 2),
                  Math.Round((double)item.Precio_Venta, 2)


              );

                    lista_cargada.Add(producto); // Agregar a la lista de productos con existencia 0
                }



            }



        }





        public void Cargar_Agotados_Lista(List<Producto> lista_productos)
        {
            List<Producto> lista_cargada = new List<Producto>();
            groupBox2.Visible = true;
            groupBox1.Visible = false;

            comboBox3.Items.Clear();
            comboBox3.Items.Add("Codigo");
            comboBox3.Items.Add("Codigo_Barra");
            comboBox3.Items.Add("Codigo_Fabricante");
            comboBox3.Items.Add("Descripcion");
            comboBox3.Items.Add("Fecha_Caducidad");
            comboBox3.Items.Add("Marca");
            comboBox3.SelectedIndex = 0;


            dataGridView1.Rows.Clear();
            flowLayoutPanel1.Controls.Clear();
            dataGridView1.Visible = true;
            Configurar_DataGridView_Inventario();
            flowLayoutPanel1.Visible = false;
            dataGridView2.Visible = false;



            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe




            // **READ**
            var productos = context.Productos.ToList();
            int i = 0;



            foreach (var item in productos)
            {


                Producto producto = new Producto
                {
                    Secuencial = item.Secuencial,
                    Codigo = item.Codigo,
                    Descripcion = item.Descripcion,
                    Marca = item.Marca,
                    Cantidad = item.Cantidad,
                    Existencia_Minima = item.Existencia_Minima,
                    Codigo_Barra = item.Codigo_Barra,
                    Codigo_Fabricante = item.Codigo_Fabricante,
                    Fecha_Caducidad = item.Fecha_Caducidad,
                    Precio_Costo = Math.Round((double)item.Precio_Costo, 2),
                    Precio_Venta = Math.Round((double)item.Precio_Venta, 2),
                    Secuencial_Proveedor = item.Secuencial_Proveedor,
                    Secuencial_Categoria = item.Secuencial_Categoria,
                    Expira = Convert.ToBoolean(item.Expira),
                    Tipo = item.Tipo
                };





                if (producto.Cantidad < producto.Existencia_Minima && producto.Tipo == "Producto")
                {
                    dataGridView1.Rows.Add(item.Secuencial,
                  item.Codigo,
                  item.Descripcion,
                      item.Marca,
                  item.Cantidad,
                  item.Existencia_Minima,
                  item.Codigo_Barra,
                  item.Codigo_Fabricante,
                  item.Fecha_Caducidad,
                  Math.Round((double)item.Precio_Costo, 2),
                  Math.Round((double)item.Precio_Venta, 2)


              );

                    lista_cargada.Add(producto); // Agregar a la lista de productos con existencia 0
                }



            }



        }






        public void Filtrar_Items_Lista(string campo, string valor)
        {
            dataGridView2.Visible = false;
            dataGridView1.Rows.Clear();
            flowLayoutPanel1.Controls.Clear();
            dataGridView1.Visible = true;
            Configurar_DataGridView_Inventario();
            flowLayoutPanel1.Visible = false;




            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe








            var productos = context.Productos
                    .Where(c => EF.Property<string>(c, campo).Contains(valor))
                    .ToList();

            dataGridView1.Rows.Clear();
            Configurar_DataGridView_Inventario();





            foreach (var item in productos)
            {





                dataGridView1.Rows.Add(item.Secuencial,
                    item.Codigo,
                    item.Descripcion,
                        item.Marca,
                    item.Cantidad,
                    item.Existencia_Minima,
                    item.Codigo_Barra,
                    item.Codigo_Fabricante,
                    item.Fecha_Caducidad,
                    Math.Round((double)item.Precio_Costo, 2),
                    Math.Round((double)item.Precio_Venta, 2)


                );





            }



        }





        public void Filtrar_Kardex(string campo, string valor)
        {
            dataGridView2.Visible = true;
            dataGridView2.Rows.Clear();
            flowLayoutPanel1.Controls.Clear();
            dataGridView1.Visible = false;
            Configurar_DataGridView_Kardex();
            flowLayoutPanel1.Visible = false;




            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe








            var kardex = context.Kardex
                    .Where(c => EF.Property<string>(c, campo).Contains(valor))
                    .ToList();

            dataGridView2.Rows.Clear();
            Configurar_DataGridView_Kardex();





            foreach (var item in kardex)
            {




                dataGridView2.Rows.Add(item.Secuencial,
                    item.Fecha,
                    item.Descripcion,
                        item.Movimiento,
                        item.Cantidad,
                        Math.Round((double)item.Costo, 2),
                        Math.Round((double)item.Costo_Total, 2),
                        Math.Round((double)item.Venta, 2),
                        Math.Round((double)item.Venta_Total, 2),
                        Math.Round((double)item.Saldo, 2),
                        item.Secuencial_Producto



                );





            }



        }







        public void Configurar_DataGridView_Inventario()
        {
            // Configurar las columnas del DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Secuencial", "S");
            dataGridView1.Columns.Add("Codigo", "Codigo");
            dataGridView1.Columns.Add("Descripcion", "Descripcion");
            dataGridView1.Columns.Add("Marca", "Marca");
            dataGridView1.Columns.Add("Cantidad", "Existencia");
            dataGridView1.Columns.Add("Existencia_Minima", "E.Minima");
            dataGridView1.Columns.Add("Codigo_Barra", "C.Barra");
            dataGridView1.Columns.Add("Codigo_Fabricante", "C.Fabricante");
            dataGridView1.Columns.Add("Fecha_Caducidad", "F.Vencimiento");
            dataGridView1.Columns.Add("Precio_Costo", "P.Costo");
            dataGridView1.Columns.Add("Precio_Venta", "P.Venta");




            dataGridView1.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }





        public void Cargar_Items_Kardex()
        {


            dataGridView2.Rows.Clear();
            flowLayoutPanel1.Controls.Clear();
            dataGridView2.Visible = true;
            Configurar_DataGridView_Inventario();
            flowLayoutPanel1.Visible = false;
            dataGridView1.Visible = false;

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            var kardex = from k in context.Kardex
                         join p in context.Productos on k.Secuencial_Producto equals p.Secuencial
                         select new
                         {
                             Codigo = p.Codigo,
                             Secuencial = k.Secuencial,
                             Fecha = k.Fecha,
                             Descripcion = k.Descripcion,
                             Movimiento = k.Movimiento,
                             Cantidad = k.Cantidad,
                             Costo = Math.Round((double)k.Costo, 2),
                             Costo_Total = Math.Round((double)k.Costo_Total, 2),
                             Venta = Math.Round((double)k.Venta, 2),
                             Venta_Total = Math.Round((double)k.Venta_Total, 2),
                             Saldo = Math.Round((double)k.Saldo, 2),
                             Secuencial_Producto = k.Secuencial_Producto
                         };


            var lista = kardex.ToList();
            // MessageBox.Show($"Registros obtenidos: {lista.Count}");


            foreach (var item in kardex.ToList())
            {
                dataGridView2.Rows.Add(
                    item.Secuencial,
                    item.Codigo,
                    item.Fecha,
                    item.Descripcion,
                    item.Movimiento,
                    item.Cantidad,
                    item.Costo,
                    item.Costo_Total,
                    item.Venta,
                    item.Venta_Total,
                    item.Saldo,
                    item.Secuencial_Producto
                );
            }
        }




        /// <summary>



        public void Filtrar_Items_Kardex_Consulta(string fecha_inicio, string fecha_fin, string codigo, string movimiento)
        {
            /* comboBox3.Items.Clear();
             comboBox3.Items.Add("Codigo");
             comboBox3.Items.Add("Descripcion");
             comboBox3.Items.Add("Fecha");
             comboBox3.Items.Add("Secuencial_Producto");
             comboBox3.SelectedIndex = 0;*/

            dataGridView2.Rows.Clear();
            flowLayoutPanel1.Controls.Clear();
            dataGridView2.Visible = true;
            Configurar_DataGridView_Inventario();
            flowLayoutPanel1.Visible = false;
            dataGridView1.Visible = false;

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Convertir las fechas con TryParse para evitar errores
            if (!DateTime.TryParse(fecha_inicio, out DateTime inicio) ||
                !DateTime.TryParse(fecha_fin, out DateTime fin))
            {
                MessageBox.Show("Las fechas ingresadas no son válidas.");
                return; // Detiene el proceso si la conversión falla
            }



            var kardex = context.Kardex
                .Join(context.Productos,
          k => k.Secuencial_Producto,
          p => p.Secuencial,
          (k, p) => new
          {
              Codigo = p.Codigo,
              Secuencial = k.Secuencial,
              Fecha = k.Fecha,
              Descripcion = k.Descripcion,
              Movimiento = k.Movimiento,
              Cantidad = k.Cantidad,
              Costo = Math.Round((double)k.Costo, 2),
              Costo_Total = Math.Round((double)k.Costo_Total, 2),
              Venta = Math.Round((double)k.Venta, 2),
              Venta_Total = Math.Round((double)k.Venta_Total, 2),
              Saldo = Math.Round((double)k.Saldo, 2),
              Secuencial_Producto = k.Secuencial_Producto
          })
    .AsEnumerable() // **Convierte la consulta a evaluación en memoria**
    .Where(item => DateTime.Parse(item.Fecha) >= inicio && DateTime.Parse(item.Fecha) <= fin
                   && item.Codigo == codigo && item.Movimiento == movimiento)
    .ToList();



            var lista = kardex.ToList();

            if (lista.Count == 0)
            {
                V_Menu_Principal.MSG.ShowMSG("No se encontraron registros que coincidan con los criterios de búsqueda.", "Kardex");
                return; // Detiene el proceso si no hay registros
            }
            else
            {

                V_Menu_Principal.MSG.ShowMSG($"Registros obtenidos: {lista.Count}", "Kardex");
            }
            //  MessageBox.Show($"Registros obtenidos: {lista.Count}");

            foreach (var item in lista)
            {
                dataGridView2.Rows.Add(
                    item.Secuencial,
                    item.Codigo,
                    item.Fecha,
                    item.Descripcion,
                    item.Movimiento,
                    item.Cantidad,
                    item.Costo,
                    item.Costo_Total,
                    item.Venta,
                    item.Venta_Total,
                    item.Saldo,
                    item.Secuencial_Producto
                );
            }
        }


        /// </summary>




        public void Filtrar_Items_Kardex(string campo, string valor)
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("Codigo");
            comboBox3.Items.Add("Descripcion");
            comboBox3.Items.Add("Fecha");
            comboBox3.Items.Add("Secuencial_Producto");
            comboBox3.SelectedIndex = 0;

            dataGridView2.Rows.Clear();
            flowLayoutPanel1.Controls.Clear();
            dataGridView2.Visible = true;
            Configurar_DataGridView_Inventario();
            flowLayoutPanel1.Visible = false;
            dataGridView1.Visible = false;

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe





            var kardex = context.Kardex
                .Join(context.Productos,
          k => k.Secuencial_Producto,
          p => p.Secuencial,
          (k, p) => new
          {
              Codigo = p.Codigo,
              Secuencial = k.Secuencial,
              Fecha = k.Fecha,
              Descripcion = k.Descripcion,
              Movimiento = k.Movimiento,
              Cantidad = k.Cantidad,
              Costo = Math.Round((double)k.Costo, 2),
              Costo_Total = Math.Round((double)k.Costo_Total, 2),
              Venta = Math.Round((double)k.Venta, 2),
              Venta_Total = Math.Round((double)k.Venta_Total, 2),
              Saldo = Math.Round((double)k.Saldo, 2),
              Secuencial_Producto = k.Secuencial_Producto
          })
     .Where(c => EF.Property<string>(c, campo).Contains(valor))
                    .ToList();


            var lista = kardex.ToList();


            foreach (var item in lista)
            {
                dataGridView2.Rows.Add(
                    item.Secuencial,
                    item.Codigo,
                    item.Fecha,
                    item.Descripcion,
                    item.Movimiento,
                    item.Cantidad,
                    item.Costo,
                    item.Costo_Total,
                    item.Venta,
                    item.Venta_Total,
                    item.Saldo,
                    item.Secuencial_Producto
                );
            }
        }





        public void Configurar_DataGridView_Kardex()
        {
            // Configurar las columnas del DataGridView
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("Secuencial", "S");
            dataGridView2.Columns.Add("Codigo", "Codigo");
            dataGridView2.Columns.Add("Fecha", "Fecha");
            dataGridView2.Columns.Add("Descripcion", "Descripcion");
            dataGridView2.Columns.Add("Movimiento", "Movimiento");
            dataGridView2.Columns.Add("Cantidad", "Cantidad");
            dataGridView2.Columns.Add("Costo", "P.Costo");
            dataGridView2.Columns.Add("Costo_Total", "Total P.Costo");
            dataGridView2.Columns.Add("Precio_Venta", "P.Venta");
            dataGridView2.Columns.Add("Venta_Total", "Total P.Venta");
            dataGridView2.Columns.Add("Saldo", "Saldo");
            dataGridView2.Columns.Add("Secuencial_Producto", "SP");








            dataGridView2.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.ReadOnly = true;
        }






        private void Filtrar_Items_Cuadricula(string campo, string valor)
        {

            dataGridView2.Visible = false;
            flowLayoutPanel1.Controls.Clear();
            dataGridView1.Visible = false;
            flowLayoutPanel1.Visible = true;


            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            var productos = context.Productos
                    .Where(c => EF.Property<string>(c, campo).Contains(valor))
                    .ToList();

            flowLayoutPanel1.Controls.Clear();


            foreach (var item in productos)
            {




                Miniatura_Producto miniatura_Producto1 = new Miniatura_Producto();

                miniatura_Producto1.Cantidad = item.Cantidad;
                miniatura_Producto1.Imagen = item.Imagen;
                miniatura_Producto1.Item_Seleccionado.Visible = false; // Asegúrate de que el Item_Seleccionado esté oculto al crear la miniatura
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
                miniatura_Producto1.Secuencial_Usuario = V_Menu_Principal.Secuencial_Usuario;
                miniatura_Producto1.Fecha_Caducidad = item.Fecha_Caducidad;
                miniatura_Producto1.Expira = Convert.ToBoolean(item.Expira);
                miniatura_Producto1.moneda = V_Menu_Principal.moneda; // Asignar la moneda a la miniatura del producto
                miniatura_Producto1.Tipo = item.Tipo; // Asignar el tipo de producto (si es necesario)

                miniatura_Producto1.Item_Imagen.Click += (s, ev) =>
               {
                   miniatura_Producto1.Seleccionado = false;
                   miniatura_Producto1.Item_Seleccionado.Checked = false;
               };



                miniatura_Producto1.Item_Imagen.DoubleClick += async (s, ev) =>
                {



                };



                flowLayoutPanel1.Controls.Add(miniatura_Producto1);



            }

        }










        private Producto Filtrar_Item_Miniatura(string campo, string valor, Producto miniatura)
        {



            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            var productos = context.Productos
                    .Where(c => EF.Property<string>(c, campo).Equals(valor))
                    .ToList();




            foreach (var item in productos)
            {






                miniatura.Cantidad = item.Cantidad;
                miniatura.Imagen = item.Imagen;

                miniatura.Secuencial = item.Secuencial;
                miniatura.Codigo = item.Codigo;
                miniatura.Marca = item.Marca;
                miniatura.Descripcion = item.Descripcion;
                miniatura.Precio_Venta = item.Precio_Venta;
                miniatura.Precio_Costo = item.Precio_Costo;
                miniatura.Existencia_Minima = item.Existencia_Minima;
                miniatura.Codigo_Barra = item.Codigo_Barra;
                miniatura.Codigo_Fabricante = item.Codigo_Fabricante;
                miniatura.Codigo_QR = item.Codigo_QR;
                miniatura.Secuencial_Proveedor = item.Secuencial_Proveedor;
                miniatura.Secuencial_Categoria = item.Secuencial_Categoria;

                miniatura.Fecha_Caducidad = item.Fecha_Caducidad;
                miniatura.Expira = Convert.ToBoolean(item.Expira);

                miniatura.Tipo = item.Tipo; // Asignar el tipo de producto (si es necesario)



            }

            return miniatura; // Retorna el objeto miniatura actualizado con los datos filtrados

        }





        private void button2_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Visible == true)
            {

                Filtrar_Items_Cuadricula("Tipo", "Servicio");

            }

            if (dataGridView1.Visible == true)
            {
                Filtrar_Items_Lista("Tipo", "Servicio");
            }


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (flowLayoutPanel1.Visible == true)
            {

                Filtrar_Items_Cuadricula("Secuencial_Proveedor", comboProveedor.SelectedItem.ToString().Split('-')[0].Trim());

            }

            if (dataGridView1.Visible == true)
            {
                Filtrar_Items_Lista("Secuencial_Proveedor", comboProveedor.SelectedItem.ToString().Split('-')[0].Trim());
            }






        }



        public void llenar_Combo_Proveedor()
        {

            comboProveedor.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            //var proveedores = context.Proveedores.ToList();

            var proveedores = context.Proveedores
    .Where(p => (bool)p.Activo)
    .ToList();






            foreach (var item in proveedores)
            {
                comboProveedor.Items.Add(item.Secuencial + " - " + item.Nombre);



            }


            /* foreach (var item in comboProveedor.Items)
             {
                 if (item.ToString().Contains(this.Secuencial_Proveedor.ToString())) // Verifica si hay un número
                 {
                     comboProveedor.SelectedItem = item;
                     break;
                 }
             }*/





        }


        public void llenar_Combo_Categoria()
        {

            comboCategoria.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            var categorias = context.Categorias.ToList();





            foreach (var item in categorias)
            {
                comboCategoria.Items.Add(item.Secuencial + " - " + item.Nombre);



            }

            foreach (var item in comboCategoria.Items)
            {
                /* if (item.ToString().Contains(this.Secuencial_Categoria.ToString())) // Verifica si hay un número
                 {
                     comboCategoria.SelectedItem = item;
                     break;
                 }*/
            }





        }

        private void V_Inventario_Load(object sender, EventArgs e)
        {
            button4.PerformClick();
            llenar_Combo_Categoria();
            llenar_Combo_Proveedor();
            // Cargar_Items_Cuadricula();
            // comboBox3.SelectedIndex = 0; // Selecciona el primer elemento del comboBox3 por defecto


        }

        private void comboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Visible == true)
            {
                Filtrar_Items_Cuadricula("Secuencial_Categoria", comboCategoria.SelectedItem.ToString().Split('-')[0].Trim());
            }

            if (dataGridView1.Visible == true)
            {
                Filtrar_Items_Lista("Secuencial_Categoria", comboCategoria.SelectedItem.ToString().Split('-')[0].Trim());
            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Visible == true)
            {
                if (textBox1.Text.Length > 0)
                {
                    Filtrar_Items_Cuadricula(comboBox3.SelectedItem.ToString(), textBox1.Text);
                }
                else
                {
                    Cargar_Items_Cuadricula(); // Si el campo de texto está vacío, recargar todos los items
                }
            }


            if (dataGridView1.Visible == true)
            {
                if (textBox1.Text.Length > 0)
                {
                    Filtrar_Items_Lista(comboBox3.SelectedItem.ToString(), textBox1.Text);
                }
                else
                {
                    Cargar_Items_Lista(); // Si el campo de texto está vacío, recargar todos los items
                }
            }


            if (dataGridView2.Visible == true)
            {
                if (textBox1.Text.Length > 0)
                {
                    Filtrar_Items_Kardex(comboBox3.SelectedItem.ToString(), textBox1.Text);
                }
                else
                {
                    Cargar_Items_Kardex(); // Si el campo de texto está vacío, recargar todos los items
                }
            }




        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            if (flowLayoutPanel1.Visible == true)
            {
                Filtrar_Items_Cuadricula("Fecha_Caducidad", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
            }

            if (dataGridView1.Visible == true)
            {
                Filtrar_Items_Lista("Fecha_Caducidad", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
            }

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now; // Reiniciar el DateTimePicker a la fecha actual
            if (flowLayoutPanel1.Visible == true || dataGridView1.Visible == true)
            {

                string fechaSeleccionada = dateTimePicker1.Value.AddDays(7).ToString("dd/MM/yyyy");
                if (flowLayoutPanel1.Visible == true)
                {
                    Filtrar_Items_Cuadricula("Fecha_Caducidad", fechaSeleccionada);
                }

                if (dataGridView1.Visible == true)
                {
                    Filtrar_Items_Lista("Fecha_Caducidad", fechaSeleccionada);
                }

                V_Menu_Principal.MSG.ShowMSG(fechaSeleccionada + "\nSe han filtrado los productos que caducan en los próximos 7 días. ", "Inventario");



            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now; // Reiniciar el DateTimePicker a la fecha actual

            if (flowLayoutPanel1.Visible == true || dataGridView1.Visible == true)
            {


                string fechaSeleccionada = dateTimePicker1.Value.AddMonths(1).ToString("dd/MM/yyyy");

                if (flowLayoutPanel1.Visible == true)
                {
                    Filtrar_Items_Cuadricula("Fecha_Caducidad", fechaSeleccionada);

                }

                if (dataGridView1.Visible == true)
                {
                    Filtrar_Items_Lista("Fecha_Caducidad", fechaSeleccionada);
                }

                V_Menu_Principal.MSG.ShowMSG(fechaSeleccionada + "\nSe han filtrado los productos que caducan en 1 Mes. ", "Inventario");
            }

        }




        private void button7_Click_1(object sender, EventArgs e)
        {

            dateTimePicker1.Value = DateTime.Now; // Reiniciar el DateTimePicker a la fecha actual

            if (flowLayoutPanel1.Visible == true || dataGridView1.Visible == true)
            {

                string fechaSeleccionada = dateTimePicker1.Value.AddYears(1).ToString("dd/MM/yyyy");
                if (flowLayoutPanel1.Visible == true)
                {
                    Filtrar_Items_Cuadricula("Fecha_Caducidad", fechaSeleccionada);

                }
                if (dataGridView1.Visible == true)
                {
                    Filtrar_Items_Lista("Fecha_Caducidad", fechaSeleccionada);
                }

                V_Menu_Principal.MSG.ShowMSG(fechaSeleccionada + "\nSe han filtrado los productos que caducan en 1 Año. ", "Inventario");


            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Visible == true)
            {
                Cargar_Items_Cuadricula();
            }
            else if (dataGridView1.Visible == true)
            {
                Cargar_Items_Lista();
            }

            else if (dataGridView2.Visible == true)
            {
                Cargar_Items_Kardex();
            }

            textBox1.Text = string.Empty; // Limpiar el campo de búsqueda
            pictureBox1.Focus(); // Enfocar el PictureBox para evitar que se pierda el foco del formulario
        }

        private void comboProveedor_Click(object sender, EventArgs e)
        {
            llenar_Combo_Proveedor();
        }

        private void comboCategoria_Click(object sender, EventArgs e)
        {
            llenar_Combo_Categoria();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            V_Producto producto = new V_Producto();
            producto.ShowDialog();

            if (flowLayoutPanel1.Visible == true)
            {
                Cargar_Items_Cuadricula();
            }
            else
            {
                Cargar_Items_Lista();
            }


        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {



            if (dataGridView1.SelectedRows.Count > 0)
            {
                int secuencial = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Secuencial"].Value);
                Producto producto = new Producto();
                producto.Secuencial = secuencial;
                // Filtrar el producto seleccionado
                SQLitePCL.Batteries.Init();
                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated(); // Crea la base de datos si no existe
                var item = context.Productos.Find(secuencial);
                if (item != null)
                {
                    producto.setProducto(item);
                    V_Producto v_producto = new V_Producto(false, producto);
                    v_producto.ShowDialog();

                    if (flowLayoutPanel1.Visible == true)
                    {
                        Cargar_Items_Cuadricula();
                    }
                    else
                    {
                        Cargar_Items_Lista();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("Codigo");
            comboBox3.Items.Add("Descripcion");
            comboBox3.Items.Add("Fecha");

            comboBox3.SelectedIndex = 0;
            label8.Text = "Modo: Kardex";
            groupBox2.Visible = false;
            button2.Visible = false;
            groupBox1.Visible = true;
            dataGridView1.Visible = false;
            flowLayoutPanel1.Visible = false;
            dataGridView2.Visible = true;
            linkLabel1.Visible = false;
            linkLabel2.Visible = false;
            Configurar_DataGridView_Kardex();
            Cargar_Items_Kardex();


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {


            if (dataGridView2.Visible == true)
            {

                if (fecha_inicio.Value != DateTime.MinValue && fecha_fin.Value != DateTime.MinValue)
                {
                    Filtrar_Items_Kardex_Consulta(
                        fecha_inicio.Value.Date.ToString("dd/MM/yyyy"),
                        fecha_fin.Value.Date.ToString("dd/MM/yyyy"),
                        textBox2.Text,
                        comboBox1.SelectedItem?.ToString() ?? ""
                    );
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un rango de fechas válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (flowLayoutPanel1.Visible == true)
            {
                List<Miniatura_Producto> productos = Cargar_Items_Cuadricula();
                Cargar_Existencia_Minima_Cuadricula(productos);
            }

            else if (dataGridView1.Visible == true)
            {
                List<Producto> productos = Cargar_Items_Lista();
                Cargar_Existencia_Minima_Lista(productos);
            }


        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {




            if (flowLayoutPanel1.Visible == true)
            {
                List<Miniatura_Producto> productos = Cargar_Items_Cuadricula();
                Cargar_Agotados_Cuadricula(productos);
            }

            else if (dataGridView1.Visible == true)
            {
                List<Producto> productos = Cargar_Items_Lista();
                Cargar_Agotados_Lista(productos);
            }





        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {






            if (dataGridView2.SelectedRows.Count > 0)
            {
                int secuencial = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["Secuencial"].Value);
                string codigo = dataGridView2.SelectedRows[0].Cells["Codigo"].Value.ToString();
                V_Kardex kardex = new V_Kardex();

                V_Kardex.Secuencial_Producto = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["Secuencial_Producto"].Value);
                V_Kardex.Codigo_Producto = codigo;
                // MessageBox.Show($"Abriendo Kardex del producto: {secuencial} {codigo}", "Kardex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                kardex.ShowDialog();


            }
        }

        private void V_Inventario_FormClosing(object sender, FormClosingEventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
        }
    }//Fin de Clase
}//Fin de Namespace
