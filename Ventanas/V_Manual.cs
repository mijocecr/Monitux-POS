using PdfiumViewer;
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
    public partial class V_Manual : Form
    {
        public V_Manual()
        {
            InitializeComponent();
        }

        private void V_Manual_Load(object sender, EventArgs e)
        {
            var pdfViewer = new PdfViewer
            {
                Dock = DockStyle.Fill
            };
            string rutaArchivo= "C:\\Users\\Miguel Cerrato\\Desktop\\fffxxxx.pdf";
            pdfViewer.Document = PdfDocument.Load(rutaArchivo);
            this.Controls.Add(pdfViewer);
        }
    }
}
