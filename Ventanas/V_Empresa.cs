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
    public partial class V_Empresa : Form
    {


        public int Secuencial { get; set; }
        public string Imagen { get; set; } = string.Empty;
        public int Secuencial_Usuario { get; set; } = V_Menu_Principal.Secuencial_Usuario;
        public V_Empresa()
        {
            InitializeComponent();
        }




        private void Cargar_Datos()
        {
            try
            {
                // Inicializar componentes
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Inicializar SQLite
                SQLitePCL.Batteries.Init();

                // Crear contexto y asegurar base de datos
                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                // Obtener datos de empresas
                var empresas = context.Empresas.ToList();

                // Definir columnas del DataGridView
                dataGridView1.Columns.Add("Secuencial", "S");


                dataGridView1.Columns.Add("Nombre", "Nombre");


                dataGridView1.Columns.Add("Direccion", "Dirección");


                dataGridView1.Columns.Add("Telefono", "Teléfono");


                dataGridView1.Columns.Add("Email", "Email");


                dataGridView1.Columns.Add("ISV", "ISV");


                dataGridView1.Columns.Add("Moneda", "Moneda");



                dataGridView1.Columns.Add("Activa", "Activa");
                dataGridView1.Columns.Add("Imagen", "Imagen");

                dataGridView1.Columns.Add("RSS", "RSS");

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


                // Agregar filas con datos de empresas
                foreach (var item in empresas)
                {
                    dataGridView1.Rows.Add(
                        item.Secuencial,
                        item.Nombre,
                        item.Direccion,
                        item.Telefono,
                        item.Email,
                        item.ISV.ToString("F2"), // Formatea el ISV a dos decimales
                        item.Moneda,

                    (bool)item.Activa ? "Si" : "No",

                        item.Imagen ?? "Sin imagen",
                        item.RSS ?? "Sin RSS"
                    );
                }
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Error al cargar datos: {ex.Message}", "Error");
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

            var empresa = context.Empresas
                    .Where(c => EF.Property<string>(c, columnaSeleccionada).Contains(valor))
                    .ToList();

            dataGridView1.Rows.Clear();
            foreach (var item in empresa)
            {


                dataGridView1.Rows.Add(
                        item.Secuencial,
                        item.Nombre,
                        item.Direccion,
                        item.Telefono,
                        item.Email,
                        item.ISV.ToString("F2"), // Formatea el ISV a dos decimales
                        item.Moneda,

                    (bool)item.Activa ? "Si" : "No",

                        item.Imagen ?? "Sin imagen",
                        item.RSS ?? "Sin RSS"
                    );


            }


            //-------------------Filtro que usare




            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selecciona toda la fila

            // Agregar columnas si no existen
            if (dataGridView1.Columns.Count == 0)
            {
                // Definir columnas del DataGridView
                dataGridView1.Columns.Add("Secuencial", "S");


                dataGridView1.Columns.Add("Nombre", "Nombre");


                dataGridView1.Columns.Add("Direccion", "Dirección");


                dataGridView1.Columns.Add("Telefono", "Teléfono");


                dataGridView1.Columns.Add("Email", "Email");


                dataGridView1.Columns.Add("ISV", "ISV");


                dataGridView1.Columns.Add("Moneda", "Moneda");



                dataGridView1.Columns.Add("Activa", "Activa");
                dataGridView1.Columns.Add("Imagen", "Imagen");

                dataGridView1.Columns.Add("RSS", "RSS");

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


            }

            // Limpiar filas antes de agregar nuevas
            dataGridView1.Rows.Clear();

            foreach (var item in empresa)
            {
                dataGridView1.Rows.Add(
                         item.Secuencial,
                         item.Nombre,
                         item.Direccion,
                         item.Telefono,
                         item.Email,
                         item.ISV.ToString("F2"), // Formatea el ISV a dos decimales
                         item.Moneda,

                     (bool)item.Activa ? "Si" : "No",

                         item.Imagen ?? "Sin imagen",
                         item.RSS ?? "Sin RSS"
                     );
            }


        }







        private void V_Empresa_Load(object sender, EventArgs e)

        {
            if (Properties.Settings.Default.Primer_Arranque)
            {
                Menu_Eliminar.Visible = false; // Oculta el botón de eliminar si es el primer arranque
            }



            combo_Moneda.Items.Add("L - Lempira Hondureño");
            combo_Moneda.Items.Add("$ - Dólar Estadounidense");
            combo_Moneda.Items.Add("€ - Euro");

            /*
            if (V_Menu_Principal.Acceso_Usuario != "Administrador")
            {
                Menu_Eliminar.Visible = false; // Oculta el botón de eliminar si el usuario no es administrador
            }
            else
            {
                Menu_Eliminar.Visible = true; // Muestra el botón de eliminar si el usuario es administrador
            }
            */

            this.Text = "Monitux-POS v." + V_Menu_Principal.VER; // Establece el título del formulario
            Cargar_Datos();
            dataGridView1.ReadOnly = true;
            comboBox2.Items.Add("Nombre");
            comboBox2.Items.Add("Telefono");
            comboBox2.Items.Add("Email");
            comboBox2.SelectedIndex = 0;
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


                if (dataGridView1.Rows[e.RowIndex].Cells["RSS"].Value != null)
                {
                    txt_RSS.Text = dataGridView1.Rows[e.RowIndex].Cells["RSS"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["ISV"].Value != null)
                {
                    txt_ISV.Text = dataGridView1.Rows[e.RowIndex].Cells["ISV"].Value.ToString();
                }



                if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells["Moneda"].Value != null)
                {
                    string valorMoneda = dataGridView1.Rows[e.RowIndex].Cells["Moneda"].Value.ToString().Trim();

                    // Verificar si el valor está en el ComboBox
                    var itemEncontrado = combo_Moneda.Items.Cast<string>()
                        .FirstOrDefault(item => item.StartsWith(valorMoneda));

                    if (itemEncontrado != null)
                    {
                        combo_Moneda.SelectedItem = itemEncontrado;
                    }
                    else
                    {
                        V_Menu_Principal.MSG.ShowMSG($"La moneda '{valorMoneda}' no se encuentra en el listado.", "Moneda desconocida");
                    }
                }






                if (dataGridView1.Rows[e.RowIndex].Cells["Activa"].Value != null)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["Activa"].Value.ToString() == "Si")
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


                if (dataGridView1.Rows[e.RowIndex].Cells["RSS"].Value != null)
                {
                    txt_RSS.Text = dataGridView1.Rows[e.RowIndex].Cells["RSS"].Value.ToString();
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["ISV"].Value != null)
                {
                    txt_ISV.Text = dataGridView1.Rows[e.RowIndex].Cells["ISV"].Value.ToString();
                }



                if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells["Moneda"].Value != null)
                {
                    string valorMoneda = dataGridView1.Rows[e.RowIndex].Cells["Moneda"].Value.ToString().Trim();

                    // Verificar si el valor está en el ComboBox
                    var itemEncontrado = combo_Moneda.Items.Cast<string>()
                        .FirstOrDefault(item => item.StartsWith(valorMoneda));

                    if (itemEncontrado != null)
                    {
                        combo_Moneda.SelectedItem = itemEncontrado;
                    }
                    else
                    {
                        V_Menu_Principal.MSG.ShowMSG($"La moneda '{valorMoneda}' no se encuentra en el listado.", "Moneda desconocida");
                    }
                }






                if (dataGridView1.Rows[e.RowIndex].Cells["Activa"].Value != null)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["Activa"].Value.ToString() == "Si")
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


            string rutaGuardado = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\EMP\\" + V_Menu_Principal.Secuencial_Empresa + "-Emp - " + Secuencial + ".PNG");


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

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            V_Captura_Imagen capturaImagen = new V_Captura_Imagen(Secuencial);

            capturaImagen.ShowDialog();
            Bitmap imagenCapturada = V_Captura_Imagen.Get_Imagen();
            if (imagenCapturada != null)
            {
                pictureBox1.Image = imagenCapturada;
                string rutaGuardado = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\EMP\\" + V_Menu_Principal.Secuencial_Empresa + "-Emp - " + Secuencial + ".PNG");
                imagenCapturada.Save(rutaGuardado); // Guarda la imagen capturada en la ruta especificada
                Imagen = rutaGuardado; // Actualiza la variable Imagen con la ruta guardada
            }
            else
            {
                V_Menu_Principal.MSG.ShowMSG("No se ha capturado ninguna imagen.", "Error");
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Secuencial = 0;
            pictureBox1.Image = null;
            txt_Nombre.Text = "";
            txt_Telefono.Text = "";
            txt_Direccion.Text = "";
            txt_Email.Text = "";
            txt_ISV.Text = "";
            txt_RSS.Text = "https://www.tunota.com/rss/honduras-hoy.xml"; // Establece un valor por defecto para RSS
            combo_Moneda.SelectedIndex = -1;

            checkBox1.Checked = true; // Marca el checkbox como activo


        }

        private void Menu_Guardar_Click(object sender, EventArgs e)
        {

            //MessageBox.Show($"Secuencial actual: {Secuencial}");




            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txt_Nombre.Text))
            {
                V_Menu_Principal.MSG.ShowMSG("El nombre de la empresa no puede estar vacío.", "Error");
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_Telefono.Text))
            {
                V_Menu_Principal.MSG.ShowMSG("El teléfono no puede estar vacío.", "Error");
                return;
            }

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            if (Secuencial != 0) // MODO EDICIÓN
            {
                var empresa = context.Empresas.FirstOrDefault(p => p.Secuencial == this.Secuencial);
                if (empresa != null)
                {
                    empresa.Secuencial = Secuencial;
                    empresa.Nombre = txt_Nombre.Text;
                    empresa.Telefono = txt_Telefono.Text;
                    empresa.Direccion = txt_Direccion.Text;
                    empresa.Email = txt_Email.Text;
                    empresa.ISV = Convert.ToDouble(txt_ISV.Text);
                    empresa.RSS = txt_RSS.Text;
                    empresa.Moneda = combo_Moneda.SelectedItem.ToString().Split('-')[0].Trim();
                    empresa.Activa = checkBox1.Checked;
                    empresa.Imagen = Imagen;

                    context.SaveChanges();

                    Util.Registrar_Actividad(Secuencial_Usuario, $"Ha modificado la empresa: {empresa.Nombre}", V_Menu_Principal.Secuencial_Empresa);
                    V_Menu_Principal.MSG.ShowMSG("Empresa actualizada correctamente.", "Éxito");
                    Cargar_Datos();
                }
            }
            else // MODO CREACIÓN
            {
                var empresa = new Empresa
                {

                    Nombre = txt_Nombre.Text,
                    Telefono = txt_Telefono.Text,
                    Direccion = txt_Direccion.Text,
                    Email = txt_Email.Text,
                    ISV = Convert.ToDouble(txt_ISV.Text),
                    RSS = txt_RSS.Text,
                    Moneda = combo_Moneda.SelectedItem.ToString().Split('-')[0].Trim(),
                    Secuencial_Usuario = Secuencial_Usuario,
                    Activa = true,
                    Imagen = pictureBox1.Image != null ? Imagen : "Sin Imagen"
                };

                try
                {
                    context.Empresas.Add(empresa);
                    context.SaveChanges();
                    Properties.Settings.Default.Empresa_Creada = true; // Marca que se ha creado una empresa
                    Util.Registrar_Actividad(Secuencial_Usuario, $"Ha creado la empresa: {empresa.Nombre}", V_Menu_Principal.Secuencial_Empresa);
                    V_Menu_Principal.MSG.ShowMSG("Empresa creada correctamente.", "Éxito");
                    Cargar_Datos();
                }
                catch (Exception ex)
                {
                    V_Menu_Principal.MSG.ShowMSG("Error al crear la empresa: Ya existe o los datos no son válidos.\n" + "\n" + ex.InnerException, "Error");
                }


            }


            if (Properties.Settings.Default.Primer_Arranque)
            {
                Properties.Settings.Default.Primer_Arranque = false; // Cambia el valor a false para indicar que ya no es el primer arranque
                Properties.Settings.Default.Save(); // Guarda los cambios en la configuración
                this.Dispose();
            }


        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Eliminar_Click(object sender, EventArgs e)
        {




            var res = V_Menu_Principal.MSG.ShowMSG("¿Está seguro de eliminar esta empresa?", "Confirmación");

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

                var empresa = context.Empresas.FirstOrDefault(p => p.Secuencial == this.Secuencial);
                if (empresa != null)
                {
                    context.Empresas.Remove(empresa);
                    context.SaveChanges();
                    Util.Registrar_Actividad(Secuencial_Usuario, "Ha eliminado la empresa: " + empresa.Nombre, Secuencial);
                    V_Menu_Principal.MSG.ShowMSG("Empresa eliminada correctamente.", "Éxito");
                    Cargar_Datos(); // Recargar los datos después de eliminar el cliente
                }
            }





        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filtrar(comboBox2.SelectedItem.ToString(), textBox1.Text.Trim()); // Llama al método Filtrar con el valor del TextBox
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose(); // Cierra el formulario actual
        }
    }
}
