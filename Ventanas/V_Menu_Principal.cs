using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Wpf;
using Monitux_POS.Clases;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ZXing;
using WebView2 = Microsoft.Web.WebView2.WinForms.WebView2;

namespace Monitux_POS.Ventanas
{
    public partial class V_Menu_Principal : Form
    {



        private IWavePlayer waveOut;
        private MediaFoundationReader reader;
        private bool isPlaying = false;
        private string streamUrl; //= "https://hchradio.innovandote.com:8000/stream";





        List<(string Titulo, string Enlace)> titularesRSS = new();
        int indiceActual = 0;
        int titularesMostrados = 0;
        int maxTitularesPorCiclo = 20;
        string enlaceActual = "";



        //Bloque de Variables Globales

        public static MSG MSG = new MSG();
        public static IPB IPB = new IPB();
        public static string VER = Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, 3);
        public static int Secuencial_Usuario;
        public static string Nombre_Usuario = string.Empty;
        public static string Codigo_Usuario = string.Empty;
        public static string Imagen_Usuario = "Sin Imagen"; // Variable para almacenar la imagen del usuario
        public static string Acceso_Usuario = string.Empty; // Variable para almacenar los permisos del usuario


        public static int Secuencial_Empresa = 1; // Cambiar esto, es solo para probar

        public static string moneda;

        //Estos settings hay que crearlos
        public static string Nombre_Empresa = "One Click Solutions";
        public static string Direccion_Empresa = "Calle Lagreo, 4, Benidorm 03502";
        public static string Telefono_Empresa = "+34642883288";
        public static string Email_Empresa = "miguel.cerrato.es@gmail.com";
        public static string RSS = "http://www.bbc.co.uk/mundo/ultimas_noticias/index.xml";
        //Estos settings hay que crearlos


        //Bloque de Variables Globales



        public static V_Login v_Login = new V_Login();
        public V_Menu_Principal()
        {



            InitializeComponent();
        }



        private void MostrarSiguienteTitular()
        {
            if (titularesRSS.Count == 0) return;

            lbl_Cinta.Text = titularesRSS[indiceActual].Titulo;
            enlaceActual = titularesRSS[indiceActual].Enlace;
            lbl_Cinta.Left = panel10.Width;

            indiceActual = (indiceActual + 1) % titularesRSS.Count;
        }




        private List<string> titularesYaMostrados = new();




        //https://www.tunota.com/rss/honduras-hoy.xml
        //http://www.bbc.co.uk/mundo/ultimas_noticias/index.xml
        private void CargarTitularesRSS(string url = "https://www.tunota.com/rss/honduras-hoy.xml")
        {
            try
            {
                using var reader = XmlReader.Create(url);
                var feed = SyndicationFeed.Load(reader);

                var nuevosTitulares = feed.Items
                    .OrderByDescending(item => item.PublishDate) // Asegura orden por fecha
                    .Take(20)
                    .Where(item => !titularesYaMostrados.Contains(item.Title.Text.Trim()))
                    .Select(item => (
                        "🗞️ " + item.Title.Text.Trim(),
                        item.Links.FirstOrDefault()?.Uri.ToString() ?? ""))
                    .ToList();

                // Agregar los nuevos títulos al historial
                titularesYaMostrados.AddRange(nuevosTitulares.Select(t => t.Item1));

                // Actualizar la lista de titulares
                titularesRSS = nuevosTitulares.Any()
                    ? nuevosTitulares
                    : new() { ("No hay titulares nuevos.", "") };
            }
            catch (Exception ex)
            {
                titularesRSS = new() { ($"No se pudieron cargar las noticias: {ex.Message}", "") };
            }

            indiceActual = 0;
            titularesMostrados = 0;
            MostrarSiguienteTitular();
        }





        /*  private void CargarTitularesRSS(string url = "http://www.bbc.co.uk/mundo/ultimas_noticias/index.xml")
          {
              try
              {
                  using var reader = XmlReader.Create(url);
                  var feed = SyndicationFeed.Load(reader);

                  var nuevosTitulares = feed.Items
                      .Take(20)
                      .Where(item => !titularesYaMostrados.Contains(item.Title.Text.Trim()))
                      .Select(item => (
                          "🗞️ " + item.Title.Text.Trim(),
                          item.Links.FirstOrDefault()?.Uri.ToString() ?? ""))
                      .ToList();

                  // Agregar los nuevos títulos al historial
                  titularesYaMostrados.AddRange(nuevosTitulares.Select(t => t.Item1));

                  // Actualizar la lista de titulares
                  titularesRSS = nuevosTitulares.Any()
                      ? nuevosTitulares
                      : new() { ("No hay titulares nuevos.", "") };
              }
              catch
              {
                  titularesRSS = new() { ("No se pudieron cargar las noticias.", "") };
              }

              indiceActual = 0;
              titularesMostrados = 0;
              MostrarSiguienteTitular();
          }

          */




