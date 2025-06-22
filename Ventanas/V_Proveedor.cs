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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Monitux_POS.Ventanas
{
    public partial class V_Proveedor : Form
    {
        public int Secuencial_Usuario { get; set; } = V_Menu_Principal.Secuencial_Usuario;

        int Secuencial = 0;
        string Imagen = "";

        public V_Proveedor()
        {
            InitializeComponent();
        }

        private void V_Proveedor_Load(object sender, EventArgs e)
        {



            if (V_Menu_Principal.Acceso_Usuario != "Administrador")
            {
                Menu_Eliminar.Visible = false; // Oculta el botón de eliminar si el usuario no es administrador
            }
            else
            {
                Menu_Eliminar.Visible = true; // Muestra el botón de eliminar si el usuario es administrador
            }


            this.Text = "Monitux-POS v." + V_Menu_Principal.VER; // Establece el título del formulario
            comboBox2.Items.Add("Nombre");
            comboBox2.Items.Add("Telefono");
            comboBox2.Items.Add("Contacto");
            comboBox2.Items.Add("Email");
            Cargar_Datos(); // Carga los datos al iniciar el formulario
                            // comboBox1.Items.Add("Nombre");
                            // comboBox1.Items.Add("Descripcion");
                            // comboBox1.SelectedIndex = 0; // Selecciona el primer elemento por defecto
            dataGridView1.ReadOnly = true; // Hace que el DataGridView sea de solo lectura
            comboBox2.SelectedIndex = 0; // Selecciona el primer elemento por defecto

        }






        private void Cargar_Datos()
        {

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            var proveedor = context.Proveedores.ToList();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selecciona toda la fila
            dataGridView1.Columns.Add("Secuencial", "S");
            dataGridView1.Columns["Secuencial"].Width = 20; // Ajusta el ancho de la columna Secuencial
            dataGridView1.Columns.Add("Nombre", "Nombre");
            dataGridView1.Columns["Nombre"].Width = 80; // Ajusta el ancho de la columna Nombre
            dataGridView1.Columns.Add("Telefono", "Telefono");
            dataGridView1.Columns["Telefono"].Width = 60; // Ajusta el ancho de la columna Nombre

            dataGridView1.Columns.Add("Direccion", "Direccion");
            dataGridView1.Columns["Direccion"].Width = 150; // Ajusta el ancho de la columna Descripcion

            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns["Email"].Width = 100; // Ajusta el ancho de la columna Email
            dataGridView1.Columns.Add("Contacto", "Contacto");
            dataGridView1.Columns["Contacto"].Width = 100; // Ajusta el ancho de la columna Contacto
            dataGridView1.Columns.Add("Tipo", "Tipo");
            dataGridView1.Columns["Tipo"].Width = 50; // Ajusta el ancho de la columna Tipo
            dataGridView1.Columns.Add("Activo", "Activo");
            dataGridView1.Columns.Add("Imagen", "Imagen");
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            foreach (var item in proveedor)
            {
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Nombre,
                    item.Telefono,
                    item.Direccion,
                    item.Email,
                    item.Contacto,
                    item.Tipo,
                    (bool)item.Activo ? "Si" : "No",


                    item.Imagen ?? "No Imagen" // Maneja el caso donde Imagen sea null

                );


            }


        }



        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                    txt_Nombre.Text = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Telefono"].Value != null)
                {
                    txt_Telefono.Text = dataGridView1.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Direccion"].Value != null)
                {
                    txt_Direccion.Text = dataGridView1.Rows[e.RowIndex].Cells["Direccion"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Email"].Value != null)
                {
                    txt_Email.Text = dataGridView1.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Contacto"].Value != null)
                {
                    txt_Contacto.Text = dataGridView1.Rows[e.RowIndex].Cells["Contacto"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Tipo"].Value != null)
                {


                    foreach (var item in combo_Tipo.Items)
                    {
                        if (item.ToString().Contains(dataGridView1.Rows[e.RowIndex].Cells["Tipo"].Value.ToString()))  // Verifica si hay un número
                        {
                            combo_Tipo.SelectedItem = item;
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
                        pictureBox1.Image = Util.Cargar_Imagen_Local(dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value.ToString());
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

                pictureBox1.Image = null;
            }






        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {



            string rutaGuardado = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\PRO\\Pro - " + Secuencial + ".PNG");


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
            catch
            {

                Imagen = "Sin Imagen";

            }


        }

        private void Menu_Guardar_Click(object sender, EventArgs e)
        {





            if (Secuencial != 0)
            {

                if (combo_Tipo.SelectedIndex == -1)
                {
                    V_Menu_Principal.MSG.ShowMSG("Debe seleccionar un tipo de proveedor.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Nombre.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El nombre del proveedor no puede estar vacío.", "Error");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txt_Telefono.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El telefono no puede estar vacío.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Contacto.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El contacto no puede estar vacío.", "Error");
                    return;
                }
                // **UPDATE**
                SQLitePCL.Batteries.Init();

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated(); // Crea la base de datos si no existe


                var proveedor = context.Proveedores.FirstOrDefault(p => p.Secuencial == this.Secuencial);
                if (proveedor != null)
                {
                    proveedor.Nombre = txt_Nombre.Text;
                    proveedor.Telefono = txt_Telefono.Text;
                    proveedor.Direccion = txt_Direccion.Text;
                    proveedor.Email = txt_Email.Text;
                    proveedor.Contacto = txt_Contacto.Text;
                    proveedor.Tipo = combo_Tipo.SelectedItem != null ? combo_Tipo.SelectedItem.ToString() : "Sin Tipo";
                    proveedor.Activo = checkBox1.Checked;



                    proveedor.Imagen = Imagen;
                    context.SaveChanges();
                    Util.Registrar_Actividad(Secuencial_Usuario, "Ha modificado al proveedor: " + proveedor.Nombre);
                    V_Menu_Principal.MSG.ShowMSG("Proveedor actualizado correctamente.", "Éxito");
                    Cargar_Datos(); // Recarga los datos después de actualizar el proveedor
                }


            }
            else
            {
                if (combo_Tipo.SelectedIndex == -1)
                {
                    V_Menu_Principal.MSG.ShowMSG("Debe seleccionar un tipo de proveedor.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Nombre.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El nombre del proveedor no puede estar vacío.", "Error");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txt_Telefono.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El telefono no puede estar vacío.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Contacto.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El contacto no puede estar vacío.", "Error");
                    return;
                }
                // **Create**
                SQLitePCL.Batteries.Init();

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated(); // Crea la base de datos si no existe

                var proveedor = new Proveedor();

                proveedor.Nombre = txt_Nombre.Text;
                proveedor.Telefono = txt_Telefono.Text;
                proveedor.Direccion = txt_Direccion.Text;
                proveedor.Email = txt_Email.Text;
                proveedor.Contacto = txt_Contacto.Text;
                proveedor.Tipo = combo_Tipo.SelectedItem != null ? combo_Tipo.SelectedItem.ToString() : "Sin Tipo";
                proveedor.Activo = true;


                if (pictureBox1.Image != null)
                {
                    proveedor.Imagen = Imagen;
                }
                else
                {
                    proveedor.Imagen = "Sin Imagen"; // Asigna una imagen por defecto si no se ha seleccionado una imagen
                }


                context.Proveedores.Add(proveedor);
                context.SaveChanges();
                Util.Registrar_Actividad(Secuencial_Usuario, "Ha creado al proveedor: " + txt_Nombre.Text);
                V_Menu_Principal.MSG.ShowMSG("Proveedor creado correctamente.", "Éxito");
                Cargar_Datos(); // Recarga los datos después de crear el proveedor


            }





        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Agregar_Click(object sender, EventArgs e)
        {

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Secuencial = 0;
            pictureBox1.Image = null;
            txt_Nombre.Text = "";
            txt_Telefono.Text = "";
            txt_Direccion.Text = "";
            txt_Email.Text = "";
            txt_Contacto.Text = "";
            checkBox1.Checked = true; // Marca el checkbox como activo
            combo_Tipo.SelectedIndex = -1; // Desmarca el combo box



        }

        private void Menu_Eliminar_Click(object sender, EventArgs e)
        {



            var res = V_Menu_Principal.MSG.ShowMSG("¿Está seguro de eliminar este proveedor?", "Confirmación");

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

                var proveedor = context.Proveedores.FirstOrDefault(p => p.Secuencial == this.Secuencial);
                if (proveedor != null)
                {
                    context.Proveedores.Remove(proveedor);
                    context.SaveChanges();
                    Util.Registrar_Actividad(Secuencial_Usuario, "Ha eliminado al proveedor: " + proveedor.Nombre);
                    V_Menu_Principal.MSG.ShowMSG("Proveedor eliminado correctamente.", "Éxito");
                    Cargar_Datos(); // Recarga los datos después de eliminar el proveedor
                }
            }


        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {





        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
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
                    txt_Nombre.Text = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Telefono"].Value != null)
                {
                    txt_Telefono.Text = dataGridView1.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Direccion"].Value != null)
                {
                    txt_Direccion.Text = dataGridView1.Rows[e.RowIndex].Cells["Direccion"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Email"].Value != null)
                {
                    txt_Email.Text = dataGridView1.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Contacto"].Value != null)
                {
                    txt_Contacto.Text = dataGridView1.Rows[e.RowIndex].Cells["Contacto"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Tipo"].Value != null)
                {


                    foreach (var item in combo_Tipo.Items)
                    {
                        if (item.ToString().Contains(dataGridView1.Rows[e.RowIndex].Cells["Tipo"].Value.ToString()))  // Verifica si hay un número
                        {
                            combo_Tipo.SelectedItem = item;
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
                        pictureBox1.Image = Util.Cargar_Imagen_Local(dataGridView1.Rows[e.RowIndex].Cells["Imagen"].Value.ToString());
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

                pictureBox1.Image = null;
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

            var proveedores = context.Proveedores
                    .Where(c => EF.Property<string>(c, columnaSeleccionada).Contains(valor))
                    .ToList();

            dataGridView1.Rows.Clear();
            foreach (var item in proveedores)
            {
                dataGridView1.Rows.Add(item.Secuencial,
                    item.Nombre,
                    item.Telefono,
                    item.Direccion,
                    item.Email,
                    item.Contacto,
                    item.Tipo,
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
                dataGridView1.Columns.Add("Nombre", "Nombre");
                dataGridView1.Columns["Nombre"].Width = 80;
                dataGridView1.Columns.Add("Telefono", "Telefono");
                dataGridView1.Columns["Telefono"].Width = 60;
                dataGridView1.Columns.Add("Direccion", "Direccion");
                dataGridView1.Columns["Direccion"].Width = 150;
                dataGridView1.Columns.Add("Email", "Email");
                dataGridView1.Columns["Email"].Width = 100;
                dataGridView1.Columns.Add("Contacto", "Contacto");
                dataGridView1.Columns["Contacto"].Width = 100;
                dataGridView1.Columns.Add("Tipo", "Tipo");
                dataGridView1.Columns["Tipo"].Width = 50;
                dataGridView1.Columns.Add("Activo", "Activo");
                dataGridView1.Columns.Add("Imagen", "Imagen");


            }

            // Limpiar filas antes de agregar nuevas
            dataGridView1.Rows.Clear();

            foreach (var item in proveedores)
            {
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Nombre,
                    item.Telefono,
                    item.Direccion,
                    item.Email,
                    item.Contacto,
                    item.Tipo,
                    (bool)item.Activo ? "Si" : "No",

                    item.Imagen ?? "No Imagen" // Maneja el caso donde Imagen sea null
                );
            }


        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filtrar(comboBox2.SelectedItem.ToString(), textBox1.Text.Trim()); // Llama al método Filtrar con el valor del TextBox
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            V_Captura_Imagen capturaImagen = new V_Captura_Imagen();
            capturaImagen.ShowDialog(); // Muestra el formulario de captura de imagen
            Bitmap imagenCapturada = V_Captura_Imagen.Get_Imagen(); // Obtiene la imagen capturada
            if (imagenCapturada != null)
            {
                pictureBox1.Image = imagenCapturada; // Asigna la imagen capturada al PictureBox
                string rutaGuardado = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\PRO\\Pro - " + Secuencial + ".PNG");
                imagenCapturada.Save(rutaGuardado); // Guarda la imagen en la ruta especificada
                Imagen = rutaGuardado; // Actualiza la variable Imagen con la ruta guardada
            }
            else
            {
                V_Menu_Principal.MSG.ShowMSG("No se ha capturado ninguna imagen.", "Error");
            }
        }
    }
}
