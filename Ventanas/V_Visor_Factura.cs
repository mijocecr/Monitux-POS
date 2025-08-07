using PdfiumViewer;
using System;
using System.IO;
using System.Windows.Forms;

namespace Monitux_POS.Ventanas
{
    public partial class V_Visor_Factura : Form
    {
        public string rutaArchivo = "";
        public string titulo = "";
        public byte[] DocumentoEnBytes { get; set; }
        public Stream streamActual;

        public V_Visor_Factura()
        {
            InitializeComponent();
            this.Load += V_Visor_Factura_Load;
            this.FormClosed += V_Visor_Factura_FormClosed;
        }

        private void V_Visor_Factura_Load(object sender, EventArgs e)
        {
            this.Text = titulo;

            var pdfViewer = new PdfViewer
            {
                Dock = DockStyle.Fill,
                ZoomMode = PdfViewerZoomMode.FitWidth
            };

            try
            {
                if (DocumentoEnBytes != null && DocumentoEnBytes.Length > 0)
                {
                    streamActual = new MemoryStream(DocumentoEnBytes);
                }
                else if (!string.IsNullOrWhiteSpace(rutaArchivo) && File.Exists(rutaArchivo))
                {
                    streamActual = new MemoryStream(File.ReadAllBytes(rutaArchivo));
                }
                else
                {
                    V_Menu_Principal.MSG.ShowMSG("No se pudo cargar el documento PDF.", "Error");
                    return;
                }

                pdfViewer.Document = PdfDocument.Load(streamActual);
                this.Controls.Add(pdfViewer);
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Error al cargar el PDF: {ex.Message}", "Error");
            }
        }

        private void V_Visor_Factura_FormClosed(object sender, FormClosedEventArgs e)
        {
            streamActual?.Dispose();
            streamActual = null;
        }
    }
}
