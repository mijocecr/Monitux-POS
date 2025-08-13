using Microsoft.VisualBasic;
using Monitux_POS.Clases;
using Monitux_POS.Ventanas;
using System.Drawing.Imaging;
using System.Security.Policy;

namespace Monitux_POS
{
    public partial class Miniatura_Producto : UserControl
    {

        public string Origen;
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
        public byte[]? Imagen { get; set; }
        public int Secuencial_Categoria { get; set; }
        public double Existencia_Minima { get; set; } = 0;
        public string Tipo { get; set; } = "Producto"; // Tipo de producto, por defecto es "Producto"
        public string? Fecha_Caducidad { get; set; } = null;
        public int Secuencial_Empresa { get; set; }

        //Variables del Control

        public string? moneda { get; set; } // Moneda por defecto, puede ser cambiada en el formulario principal
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
            context.Database.EnsureCreated();

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

                Imagen = item.Imagen; // byte[]
                Secuencial_Categoria = item.Secuencial_Categoria;
                Item_Codigo.Text = item.Codigo;
                Item_Precio.Text = item.Precio_Venta.ToString();
                Secuencial_Proveedor = item.Secuencial_Proveedor;
                Codigo = item.Codigo;
                Existencia_Minima = item.Existencia_Minima;
                Fecha_Caducidad = item.Fecha_Caducidad;
                Expira = item.Expira;
                Item_Moneda.Text = moneda ?? "$";
                Tipo = item.Tipo;

                var comentarioFiltrado = context.Comentarios
                                                .FirstOrDefault(c => c.Secuencial_Producto == item.Secuencial);

                Comentario = comentarioFiltrado?.Contenido ?? "";

                this.Producto = item.getProducto();

                try
                {
                    if (Imagen != null)
                    {
                        using var ms = new MemoryStream(Imagen);
                        Image imagenCargada = Image.FromStream(ms);
                        Item_Imagen.Image = new Bitmap(imagenCargada);
                    }
                }
                catch
                {
                    // Manejo silencioso del error de imagen
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

                producto.Imagen,
                producto.Secuencial_Categoria,
                producto.Fecha_Caducidad,
                producto.Expira,
                producto.Tipo,
                producto.Secuencial_Empresa
            );
        }






