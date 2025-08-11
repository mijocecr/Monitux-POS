namespace Monitux_POS.Ventanas
{
    partial class V_Venta_Rapida
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Venta_Rapida));
            panel3 = new Panel();
            comboCliente = new ComboBox();
            checkBox1 = new CheckBox();
            label4 = new Label();
            pictureBox1 = new PictureBox();
            cboCamaras = new ComboBox();
            label9 = new Label();
            label8 = new Label();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            groupBox1 = new GroupBox();
            checkBox2 = new CheckBox();
            lbl_Total = new Label();
            lbl_ISV = new Label();
            lbl_SubTotal = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            button6 = new Button();
            dataGridView1 = new DataGridView();
            button7 = new Button();
            button8 = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panel2 = new Panel();
            pictureBox2 = new PictureBox();
            label7 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(35, 32, 45);
            panel3.Controls.Add(comboCliente);
            panel3.Controls.Add(checkBox1);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(pictureBox1);
            panel3.Controls.Add(cboCamaras);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(comboBox1);
            panel3.Controls.Add(textBox1);
            panel3.Controls.Add(groupBox1);
            panel3.Controls.Add(button6);
            panel3.Controls.Add(dataGridView1);
            panel3.Controls.Add(button7);
            panel3.Controls.Add(button8);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(792, 599);
            panel3.TabIndex = 2;
            panel3.Paint += panel3_Paint;
            panel3.MouseMove += panel3_MouseMove;
            // 
            // comboCliente
            // 
            comboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCliente.FormattingEnabled = true;
            comboCliente.Location = new Point(540, 58);
            comboCliente.Name = "comboCliente";
            comboCliente.Size = new Size(243, 23);
            comboCliente.TabIndex = 45;
            comboCliente.SelectedIndexChanged += comboCliente_SelectedIndexChanged;
            comboCliente.MouseClick += comboCliente_MouseClick;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.ForeColor = Color.White;
            checkBox1.Location = new Point(178, 61);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(66, 19);
            checkBox1.TabIndex = 42;
            checkBox1.Text = "Escaner";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(490, 62);
            label4.Name = "label4";
            label4.Size = new Size(47, 15);
            label4.TabIndex = 44;
            label4.Text = "Cliente:";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(143, 80);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // cboCamaras
            // 
            cboCamaras.FormattingEnabled = true;
            cboCamaras.Location = new Point(248, 58);
            cboCamaras.Name = "cboCamaras";
            cboCamaras.Size = new Size(157, 23);
            cboCamaras.TabIndex = 43;
            cboCamaras.Visible = false;
            cboCamaras.SelectedIndexChanged += cboCamaras_SelectedIndexChanged;
            // 
            // label9
            // 
            label9.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label9.ForeColor = Color.White;
            label9.Location = new Point(12, 86);
            label9.Name = "label9";
            label9.Size = new Size(143, 43);
            label9.TabIndex = 42;
            label9.Text = "Favoritos de nuestros clientes";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.White;
            label8.Location = new Point(388, 510);
            label8.Name = "label8";
            label8.Size = new Size(66, 15);
            label8.TabIndex = 41;
            label8.Text = "Buscar por:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(349, 528);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(143, 23);
            comboBox1.TabIndex = 36;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Fuchsia;
            textBox1.Location = new Point(349, 558);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Quiero Vender...";
            textBox1.Size = new Size(143, 23);
            textBox1.TabIndex = 35;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Controls.Add(lbl_Total);
            groupBox1.Controls.Add(lbl_ISV);
            groupBox1.Controls.Add(lbl_SubTotal);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(511, 494);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(192, 94);
            groupBox1.TabIndex = 34;
            groupBox1.TabStop = false;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(29, 51);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(15, 14);
            checkBox2.TabIndex = 43;
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged_1;
            // 
            // lbl_Total
            // 
            lbl_Total.AutoSize = true;
            lbl_Total.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lbl_Total.ForeColor = Color.Yellow;
            lbl_Total.Location = new Point(83, 69);
            lbl_Total.Name = "lbl_Total";
            lbl_Total.Size = new Size(19, 21);
            lbl_Total.TabIndex = 5;
            lbl_Total.Text = "0";
            // 
            // lbl_ISV
            // 
            lbl_ISV.AutoSize = true;
            lbl_ISV.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lbl_ISV.ForeColor = Color.Yellow;
            lbl_ISV.Location = new Point(83, 43);
            lbl_ISV.Name = "lbl_ISV";
            lbl_ISV.Size = new Size(19, 21);
            lbl_ISV.TabIndex = 4;
            lbl_ISV.Text = "0";
            // 
            // lbl_SubTotal
            // 
            lbl_SubTotal.AutoSize = true;
            lbl_SubTotal.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lbl_SubTotal.ForeColor = Color.Yellow;
            lbl_SubTotal.Location = new Point(83, 17);
            lbl_SubTotal.Name = "lbl_SubTotal";
            lbl_SubTotal.Size = new Size(19, 21);
            lbl_SubTotal.TabIndex = 3;
            lbl_SubTotal.Text = "0";
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(5, 71);
            label3.Name = "label3";
            label3.Size = new Size(72, 17);
            label3.TabIndex = 2;
            label3.Text = "Total:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(5, 46);
            label2.Name = "label2";
            label2.Size = new Size(72, 17);
            label2.TabIndex = 1;
            label2.Text = "ISV:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(5, 17);
            label1.Name = "label1";
            label1.Size = new Size(72, 23);
            label1.TabIndex = 0;
            label1.Text = "Sub Total:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(35, 32, 45);
            button6.ForeColor = Color.Lime;
            button6.Location = new Point(709, 498);
            button6.Name = "button6";
            button6.Size = new Size(75, 90);
            button6.TabIndex = 33;
            button6.Text = "Generar Venta";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Info;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.Gainsboro;
            dataGridViewCellStyle2.SelectionForeColor = Color.Blue;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(255, 224, 192);
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.Highlight;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.Location = new Point(175, 86);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.Black;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.Size = new Size(608, 402);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellMouseMove += dataGridView1_CellMouseMove;
            dataGridView1.MouseHover += dataGridView1_MouseHover;
            dataGridView1.MouseMove += dataGridView1_MouseMove;
            // 
            // button7
            // 
            button7.BackColor = Color.FromArgb(35, 32, 45);
            button7.FlatAppearance.BorderColor = Color.FromArgb(252, 114, 95);
            button7.FlatAppearance.MouseDownBackColor = Color.FromArgb(252, 114, 95);
            button7.FlatStyle = FlatStyle.Flat;
            button7.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button7.ForeColor = Color.White;
            button7.Image = (Image)resources.GetObject("button7.Image");
            button7.Location = new Point(256, 498);
            button7.Name = "button7";
            button7.Size = new Size(74, 90);
            button7.TabIndex = 31;
            button7.Text = "Reset Factura";
            button7.TextImageRelation = TextImageRelation.ImageAboveText;
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.BackColor = Color.FromArgb(35, 32, 45);
            button8.FlatStyle = FlatStyle.Flat;
            button8.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button8.ForeColor = Color.White;
            button8.Image = (Image)resources.GetObject("button8.Image");
            button8.Location = new Point(176, 498);
            button8.Name = "button8";
            button8.Size = new Size(74, 90);
            button8.TabIndex = 32;
            button8.Text = "Quitar Elemento";
            button8.TextImageRelation = TextImageRelation.ImageAboveText;
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = Color.FromArgb(73, 147, 40);
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            flowLayoutPanel1.ForeColor = Color.Black;
            flowLayoutPanel1.Location = new Point(0, 131);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(161, 465);
            flowLayoutPanel1.TabIndex = 34;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(35, 32, 45);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(label7);
            panel2.ForeColor = Color.Black;
            panel2.Location = new Point(177, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(606, 53);
            panel2.TabIndex = 1;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(559, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(48, 43);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 51;
            pictureBox2.TabStop = false;
            pictureBox2.Visible = false;
            // 
            // label7
            // 
            label7.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.Location = new Point(149, 3);
            label7.Name = "label7";
            label7.Size = new Size(178, 37);
            label7.TabIndex = 40;
            label7.Text = "Venta Rapida";
            label7.TextAlign = ContentAlignment.TopCenter;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // V_Venta_Rapida
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(792, 599);
            ControlBox = false;
            Controls.Add(panel2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(panel3);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "V_Venta_Rapida";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "V_Venta_Rapida";
            FormClosing += V_Venta_Rapida_FormClosing;
            Load += V_Venta_Rapida_Load;
            Shown += V_Venta_Rapida_Shown;
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel3;
        private DataGridView dataGridView1;
        private Button button7;
        private Button button8;
        private Button button6;
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel panel2;
        private Label label3;
        private Label label2;
        private Label lbl_ISV;
        private Label lbl_SubTotal;
        private GroupBox groupBox1;
        private Label label7;
        private TextBox textBox1;
        private ComboBox comboBox1;
        private Label label8;
        private PictureBox pictureBox1;
        private CheckBox checkBox1;
        private Label label9;
        private System.Windows.Forms.Timer timer1;
        private Label lbl_Total;
        private CheckBox checkBox2;
        private Label label1;
        private ComboBox cboCamaras;
        private ComboBox comboCliente;
        private Label label4;
        private PictureBox pictureBox2;
    }
}