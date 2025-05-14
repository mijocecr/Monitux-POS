using Monitux_POS.Clases;
using static Monitux_POS.Clases.Util;

namespace Monitux_POS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Text = Util.Abrir_Dialogo_Seleccion_URL();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Usb oUsb = new Usb();
            List<USBInfo> lstUSBD = oUsb.GetUSBDevices();

            foreach (USBInfo x in lstUSBD)
            {

                listBox1.Items.Add(x.Description + " ID:" + x.DeviceID);

            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Text = Util.Comprobar_Llave_USB(textBox1.Text).ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            miniatura_Inventario1.Precio.Text = "1500";

            
            miniatura_Inventario1.Imagen.Load(Util.Abrir_Dialogo_Seleccion_URL());
            //textBox1.Text = Util.Encriptador.Encriptar("Miguel");

            
            

            
            
         


            // Crear elemento principal del menú
            ToolStripMenuItem menuPrincipal = new ToolStripMenuItem("Opciones");

            // Crear sub ítems
            ToolStripMenuItem subItem1 = new ToolStripMenuItem("Sub Opción 1");
            ToolStripMenuItem subItem2 = new ToolStripMenuItem("Sub Opción 2");

            // Agregar los sub ítems al ítem principal
            menuPrincipal.DropDownItems.Add(subItem1);
            menuPrincipal.DropDownItems.Add(subItem2);

            // Agregar el ítem principal al menú contextual
            miniatura_Inventario1.Menu.Items.Add(menuPrincipal);

           





        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Util.Encriptador.Desencriptar("mERVc1DJ9Coh1Ev/3PCgWg=="));
        }
    }
}
