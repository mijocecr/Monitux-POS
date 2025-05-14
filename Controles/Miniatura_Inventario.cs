using Microsoft.VisualBasic;
using System.Drawing.Drawing2D;
using System.Reflection;








namespace Prueba
{






    public partial class Miniatura_Inventario : UserControl
    {



        public int secuencial_categoria { get; set; }
        public int secuencial { get; set; }
        public int secuencial_proveedor { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public double cantidad { get; set; }
        public double precio_costo { get; set; }
        public double precio_venta { get; set; }
        public string marca { get; set; }

        public string codigo_barra { get; set; }

        public string codigo_fabricante { get; set; }

        public string codigo_qr { get; set; }

        public string urlImagen { get; set; }
        public string proveedor { get; set; }
        public string comentario { get; set; }



        //Propiedades del Control
        public bool actualizarItem { get; set; }





        ToolTip toolTip = new ToolTip();
        public Form vista { get; set; }
        public Form vistaEditar { get; set; }

        public bool seleccionado { get; set; }
        public double unidadesAgregar { get; set; }
        public double unidadesRetirar { get; set; }



        public void RedondearEsquinas(PictureBox picBox, int radio)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radio * 2, radio * 2, 180, 90);
            path.AddArc(picBox.Width - radio * 2, 0, radio * 2, radio * 2, 270, 90);
            path.AddArc(picBox.Width - radio * 2, picBox.Height - radio * 2, radio * 2, radio * 2, 0, 90);
            path.AddArc(0, picBox.Height - radio * 2, radio * 2, radio * 2, 90, 90);
            path.CloseFigure();

            picBox.Region = new Region(path);
        }




        public void cargarVista(Form vista)
        {
            Form nuevaVista = new Form();
            nuevaVista = vista;


            if (nuevaVista != null)
            {

                nuevaVista.Show();


            }



        }





        public bool estaSelecccionado()
        {
            return this.seleccionado; ;
        }

        public void cargarVistaEditar(Form vistaEditar)
        {
            Form nuevaVista = new Form();
            nuevaVista = vistaEditar;


            if (nuevaVista != null)
            {

                nuevaVista.Show();


            }



        }


        public double getUnidadesRetirar()
        {
            string respuesta = Interaction.InputBox("Escriba la cantidad en numeros a retirar de este producto:", "Retirar Unidades");
            //  MessageBox.Show(respuesta, "Retirar Unidades");
            ///

            if (int.TryParse(respuesta, out int numero))
            {
                unidadesRetirar = numero;
                cantidad = cantidad - unidadesRetirar;
                actualizarItem = true;
                return numero;
            }
            else
            {
                MessageBox.Show("Error: Solo se permiten números.", "Retirar Unidades");
                return 0;
            }

            ///

        }



        public double getUnidadesAgregar()
        {
            string respuesta = Interaction.InputBox("Escriba la cantidad en numeros a agregar de este producto:", "Agregar Unidades");
            // MessageBox.Show(respuesta, "Agregar Unidades");
            ///

            if (int.TryParse(respuesta, out int numero))
            {
                unidadesAgregar = numero;
                cantidad = cantidad + unidadesAgregar;
                actualizarItem = true;
                return numero;

            }
            else
            {
                MessageBox.Show("Error: Solo se permiten números.", "Agregar Unidades");

                return 0;
            }

            ///

        }


        public string getComentario()
        {
            string respuesta = Interaction.InputBox("Escriba el comentario que estara asociado a este producto:", "Comentario");
            MessageBox.Show(respuesta, "Comentario");
            actualizarItem = true;
            return respuesta;
        }


        public string getImagenWeb()
        {
            string imagenWeb = Interaction.InputBox("Pegue aqui la direccion de la imagen a la que estara asociada a este producto:", "Imagen Web");


            urlImagen = imagenWeb;
            if (urlImagen != "")
            {
                actualizarItem = true;

            }

            return imagenWeb;
        }



        public Miniatura_Inventario()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            Imagen.ContextMenuStrip = Menu;
            Codigo.Text = codigo;
            Precio.Text = precio_venta.ToString();
            Codigo.BackColor = Color.Transparent;
            Precio.BackColor = Color.Transparent;
            //pictureBox1.BackColor = Color.Transparent;

