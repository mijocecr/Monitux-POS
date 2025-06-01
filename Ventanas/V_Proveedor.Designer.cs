namespace Monitux_POS.Ventanas
{
    partial class V_Proveedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Proveedor));
            label1 = new Label();
            txt_Nombre = new TextBox();
            txt_Telefono = new TextBox();
            label2 = new Label();
            txt_Direccion = new TextBox();
            label3 = new Label();
            txt_Email = new TextBox();
            label4 = new Label();
            txt_Contacto = new TextBox();
            label5 = new Label();
            label6 = new Label();
            checkBox1 = new CheckBox();
            combo_Tipo = new ComboBox();
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            nuevoToolStripMenuItem = new ToolStripMenuItem();
            Menu_Guardar = new ToolStripMenuItem();
            Menu_Eliminar = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            salirToolStripMenuItem = new ToolStripMenuItem();
            pictureBox1 = new PictureBox();
            dataGridView1 = new DataGridView();
            groupBox1 = new GroupBox();
            label7 = new Label();
            comboBox2 = new ComboBox();
            textBox1 = new TextBox();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 39);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 0;
            label1.Text = "Nombre:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_Nombre
            // 
            txt_Nombre.Location = new Point(81, 36);
            txt_Nombre.Name = "txt_Nombre";
            txt_Nombre.Size = new Size(205, 23);
            txt_Nombre.TabIndex = 1;
            // 
            // txt_Telefono
            // 
            txt_Telefono.Location = new Point(81, 65);
            txt_Telefono.Name = "txt_Telefono";
            txt_Telefono.Size = new Size(205, 23);
            txt_Telefono.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 68);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 2;
            label2.Text = "Telefono:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_Direccion
            // 
            txt_Direccion.Location = new Point(81, 93);
            txt_Direccion.Name = "txt_Direccion";
            txt_Direccion.Size = new Size(205, 23);
            txt_Direccion.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 96);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 4;
            label3.Text = "Direccion:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_Email
            // 
            txt_Email.Location = new Point(81, 123);
            txt_Email.Name = "txt_Email";
            txt_Email.Size = new Size(205, 23);
            txt_Email.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(27, 126);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 6;
            label4.Text = "Email:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_Contacto
            // 
            txt_Contacto.Location = new Point(81, 151);
            txt_Contacto.Name = "txt_Contacto";
            txt_Contacto.Size = new Size(205, 23);
            txt_Contacto.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 154);
            label5.Name = "label5";
            label5.Size = new Size(59, 15);
            label5.TabIndex = 8;
            label5.Text = "Contacto:";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(32, 183);
            label6.Name = "label6";
            label6.Size = new Size(34, 15);
            label6.TabIndex = 10;
            label6.Text = "Tipo:";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(322, 190);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(60, 19);
            checkBox1.TabIndex = 12;
            checkBox1.Text = "Activo";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // combo_Tipo
            // 
            combo_Tipo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_Tipo.FormattingEnabled = true;
            combo_Tipo.Items.AddRange(new object[] { "Local", "Nacional", "Internacional", "Virtual" });
            combo_Tipo.Location = new Point(81, 180);
            combo_Tipo.Name = "combo_Tipo";
            combo_Tipo.Size = new Size(107, 23);
            combo_Tipo.TabIndex = 13;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ControlLightLight;
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(420, 24);
            menuStrip1.TabIndex = 14;
            menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { nuevoToolStripMenuItem, Menu_Guardar, Menu_Eliminar, toolStripSeparator1, salirToolStripMenuItem });
            archivoToolStripMenuItem.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            archivoToolStripMenuItem.ForeColor = Color.DarkViolet;
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
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(292, 36);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(122, 138);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 15;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 224);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(402, 199);
            dataGridView1.TabIndex = 16;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellEnter += dataGridView1_CellEnter;
            dataGridView1.CellLeave += dataGridView1_CellLeave;
            dataGridView1.CellMouseLeave += dataGridView1_CellMouseLeave;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(12, 432);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(402, 55);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
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
            textBox1.Size = new Size(139, 23);
            textBox1.TabIndex = 8;
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // V_Proveedor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 499);
            ControlBox = false;
            Controls.Add(groupBox1);
            Controls.Add(dataGridView1);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            Controls.Add(combo_Tipo);
            Controls.Add(checkBox1);
            Controls.Add(label6);
            Controls.Add(txt_Contacto);
            Controls.Add(label5);
            Controls.Add(txt_Email);
            Controls.Add(label4);
            Controls.Add(txt_Direccion);
            Controls.Add(label3);
            Controls.Add(txt_Telefono);
            Controls.Add(label2);
            Controls.Add(txt_Nombre);
            Controls.Add(label1);
            MaximizeBox = false;
            Name = "V_Proveedor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Proveedores";
            Load += V_Proveedor_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txt_Nombre;
        private TextBox txt_Telefono;
        private Label label2;
        private TextBox txt_Direccion;
        private Label label3;
        private TextBox txt_Email;
        private Label label4;
        private TextBox txt_Contacto;
        private Label label5;
        private Label label6;
        private CheckBox checkBox1;
        private ComboBox combo_Tipo;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem Menu_Guardar;
        private ToolStripMenuItem Menu_Eliminar;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem salirToolStripMenuItem;
        private PictureBox pictureBox1;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private Label label7;
        private ComboBox comboBox2;
        private TextBox textBox1;
        private ToolStripMenuItem nuevoToolStripMenuItem;
    }
}