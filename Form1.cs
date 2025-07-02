using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Monitux_POS.Clases;
using Monitux_POS.Ventanas;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using static Monitux_POS.Clases.Util;
namespace Monitux_POS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Text = Util.Abrir_Dialogo_Seleccion_URL();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Usb oUsb = new Usb();
            List<USBInfo> lstUSBD = oUsb.GetUSBDevices();

            foreach (USBInfo x in lstUSBD)
            {

                listBox1.Items.Add(x.Description + " ID:" + x.DeviceID);

            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Text = Util.Comprobar_Llave_USB(textBox1.Text).ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {









            // Crear elemento principal del menú
            ToolStripMenuItem menuPrincipal = new ToolStripMenuItem("Opciones");

            // Crear sub ítems
            ToolStripMenuItem subItem1 = new ToolStripMenuItem("Sub Opción 1");
            ToolStripMenuItem subItem2 = new ToolStripMenuItem("Sub Opción 2");

            // Agregar los sub ítems al ítem principal
            menuPrincipal.DropDownItems.Add(subItem1);
            menuPrincipal.DropDownItems.Add(subItem2);

            // Agregar el ítem principal al menú contextual








        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {


            /*
            Miniatura_Producto miniatura_Producto1 = new Miniatura_Producto();

             miniatura_Producto1.Marca = "Marca de prueba";
             miniatura_Producto1.Codigo = "1990";
             miniatura_Producto1.Precio_Venta = 4.5;
             miniatura_Producto1.Comentario = "Comentario de prueba";
             miniatura_Producto1.Secuencial = 1;    
            miniatura_Producto1.Cantidad = 1;
            flowLayoutPanel1.Controls.Add(miniatura_Producto1);


            */



            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe





            //flowLayoutPanel1.Controls.Add(miniatura_Producto1);
            /*

                        // **CREATE**
                        var nuevoProducto = new Producto { Codigo="1990",Secuencial_Proveedor=1,Descripcion = "Agua", Precio_Venta = 4.5 };
                        context.Productos.Add(nuevoProducto);
                        context.SaveChanges();
                        Console.WriteLine("Producto agregado.");

                        */






            /*

            // **UPDATE**

            var producto = context.Productos.FirstOrDefault(p => p.Secuencial == 1);
            if (producto != null)
            {
                producto.Precio_Venta = 500;
                context.SaveChanges();
            }
            */



            /*

            // **DELETE**

            var producto = context.Productos.FirstOrDefault(p => p.Secuencial == 1);
            if (producto != null)
            {
                context.Productos.Remove(producto);
                context.SaveChanges();
            }
            */


            /*


            //Filtrar

            string nombreBuscado = "Agua";
            var productosFiltrados = context.Productos
                                            .Where(p => p.Descripcion.Contains(nombreBuscado))
                                            .ToList();
            foreach (var item in productosFiltrados)
            {
                ImageList imageList = new ImageList();
                imageList.ImageSize = new Size(50, 50); // Tamaño de las imágenes en el ListView
                Miniatura_Producto miniatura_Producto1 = new Miniatura_Producto();
                listBox1.Items.Add($"Secuencial: {item.Secuencial}, Descripcion: {item.Descripcion}, Cantidad: {item.Cantidad}, Imagen: {item.Imagen}");
                miniatura_Producto1.Cantidad = item.Cantidad;
                miniatura_Producto1.Imagen = item.Imagen;
                miniatura_Producto1.Secuencial = item.Secuencial;
                miniatura_Producto1.Codigo = item.Codigo;
                miniatura_Producto1.Marca = item.Marca;
                miniatura_Producto1.Descripcion = item.Descripcion;
                miniatura_Producto1.Precio_Venta = item.Precio_Venta;
                miniatura_Producto1.Precio_Costo = item.Precio_Costo;
                miniatura_Producto1.Existencia_Minima = item.Existencia_Minima;


                imageList.Images.Add(Image.FromFile(miniatura_Producto1.Imagen));


                listView1.LargeImageList = imageList;
                listView1.Items.Add(new ListViewItem(item.Codigo, 0)); // Usa la primera imagen

                
                flowLayoutPanel1.Controls.Add(miniatura_Producto1);
            }

            */







            // **READ**
            var productos = context.Productos.ToList();
            int i = 0;
            Console.WriteLine("Lista de productos:");
            ImageList imageList = new ImageList();
            foreach (var item in productos)
            {


                imageList.ImageSize = new Size(64, 64); // Tamaño de las imágenes en el ListView

                Miniatura_Producto miniatura_Producto1 = new Miniatura_Producto();
                listBox1.Items.Add($"Secuencial: {item.Secuencial}, Descripcion: {item.Descripcion}, Cantidad: {item.Cantidad}, Imagen: {item.Imagen}");
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
                miniatura_Producto1.Secuencial_Usuario = 1;
                miniatura_Producto1.Fecha_Caducidad = item.Fecha_Caducidad;
              
                /* miniatura_Producto1.Item_Imagen.Click += (s, ev) =>
                {
                    
                };*/

                //miniatura_Producto1.vistaEditar = new V_Producto(false,item.getProducto());
                /*
                                if (item.Imagen != "0")
                                {
                                    try {
                                        imageList.Images.Add(Image.FromFile(miniatura_Producto1.Imagen));

                                    }
                                    catch
                                    (Exception ex)
                                    {
                                        
                                        imageList.Images.Add(Image.FromFile("C:\\Users\\Miguel Cerrato\\source\\repos\\Monitux-POS\\f1.png"));
                                    }

                                }


                                else
                                {
                                    //imageList.Images.Add(Image.FromFile(miniatura_Producto1.Imagen));
                                    imageList.Images.Add(Image.FromFile("C:\\Users\\Miguel Cerrato\\source\\repos\\Monitux-POS\\f1.png"));
                                }


                                listView1.LargeImageList = imageList;
                                listView1.Items.Add(new ListViewItem(item.Codigo, i)); // Usa la primera imagen
                                i++;
                */
               
                flowLayoutPanel1.Controls.Add(miniatura_Producto1);
              

            }
            PictureBox xx = new PictureBox();
            xx.Height = miniatura_Producto1.Height;
            xx.Width = miniatura_Producto1.Width;
            xx.BackColor= Color.White;
            xx.Visible = true;
            flowLayoutPanel1.Controls.Add(xx);

            xx.Click += (s, ev) =>
            {
                V_Producto ventana_Producto = new V_Producto();
                ventana_Producto.ShowDialog();
            };

            foreach (var resourceName in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            {
                listBox1.Items.Add(resourceName);




            }





        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {




        }

        private void button7_Click(object sender, EventArgs e)
        {


            Util.Limpiar_Cache(V_Menu_Principal.Secuencial_Empresa);
            Util.Registrar_Actividad(1, "Limpio Cache", V_Menu_Principal.Secuencial_Empresa);

            /*
            listBox1.Items.Add(miniatura_Producto1.cantidadSelecccionItem + "x" + miniatura_Producto1.Descripcion);
            Ventanas.V_Producto ventana_Producto = new Ventanas.V_Producto();
            ventana_Producto.Show();
            this.Text = Util.Convertir_Numeros_Palabras("150.35.1.3l");
            */
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            ImageList imageList = new ImageList();
            imageList.Images.Add(Image.FromFile("C:\\Users\\Miguel Cerrato\\source\\repos\\Monitux-POS\\f1.png"));
            //imageList.Images.Add(Image.FromFile("icono2.png"));

            listView1.LargeImageList = imageList;
            listView1.Items.Add(new ListViewItem("Elemento 1", 0)); // Usa la primera imagen
            //listView1.Items.Add(new ListViewItem("Elemento 2", 1)); // Usa la segunda imagen

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                Miniatura_Producto txtBox = new Miniatura_Producto
                {
                    Text = item.Text,
                    Bounds = item.Bounds,
                    Parent = listView1
                };

                txtBox.LostFocus += (s, ev) =>
                {
                    item.Text = txtBox.Text;
                    //txtBox.Dispose();
                };

                listView1.Controls.Add(txtBox);
                txtBox.Focus();
            }

        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            /*
            if (listView1.SelectedItems.Count > 0)
            {
                
            }*/


        }

        private void button9_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                Miniatura_Producto txtBox = new Miniatura_Producto
                {
                    Text = item.Text,
                    Bounds = item.Bounds,
                    Parent = listView1
                };

                txtBox.LostFocus += (s, ev) =>
                {
                    item.Text = txtBox.Text;
                    txtBox.Dispose();
                };

                listView1.Controls.Add(txtBox);
                txtBox.Focus();
            }

        }





        private void button10_Click(object sender, EventArgs e)
        {


            V_Usuario ventana_Usuario = new V_Usuario();
            ventana_Usuario.ShowDialog();
            // V_Producto ventana_Producto = new V_Producto();
            //ventana_Producto.ShowDialog();

            ///

            //Ojo

            ///

        }
    }
}