            try
            {
                Imagen.Load(urlImagen);
            }
            catch { }
        }

        private void UserControl1_MouseLeave(object sender, EventArgs e)
        {
            if (seleccionado != true)
            {
                this.BorderStyle = BorderStyle.FixedSingle;
                this.BackColor = Control.DefaultBackColor;
                //this.BackColor = Color.White;
                Codigo.Font = new Font(Codigo.Font, FontStyle.Regular);
                Codigo.ForeColor = Color.Black;
                Precio.ForeColor = Codigo.ForeColor;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Check.Checked == true)
            {
                seleccionado = true;
                this.BackColor = Color.LightBlue;

            }

            else
            {
                seleccionado = false;
            }
        }

        private void UserControl1_Paint(object sender, PaintEventArgs e)
        {




            Codigo.BackColor = Color.Transparent;
            Precio.BackColor = Color.Transparent;
            //pictureBox1.BackColor = Color.Transparent;




            Check.Checked = seleccionado;
            Imagen.ContextMenuStrip = Menu;
            //Precio.Text = precio_venta.ToString();
            try
            {
                Imagen.Load(urlImagen);

            }

            catch { }

           // Codigo.Text = codigo;
        }

        public void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        public void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {



            openFileDialog1.ShowDialog();
            urlImagen = openFileDialog1.FileName;
            try
            {

                Imagen.Load(urlImagen);
                actualizarItem = true;

            }
            catch
            {

            }


        }

        private void cambiarImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            urlImagen = getImagenWeb();
            if (urlImagen != "")
            {

                try
                {
                    Imagen.Load(urlImagen);
                }
                catch { }


            }
        }

        private void agregarComentarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.comentario = getComentario();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            getUnidadesAgregar();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            getUnidadesRetirar();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            cargarVistaEditar(vistaEditar);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Check.Checked == true)
            {
                Check.Checked = false;
            }
            else
            {
                Check.Checked = true;
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            if (seleccionado != true)
            {
                //this.BorderStyle = BorderStyle.Fixed3D;
                Codigo.Text = codigo;
                Precio.Text = precio_venta.ToString();
                Codigo.Font = new Font(Codigo.Font, FontStyle.Bold);
                Precio.ForeColor = Codigo.ForeColor;

                if (cantidad == 0 || cantidad < 0)
                {
                    this.BackColor = Color.Red;
                    Codigo.ForeColor = Color.White;
                    Precio.ForeColor = Color.White;
                }


                else if (cantidad > 0 && cantidad <= 3)
                {
                    this.BackColor = Color.Yellow;
                    Codigo.ForeColor = Color.Coral;
                    Precio.ForeColor = Color.Coral;
                }

                else
                {

                    this.BackColor = Color.LightGreen;
                    Codigo.ForeColor = Color.BlueViolet;
                    Precio.ForeColor = Color.BlueViolet;

                }


            }




            toolTip.SetToolTip(Imagen, "Marca: " + marca + "\nPrecio: " + precio_venta + "\nStock: " + cantidad + "\n" + comentario);
            Check.BackColor = Color.Transparent;
            Check.Checked = seleccionado;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            if (seleccionado != true)
            {

                this.BackColor = Control.DefaultBackColor;
                Codigo.Font = new Font(Codigo.Font, FontStyle.Regular);
                Codigo.ForeColor = Color.Black;
                Precio.ForeColor = Color.Black;
            }

            //this.BackColor = Color.White;

        }

        private void Miniatura_Inventario_MouseHover(object sender, EventArgs e)
        {
            if (seleccionado != true)
            {
                //this.BorderStyle = BorderStyle.Fixed3D;
                Codigo.Text = codigo;
                Precio.Text = precio_venta.ToString();
                Codigo.Font = new Font(Codigo.Font, FontStyle.Bold);
                Precio.ForeColor = Codigo.ForeColor;

                if (cantidad == 0 || cantidad < 0)
                {
                    this.BackColor = Color.Red;
                    Codigo.ForeColor = Color.White;
                    Precio.ForeColor = Color.White;
                }


                else if (cantidad > 0 && cantidad <= 3)
                {
                    this.BackColor = Color.Yellow;
                    Codigo.ForeColor = Color.Coral;
                    Precio.ForeColor = Color.Coral;
                }

                else
                {

                    this.BackColor = Color.LightGreen;
                    Codigo.ForeColor = Color.BlueViolet;
                    Precio.ForeColor = Color.BlueViolet;

                }
            }
        }
    }
}
