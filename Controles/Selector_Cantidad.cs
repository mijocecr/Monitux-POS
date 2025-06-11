using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitux_POS.Controles
{


    public partial class Selector_Cantidad : UserControl
    {





        public double GetCantidad()
        {
            return (double)numericUpDown1.Value;
        }



        public void SetCodigo(string codigo)
        {
            label1.Text = codigo;

        }
        public string GetCodigo()
        {
            return label1.Text;
        }


        public Selector_Cantidad()
        {


            InitializeComponent();

            this.BackColor = Color.LightBlue;
            label1.ForeColor = Color.Fuchsia;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                this.BackColor=Color.Red;
                label1.ForeColor = Color.White;
            }
            else
            {
                this.BackColor=Color.LightGreen;
                label1.ForeColor = Color.Blue;
                //this.BackColor = SystemColors.Control;
                //label1.ForeColor = SystemColors.ControlText;
            }
        }
    }
}
