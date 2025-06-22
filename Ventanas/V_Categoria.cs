using Microsoft.EntityFrameworkCore;
using Monitux_POS.Clases;
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
    public partial class V_Categoria : Form
    {
        public int Secuencial_Usuario { get; set; } = V_Menu_Principal.Secuencial_Usuario;
        int Secuencial = 0; // Variable para almacenar el secuencial de la categoria seleccionada
        string Imagen = ""; // Variable para almacenar la imagen de la categoria seleccionada
        public V_Categoria()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Dispose();
        }

        private void V_Categoria_Load(object sender, EventArgs e)
        {


            if (V_Menu_Principal.Acceso_Usuario != "Administrador")
            {
                eliminarToolStripMenuItem.Visible = false; // Oculta el botón de eliminar si el usuario no es administrador
            }
            else
            {
                eliminarToolStripMenuItem.Visible = true; // Muestra el botón de eliminar si el usuario es administrador
            }


            Cargar_Datos(); // Carga los datos al iniciar el formulario
            comboBox1.Items.Add("Nombre");
            comboBox1.Items.Add("Descripcion");
            comboBox1.SelectedIndex = 0; // Selecciona el primer elemento por defecto
            dataGridView1.ReadOnly = true; // Hace que el DataGridView sea de solo lectura
            this.Text = "Monitux-POS v." + V_Menu_Principal.VER; // Establece el título del formulario
        }


        private void Cargar_Datos()
        {

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            var categorias = context.Categorias.ToList();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selecciona toda la fila
            dataGridView1.Columns.Add("Secuencial", "S");
            dataGridView1.Columns["Secuencial"].Width = 20; // Ajusta el ancho de la columna Secuencial
            dataGridView1.Columns.Add("Nombre", "Nombre");
            dataGridView1.Columns["Nombre"].Width = 80; // Ajusta el ancho de la columna Nombre
            dataGridView1.Columns.Add("Descripcion", "Descripcion");
            dataGridView1.Columns["Descripcion"].Width = 250; // Ajusta el ancho de la columna Descripcion
            dataGridView1.Columns.Add("Imagen", "Imagen");
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            foreach (var item in categorias)
            {
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Nombre,
                    item.Descripcion,
                    item.Imagen ?? "No Imagen" // Maneja el caso donde Imagen sea null

                );


            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value != null)
                {
                    this.Secuencial = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value);
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value != null)
                {
                    txtNombre.Text = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Descripcion"].Value != null)
                {
                    txtDescripcion.Text = dataGridView1.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value != null &&
                    !string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value.ToString()))
                {
                    try
                    {
                        pictureBox1.Image = Util.Cargar_Imagen_Local(dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value.ToString());
                    }
                    catch
                    {

                        pictureBox1.Image = null; // Si no se puede cargar la imagen, establece la imagen como nula
                    }

                }
                else
                {
                    Secuencial = 0;
                    pictureBox1.Image = null; // Si no se puede cargar la imagen, establece la imagen como nula
                    txtNombre.Text = "";
                    txtDescripcion.Text = "";

                }
            }
            catch (Exception ex)
            {

                pictureBox1.Image = null;
            }


        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {



            var res = V_Menu_Principal.MSG.ShowMSG("¿Está seguro de eliminar esta categoria?", "Confirmación");

            if (res == DialogResult.Yes)
            {
                // **DELETE**
                try
                {
                    if (pictureBox1.Image != null)
                        pictureBox1.Image.Dispose(); // Libera la imagen del PictureBox antes de eliminarla
                }
                catch { }

                SQLitePCL.Batteries.Init();

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated(); // Crea la base de datos si no existe

                var categoria = context.Categorias.FirstOrDefault(p => p.Secuencial == this.Secuencial);
                if (categoria != null)
                {
                    context.Categorias.Remove(categoria);
                    context.SaveChanges();
                    Util.Registrar_Actividad(Secuencial_Usuario, "Ha eliminado la categoria: " + categoria.Nombre);
                    V_Menu_Principal.MSG.ShowMSG("Categoria eliminada correctamente.", "Éxito");
                    Cargar_Datos();
                }
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Secuencial != 0)
            {



                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El nombre de la categoria no puede estar vacío.", "Error");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("La descripción de la categoria no puede estar vacía.", "Error");
                    return;
                }
                // **UPDATE**
                SQLitePCL.Batteries.Init();

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated(); // Crea la base de datos si no existe


                var categoria = context.Categorias.FirstOrDefault(p => p.Secuencial == this.Secuencial);
                if (categoria != null)
                {
                    categoria.Nombre = txtNombre.Text;
                    categoria.Descripcion = txtDescripcion.Text;
                    categoria.Imagen = Imagen;
                    context.SaveChanges();
                    Util.Registrar_Actividad(Secuencial_Usuario, "Ha modificado la categoria: " + categoria.Nombre);
                    V_Menu_Principal.MSG.ShowMSG("Categoria actualizada correctamente.", "Éxito");
                    Cargar_Datos(); // Recarga los datos para mostrar la categoria actualizada
                }


            }
            else
            {

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El nombre de la categoria no puede estar vacío.", "Error");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("La descripción de la categoria no puede estar vacía.", "Error");
                    return;
                }
                // **Create**
                SQLitePCL.Batteries.Init();

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated(); // Crea la base de datos si no existe

                var categoria = new Categoria();
                categoria.Secuencial = context.Categorias.Any() ? context.Categorias.Max(c => c.Secuencial) + 1 : 1; // Asigna un nuevo secuencial
                categoria.Nombre = txtNombre.Text;
                categoria.Descripcion = txtDescripcion.Text;
                if (pictureBox1.Image != null)
                {
                    categoria.Imagen = Imagen;
                }
                else
                {
                    categoria.Imagen = "Sin Imagen"; // Asigna una imagen por defecto si no se ha seleccionado una imagen
                }


                context.Categorias.Add(categoria);
                context.SaveChanges();
                Util.Registrar_Actividad(Secuencial_Usuario, "Ha creado la categoria: " + txtNombre.Text);
                V_Menu_Principal.MSG.ShowMSG("Categoria creada correctamente.", "Éxito");
                Cargar_Datos(); // Recarga los datos para mostrar la nueva categoria


            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            string rutaGuardado = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\CAT\\Cat - " + Secuencial + ".PNG");


            try
            {
                string Imagen_Seleccionada = Util.Abrir_Dialogo_Seleccion_URL();
                if (Imagen_Seleccionada != "")
                {
                    Imagen = Imagen_Seleccionada;
                    pictureBox1.Image = Util.Cargar_Imagen_Local(Imagen);

                    pictureBox1.Image.Save(rutaGuardado);
                    this.Imagen = rutaGuardado;
                }



            }
            catch { }

        }

        private void nuevoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Secuencial = 0; // Reinicia el secuencial para una nueva categoria
            txtNombre.Text = ""; // Limpia el campo de nombre
            txtDescripcion.Text = ""; // Limpia el campo de descripción 
            pictureBox1.Image = null; // Limpia la imagen del PictureBox
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            //  if (textBox1.Text == "")
            // {

            //   Cargar_Datos();
            //}
            //else
            //{

            Filtrar(comboBox1.SelectedItem.ToString(), textBox1.Text);

            //}
        }


        private void Filtrar(string campo, string valor)
        {





            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar categorías antes de agregarlas al DataGridView
            /*  string filtro = "eeee"; // Define el criterio de búsqueda
              var categoriasFiltradas = context.Categorias
                  .Where(c => c.Nombre.Contains(filtro)) // Aplica filtro en la consulta
                  .ToList();*/





            //-------------------Filtro que usare



            string columnaSeleccionada = campo; // Cambia esto a la columna que desees filtrar

            var categoriasFiltradas = context.Categorias
                    .Where(c => EF.Property<string>(c, columnaSeleccionada).Contains(valor))
                    .ToList();

            dataGridView1.Rows.Clear();
            foreach (var item in categoriasFiltradas)
            {
                dataGridView1.Rows.Add(item.Secuencial, item.Nombre, item.Descripcion, item.Imagen ?? "No Imagen");
            }


            //-------------------Filtro que usare






            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selecciona toda la fila

            // Agregar columnas si no existen
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("Secuencial", "S");
                dataGridView1.Columns["Secuencial"].Width = 20;
                dataGridView1.Columns.Add("Nombre", "Nombre");
                dataGridView1.Columns["Nombre"].Width = 80;
                dataGridView1.Columns.Add("Descripcion", "Descripcion");
                dataGridView1.Columns["Descripcion"].Width = 250;
                dataGridView1.Columns.Add("Imagen", "Imagen");
            }

            // Limpiar filas antes de agregar nuevas
            dataGridView1.Rows.Clear();

            foreach (var item in categoriasFiltradas)
            {
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Nombre,
                    item.Descripcion,
                    item.Imagen ?? "No Imagen" // Maneja el caso donde Imagen sea null
                );
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {




        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void opcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {





        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value != null)
                {
                    this.Secuencial = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value);
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value != null)
                {
                    txtNombre.Text = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Descripcion"].Value != null)
                {
                    txtDescripcion.Text = dataGridView1.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value != null &&
                    !string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value.ToString()))
                {
                    try
                    {
                        pictureBox1.Image = Util.Cargar_Imagen_Local(dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value.ToString());
                        Imagen = dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value.ToString(); // Guarda la imagen seleccionada
                    }
                    catch
                    {

                        pictureBox1.Image = null; // Si no se puede cargar la imagen, establece la imagen como nula
                    }

                }
                else
                {
                    Secuencial = 0;
                    pictureBox1.Image = null; // Si no se puede cargar la imagen, establece la imagen como nula
                    txtNombre.Text = "";
                    txtDescripcion.Text = "";

                }
            }
            catch (Exception ex)
            {

                pictureBox1.Image = null;
            }

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            V_Captura_Imagen capturaImagen = new V_Captura_Imagen();
            capturaImagen.ShowDialog();
            Bitmap imagenCapturada = V_Captura_Imagen.Get_Imagen();
            if (imagenCapturada != null)
            {
                pictureBox1.Image = imagenCapturada;
                Imagen = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\CAT\\Cat - " + Secuencial + ".PNG");
                pictureBox1.Image.Save(Imagen); // Guarda la imagen capturada en la ruta especificada
            }
            else
            {
                V_Menu_Principal.MSG.ShowMSG("No se ha capturado ninguna imagen.", "Error");
            }
        }
    }
}
