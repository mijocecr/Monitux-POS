namespace Monitux_POS.Ventanas
{
    partial class V_CTAS_Cobrar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_CTAS_Cobrar));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            label1 = new Label();
            comboCliente = new ComboBox();
            fecha_fin = new DateTimePicker();
            label9 = new Label();
            label5 = new Label();
            fecha_inicio = new DateTimePicker();
            label6 = new Label();
            dataGridView1 = new DataGridView();
            linkLabel2 = new LinkLabel();
            groupBox1 = new GroupBox();
            label2 = new Label();
            groupBox2 = new GroupBox();
            label3 = new Label();
            label4 = new Label();
            label7 = new Label();
            label8 = new Label();
            label10 = new Label();
            label11 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(757, 544);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 36);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 64;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(128, 255, 128);
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.Black;
            button1.Location = new Point(333, 15);
            button1.Name = "button1";
            button1.Size = new Size(103, 51);
            button1.TabIndex = 71;
            button1.Text = "Consultar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(18, 25);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 70;
            label1.Text = "Cliente:";
            // 
            // comboCliente
            // 
            comboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCliente.FormattingEnabled = true;
            comboCliente.Location = new Point(74, 22);
            comboCliente.Name = "comboCliente";
            comboCliente.Size = new Size(225, 23);
            comboCliente.TabIndex = 69;
            comboCliente.SelectedIndexChanged += comboCliente_SelectedIndexChanged;
            // 
            // fecha_fin
            // 
            fecha_fin.Format = DateTimePickerFormat.Short;
            fecha_fin.Location = new Point(207, 16);
            fecha_fin.Name = "fecha_fin";
            fecha_fin.Size = new Size(100, 23);
            fecha_fin.TabIndex = 68;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(161, 20);
            label9.Name = "label9";
            label9.Size = new Size(40, 15);
            label9.TabIndex = 67;
            label9.Text = "Hasta:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(6, 19);
            label5.Name = "label5";
            label5.Size = new Size(42, 15);
            label5.TabIndex = 66;
            label5.Text = "Desde:";
            // 
            // fecha_inicio
            // 
            fecha_inicio.Format = DateTimePickerFormat.Short;
            fecha_inicio.Location = new Point(54, 16);
            fecha_inicio.Name = "fecha_inicio";
            fecha_inicio.Size = new Size(100, 23);
            fecha_inicio.TabIndex = 65;
            // 
            // label6
            // 
            label6.BackColor = Color.FromArgb(11, 8, 20);
            label6.Dock = DockStyle.Top;
            label6.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Lime;
            label6.Location = new Point(0, 0);
            label6.Name = "label6";
            label6.Size = new Size(798, 37);
            label6.TabIndex = 62;
            label6.Text = "Cuentas a Cobrar";
            label6.TextAlign = ContentAlignment.TopCenter;
            label6.Click += label6_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Cursor = Cursors.Hand;
            dataGridView1.Location = new Point(7, 121);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(779, 394);
            dataGridView1.TabIndex = 61;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // linkLabel2
            // 
            linkLabel2.BackColor = Color.FromArgb(35, 32, 45);
            linkLabel2.LinkBehavior = LinkBehavior.HoverUnderline;
            linkLabel2.LinkColor = Color.Red;
            linkLabel2.Location = new Point(624, 557);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(111, 23);
            linkLabel2.TabIndex = 65;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Cuentas Vencidas";
            linkLabel2.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(fecha_inicio);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(fecha_fin);
            groupBox1.Controls.Add(label9);
            groupBox1.Location = new Point(331, 40);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(455, 75);
            groupBox1.TabIndex = 72;
            groupBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Yellow;
            label2.Location = new Point(45, 51);
            label2.Name = "label2";
            label2.Size = new Size(265, 15);
            label2.TabIndex = 72;
            label2.Text = "Todas las cuentas a cobrar en un rango de fechas";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(comboCliente);
            groupBox2.Location = new Point(7, 40);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(318, 75);
            groupBox2.TabIndex = 73;
            groupBox2.TabStop = false;
            groupBox2.Enter += groupBox2_Enter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Yellow;
            label3.Location = new Point(20, 51);
            label3.Name = "label3";
            label3.Size = new Size(282, 15);
            label3.TabIndex = 73;
            label3.Text = "Todas las cuentas a cobrar para un cliente especifico";
            // 
            // label4
            // 
            label4.ForeColor = Color.White;
            label4.Location = new Point(7, 557);
            label4.Name = "label4";
            label4.Size = new Size(577, 35);
            label4.TabIndex = 74;
            label4.Text = "Se muestran solo las cuentas con saldo pendiente. En el archivo de cada cliente podra ver todas las cuentas asociadas al mismo. Indistintamente del saldo.";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.Location = new Point(12, 521);
            label7.Name = "label7";
            label7.Size = new Size(134, 25);
            label7.TabIndex = 75;
            label7.Text = "Total a Cobrar:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            label8.ForeColor = Color.Red;
            label8.Location = new Point(147, 521);
            label8.Name = "label8";
            label8.Size = new Size(23, 25);
            label8.TabIndex = 76;
            label8.Text = "0";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            label10.ForeColor = Color.Yellow;
            label10.Location = new Point(577, 521);
            label10.Name = "label10";
            label10.Size = new Size(23, 25);
            label10.TabIndex = 78;
            label10.Text = "0";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.White;
            label11.Location = new Point(360, 521);
            label11.Name = "label11";
            label11.Size = new Size(213, 25);
            label11.TabIndex = 77;
            label11.Text = "Total Vendido a Credito:";
            // 
            // V_CTAS_Cobrar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 32, 40);
            ClientSize = new Size(798, 601);
            Controls.Add(label10);
            Controls.Add(label11);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label4);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(linkLabel2);
            Controls.Add(pictureBox1);
            Controls.Add(label6);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "V_CTAS_Cobrar";
            Text = "V_CTAS_Cobrar";
            Load += V_CTAS_Cobrar_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button button1;
        private Label label1;
        private ComboBox comboCliente;
        private DateTimePicker fecha_fin;
        private Label label9;
        private Label label5;
        private DateTimePicker fecha_inicio;
        private Label label6;
        private DataGridView dataGridView1;
        private LinkLabel linkLabel2;
        private GroupBox groupBox1;
        private Label label2;
        private GroupBox groupBox2;
        private Label label3;
        private Label label4;
        private Label label7;
        private Label label8;
        private Label label10;
        private Label label11;
    }
}