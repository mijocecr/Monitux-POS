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
        byte[]? Imagen; // Variable para almacenar la imagen de la categoria seleccionada
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
            context.Database.EnsureCreated();

            var categorias = context.Categorias
                .Where(c => c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
                .ToList();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Columnas
            dataGridView1.Columns.Add("Secuencial", "S");
            dataGridView1.Columns["Secuencial"].Width = 20;

            dataGridView1.Columns.Add("Nombre", "Nombre");
            dataGridView1.Columns["Nombre"].Width = 80;

            dataGridView1.Columns.Add("Descripcion", "Descripcion");
            dataGridView1.Columns["Descripcion"].Width = 250;

           /* // Columna de imagen como tipo imagen
            var imageColumn = new DataGridViewImageColumn
            {
                Name = "Imagen",
                HeaderText = "Imagen",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                Width = 100
            };
            dataGridView1.Columns.Add(imageColumn);*/

            // Cargar datos
            foreach (var item in categorias)
            {
                Image? imagen = null;

                if (item.Imagen != null && item.Imagen.Length > 0)
                {
                    using var ms = new MemoryStream(item.Imagen);
                    imagen = Image.FromStream(ms);
                }

                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Nombre,
                    item.Descripcion
                    //imagen ?? Properties.Resources.SinImagen // Usa una imagen por defecto si no hay
                );
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value != null)
                {
                    int secuencialSeleccionado = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value);

                    using var context = new Monitux_DB_Context();
                    var categoria = context.Categorias.FirstOrDefault(c => c.Secuencial == secuencialSeleccionado);

                    if (categoria != null)
                    {
                        this.Secuencial = categoria.Secuencial;
                        txtNombre.Text = categoria.Nombre ?? "";
                        txtDescripcion.Text = categoria.Descripcion ?? "";

                        if (categoria.Imagen != null && categoria.Imagen.Length > 0)
                        {
                            try
                            {
                                using var ms = new MemoryStream(categoria.Imagen);
                                pictureBox1.Image = Image.FromStream(ms);
                            }
                            catch
                            {
                                pictureBox1.Image = null;//Properties.Resources.SinImagen;
                            }
                        }
                        else
                        {
                            pictureBox1.Image = null; //Properties.Resources.SinImagen;
                        }
                    }
                    else
                    {
                       // Limpiar
                    }
                }
            }
            catch (Exception ex)
            {
               // LimpiarCampos();
            }


        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var res = V_Menu_Principal.MSG.ShowMSG("¿Está seguro de eliminar esta categoría?", "Confirmación");

            if (res == DialogResult.Yes)
            {
                try
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose(); // Libera la imagen del PictureBox
                        pictureBox1.Image = null;    // Limpia la referencia visual
                    }

                    Imagen = null; // Limpia también la imagen en memoria
                }
                catch
                {
                    // Puedes registrar el error si lo deseas
                }

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var categoria = context.Categorias.FirstOrDefault(p =>
                    p.Secuencial == this.Secuencial &&
                    p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

                if (categoria != null)
                {
                    string nombreCategoria = categoria.Nombre;

                    context.Categorias.Remove(categoria);
                    context.SaveChanges();

                    Util.Registrar_Actividad(Secuencial_Usuario, $"Ha eliminado la categoría: {nombreCategoria}", V_Menu_Principal.Secuencial_Empresa);
                    V_Menu_Principal.MSG.ShowMSG("Categoría eliminada correctamente.", "Éxito");
                    Cargar_Datos();
                }
                else
                {
                    V_Menu_Principal.MSG.ShowMSG("No se encontró la categoría a eliminar.", "Error");
                }
            }


        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Comprimir imagen si existe
            byte[] imagenBytes = null;
            if (pictureBox1.Image != null)
            {
                //imagenBytes = Util.ComprimirImagen(pictureBox1.Image, 40L); // Calidad ajustable
                using var imagenCopia = new Bitmap(pictureBox1.Image); // ✅ Clona la imagen
                imagenBytes = Util.ComprimirImagen(imagenCopia, 40L);
            }

            if (Secuencial != 0)
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El nombre de la categoría no puede estar vacío.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("La descripción de la categoría no puede estar vacía.", "Error");
                    return;
                }

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var categoria = context.Categorias.FirstOrDefault(p => p.Secuencial == this.Secuencial);
                if (categoria != null)
                {
                    categoria.Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa;
                    categoria.Nombre = txtNombre.Text;
                    categoria.Descripcion = txtDescripcion.Text;
                    categoria.Imagen = imagenBytes ?? categoria.Imagen;

                    context.SaveChanges();
                    Util.Registrar_Actividad(Secuencial_Usuario, $"Ha modificado la categoría: {categoria.Nombre}", V_Menu_Principal.Secuencial_Empresa);
                    V_Menu_Principal.MSG.ShowMSG("Categoría actualizada correctamente.", "Éxito");
                    Cargar_Datos();
                }
            }
            else
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El nombre de la categoría no puede estar vacío.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("La descripción de la categoría no puede estar vacía.", "Error");
                    return;
                }

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var categoria = new Categoria
                {
                    Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa,
                //    Secuencial = context.Categorias.Any() ? context.Categorias.Max(c => c.Secuencial) + 1 : 1,
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    Imagen = imagenBytes // Comprimida o null
                };

                context.Categorias.Add(categoria);
                context.SaveChanges();
                Util.Registrar_Actividad(Secuencial_Usuario, $"Ha creado la categoría: {categoria.Nombre}", V_Menu_Principal.Secuencial_Empresa);
                V_Menu_Principal.MSG.ShowMSG("Categoría creada correctamente.", "Éxito");
                Cargar_Datos();
            }

            this.Dispose(); // Cierra el formulario después de guardar

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {


            try
            {
                string imagenSeleccionada = Util.Abrir_Dialogo_Seleccion_URL();

                if (!string.IsNullOrEmpty(imagenSeleccionada))
                {
                    Image imagenCargada = Image.FromFile(imagenSeleccionada);
                    pictureBox1.Image = imagenCargada;

                    using var ms = new MemoryStream();
                    imagenCargada.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    Imagen = ms.ToArray(); // Guarda la imagen como byte[]
                }
            }
            catch
            {
                Imagen = null;
            }


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
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            // Filtrar por el campo especificado y empresa actual
            var categoriasFiltradas = context.Categorias
                .Where(c => EF.Property<string>(c, campo).Contains(valor) &&
                            c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
                .ToList();

            // Configurar columnas si aún no existen
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("Secuencial", "S");
                dataGridView1.Columns["Secuencial"].Width = 20;

                dataGridView1.Columns.Add("Nombre", "Nombre");
                dataGridView1.Columns["Nombre"].Width = 80;

                dataGridView1.Columns.Add("Descripcion", "Descripción");
                dataGridView1.Columns["Descripcion"].Width = 250;

                
            }

            dataGridView1.Rows.Clear();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            foreach (var item in categoriasFiltradas)
            {
               

                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Nombre,
                    item.Descripcion
                    
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
                    int secuencialSeleccionado = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value);

                    using var context = new Monitux_DB_Context();
                    var categoria = context.Categorias.FirstOrDefault(c => c.Secuencial == secuencialSeleccionado);

                    if (categoria != null)
                    {
                        this.Secuencial = categoria.Secuencial;
                        txtNombre.Text = categoria.Nombre ?? "";
                        txtDescripcion.Text = categoria.Descripcion ?? "";

                        if (categoria.Imagen != null && categoria.Imagen.Length > 0)
                        {
                            try
                            {
                                using var ms = new MemoryStream(categoria.Imagen);
                                pictureBox1.Image = Image.FromStream(ms);
                            }
                            catch
                            {
                                pictureBox1.Image = null;//Properties.Resources.SinImagen;
                            }
                        }
                        else
                        {
                            pictureBox1.Image = null; //Properties.Resources.SinImagen;
                        }
                    }
                    else
                    {
                        // Limpiar
                    }
                }
            }
            catch (Exception ex)
            {
                // LimpiarCampos();
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

                using var ms = new MemoryStream();
                imagenCapturada.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                Imagen = ms.ToArray(); // Guarda la imagen como byte[]
            }
            else
            {
                V_Menu_Principal.MSG.ShowMSG("No se ha capturado ninguna imagen.", "Error");
            }


        }
    }
}
