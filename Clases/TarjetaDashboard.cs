using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{

    public class TarjetaDashboard
    {
        public Panel Panel { get; private set; }
        public string Titulo { get; private set; } // 🔙 Restaurado

        public Label LabelTitulo { get; private set; }
        public Label LabelValor { get; private set; }
        public Label LabelVariacion { get; private set; }

        private PictureBox PicIcono;

        public TarjetaDashboard(string titulo, string valorPrincipal, string variacion, Image icono, Color fondo, Point ubicacion)
        {
            Titulo = titulo;

            Panel = new Panel
            {
                Size = new Size(200, 80),
                BackColor = fondo,
                Location = ubicacion,
                Padding = new Padding(8),
                Cursor = Cursors.Hand,
                BorderStyle = BorderStyle.FixedSingle
            };

            LabelTitulo = new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 6),
                AutoSize = true
            };

            LabelValor = new Label
            {
                Text = valorPrincipal,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 28),
                AutoSize = true
            };

            LabelVariacion = new Label
            {
                Text = variacion,
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = Color.LightGray,
                Location = new Point(10, 50),
                AutoSize = true
            };

            PicIcono = new PictureBox
            {
                Image = icono,
                Size = new Size(28, 28),
                SizeMode = PictureBoxSizeMode.Zoom,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(Panel.Width - 40, 30),
                Margin = new Padding(0)
            };

            Panel.Controls.Add(LabelTitulo);
            Panel.Controls.Add(LabelValor);
            Panel.Controls.Add(LabelVariacion);
            Panel.Controls.Add(PicIcono);
        }

        public void AsignarClickComun(EventHandler eventoClick)
        {
            Panel.Click += eventoClick;
            foreach (Control control in Panel.Controls)
            {
                control.Click += eventoClick;
            }
        }
    }


}
