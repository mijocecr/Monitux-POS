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
    public partial class V_Vista_Ampliada : Form
    {

        
        public static string URL;
        public static string Codigo;
        public static string Descripcion;

        public V_Vista_Ampliada(string url, string codigo,string descripcion)
        {

            URL = url;
            Codigo = codigo;
            Descripcion = descripcion;
            InitializeComponent();
        }

        private void V_Vista_Ampliada_Load(object sender, EventArgs e)
        {
            this.Text = "Monitux-POS v."+V_Menu_Principal.VER;
            pictureBox1.Image = Util.Cargar_Imagen_Local(URL);
            label8.Text =  Codigo;
            label1.Text = Descripcion;

        }
    }
}