        private void V_Menu_Principal_Load(object sender, EventArgs e)
        {

            string cultura = Properties.Settings.Default.CulturaMoneda;
            CultureInfo info = new CultureInfo(cultura);
            moneda = info.NumberFormat.CurrencySymbol;

            CargarTitularesRSS();
            lbl_Titulo.Text = "Monitux-POS v." + VER;
            //Abrir_Ventana(v_Login);
            if (v_Login.ShowDialog() == DialogResult.OK)
            {
                // Si el usuario ha iniciado sesión correctamente, se puede continuar
                lbl_Descripcion.Text = Nombre_Usuario + " ha iniciado sesión correctamente.";
                lbl_Titulo.Text = "Monitux-POS v." + VER;
                label1.Text = Nombre_Usuario; // Variable para almacenar el nombre del usuario
                label2.Text = Acceso_Usuario; // Variable para almacenar el codigo del usuario

                btn_inicio.Visible = true;//Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de resumen solo si el usuario es administrador
                                          //  btn_resumen.Visible = true; // Mostrar el boton de resumen solo si el usuario es administrador
                btn_ajustes.Visible = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de ajustes solo si el usuario es administrador
                                                                                        // btn_ajustes.Visible = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de ajustes solo si el usuario es administrador
                btn_cuentas.Visible = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de cuentas solo si el usuario es administrador
                                                                                        // btn_cuentas.Visible = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de cuentas solo si el usuario es administrador
                btn_facturas.Visible = true; //Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de facturas solo si el usuario es administrador
                                             //  btn_facturas.Visible = true; // Mostrar el boton de facturas solo si el usuario es administrador
                                             //btn_inventario.Enabled = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de inventario solo si el usuario es administrador
                                             //  btn_inventario.Visible = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de inventario solo si el usuario es administrador
                btn_reportes.Visible = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de reportes solo si el usuario es administrador
                                                                                         //  btn_reportes.Visible = Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de reportes solo si el usuario es administrador

                btn_movimientos.Visible = true;// Acceso_Usuario == "Administrador" ? true : false; // Mostrar el boton de campañas solo si el usuario es administrador
                try
                {
                    pictureBox1.Image = Util.Cargar_Imagen_Local(Imagen_Usuario);
                }
                catch (Exception ex)
                {
                    // MSG.ShowMSG("Error al cargar la imagen del usuario: " + ex.Message, "Error");
                }

                btn_inicio.PerformClick();



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

            Util.Limpiar_Cache(V_Menu_Principal.Secuencial_Empresa);
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

            lbl_Cinta.Left -= 4;

            if (lbl_Cinta.Right < 0)
            {
                titularesMostrados++;

                if (titularesMostrados >= maxTitularesPorCiclo)
                {
                    CargarTitularesRSS(); // Recarga titulares y reinicia contador
                }
                else
                {
                    MostrarSiguienteTitular();
                }
            }

        }

        private void lbl_Cinta_Click(object sender, EventArgs e)
        {

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
            lbl_Descripcion.Text = "Punto de partida para gestionar clientes, proveedores, venta rapida y obtener una visión general en tiempo real del estado del negocio. Tambien puede realizar abonos y consultar los saldos de CTAS.";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Mostrar_SubMenu(panel12);
            lbl_Descripcion.Text = "Configuración general del sistema, gestión de usuarios e información del programa.";
        }

        private void button24_Click(object sender, EventArgs e)
        {
            lbl_Descripcion.Text = "Administre sus cuentas por cobrar y por pagar, registre pagos parciales y acceda al detalle completo de transacciones.";
            Mostrar_SubMenu(panel2);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Mostrar_SubMenu(panel6);
            lbl_Descripcion.Text = "Desde aquí podrá generar cotizaciones, emitir órdenes de compra y registrar todas sus compras y ventas fácilmente.";
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
            lbl_Descripcion.Text = "Consulte reportes detallados de todos los procesos registrados en Monitux-POS, incluyendo ventas, compras, inventario y más, todo en un solo lugar.";
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
            lbl_Descripcion.Text = "Esta ventana le permite gestionar su inventario de forma eficiente: puede consultar sus productos, agregar nuevos ítems y acceder al historial de movimientos (kardex).";
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

        private void button21_Click_1(object sender, EventArgs e)
        {
            V_Venta_Rapida v_Venta_Rapida = new V_Venta_Rapida();

            Abrir_Ventana(v_Venta_Rapida);


            lbl_Descripcion.Text = "Interfaz optimizada para agilizar el proceso de venta mediante lector de códigos de barras. " +
                "Muestra automáticamente los 7 productos más vendidos y permite registrar ventas con rapidez y precisión.";
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            V_Actividades v_Actividades = new V_Actividades();
            Abrir_Ventana(v_Actividades);
            lbl_Descripcion.Text = "Bitácora de actividades ventana que muestra un registro cronológico de tareas o eventos realizados, útil para el seguimiento y control de actividades.";

        }

        private void button22_Click(object sender, EventArgs e)
        {


            V_Cliente v_Cliente = new V_Cliente();
            v_Cliente.Secuencial_Usuario = V_Menu_Principal.Secuencial_Usuario;

            v_Cliente.ShowDialog();




        }

        private void lbl_Cinta_MouseEnter(object sender, EventArgs e)
        {
            lbl_Cinta.Cursor = Cursors.Hand;
            lbl_Cinta.ForeColor = Color.RoyalBlue;
            lbl_Cinta.Font = new Font(lbl_Cinta.Font, FontStyle.Underline);
        }

        private void lbl_Cinta_MouseLeave(object sender, EventArgs e)
        {
            lbl_Cinta.Cursor = Cursors.Default;
            lbl_Cinta.ForeColor = Color.FromArgb(128, 255, 255);
            lbl_Cinta.Font = new Font(lbl_Cinta.Font, FontStyle.Regular);
        }

        private void lbl_Cinta_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(enlaceActual))
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = enlaceActual,
                        UseShellExecute = true
                    });
                }
                catch
                {
                    MessageBox.Show("No se pudo abrir la noticia.");
                }
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {

            V_Proveedor v_Proveedor = new V_Proveedor();
            v_Proveedor.Secuencial_Usuario = V_Menu_Principal.Secuencial_Usuario;

            v_Proveedor.ShowDialog();


        }

        private void button6_Click(object sender, EventArgs e)
        {


            V_CTAS_Cobrar v_CTAS_Cobrar = new V_CTAS_Cobrar();
            Abrir_Ventana(v_CTAS_Cobrar);
            lbl_Descripcion.Text = "Consulte rápidamente sus cuentas por cobrar y obtenga una visión general del estado de sus ventas a crédito.";




        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            V_CTAS_Pagar v_CTAS_Pagar = new V_CTAS_Pagar();
            Abrir_Ventana(v_CTAS_Pagar);
            lbl_Descripcion.Text = "Consulte rápidamente sus cuentas por pagar y obtenga una visión general del estado de sus compras a crédito.";

        }

        private void btn_reportes_Click(object sender, EventArgs e)
        {
            Mostrar_SubMenu(p_Reportes);
        }

        private void pictureBox2_ClickAsync(object sender, EventArgs e)
        {




            /*
            
            // Si decides volver a usar la lógica de reproducción directa:
            if (!isPlaying)
            {
                try
                {
                    reader = new MediaFoundationReader(streamUrl="https://hchradio.innovandote.com:8000/stream");
                    waveOut = new WaveOutEvent();
                    waveOut.Init(reader);
                    waveOut.Play();
                    isPlaying = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al reproducir: " + ex.Message);
                }
            }
            else
            {
                waveOut?.Stop();
                reader?.Dispose();
                waveOut?.Dispose();
                isPlaying = false;
            }
            
            */

        }

        private void button4_Click_2(object sender, EventArgs e)
        {


            try
            {
                // Configura el mensaje
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("miguel.cerrato.es@gmail.com");
                mail.To.Add("cerratonix@gmail.com");
                mail.Subject = "Envío de PDF desde C#";
                mail.Body = "Hola, adjunto te envío el archivo PDF.";

                // Adjuntar el PDF
                string rutaPdf = @"C:\Users\Miguel Cerrato\Desktop\Factura.pdf";
                Attachment adjunto = new Attachment(rutaPdf, MediaTypeNames.Application.Pdf);
                mail.Attachments.Add(adjunto);

                // Configura el servidor SMTP
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                smtp.Credentials = new NetworkCredential("miguel.cerrato.es@gmail.com", "pnma abut vzvp rdld");
                smtp.EnableSsl = true;

                // Enviar
                smtp.Send(mail);
                Console.WriteLine("Correo enviado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar el correo: " + ex.Message);
            }


        }
    }
}
