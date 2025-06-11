using Microsoft.VisualBasic;
using Monitux_POS.Clases;
using Monitux_POS.Ventanas;
using System.Drawing.Imaging;

namespace Monitux_POS
{
    public partial class Miniatura_Producto : UserControl
    {
        public bool Expira { get; set; } 
        public Producto Producto { get; set; }
        public int Secuencial { get; set; }
        public int Secuencial_Proveedor { get; set; }
        public int Secuencial_Usuario { get; set; } = 0;
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public double Cantidad { get; set; }
        public double Precio_Costo { get; set; }
        public double Precio_Venta { get; set; }
        public string? Marca { get; set; }
        public string? Codigo_Barra { get; set; }
        public string? Codigo_Fabricante { get; set; }
        public string? Codigo_QR { get; set; }
        public string? Imagen { get; set; }
        public int Secuencial_Categoria { get; set; }
        public double Existencia_Minima { get; set; } = 0;
        public string Tipo { get; set; } = "Producto"; // Tipo de producto, por defecto es "Producto"
        public string? Fecha_Caducidad { get; set; } = "No Expira";

        //Variables del Control

        public string ?moneda { get; set; } // Moneda por defecto, puede ser cambiada en el formulario principal
        public bool actualizarItem { get; set; }
        




        System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
        public Form vista { get; set; }
        public V_Producto vistaEditar { get; set; }

        public double unidadesAgregar { get; set; }
        public double unidadesRetirar { get; set; }
        public double cantidadSelecccionItem { get; set; } = 0;

        public bool Seleccionado { get; set; } = false;
        public string Comentario { get; set; } = "";


        public event EventHandler CustomClick;

        public void SimularClick()
        {
            CustomClick?.Invoke(this, EventArgs.Empty); // Dispara el evento manualmente
        }


        public Miniatura_Producto()
        {
            InitializeComponent();
        }

        private void Miniatura_Producto_Load(object sender, EventArgs e)
        {
            Item_Imagen.ContextMenuStrip = Menu;

            Set_Item();


            actualizarItem = false;
        }

        #region Funciones Publicas




        public void Set_Item()
        {




            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // MessageBox.Show(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Monitux_POS.Datos.monitux.db"));



            //Filtrar



            int secuencialItem = Secuencial;


            var productosFiltrados = context.Productos
                                            .Where(p => p.Secuencial.Equals(secuencialItem))
                                            .ToList();
            foreach (var item in productosFiltrados)
            {
                Descripcion = item.Descripcion;
                Cantidad = item.Cantidad;
                Precio_Costo = item.Precio_Costo;
                Precio_Venta = item.Precio_Venta;
                Marca = item.Marca;
                Codigo_Barra = item.Codigo_Barra;
                Codigo_Fabricante = item.Codigo_Fabricante;
                Codigo_QR = item.Codigo_QR;
                Imagen = item.Imagen;
                Secuencial_Categoria = item.Secuencial_Categoria;
                Item_Codigo.Text = item.Codigo;
                Item_Precio.Text = item.Precio_Venta.ToString();
                Secuencial_Proveedor = item.Secuencial_Proveedor;
                Codigo = item.Codigo;
                Existencia_Minima = item.Existencia_Minima;
                Fecha_Caducidad = item.Fecha_Caducidad;
                Expira = item.Expira; // Asignar el valor de Expira desde el producto
                Item_Moneda.Text = moneda ?? "$"; // Asignar la moneda por defecto si no se ha establecido
                Tipo = item.Tipo; // Asignar el tipo de producto (Producto o Servicio)
                var comentarioFiltrado = context.Comentarios
                                              .FirstOrDefault(c => c.Secuencial_Producto == item.Secuencial);

                if (comentarioFiltrado != null)
                {
                    Comentario = comentarioFiltrado.Contenido;

                }

                else
                {
                    Comentario = "";
                }


                this.Producto = item.getProducto();

                try
                {
                    Item_Imagen.Load(Imagen);


                }
                catch
                {



                    // MessageBox.Show("Error al cargar la imagen: " + Imagen, "Error de Carga");
                }


                actualizarItem = false;



            }







        }





        #endregion






