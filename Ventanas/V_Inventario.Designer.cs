namespace Monitux_POS.Ventanas
{
    partial class V_Inventario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Inventario));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panel1 = new Panel();
            panel4 = new Panel();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            label8 = new Label();
            pictureBox1 = new PictureBox();
            label4 = new Label();
            comboBox3 = new ComboBox();
            textBox1 = new TextBox();
            panel3 = new Panel();
            dataGridView1 = new DataGridView();
            flowLayoutPanel1 = new FlowLayoutPanel();
            dataGridView2 = new DataGridView();
            panel2 = new Panel();
            button8 = new Button();
            button2 = new Button();
            button3 = new Button();
            button1 = new Button();
            button4 = new Button();
            groupBox1 = new GroupBox();
            button9 = new Button();
            textBox2 = new TextBox();
            label10 = new Label();
            comboBox1 = new ComboBox();
            fecha_fin = new DateTimePicker();
            label9 = new Label();
            label7 = new Label();
            label5 = new Label();
            fecha_inicio = new DateTimePicker();
            groupBox2 = new GroupBox();
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            dateTimePicker1 = new DateTimePicker();
            comboProveedor = new ComboBox();
            label3 = new Label();
            comboCategoria = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            label6 = new Label();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(44, 117, 255);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label6);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(795, 591);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // panel4
            // 
            panel4.Controls.Add(linkLabel2);
            panel4.Controls.Add(linkLabel1);
            panel4.Controls.Add(label8);
            panel4.Controls.Add(pictureBox1);
            panel4.Controls.Add(label4);
            panel4.Controls.Add(comboBox3);
            panel4.Controls.Add(textBox1);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 543);
            panel4.Name = "panel4";
            panel4.Size = new Size(795, 39);
            panel4.TabIndex = 62;
            // 
            // linkLabel2
            // 
            linkLabel2.BackColor = Color.FromArgb(35, 32, 45);
            linkLabel2.LinkBehavior = LinkBehavior.HoverUnderline;
            linkLabel2.LinkColor = Color.Red;
            linkLabel2.Location = new Point(559, 8);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(66, 23);
            linkLabel2.TabIndex = 62;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Agotados";
            linkLabel2.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel2.Visible = false;
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.BackColor = Color.FromArgb(35, 32, 45);
            linkLabel1.LinkBehavior = LinkBehavior.HoverUnderline;
            linkLabel1.LinkColor = Color.Yellow;
            linkLabel1.Location = new Point(449, 8);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(102, 23);
            linkLabel1.TabIndex = 61;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Existencia Minima";
            linkLabel1.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel1.Visible = false;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label8
            // 
            label8.ForeColor = Color.White;
            label8.Location = new Point(635, 16);
            label8.Name = "label8";
            label8.Size = new Size(109, 15);
            label8.TabIndex = 60;
            label8.Text = "Modo: Cuadricula";
            label8.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(750, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 36);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 59;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(13, 11);
            label4.Name = "label4";
            label4.Size = new Size(66, 15);
            label4.TabIndex = 58;
            label4.Text = "Buscar Por:";
            // 
            // comboBox3
            // 
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(85, 8);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(132, 23);
            comboBox3.TabIndex = 57;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.ForeColor = Color.Fuchsia;
            textBox1.Location = new Point(223, 8);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Quiero Encontrar...";
            textBox1.Size = new Size(204, 23);
            textBox1.TabIndex = 56;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // panel3
            // 
            panel3.Controls.Add(dataGridView1);
            panel3.Controls.Add(flowLayoutPanel1);
            panel3.Controls.Add(dataGridView2);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 139);
            panel3.Name = "panel3";
            panel3.Size = new Size(795, 404);
            panel3.TabIndex = 61;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(14, -1);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(773, 405);
            dataGridView1.TabIndex = 53;
            dataGridView1.Visible = false;
            dataGridView1.MouseDoubleClick += dataGridView1_MouseDoubleClick;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = Color.FromArgb(35, 32, 45);
            flowLayoutPanel1.Location = new Point(12, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(774, 405);
            flowLayoutPanel1.TabIndex = 54;
            flowLayoutPanel1.Visible = false;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(12, 0);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(775, 405);
            dataGridView2.TabIndex = 55;
            dataGridView2.Visible = false;
            dataGridView2.CellDoubleClick += dataGridView2_CellDoubleClick;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(44, 117, 255);
            panel2.Controls.Add(button8);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button3);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(button4);
            panel2.Controls.Add(groupBox2);
            panel2.Controls.Add(groupBox1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 37);
            panel2.Name = "panel2";
            panel2.Size = new Size(795, 102);
            panel2.TabIndex = 60;
            panel2.Paint += panel2_Paint;
            // 
            // button8
            // 
            button8.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            button8.FlatStyle = FlatStyle.Flat;
            button8.ForeColor = Color.White;
            button8.Image = (Image)resources.GetObject("button8.Image");
            button8.Location = new Point(12, 10);
            button8.Name = "button8";
            button8.Size = new Size(75, 89);
            button8.TabIndex = 64;
            button8.Text = "Nuevo Producto";
            button8.TextAlign = ContentAlignment.BottomCenter;
            button8.TextImageRelation = TextImageRelation.ImageAboveText;
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button2
            // 
            button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = Color.White;
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.Location = new Point(366, 10);
            button2.Name = "button2";
            button2.Size = new Size(75, 89);
            button2.TabIndex = 20;
            button2.Text = "Servicios";
            button2.TextAlign = ContentAlignment.BottomCenter;
            button2.TextImageRelation = TextImageRelation.ImageAboveText;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            button3.FlatStyle = FlatStyle.Flat;
            button3.ForeColor = Color.White;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.Location = new Point(271, 10);
            button3.Name = "button3";
            button3.Size = new Size(75, 89);
            button3.TabIndex = 18;
            button3.Text = "Kardex";
            button3.TextAlign = ContentAlignment.BottomCenter;
            button3.TextImageRelation = TextImageRelation.ImageAboveText;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button1
            // 
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(194, 10);
            button1.Name = "button1";
            button1.Size = new Size(75, 89);
            button1.TabIndex = 19;
            button1.Text = "Lista";
            button1.TextAlign = ContentAlignment.BottomCenter;
            button1.TextImageRelation = TextImageRelation.ImageAboveText;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button4
            // 
            button4.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            button4.FlatStyle = FlatStyle.Flat;
            button4.ForeColor = Color.White;
            button4.Image = (Image)resources.GetObject("button4.Image");
            button4.Location = new Point(117, 10);
            button4.Name = "button4";
            button4.Size = new Size(75, 89);
            button4.TabIndex = 53;
            button4.Text = "Cuadricula";
            button4.TextAlign = ContentAlignment.BottomCenter;
            button4.TextImageRelation = TextImageRelation.ImageAboveText;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button9);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(fecha_fin);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(fecha_inicio);
            groupBox1.Location = new Point(446, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(339, 97);
            groupBox1.TabIndex = 65;
            groupBox1.TabStop = false;
            groupBox1.Visible = false;
            // 
            // button9
            // 
            button9.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            button9.FlatStyle = FlatStyle.Flat;
            button9.ForeColor = Color.White;
            button9.Image = (Image)resources.GetObject("button9.Image");
            button9.Location = new Point(256, 13);
            button9.Name = "button9";
            button9.Size = new Size(75, 77);
            button9.TabIndex = 68;
            button9.Text = "Ejecutar Consulta";
            button9.TextAlign = ContentAlignment.BottomCenter;
            button9.TextImageRelation = TextImageRelation.ImageAboveText;
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(28, 64);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 67;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = Color.White;
            label10.Location = new Point(28, 46);
            label10.Name = "label10";
            label10.Size = new Size(49, 15);
            label10.TabIndex = 66;
            label10.Text = "Codigo:";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Entrada", "Salida" });
            comboBox1.Location = new Point(148, 64);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(100, 23);
            comboBox1.TabIndex = 65;
            // 
            // fecha_fin
            // 
            fecha_fin.Format = DateTimePickerFormat.Short;
            fecha_fin.Location = new Point(150, 13);
            fecha_fin.Name = "fecha_fin";
            fecha_fin.Size = new Size(100, 23);
            fecha_fin.TabIndex = 64;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(132, 19);
            label9.Name = "label9";
            label9.Size = new Size(18, 15);
            label9.TabIndex = 63;
            label9.Text = "A:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(148, 46);
            label7.Name = "label7";
            label7.Size = new Size(75, 15);
            label7.TabIndex = 62;
            label7.Text = "Movimiento:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(4, 19);
            label5.Name = "label5";
            label5.Size = new Size(24, 15);
            label5.TabIndex = 61;
            label5.Text = "De:";
            // 
            // fecha_inicio
            // 
            fecha_inicio.Format = DateTimePickerFormat.Short;
            fecha_inicio.Location = new Point(28, 13);
            fecha_inicio.Name = "fecha_inicio";
            fecha_inicio.Size = new Size(100, 23);
            fecha_inicio.TabIndex = 59;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.FromArgb(44, 117, 255);
            groupBox2.Controls.Add(button7);
            groupBox2.Controls.Add(button6);
            groupBox2.Controls.Add(button5);
            groupBox2.Controls.Add(dateTimePicker1);
            groupBox2.Controls.Add(comboProveedor);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(comboCategoria);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new Point(447, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(339, 96);
            groupBox2.TabIndex = 63;
            groupBox2.TabStop = false;
            groupBox2.Visible = false;
            // 
            // button7
            // 
            button7.Location = new Point(283, 70);
            button7.Name = "button7";
            button7.Size = new Size(48, 23);
            button7.TabIndex = 62;
            button7.Text = "1 Año";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click_1;
            // 
            // button6
            // 
            button6.Location = new Point(229, 69);
            button6.Name = "button6";
            button6.Size = new Size(48, 23);
            button6.TabIndex = 61;
            button6.Text = "1 Mes";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click_1;
            // 
            // button5
            // 
            button5.Location = new Point(166, 69);
            button5.Name = "button5";
            button5.Size = new Size(58, 23);
            button5.TabIndex = 60;
            button5.Text = "1 Sem";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click_1;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(74, 69);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(85, 23);
            dateTimePicker1.TabIndex = 58;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // comboProveedor
            // 
            comboProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            comboProveedor.FormattingEnabled = true;
            comboProveedor.Location = new Point(75, 16);
            comboProveedor.Name = "comboProveedor";
            comboProveedor.Size = new Size(256, 23);
            comboProveedor.TabIndex = 56;
            comboProveedor.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            comboProveedor.Click += comboProveedor_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(29, 75);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 59;
            label3.Text = "Expira:";
            // 
            // comboCategoria
            // 
            comboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCategoria.FormattingEnabled = true;
            comboCategoria.Location = new Point(75, 43);
            comboCategoria.Name = "comboCategoria";
            comboCategoria.Size = new Size(256, 23);
            comboCategoria.TabIndex = 54;
            comboCategoria.SelectedIndexChanged += comboCategoria_SelectedIndexChanged;
            comboCategoria.Click += comboCategoria_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(10, 46);
            label2.Name = "label2";
            label2.Size = new Size(61, 15);
            label2.TabIndex = 57;
            label2.Text = "Categoria:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(7, 19);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 55;
            label1.Text = "Proveedor:";
            // 
            // label6
            // 
            label6.BackColor = Color.FromArgb(11, 8, 20);
            label6.Dock = DockStyle.Top;
            label6.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(0, 0);
            label6.Name = "label6";
            label6.Size = new Size(795, 37);
            label6.TabIndex = 52;
            label6.Text = "Inventario";
            label6.TextAlign = ContentAlignment.TopCenter;
            label6.Click += label6_Click;
            label6.MouseMove += label6_MouseMove;
            // 
            // V_Inventario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(44, 117, 255);
            ClientSize = new Size(795, 591);
            ControlBox = false;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "V_Inventario";
            Text = "V_Inventario";
            FormClosing += V_Inventario_FormClosing;
            Load += V_Inventario_Load;
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            panel2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label6;
        private DataGridView dataGridView1;
        private Button button3;
        private Button button2;
        private Button button1;
        private Button button4;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private ComboBox comboProveedor;
        private Label label1;
        private ComboBox comboCategoria;
        private TextBox textBox1;
        private ComboBox comboBox3;
        private Label label4;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox2;
        private Button button7;
        private Button button6;
        private Button button5;
        private PictureBox pictureBox1;
        private Label label8;
        private Button button8;
        private DataGridView dataGridView2;
        private GroupBox groupBox1;
        private DateTimePicker fecha_inicio;
        private ComboBox comboBox1;
        private DateTimePicker fecha_fin;
        private Label label9;
        private Label label7;
        private Label label5;
        private TextBox textBox2;
        private Label label10;
        private Button button9;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel1;
    }
}