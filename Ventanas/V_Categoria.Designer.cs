namespace Monitux_POS.Ventanas
{
    partial class V_Categoria
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Categoria));
            dataGridView1 = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            txtNombre = new TextBox();
            menuStrip1 = new MenuStrip();
            opcionesToolStripMenuItem = new ToolStripMenuItem();
            nuevoToolStripMenuItem1 = new ToolStripMenuItem();
            guardarToolStripMenuItem = new ToolStripMenuItem();
            eliminarToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            cerrarToolStripMenuItem = new ToolStripMenuItem();
            txtDescripcion = new TextBox();
            textBox1 = new TextBox();
            comboBox1 = new ComboBox();
            groupBox1 = new GroupBox();
            label3 = new Label();
            panel1 = new Panel();
            pictureBox6 = new PictureBox();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(11, 155);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(361, 161);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellEnter += dataGridView1_CellEnter;
            dataGridView1.CellLeave += dataGridView1_CellLeave;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            dataGridView1.KeyPress += dataGridView1_KeyPress;
            dataGridView1.KeyUp += dataGridView1_KeyUp;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(11, 14);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 1;
            label1.Text = "Nombre:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(94, 44);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 2;
            label2.Text = "Descripcion:";
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(262, 6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(110, 114);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            txtNombre.ForeColor = Color.Black;
            txtNombre.Location = new Point(71, 11);
            txtNombre.MaxLength = 20;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(185, 23);
            txtNombre.TabIndex = 4;
            txtNombre.TextChanged += textBox1_TextChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(11, 8, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { opcionesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(381, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // opcionesToolStripMenuItem
            // 
            opcionesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { nuevoToolStripMenuItem1, guardarToolStripMenuItem, eliminarToolStripMenuItem, toolStripSeparator1, cerrarToolStripMenuItem });
            opcionesToolStripMenuItem.ForeColor = Color.FromArgb(128, 255, 128);
            opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            opcionesToolStripMenuItem.Size = new Size(69, 20);
            opcionesToolStripMenuItem.Text = "Opciones";
            opcionesToolStripMenuItem.Click += opcionesToolStripMenuItem_Click;
            // 
            // nuevoToolStripMenuItem1
            // 
            nuevoToolStripMenuItem1.Image = (Image)resources.GetObject("nuevoToolStripMenuItem1.Image");
            nuevoToolStripMenuItem1.Name = "nuevoToolStripMenuItem1";
            nuevoToolStripMenuItem1.Size = new Size(117, 22);
            nuevoToolStripMenuItem1.Text = "Nuevo";
            nuevoToolStripMenuItem1.Click += nuevoToolStripMenuItem1_Click;
            // 
            // guardarToolStripMenuItem
            // 
            guardarToolStripMenuItem.ForeColor = Color.FromArgb(0, 192, 0);
            guardarToolStripMenuItem.Image = (Image)resources.GetObject("guardarToolStripMenuItem.Image");
            guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            guardarToolStripMenuItem.Size = new Size(117, 22);
            guardarToolStripMenuItem.Text = "Guardar";
            guardarToolStripMenuItem.Click += guardarToolStripMenuItem_Click;
            // 
            // eliminarToolStripMenuItem
            // 
            eliminarToolStripMenuItem.ForeColor = Color.Red;
            eliminarToolStripMenuItem.Image = (Image)resources.GetObject("eliminarToolStripMenuItem.Image");
            eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            eliminarToolStripMenuItem.Size = new Size(117, 22);
            eliminarToolStripMenuItem.Text = "Eliminar";
            eliminarToolStripMenuItem.Click += eliminarToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(114, 6);
            // 
            // cerrarToolStripMenuItem
            // 
            cerrarToolStripMenuItem.Image = (Image)resources.GetObject("cerrarToolStripMenuItem.Image");
            cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            cerrarToolStripMenuItem.Size = new Size(117, 22);
            cerrarToolStripMenuItem.Text = "Salir";
            cerrarToolStripMenuItem.Click += cerrarToolStripMenuItem_Click;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            txtDescripcion.ForeColor = Color.Black;
            txtDescripcion.Location = new Point(11, 62);
            txtDescripcion.MaxLength = 110;
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(245, 58);
            txtDescripcion.TabIndex = 6;
            // 
            // textBox1
            // 
            textBox1.ForeColor = Color.FromArgb(192, 0, 192);
            textBox1.Location = new Point(223, 22);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Quiero encontrar...";
            textBox1.Size = new Size(118, 23);
            textBox1.TabIndex = 8;
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.TextChanged += textBox1_TextChanged_1;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(96, 22);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 9;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(11, 314);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(361, 55);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(24, 25);
            label3.Name = "label3";
            label3.Size = new Size(66, 15);
            label3.TabIndex = 10;
            label3.Text = "Buscar por:";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 32, 40);
            panel1.Controls.Add(pictureBox6);
            panel1.Controls.Add(txtDescripcion);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(dataGridView1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtNombre);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(4, 76);
            panel1.Name = "panel1";
            panel1.Size = new Size(382, 375);
            panel1.TabIndex = 11;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(299, 124);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(31, 25);
            pictureBox6.TabIndex = 65;
            pictureBox6.TabStop = false;
            pictureBox6.Click += pictureBox6_Click;
            // 
            // label4
            // 
            label4.Dock = DockStyle.Top;
            label4.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(0, 24);
            label4.Name = "label4";
            label4.Size = new Size(381, 37);
            label4.TabIndex = 12;
            label4.Text = "Categorias";
            label4.TextAlign = ContentAlignment.TopCenter;
            // 
            // V_Categoria
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            ClientSize = new Size(381, 452);
            ControlBox = false;
            Controls.Add(label4);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "V_Categoria";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Categoria";
            Load += V_Categoria_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label1;
        private Label label2;
        private PictureBox pictureBox1;
        private TextBox txtNombre;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem opcionesToolStripMenuItem;
        private TextBox txtDescripcion;
        private ToolStripMenuItem guardarToolStripMenuItem;
        private ToolStripMenuItem eliminarToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem cerrarToolStripMenuItem;
        private ToolStripMenuItem nuevoToolStripMenuItem1;
        private TextBox textBox1;
        private ComboBox comboBox1;
        private GroupBox groupBox1;
        private Label label3;
        private Panel panel1;
        private Label label4;
        private PictureBox pictureBox6;
    }
}