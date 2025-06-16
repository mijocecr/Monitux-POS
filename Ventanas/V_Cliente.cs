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
using static System.Net.Mime.MediaTypeNames;

namespace Monitux_POS.Ventanas
{
    public partial class V_Cliente : Form
    {

        int Secuencial = 0;
        string Imagen = "";
        public int Secuencial_Usuario { get; set; } = V_Menu_Principal.Secuencial_Usuario;

        public V_Cliente()
        {
            InitializeComponent();
        }





        private void Cargar_Datos()
        {

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            var cliente = context.Clientes.ToList();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selecciona toda la fila
            dataGridView1.Columns.Add("Secuencial", "S");
            dataGridView1.Columns["Secuencial"].Width = 20; // Ajusta el ancho de la columna Secuencial


            dataGridView1.Columns.Add("Codigo", "Codigo");
            dataGridView1.Columns["Codigo"].Width = 80; // Ajusta el ancho de la columna Nombre

            dataGridView1.Columns.Add("Nombre", "Nombre");
            dataGridView1.Columns["Nombre"].Width = 80; // Ajusta el ancho de la columna Nombre

            dataGridView1.Columns.Add("Telefono", "Telefono");
            dataGridView1.Columns["Telefono"].Width = 60; // Ajusta el ancho de la columna Nombre

            dataGridView1.Columns.Add("Direccion", "Direccion");
            dataGridView1.Columns["Direccion"].Width = 150; // Ajusta el ancho de la columna Descripcion

            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns["Email"].Width = 100; // Ajusta el ancho de la columna Email


            dataGridView1.Columns.Add("Activo", "Activo");
            dataGridView1.Columns.Add("Imagen", "Imagen");


            foreach (var item in cliente)
            {
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Codigo,
                    item.Nombre,
                    item.Telefono,
                    item.Direccion,
                    item.Email,


                    (bool)item.Activo ? "Si" : "No",


                    item.Imagen ?? "No Imagen" // Maneja el caso donde Imagen sea null

                );


            }


        }