        public Producto getProducto(Miniatura_Producto producto)
        {
            return new Producto(
                producto.Secuencial,
                producto.Secuencial_Proveedor,
                producto.Codigo,
                producto.Descripcion,
                producto.Cantidad,
                producto.Precio_Costo,
                producto.Precio_Venta,
                producto.Marca,
                producto.Codigo_Barra,
                producto.Codigo_Fabricante,
                producto.Codigo_QR,
                producto.Imagen,
                producto.Secuencial_Categoria,
                producto.Fecha_Caducidad,
                producto.Expira,
                producto.Tipo
            );
        }






        public string Cargar_Comentario()
        {


            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            var comentarioFiltrado = context.Comentarios
                                          .FirstOrDefault(c => c.Secuencial_Producto == Secuencial);
            string comentario = "";

            if (comentarioFiltrado != null)
            {
                comentario = comentarioFiltrado.Contenido;

            }

            else
            {
                comentario = "";
            }

            return comentario;
        }

        private void Item_Imagen_MouseLeave(object sender, EventArgs e)
        {



            ///


            if (Seleccionado != true)
            {
                this.BorderStyle = BorderStyle.FixedSingle;
                this.BackColor = Control.DefaultBackColor;
                //this.BackColor = Color.White;
                Item_Codigo.Font = new Font(Item_Codigo.Font, FontStyle.Regular);
                Item_Codigo.ForeColor = Color.Black;
                Item_Precio.ForeColor = Item_Codigo.ForeColor;
            }

            //this.BackColor = Color.White;

        }





        private void Item_Imagen_MouseHover(object sender, EventArgs e)
        {


            if (actualizarItem == true)
            {
                Set_Item();
            }




            if (Seleccionado != true)
            {
                AplicarFormato();
               
            }

                toolTip.SetToolTip(Item_Imagen, "Codigo: " + Codigo + "\nMarca: " + Marca + "\nPrecio: " + Precio_Venta +" "+Item_Moneda.Text + "\nStock: " + Cantidad +
                " -- [Minimo: " + Existencia_Minima + "]" + "\nExpira: " + Fecha_Caducidad +"\n"+ Cargar_Comentario());
        }




       


        private void AplicarFormato()
        {
            Item_Codigo.Text = Codigo;
            Item_Precio.Text = Precio_Venta.ToString();
            Item_Codigo.Font = new Font(Item_Codigo.Font, FontStyle.Bold);
            Item_Precio.ForeColor = Item_Codigo.ForeColor;

            if (Tipo == "Producto")
            {
                if (Cantidad < Existencia_Minima)
                {
                    this.BackColor = Color.Red;
                    Item_Codigo.ForeColor = Color.White;
                    Item_Precio.ForeColor = Color.White;
                }
                else if (Cantidad <= Existencia_Minima)
                {
                    this.BackColor = Color.Yellow;
                    Item_Codigo.ForeColor = Color.Coral;
                    Item_Precio.ForeColor = Color.Coral;
                }
                else
                {
                    this.BackColor = Color.LightGreen;
                    Item_Codigo.ForeColor = Color.BlueViolet;
                    Item_Precio.ForeColor = Color.BlueViolet;
                }
            }
            else if (Tipo == "Servicio" && Cantidad == 0 && Existencia_Minima == 0)
            {
                this.BackColor = Color.LightGray;
                Item_Codigo.ForeColor = Color.Blue;
                Item_Precio.ForeColor = Color.White;
            }
        }






        private void Item_Imagen_Click(object sender, EventArgs e)
        {
            if (Item_Seleccionado.Checked == true)
            {
                Item_Seleccionado.Checked = false;

            }
            else
            {
                Item_Seleccionado.Checked = true;
            }





        }

        private void Miniatura_Producto_MouseLeave(object sender, EventArgs e)
        {
            if (Seleccionado != true)
            {
                this.BorderStyle = BorderStyle.FixedSingle;
                this.BackColor = Control.DefaultBackColor;
                //this.BackColor = Color.White;
                Item_Codigo.Font = new Font(Item_Codigo.Font, FontStyle.Regular);
                Item_Codigo.ForeColor = Color.Black;
                Item_Precio.ForeColor = Item_Codigo.ForeColor;
            }
        }




