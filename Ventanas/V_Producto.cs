

using Monitux_POS.Clases;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

using static Monitux_POS.Ventanas.V_Producto;


using ZXing.Windows.Compatibility;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;




namespace Monitux_POS.Ventanas
{


    public partial class V_Producto : Form
    {

        
        public Producto producto { get; set; } = new Producto();
        public int Secuencial_Usuario { get; set; } = 0;

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
              if(Vista_producto.Fecha_Caducidad!="No Caduca" && Vista_producto.Fecha_Caducidad !=null)
                {

                    dateTimePicker1.Value = DateTime.ParseExact(Vista_producto.Fecha_Caducidad, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                }
              /*  else
                {
                    dateTimePicker1.Value = DateTime.Today;
                }*/




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
                        //MessageBox.Show("Error al cargar la imagen: " + ex.Message);
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
    .Where(p => (bool)p.Activo)
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


            var categorias = context.Categorias.ToList();




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


            if (esNuevo == true)
            {
                Menu_Eliminar.Visible = false;
            }
         


        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image?.Dispose();
            System.GC.Collect();

            this.Close();
            System.GC.Collect();
        }


        private void Menu_Agregar_Click(object sender, EventArgs e)
        {
        }











        private void Menu_Guardar_Click(object sender, EventArgs e)
        {
            if (this.esNuevo == false)
            {



                // **UPDATE**
                SQLitePCL.Batteries.Init();

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated(); // Crea la base de datos si no existe


                var producto = context.Productos.FirstOrDefault(p => p.Secuencial == this.Secuencial);
                if (producto != null)
                {
                    if (Fecha_Caducidad!="No Caduca")
                    {
                        producto.Fecha_Caducidad = dateTimePicker1.Value.Date.ToShortDateString();
                    }
                    
                    MessageBox.Show(dateTimePicker1.Value.Date.ToShortDateString());
                    producto.Precio_Venta = double.Parse(txtPrecioVenta.Text);
                    producto.Precio_Costo = double.Parse(txtPrecioCosto.Text);
                    producto.Codigo = txtCodigo.Text;
                    producto.Descripcion = txtDescripcion.Text;
                    producto.Cantidad = double.Parse(txtCantidad.Text);
                    producto.Marca = txtMarca.Text;
                    producto.Codigo_Barra = txtCodigoBarra.Text;
                    producto.Codigo_Fabricante = txtCodigoFabricante.Text;
                    producto.Codigo_QR =
                    producto.Imagen = this.Imagen; // Ruta de la imagen del producto
                    producto.Existencia_Minima = double.Parse(txtExistenciaMinima.Text);
                    producto.Codigo_QR = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\QR\\" + "QR-" + producto.Secuencial + ".PNG");


                    try
                    {

                        producto.Secuencial_Categoria = int.Parse(comboCategoria.SelectedItem.ToString().Substring(0, comboCategoria.SelectedItem.ToString().IndexOf("-")));

                    }
                    catch
                    {
                        MessageBox.Show("Debe seleccionar una categoría válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        if (comboProveedor.SelectedItem == null)
                        {
                            MessageBox.Show("Debe seleccionar un proveedor válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        producto.Secuencial_Proveedor = int.Parse(comboProveedor.SelectedItem.ToString().Substring(0, comboProveedor.SelectedItem.ToString().IndexOf("-")));

                    }
                    catch
                    {
                        MessageBox.Show("Debe seleccionar un proveedor válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }





                    context.SaveChanges();


                }


                MessageBox.Show("Producto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Util.Registrar_Actividad(Secuencial_Usuario, "Ha modificado el producto: " + producto.Codigo);
                this.Dispose();





            }


            else
            {
                SQLitePCL.Batteries.Init();

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated(); // Crea la base de datos si no existe

                // **CREATE**
                var nuevoProducto = new Producto();
                try
                {

                    if (dateTimePicker1.Value.Date != DateTime.Today)
                    {
                        nuevoProducto.Fecha_Caducidad = dateTimePicker1.Value.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        nuevoProducto.Fecha_Caducidad = "No Caduca";
                    }



                        nuevoProducto.Precio_Venta = double.Parse(txtPrecioVenta.Text);
                    nuevoProducto.Precio_Costo = double.Parse(txtPrecioCosto.Text);
                    nuevoProducto.Codigo = txtCodigo.Text;
                    nuevoProducto.Descripcion = txtDescripcion.Text;
                    nuevoProducto.Cantidad = double.Parse(txtCantidad.Text);
                    nuevoProducto.Marca = txtMarca.Text;
                    nuevoProducto.Codigo_Barra = txtCodigoBarra.Text;
                    nuevoProducto.Codigo_Fabricante = txtCodigoFabricante.Text;
                    nuevoProducto.Codigo_QR = pictureBox2.Image != null ? Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\QR\\" + "QR-" + Secuencial + ".PNG") : string.Empty; // Ruta del código QR   
                    nuevoProducto.Imagen = this.Imagen; // Ruta de la imagen del producto
                    nuevoProducto.Existencia_Minima = double.Parse(txtExistenciaMinima.Text);
                }
                catch
                {

                    MessageBox.Show("Debe completar todos los campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {

                    nuevoProducto.Secuencial_Categoria = int.Parse(comboCategoria.SelectedItem.ToString().Substring(0, comboCategoria.SelectedItem.ToString().IndexOf("-")));

                }
                catch
                {
                    MessageBox.Show("Debe seleccionar una categoría válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    nuevoProducto.Secuencial_Proveedor = int.Parse(comboProveedor.SelectedItem.ToString().Substring(0, comboProveedor.SelectedItem.ToString().IndexOf("-")));

                }
                catch
                {
                    MessageBox.Show("Debe seleccionar un proveedor válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }



                context.Productos.Add(nuevoProducto);
                context.SaveChanges();

                Util.Registrar_Actividad(Secuencial_Usuario, "Ha creado el producto: " + txtCodigo.Text);

                MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                this.Dispose();
                



            }

            ///



            ////



        }






        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void txtCodigoBarra_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigoBarra.Text != string.Empty)
            {
                pictureBox3.Image?.Dispose(); // Liberar la imagen anterior si existe
                pictureBox3.Image = Util.Generar_Codigo_Barra(Secuencial, txtCodigoBarra.Text);
                pictureBox3.Image.Save(Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\BC\\BC-" + Secuencial + ".PNG"));
            }
        }

        private void archivoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string mensaje_QR = "Codigo: " + txtCodigo.Text + "\nDescripcion: " + txtDescripcion.Text + "\nPrecio Venta: " + txtPrecioVenta.Text + "\nMarca: " + txtMarca.Text + "\nCodigo Barra: " + txtCodigoBarra.Text + "\nCodigo Fabricante: " + txtCodigoFabricante.Text + "\nStock Minimo: " + txtExistenciaMinima.Text;
            pictureBox2.Image?.Dispose();
            pictureBox2.Image = Util.Generar_Codigo_QR(Secuencial, mensaje_QR);
            pictureBox2.Image.Save(Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\QR\\QR-" + Secuencial + ".PNG"));

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //pictureBox2.Image = Util.Generar_Codigo_QR(Secuencial, txtCodigoBarra.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            string rutaGuardado = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Imagenes\\" + Secuencial + "-" + txtCodigo.Text + ".PNG");


            try
            {
                string Imagen_Seleccionada = Util.Abrir_Dialogo_Seleccion_URL();
                if (Imagen_Seleccionada != "")
                {
                    this.Imagen = Imagen_Seleccionada;
                    pictureBox1.Load(Imagen);

                    pictureBox1.Image.Save(rutaGuardado);
                    this.Imagen = rutaGuardado;
                }



            }
            catch { }


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

           


            var res = MessageBox.Show("¿Está seguro de eliminar este producto?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                        context.Productos.Remove(producto);
                        context.SaveChanges();

                        Util.Registrar_Actividad(Secuencial_Usuario, "Ha eliminado el producto: " + producto.Codigo);

                        MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("El producto no existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch
                {

                    // MessageBox.Show($"Error al eliminar el producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
