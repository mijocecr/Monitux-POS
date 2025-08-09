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
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Monitux_POS.Ventanas
{
    public partial class V_Proveedor : Form
    {
        public int Secuencial_Usuario { get; set; } = V_Menu_Principal.Secuencial_Usuario;

        int Secuencial = 0;
        public byte[]? Imagen { get; set; }
        public string Nombre { get; set; }
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
            context.Database.EnsureCreated();

            var proveedores = context.Proveedores
                .Where(p => p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
                .ToList();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.Columns.Add("Secuencial", "S");
            dataGridView1.Columns["Secuencial"].Width = 20;

            dataGridView1.Columns.Add("Nombre", "Nombre");
            dataGridView1.Columns["Nombre"].Width = 80;

            dataGridView1.Columns.Add("Telefono", "Teléfono");
            dataGridView1.Columns["Telefono"].Width = 60;

            dataGridView1.Columns.Add("Direccion", "Dirección");
            dataGridView1.Columns["Direccion"].Width = 150;

            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns["Email"].Width = 100;

            dataGridView1.Columns.Add("Contacto", "Contacto");
            dataGridView1.Columns["Contacto"].Width = 100;

            dataGridView1.Columns.Add("Tipo", "Tipo");
            dataGridView1.Columns["Tipo"].Width = 50;

            dataGridView1.Columns.Add("Activo", "Activo");

            // Columna oculta para la imagen (byte[])
            var colImagen = new DataGridViewTextBoxColumn
            {
                Name = "Imagen",
                HeaderText = "Imagen",
                Visible = false // Oculta la columna en la vista
            };
            dataGridView1.Columns.Add(colImagen);

            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
                    item.Imagen // byte[] se guarda internamente, no se muestra
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
                var fila = dataGridView1.Rows[e.RowIndex];

                if (fila.Cells["Secuencial"].Value != null)
                    this.Secuencial = Convert.ToInt32(fila.Cells["Secuencial"].Value);

                txt_Nombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
                txt_Telefono.Text = fila.Cells["Telefono"].Value?.ToString() ?? "";
                txt_Direccion.Text = fila.Cells["Direccion"].Value?.ToString() ?? "";
                txt_Email.Text = fila.Cells["Email"].Value?.ToString() ?? "";
                txt_Contacto.Text = fila.Cells["Contacto"].Value?.ToString() ?? "";

                string tipo = fila.Cells["Tipo"].Value?.ToString();
                if (!string.IsNullOrEmpty(tipo))
                {
                    foreach (var item in combo_Tipo.Items)
                    {
                        if (item.ToString().Contains(tipo))
                        {
                            combo_Tipo.SelectedItem = item;
                            break;
                        }
                    }
                }

                
                checkBox1.Checked = fila.Cells["Activo"].Value?.ToString() == "Si";

                // Cargar imagen desde base de datos (byte[])
                if (fila.Cells["Imagen"].Value != null && fila.Cells["Imagen"].Value is byte[] imagenBytes && imagenBytes.Length > 0)
                {
                    using var ms = new MemoryStream(imagenBytes);
                    pictureBox1.Image = Image.FromStream(ms);
                    Imagen = imagenBytes;
                }
                else
                {
                    pictureBox1.Image = null;
                    Imagen = null;
                }
            }
            catch (Exception ex)
            {
                pictureBox1.Image = null;
                Imagen = null;
                V_Menu_Principal.MSG.ShowMSG("Error al cargar los datos del proveedor: " + ex.Message, "Error");
            }



        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaImagen = Util.Abrir_Dialogo_Seleccion_URL();
                if (!string.IsNullOrWhiteSpace(rutaImagen))
                {
                    Image imagenCargada = Util.Cargar_Imagen_Local(rutaImagen);
                    pictureBox1.Image = imagenCargada;

                    using var ms = new MemoryStream();
                    imagenCargada.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    Imagen = ms.ToArray(); // Guardamos la imagen como byte[]
                }
            }
            catch (Exception ex)
            {
                Imagen = null;
                V_Menu_Principal.MSG.ShowMSG("Error al cargar la imagen: " + ex.Message, "Error");
            }
        }


        private void Menu_Guardar_Click(object sender, EventArgs e)
        {

            // Comprimir imagen si existe
            byte[] imagenBytes = null;
            if (pictureBox1.Image != null)
            {
                // imagenBytes = Util.ComprimirImagen(pictureBox1.Image, 40L); // Calidad ajustable

                using var imagenCopia = new Bitmap(pictureBox1.Image); // ✅ Clona la imagen
                imagenBytes = Util.ComprimirImagen(imagenCopia, 40L);

            }

            if (Secuencial != 0) // MODO EDICIÓN
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
                    V_Menu_Principal.MSG.ShowMSG("El teléfono no puede estar vacío.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Contacto.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El contacto no puede estar vacío.", "Error");
                    return;
                }

                SQLitePCL.Batteries.Init();
                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var proveedor = context.Proveedores.FirstOrDefault(p => p.Secuencial == this.Secuencial);
                if (proveedor != null)
                {
                    proveedor.Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa;
                    proveedor.Nombre = txt_Nombre.Text;
                    proveedor.Telefono = txt_Telefono.Text;
                    proveedor.Direccion = txt_Direccion.Text;
                    proveedor.Email = txt_Email.Text;
                    proveedor.Contacto = txt_Contacto.Text;
                    proveedor.Tipo = combo_Tipo.SelectedItem?.ToString() ?? "Sin Tipo";
                    proveedor.Activo = checkBox1.Checked;
                    proveedor.Imagen = imagenBytes ?? proveedor.Imagen;

                    context.SaveChanges();
                    Util.Registrar_Actividad(Secuencial_Usuario, $"Ha modificado al proveedor: {proveedor.Nombre}", V_Menu_Principal.Secuencial_Empresa);
                    V_Menu_Principal.MSG.ShowMSG("Proveedor actualizado correctamente.", "Éxito");
                    Cargar_Datos();
                }
            }
            else // MODO CREACIÓN
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
                    V_Menu_Principal.MSG.ShowMSG("El teléfono no puede estar vacío.", "Error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_Contacto.Text))
                {
                    V_Menu_Principal.MSG.ShowMSG("El contacto no puede estar vacío.", "Error");
                    return;
                }

                SQLitePCL.Batteries.Init();
                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                var proveedor = new Proveedor
                {
                    Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa,
                    Nombre = txt_Nombre.Text,
                    Telefono = txt_Telefono.Text,
                    Direccion = txt_Direccion.Text,
                    Email = txt_Email.Text,
                    Contacto = txt_Contacto.Text,
                    Tipo = combo_Tipo.SelectedItem?.ToString() ?? "Sin Tipo",
                    Activo = true,
                    Imagen = imagenBytes // Comprimida o null
                };

                context.Proveedores.Add(proveedor);
                context.SaveChanges();
                Util.Registrar_Actividad(Secuencial_Usuario, $"Ha creado al proveedor: {proveedor.Nombre}", V_Menu_Principal.Secuencial_Empresa);
                V_Menu_Principal.MSG.ShowMSG("Proveedor creado correctamente.", "Éxito");
                Cargar_Datos();
            }

            this.Dispose();

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

                var proveedor = context.Proveedores.FirstOrDefault(p =>
                    p.Secuencial == this.Secuencial &&
                    p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

                if (proveedor != null)
                {
                    context.Proveedores.Remove(proveedor);
                    context.SaveChanges();

                    Util.Registrar_Actividad(Secuencial_Usuario, $"Ha eliminado al proveedor: {proveedor.Nombre}", V_Menu_Principal.Secuencial_Empresa);
                    V_Menu_Principal.MSG.ShowMSG("Proveedor eliminado correctamente.", "Éxito");
                    Cargar_Datos(); // Refresca la vista
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
                var fila = dataGridView1.Rows[e.RowIndex];

                if (fila.Cells["Secuencial"].Value != null)
                    this.Secuencial = Convert.ToInt32(fila.Cells["Secuencial"].Value);

                txt_Nombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
                txt_Telefono.Text = fila.Cells["Telefono"].Value?.ToString() ?? "";
                txt_Direccion.Text = fila.Cells["Direccion"].Value?.ToString() ?? "";
                txt_Email.Text = fila.Cells["Email"].Value?.ToString() ?? "";
                txt_Contacto.Text = fila.Cells["Contacto"].Value?.ToString() ?? "";

                string tipo = fila.Cells["Tipo"].Value?.ToString();
                if (!string.IsNullOrEmpty(tipo))
                {
                    foreach (var item in combo_Tipo.Items)
                    {
                        if (item.ToString().Contains(tipo))
                        {
                            combo_Tipo.SelectedItem = item;
                            break;
                        }
                    }
                }


                checkBox1.Checked = fila.Cells["Activo"].Value?.ToString() == "Si";

                // Cargar imagen desde base de datos (byte[])
                if (fila.Cells["Imagen"].Value != null && fila.Cells["Imagen"].Value is byte[] imagenBytes && imagenBytes.Length > 0)
                {
                    using var ms = new MemoryStream(imagenBytes);
                    pictureBox1.Image = Image.FromStream(ms);
                    Imagen = imagenBytes;
                }
                else
                {
                    pictureBox1.Image = null;
                    Imagen = null;
                }
            }
            catch (Exception ex)
            {
                pictureBox1.Image = null;
                Imagen = null;
                V_Menu_Principal.MSG.ShowMSG("Error al cargar los datos del proveedor: " + ex.Message, "Error");
            }





        }




        private void Filtrar(string campo, string valor)
        {
            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var proveedores = context.Proveedores
                .Where(p => EF.Property<string>(p, campo).Contains(valor) &&
                            p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
                .ToList();

            // Inicializar columnas si no existen
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dataGridView1.Columns.Add("Secuencial", "S");
                dataGridView1.Columns["Secuencial"].Width = 20;

                dataGridView1.Columns.Add("Nombre", "Nombre");
                dataGridView1.Columns["Nombre"].Width = 80;

                dataGridView1.Columns.Add("Telefono", "Teléfono");
                dataGridView1.Columns["Telefono"].Width = 60;

                dataGridView1.Columns.Add("Direccion", "Dirección");
                dataGridView1.Columns["Direccion"].Width = 150;

                dataGridView1.Columns.Add("Email", "Email");
                dataGridView1.Columns["Email"].Width = 100;

                dataGridView1.Columns.Add("Contacto", "Contacto");
                dataGridView1.Columns["Contacto"].Width = 100;

                dataGridView1.Columns.Add("Tipo", "Tipo");
                dataGridView1.Columns["Tipo"].Width = 50;

                dataGridView1.Columns.Add("Activo", "Activo");

                var colImagen = new DataGridViewTextBoxColumn
                {
                    Name = "Imagen",
                    HeaderText = "Imagen",
                    Visible = false // Oculta la columna de imagen binaria
                };
                dataGridView1.Columns.Add(colImagen);
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
                    item.Imagen // byte[] se guarda internamente
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
            capturaImagen.ShowDialog(); // Muestra el formulario de captura

            Bitmap imagenCapturada = V_Captura_Imagen.Get_Imagen(); // Obtiene la imagen
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

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
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


                V_CTA_Proveedor v_CTA_Proveedor = new V_CTA_Proveedor(Secuencial, Nombre);

                v_CTA_Proveedor.Secuencial_Proveedor = Secuencial;

                v_CTA_Proveedor.ShowDialog();



            }
            catch
            {



            }


        }
    }
}
