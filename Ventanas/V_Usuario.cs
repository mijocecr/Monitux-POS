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
        public int Secuencial_Usuario { get; set; } = V_Menu_Principal.Secuencial_Usuario;
        int Secuencial = 0;
        public byte[]? Imagen { get; set; }
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
            this.Text = "Monitux-POS v." + V_Menu_Principal.VER; // Establece el título del formulario
            Secuencial_Usuario = V_Menu_Principal.Secuencial_Usuario;

            if (V_Menu_Principal.Acceso_Usuario != "Administrador")
            {
                Menu_Eliminar.Visible = false; // Oculta el botón de eliminar si el usuario no es administrador
            }
            else
            {
                Menu_Eliminar.Visible = true; // Muestra el botón de eliminar si el usuario es administrador
            }

            dataGridView1.Rows.Clear();

            Cargar_Datos();

            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            comboBox1.Items.Add("Vendedor");
            comboBox1.Items.Add("Administrador");

            comboBox1.Items.Add("Almacen");

            comboBox1.SelectedIndex = 0;
            dataGridView1.ReadOnly = true; // Hace que el DataGridView sea de solo lectura
            comboBox2.Items.Add("Codigo");
            comboBox2.Items.Add("Nombre");
            comboBox2.SelectedIndex = 0; // Selecciona el primer elemento del comboBox2



            if (dataGridView1.Rows.Count > 0)
            {
                // Seleccionar visualmente la primera fila
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];

                // Simular el evento CellClick manualmente
                DataGridViewCellEventArgs args = new DataGridViewCellEventArgs(0, 0);
                dataGridView1_CellClick(dataGridView1, args); // <- llama tu propio método
            }

        }


        private void Cargar_Datos()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var usuarios = context.Usuarios
                .Where(u => u.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
                .ToList();

            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.Columns.Add("Secuencial", "S");
            dataGridView1.Columns["Secuencial"].Width = 20;

            dataGridView1.Columns.Add("Codigo", "Código");
            dataGridView1.Columns["Codigo"].Width = 80;

            dataGridView1.Columns.Add("Nombre", "Nombre");
            dataGridView1.Columns["Nombre"].Width = 80;

            dataGridView1.Columns.Add("Password", "Password");
            dataGridView1.Columns["Password"].Width = 60;
            dataGridView1.Columns["Password"].Visible = false;

            var colImagen = new DataGridViewTextBoxColumn
            {
                Name = "Imagen",
                HeaderText = "Imagen",
                Visible = false // Oculta la columna de imagen binaria
            };
            dataGridView1.Columns.Add(colImagen);

            dataGridView1.Columns.Add("Acceso", "Acceso");
            dataGridView1.Columns["Acceso"].Width = 150;

            dataGridView1.Columns.Add("Activo", "Activo");

            foreach (var item in usuarios)
            {
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Codigo,
                    item.Nombre,
                    item.Password,
                    item.Imagen, // byte[] se guarda internamente
                    item.Acceso,
                    item.Activo ? "Si" : "No"
                );
            }
        }




        private void pictureBox1_Click(object sender, EventArgs e)
        {

            try
            {
                string imagenSeleccionada = Util.Abrir_Dialogo_Seleccion_URL();
                if (!string.IsNullOrWhiteSpace(imagenSeleccionada))
                {
                    Image imagenCargada = Util.Cargar_Imagen_Local(imagenSeleccionada);
                    pictureBox1.Image = imagenCargada;

                    using var ms = new MemoryStream();
                    imagenCargada.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    Imagen = ms.ToArray(); // Guarda la imagen como byte[]
                }
            }
            catch
            {
                Imagen = null; // Si falla, no se asigna imagen
                V_Menu_Principal.MSG.ShowMSG("No se pudo cargar la imagen seleccionada.", "Error");
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
                    string password = dataGridView1.Rows[e.RowIndex].Cells["Password"].Value.ToString();
                    txt_Password.Text = Util.Encriptador.Desencriptar(password);
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Acceso"].Value != null)
                {
                    string acceso = dataGridView1.Rows[e.RowIndex].Cells["Acceso"].Value.ToString();
                    foreach (var item in comboBox1.Items)
                    {
                        if (item.ToString().Contains(acceso))
                        {
                            comboBox1.SelectedItem = item;
                            break;
                        }
                    }
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Activo"].Value != null)
                {
                    checkBox1.Checked = dataGridView1.Rows[e.RowIndex].Cells["Activo"].Value.ToString() == "Si";
                }

                // Cargar imagen desde la base de datos como byte[]
                SQLitePCL.Batteries.Init();
                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var usuario = context.Usuarios.FirstOrDefault(u =>
                    u.Secuencial == this.Secuencial &&
                    u.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

                if (usuario != null && usuario.Imagen != null && usuario.Imagen.Length > 0)
                {
                    try
                    {
                        using var ms = new MemoryStream(usuario.Imagen);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                    catch
                    {
                        pictureBox1.Image = null;
                    }
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            catch
            {
                pictureBox1.Image = null;
            }



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Menu_Guardar_Click(object sender, EventArgs e)
        {


            byte[] imagenBytes = null;
            if (pictureBox1.Image != null)
            {
                imagenBytes = Util.ComprimirImagen(pictureBox1.Image, 40L); // Calidad ajustable
            }

            if (Secuencial != 0) // MODO EDICIÓN
            {
                if (string.IsNullOrWhiteSpace(txt_Nombre.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El nombre del usuario no puede estar vacío.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Codigo.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El código de usuario no puede estar vacío.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Password.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El password no puede estar vacío.", "Error");
                    return;
                }

                SQLitePCL.Batteries.Init();
                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var usuario = context.Usuarios.FirstOrDefault(p => p.Secuencial == this.Secuencial);
                if (usuario != null)
                {
                    usuario.Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa;
                    usuario.Nombre = txt_Nombre.Text;
                    usuario.Codigo = txt_Codigo.Text;
                    usuario.Password = Util.Encriptador.Encriptar(txt_Password.Text);
                    usuario.Acceso = comboBox1.SelectedItem?.ToString() ?? "Sin Tipo";
                    usuario.Activo = checkBox1.Checked;
                    usuario.Imagen = imagenBytes ?? usuario.Imagen; // Actualiza si hay nueva imagen

                    context.SaveChanges();
                    Util.Registrar_Actividad(Secuencial_Usuario, $"Ha modificado al usuario: {usuario.Nombre}", V_Menu_Principal.Secuencial_Empresa);
                    V_Menu_Principal.MSG.ShowMSG("Usuario actualizado correctamente.", "Éxito");
                }
            }
            else // MODO CREACIÓN
            {
                if (comboBox1.SelectedIndex == -1)
                {
                    V_Menu_Principal.MSG.ShowMSG("Debe seleccionar un acceso de usuario.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Nombre.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El nombre del usuario no puede estar vacío.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Codigo.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El código no puede estar vacío.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Password.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El password no puede estar vacío.", "Error");
                    return;
                }

                SQLitePCL.Batteries.Init();
                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var usuario = new Usuario
                {
                    Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa,
                    Nombre = txt_Nombre.Text,
                    Codigo = txt_Codigo.Text,
                    Password = Util.Encriptador.Encriptar(txt_Password.Text),
                    Acceso = comboBox1.SelectedItem?.ToString() ?? "Sin Tipo",
                    Activo = true,
                    Imagen = imagenBytes // Comprimida o null
                };

                try
                {
                    context.Usuarios.Add(usuario);
                    context.SaveChanges();
                    V_Menu_Principal.MSG.ShowMSG("Usuario creado correctamente.", "Éxito");
                    Util.Registrar_Actividad(Secuencial_Usuario, $"Ha creado al usuario: {usuario.Nombre}", V_Menu_Principal.Secuencial_Empresa);
                }
                catch (Exception)
                {
                    V_Menu_Principal.MSG.ShowMSG("Error al crear usuario: Ya existe o los datos proporcionados no son válidos.", "Error");
                    return;
                }
            }

            Cargar_Datos(); // Refresca la vista





        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txt_Codigo.Text = "";
            txt_Nombre.Text = "";
            txt_Password.Text = "";
            pictureBox1.Image = null; // Limpia la imagen
            
            Secuencial = 0; // Reinicia el secuencial
            comboBox1.SelectedIndex = 0; // Selecciona el primer elemento del comboBox
            checkBox1.Checked = true; // Marca el checkbox como activo

        }

        private void Menu_Eliminar_Click(object sender, EventArgs e)
        {


            var res = V_Menu_Principal.MSG.ShowMSG("¿Está seguro de eliminar este usuario?", "Confirmación");

            if (res == DialogResult.Yes)
            {
                try
                {
                    pictureBox1.Image?.Dispose(); // Libera la imagen si existe
                    pictureBox1.Image = null;
                    Imagen = null;
                }
                catch
                {
                    // Silenciar errores de liberación de imagen
                }

                SQLitePCL.Batteries.Init();
                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var usuario = context.Usuarios.FirstOrDefault(p =>
                    p.Secuencial == this.Secuencial &&
                    p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

                if (usuario != null)
                {
                    context.Usuarios.Remove(usuario);
                    context.SaveChanges();

                    Util.Registrar_Actividad(Secuencial_Usuario, $"Ha eliminado al usuario: {usuario.Nombre}", V_Menu_Principal.Secuencial_Empresa);
                    V_Menu_Principal.MSG.ShowMSG("Usuario eliminado correctamente.", "Éxito");
                    Cargar_Datos();
                }
            }




        }




        private void Filtrar(string campo, string valor)
        {
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            // Obtener usuarios filtrados por el campo y valor especificado
            var usuarios = context.Usuarios
                .Where(u => EF.Property<string>(u, campo).Contains(valor) &&
                            u.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
                .ToList();

            // Configurar columnas si aún no existen
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("Secuencial", "S");
                dataGridView1.Columns["Secuencial"].Width = 20;

                dataGridView1.Columns.Add("Codigo", "Código");
                dataGridView1.Columns["Codigo"].Width = 80;

                dataGridView1.Columns.Add("Nombre", "Nombre");
                dataGridView1.Columns["Nombre"].Width = 80;

                dataGridView1.Columns.Add("Password", "Password");
                dataGridView1.Columns["Password"].Width = 60;
                dataGridView1.Columns["Password"].Visible = false;

                dataGridView1.Columns.Add("Activo", "Activo");
                
            }

            // Limpiar y cargar resultados
            dataGridView1.Rows.Clear();
            foreach (var u in usuarios)
            {
                dataGridView1.Rows.Add(
                    u.Secuencial,
                    u.Codigo,
                    u.Nombre,
                    u.Password,
                    u.Activo ? "Sí" : "No"
                    
                );
            }

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
                    string password = dataGridView1.Rows[e.RowIndex].Cells["Password"].Value.ToString();
                    txt_Password.Text = Util.Encriptador.Desencriptar(password);
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Acceso"].Value != null)
                {
                    string acceso = dataGridView1.Rows[e.RowIndex].Cells["Acceso"].Value.ToString();
                    foreach (var item in comboBox1.Items)
                    {
                        if (item.ToString().Contains(acceso))
                        {
                            comboBox1.SelectedItem = item;
                            break;
                        }
                    }
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["Activo"].Value != null)
                {
                    checkBox1.Checked = dataGridView1.Rows[e.RowIndex].Cells["Activo"].Value.ToString() == "Si";
                }

                // Cargar imagen desde la base de datos como byte[]
                SQLitePCL.Batteries.Init();
                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var usuario = context.Usuarios.FirstOrDefault(u =>
                    u.Secuencial == this.Secuencial &&
                    u.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

                if (usuario != null && usuario.Imagen != null && usuario.Imagen.Length > 0)
                {
                    try
                    {
                        using var ms = new MemoryStream(usuario.Imagen);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                    catch
                    {
                        pictureBox1.Image = null;
                    }
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            catch
            {
                pictureBox1.Image = null;
            }



        }

        private void txt_Codigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void txt_Password_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Nombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            V_Captura_Imagen capturaImagen = new V_Captura_Imagen(Secuencial);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
