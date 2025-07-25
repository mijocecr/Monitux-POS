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

        public TarjetaDashboard(string titulo, string valorPrincipal, string variacion, Image icono, Color fondo, Point ubicacion)
        {
            Panel = new Panel
            {
                Size = new Size(200, 100),
                BackColor = fondo,
                Location = ubicacion
            };

            var lblTitulo = new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 10),
                AutoSize = true
            };

            var lblValor = new Label
            {
                Text = valorPrincipal,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 30),
                AutoSize = true
            };

            var lblVariacion = new Label
            {
                Text = variacion,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.White,
                Location = new Point(10, 60),
                AutoSize = true
            };

            var picIcono = new PictureBox
            {
                Image = icono,
                Size = new Size(32, 32),
                Location = new Point(150, 30),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            Panel.Controls.Add(lblTitulo);
            Panel.Controls.Add(lblValor);
            Panel.Controls.Add(lblVariacion);
            Panel.Controls.Add(picIcono);
        }
    }







}