        private void Item_Seleccionado_CheckedChanged(object sender, EventArgs e)
        {
            

            if (Item_Seleccionado.Checked == true)
            {
                Seleccionado = true;
                this.BackColor = Color.LightBlue;
                //numericUpDown1.Visible = true;


            }

            else
            {
                Seleccionado = false;
               
            }
        }

        private void Miniatura_Producto_Paint(object sender, PaintEventArgs e)
        {


            //Set_Item();

            Item_Seleccionado.Checked = Seleccionado;
            Item_Imagen.ContextMenuStrip = Menu;
            Item_Precio.Text = Precio_Venta.ToString();
            try
            {
                Item_Imagen.Load(Imagen);

            }

            catch { }

            Item_Codigo.Text = Codigo;
        }



        private void actualizar_Imagen_Local()
        {


            string rutaGuardado = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Imagenes\\" + Secuencial + "-" + Codigo + ".PNG");

            try
            {
                Imagen = Util.Abrir_Dialogo_Seleccion_URL();
                Item_Imagen.Load(Imagen);

                Item_Imagen.Image.Save(rutaGuardado, ImageFormat.Png);
                Imagen = rutaGuardado;


            }
            catch (Exception ex)
            {
                //  MessageBox.Show("Error al guardar la imagen: " + ex.Message, "Error de Guardado");
                return;
            }
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // **UPDATE**

            var producto = context.Productos.FirstOrDefault(p => p.Secuencial == Secuencial);
            if (producto != null)
            {


                actualizarItem = true;

                Item_Imagen.Image.Save(rutaGuardado, ImageFormat.Png);
                producto.Imagen = rutaGuardado;
                context.SaveChanges();
            }


        }



        private void actualizar_Imagen_Web()
        {








            string rutaGuardado = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Imagenes\\WEB-" + Secuencial + "-" + Codigo + ".PNG");

            try
            {
                Imagen = Interaction.InputBox("Pegue aqui la direccion de la imagen a la que estara asociada a este producto:", "Imagen Web");
                Item_Imagen.Load(Imagen);

                Item_Imagen.Image.Save(rutaGuardado, ImageFormat.Png);
                Imagen = rutaGuardado;


            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error al guardar la imagen: " + ex.Message, "Error de Guardado");
                return;
            }
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // **UPDATE**

            var producto = context.Productos.FirstOrDefault(p => p.Secuencial == Secuencial);
            if (producto != null)
            {


                actualizarItem = true;

                Item_Imagen.Image.Save(rutaGuardado, ImageFormat.Png);
                producto.Imagen = rutaGuardado;
                context.SaveChanges();
            }








        }



        public Producto getProducto()
        {
            return new Producto
            {
                Secuencial = Secuencial,
                Secuencial_Proveedor = Secuencial_Proveedor,
                Codigo = Codigo,
                Descripcion = Descripcion,
                Cantidad = Cantidad,
                Precio_Costo = Precio_Costo,
                Precio_Venta = Precio_Venta,
                Marca = Marca,
                Codigo_Barra = Codigo_Barra,
                Codigo_Fabricante = Codigo_Fabricante,
                Codigo_QR = Codigo_QR,
                Imagen = Imagen,
                Secuencial_Categoria = Secuencial_Categoria,
                Existencia_Minima = Existencia_Minima,
                Fecha_Caducidad=Fecha_Caducidad,
                Expira = Expira,
                Tipo = Tipo

            };
        }




        private void imagenLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {


            actualizar_Imagen_Local();
            actualizarItem = true;
        }


        public string getComentario()
        {
            string respuesta = Interaction.InputBox("Escriba el comentario que estara asociado a este producto:", "Comentario");
            MessageBox.Show(respuesta, "Comentario");
            actualizarItem = true;
            return respuesta;
        }



        public void Agregar_Comentario(string comentario)
        {
            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe



            var comentarioFiltrado = context.Comentarios
                                             .FirstOrDefault(p => p.Secuencial_Producto == Secuencial);

            if (comentarioFiltrado != null)
            {
                comentarioFiltrado.Contenido = comentario;


            }

            ///


            else
            {


                // **CREATE**
                var nuevoComentario = new Comentario { Secuencial_Producto = Secuencial, Contenido = comentario };
                context.Comentarios.Add(nuevoComentario);

                Console.WriteLine("Comentario agregado.");

            }

            context.SaveChanges();






        }



