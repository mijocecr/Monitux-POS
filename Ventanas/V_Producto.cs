

using Monitux_POS.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;
using static Monitux_POS.Ventanas.V_Producto;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;




namespace Monitux_POS.Ventanas
{


    public partial class V_Producto : Form
    {


        public Producto producto { get; set; } = new Producto();
        public int Secuencial_Usuario { get; set; } = V_Menu_Principal.Secuencial_Usuario;

        public int Secuencial { get; set; }
        public int Secuencial_Proveedor { get; set; } = 0;
        public int Secuencial_Categoria { get; set; } = 0;

        public string Fecha_Caducidad { get; set; }

        bool esNuevo = true;

        public string Imagen { get; set; } = string.Empty; // Ruta de la imagen del producto

        public V_Producto()
        {
            InitializeComponent();
        }

        public V_Producto(bool esNuevo, Producto Vista_producto)
        {
            this.esNuevo = esNuevo;
            InitializeComponent();
            if (esNuevo == false)
            {

                txtCantidad.Enabled = false; // Deshabilitar el campo Cantidad para edición
                txtCodigo.Text = Vista_producto.Codigo;
                txtDescripcion.Text = Vista_producto.Descripcion;
                txtCantidad.Text = Vista_producto.Cantidad.ToString();
                txtPrecioCosto.Text = Vista_producto.Precio_Costo.ToString();
                txtPrecioVenta.Text = Vista_producto.Precio_Venta.ToString();
                txtMarca.Text = Vista_producto.Marca ?? string.Empty;
                txtCodigoBarra.Text = Vista_producto.Codigo_Barra;
                txtCodigoFabricante.Text = Vista_producto.Codigo_Fabricante ?? string.Empty;
                Fecha_Caducidad = Vista_producto.Fecha_Caducidad;
                Secuencial = Vista_producto.Secuencial;
                Secuencial_Proveedor = Vista_producto.Secuencial_Proveedor;
                Secuencial_Categoria = Vista_producto.Secuencial_Categoria;
                txtExistenciaMinima.Text = Vista_producto.Existencia_Minima.ToString();
                checkBox1.Checked = Vista_producto.Expira; // Marcar el checkbox según el estado de expiración del producto
                comboBox1.SelectedItem = Vista_producto.Tipo; // Asignar el tipo de producto al ComboBox
                // Marcar el checkbox según el estado de expiración del producto
                comboBox1.Enabled = false;
                if (Vista_producto.Expira != false && Vista_producto.Fecha_Caducidad != "No Expira")
                {
                    checkBox1.Checked = true; // Marcar el checkbox si el producto tiene fecha de caducidad
                    dateTimePicker1.Value = DateTime.ParseExact(Vista_producto.Fecha_Caducidad, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                }





                llenar_Combo_Proveedor();
                llenar_Combo_Categoria();






                // Menu_Agregar.Visible = esNuevo;
                // Menu_Eliminar.Visible = esNuevo;
                try
                {
                    pictureBox2.Image = Vista_producto.Codigo_QR != null ? Image.FromFile(Vista_producto.Codigo_QR) : null; // Asignar la imagen del código QR si existe
                }
                catch { }
                Imagen = Vista_producto.Imagen ?? string.Empty; // Asignar la ruta de la imagen del producto

                if (Vista_producto.Imagen != null)
                {
                    try
                    {
                        pictureBox1.Image = Image.FromFile(Vista_producto.Imagen);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    pictureBox1.Image = null;
                }



            }
            else
            {
                Secuencial = -1;
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
    .Where(p => (bool)p.Activo && p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
    .ToList();


            if (Secuencial != -1)
            {



                foreach (var item in proveedores)
                {
                    comboProveedor.Items.Add(item.Secuencial + " - " + item.Nombre);



                }


                foreach (var item in comboProveedor.Items)
                {
                    if (item.ToString().Contains(this.Secuencial_Proveedor.ToString())) // Verifica si hay un número
                    {
                        comboProveedor.SelectedItem = item;
                        break;
                    }
                }

            }








        }


        public void llenar_Combo_Categoria()
        {

            comboCategoria.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            var categorias = context.Categorias
    .Where(c => c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
    .ToList();




            if (Secuencial != -1)
            {




                foreach (var item in categorias)
                {
                    comboCategoria.Items.Add(item.Secuencial + " - " + item.Nombre);



                }

                foreach (var item in comboCategoria.Items)
                {
                    if (item.ToString().Contains(this.Secuencial_Categoria.ToString())) // Verifica si hay un número
                    {
                        comboCategoria.SelectedItem = item;
                        break;
                    }
                }



            }






        }


        private void V_Producto_Load(object sender, EventArgs e)
        {


            txtCantidad.KeyPress += txtCantidad_KeyPress;
            txtPrecioCosto.KeyPress += txtPrecioCosto_KeyPress;
            txtPrecioVenta.KeyPress += txtPrecioVenta_KeyPress;
            txtExistenciaMinima.KeyPress += txtExistenciaMinima_KeyPress;




            if (V_Menu_Principal.Acceso_Usuario != "Administrador")
            {
                Menu_Eliminar.Visible = false; // Oculta el botón de eliminar si el usuario no es administrador
            }
            else
            {
                Menu_Eliminar.Visible = true; // Muestra el botón de eliminar si el usuario es administrador
            }



            this.Text = "Monitux-POS v." + V_Menu_Principal.VER; // Establece el título del formulario

            if (esNuevo == true)
            {
                Menu_Eliminar.Visible = false;
            }



        }

        private void TxtPrecioCosto_KeyPress(object? sender, KeyPressEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pictureBox1.Image?.Dispose();
            System.GC.Collect();

            Util.Limpiar_Cache(V_Menu_Principal.Secuencial_Empresa); // Limpiar la caché de imágenes y otros recursos





            this.Close();

        }


        private void Menu_Agregar_Click(object sender, EventArgs e)
        {
        }






        public event Action OnProductoEditado;





        public void Menu_Guardar_Click(object sender, EventArgs e)
        {

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            // Validaciones iniciales
            bool esServicio = comboBox1.SelectedItem?.ToString() == "Servicio";
            bool tipoSeleccionado = comboBox1.SelectedIndex != -1;
            bool proveedorValido = comboProveedor.SelectedItem != null;
            bool categoriaValida = comboCategoria.SelectedItem != null;

            if (!tipoSeleccionado)
            {
                V_Menu_Principal.MSG.ShowMSG("Debe seleccionar un tipo de producto válido.", "Error");
                return;
            }

            if (!esServicio && (!proveedorValido || !categoriaValida))
            {
                V_Menu_Principal.MSG.ShowMSG("Debe seleccionar un proveedor y una categoría válidos.", "Error");
                return;
            }

            // Verificar si ya existe el producto
            Producto productoExistente = context.Productos
                .FirstOrDefault(p => p.Codigo == txtCodigo.Text && p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

            bool esNuevo = productoExistente == null;
            Producto producto = esNuevo ? new Producto() : productoExistente;

            // Asignar datos
            producto.Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa;
            producto.Codigo = txtCodigo.Text;
            producto.Descripcion = txtDescripcion.Text;
            producto.Marca = txtMarca.Text;
            producto.Codigo_Barra = txtCodigoBarra.Text;
            producto.Codigo_Fabricante = txtCodigoFabricante.Text;
            producto.Tipo = comboBox1.SelectedItem?.ToString();
            producto.Expira = checkBox1.Checked;
            producto.Fecha_Caducidad = checkBox1.Checked ? dateTimePicker1.Value.ToString("dd/MM/yyyy") : "No Expira";

            double.TryParse(txtPrecioVenta.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double precioVenta);
            double.TryParse(txtPrecioCosto.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double precioCosto);
            producto.Precio_Venta = precioVenta;
            producto.Precio_Costo = precioCosto;

            if (!esServicio)
            {
                double.TryParse(txtCantidad.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double cantidad);
                producto.Cantidad = cantidad;

                double.TryParse(txtExistenciaMinima.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double existenciaMinima);
                producto.Existencia_Minima = existenciaMinima;

                producto.Secuencial_Categoria = int.Parse(comboCategoria.SelectedItem.ToString().Split('-')[0].Trim());
                producto.Secuencial_Proveedor = int.Parse(comboProveedor.SelectedItem.ToString().Split('-')[0].Trim());
            }
            else
            {
                producto.Cantidad = 0;
                producto.Existencia_Minima = 0;
                producto.Secuencial_Categoria = 0;
                producto.Secuencial_Proveedor = 0;
            }

            try
            {
                // Si es nuevo, agregar
                if (esNuevo)
                    context.Productos.Add(producto);

                context.SaveChanges();

                // Recargar el objeto para tener el Secuencial asignado
                context.Entry(producto).Reload();

                // Asignar ruta QR
                producto.Codigo_QR = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "QR", $"{producto.Secuencial_Empresa}-QR-{producto.Secuencial}.PNG");

                // Guardar imagen renombrada si existe una temporal
                if (!string.IsNullOrWhiteSpace(this.Imagen) && File.Exists(this.Imagen))
                {
                    string rutaFinal = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "Resources",
                        "Imagenes",
                        $"{producto.Secuencial_Empresa}-{producto.Secuencial}-{producto.Codigo}.PNG"
                    );

                    File.Move(this.Imagen, rutaFinal);
                    producto.Imagen = rutaFinal;
                }

                context.SaveChanges(); // Guarda QR e imagen

                // Registrar movimiento si es nuevo y no es servicio
                if (esNuevo && !esServicio)
                {
                    Util.Registrar_Movimiento_Kardex(
                        producto.Secuencial,
                        producto.Cantidad,
                        producto.Descripcion,
                        producto.Cantidad,
                        producto.Precio_Costo,
                        producto.Precio_Venta,
                        "Entrada",
                        producto.Secuencial_Empresa
                    );
                }

                MessageBox.Show("Secuencial: " + producto.Secuencial.ToString(), "Información");

                V_Menu_Principal.MSG.ShowMSG(esNuevo ? "Producto creado correctamente." : "Producto actualizado correctamente.", "Éxito");
                Util.Registrar_Actividad(Secuencial_Usuario, $"Ha {(esNuevo ? "creado" : "modificado")} el producto: {producto.Codigo}", producto.Secuencial_Empresa);

                this.Dispose();
                OnProductoEditado?.Invoke();
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Error al guardar el producto:\n{ex.Message}", "Error");
            }



            /*

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            // Validaciones iniciales
            bool esServicio = comboBox1.SelectedItem?.ToString() == "Servicio";
            bool tipoSeleccionado = comboBox1.SelectedIndex != -1;
            bool proveedorValido = comboProveedor.SelectedItem != null;
            bool categoriaValida = comboCategoria.SelectedItem != null;

            if (!tipoSeleccionado)
            {
                V_Menu_Principal.MSG.ShowMSG("Debe seleccionar un tipo de producto válido.", "Error");
                return;
            }

            if (!esServicio && (!proveedorValido || !categoriaValida))
            {
                V_Menu_Principal.MSG.ShowMSG("Debe seleccionar un proveedor y una categoría válidos.", "Error");
                return;
            }

            // Inicializar producto
            bool esNuevo = this.esNuevo;
            int secuencial = esNuevo
                ? context.Productos.Any() ? context.Productos.Max(p => p.Secuencial) + 1 : 1
                : this.Secuencial;

            Producto producto = esNuevo
                ? new Producto()
                : context.Productos.FirstOrDefault(p => p.Secuencial == secuencial);

            if (producto == null)
            {
                V_Menu_Principal.MSG.ShowMSG("Producto no encontrado.", "Error");
                return;
            }

            // Asignación común
            producto.Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa;

            MessageBox.Show("Secuencial: " + secuencial.ToString());    
            producto.Secuencial = secuencial;
            producto.Codigo = txtCodigo.Text;
            producto.Descripcion = txtDescripcion.Text;
            producto.Marca = txtMarca.Text;
            producto.Codigo_Barra = txtCodigoBarra.Text;
            producto.Codigo_Fabricante = txtCodigoFabricante.Text;
            producto.Imagen = this.Imagen;
            producto.Tipo = comboBox1.SelectedItem?.ToString();
            producto.Expira = checkBox1.Checked;
            producto.Fecha_Caducidad = producto.Expira
                ? dateTimePicker1.Value.ToString("dd/MM/yyyy")
                : "No Expira";
            producto.Codigo_QR = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "QR", $"{V_Menu_Principal.Secuencial_Empresa}-QR-{producto.Secuencial}.PNG");

            // Conversión segura de precios
            double.TryParse(txtPrecioVenta.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double precioVenta);
            double.TryParse(txtPrecioCosto.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double precioCosto);
            producto.Precio_Venta = precioVenta;
            producto.Precio_Costo = precioCosto;

            // Si no es servicio, asignar inventario
            if (!esServicio)
            {
                double.TryParse(txtCantidad.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double cantidad);
                producto.Cantidad = cantidad;

                double.TryParse(txtExistenciaMinima.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double existenciaMinima);
                producto.Existencia_Minima = existenciaMinima;

                producto.Secuencial_Categoria = int.Parse(comboCategoria.SelectedItem.ToString().Split('-')[0].Trim());
                producto.Secuencial_Proveedor = int.Parse(comboProveedor.SelectedItem.ToString().Split('-')[0].Trim());

                if (esNuevo)
                {
                    Util.Registrar_Movimiento_Kardex(
                        producto.Secuencial,
                        producto.Cantidad,
                        producto.Descripcion,
                        0,
                        producto.Precio_Costo,
                        producto.Precio_Venta,
                        "Entrada",
                        V_Menu_Principal.Secuencial_Empresa
                    );
                }
            }
            else
            {
                producto.Cantidad = 0;
                producto.Existencia_Minima = 0;
                producto.Secuencial_Categoria = 0;
                producto.Secuencial_Proveedor = 0;
            }

            // Guardado final
            try
            {
                if (esNuevo)
                    context.Productos.Add(producto);

                context.SaveChanges();

                string accion = esNuevo ? "creado" : "modificado";
                string mensaje = $"Producto {accion} correctamente.";
                string log = $"Ha {accion} el producto: {producto.Codigo}";

                V_Menu_Principal.MSG.ShowMSG(mensaje, "Éxito");
                Util.Registrar_Actividad(Secuencial_Usuario, log, V_Menu_Principal.Secuencial_Empresa);

                this.Dispose();
                OnProductoEditado?.Invoke();
            }
            catch
            {
                V_Menu_Principal.MSG.ShowMSG("Error al guardar el producto: Ya existe o los datos no son válidos.", "Error");
            }

            */



        }






        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void txtCodigoBarra_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigoBarra.Text != string.Empty)
            {
                pictureBox3.Image?.Dispose(); // Liberar la imagen anterior si existe
                pictureBox3.Image = Util.Generar_Codigo_Barra(Secuencial, txtCodigoBarra.Text, V_Menu_Principal.Secuencial_Empresa);
                pictureBox3.Image.Save(Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\BC\\" + V_Menu_Principal.Secuencial_Empresa + "-BC-" + Secuencial + ".PNG"));
            }
        }

        private void archivoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string mensaje_QR = "Codigo: " + txtCodigo.Text + "\nDescripcion: " + txtDescripcion.Text + "\nPrecio Venta: " + txtPrecioVenta.Text + "\nMarca: " + txtMarca.Text + "\nCodigo Barra: " + txtCodigoBarra.Text + "\nCodigo Fabricante: " + txtCodigoFabricante.Text + "\nStock Minimo: " + txtExistenciaMinima.Text;
            pictureBox2.Image?.Dispose();
            pictureBox2.Image = Util.Generar_Codigo_QR(Secuencial, mensaje_QR, V_Menu_Principal.Secuencial_Empresa);
            pictureBox2.Image.Save(Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\QR\\" + V_Menu_Principal.Secuencial_Empresa + "-QR-" + Secuencial + ".PNG"));

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //pictureBox2.Image = Util.Generar_Codigo_QR(Secuencial, txtCodigoBarra.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {


          
            try
            {
                // Seleccionar imagen
                string imagenSeleccionada = Util.Abrir_Dialogo_Seleccion_URL();
                if (!string.IsNullOrWhiteSpace(imagenSeleccionada))
                {
                    this.Imagen = imagenSeleccionada;

                    // Cargar imagen en el PictureBox
                    pictureBox1.Image = Util.Cargar_Imagen_Local(imagenSeleccionada);

                    // Guardar con nombre temporal para renombrar después del SaveChanges
                    string rutaTemporal = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "Resources",
                        "Imagenes",
                        "imagen-temporal.png"
                    );

                    pictureBox1.Image.Save(rutaTemporal);
                    this.Imagen = rutaTemporal;
                }
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Error al seleccionar/guardar la imagen:\n{ex.Message}", "Error");
            }
        




            /*
            string rutaGuardado = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Imagenes\\" + Secuencial + "-" + txtCodigo.Text + ".PNG");


            try
            {
                string Imagen_Seleccionada = Util.Abrir_Dialogo_Seleccion_URL();
                if (Imagen_Seleccionada != "")
                {
                    this.Imagen = Imagen_Seleccionada;
                    pictureBox1.Image = Util.Cargar_Imagen_Local(Imagen);

                    pictureBox1.Image.Save(rutaGuardado);
                    this.Imagen = rutaGuardado;
                }



            }
            catch { }

            */
        }

        private void comboProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {







        }

        private void comboProveedor_SelectedValueChanged(object sender, EventArgs e)
        {



        }

        private void comboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboProveedor_DropDown(object sender, EventArgs e)
        {

        }

        private void comboProveedor_MouseClick(object sender, MouseEventArgs e)
        {
            llenar_Combo_Proveedor();
        }

        private void comboCategoria_MouseClick(object sender, MouseEventArgs e)
        {
            llenar_Combo_Categoria();
        }

        private void Menu_Eliminar_Click(object sender, EventArgs e)
        {




            var res = V_Menu_Principal.MSG.ShowMSG("¿Está seguro de eliminar este producto?", "Confirmación");

            if (res == DialogResult.Yes)
            {
                try
                {
                    SQLitePCL.Batteries.Init();

                    using var context = new Monitux_DB_Context();
                    context.Database.EnsureCreated(); // Crea la base de datos si no existe

                    pictureBox1.Image?.Dispose();
                    pictureBox2.Image?.Dispose();
                    pictureBox3.Image?.Dispose();


                    var producto = context.Productos.FirstOrDefault(p => p.Secuencial == this.Secuencial);

                    if (producto != null)
                    {
                        string rutaArchivo = producto.Imagen;
                        string rutaArchivo1 = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\BC\\BC-" + producto.Secuencial + "-" + producto.Codigo_Barra + ".PNG");
                        string rutaArchivo2 = producto.Codigo_QR;

                        Util.Registrar_Actividad(Secuencial_Usuario, "Ha eliminado el producto: " + producto.Codigo, V_Menu_Principal.Secuencial_Empresa);

                        if (producto.Tipo != "Servicio")
                        {

                            Util.Registrar_Movimiento_Kardex(producto.Secuencial, producto.Cantidad, producto.Descripcion, producto.Cantidad,
                                producto.Precio_Costo, producto.Precio_Venta, "Salida", V_Menu_Principal.Secuencial_Empresa);

                        }
                        context.Productos.Remove(producto);
                        context.SaveChanges();



                        V_Menu_Principal.MSG.ShowMSG("Producto eliminado correctamente.", "Éxito");

                        OnProductoEditado?.Invoke();

                        this.Dispose();
                    }
                    else
                    {
                        V_Menu_Principal.MSG.ShowMSG("El producto no existe en la base de datos.", "Error");
                    }
                }
                catch
                {


                }
            }






        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            V_Categoria v_Categoria = new V_Categoria();
            v_Categoria.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {



        }

        private void comboCategoria_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            llenar_Combo_Categoria();
        }

        private void comboCategoria_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }

        private void comboCategoria_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void comboCategoria_MouseClick_1(object sender, MouseEventArgs e)
        {
            llenar_Combo_Categoria();
        }

        private void nuevoProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            V_Proveedor proveedor = new V_Proveedor();
            proveedor.ShowDialog();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Fecha_Caducidad = dateTimePicker1.Value.ToShortDateString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Visible = checkBox1.Checked; // Mostrar u ocultar el DateTimePicker según el estado del CheckBox
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                checkBox1.Checked = false;
                checkBox1.Enabled = false; // Deshabilitar el checkbox si el tipo es "Servicio"
                comboCategoria.Enabled = false; // Deshabilitar el ComboBox de categoría si el tipo es "Servicio"
                txtCodigoFabricante.Text = "No Aplica"; // Establecer el campo Código Fabricante a "No Aplica" si el tipo es "Servicio"

                comboProveedor.Enabled = false; // Deshabilitar el ComboBox de proveedor si el tipo es "Servicio"
                txtCantidad.Enabled = false; // Deshabilitar el campo Cantidad si el tipo es "Servicio"
                txtExistenciaMinima.Text = "0"; // Establecer la existencia mínima a 0 si el tipo es "Servicio"
                txtExistenciaMinima.Enabled = false; // Deshabilitar el campo de existencia mínima si el tipo es "Servicio"
                txtCantidad.Text = "0"; // Establecer la cantidad a 0 si el tipo es "Servicio"

                txtMarca.Enabled = false; // Deshabilitar el campo Marca si el tipo es "Servicio"
                txtCodigoFabricante.Enabled = false; // Deshabilitar el campo Código Fabricante si el tipo es "Servicio"
                txtExistenciaMinima.Text = "0"; // Establecer la existencia mínima a 0 si el tipo es "Servicio"
            }
            else
            {


                //checkBox1.Checked = false;
                checkBox1.Enabled = true; // Deshabilitar el checkbox si el tipo es "Servicio"
                comboCategoria.Enabled = true; // Deshabilitar el ComboBox de categoría si el tipo es "Servicio"


                comboProveedor.Enabled = true; // Deshabilitar el ComboBox de proveedor si el tipo es "Servicio"

                if (Secuencial == 0)
                {
                    txtCantidad.Enabled = true; // Deshabilitar el campo Cantidad si el tipo es "Servicio"

                }

                txtExistenciaMinima.Enabled = true; // Deshabilitar el campo de existencia mínima si el tipo es "Servicio"


                txtMarca.Enabled = true; // Deshabilitar el campo Marca si el tipo es "Servicio"
                txtCodigoFabricante.Enabled = true; // Deshabilitar el campo Código Fabricante si el tipo es "Servicio"




            }


        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            V_Captura_Imagen v_Captura_Imagen = new V_Captura_Imagen();
            v_Captura_Imagen.ShowDialog();
            Image imagenCapturada = V_Captura_Imagen.Get_Imagen();
            if (imagenCapturada != null)
            {
                pictureBox1.Image?.Dispose(); // Liberar la imagen anterior si existe
                pictureBox1.Image = imagenCapturada; // Asignar la imagen capturada al PictureBox
                Imagen = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Imagenes\\" + Secuencial + "-" + txtCodigo.Text + ".PNG");
                pictureBox1.Image.Save(Imagen); // Guardar la imagen en la ruta especificada
            }
            else
            {
                V_Menu_Principal.MSG.ShowMSG("No se pudo capturar la imagen.", "Error");
            }

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Permitir solo dígitos, retroceso y punto
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Bloquea el carácter
            }

            // Solo un punto decimal permitido
            if (e.KeyChar == '.' && (sender as System.Windows.Forms.TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }

        }

        private void txtPrecioCosto_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Permitir solo dígitos, retroceso y punto
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Bloquea el carácter
            }

            // Solo un punto decimal permitido
            if (e.KeyChar == '.' && (sender as System.Windows.Forms.TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }

        }

        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Permitir solo dígitos, retroceso y punto
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Bloquea el carácter
            }

            // Solo un punto decimal permitido
            if (e.KeyChar == '.' && (sender as System.Windows.Forms.TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }

        }

        private void txtExistenciaMinima_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Permitir solo dígitos, retroceso y punto
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloquea el carácter
            }


        }

        private void txtPrecioCosto_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtExistenciaMinima_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
