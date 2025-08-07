using Monitux_POS.Clases;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Monitux_POS.Ventanas
{
    public partial class V_Vista_Ampliada : Form
    {
        private readonly byte[]? imagenBytes;
        private readonly string codigo;
        private readonly string descripcion;

        public V_Vista_Ampliada(byte[]? imagen, string codigo, string descripcion)
        {
            imagenBytes = imagen;
            this.codigo = codigo;
            this.descripcion = descripcion;
            InitializeComponent();
        }

        private void V_Vista_Ampliada_Load(object sender, EventArgs e)
        {
            this.Text = "Monitux-POS v." + V_Menu_Principal.VER;

            try
            {
                if (imagenBytes != null)
                {
                    using var ms = new MemoryStream(imagenBytes);
                    Image imagenCargada = Image.FromStream(ms);
                    pictureBox1.Image = new Bitmap(imagenCargada);
                }
            }
            catch
            {
                // Manejo silencioso del error de imagen
            }

            label8.Text = codigo;
            label1.Text = descripcion;
        }
    }
}
