namespace Monitux_POS.Ventanas
{
    partial class V_Cliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Cliente));
            groupBox1 = new GroupBox();
            label7 = new Label();
            comboBox2 = new ComboBox();
            textBox1 = new TextBox();
            dataGridView1 = new DataGridView();
            pictureBox1 = new PictureBox();
            checkBox1 = new CheckBox();
            txt_Email = new TextBox();
            label5 = new Label();
            txt_Direccion = new TextBox();
            label4 = new Label();
            txt_Telefono = new TextBox();
            label3 = new Label();
            txt_Nombre = new TextBox();
            label2 = new Label();
            txt_Codigo = new TextBox();
            label1 = new Label();
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            nuevoToolStripMenuItem = new ToolStripMenuItem();
            Menu_Guardar = new ToolStripMenuItem();
            Menu_Eliminar = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            salirToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            pictureBox6 = new PictureBox();
            label6 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(7, 382);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(402, 55);
            groupBox1.TabIndex = 33;
            groupBox1.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(8, 25);
            label7.Name = "label7";
            label7.Size = new Size(66, 15);
            label7.TabIndex = 10;
            label7.Text = "Buscar por:";
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(80, 22);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(121, 23);
            comboBox2.TabIndex = 9;
            // 
            // textBox1
            // 
            textBox1.ForeColor = Color.FromArgb(192, 0, 192);
            textBox1.Location = new Point(229, 22);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Quiero encontrar...";
            textBox1.Size = new Size(139, 23);
            textBox1.TabIndex = 8;
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Cursor = Cursors.Hand;
            dataGridView1.Location = new Point(6, 183);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(402, 199);
            dataGridView1.TabIndex = 32;
            dataGridView1.CellClick += dataGridView1_CellClick_1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.CellEnter += dataGridView1_CellEnter;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(287, 8);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(122, 138);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 31;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.ForeColor = Color.White;
            checkBox1.Location = new Point(218, 156);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(60, 19);
            checkBox1.TabIndex = 29;
            checkBox1.Text = "Activo";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // txt_Email
            // 
            txt_Email.Location = new Point(76, 123);
            txt_Email.Name = "txt_Email";
            txt_Email.Size = new Size(205, 23);
            txt_Email.TabIndex = 27;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(22, 126);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 26;
            label5.Text = "Email:";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_Direccion
            // 
            txt_Direccion.Location = new Point(76, 95);
            txt_Direccion.Name = "txt_Direccion";
            txt_Direccion.Size = new Size(205, 23);
            txt_Direccion.TabIndex = 25;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(4, 98);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 24;
            label4.Text = "Direccion:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_Telefono
            // 
            txt_Telefono.Location = new Point(76, 65);
            txt_Telefono.Name = "txt_Telefono";
            txt_Telefono.Size = new Size(205, 23);
            txt_Telefono.TabIndex = 23;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(7, 68);
            label3.Name = "label3";
            label3.Size = new Size(56, 15);
            label3.TabIndex = 22;
            label3.Text = "Telefono:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_Nombre
            // 
            txt_Nombre.Location = new Point(76, 37);
            txt_Nombre.Name = "txt_Nombre";
            txt_Nombre.Size = new Size(205, 23);
            txt_Nombre.TabIndex = 21;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(12, 40);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 20;
            label2.Text = "Nombre:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_Codigo
            // 
            txt_Codigo.Location = new Point(76, 8);
            txt_Codigo.Name = "txt_Codigo";
            txt_Codigo.Size = new Size(205, 23);
            txt_Codigo.TabIndex = 19;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 18;
            label1.Text = "Codigo:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(11, 8, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(426, 24);
            menuStrip1.TabIndex = 34;
            menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { nuevoToolStripMenuItem, Menu_Guardar, Menu_Eliminar, toolStripSeparator1, salirToolStripMenuItem });
            archivoToolStripMenuItem.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            archivoToolStripMenuItem.ForeColor = Color.FromArgb(128, 255, 128);
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(69, 20);
            archivoToolStripMenuItem.Text = "Opciones";
            archivoToolStripMenuItem.Click += archivoToolStripMenuItem_Click;
            // 
            // nuevoToolStripMenuItem
            // 
            nuevoToolStripMenuItem.Image = (Image)resources.GetObject("nuevoToolStripMenuItem.Image");
            nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            nuevoToolStripMenuItem.Size = new Size(117, 22);
            nuevoToolStripMenuItem.Text = "Nuevo";
            nuevoToolStripMenuItem.Click += nuevoToolStripMenuItem_Click;
            // 
            // Menu_Guardar
            // 
            Menu_Guardar.ForeColor = Color.ForestGreen;
            Menu_Guardar.Image = (Image)resources.GetObject("Menu_Guardar.Image");
            Menu_Guardar.Name = "Menu_Guardar";
            Menu_Guardar.Size = new Size(117, 22);
            Menu_Guardar.Text = "Guardar";
            Menu_Guardar.Click += Menu_Guardar_Click;
            // 
            // Menu_Eliminar
            // 
            Menu_Eliminar.ForeColor = Color.Red;
            Menu_Eliminar.Image = (Image)resources.GetObject("Menu_Eliminar.Image");
            Menu_Eliminar.Name = "Menu_Eliminar";
            Menu_Eliminar.Size = new Size(117, 22);
            Menu_Eliminar.Text = "Eliminar";
            Menu_Eliminar.Click += Menu_Eliminar_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(114, 6);
            // 
            // salirToolStripMenuItem
            // 
            salirToolStripMenuItem.Image = (Image)resources.GetObject("salirToolStripMenuItem.Image");
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(117, 22);
            salirToolStripMenuItem.Text = "Salir";
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 32, 40);
            panel1.Controls.Add(pictureBox6);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(txt_Codigo);
            panel1.Controls.Add(dataGridView1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txt_Nombre);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txt_Email);
            panel1.Controls.Add(txt_Telefono);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(txt_Direccion);
            panel1.Location = new Point(5, 74);
            panel1.Name = "panel1";
            panel1.Size = new Size(417, 447);
            panel1.TabIndex = 35;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(333, 152);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(31, 25);
            pictureBox6.TabIndex = 66;
            pictureBox6.TabStop = false;
            pictureBox6.Click += pictureBox6_Click;
            // 
            // label6
            // 
            label6.Dock = DockStyle.Top;
            label6.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(0, 24);
            label6.Name = "label6";
            label6.Size = new Size(426, 37);
            label6.TabIndex = 36;
            label6.Text = "Clientes";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // V_Cliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            ClientSize = new Size(426, 530);
            ControlBox = false;
            Controls.Add(label6);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "V_Cliente";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Clientes";
            Load += V_Cliente_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Label label7;
        private ComboBox comboBox2;
        private TextBox textBox1;
        private DataGridView dataGridView1;
        private PictureBox pictureBox1;
        private CheckBox checkBox1;
        private TextBox txt_Email;
        private Label label5;
        private TextBox txt_Direccion;
        private Label label4;
        private TextBox txt_Telefono;
        private Label label3;
        private TextBox txt_Nombre;
        private Label label2;
        private TextBox txt_Codigo;
        private Label label1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem nuevoToolStripMenuItem;
        private ToolStripMenuItem Menu_Guardar;
        private ToolStripMenuItem Menu_Eliminar;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem salirToolStripMenuItem;
        private Panel panel1;
        private Label label6;
        private PictureBox pictureBox6;
    }
}