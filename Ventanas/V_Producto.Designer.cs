namespace Monitux_POS.Ventanas
{
    partial class V_Producto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Producto));
            panel1 = new Panel();
            label12 = new Label();
            comboBox1 = new ComboBox();
            checkBox1 = new CheckBox();
            dateTimePicker1 = new DateTimePicker();
            comboCategoria = new ComboBox();
            txtCodigoFabricante = new TextBox();
            label8 = new Label();
            comboProveedor = new ComboBox();
            txtExistenciaMinima = new TextBox();
            txtPrecioVenta = new TextBox();
            txtPrecioCosto = new TextBox();
            txtCodigoBarra = new TextBox();
            txtMarca = new TextBox();
            txtCantidad = new TextBox();
            txtCodigo = new TextBox();
            label11 = new Label();
            label10 = new Label();
            txtDescripcion = new TextBox();
            label9 = new Label();
            label7 = new Label();
            label6 = new Label();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            Menu_Agregar = new ToolStripMenuItem();
            nuevoProveedorToolStripMenuItem = new ToolStripMenuItem();
            categoriaToolStripMenuItem = new ToolStripMenuItem();
            Menu_Guardar = new ToolStripMenuItem();
            Menu_Eliminar = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            salirToolStripMenuItem = new ToolStripMenuItem();
            label13 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 32, 40);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(dateTimePicker1);
            panel1.Controls.Add(comboCategoria);
            panel1.Controls.Add(txtCodigoFabricante);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(comboProveedor);
            panel1.Controls.Add(txtExistenciaMinima);
            panel1.Controls.Add(txtPrecioVenta);
            panel1.Controls.Add(txtPrecioCosto);
            panel1.Controls.Add(txtCodigoBarra);
            panel1.Controls.Add(txtMarca);
            panel1.Controls.Add(txtCantidad);
            panel1.Controls.Add(txtCodigo);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(txtDescripcion);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(8, 67);
            panel1.Name = "panel1";
            panel1.Size = new Size(402, 448);
            panel1.TabIndex = 0;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = Color.White;
            label12.Location = new Point(248, 275);
            label12.Name = "label12";
            label12.Size = new Size(34, 15);
            label12.TabIndex = 30;
            label12.Text = "Tipo:";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Producto", "Servicio" });
            comboBox1.Location = new Point(288, 272);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(91, 23);
            comboBox1.TabIndex = 29;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.ForeColor = Color.White;
            checkBox1.Location = new Point(9, 333);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(109, 19);
            checkBox1.TabIndex = 28;
            checkBox1.Text = "Producto Expira";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(124, 329);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(103, 23);
            dateTimePicker1.TabIndex = 27;
            dateTimePicker1.Visible = false;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // comboCategoria
            // 
            comboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCategoria.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            comboCategoria.ForeColor = SystemColors.MenuHighlight;
            comboCategoria.FormattingEnabled = true;
            comboCategoria.Location = new Point(9, 157);
            comboCategoria.Name = "comboCategoria";
            comboCategoria.Size = new Size(218, 23);
            comboCategoria.TabIndex = 25;
            comboCategoria.SelectedIndexChanged += comboCategoria_SelectedIndexChanged_2;
            comboCategoria.SelectedValueChanged += comboCategoria_SelectedValueChanged;
            comboCategoria.MouseClick += comboCategoria_MouseClick_1;
            // 
            // txtCodigoFabricante
            // 
            txtCodigoFabricante.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            txtCodigoFabricante.ForeColor = SystemColors.MenuHighlight;
            txtCodigoFabricante.Location = new Point(117, 300);
            txtCodigoFabricante.MaxLength = 20;
            txtCodigoFabricante.Name = "txtCodigoFabricante";
            txtCodigoFabricante.Size = new Size(110, 23);
            txtCodigoFabricante.TabIndex = 10;
            txtCodigoFabricante.TextAlign = HorizontalAlignment.Center;
            txtCodigoFabricante.TextChanged += textBox1_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.White;
            label8.Location = new Point(5, 303);
            label8.Name = "label8";
            label8.Size = new Size(107, 15);
            label8.TabIndex = 24;
            label8.Text = "Codigo Fabricante:";
            label8.TextAlign = ContentAlignment.MiddleRight;
            // 
            // comboProveedor
            // 
            comboProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            comboProveedor.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            comboProveedor.ForeColor = SystemColors.MenuHighlight;
            comboProveedor.FormattingEnabled = true;
            comboProveedor.Location = new Point(9, 113);
            comboProveedor.Name = "comboProveedor";
            comboProveedor.Size = new Size(218, 23);
            comboProveedor.TabIndex = 4;
            comboProveedor.DropDown += comboProveedor_DropDown;
            comboProveedor.SelectedIndexChanged += comboProveedor_SelectedIndexChanged;
            comboProveedor.SelectedValueChanged += comboProveedor_SelectedValueChanged;
            comboProveedor.MouseClick += comboProveedor_MouseClick;
            // 
            // txtExistenciaMinima
            // 
            txtExistenciaMinima.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            txtExistenciaMinima.ForeColor = SystemColors.MenuHighlight;
            txtExistenciaMinima.Location = new Point(117, 272);
            txtExistenciaMinima.MaxLength = 4;
            txtExistenciaMinima.Name = "txtExistenciaMinima";
            txtExistenciaMinima.Size = new Size(51, 23);
            txtExistenciaMinima.TabIndex = 9;
            txtExistenciaMinima.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPrecioVenta
            // 
            txtPrecioVenta.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            txtPrecioVenta.ForeColor = SystemColors.MenuHighlight;
            txtPrecioVenta.Location = new Point(96, 245);
            txtPrecioVenta.MaxLength = 10;
            txtPrecioVenta.Name = "txtPrecioVenta";
            txtPrecioVenta.Size = new Size(132, 23);
            txtPrecioVenta.TabIndex = 8;
            txtPrecioVenta.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPrecioCosto
            // 
            txtPrecioCosto.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            txtPrecioCosto.ForeColor = SystemColors.MenuHighlight;
            txtPrecioCosto.Location = new Point(96, 218);
            txtPrecioCosto.MaxLength = 10;
            txtPrecioCosto.Name = "txtPrecioCosto";
            txtPrecioCosto.Size = new Size(132, 23);
            txtPrecioCosto.TabIndex = 7;
            txtPrecioCosto.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCodigoBarra
            // 
            txtCodigoBarra.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            txtCodigoBarra.ForeColor = SystemColors.MenuHighlight;
            txtCodigoBarra.Location = new Point(97, 189);
            txtCodigoBarra.MaxLength = 20;
            txtCodigoBarra.Name = "txtCodigoBarra";
            txtCodigoBarra.Size = new Size(132, 23);
            txtCodigoBarra.TabIndex = 6;
            txtCodigoBarra.TextAlign = HorizontalAlignment.Center;
            txtCodigoBarra.TextChanged += txtCodigoBarra_TextChanged;
            // 
            // txtMarca
            // 
            txtMarca.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            txtMarca.ForeColor = SystemColors.MenuHighlight;
            txtMarca.Location = new Point(76, 67);
            txtMarca.MaxLength = 20;
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(121, 23);
            txtMarca.TabIndex = 3;
            txtMarca.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCantidad
            // 
            txtCantidad.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            txtCantidad.ForeColor = SystemColors.MenuHighlight;
            txtCantidad.Location = new Point(76, 36);
            txtCantidad.MaxLength = 6;
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(121, 23);
            txtCantidad.TabIndex = 2;
            txtCantidad.TextAlign = HorizontalAlignment.Center;
            // 
            // txtCodigo
            // 
            txtCodigo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            txtCodigo.ForeColor = SystemColors.MenuHighlight;
            txtCodigo.Location = new Point(76, 7);
            txtCodigo.MaxLength = 15;
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(121, 23);
            txtCodigo.TabIndex = 1;
            txtCodigo.TextAlign = HorizontalAlignment.Center;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = Color.White;
            label11.Location = new Point(6, 277);
            label11.Name = "label11";
            label11.Size = new Size(105, 15);
            label11.TabIndex = 14;
            label11.Text = "Existencia Minima:";
            label11.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = Color.White;
            label10.Location = new Point(76, 358);
            label10.Name = "label10";
            label10.Size = new Size(69, 15);
            label10.TabIndex = 13;
            label10.Text = "Descripcion";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            txtDescripcion.ForeColor = SystemColors.MenuHighlight;
            txtDescripcion.Location = new Point(9, 377);
            txtDescripcion.MaxLength = 110;
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ScrollBars = ScrollBars.Vertical;
            txtDescripcion.Size = new Size(222, 68);
            txtDescripcion.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(9, 139);
            label9.Name = "label9";
            label9.Size = new Size(58, 15);
            label9.TabIndex = 11;
            label9.Text = "Categoria";
            label9.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(9, 192);
            label7.Name = "label7";
            label7.Size = new Size(84, 15);
            label7.TabIndex = 9;
            label7.Text = "Codigo Barras:";
            label7.TextAlign = ContentAlignment.MiddleRight;
            label7.Click += label7_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.White;
            label6.Location = new Point(9, 95);
            label6.Name = "label6";
            label6.Size = new Size(61, 15);
            label6.TabIndex = 8;
            label6.Text = "Proveedor";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(235, 3);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(159, 64);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(235, 305);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(159, 140);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(6, 38);
            label5.Name = "label5";
            label5.Size = new Size(58, 15);
            label5.TabIndex = 5;
            label5.Text = "Cantidad:";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(9, 251);
            label4.Name = "label4";
            label4.Size = new Size(75, 15);
            label4.TabIndex = 4;
            label4.Text = "Precio Venta:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(9, 223);
            label3.Name = "label3";
            label3.Size = new Size(77, 15);
            label3.TabIndex = 3;
            label3.Text = "Precio Costo:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(9, 67);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 2;
            label2.Text = "Marca:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(9, 11);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 1;
            label1.Text = "Codigo:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(235, 73);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(159, 192);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(11, 8, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(415, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { Menu_Agregar, Menu_Guardar, Menu_Eliminar, toolStripSeparator1, salirToolStripMenuItem });
            archivoToolStripMenuItem.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            archivoToolStripMenuItem.ForeColor = Color.FromArgb(128, 255, 128);
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(69, 20);
            archivoToolStripMenuItem.Text = "Opciones";
            archivoToolStripMenuItem.Click += archivoToolStripMenuItem_Click_1;
            // 
            // Menu_Agregar
            // 
            Menu_Agregar.BackColor = SystemColors.Control;
            Menu_Agregar.DropDownItems.AddRange(new ToolStripItem[] { nuevoProveedorToolStripMenuItem, categoriaToolStripMenuItem });
            Menu_Agregar.ForeColor = Color.Black;
            Menu_Agregar.Image = (Image)resources.GetObject("Menu_Agregar.Image");
            Menu_Agregar.Name = "Menu_Agregar";
            Menu_Agregar.Size = new Size(120, 22);
            Menu_Agregar.Text = "Registrar";
            // 
            // nuevoProveedorToolStripMenuItem
            // 
            nuevoProveedorToolStripMenuItem.Image = (Image)resources.GetObject("nuevoProveedorToolStripMenuItem.Image");
            nuevoProveedorToolStripMenuItem.Name = "nuevoProveedorToolStripMenuItem";
            nuevoProveedorToolStripMenuItem.Size = new Size(128, 22);
            nuevoProveedorToolStripMenuItem.Text = "Proveedor";
            nuevoProveedorToolStripMenuItem.Click += nuevoProveedorToolStripMenuItem_Click;
            // 
            // categoriaToolStripMenuItem
            // 
            categoriaToolStripMenuItem.Image = (Image)resources.GetObject("categoriaToolStripMenuItem.Image");
            categoriaToolStripMenuItem.Name = "categoriaToolStripMenuItem";
            categoriaToolStripMenuItem.Size = new Size(128, 22);
            categoriaToolStripMenuItem.Text = "Categoria";
            categoriaToolStripMenuItem.Click += categoriaToolStripMenuItem_Click;
            // 
            // Menu_Guardar
            // 
            Menu_Guardar.BackColor = SystemColors.Control;
            Menu_Guardar.ForeColor = Color.ForestGreen;
            Menu_Guardar.Image = (Image)resources.GetObject("Menu_Guardar.Image");
            Menu_Guardar.Name = "Menu_Guardar";
            Menu_Guardar.Size = new Size(120, 22);
            Menu_Guardar.Text = "Guardar";
            Menu_Guardar.Click += Menu_Guardar_Click;
            // 
            // Menu_Eliminar
            // 
            Menu_Eliminar.BackColor = SystemColors.Control;
            Menu_Eliminar.ForeColor = Color.Red;
            Menu_Eliminar.Image = (Image)resources.GetObject("Menu_Eliminar.Image");
            Menu_Eliminar.Name = "Menu_Eliminar";
            Menu_Eliminar.Size = new Size(120, 22);
            Menu_Eliminar.Text = "Eliminar";
            Menu_Eliminar.Click += Menu_Eliminar_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.BackColor = Color.FromArgb(35, 32, 40);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(117, 6);
            // 
            // salirToolStripMenuItem
            // 
            salirToolStripMenuItem.BackColor = SystemColors.Control;
            salirToolStripMenuItem.Image = (Image)resources.GetObject("salirToolStripMenuItem.Image");
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(120, 22);
            salirToolStripMenuItem.Text = "Salir";
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
            // 
            // label13
            // 
            label13.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.White;
            label13.Location = new Point(11, 24);
            label13.Name = "label13";
            label13.Size = new Size(394, 37);
            label13.TabIndex = 37;
            label13.Text = "Productos";
            label13.TextAlign = ContentAlignment.TopCenter;
            // 
            // V_Producto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            ClientSize = new Size(415, 523);
            ControlBox = false;
            Controls.Add(label13);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "V_Producto";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Producto";
            Load += V_Producto_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label3;
        private Label label2;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label6;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private Label label5;
        private Label label4;
        private Label label7;
        private Label label10;
        private TextBox txtDescripcion;
        private Label label9;
        private Label label11;
        private TextBox txtMarca;
        private TextBox txtCantidad;
        private TextBox txtCodigo;
        private TextBox txtExistenciaMinima;
        private TextBox txtPrecioVenta;
        private TextBox txtPrecioCosto;
        private TextBox txtCodigoBarra;
        private ComboBox comboProveedor;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem Menu_Guardar;
        private ToolStripMenuItem Menu_Eliminar;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem salirToolStripMenuItem;
        private TextBox txtCodigoFabricante;
        private Label label8;
        private ComboBox comboCategoria;
        private ToolStripMenuItem Menu_Agregar;
        private ToolStripMenuItem nuevoProveedorToolStripMenuItem;
        private ToolStripMenuItem categoriaToolStripMenuItem;
        private DateTimePicker dateTimePicker1;
        private CheckBox checkBox1;
        private Label label12;
        private ComboBox comboBox1;
        private Label label13;
    }
}