        private void V_Cliente_Load(object sender, EventArgs e)
        {


            if (V_Menu_Principal.Acceso_Usuario != "Administrador")
            {
                Menu_Eliminar.Visible = false; // Oculta el botón de eliminar si el usuario no es administrador
            }
            else
            {
                Menu_Eliminar.Visible = true; // Muestra el botón de eliminar si el usuario es administrador
            }


            this.Text = "Monitux POS ver." + V_Menu_Principal.VER; // Establece el título del formulario
            Cargar_Datos();
            dataGridView1.ReadOnly = true;
            comboBox2.Items.Add("Nombre");
            comboBox2.Items.Add("Telefono");
            comboBox2.Items.Add("Codigo");
            comboBox2.SelectedIndex = 0;
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {




            string rutaGuardado = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\CLI\\Cli - " + Secuencial + ".PNG");


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

        private void Menu_Guardar_Click(object sender, EventArgs e)
        {







            if (Secuencial != 0)
            {

                if (string.IsNullOrWhiteSpace(txt_Codigo.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El codigo del cliente no puede estar vacío.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Nombre.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El nombre del cliente no puede estar vacío.", "Error");

                    return;
                }
                if (string.IsNullOrWhiteSpace(txt_Telefono.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El telefono no puede estar vacío.", "Error");
                    return;
                }

                // **UPDATE**
                SQLitePCL.Batteries.Init();

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated(); // Crea la base de datos si no existe


                var cliente = context.Clientes.FirstOrDefault(p => p.Secuencial == this.Secuencial);
                if (cliente != null)
                {
                    cliente.Codigo = txt_Codigo.Text;
                    cliente.Nombre = txt_Nombre.Text;
                    cliente.Telefono = txt_Telefono.Text;
                    cliente.Direccion = txt_Direccion.Text;
                    cliente.Email = txt_Email.Text;


                    cliente.Activo = checkBox1.Checked;



                    cliente.Imagen = Imagen;
                    context.SaveChanges();
                    Util.Registrar_Actividad(Secuencial_Usuario, "Ha modificado el cliente: " + cliente.Nombre);
                    V_Menu_Principal.MSG.ShowMSG("Cliente actualizado correctamente.", "Éxito");
                    Cargar_Datos(); // Recargar los datos después de actualizar el cliente
                }


            }
            else
            {

                if (string.IsNullOrWhiteSpace(txt_Nombre.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El nombre del cliente no puede estar vacío.", "Error");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txt_Telefono.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El telefono no puede estar vacío.", "Error");
                    return;
                }


                // **Create**
                SQLitePCL.Batteries.Init();

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated(); // Crea la base de datos si no existe

                var cliente = new Cliente();

                cliente.Codigo = txt_Codigo.Text;
                cliente.Nombre = txt_Nombre.Text;
                cliente.Telefono = txt_Telefono.Text;
                cliente.Direccion = txt_Direccion.Text;
                cliente.Email = txt_Email.Text;

                cliente.Activo = true;


                if (pictureBox1.Image != null)
                {
                    cliente.Imagen = Imagen;
                }
                else
                {
                    cliente.Imagen = "Sin Imagen"; // Asigna una imagen por defecto si no se ha seleccionado una imagen
                }


                context.Clientes.Add(cliente);
                context.SaveChanges();
                V_Menu_Principal.MSG.ShowMSG("Cliente creado correctamente.", "Éxito");
                Util.Registrar_Actividad(Secuencial_Usuario, "Ha creado el cliente: " + txt_Nombre.Text);
                Cargar_Datos(); // Recargar los datos después de crear el cliente


            }






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

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {


            Secuencial = 0;
            pictureBox1.Image = null;
            txt_Nombre.Text = "";
            txt_Telefono.Text = "";
            txt_Direccion.Text = "";
            txt_Email.Text = "";
            txt_Codigo.Text = "";

            checkBox1.Checked = true; // Marca el checkbox como activo



        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            this.Dispose();

        }

        private void Menu_Eliminar_Click(object sender, EventArgs e)
        {



            var res = V_Menu_Principal.MSG.ShowMSG("¿Está seguro de eliminar este cliente?", "Confirmación");

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

                var cliente = context.Clientes.FirstOrDefault(p => p.Secuencial == this.Secuencial);
                if (cliente != null)
                {
                    context.Clientes.Remove(cliente);
                    context.SaveChanges();
                    Util.Registrar_Actividad(Secuencial_Usuario, "Ha eliminado al cliente: " + cliente.Nombre);
                    V_Menu_Principal.MSG.ShowMSG("Cliente eliminado correctamente.", "Éxito");
                    Cargar_Datos(); // Recargar los datos después de eliminar el cliente
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

            var clientes = context.Clientes
                    .Where(c => EF.Property<string>(c, columnaSeleccionada).Contains(valor))
                    .ToList();

            dataGridView1.Rows.Clear();
            foreach (var item in clientes)
            {
                dataGridView1.Rows.Add(item.Secuencial,
                    item.Codigo,
                    item.Nombre,
                    item.Telefono,
                    item.Direccion,
                    item.Email,
                    
                    
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

                dataGridView1.Columns.Add("Telefono", "Telefono");
                dataGridView1.Columns["Telefono"].Width = 60;

                dataGridView1.Columns.Add("Direccion", "Direccion");
                dataGridView1.Columns["Direccion"].Width = 150;

                dataGridView1.Columns.Add("Email", "Email");
                dataGridView1.Columns["Email"].Width = 100;

                dataGridView1.Columns.Add("Activo", "Activo");
                
                dataGridView1.Columns.Add("Imagen", "Imagen");


            }

            // Limpiar filas antes de agregar nuevas
            dataGridView1.Rows.Clear();

            foreach (var item in clientes)
            {
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Codigo,
                    item.Nombre,
                    item.Telefono,
                    item.Direccion,
                    item.Email,
                    
                    (bool)item.Activo ? "Si" : "No",

                    item.Imagen ?? "No Imagen" // Maneja el caso donde Imagen sea null
                );
            }


        }




        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filtrar(comboBox2.SelectedItem.ToString(), textBox1.Text.Trim()); // Llama al método Filtrar con el valor del TextBox
        }
    }
}
