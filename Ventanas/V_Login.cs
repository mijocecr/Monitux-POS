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
using static Monitux_POS.Clases.Util;
using static System.Net.Mime.MediaTypeNames;

namespace Monitux_POS.Ventanas
{
    public partial class V_Login : Form
    {
        public int suma = 0; // Variable para almacenar la suma de los números aleatorios
        public string Pin = "****"; // Variable para almacenar el PIN del usuario
        public static string Imagen = "Sin Imagen"; // Variable para almacenar la imagen del usuario
        public V_Login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            pictureBox1.Load(Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Iconos\\mostrar.PNG"));

            txtPassword.PasswordChar = '\0'; // Mostrar la contraseña
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.PasswordChar = '*'; // Mostrar la contraseña
            pictureBox1.Load(Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Iconos\\ocultar.PNG"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel4.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {




            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            var usuario = new Usuario();

            usuario.Nombre = txt_Nombre.Text;
            usuario.Codigo = txt_Codigo.Text;
            usuario.Password = Util.Encriptador.Encriptar(txt_Password.Text);


            // usuario.Acceso = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "Vendedor";

            if (comboBox1.SelectedItem == "Administrador")
            {
                string respuesta;
                if (V_Menu_Principal.IPB.Show("Ingrese el Pin", "Usuario Administrador", out respuesta) == DialogResult.OK)
                {

                    if (respuesta == "****" && Pin == "*****")
                    {
                        V_Menu_Principal.MSG.ShowMSG("Debe generar un PIN para el usuario administrador.", "Error");
                        return; // Sale del método si el PIN no ha sido generado
                    }
                    else
                    {

                        if (respuesta == Pin && Pin != "****")
                        {
                            usuario.Acceso = "Administrador"; // Asigna el acceso de administrador si el pin es correcto
                        }
                        else
                        {
                            usuario.Acceso = "Vendedor"; // Asigna el acceso de vendedor si el pin es incorrecto
                            V_Menu_Principal.MSG.ShowMSG("Pin incorrecto. El usuario no se ha creado.", "Información");

                            return; // Sale del método si el usuario cancela la operación
                        }

                    }

                }
            }
            else
            {
                usuario.Acceso = "Vendedor"; // Asigna el acceso de vendedor si no es administrador
            }



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
            V_Menu_Principal.MSG.ShowMSG("Usuario creado correctamente.\nAcesso: " + usuario.Acceso, "Éxito");
            Pin = "****"; // Reinicia el PIN después de crear el usuario
            Util.Registrar_Actividad(0, "Ha creado al usuario: " + txt_Nombre.Text);

            txt_Codigo.Clear(); // Limpia el campo de código
            txt_Nombre.Clear(); // Limpia el campo de nombre
            txt_Password.Clear(); // Limpia el campo de contraseña
            pictureBox3.Image = null; // Limpia la imagen del PictureBox






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
            Imagen = Util.Abrir_Dialogo_Seleccion_URL(); // Cargar imagen desde el disco
            if (Imagen != "Sin Imagen")
            {
                pictureBox3.Load(Imagen); // Cargar la imagen en el PictureBox
            }
            else
            {
                pictureBox3.Image = null; // Si no se selecciona una imagen, limpiar el PictureBox
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
                usuario = login.ValidarUsuario(txtCodigo.Text, password_encriptado); // Valida el usuario y obtiene sus datos
                acceso = login.ValidarUsuario(txtCodigo.Text, password_encriptado).Secuencial != null;
            }
            catch (Exception ex)
            {
                // V_Menu_Principal.MSG.ShowMSG("Error al validar el usuario: " + ex.Message, "Error");
                acceso = false;
                txtPassword.Clear(); // Limpia el campo de contraseña si el acceso es incorrecto

                msj = (acceso ? "Acceso concedido" : "Usuario o contraseña incorrectos");
                V_Menu_Principal.MSG.ShowMSG(msj, acceso ? "Éxito" : "Error");
                return;
            }



            msj = (acceso ? "Acceso concedido" : "Usuario o contraseña incorrectos");
            V_Menu_Principal.MSG.ShowMSG(msj, acceso ? "Éxito" : "Error");


            if (acceso == true)
            {
                V_Menu_Principal.Secuencial_Usuario = usuario.Secuencial; // Almacena el secuencial del usuario en la clase V_Menu_Principal
                V_Menu_Principal.Acceso_Usuario = usuario.Acceso; // Almacena el acceso del usuario en la clase V_Menu_Principal
                V_Menu_Principal.Imagen_Usuario = usuario.Imagen; // Almacena la imagen del usuario en la clase V_Menu_Principal
                V_Menu_Principal.Nombre_Usuario = usuario.Nombre; // Almacena el nombre del usuario en la clase V_Menu_Principal
                V_Menu_Principal.Codigo_Usuario = usuario.Codigo; // Almacena el código del usuario en la clase V_Menu_Principal                            

                this.DialogResult = DialogResult.OK; // Establece el resultado del diálogo como OK
                this.Hide(); // Oculta la ventana de login
            }
            else
            {
                txtPassword.Clear(); // Limpia el campo de contraseña si el acceso es incorrecto
                txtCodigo.Focus();
                DialogResult = DialogResult.None; // Establece el resultado del diálogo como None para evitar que se cierre
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
            suma = suma + 1; // Incrementa la suma de números aleatorios
            if (suma >= 5)
            {
                pictureBox2.BorderStyle = BorderStyle.Fixed3D;

                Random random = new Random();
                this.Pin = random.Next(1000, 10000).ToString() + "*"; // Genera un número entre 1000 y 9999

                suma = 0; // Reinicia la suma a 0 después de generar el PIN

                pictureBox2.BorderStyle = BorderStyle.None; // Limpia el borde del PictureBox

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
    }
}
