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
    public partial class V_Usuario : Form
    {
        public int Secuencial_Usuario { get; set; } = 0;
        int Secuencial = 0;
        string Imagen = "";
        public V_Usuario()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void V_Usuario_Load(object sender, EventArgs e)
        {
            Cargar_Datos();

            comboBox1.Items.Add("Vendedor");
            comboBox1.Items.Add("Administrador");

            comboBox1.Items.Add("Almacen");

            comboBox1.SelectedIndex = 0;
            dataGridView1.ReadOnly = true; // Hace que el DataGridView sea de solo lectura
            comboBox2.Items.Add("Codigo");
            comboBox2.Items.Add("Nombre");
            comboBox2.SelectedIndex = 0; // Selecciona el primer elemento del comboBox2
        }



        private void Cargar_Datos()
        {
            dataGridView1.Rows.Clear(); // Limpia las filas del DataGridView antes de cargar los datos
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            var usuarios = context.Usuarios.ToList();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selecciona toda la fila
            dataGridView1.Columns.Add("Secuencial", "S");
            dataGridView1.Columns["Secuencial"].Width = 20; // Ajusta el ancho de la columna Secuencial


            dataGridView1.Columns.Add("Codigo", "Codigo");
            dataGridView1.Columns["Codigo"].Width = 80; // Ajusta el ancho de la columna Nombre

            dataGridView1.Columns.Add("Nombre", "Nombre");
            dataGridView1.Columns["Nombre"].Width = 80; // Ajusta el ancho de la columna Nombre

            dataGridView1.Columns.Add("Password", "Password");
            dataGridView1.Columns["Password"].Width = 60; // Ajusta el ancho de la columna Nombre
            dataGridView1.Columns["Password"].Visible = false; // Oculta la columna Password
            dataGridView1.Columns.Add("Imagen", "Imagen");
            dataGridView1.Columns.Add("Acceso", "Acceso");
            dataGridView1.Columns["Acceso"].Width = 150; // Ajusta el ancho de la columna Descripcion



            dataGridView1.Columns.Add("Activo", "Activo");




            foreach (var item in usuarios)
            {
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Codigo,
                    item.Nombre,
                    item.Password,

                       item.Imagen ?? "No Imagen", // Maneja el caso donde Imagen sea null
                       item.Acceso,
                    (bool)item.Activo ? "Si" : "No"




                );


            }


        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {




            string rutaGuardado = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\USR\\Usr - " + Secuencial + ".PNG");


            try
            {
                string Imagen_Seleccionada = Util.Abrir_Dialogo_Seleccion_URL();
                if (Imagen_Seleccionada != "")
                {
                    Imagen = Imagen_Seleccionada;
                    pictureBox1.Load(Imagen);

                    pictureBox1.Image.Save(rutaGuardado);
                    this.Imagen = rutaGuardado;
                }



            }
            catch
            {

                Imagen = "Sin Imagen";

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

                if (dataGridView1.Rows[e.RowIndex].Cells["Codigo"].Value != null)
                {
                    txt_Codigo.Text = dataGridView1.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value != null)
                {
                    txt_Nombre.Text = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Password"].Value != null)
                {
                    Util.Encriptador.Encriptar(txt_Password.Text = dataGridView1.Rows[e.RowIndex].Cells["Password"].Value.ToString());
                }


                if (dataGridView1.Rows[e.RowIndex].Cells["Acceso"].Value != null)
                {


                    foreach (var item in comboBox1.Items)
                    {
                        if (item.ToString().Contains(dataGridView1.Rows[e.RowIndex].Cells["Acceso"].Value.ToString()))  // Verifica si hay un número
                        {
                            comboBox1.SelectedItem = item;
                            break;
                        }
                    }

                }






                if (dataGridView1.Rows[e.RowIndex].Cells["Activo"].Value != null)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["Activo"].Value.ToString() == "Si")
                    {
                        checkBox1.Checked = true;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                    }
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value != null &&
                    !string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value.ToString()))
                {
                    try
                    {
                        pictureBox1.Load(dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value.ToString());
                        Imagen = dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value.ToString(); // Guarda la ruta de la imagen
                    }
                    catch
                    {

                        pictureBox1.Image = null; // Si no se puede cargar la imagen, establece la imagen como nula
                    }

                }
                else
                {

                    pictureBox1.Image = null; // Si no se puede cargar la imagen, establece la imagen como nula
                                              // txtNombre.Text = "";
                                              //txtDescripcion.Text = "";

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error al cargar los datos: " + ex.Message);
                pictureBox1.Image = null;
            }






        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Menu_Guardar_Click(object sender, EventArgs e)
        {




            if (Secuencial != 0)
            {


                if (string.IsNullOrWhiteSpace(txt_Nombre.Text))
                {
                    MessageBox.Show("El nombre del usuario no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txt_Codigo.Text))
                {
                    MessageBox.Show("El codigo de usuario no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Password.Text))
                {
                    MessageBox.Show("El password no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // **UPDATE**
                SQLitePCL.Batteries.Init();

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated(); // Crea la base de datos si no existe


                var usuario = context.Usuarios.FirstOrDefault(p => p.Secuencial == this.Secuencial);
                if (usuario != null)
                {
                    usuario.Nombre = txt_Nombre.Text;
                    usuario.Codigo = txt_Codigo.Text;
                    usuario.Password = txt_Password.Text;
                    usuario.Acceso = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "Sin Tipo";
                    usuario.Activo = checkBox1.Checked;



                    usuario.Imagen = Imagen;
                    context.SaveChanges();
                    Util.Registrar_Actividad(Secuencial_Usuario, "Ha modificado al usuario: " + usuario.Nombre);
                    MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cargar_Datos();
                }


            }
            else
            {
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un acceso de usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Nombre.Text))
                {
                    MessageBox.Show("El nombre del usuario no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txt_Codigo.Text))
                {
                    MessageBox.Show("El codigo no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Password.Text))
                {
                    MessageBox.Show("El password no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // **Create**
                SQLitePCL.Batteries.Init();

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated(); // Crea la base de datos si no existe

                var usuario = new Usuario();

                usuario.Nombre = txt_Nombre.Text;
                usuario.Codigo = txt_Codigo.Text;
                usuario.Password = Util.Encriptador.Encriptar(txt_Password.Text);


                usuario.Acceso = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "Sin Tipo";
                usuario.Activo = true;


                if (pictureBox1.Image != null)
                {
                    usuario.Imagen = Imagen;
                }
                else
                {
                    usuario.Imagen = "Sin Imagen"; // Asigna una imagen por defecto si no se ha seleccionado una imagen
                }


                context.Usuarios.Add(usuario);
                context.SaveChanges();
                MessageBox.Show("Usuario creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Util.Registrar_Actividad(Secuencial_Usuario, "Ha creado al usuario: " + txt_Nombre.Text);
                Cargar_Datos();


            }





        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txt_Codigo.Text = "";
            txt_Nombre.Text = "";
            txt_Password.Text = "";
            pictureBox1.Image = null; // Limpia la imagen
            Imagen = ""; // Limpia la variable de imagen
            Secuencial = 0; // Reinicia el secuencial
            comboBox1.SelectedIndex = 0; // Selecciona el primer elemento del comboBox
            checkBox1.Checked = true; // Marca el checkbox como activo

        }

        private void Menu_Eliminar_Click(object sender, EventArgs e)
        {




            var res = MessageBox.Show("¿Está seguro de eliminar este usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly, false);

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

                var usuario = context.Usuarios.FirstOrDefault(p => p.Secuencial == this.Secuencial);
                if (usuario != null)
                {
                    context.Usuarios.Remove(usuario);
                    context.SaveChanges();
                    Util.Registrar_Actividad(Secuencial_Usuario, "Ha eliminado al usuario: " + usuario.Nombre);
                    MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cargar_Datos();
                }
            }


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

            var usuarios = context.Usuarios
                    .Where(c => EF.Property<string>(c, columnaSeleccionada).Contains(valor))
                    .ToList();

            dataGridView1.Rows.Clear();
            foreach (var item in usuarios)
            {
                dataGridView1.Rows.Add(item.Secuencial,
                    item.Codigo,
                    item.Nombre,
                    item.Password,


                    (bool)item.Activo ? "Si" : "No",
                    item.Imagen ?? "No Imagen" // Maneja el caso donde Imagen sea null
                );
            }


            //-------------------Filtro que usare






            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selecciona toda la fila

            // Agregar columnas si no existen
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("Secuencial", "S");
                dataGridView1.Columns["Secuencial"].Width = 20;

                dataGridView1.Columns.Add("Codigo", "Codigo");
                dataGridView1.Columns["Codigo"].Width = 80;

                dataGridView1.Columns.Add("Nombre", "Nombre");
                dataGridView1.Columns["Nombre"].Width = 80;

                dataGridView1.Columns.Add("Password", "Password");
                dataGridView1.Columns["Password"].Width = 60;
                dataGridView1.Columns["Password"].Visible = false; // Oculta la columna Password



                dataGridView1.Columns.Add("Activo", "Activo");

                dataGridView1.Columns.Add("Imagen", "Imagen");


            }

            // Limpiar filas antes de agregar nuevas
            dataGridView1.Rows.Clear();

            foreach (var item in usuarios)
            {
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Codigo,
                    item.Nombre,
                    item.Password,

                    (bool)item.Activo ? "Si" : "No",

                    item.Imagen ?? "No Imagen" // Maneja el caso donde Imagen sea null
                );
            }


        }







        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            Filtrar(comboBox2.SelectedItem.ToString(), textBox1.Text.Trim()); // Llama al método Filtrar con el valor del TextBox

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {




            try
            {





                if (dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value != null)
                {
                    this.Secuencial = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value);

                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Codigo"].Value != null)
                {
                    txt_Codigo.Text = dataGridView1.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value != null)
                {
                    txt_Nombre.Text = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Password"].Value != null)
                {
                    Util.Encriptador.Encriptar(txt_Password.Text = dataGridView1.Rows[e.RowIndex].Cells["Password"].Value.ToString());
                }


                if (dataGridView1.Rows[e.RowIndex].Cells["Acceso"].Value != null)
                {


                    foreach (var item in comboBox1.Items)
                    {
                        if (item.ToString().Contains(dataGridView1.Rows[e.RowIndex].Cells["Acceso"].Value.ToString()))  // Verifica si hay un número
                        {
                            comboBox1.SelectedItem = item;
                            break;
                        }
                    }

                }






                if (dataGridView1.Rows[e.RowIndex].Cells["Activo"].Value != null)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["Activo"].Value.ToString() == "Si")
                    {
                        checkBox1.Checked = true;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                    }
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value != null &&
                    !string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value.ToString()))
                {
                    try
                    {
                        pictureBox1.Load(dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value.ToString());
                        Imagen = dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value.ToString(); // Guarda la ruta de la imagen
                    }
                    catch
                    {

                        pictureBox1.Image = null; // Si no se puede cargar la imagen, establece la imagen como nula
                    }

                }
                else
                {

                    pictureBox1.Image = null; // Si no se puede cargar la imagen, establece la imagen como nula
                                              // txtNombre.Text = "";
                                              //txtDescripcion.Text = "";

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error al cargar los datos: " + ex.Message);
                pictureBox1.Image = null;
            }








        }
    }
}
