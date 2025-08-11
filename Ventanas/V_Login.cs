using Monitux_POS.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Monitux_POS.Clases.Util;
using static System.Net.Mime.MediaTypeNames;

namespace Monitux_POS.Ventanas
{
    public partial class V_Login : Form
    {
        bool isAdmin = false; // Variable para verificar si el usuario es administrador
        public int suma = 0; // Variable para almacenar la suma de los números aleatorios
        public string Pin = "****"; // Variable para almacenar el PIN del usuario
        public static string Imagen = "Sin Imagen"; // Variable para almacenar la imagen del usuario
        public int Secuencial = 0; // Variable para almacenar el secuencial del usuario
        public V_Login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            pictureBox1.Image = Util.Cargar_Imagen_Local(Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Iconos\\mostrar.PNG"));

            txtPassword.PasswordChar = '\0'; // Mostrar la contraseña
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.PasswordChar = '*'; // Mostrar la contraseña
            pictureBox1.Image = Util.Cargar_Imagen_Local(Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Iconos\\ocultar.PNG"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel4.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {


            if (comboEmpresa.SelectedItem == null)
            {
                V_Menu_Principal.MSG.ShowMSG("Debe seleccionar una empresa para crear el usuario.", "Error");
                return;
            }

            if (string.IsNullOrEmpty(txt_Codigo.Text))
            {
                V_Menu_Principal.MSG.ShowMSG("Debe ingresar un código para el usuario.", "Error");
                return;
            }

            if (string.IsNullOrEmpty(txt_Nombre.Text))
            {
                V_Menu_Principal.MSG.ShowMSG("Debe ingresar un nombre para el usuario.", "Error");
                return;
            }

            if (string.IsNullOrEmpty(txt_Password.Text))
            {
                V_Menu_Principal.MSG.ShowMSG("Debe ingresar una contraseña para el usuario.", "Error");
                return;
            }

            if (txt_Password.Text.Length < 4)
            {
                V_Menu_Principal.MSG.ShowMSG("La contraseña debe tener al menos 4 caracteres.", "Error");
                return;
            }

            if (comboBox1.SelectedItem == null)
            {
                V_Menu_Principal.MSG.ShowMSG("Debe seleccionar un tipo de acceso para el usuario.", "Error");
                return;
            }

            label11.ForeColor = Color.White;
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();
            Secuencial = context.Usuarios.Any() ? context.Usuarios.Max(p => p.Secuencial) + 1 : 1;

            // Comprimir imagen del PictureBox a byte[]
            byte[] imagenBytes = null;
            if (pictureBox3.Image != null)
            {
                imagenBytes = ComprimirImagen(pictureBox3.Image, 50L); // compresión al 50%
            }

            // Preparar nuevo usuario
            var usuario = new Usuario
            {
                Nombre = txt_Nombre.Text.Trim(),
                Codigo = txt_Codigo.Text.Trim(),
                Password = Util.Encriptador.Encriptar(txt_Password.Text),
                Acceso = "Vendedor",
                Activo = true,
                Imagen = imagenBytes,
                Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa
            };

            // Validar si se seleccionó "Administrador"
            if (comboBox1.SelectedItem?.ToString() == "Administrador")
            {
                if (Properties.Settings.Default.Primer_Arranque)
                {
                    usuario.Acceso = "Administrador";
                }
                else
                {
                    if (V_Menu_Principal.IPB.Show("Ingrese el Pin", "Usuario Administrador", out string respuesta) == DialogResult.OK)
                    {
                        if (respuesta == "****" && Pin == "*****")
                        {
                            V_Menu_Principal.MSG.ShowMSG("Debe generar un PIN para el usuario administrador.", "Error");
                            return;
                        }

                        if (respuesta == Pin && Pin != "****")
                        {
                            usuario.Acceso = "Administrador";
                        }
                        else
                        {
                            V_Menu_Principal.MSG.ShowMSG("Pin incorrecto. El usuario no se ha creado.", "Información");
                            return;
                        }
                    }
                }
            }

            // Guardar usuario
            try
            {
                context.Usuarios.Add(usuario);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG("Error al crear el usuario: Ya existe o los datos proporcionados no son válidos.", "Error");
                MessageBox.Show(ex.InnerException.Message);
                return;
            }

            // Confirmación y limpieza
            V_Menu_Principal.MSG.ShowMSG($"Usuario creado correctamente.\nAcceso: {usuario.Acceso}", "Éxito");
            Pin = "****";
            Util.Registrar_Actividad(0, $"Ha creado al usuario: {usuario.Nombre}", V_Menu_Principal.Secuencial_Empresa);

            Properties.Settings.Default.Primer_Arranque = false;
            Properties.Settings.Default.Save();

            txt_Codigo.Clear();
            txt_Nombre.Clear();
            txt_Password.Clear();
            pictureBox3.Image = null;

            panel4.Visible = false;
            panel3.Visible = true;




        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = true;
            label11.Text = "Miguel Cerrato";
            pictureBox2.BorderStyle = BorderStyle.None; // Limpia el borde del PictureBox
            Pin = "****"; // Reinicia el PIN al cerrar el panel de creación de usuario
            txt_Codigo.Clear(); // Limpia el campo de código
            txt_Nombre.Clear(); // Limpia el campo de nombre
            txt_Password.Clear(); // Limpia el campo de contraseña
            pictureBox3.Image = null; // Limpia la imagen del PictureBox
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();
            Secuencial = context.Usuarios.Any() ? context.Usuarios.Max(p => p.Secuencial) + 1 : 1;


            string rutaGuardado = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\USR\\" + V_Menu_Principal.Secuencial_Empresa + "-Usr - " + Secuencial + ".PNG");


            try
            {
                string Imagen_Seleccionada = Util.Abrir_Dialogo_Seleccion_URL();
                if (Imagen_Seleccionada != "")
                {
                    Imagen = Imagen_Seleccionada;
                    pictureBox3.Image = Util.Cargar_Imagen_Local(Imagen);

                    pictureBox3.Image.Save(rutaGuardado);
                    Imagen = rutaGuardado;
                }



            }
            catch
            {

                Imagen = "Sin Imagen";

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msj = string.Empty; // Variable para almacenar el mensaje de estado
            Usuario usuario = new Usuario(); // Crea una instancia de Usuario para almacenar los datos del usuario

            SQLitePCL.Batteries.Init(); // Inicializa SQLitePCL

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe
            string password_encriptado = Util.Encriptador.Encriptar(txtPassword.Text);
            LoginManager login = new LoginManager();
            bool acceso;
            try
            {
                usuario = login.ValidarUsuario(txtCodigo.Text, password_encriptado, V_Menu_Principal.Secuencial_Empresa); // Valida el usuario y obtiene sus datos
                acceso = login.ValidarUsuario(txtCodigo.Text, password_encriptado, V_Menu_Principal.Secuencial_Empresa).Secuencial != null;
            }
            catch (Exception ex)
            {
                // V_Menu_Principal.MSG.ShowMSG("Error al validar el usuario: " + ex.Message, "Error");
                acceso = false;
                txtPassword.Clear(); // Limpia el campo de contraseña si el acceso es incorrecto

                V_Menu_Principal.MSG.ShowMSG("Error al validar el usuario. "+ex.Message, "Error");

                return;
            }



            if (acceso == true)
            {
                V_Menu_Principal.Secuencial_Usuario = usuario.Secuencial;
                V_Menu_Principal.Acceso_Usuario = usuario.Acceso;
                V_Menu_Principal.Imagen_Usuario = usuario.Imagen; // byte[] directamente
                V_Menu_Principal.Nombre_Usuario = usuario.Nombre;
                V_Menu_Principal.Codigo_Usuario = usuario.Codigo;

                this.DialogResult = DialogResult.OK;
                this.Hide();
            }
            else
            {
                txtPassword.Clear();
                txtCodigo.Focus();
                this.DialogResult = DialogResult.None;
            }

        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {



            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus(); // Mover el foco al campo de contraseña

            }



        }

        private void txtPassword_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
                button1.PerformClick();

            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {




            this.Close(); // Cierra la ventana de login
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            if (isAdmin != false)
            {

                suma = suma + 1; // Incrementa la suma de números aleatorios
                if (suma >= 10)
                {


                    Random random = new Random();
                    this.Pin = random.Next(1000, 10000).ToString() + "*"; // Genera un número entre 1000 y 9999
                    label11.ForeColor = Color.Red; // Cambia el color del texto a rojo
                    isAdmin = false; // Cambia el estado a no administrador
                    suma = 0; // Reinicia la suma a 0 después de generar el PIN


                }

            }


        }

        private void label11_MouseDown(object sender, MouseEventArgs e)
        {
            label11.Text = "Pin: " + Pin; // Muestra el PIN generado al hacer clic en la etiqueta
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseHover(object sender, EventArgs e)
        {
            label11.Text = "Miguel Cerrato";
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        int posX = 0, posY = 0; // Variables para almacenar la posición del mouse
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                posX = e.X;
                posY = e.Y;

            }
            else
            {
                panel1.Cursor = Cursor.Current = Cursors.SizeAll;
                Left = Left + (e.X - posX);
                Top = Top + (e.Y - posY);

            }
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {

            label11.Text = "Miguel Cerrato"; // Muestra el nombre del usuario al mover el mouse sobre el panel

            if (e.Button != MouseButtons.Left)
            {
                posX = e.X;
                posY = e.Y;

            }
            else
            {
                // panel1.Cursor = Cursor.Current = Cursors.SizeAll;
                Left = Left + (e.X - posX);
                Top = Top + (e.Y - posY);

            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label11_DoubleClick(object sender, EventArgs e)
        {
            if (isAdmin == false)
            {
                isAdmin = true; // Cambia el estado a administrador
                label11.ForeColor = Color.Green; // Cambia el color del texto a verde
            }
            else
            {
                isAdmin = false; // Cambia el estado a no administrador
                label11.ForeColor = Color.White; // Cambia el}
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();
            Secuencial = context.Usuarios.Any() ? context.Usuarios.Max(p => p.Secuencial) + 1 : 1;



            V_Captura_Imagen capturaImagen = new V_Captura_Imagen(Secuencial);
            capturaImagen.ShowDialog();
            Bitmap imagenCapturada = V_Captura_Imagen.Get_Imagen();
            if (imagenCapturada != null)
            {
                pictureBox3.Image = imagenCapturada; // Asigna la imagen capturada al PictureBox
                string rutaGuardado = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\USR\\" + V_Menu_Principal.Secuencial_Empresa + "-Usr - " + Secuencial + ".PNG");
                imagenCapturada.Save(rutaGuardado); // Guarda la imagen en la ruta especificada
                Imagen = rutaGuardado; // Actualiza la variable Imagen con la ruta guardada
            }
            else
            {
                V_Menu_Principal.MSG.ShowMSG("No se ha capturado ninguna imagen.", "Error");
            }

        }




        public void llenar_Combo_Empresa()
        {


            comboEmpresa.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar solo clientes activos
            //var empresaActiva = context.Empresas.Where(c => (bool)c.Activa).ToList();
            var empresaActiva = context.Empresas.Where(c => (bool)c.Activa).ToList();
            foreach (var item in empresaActiva)
            {
                comboEmpresa.Items.Add(item.Secuencial + " - " + item.Nombre);
            }





        }






        private async void V_Login_Load(object sender, EventArgs e)
        {


            // ======================================
            // 🔐 Validación de Licencia
            // ======================================

            // Si deseas reiniciar la configuración (solo para pruebas o soporte):
            // Properties.Settings.Default.Reset();
            //Properties.Settings.Default.Save();

            if (!Properties.Settings.Default.LicenciaValida)
            {
                V_Menu_Principal.MSG.ShowMSG(
                    "❌ Licencia no válida o vencida.\nPor favor, valide su licencia para continuar.",
                    "Error"
                );

                using var validador = new V_Validador_Licencia();
                validador.ShowDialog();

                // Verifica si la licencia fue validada después del diálogo
                if (!Properties.Settings.Default.LicenciaValida)
                {
                    System.Windows.Forms.Application.Exit(); // Cierra la app si no se valida
                    return;
                }
            }

            // Si llegamos aquí, la licencia es válida
            // V_Menu_Principal.MSG.ShowMSG("✅ Licencia válida. Bienvenido al sistema.", "Éxito");




            /////////////////////////////////////////////////////////////////////////////////


            this.Text = "Monitux-POS ver." + V_Menu_Principal.VER;

            if (Properties.Settings.Default.Primer_Arranque)
            {

                V_Menu_Principal.MSG.ShowMSG(
                    "¡Bienvenido al primer arranque de Monitux-POS!\nPor favor, cree una empresa y a continuación un usuario administrador para continuar.",
                    "Primer Arranque"

                );

                V_Empresa ventanaEmpresa = new V_Empresa();
                ventanaEmpresa.ShowDialog();
                // Verifica si se creó la empresa correctamente
                if (ventanaEmpresa.DialogResult != DialogResult.OK)
                {
                    V_Menu_Principal.MSG.ShowMSG("Debe crear una empresa para continuar.", "Error");
                    System.Windows.Forms.Application.Exit(); // Cierra la app si no se crea la empresa
                    return;
                }

                // Si es el primer arranque, muestra el panel de creación de usuario
                panel3.Visible = false;
                panel4.Visible = true;
                label11.ForeColor = Color.White; // Resetea el color del texto
            }
            else
            {
                // Si no es el primer arranque, muestra el panel de login
                panel3.Visible = true;
                panel4.Visible = false;
            }



            llenar_Combo_Empresa();


           
            



            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe
           // int secuencialn = context.Productos.Any() ? context.Productos.Max(p => p.Secuencial) + 1 : 1;
            var usuario = new Usuario();
           // Secuencial = secuencialn; // Asigna el secuencial al usuario


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboEmpresa_MouseClick(object sender, MouseEventArgs e)
        {

            llenar_Combo_Empresa();

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void comboEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {






            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            //var proveedores = context.Proveedores.ToList();

            var empresa = context.Empresas
    .Where(p => (bool)p.Activa)
    .ToList();






            foreach (var item in empresa)
            {

                // V_Menu_Principal.Secuencial_Empresa = item.Secuencial; // Asigna el secuencial de la empresa seleccionada
                V_Menu_Principal.Nombre_Empresa = item.Nombre; // Asigna el nombre de la empresa seleccionada
                V_Menu_Principal.Direccion_Empresa = item.Direccion; // Asigna la dirección de la empresa seleccionada
                V_Menu_Principal.Telefono_Empresa = item.Telefono; // Asigna el teléfono de la empresa seleccionada
                V_Menu_Principal.Email_Empresa = item.Email; // Asigna el email de la empresa seleccionada
                V_Menu_Principal.RSS = item.RSS; // Asigna el RUC de la empresa seleccionada
                V_Menu_Principal.moneda = item.Moneda; // Asigna la moneda de la empresa seleccionada
                V_Menu_Principal.ISV = (double)item.ISV; // Asigna el ISV de la empresa seleccionada

            }



            V_Menu_Principal.Secuencial_Empresa = int.Parse(comboEmpresa.SelectedItem.ToString().Split('-')[0].Trim());

           







        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.Reset(); // Reinicia la configuración de la aplicación
            Properties.Settings.Default.Primer_Arranque=true;
            Properties.Settings.Default.Save(); 
            // Reinicia la configuración de la aplicación
        }
    }// namespace Ventanas
}// namespace Monitux_POS.Ventanas
