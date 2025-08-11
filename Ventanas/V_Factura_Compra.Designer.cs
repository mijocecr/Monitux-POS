namespace Monitux_POS.Ventanas
{
    partial class V_Factura_Compra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Factura_Compra));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            label16 = new Label();
            label15 = new Label();
            groupBox2 = new GroupBox();
            checkBox1 = new CheckBox();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            txt_Descuento = new TextBox();
            txt_Impuesto = new TextBox();
            txt_OtrosCargos = new TextBox();
            lbl_Descuento = new Label();
            lbl_Impuesto = new Label();
            lbl_OtrosCargos = new Label();
            lbl_sub_Total = new Label();
            lbl_Total = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            comboBox1 = new ComboBox();
            label7 = new Label();
            label6 = new Label();
            textBox2 = new TextBox();
            linkLabel3 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            dataGridView1 = new DataGridView();
            button6 = new Button();
            label3 = new Label();
            button1 = new Button();
            comboCliente = new ComboBox();
            dateTimePicker1 = new DateTimePicker();
            label1 = new Label();
            comboBox3 = new ComboBox();
            label2 = new Label();
            flowLayoutPanel2 = new FlowLayoutPanel();
            button7 = new Button();
            label5 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            button8 = new Button();
            button2 = new Button();
            groupBox1 = new GroupBox();
            comboBox2 = new ComboBox();
            textBox1 = new TextBox();
            label4 = new Label();
            button3 = new Button();
            button5 = new Button();
            button4 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(11, 8, 20);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label16);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(flowLayoutPanel2);
            panel1.Controls.Add(button7);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(flowLayoutPanel1);
            panel1.Controls.Add(button8);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button4);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(798, 592);
            panel1.TabIndex = 35;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(747, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(48, 43);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 51;
            pictureBox1.TabStop = false;
            pictureBox1.Visible = false;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.ForeColor = Color.Yellow;
            label16.Location = new Point(8, 22);
            label16.Name = "label16";
            label16.Size = new Size(372, 15);
            label16.TabIndex = 39;
            label16.Text = "Mecanica: Señale Producto -> Click -> Cantidad -> Actualizar Detalle";
            // 
            // label15
            // 
            label15.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.ForeColor = Color.FromArgb(128, 255, 255);
            label15.Location = new Point(412, 9);
            label15.Name = "label15";
            label15.Size = new Size(379, 37);
            label15.TabIndex = 38;
            label15.Text = "Registrar Compra";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            label15.Click += label15_Click;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.FromArgb(0, 75, 160);
            groupBox2.Controls.Add(checkBox1);
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(txt_Descuento);
            groupBox2.Controls.Add(txt_Impuesto);
            groupBox2.Controls.Add(txt_OtrosCargos);
            groupBox2.Controls.Add(lbl_Descuento);
            groupBox2.Controls.Add(lbl_Impuesto);
            groupBox2.Controls.Add(lbl_OtrosCargos);
            groupBox2.Controls.Add(lbl_sub_Total);
            groupBox2.Controls.Add(lbl_Total);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(comboBox1);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(linkLabel3);
            groupBox2.Controls.Add(linkLabel2);
            groupBox2.Controls.Add(linkLabel1);
            groupBox2.Controls.Add(dataGridView1);
            groupBox2.Controls.Add(button6);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(comboCliente);
            groupBox2.Controls.Add(dateTimePicker1);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(comboBox3);
            groupBox2.Controls.Add(label2);
            groupBox2.ForeColor = Color.Black;
            groupBox2.Location = new Point(412, 40);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(379, 543);
            groupBox2.TabIndex = 26;
            groupBox2.TabStop = false;
            groupBox2.Enter += groupBox2_Enter;
            groupBox2.MouseHover += groupBox2_MouseHover;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.ForeColor = Color.White;
            checkBox1.Location = new Point(256, 407);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(112, 19);
            checkBox1.TabIndex = 49;
            checkBox1.Text = "Calcular cambio";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.Visible = false;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.ForeColor = Color.White;
            label14.Location = new Point(167, 125);
            label14.Name = "label14";
            label14.Size = new Size(46, 15);
            label14.TabIndex = 48;
            label14.Text = "Detalle:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.ForeColor = Color.White;
            label13.Location = new Point(167, 406);
            label13.Name = "label13";
            label13.Size = new Size(17, 15);
            label13.TabIndex = 47;
            label13.Text = "%";
            label13.Visible = false;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = Color.White;
            label12.Location = new Point(166, 380);
            label12.Name = "label12";
            label12.Size = new Size(17, 15);
            label12.TabIndex = 46;
            label12.Text = "%";
            label12.Visible = false;
            // 
            // txt_Descuento
            // 
            txt_Descuento.Location = new Point(127, 403);
            txt_Descuento.Name = "txt_Descuento";
            txt_Descuento.Size = new Size(39, 23);
            txt_Descuento.TabIndex = 45;
            txt_Descuento.TextAlign = HorizontalAlignment.Center;
            txt_Descuento.Visible = false;
            txt_Descuento.TextChanged += txt_Descuento_TextChanged;
            txt_Descuento.KeyDown += txt_Descuento_KeyDown;
            // 
            // txt_Impuesto
            // 
            txt_Impuesto.Location = new Point(126, 376);
            txt_Impuesto.Name = "txt_Impuesto";
            txt_Impuesto.Size = new Size(39, 23);
            txt_Impuesto.TabIndex = 44;
            txt_Impuesto.TextAlign = HorizontalAlignment.Center;
            txt_Impuesto.Visible = false;
            txt_Impuesto.TextChanged += txt_Impuesto_TextChanged;
            txt_Impuesto.KeyDown += txt_Impuesto_KeyDown;
            // 
            // txt_OtrosCargos
            // 
            txt_OtrosCargos.Location = new Point(145, 350);
            txt_OtrosCargos.Name = "txt_OtrosCargos";
            txt_OtrosCargos.Size = new Size(47, 23);
            txt_OtrosCargos.TabIndex = 43;
            txt_OtrosCargos.TextAlign = HorizontalAlignment.Center;
            txt_OtrosCargos.Visible = false;
            txt_OtrosCargos.TextChanged += txt_OtrosCargos_TextChanged;
            txt_OtrosCargos.KeyDown += txt_OtrosCargos_KeyDown;
            // 
            // lbl_Descuento
            // 
            lbl_Descuento.AutoSize = true;
            lbl_Descuento.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_Descuento.ForeColor = Color.Yellow;
            lbl_Descuento.Location = new Point(111, 502);
            lbl_Descuento.Name = "lbl_Descuento";
            lbl_Descuento.Size = new Size(15, 17);
            lbl_Descuento.TabIndex = 42;
            lbl_Descuento.Text = "0";
            lbl_Descuento.Visible = false;
            // 
            // lbl_Impuesto
            // 
            lbl_Impuesto.AutoSize = true;
            lbl_Impuesto.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_Impuesto.ForeColor = Color.Yellow;
            lbl_Impuesto.Location = new Point(111, 473);
            lbl_Impuesto.Name = "lbl_Impuesto";
            lbl_Impuesto.Size = new Size(15, 17);
            lbl_Impuesto.TabIndex = 41;
            lbl_Impuesto.Text = "0";
            lbl_Impuesto.Visible = false;
            // 
            // lbl_OtrosCargos
            // 
            lbl_OtrosCargos.AutoSize = true;
            lbl_OtrosCargos.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_OtrosCargos.ForeColor = Color.Yellow;
            lbl_OtrosCargos.Location = new Point(111, 444);
            lbl_OtrosCargos.Name = "lbl_OtrosCargos";
            lbl_OtrosCargos.Size = new Size(15, 17);
            lbl_OtrosCargos.TabIndex = 40;
            lbl_OtrosCargos.Text = "0";
            lbl_OtrosCargos.Visible = false;
            // 
            // lbl_sub_Total
            // 
            lbl_sub_Total.AutoSize = true;
            lbl_sub_Total.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_sub_Total.ForeColor = Color.Yellow;
            lbl_sub_Total.Location = new Point(288, 331);
            lbl_sub_Total.Name = "lbl_sub_Total";
            lbl_sub_Total.Size = new Size(19, 21);
            lbl_sub_Total.TabIndex = 39;
            lbl_sub_Total.Text = "0";
            // 
            // lbl_Total
            // 
            lbl_Total.AutoSize = true;
            lbl_Total.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            lbl_Total.ForeColor = Color.Yellow;
            lbl_Total.Location = new Point(263, 365);
            lbl_Total.Name = "lbl_Total";
            lbl_Total.Size = new Size(25, 30);
            lbl_Total.TabIndex = 38;
            lbl_Total.Text = "0";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.White;
            label11.Location = new Point(33, 502);
            label11.Name = "label11";
            label11.Size = new Size(77, 17);
            label11.TabIndex = 37;
            label11.Text = "Descuento:";
            label11.Visible = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.White;
            label10.Location = new Point(40, 473);
            label10.Name = "label10";
            label10.Size = new Size(70, 17);
            label10.TabIndex = 36;
            label10.Text = "Impuesto:";
            label10.Visible = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.White;
            label9.Location = new Point(18, 444);
            label9.Name = "label9";
            label9.Size = new Size(92, 17);
            label9.TabIndex = 35;
            label9.Text = "Otros Cargos:";
            label9.Visible = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.White;
            label8.Location = new Point(203, 331);
            label8.Name = "label8";
            label8.Size = new Size(87, 21);
            label8.TabIndex = 34;
            label8.Text = "Sub-Total:";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Efectivo", "Tarjeta", "Otro", "Ninguno" });
            comboBox1.Location = new Point(111, 94);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(91, 23);
            comboBox1.TabIndex = 33;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(6, 97);
            label7.Name = "label7";
            label7.Size = new Size(98, 15);
            label7.TabIndex = 32;
            label7.Text = "Metodo de Pago:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(201, 367);
            label6.Name = "label6";
            label6.Size = new Size(67, 30);
            label6.TabIndex = 31;
            label6.Text = "Total:";
            // 
            // textBox2
            // 
            textBox2.ForeColor = Color.FromArgb(192, 0, 192);
            textBox2.Location = new Point(296, 35);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Telefono...";
            textBox2.Size = new Size(75, 23);
            textBox2.TabIndex = 30;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.LinkColor = Color.FromArgb(192, 255, 192);
            linkLabel3.Location = new Point(18, 353);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(121, 15);
            linkLabel3.TabIndex = 28;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "Agregar Otros Cargos";
            linkLabel3.LinkClicked += linkLabel3_LinkClicked;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.LinkColor = Color.FromArgb(192, 255, 192);
            linkLabel2.Location = new Point(18, 406);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(108, 15);
            linkLabel2.TabIndex = 27;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Agregar Descuento";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.LinkColor = Color.FromArgb(192, 255, 192);
            linkLabel1.Location = new Point(18, 379);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(102, 15);
            linkLabel1.TabIndex = 26;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Agregar Impuesto";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 143);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.RowHeadersWidth = 10;
            dataGridView1.Size = new Size(365, 181);
            dataGridView1.TabIndex = 8;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(35, 32, 45);
            button6.FlatAppearance.BorderColor = Color.White;
            button6.FlatAppearance.BorderSize = 0;
            button6.ForeColor = Color.FromArgb(128, 255, 255);
            button6.Location = new Point(296, 438);
            button6.Name = "button6";
            button6.Size = new Size(75, 90);
            button6.TabIndex = 21;
            button6.Text = "Generar Compra";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(226, 72);
            label3.Name = "label3";
            label3.Size = new Size(56, 15);
            label3.TabIndex = 25;
            label3.Text = "Se vence:";
            label3.Visible = false;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(35, 32, 45);
            button1.FlatAppearance.BorderColor = Color.White;
            button1.FlatAppearance.BorderSize = 0;
            button1.ForeColor = Color.Yellow;
            button1.Location = new Point(215, 440);
            button1.Name = "button1";
            button1.Size = new Size(75, 88);
            button1.TabIndex = 20;
            button1.Text = "Generar Orden";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // comboCliente
            // 
            comboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCliente.FormattingEnabled = true;
            comboCliente.Location = new Point(72, 35);
            comboCliente.Name = "comboCliente";
            comboCliente.Size = new Size(218, 23);
            comboCliente.TabIndex = 9;
            comboCliente.SelectedIndexChanged += comboCliente_SelectedIndexChanged;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(288, 68);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(83, 23);
            dateTimePicker1.TabIndex = 24;
            dateTimePicker1.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(7, 38);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 10;
            label1.Text = "Proveedor:";
            // 
            // comboBox3
            // 
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "Contado", "Credito" });
            comboBox3.Location = new Point(111, 66);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(91, 23);
            comboBox3.TabIndex = 23;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(7, 72);
            label2.Name = "label2";
            label2.Size = new Size(96, 15);
            label2.TabIndex = 22;
            label2.Text = "Tipo de Compra:";
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoScroll = true;
            flowLayoutPanel2.BackColor = Color.FromArgb(35, 32, 45);
            flowLayoutPanel2.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Location = new Point(159, 495);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(168, 88);
            flowLayoutPanel2.TabIndex = 32;
            flowLayoutPanel2.WrapContents = false;
            // 
            // button7
            // 
            button7.BackColor = Color.FromArgb(11, 8, 20);
            button7.FlatAppearance.BorderColor = Color.FromArgb(252, 114, 95);
            button7.FlatAppearance.MouseDownBackColor = Color.FromArgb(252, 114, 95);
            button7.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            button7.FlatStyle = FlatStyle.Flat;
            button7.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button7.ForeColor = Color.White;
            button7.Image = (Image)resources.GetObject("button7.Image");
            button7.Location = new Point(5, 509);
            button7.Name = "button7";
            button7.Size = new Size(74, 74);
            button7.TabIndex = 27;
            button7.Text = "Reset Factura";
            button7.TextImageRelation = TextImageRelation.ImageAboveText;
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.DodgerBlue;
            label5.Location = new Point(123, 489);
            label5.Name = "label5";
            label5.Size = new Size(15, 17);
            label5.TabIndex = 31;
            label5.Text = "0";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = Color.FromArgb(35, 32, 45);
            flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel1.Location = new Point(8, 144);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(396, 342);
            flowLayoutPanel1.TabIndex = 7;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            flowLayoutPanel1.MouseMove += flowLayoutPanel1_MouseMove;
            // 
            // button8
            // 
            button8.BackColor = Color.FromArgb(11, 8, 20);
            button8.FlatStyle = FlatStyle.Flat;
            button8.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button8.ForeColor = Color.White;
            button8.Image = (Image)resources.GetObject("button8.Image");
            button8.Location = new Point(81, 509);
            button8.Name = "button8";
            button8.Size = new Size(74, 74);
            button8.TabIndex = 30;
            button8.Text = "Quitar Elemento";
            button8.TextImageRelation = TextImageRelation.ImageAboveText;
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // button2
            // 
            button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 192);
            button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = Color.White;
            button2.Location = new Point(332, 495);
            button2.Name = "button2";
            button2.Size = new Size(74, 88);
            button2.TabIndex = 12;
            button2.Text = "Actualizar Detalle";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(8, 40);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(159, 98);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            groupBox1.Text = "Buscar Por:";
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Codigo", "Descripcion", "Marca", "Codigo_Barra" });
            comboBox2.Location = new Point(7, 22);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(146, 23);
            comboBox2.TabIndex = 14;
            // 
            // textBox1
            // 
            textBox1.ForeColor = Color.FromArgb(192, 0, 192);
            textBox1.Location = new Point(7, 51);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Quiero encontrar...";
            textBox1.Size = new Size(146, 23);
            textBox1.TabIndex = 13;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(10, 490);
            label4.Name = "label4";
            label4.Size = new Size(107, 15);
            label4.TabIndex = 29;
            label4.Text = "Productos en Lista:";
            // 
            // button3
            // 
            button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            button3.FlatStyle = FlatStyle.Flat;
            button3.ForeColor = Color.White;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.Location = new Point(174, 48);
            button3.Name = "button3";
            button3.Size = new Size(75, 90);
            button3.TabIndex = 17;
            button3.Text = "Nuevo Producto";
            button3.TextAlign = ContentAlignment.BottomCenter;
            button3.TextImageRelation = TextImageRelation.ImageAboveText;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button5
            // 
            button5.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            button5.FlatStyle = FlatStyle.Flat;
            button5.ForeColor = Color.White;
            button5.Image = (Image)resources.GetObject("button5.Image");
            button5.Location = new Point(331, 48);
            button5.Name = "button5";
            button5.Size = new Size(75, 90);
            button5.TabIndex = 19;
            button5.Text = "Importar Orden";
            button5.TextImageRelation = TextImageRelation.ImageAboveText;
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            button4.FlatStyle = FlatStyle.Flat;
            button4.ForeColor = Color.White;
            button4.Image = (Image)resources.GetObject("button4.Image");
            button4.Location = new Point(253, 48);
            button4.Name = "button4";
            button4.Size = new Size(75, 90);
            button4.TabIndex = 18;
            button4.Text = "Gestionar Proveedor";
            button4.TextImageRelation = TextImageRelation.ImageAboveText;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // V_Factura_Compra
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 590);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "V_Factura_Compra";
            Text = "V_Factura_Compra";
            WindowState = FormWindowState.Maximized;
            Load += V_Factura_Compra_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox2;
        private CheckBox checkBox1;
        private Label label14;
        private Label label13;
        private Label label12;
        private TextBox txt_Descuento;
        private TextBox txt_Impuesto;
        private TextBox txt_OtrosCargos;
        private Label lbl_Descuento;
        private Label lbl_Impuesto;
        private Label lbl_OtrosCargos;
        private Label lbl_sub_Total;
        private Label lbl_Total;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private ComboBox comboBox1;
        private Label label7;
        private Label label6;
        private TextBox textBox2;
        private LinkLabel linkLabel3;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel1;
        private DataGridView dataGridView1;
        private Button button6;
        private Label label3;
        private Button button1;
        private ComboBox comboCliente;
        private DateTimePicker dateTimePicker1;
        public Label label1;
        private ComboBox comboBox3;
        private Label label2;
        private Button button7;
        private Label label5;
        private Button button8;
        private Button button2;
        private GroupBox groupBox1;
        private ComboBox comboBox2;
        private TextBox textBox1;
        private Label label4;
        private Button button3;
        private Button button4;
        public  FlowLayoutPanel flowLayoutPanel2;
        public  FlowLayoutPanel flowLayoutPanel1;
        public Button button5;
        private Label label15;
        private Label label16;
        private PictureBox pictureBox1;
    }
}