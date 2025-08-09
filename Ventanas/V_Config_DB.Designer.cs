namespace Monitux_POS.Ventanas
{
    partial class V_Config_DB
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Config_DB));
            panel1 = new Panel();
            textBox5 = new TextBox();
            label5 = new Label();
            textBox4 = new TextBox();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            instalarSQLServerToolStripMenuItem = new ToolStripMenuItem();
            obtenerCadenaDeConexionDeInstanciaToolStripMenuItem = new ToolStripMenuItem();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            contextMenuStrip2 = new ContextMenuStrip(components);
            instalarMySQLToolStripMenuItem = new ToolStripMenuItem();
            datosDeConexionAInstanciaToolStripMenuItem = new ToolStripMenuItem();
            label4 = new Label();
            textBox3 = new TextBox();
            label3 = new Label();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            label2 = new Label();
            textBox1 = new TextBox();
            label1 = new Label();
            comboBox1 = new ComboBox();
            label15 = new Label();
            textBox2 = new TextBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            contextMenuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(textBox5);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(textBox4);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(comboBox1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(472, 318);
            panel1.TabIndex = 0;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(72, 206);
            textBox5.Name = "textBox5";
            textBox5.PlaceholderText = "Servidor";
            textBox5.Size = new Size(271, 23);
            textBox5.TabIndex = 53;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(13, 210);
            label5.Name = "label5";
            label5.Size = new Size(53, 15);
            label5.TabIndex = 52;
            label5.Text = "Servidor:";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(243, 236);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 23);
            textBox4.TabIndex = 51;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(11, 8, 20);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(pictureBox3);
            panel2.Controls.Add(pictureBox2);
            panel2.Location = new Point(22, 6);
            panel2.Name = "panel2";
            panel2.Size = new Size(287, 100);
            panel2.TabIndex = 46;
            // 
            // pictureBox1
            // 
            pictureBox1.ContextMenuStrip = contextMenuStrip1;
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Image = Properties.Resources.microsoft_sql_server_logo_png_seeklogo_298266;
            pictureBox1.Location = new Point(11, 10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(84, 81);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 43;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            pictureBox1.MouseEnter += pictureBox1_MouseEnter;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { instalarSQLServerToolStripMenuItem, obtenerCadenaDeConexionDeInstanciaToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(233, 48);
            // 
            // instalarSQLServerToolStripMenuItem
            // 
            instalarSQLServerToolStripMenuItem.Name = "instalarSQLServerToolStripMenuItem";
            instalarSQLServerToolStripMenuItem.Size = new Size(232, 22);
            instalarSQLServerToolStripMenuItem.Text = "Instalar SQL Server";
            instalarSQLServerToolStripMenuItem.Click += instalarSQLServerToolStripMenuItem_Click;
            // 
            // obtenerCadenaDeConexionDeInstanciaToolStripMenuItem
            // 
            obtenerCadenaDeConexionDeInstanciaToolStripMenuItem.Name = "obtenerCadenaDeConexionDeInstanciaToolStripMenuItem";
            obtenerCadenaDeConexionDeInstanciaToolStripMenuItem.Size = new Size(232, 22);
            obtenerCadenaDeConexionDeInstanciaToolStripMenuItem.Text = "Datos de Conexion a Instancia";
            obtenerCadenaDeConexionDeInstanciaToolStripMenuItem.Click += obtenerCadenaDeConexionDeInstanciaToolStripMenuItem_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(193, 10);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(84, 81);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 45;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            pictureBox3.MouseEnter += pictureBox3_MouseEnter;
            pictureBox3.MouseLeave += pictureBox3_MouseLeave;
            // 
            // pictureBox2
            // 
            pictureBox2.ContextMenuStrip = contextMenuStrip2;
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(102, 10);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(84, 81);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 44;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            pictureBox2.MouseEnter += pictureBox2_MouseEnter;
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] { instalarMySQLToolStripMenuItem, datosDeConexionAInstanciaToolStripMenuItem });
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new Size(233, 48);
            // 
            // instalarMySQLToolStripMenuItem
            // 
            instalarMySQLToolStripMenuItem.Name = "instalarMySQLToolStripMenuItem";
            instalarMySQLToolStripMenuItem.Size = new Size(232, 22);
            instalarMySQLToolStripMenuItem.Text = "Instalar MySQL";
            instalarMySQLToolStripMenuItem.Click += instalarMySQLToolStripMenuItem_Click;
            // 
            // datosDeConexionAInstanciaToolStripMenuItem
            // 
            datosDeConexionAInstanciaToolStripMenuItem.Name = "datosDeConexionAInstanciaToolStripMenuItem";
            datosDeConexionAInstanciaToolStripMenuItem.Size = new Size(232, 22);
            datosDeConexionAInstanciaToolStripMenuItem.Text = "Datos de Conexion a Instancia";
            datosDeConexionAInstanciaToolStripMenuItem.Click += datosDeConexionAInstanciaToolStripMenuItem_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(178, 239);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 50;
            label4.Text = "Password:";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(72, 236);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 49;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(16, 239);
            label3.Name = "label3";
            label3.Size = new Size(50, 15);
            label3.TabIndex = 48;
            label3.Text = "Usuario:";
            // 
            // button3
            // 
            button3.FlatStyle = FlatStyle.Flat;
            button3.ForeColor = Color.White;
            button3.Location = new Point(362, 61);
            button3.Name = "button3";
            button3.Size = new Size(101, 45);
            button3.TabIndex = 8;
            button3.Text = "Generar Cadena de Conexion";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = Color.White;
            button2.Location = new Point(362, 206);
            button2.Name = "button2";
            button2.Size = new Size(101, 100);
            button2.TabIndex = 7;
            button2.Text = "Guardar y Continuar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(362, 6);
            button1.Name = "button1";
            button1.Size = new Size(101, 45);
            button1.TabIndex = 4;
            button1.Text = "Probar Conexion";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(22, 114);
            label2.Name = "label2";
            label2.Size = new Size(119, 15);
            label2.TabIndex = 3;
            label2.Text = "Cadena de Conexion:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(18, 133);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(445, 59);
            textBox1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(59, 275);
            label1.Name = "label1";
            label1.Size = new Size(113, 15);
            label1.TabIndex = 1;
            label1.Text = "Proveedor de Datos:";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "SQLITE", "MYSQL", "SQLSERVER" });
            comboBox1.Location = new Point(177, 272);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(166, 23);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label15
            // 
            label15.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.ForeColor = Color.White;
            label15.Location = new Point(62, 333);
            label15.Name = "label15";
            label15.Size = new Size(350, 37);
            label15.TabIndex = 40;
            label15.Text = "Monitux-POS ver. 1.5";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(35, 32, 45);
            textBox2.ForeColor = Color.FromArgb(255, 255, 192);
            textBox2.Location = new Point(17, 379);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.Size = new Size(467, 182);
            textBox2.TabIndex = 47;
            textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // V_Config_DB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 32, 45);
            ClientSize = new Size(494, 575);
            Controls.Add(textBox2);
            Controls.Add(label15);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "V_Config_DB";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ajustes de Proveedor de Datos";
            Load += V_Config_DB_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            contextMenuStrip2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private ComboBox comboBox1;
        private Button button1;
        private Label label2;
        private TextBox textBox1;
        private Label label1;
        private Button button2;
        private Label label15;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Panel panel2;
        private TextBox textBox2;
        private Button button3;
        private Label label3;
        private TextBox textBox5;
        private Label label5;
        private TextBox textBox4;
        private Label label4;
        private TextBox textBox3;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem instalarSQLServerToolStripMenuItem;
        private ToolStripMenuItem obtenerCadenaDeConexionDeInstanciaToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem instalarMySQLToolStripMenuItem;
        private ToolStripMenuItem datosDeConexionAInstanciaToolStripMenuItem;
    }
}