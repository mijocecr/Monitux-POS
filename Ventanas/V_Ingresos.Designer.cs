namespace Monitux_POS.Ventanas
{
    partial class V_Ingresos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Ingresos));
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            label15 = new Label();
            panel1 = new Panel();
            button4 = new Button();
            button2 = new Button();
            button3 = new Button();
            label10 = new Label();
            label11 = new Label();
            label8 = new Label();
            label7 = new Label();
            label4 = new Label();
            pictureBox1 = new PictureBox();
            groupBox1 = new GroupBox();
            label2 = new Label();
            button1 = new Button();
            fecha_inicio = new DateTimePicker();
            label5 = new Label();
            fecha_fin = new DateTimePicker();
            label9 = new Label();
            dataGridView1 = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label15
            // 
            label15.Dock = DockStyle.Top;
            label15.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.ForeColor = Color.Lime;
            label15.Location = new Point(0, 0);
            label15.Name = "label15";
            label15.Size = new Size(802, 37);
            label15.TabIndex = 40;
            label15.Text = "Gestión de Ingresos";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            label15.Click += label15_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 32, 45);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(dataGridView1);
            panel1.Location = new Point(13, 46);
            panel1.Name = "panel1";
            panel1.Size = new Size(778, 548);
            panel1.TabIndex = 41;
            // 
            // button4
            // 
            button4.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            button4.FlatStyle = FlatStyle.Flat;
            button4.ForeColor = Color.White;
            button4.Image = (Image)resources.GetObject("button4.Image");
            button4.Location = new Point(170, 6);
            button4.Name = "button4";
            button4.Size = new Size(75, 78);
            button4.TabIndex = 87;
            button4.Text = "Exportar a Excel";
            button4.TextAlign = ContentAlignment.BottomCenter;
            button4.TextImageRelation = TextImageRelation.ImageAboveText;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button2
            // 
            button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = Color.White;
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.Location = new Point(88, 6);
            button2.Name = "button2";
            button2.Size = new Size(75, 78);
            button2.TabIndex = 86;
            button2.Text = "Eliminar Ingreso";
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
            button3.Location = new Point(7, 6);
            button3.Name = "button3";
            button3.Size = new Size(75, 78);
            button3.TabIndex = 85;
            button3.Text = "Nuevo Ingreso";
            button3.TextAlign = ContentAlignment.BottomCenter;
            button3.TextImageRelation = TextImageRelation.ImageAboveText;
            button3.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            label10.ForeColor = Color.Yellow;
            label10.Location = new Point(572, 471);
            label10.Name = "label10";
            label10.Size = new Size(23, 25);
            label10.TabIndex = 84;
            label10.Text = "0";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.White;
            label11.Location = new Point(431, 471);
            label11.Name = "label11";
            label11.Size = new Size(138, 25);
            label11.TabIndex = 83;
            label11.Text = "Otros Ingresos:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            label8.ForeColor = Color.Lime;
            label8.Location = new Point(154, 471);
            label8.Name = "label8";
            label8.Size = new Size(23, 25);
            label8.TabIndex = 82;
            label8.Text = "0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.Location = new Point(7, 471);
            label7.Name = "label7";
            label7.Size = new Size(150, 25);
            label7.TabIndex = 81;
            label7.Text = "Ingresos Totales:";
            // 
            // label4
            // 
            label4.ForeColor = Color.White;
            label4.Location = new Point(2, 508);
            label4.Name = "label4";
            label4.Size = new Size(708, 35);
            label4.TabIndex = 80;
            label4.Text = "No se puede eliminar ningun ingreso asociado a una factura o a un abono de cuentas por cobrar. La funcion \"Eliminar Ingreso\" solo puede aplicarse a registros manuales de ingresos.";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(727, 490);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 36);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 79;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(fecha_inicio);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(fecha_fin);
            groupBox1.Controls.Add(label9);
            groupBox1.Location = new Point(323, 7);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(447, 78);
            groupBox1.TabIndex = 73;
            groupBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Yellow;
            label2.Location = new Point(53, 56);
            label2.Name = "label2";
            label2.Size = new Size(223, 15);
            label2.TabIndex = 72;
            label2.Text = "Todas los ingresos en un rango de fechas";
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
            // fecha_inicio
            // 
            fecha_inicio.Format = DateTimePickerFormat.Short;
            fecha_inicio.Location = new Point(54, 22);
            fecha_inicio.Name = "fecha_inicio";
            fecha_inicio.Size = new Size(100, 23);
            fecha_inicio.TabIndex = 65;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(6, 25);
            label5.Name = "label5";
            label5.Size = new Size(42, 15);
            label5.TabIndex = 66;
            label5.Text = "Desde:";
            // 
            // fecha_fin
            // 
            fecha_fin.Format = DateTimePickerFormat.Short;
            fecha_fin.Location = new Point(207, 22);
            fecha_fin.Name = "fecha_fin";
            fecha_fin.Size = new Size(100, 23);
            fecha_fin.TabIndex = 68;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(161, 26);
            label9.Name = "label9";
            label9.Size = new Size(40, 15);
            label9.TabIndex = 67;
            label9.Text = "Hasta:";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Cursor = Cursors.Hand;
            dataGridView1.Location = new Point(7, 94);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(763, 373);
            dataGridView1.TabIndex = 62;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellEnter += dataGridView1_CellEnter;
            // 
            // V_Ingresos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            ClientSize = new Size(802, 599);
            Controls.Add(panel1);
            Controls.Add(label15);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "V_Ingresos";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "V_Ingresos";
            Load += V_Ingresos_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label15;
        private Panel panel1;
        private GroupBox groupBox1;
        private Label label2;
        private Button button1;
        private DateTimePicker fecha_inicio;
        private Label label5;
        private DateTimePicker fecha_fin;
        private Label label9;
        private DataGridView dataGridView1;
        private Label label10;
        private Label label11;
        private Label label8;
        private Label label7;
        private Label label4;
        private PictureBox pictureBox1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}