        public string Cargar_Comentario()
        {


            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            var comentarioFiltrado = context.Comentarios
                                          .FirstOrDefault(c => c.Secuencial_Producto == Secuencial && Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);
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

            toolTip.SetToolTip(Item_Imagen, "Codigo: " + Codigo + "\nMarca: " + Marca + "\nPrecio: " + Precio_Venta + " " + Item_Moneda.Text + "\nStock: " + Cantidad +
            " -- [Minimo: " + Existencia_Minima + "]" + "\nExpira: " + Fecha_Caducidad + "\n" + Cargar_Comentario());
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
                    this.BackColor = Color.YellowGreen;
                    Item_Codigo.ForeColor = Color.DarkGreen;
                    Item_Precio.ForeColor = Color.DarkGreen;
                }
            }
            else if (Tipo == "Servicio" && Cantidad == 0 && Existencia_Minima == 0)
            {
                this.BackColor = Color.LightGray;
                Item_Codigo.ForeColor = Color.Blue;
                Item_Precio.ForeColor = Color.Blue;
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
                this.BackColor = Color.FromArgb(35, 32, 45);
                //this.BackColor = Color.White;
                Item_Codigo.Font = new Font(Item_Codigo.Font, FontStyle.Regular);
                Item_Codigo.ForeColor = Color.White;
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
            Item_Seleccionado.Checked = Seleccionado;
            Item_Imagen.ContextMenuStrip = Menu;
            Item_Precio.Text = Precio_Venta.ToString();

            try
            {
                if (Imagen != null)
                {
                    using var ms = new MemoryStream(Imagen);
                    Image imagenCargada = Image.FromStream(ms);
                    Item_Imagen.Image = new Bitmap(imagenCargada);
                }
            }
            catch
            {
                // Manejo silencioso del error de imagen
            }

            Item_Codigo.Text = Codigo;
        }




        private void actualizar_Imagen_Local()
        {
            try
            {
                string rutaSeleccionada = Util.Abrir_Dialogo_Seleccion_URL();
                Image original = Image.FromFile(rutaSeleccionada);

                // Clonamos para evitar bloqueo del archivo
                Image clon = new Bitmap(original);
                original.Dispose(); // Libera el archivo original

                // Mostramos en PictureBox
                Item_Imagen.Image = new Bitmap(clon);

                // Comprimimos la imagen
                byte[] imagenComprimida = Util.ComprimirImagen(clon, 50L); // Usa tu método de compresión

                V_Menu_Principal.MSG.ShowMSG("Imagen actualizada con éxito", "Listo");

                SQLitePCL.Batteries.Init();

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var producto = context.Productos.FirstOrDefault(p => p.Secuencial == Secuencial);
                if (producto != null)
                {
                    actualizarItem = true;
                    producto.Imagen = imagenComprimida;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Puedes loguear el error si lo deseas
                return;
            }
        }



        public void Actualizar_Imagen_Camara()
        {
            actualizarItem = true;
            Item_Imagen.Image = null; // Limpiar la imagen actual antes de capturar una nueva

            V_Captura_Imagen ventanaCamara = new V_Captura_Imagen(Secuencial, Codigo);
            ventanaCamara.ShowDialog();
            Item_Imagen.Image = V_Captura_Imagen.Get_Imagen();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var producto = context.Productos.FirstOrDefault(p => p.Secuencial == Secuencial);
            if (producto != null && Item_Imagen.Image != null)
            {
                // Comprimir la imagen capturada
                byte[] imagenComprimida = Util.ComprimirImagen(Item_Imagen.Image, 50L); // Ajusta la calidad si lo deseas

                producto.Imagen = imagenComprimida;
                context.SaveChanges();

                V_Menu_Principal.MSG.ShowMSG("Imagen actualizada con éxito", "Listo");
            }
        }


        private void actualizar_Imagen_Web()
        {
            string url = Interaction.InputBox("Pega la URL de la imagen:", "Imagen desde la web");

            Image imagenWeb = Util.CargarImagenDesdeUrl(url);

            if (imagenWeb != null)
            {
                // Clonamos para evitar bloqueo del archivo
                Image clon = new Bitmap(imagenWeb);
                imagenWeb.Dispose();

                // Mostramos en PictureBox
                Item_Imagen.Image = new Bitmap(clon);

                // Comprimir imagen
                byte[] imagenComprimida = Util.ComprimirImagen(clon, 50L); // Ajusta la calidad si lo deseas

                SQLitePCL.Batteries.Init();

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var producto = context.Productos.FirstOrDefault(p => p.Secuencial == Secuencial);
                if (producto != null)
                {
                    actualizarItem = true;
                    producto.Imagen = imagenComprimida;
                    context.SaveChanges();
                }

                V_Menu_Principal.MSG.ShowMSG("Imagen actualizada con éxito", "Listo");
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

                Imagen = Imagen,
                Secuencial_Categoria = Secuencial_Categoria,
                Existencia_Minima = Existencia_Minima,
                Fecha_Caducidad = Fecha_Caducidad,
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
            string respuesta;

            if (V_Menu_Principal.IPB.Show("Escriba el comentario asociado a: " + Codigo, "Comentario...", out respuesta) == DialogResult.OK)
            {
                respuesta = respuesta?.Trim();
            }

            V_Menu_Principal.MSG.ShowMSG(respuesta, "Comentario");
            actualizarItem = true;
            return respuesta;
        }



        public void Agregar_Comentario(string comentario)
        {
            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe



            var comentarioFiltrado = context.Comentarios
                                             .FirstOrDefault(p => p.Secuencial_Producto == Secuencial && p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

            if (comentarioFiltrado != null)
            {
                comentarioFiltrado.Contenido = comentario;


            }

            ///


            else
            {


                // **CREATE**
                var nuevoComentario = new Comentario { Secuencial_Producto = Secuencial, Contenido = comentario, Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa };
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
            string respuesta = null;

            if (V_Menu_Principal.IPB.Show("Escriba la cantidad en números de unidades a Agregar", "Agregar Unidades", out respuesta) == DialogResult.OK)
            {
                respuesta = respuesta?.Trim();


                if (int.TryParse(respuesta, out int numero))
                {
                    unidadesAgregar = numero;
                    return unidadesAgregar;
                }
                else
                {
                    V_Menu_Principal.MSG.ShowMSG("Error: Solo se permiten números.", "Agregar Unidades");
                    return 0;
                }


            }
            else
            {
                V_Menu_Principal.MSG.ShowMSG("Error: Solo se permiten números.", "Agregar Unidades");

                return 0;
            }



        }




        public double getUnidadesRetirar()
        {


            string respuesta = null;

            if (V_Menu_Principal.IPB.Show("Escriba la cantidad en números de unidades a Retirar", "Retirar Unidades", out respuesta) == DialogResult.OK)
            {
                respuesta = respuesta?.Trim();


                if (int.TryParse(respuesta, out int numero))
                {
                    unidadesRetirar = numero;
                    return unidadesRetirar;
                }
                else
                {
                    V_Menu_Principal.MSG.ShowMSG("Error: Solo se permiten números.", "Retirar Unidades");
                    return 0;

                }


            }
            else
            {
                V_Menu_Principal.MSG.ShowMSG("Error: Solo se permiten números.", "Retirar Unidades");

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
                V_Menu_Principal.MSG.ShowMSG("No se pueden agregar unidades a un servicio.", "Error");
                return;
            }

            //getUnidadesAgregar(Secuencial);   
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe



            // **UPDATE**

            var producto = context.Productos.FirstOrDefault(p => p.Secuencial == Secuencial && p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);
            if (producto != null)
            {
                Util.Registrar_Movimiento_Kardex(producto.Secuencial, producto.Cantidad, producto.Descripcion, getUnidadesAgregar(), producto.Precio_Costo, producto.Precio_Venta, "Entrada", V_Menu_Principal.Secuencial_Empresa);

                producto.Cantidad = Cantidad + unidadesAgregar;
                context.SaveChanges();
                V_Menu_Principal.MSG.ShowMSG("Se han agregado " + unidadesAgregar + " unidades al producto: " + Codigo, "Agregar Unidades");
                actualizarItem = true;


                Util.Registrar_Actividad(Secuencial_Usuario, "Ha agregado " + unidadesAgregar + " unidades al producto: " + Codigo, V_Menu_Principal.Secuencial_Empresa);
            }



        }





        public void Actualizar_Producto_Retirar_Unidades()
        {


            if (Tipo == "Servicio")
            {
                V_Menu_Principal.MSG.ShowMSG("No se pueden retirar unidades a un servicio.", "Error");
                return;
            }

            //getUnidadesAgregar(Secuencial);   
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe



            // **UPDATE**

            var producto = context.Productos.FirstOrDefault(p => p.Secuencial == Secuencial && p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);
            if (producto != null)
            {

                Util.Registrar_Movimiento_Kardex(producto.Secuencial, producto.Cantidad, producto.Descripcion, getUnidadesRetirar(), producto.Precio_Costo, producto.Precio_Venta, "Salida", V_Menu_Principal.Secuencial_Empresa);

                producto.Cantidad = Cantidad - unidadesRetirar;
                context.SaveChanges();
                V_Menu_Principal.MSG.ShowMSG("Se han retirado " + unidadesRetirar + " unidades al producto: " + Codigo, "Retirar Unidades");
                actualizarItem = true;
                Util.Registrar_Actividad(Secuencial_Usuario, "Ha retirado " + unidadesRetirar + " unidades al producto: " + Codigo, V_Menu_Principal.Secuencial_Empresa);
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



        public void cargarVistaEditar(string origen) //Indicar desde donde se abre esta vista
        {

            /*
             1. V_Producto form = new V_Producto(false,this.getProducto);
                Aquí estoy creando una instancia del formulario de producto (V_Producto) 
                y le estamos pasando dos parametros el identificador único (Secuencial) y 
                el producto que se quiere editar. Así ese formulario puede precargar los datos del
                producto correspondiente.


            2. FormPrincipal principal = this.FindForm() as FormPrincipal;
               Esta línea se ejecuta desde dentro de un control, en este caso, el Miniatura_Producto.
               this.FindForm() busca el formulario al que pertenece ese control 
               (es decir, el formulario principal que contiene el FlowLayoutPanel).

               El as FormPrincipal intenta convertirlo al tipo FormPrincipal, y si funciona, 
               ya puedes interactuar con él.
               Es como decir: “Busca el formulario contenedor y dime si es del tipo principal que conozco”.

            3. El if (principal != null)
               Esto simplemente confirma que el control está efectivamente dentro del FormPrincipal, 
               para evitar errores de conversión o referencias nulas.

            4. form.OnProductoEditado += () => principal.Cargar_Items(...);
               Aquí es donde ocurre la magia de la comunicación entre formularios:

               Estás diciendo: “Cuando el formulario de edición (form) dispare su evento OnProductoEditado, 
               quiero que el formulario principal (principal) recargue los productos llamando a Cargar_Items(...)”

               El += indica que estás suscribiendo una acción (un método anónimo tipo lambda) al evento.

            > Ese evento OnProductoEditado debe estar declarado en el formulario de edición así: 

            > > csharp > public event Action OnProductoEditado; >
            
            Y cuando se edite y guarde correctamente el producto, en el formulario secundario llamas a
              OnProductoEditado?.Invoke();
             */

            if (origen == "Factura_Venta")
            {

                V_Producto form = new V_Producto(false, this.getProducto());
                V_Factura_Venta principal = this.FindForm() as V_Factura_Venta;

                if (principal != null)
                {
                    form.OnProductoEditado += () => principal.Cargar_Items(); // pasa el objeto que usas normalmente }

                }

                form.ShowDialog();

            }



            else if (origen == "Factura_Compra")
            {

                V_Producto form = new V_Producto(false, this.getProducto());
                V_Factura_Compra principal = this.FindForm() as V_Factura_Compra;

                if (principal != null)
                {
                    form.OnProductoEditado += () => principal.Cargar_Items(); // pasa el objeto que usas normalmente }

                }

                form.ShowDialog();

            }




            else if (origen == "Inventario")
            {

                V_Producto form = new V_Producto(false, this.getProducto());
                V_Inventario principal = this.FindForm() as V_Inventario;

                if (principal != null)
                {
                    form.OnProductoEditado += () => principal.Cargar_Items_Cuadricula(); // pasa el objeto que usas normalmente }

                }

                form.ShowDialog();

            }



            else if (origen == "Editar_Factura_Venta")
            {

                V_Producto form = new V_Producto(false, this.getProducto());
                V_Editar_Factura_Venta principal = this.FindForm() as V_Editar_Factura_Venta;

                if (principal != null)
                {
                    form.OnProductoEditado += () => principal.Cargar_Items(); // pasa el objeto que usas normalmente }

                }

                form.ShowDialog();

            }



            else if (origen == "Editar_Factura_Compra")
            {

                V_Producto form = new V_Producto(false, this.getProducto());
                V_Editar_Factura_Compra principal = this.FindForm() as V_Editar_Factura_Compra;

                if (principal != null)
                {
                    form.OnProductoEditado += () => principal.Cargar_Items(); // pasa el objeto que usas normalmente }

                }

                form.ShowDialog();

            }





            else
            {
                V_Menu_Principal.MSG.ShowMSG("Origen no reconocido para la edición del producto.", "Error de Origen");
            }





        }


        public void cargarVista_Ampliada()
        {
            try
            {
                V_Vista_Ampliada v_Vista_Ampliada = new V_Vista_Ampliada(Imagen, Codigo, Descripcion);
                v_Vista_Ampliada.ShowDialog();

            }
            catch
            {
                V_Menu_Principal.MSG.ShowMSG("Error al cargar la vista ampliada.", "Error");
                return;
            }


        }



        private void editarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {




            cargarVistaEditar(Origen);


        }

        private void Miniatura_Producto_Load_1(object sender, EventArgs e)
        {
            Item_Moneda.Text = moneda ?? "$"; // Asignar la moneda por defecto si no se ha establecido
        }

        private void Item_Imagen_MouseLeave(object sender, EventArgs e)
        {

            if (Seleccionado != true)
            {
                this.BorderStyle = BorderStyle.FixedSingle;
                this.BackColor = Color.FromArgb(35, 32, 45);
                //this.BackColor = Color.White;
                Item_Codigo.Font = new Font(Item_Codigo.Font, FontStyle.Regular);
                Item_Codigo.ForeColor = Color.White;
                Item_Precio.ForeColor = Item_Codigo.ForeColor;

            }

        }

        private void camaraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actualizar_Imagen_Camara();

        }

        private void vistaAmpliadaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ampliarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                cargarVista_Ampliada();
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG("Error al cargar la vista ampliada: " + ex.Message, "Error");
                return;
            }
        }

        private void Menu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Item_Precio_Click(object sender, EventArgs e)
        {

        }

        private void Item_Precio_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Item_Precio_MouseEnter(object sender, EventArgs e)
        {
            
        }
    }//Fin de Clase
}
