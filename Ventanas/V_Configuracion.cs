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
    public partial class V_Configuracion : Form
    {
        public V_Configuracion()
        {
            InitializeComponent();
        }

        private void V_Configuracion_Load(object sender, EventArgs e)
        {
            this.Text = "Configuración del Sistema";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            V_Empresa ventanaEmpresa = new V_Empresa();
            ventanaEmpresa.ShowDialog();


            V_Menu_Principal.MSG.ShowMSG("Configuración de la empresa guardada correctamente.", "Monitux-POS");

            button1.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            V_Usuario ventanaUsuario = new V_Usuario();
            ventanaUsuario.ShowDialog();

          
            V_Menu_Principal.MSG.ShowMSG("Configuración de usuario guardada correctamente.", "Monitux-POS");

            button2.Enabled = false;
        }
    }
}
