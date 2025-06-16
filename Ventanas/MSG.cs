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
    public partial class MSG : Form
    {
        bool sePuedeCerrar = true;
        public MSG()
        {
            InitializeComponent();

            btn_Aceptar.DialogResult = DialogResult.Yes;
            btn_Cancelar.DialogResult = DialogResult.No;

            this.AcceptButton = btn_Aceptar;
            this.CancelButton = btn_Cancelar;

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


        public DialogResult ShowMSG(string? mensaje, string? titulo)
        {
            lbl_Mensaje.Text = mensaje;
            this.Text = titulo;
            return this.ShowDialog(); // retornamos el resultado
        }

        private void MSG_Load(object sender, EventArgs e)
        {

        }


   


        private void button1_Click(object sender, EventArgs e)
        {
            if (sePuedeCerrar==true)
            {
                this.Close();
                return;
            }
        }
    }
}
