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
    public partial class V_Visor_Factura : Form
    {
        public string rutaArchivo = "";
        public string titulo = "";
        public Stream streamActual;
        public V_Visor_Factura()
        {
            InitializeComponent();
        }

        private void V_Visor_Factura_Load(object sender, EventArgs e)
        {
            this.Text = titulo;
            var pdfViewer = new PdfViewer
            {
                Dock = DockStyle.Fill
            };

            byte[] pdfBytes = File.ReadAllBytes(rutaArchivo);
            MemoryStream stream = new MemoryStream(pdfBytes); // sin 'using'
                                                              // Guarda el stream en un campo si necesitas liberarlo luego manualmente
            var streamActual = stream;

            pdfViewer.Document = PdfDocument.Load(streamActual);
            pdfViewer.ZoomMode = PdfViewerZoomMode.FitWidth;
            this.Controls.Add(pdfViewer);

           


        }

        private void V_Visor_Factura_FormClosed(object sender, FormClosedEventArgs e)
        {
            streamActual = null;
        }
    }
}
