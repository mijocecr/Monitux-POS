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
    public partial class IPB : Form
    {

        public string ValorIngresado { get; private set; } = string.Empty;
        public IPB(string mensaje, string titulo)
        {
            InitializeComponent();
            this.Text = titulo;
            lbl_Mensaje.Text = mensaje;

            btn_Aceptar.DialogResult = DialogResult.OK;
            btn_Cancelar.DialogResult = DialogResult.No;

            this.AcceptButton = btn_Aceptar;
            this.CancelButton = btn_Cancelar;

        }

        public DialogResult Show(string mensaje, string titulo, out string valor)
        {
            using (var form = new IPB(mensaje, titulo))
            {
                var result = form.ShowDialog();
                valor = form.ValorIngresado;
                return result;
            }
        }

        public IPB()
        {

        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            ValorIngresado = textBox1.Text;
            this.Close();
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {

        }
    }
}
