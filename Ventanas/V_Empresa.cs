using Microsoft.EntityFrameworkCore;
using Monitux_POS.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitux_POS.Ventanas
{
    public partial class V_Empresa : Form
    {


        public int Secuencial { get; set; }
        public byte[]? Imagen { get; set; }
        public int Secuencial_Usuario { get; set; } = V_Menu_Principal.Secuencial_Usuario;
        public int suma = 0; // Variable para la suma de números aleatorios
        public string Pin { get; set; } = ""; // Variable para el PIN
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
                        item.ISV.ToString("F2"),
                        item.Moneda,
                        item.Activa ? "Sí" : "No",
                        
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
            context.Database.EnsureCreated();

            // Filtrar empresas por el campo y valor proporcionado
            var empresasFiltradas = context.Empresas
                .Where(c => EF.Property<string>(c, campo).Contains(valor))
                .ToList();

            // Configurar DataGridView si aún no tiene columnas
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("Secuencial", "S");
                dataGridView1.Columns.Add("Nombre", "Nombre");
                dataGridView1.Columns.Add("Direccion", "Dirección");
                dataGridView1.Columns.Add("Telefono", "Teléfono");
                dataGridView1.Columns.Add("Email", "Email");
                dataGridView1.Columns.Add("ISV", "ISV");
                dataGridView1.Columns.Add("Moneda", "Moneda");
                dataGridView1.Columns.Add("Activa", "Activa");
                
                dataGridView1.Columns.Add("RSS", "RSS");

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }

            // Limpiar filas antes de agregar nuevas
            dataGridView1.Rows.Clear();

            foreach (var item in empresasFiltradas)
            {
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Nombre,
                    item.Direccion,
                    item.Telefono,
                    item.Email,
                    item.ISV.ToString("F2"),
                    item.Moneda,
                    item.Activa ? "Sí" : "No",
                    
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
                var fila = dataGridView1.Rows[e.RowIndex];

                if (fila.Cells["Secuencial"].Value != null)
                    this.Secuencial = Convert.ToInt32(fila.Cells["Secuencial"].Value);

                txt_Nombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
                txt_Telefono.Text = fila.Cells["Telefono"].Value?.ToString() ?? "";
                txt_Direccion.Text = fila.Cells["Direccion"].Value?.ToString() ?? "";
                txt_Email.Text = fila.Cells["Email"].Value?.ToString() ?? "";
                txt_RSS.Text = fila.Cells["RSS"].Value?.ToString() ?? "";
                txt_ISV.Text = fila.Cells["ISV"].Value?.ToString() ?? "";
                

                if (fila.Cells["Moneda"].Value != null)
                {
                    string valorMoneda = fila.Cells["Moneda"].Value.ToString().Trim();
                    var itemEncontrado = combo_Moneda.Items.Cast<string>()
                        .FirstOrDefault(item => item.StartsWith(valorMoneda));

                    combo_Moneda.SelectedItem = itemEncontrado ?? combo_Moneda.Items[0];
                }

                //checkBox1.Checked = fila.Cells["Activa"].Value?.ToString() == "Si";

                // Cargar imagen desde la base de datos
                using var context = new Monitux_DB_Context();
                var empresa = context.Empresas.FirstOrDefault(p =>
                    p.Secuencial == this.Secuencial);

                checkBox1.Checked = empresa?.Activa ?? true; // Marca el checkbox según el estado de la empresa
                if (empresa?.Imagen != null && empresa.Imagen.Length > 0)
                {
                    using var ms = new MemoryStream(empresa.Imagen);
                    pictureBox1.Image = Image.FromStream(ms);
                    Imagen = empresa.Imagen; // Guarda la imagen como byte[]
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
            }




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
                txt_RSS.Text = fila.Cells["RSS"].Value?.ToString() ?? "";
                txt_ISV.Text = fila.Cells["ISV"].Value?.ToString() ?? "";


                if (fila.Cells["Moneda"].Value != null)
                {
                    string valorMoneda = fila.Cells["Moneda"].Value.ToString().Trim();
                    var itemEncontrado = combo_Moneda.Items.Cast<string>()
                        .FirstOrDefault(item => item.StartsWith(valorMoneda));

                    combo_Moneda.SelectedItem = itemEncontrado ?? combo_Moneda.Items[0];
                }

                //checkBox1.Checked = fila.Cells["Activa"].Value?.ToString() == "Si";

                // Cargar imagen desde la base de datos
                using var context = new Monitux_DB_Context();
                var empresa = context.Empresas.FirstOrDefault(p =>
                    p.Secuencial == this.Secuencial);

                checkBox1.Checked = empresa?.Activa ?? true; // Marca el checkbox según el estado de la empresa
                if (empresa?.Imagen != null && empresa.Imagen.Length > 0)
                {
                    using var ms = new MemoryStream(empresa.Imagen);
                    pictureBox1.Image = Image.FromStream(ms);
                    Imagen = empresa.Imagen; // Guarda la imagen como byte[]
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
            }






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
                pictureBox1.Image = null;
                Imagen = null;
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

                using var ms = new MemoryStream();
                imagenCapturada.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                Imagen = ms.ToArray(); // Guarda la imagen como byte[]
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

            string texto = txt_ISV.Text.Replace(",", ".");

            // Comprimir imagen si existe
            byte[] imagenBytes = null;
            if (pictureBox1.Image != null)
            {
                imagenBytes = Util.ComprimirImagen(pictureBox1.Image, 40L); // Calidad ajustable
            }

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
                    empresa.Nombre = txt_Nombre.Text;
                    empresa.Telefono = txt_Telefono.Text;
                    empresa.Direccion = txt_Direccion.Text;
                    empresa.Email = txt_Email.Text;
                    empresa.ISV = double.Parse(texto, CultureInfo.InvariantCulture);
                    empresa.RSS = txt_RSS.Text;
                    empresa.Moneda = combo_Moneda.SelectedItem.ToString().Split('-')[0].Trim();
                    empresa.Activa = checkBox1.Checked;
                    empresa.Imagen = imagenBytes ?? empresa.Imagen; // Actualiza si hay nueva imagen

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
                    ISV = double.Parse(texto, CultureInfo.InvariantCulture),
                    RSS = txt_RSS.Text,
                    Moneda = combo_Moneda.SelectedItem.ToString().Split('-')[0].Trim(),
                    Secuencial_Usuario = Secuencial_Usuario,
                    Activa = true,
                    Imagen = imagenBytes // Comprimida o null
                };

                try
                {
                    context.Empresas.Add(empresa);
                    context.SaveChanges();
                    Properties.Settings.Default.Empresa_Creada = true;
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
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }



        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label6.ForeColor = Color.White;
            label6.Text="Empresas";
        }

        private void Menu_Eliminar_Click(object sender, EventArgs e)
        {

            if (V_Menu_Principal.IPB.Show("Ingrese el Pin", "Usuario Administrador", out string respuesta) == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(Pin))
                {
                    V_Menu_Principal.MSG.ShowMSG("Debe generar un PIN para eliminar la empresa.", "Error");
                    return;
                }

                if (respuesta == Pin)
                {
                    var res = V_Menu_Principal.MSG.ShowMSG("¿Está seguro de eliminar esta empresa?", "Confirmación");

                    if (res == DialogResult.Yes)
                    {
                        try
                        {
                            pictureBox1.Image?.Dispose(); // Libera la imagen si existe
                            pictureBox1.Image = null;
                            Imagen = null;
                        }
                        catch { }

                        SQLitePCL.Batteries.Init();
                        using var context = new Monitux_DB_Context();
                        context.Database.EnsureCreated();

                        var empresa = context.Empresas.FirstOrDefault(p => p.Secuencial == this.Secuencial);
                        if (empresa != null)
                        {
                            context.Empresas.Remove(empresa);
                            context.SaveChanges();

                            Util.Registrar_Actividad(Secuencial_Usuario, $"Ha eliminado la empresa: {empresa.Nombre}", Secuencial);
                            V_Menu_Principal.MSG.ShowMSG("Empresa eliminada correctamente.", "Éxito");
                            Cargar_Datos();
                        }
                    }
                }
                else
                {
                    V_Menu_Principal.MSG.ShowMSG("Pin incorrecto. La empresa no se eliminó.", "Información");
                    return;
                }
            }

            Pin = ""; // Reinicia el PIN


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filtrar(comboBox2.SelectedItem.ToString(), textBox1.Text.Trim()); // Llama al método Filtrar con el valor del TextBox
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose(); // Cierra el formulario actual
        }

        private void txt_ISV_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Permitir solo dígitos, retroceso y punto
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Bloquea el carácter
            }

            // Solo un punto decimal permitido
            if (e.KeyChar == '.' && (sender as System.Windows.Forms.TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {
            suma = suma + 1; // Incrementa la suma de números aleatorios
            if (suma >= 10)
            {


                Random random = new Random();
                this.Pin = random.Next(1000, 10000).ToString() + "*"; // Genera un número entre 1000 y 9999
                label6.ForeColor = Color.Green; // Cambia el color del texto a rojo
                label6.Text = "PIN: " + this.Pin; // Muestra el PIN generado en la etiqueta
                suma = 0; // Reinicia la suma a 0 después de generar el PIN


            }
        }
    }
}
