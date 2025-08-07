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
        byte[]? Imagen;
        public int Secuencial_Usuario { get; set; } = V_Menu_Principal.Secuencial_Usuario;
        public string Nombre { get; set; }
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

            var cliente = context.Clientes
                .Where(c => c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
                .ToList();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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

            foreach (var item in cliente)
            {
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Codigo,
                    item.Nombre,
                    item.Telefono,
                    item.Direccion,
                    item.Email,
                    item.Activo == true ? "Sí" : "No",
                    item.Imagen != null && item.Imagen.Length > 0 ? "Imagen cargada" : "Sin imagen"
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


            this.Text = "Monitux-POS v." + V_Menu_Principal.VER; // Establece el título del formulario
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
                if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
                    return;

                if (dataGridView1.Rows[e.RowIndex].IsNewRow)
                    return;

                var row = dataGridView1.Rows[e.RowIndex];

                this.Secuencial = Convert.ToInt32(row.Cells["Secuencial"].Value);
                txt_Codigo.Text = row.Cells["Codigo"].Value?.ToString();
                txt_Nombre.Text = row.Cells["Nombre"].Value?.ToString();
                txt_Telefono.Text = row.Cells["Telefono"].Value?.ToString();
                txt_Direccion.Text = row.Cells["Direccion"].Value?.ToString();
                txt_Email.Text = row.Cells["Email"].Value?.ToString();
                //checkBox1.Checked = row.Cells["Activo"].Value?.ToString() == "Si";

                // Cargar imagen desde la base de datos
                using var context = new Monitux_DB_Context();
                var cliente = context.Clientes.FirstOrDefault(c => c.Secuencial == this.Secuencial && c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

                checkBox1.Checked = cliente?.Activo ?? true; // Asegura que el checkbox esté marcado si el cliente está activo
                if (cliente?.Imagen != null && cliente.Imagen.Length > 0)
                {
                    using var ms = new MemoryStream(cliente.Imagen);
                    pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                    Imagen = cliente.Imagen;
                }
                else
                {
                    pictureBox1.Image = null;
                    Imagen = null;
                }
            }
            catch
            {
                pictureBox1.Image = null;
                Imagen = null;
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {


            try
            {
                string imagenSeleccionada = Util.Abrir_Dialogo_Seleccion_URL();

                if (!string.IsNullOrEmpty(imagenSeleccionada))
                {
                    System.Drawing.Image imagenCargada = System.Drawing.Image.FromFile(imagenSeleccionada);
                    pictureBox1.Image = imagenCargada;

                    using var ms = new MemoryStream();
                    imagenCargada.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    Imagen = ms.ToArray(); // Guarda la imagen como byte[]
                }
            }
            catch
            {
                pictureBox1.Image = null;
                Imagen = null;
            }




        }

        private void Menu_Guardar_Click(object sender, EventArgs e)
        {


            // Comprimir imagen si existe
            byte[] imagenBytes = null;
            if (pictureBox1.Image != null)
            {
                imagenBytes = Util.ComprimirImagen(pictureBox1.Image, 40L); // Calidad ajustable
            }

            if (Secuencial != 0)
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txt_Codigo.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El código del cliente no puede estar vacío.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Nombre.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El nombre del cliente no puede estar vacío.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Telefono.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El teléfono no puede estar vacío.", "Error");
                    return;
                }

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var cliente = context.Clientes.FirstOrDefault(p => p.Secuencial == this.Secuencial && p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);
                if (cliente != null)
                {
                    cliente.Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa;
                    cliente.Codigo = txt_Codigo.Text;
                    cliente.Nombre = txt_Nombre.Text;
                    cliente.Telefono = txt_Telefono.Text;
                    cliente.Direccion = txt_Direccion.Text;
                    cliente.Email = txt_Email.Text;
                    cliente.Activo = checkBox1.Checked;
                    cliente.Imagen = imagenBytes ?? cliente.Imagen;

                    context.SaveChanges();
                    Util.Registrar_Actividad(Secuencial_Usuario, $"Ha modificado el cliente: {cliente.Nombre}", V_Menu_Principal.Secuencial_Empresa);
                    V_Menu_Principal.MSG.ShowMSG("Cliente actualizado correctamente.", "Éxito");
                    Cargar_Datos();
                }
            }
            else
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txt_Nombre.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El nombre del cliente no puede estar vacío.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Telefono.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El teléfono no puede estar vacío.", "Error");
                    return;
                }

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var cliente = new Cliente
                {
                    Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa,
                    Codigo = txt_Codigo.Text,
                    Nombre = txt_Nombre.Text,
                    Telefono = txt_Telefono.Text,
                    Direccion = txt_Direccion.Text,
                    Email = txt_Email.Text,
                    Activo = true,
                    Imagen = imagenBytes // Comprimida o null
                };

                context.Clientes.Add(cliente);
                context.SaveChanges();
                Util.Registrar_Actividad(Secuencial_Usuario, $"Ha creado el cliente: {cliente.Nombre}", V_Menu_Principal.Secuencial_Empresa);
                V_Menu_Principal.MSG.ShowMSG("Cliente creado correctamente.", "Éxito");
                Cargar_Datos();
            }



        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
                    return;

                if (dataGridView1.Rows[e.RowIndex].IsNewRow)
                    return;

                var row = dataGridView1.Rows[e.RowIndex];

                this.Secuencial = Convert.ToInt32(row.Cells["Secuencial"].Value);
                txt_Codigo.Text = row.Cells["Codigo"].Value?.ToString();
                txt_Nombre.Text = row.Cells["Nombre"].Value?.ToString();
                txt_Telefono.Text = row.Cells["Telefono"].Value?.ToString();
                txt_Direccion.Text = row.Cells["Direccion"].Value?.ToString();
                txt_Email.Text = row.Cells["Email"].Value?.ToString();
                //checkBox1.Checked = row.Cells["Activo"].Value?.ToString() == "Si";

                // Cargar imagen desde la base de datos
                using var context = new Monitux_DB_Context();
                var cliente = context.Clientes.FirstOrDefault(c => c.Secuencial == this.Secuencial && c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

                checkBox1.Checked = cliente?.Activo ?? true; // Asegura que el checkbox esté marcado si el cliente está activo
                if (cliente?.Imagen != null && cliente.Imagen.Length > 0)
                {
                    using var ms = new MemoryStream(cliente.Imagen);
                    pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                    Imagen = cliente.Imagen;
                }
                else
                {
                    pictureBox1.Image = null;
                    Imagen = null;
                }
            }
            catch
            {
                pictureBox1.Image = null;
                Imagen = null;
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
                try
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose(); // Libera la imagen del PictureBox
                        pictureBox1.Image = null;
                    }

                    Imagen = null; // Limpia también la imagen en memoria
                }
                catch
                {
                    // Puedes registrar el error si lo deseas
                }

                SQLitePCL.Batteries.Init();

                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var cliente = context.Clientes.FirstOrDefault(p =>
                    p.Secuencial == this.Secuencial &&
                    p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

                if (cliente != null)
                {
                    string nombreCliente = cliente.Nombre;

                    context.Clientes.Remove(cliente);
                    context.SaveChanges();

                    Util.Registrar_Actividad(Secuencial_Usuario, $"Ha eliminado al cliente: {nombreCliente}", V_Menu_Principal.Secuencial_Empresa);
                    V_Menu_Principal.MSG.ShowMSG("Cliente eliminado correctamente.", "Éxito");
                    Cargar_Datos();
                }
                else
                {
                    V_Menu_Principal.MSG.ShowMSG("No se encontró el cliente a eliminar.", "Error");
                }
            }


        }




        private void Filtrar(string campo, string valor)
        {
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            string columnaSeleccionada = campo;

            var clientes = context.Clientes
                .Where(c => EF.Property<string>(c, columnaSeleccionada).Contains(valor) &&
                            c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
                .ToList();

            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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
                    item.Activo == true ? "Sí" : "No"
                   
                );
            }
        }





        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filtrar(comboBox2.SelectedItem.ToString(), textBox1.Text.Trim()); // Llama al método Filtrar con el valor del TextBox
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            V_Captura_Imagen capturaImagen = new V_Captura_Imagen(Secuencial);
            capturaImagen.titulo = txt_Codigo.Text;
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

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e) 
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {



            try
            {





                if (dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value != null)
                {
                    this.Secuencial = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value);


                }


                if (dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value != null)
                {
                    this.Nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value?.ToString();


                }


                V_CTA_Cliente v_CTA_Cliente = new V_CTA_Cliente(Secuencial, Nombre);

                v_CTA_Cliente.Secuencial_Cliente=Secuencial;
                
                v_CTA_Cliente.ShowDialog();



            }
            catch
            {



            }


        }
    }
}
