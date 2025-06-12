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
    public partial class V_Menu_Principal : Form
    {
        public V_Menu_Principal()
        {
            InitializeComponent();
        }

        private void V_Menu_Principal_Load(object sender, EventArgs e)
        {

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
            if (panel9.Visible == true)
            {
                panel9.Visible = false;
            }
            if (panel8.Visible == true)
            {
                panel8.Visible = false;
            }
        }

        private void Mostrar_SubMenu(Panel subMenu)
        {
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
            Mostrar_SubMenu(panel6);
            label3.Text = "Registre: Compra, Venta, Cotizacion, Orden | Gestione sus Facturas";

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
            Abrir_Ventana(new V_Factura_Venta());
            lbl_Titulo.Text = "Factura de Venta";
            lbl_Titulo.ForeColor = Color.FromArgb(96, 223, 84);
            label3.Text = "Aqui puede registrarse: Venta, Producto, Cliente, Cotizacion de Venta.";

        }

        private void button4_Click(object sender, EventArgs e)
        {
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
            Mostrar_SubMenu(panel9);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Mostrar_SubMenu(panel8);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Abrir_Ventana(new V_Factura_Compra());
            lbl_Titulo.Text = "Factura de Compra";
            lbl_Titulo.ForeColor = Color.FromArgb(128, 255, 255);
            label3.Text = "Aqui puede registrarse: Compra, Producto, Proveedor, Orden de Compra. ";


        }



        public static Form activeForm = new Form();
        private void Abrir_Ventana(Form childForm)
        {
            // Close the currently active form if it exists
            if (activeForm != null && activeForm != childForm)
            {
                activeForm.Close();
            }
            else
            {

            }
            // Set the new child form as the active form



            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
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
            Application.Exit();

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
            lbl_Cinta.Left -= 3; // Mueve el texto hacia la izquierda

            // Si el texto se sale de la izquierda, lo reinicia a la derecha
            if (lbl_Cinta.Left < -900)
            {
                lbl_Cinta.Left = panel10.Right;
                lbl_Cinta.Text = Util.Obtener_Mensaje_Cinta(); // Actualiza el texto
            }
        }

        private void lbl_Cinta_Click(object sender, EventArgs e)
        {
            MessageBox.Show(lbl_Cinta.Left.ToString());
        }
    }
}
