namespace Monitux_POS.Ventanas
{
    partial class V_Usuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Usuario));
            groupBox1 = new GroupBox();
            label7 = new Label();
            comboBox2 = new ComboBox();
            textBox1 = new TextBox();
            dataGridView1 = new DataGridView();
            pictureBox1 = new PictureBox();
            checkBox1 = new CheckBox();
            label4 = new Label();
            txt_Password = new TextBox();
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
            comboBox1 = new ComboBox();
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            label6 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(4, 322);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(402, 55);
            groupBox1.TabIndex = 47;
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
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(4, 154);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(402, 162);
            dataGridView1.TabIndex = 46;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellEnter += dataGridView1_CellEnter;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(286, 7);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(122, 138);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 45;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.ForeColor = Color.White;
            checkBox1.Location = new Point(220, 94);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(60, 19);
            checkBox1.TabIndex = 44;
            checkBox1.Text = "Activo";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(17, 97);
            label4.Name = "label4";
            label4.Size = new Size(48, 15);
            label4.TabIndex = 40;
            label4.Text = "Acceso:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_Password
            // 
            txt_Password.Location = new Point(75, 64);
            txt_Password.Name = "txt_Password";
            txt_Password.PasswordChar = '*';
            txt_Password.Size = new Size(205, 23);
            txt_Password.TabIndex = 39;
            txt_Password.TextChanged += txt_Password_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(6, 67);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 38;
            label3.Text = "Password:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_Nombre
            // 
            txt_Nombre.Location = new Point(75, 36);
            txt_Nombre.Name = "txt_Nombre";
            txt_Nombre.Size = new Size(205, 23);
            txt_Nombre.TabIndex = 37;
            txt_Nombre.TextChanged += txt_Nombre_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(11, 39);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 36;
            label2.Text = "Nombre:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_Codigo
            // 
            txt_Codigo.Location = new Point(75, 7);
            txt_Codigo.Name = "txt_Codigo";
            txt_Codigo.Size = new Size(205, 23);
            txt_Codigo.TabIndex = 35;
            txt_Codigo.TextChanged += txt_Codigo_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(16, 10);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 34;
            label1.Text = "Codigo:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(11, 8, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(423, 24);
            menuStrip1.TabIndex = 48;
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
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(75, 94);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 49;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 32, 40);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txt_Codigo);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(dataGridView1);
            panel1.Controls.Add(txt_Nombre);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(txt_Password);
            panel1.Controls.Add(label4);
            panel1.Location = new Point(5, 65);
            panel1.Name = "panel1";
            panel1.Size = new Size(413, 384);
            panel1.TabIndex = 50;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(249, 120);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(31, 25);
            pictureBox2.TabIndex = 50;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // label6
            // 
            label6.Dock = DockStyle.Top;
            label6.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(0, 24);
            label6.Name = "label6";
            label6.Size = new Size(423, 37);
            label6.TabIndex = 51;
            label6.Text = "Usuarios";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // V_Usuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            ClientSize = new Size(423, 455);
            ControlBox = false;
            Controls.Add(label6);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "V_Usuario";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Usuarios";
            Load += V_Usuario_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
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
        private Label label4;
        private TextBox txt_Password;
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
        private ComboBox comboBox1;
        private Panel panel1;
        private Label label6;
        private PictureBox pictureBox2;
    }
}