        private void agregarComentarioToolStripMenuItem_Click(object sender, EventArgs e)
        {


            Agregar_Comentario(getComentario());
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
           // cantidadSelecccionItem = (int)numericUpDown1.Value;

        }

        public double getUnidadesAgregar()
        {
            string respuesta = Interaction.InputBox("Escriba la cantidad en numeros a agregar de este producto:", "Agregar Unidades");
            // MessageBox.Show(respuesta, "Agregar Unidades");
            ///

            if (int.TryParse(respuesta, out int numero))
            {
                unidadesAgregar = numero;

                //Cantidad = Cantidad + unidadesAgregar;

                // actualizarItem = true;
                return unidadesAgregar;

            }
            else
            {
                MessageBox.Show("Error: Solo se permiten números.", "Agregar Unidades");

                return 0;
            }

            ///

        }



        public double getUnidadesRetirar()
        {
            string respuesta = Interaction.InputBox("Escriba la cantidad en numeros a retirar de este producto:", "Retirar Unidades");



            if (int.TryParse(respuesta, out int numero))
            {
                unidadesRetirar = numero;




                return unidadesRetirar;

            }
            else
            {
                MessageBox.Show("Error: Solo se permiten números.", "Retirar Unidades");

                return 0;
            }



        }




        private void agregarUnidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Actualizar_Producto_Agregar_Unidades();
        }

        public void Actualizar_Producto_Agregar_Unidades()
        {

            if (Tipo == "Servicio")
            {
                                MessageBox.Show("No se pueden agregar unidades a un servicio.", "Error");
                return;
            }

            //getUnidadesAgregar(Secuencial);   
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            //MessageBox.Show(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Monitux_POS.Datos.monitux.db"));

            // **UPDATE**

            var producto = context.Productos.FirstOrDefault(p => p.Secuencial == Secuencial);
            if (producto != null)
            {
                Util.Registrar_Movimiento_Kardex(producto.Secuencial, producto.Cantidad, producto.Descripcion, getUnidadesAgregar(), producto.Precio_Costo, producto.Precio_Venta, "Entrada");

                producto.Cantidad = Cantidad + unidadesAgregar;
                context.SaveChanges();
                MessageBox.Show("Se han agregado " + unidadesAgregar + " unidades al producto: " + Codigo, "Agregar Unidades");
                actualizarItem = true;
                

                Util.Registrar_Actividad(Secuencial_Usuario, "Ha agregado " + unidadesAgregar + " unidades al producto: " + Codigo);
            }



        }





        public void Actualizar_Producto_Retirar_Unidades()
        {


            if (Tipo == "Servicio")
            {
                MessageBox.Show("No se pueden retirar unidades a un servicio.", "Error");
                return;
            }

            //getUnidadesAgregar(Secuencial);   
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            //  MessageBox.Show(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Monitux_POS.Datos.monitux.db"));

            // **UPDATE**

            var producto = context.Productos.FirstOrDefault(p => p.Secuencial == Secuencial);
            if (producto != null)
            {

                Util.Registrar_Movimiento_Kardex(producto.Secuencial, producto.Cantidad, producto.Descripcion, getUnidadesRetirar(), producto.Precio_Costo, producto.Precio_Venta, "Salida");

                producto.Cantidad = Cantidad - unidadesRetirar;
                context.SaveChanges();
                MessageBox.Show("Se han retirado " + unidadesRetirar + " unidades al producto: " + Codigo, "Retirar Unidades");
                actualizarItem = true;
                Util.Registrar_Actividad(Secuencial_Usuario, "Ha retirado " + unidadesRetirar + " unidades al producto: " + Codigo);
            }



        }




        private void retirarUnidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Actualizar_Producto_Retirar_Unidades();
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cambiarImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }




        private void imagenWebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actualizar_Imagen_Web();

        }



        public void cargarVistaEditar() // Cambiar esto
        {




            Form vistaEditar = new V_Producto(false, this.getProducto());
            vistaEditar.ShowDialog();



        }




        private void editarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {




            cargarVistaEditar();


        }

        private void Miniatura_Producto_Load_1(object sender, EventArgs e)
        {
            Item_Moneda.Text= moneda ?? "$"; // Asignar la moneda por defecto si no se ha establecido
        }
    }//Fin de Clase
}
