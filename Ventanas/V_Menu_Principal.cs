using Monitux_POS.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitux_POS.Ventanas
{
    public partial class V_Menu_Principal : Form
    {

        //Bloque de Variables Globales

        public static MSG MSG = new MSG();
        public static IPB IPB = new IPB();
        public static string VER = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static int Secuencial_Usuario;
        public static string Nombre_Usuario = string.Empty;
        public static string Codigo_Usuario = string.Empty;
        public static string Imagen_Usuario = "Sin Imagen"; // Variable para almacenar la imagen del usuario
        public static string Acceso_Usuario = string.Empty; // Variable para almacenar los permisos del usuario
        public static string moneda = "EUR.";
        //Bloque de Variables Globales



        public static V_Login v_Login = new V_Login();
        public V_Menu_Principal()
        {



            InitializeComponent();
        }

        private void V_Menu_Principal_Load(object sender, EventArgs e)
        {


            lbl_Titulo.Text = "Monitux-POS Ver." + VER;
            //Abrir_Ventana(v_Login);
            if (v_Login.ShowDialog() == DialogResult.OK)
            {
                // Si el usuario ha iniciado sesión correctamente, se puede continuar
                lbl_Descripcion.Text = Nombre_Usuario + " ha iniciado sesión correctamente.";
                lbl_Titulo.Text = "Monitux-POS Ver." + VER;
                label1.Text = Nombre_Usuario; // Variable para almacenar el nombre del usuario
                label2.Text = Acceso_Usuario; // Variable para almacenar el codigo del usuario

                btn_resumen.Enabled = true;//Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de resumen solo si el usuario es administrador
                                           //  btn_resumen.Visible = true; // Mostrar el boton de resumen solo si el usuario es administrador
                btn_ajustes.Enabled = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de ajustes solo si el usuario es administrador
                                                                                        // btn_ajustes.Visible = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de ajustes solo si el usuario es administrador
                btn_cuentas.Enabled = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de cuentas solo si el usuario es administrador
                                                                                        // btn_cuentas.Visible = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de cuentas solo si el usuario es administrador
                btn_facturas.Enabled = true; //Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de facturas solo si el usuario es administrador
                                             //  btn_facturas.Visible = true; // Mostrar el boton de facturas solo si el usuario es administrador
                //btn_inventario.Enabled = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de inventario solo si el usuario es administrador
                                                                                           //  btn_inventario.Visible = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de inventario solo si el usuario es administrador
                btn_reportes.Enabled = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de reportes solo si el usuario es administrador
                                                                                         //  btn_reportes.Visible = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de reportes solo si el usuario es administrador

                btn_campanas.Enabled = true;// Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de campañas solo si el usuario es administrador
                try
                {
                    pictureBox1.Image = Util.Cargar_Imagen_Local(Imagen_Usuario);
                }
                catch (Exception ex)
                {
                    // MSG.ShowMSG("Error al cargar la imagen del usuario: " + ex.Message, "Error");
                }


            }
            else
            {
                // Si el usuario no ha iniciado sesión, se cierra la aplicación
                Application.Exit();
            }
            panel4.Controls.Add(pictureBox2);

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }


        private void Ocultar_SubMenu()
        {
            if (panel2.Visible == true)
            {
                panel2.Visible = false;
            }
            if (panel6.Visible == true)
            {
                panel6.Visible = false;
            }
            if (p_Reportes.Visible == true)
            {
                p_Reportes.Visible = false;
            }
            if (panel11.Visible == true)
            {
                panel11.Visible = false;
            }

            if (panel12.Visible == true)
            {
                panel12.Visible = false;
            }

            if (p_Campania.Visible == true)
            {
                p_Campania.Visible = false;
            }


            panel4.Controls.Add(pictureBox2);
            //lbl_Descripcion.Text = "Sistema integral para la gestión de procesos de compra y venta, con módulos especializados para el control de productos, proveedores, categorías, clientes y facturación. Version: " + VER;

        }

        private void Mostrar_SubMenu(Panel subMenu)
        {

            panel4.Controls.Clear();


            Ocultar_SubMenu();
            if (subMenu.Visible == false)
            {
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbl_Titulo.Text = "Facturas";

            if (activeForm != null)
            {
                activeForm.Close();
                panel4.Controls.Add(pictureBox2);
            }
            Mostrar_SubMenu(panel6);
            lbl_Descripcion.Text = "Procesos de compra y venta, con módulos especializados para el control de productos, proveedores, categorías, clientes y facturación.";

            /*V_Factura_Venta x = new V_Factura_Venta();
            x.TopLevel=false;
            panel4.Controls.Add(x);
            x.Dock = DockStyle.Fill;
            x.BringToFront();
            x.Show();
            */




        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            V_Factura_Venta v_Factura_Venta = new V_Factura_Venta();
            v_Factura_Venta.Limpiar_Factura();
            Abrir_Ventana(v_Factura_Venta);


            lbl_Titulo.Text = "Factura de Venta";

            lbl_Descripcion.Text = "Aqui puede registrarse: Venta, Producto, Cliente, Cotizacion de Venta.";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            lbl_Titulo.Text = string.Empty;

            if (activeForm != null)
            {
                activeForm.Close();
                panel4.Controls.Add(pictureBox2);
            }
            Mostrar_SubMenu(panel2);





        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            Mostrar_SubMenu(p_Reportes);
        }

        private void button21_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            V_Factura_Compra v_Factura_Compra = new V_Factura_Compra();
            v_Factura_Compra.Limpiar_Factura();
            Abrir_Ventana(v_Factura_Compra);
            lbl_Titulo.Text = "Factura de Compra";

            lbl_Descripcion.Text = "Aqui puede registrarse: Compra, Producto, Proveedor, Orden de Compra. ";


        }





        public static Form activeForm = null;

        private void Abrir_Ventana(Form childForm)
        {
            // Cierra el formulario activo si existe
            if (activeForm != null)
            {
                activeForm.Close();
            }

            // Configura el nuevo formulario hijo
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;



          



            panel4.Controls.Clear();
            panel4.Controls.Add(childForm);
            
           panel4.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();





        }






        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Ocultar_SubMenu();
        }
        int posX = 0, posY = 0;
        private void panel7_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                posX = e.X;
                posY = e.Y;

            }
            else
            {
                panel7.Cursor = Cursor.Current = Cursors.SizeAll;
                Left = Left + (e.X - posX);
                Top = Top + (e.Y - posY);

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            Util.Limpiar_Cache();
            Application.Restart(); // Reinicia la aplicación para aplicar los cambios

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.ShowIcon = true;
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {

        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_Cinta.Left -= 4; // Mueve el texto hacia la izquierda

            // Si el texto se sale de la izquierda, lo reinicia a la derecha
            if (lbl_Cinta.Left < -900)
            {
                lbl_Cinta.Left = panel10.Right;
                lbl_Cinta.Text = "Prueba de Concepto..."; //Util.Obtener_Mensaje_Cinta(); // Actualiza el texto
            }
        }

        private void lbl_Cinta_Click(object sender, EventArgs e)
        {
            MessageBox.Show(lbl_Cinta.Left.ToString());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MSG.ShowMSG("Mensaje de Prueba", "Titulo de Prueba");
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbl_Descripcion_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Descripcion_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                posX = e.X;
                posY = e.Y;

            }
            else
            {
                panel7.Cursor = Cursor.Current = Cursors.SizeAll;
                Left = Left + (e.X - posX);
                Top = Top + (e.Y - posY);

            }
        }

        private void lbl_Titulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                posX = e.X;
                posY = e.Y;

            }
            else
            {
                panel7.Cursor = Cursor.Current = Cursors.SizeAll;
                Left = Left + (e.X - posX);
                Top = Top + (e.Y - posY);

            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            Mostrar_SubMenu(panel11);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Mostrar_SubMenu(panel12);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Mostrar_SubMenu(panel2);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Mostrar_SubMenu(panel6);

            if (V_Menu_Principal.Acceso_Usuario != "Administrador")
            {
                button10.Enabled = false; // Oculta el botón de eliminar si el usuario no es administrador
            }
            else
            {
                button10.Enabled = true; // Muestra el botón de eliminar si el usuario es administrador
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            V_Factura_Compra v_Factura_Compra = new V_Factura_Compra();
            v_Factura_Compra.Limpiar_Factura();
            Abrir_Ventana(v_Factura_Compra);


            lbl_Descripcion.Text = "En esta ventana tiene disponibles todas las opciones para generar una nueva compra, " +
               "desde crear un proveedor nuevo; hasta crear un nuevo (Producto/Servicio).";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            V_Factura_Venta v_Factura_Venta = new V_Factura_Venta();
            v_Factura_Venta.Limpiar_Factura();
            Abrir_Ventana(v_Factura_Venta);

            lbl_Descripcion.Text = "En esta ventana tiene disponibles todas las opciones para generar una nueva venta, " +
                "desde crear un cliente nuevo; hasta crear un nuevo (Producto/Servicio).";
        }

        private void button10_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Mostrar_SubMenu(p_Reportes);
            MSG.ShowMSG("No Implementado aun..., este si no se como hacerlo aun.", "Ni Puta idea...");
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Ocultar_SubMenu();
            Abrir_Ventana(new V_Inventario());
        }

        private void button28_Click(object sender, EventArgs e)
        {
            V_Usuario v_Usuario = new V_Usuario();
            v_Usuario.ShowDialog();
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            Mostrar_SubMenu(p_Campania);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Abrir_Ventana(new V_Inventario());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Resize(object sender, EventArgs e)
        {/*
            panel4.Location = new Point(
       (this.ClientSize.Width - panel4.Width+panel3.Width) / 2,
       (this.ClientSize.Height - panel4.Height+panel10.Height) / 2
  );*/




        }
    }
}
