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



    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        public void UpdateStatus(string mensaje, int progreso)
        {
            lblStatus.Text = mensaje;
            progressBar.Value = progreso;
            Application.DoEvents(); // Refresca la UI
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {

        }

        private void ProgressForm_Load_1(object sender, EventArgs e)
        {

        }
    }


}
