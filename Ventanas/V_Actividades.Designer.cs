namespace Monitux_POS.Ventanas
{
    partial class V_Actividades
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Actividades));
            dataGridView1 = new DataGridView();
            label6 = new Label();
            panel1 = new Panel();
            button1 = new Button();
            label1 = new Label();
            comboBox1 = new ComboBox();
            fecha_fin = new DateTimePicker();
            label9 = new Label();
            label5 = new Label();
            fecha_inicio = new DateTimePicker();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(4, 81);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(785, 467);
            dataGridView1.TabIndex = 54;
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
            label6.TabIndex = 55;
            label6.Text = "Bitácora de Actividades";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            panel1.Controls.Add(button1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(fecha_fin);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(fecha_inicio);
            panel1.Location = new Point(0, 40);
            panel1.Name = "panel1";
            panel1.Size = new Size(795, 35);
            panel1.TabIndex = 56;
            panel1.Paint += panel1_Paint;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(128, 255, 128);
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.Black;
            button1.Location = new Point(680, 6);
            button1.Name = "button1";
            button1.Size = new Size(103, 23);
            button1.TabIndex = 71;
            button1.Text = "Consultar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(7, 9);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 70;
            label1.Text = "Usuario:";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(63, 6);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(225, 23);
            comboBox1.TabIndex = 69;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // fecha_fin
            // 
            fecha_fin.Format = DateTimePickerFormat.Short;
            fecha_fin.Location = new Point(536, 6);
            fecha_fin.Name = "fecha_fin";
            fecha_fin.Size = new Size(100, 23);
            fecha_fin.TabIndex = 68;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(490, 10);
            label9.Name = "label9";
            label9.Size = new Size(40, 15);
            label9.TabIndex = 67;
            label9.Text = "Hasta:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(328, 9);
            label5.Name = "label5";
            label5.Size = new Size(42, 15);
            label5.TabIndex = 66;
            label5.Text = "Desde:";
            // 
            // fecha_inicio
            // 
            fecha_inicio.Format = DateTimePickerFormat.Short;
            fecha_inicio.Location = new Point(376, 6);
            fecha_inicio.Name = "fecha_inicio";
            fecha_inicio.Size = new Size(100, 23);
            fecha_inicio.TabIndex = 65;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(381, 554);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 36);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 60;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // V_Actividades
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(44, 117, 255);
            ClientSize = new Size(795, 591);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Controls.Add(label6);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "V_Actividades";
            Text = "V_Actividades";
            Load += V_Actividades_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label6;
        private Panel panel1;
        private DateTimePicker fecha_fin;
        private Label label9;
        private Label label5;
        private DateTimePicker fecha_inicio;
        private Button button1;
        private Label label1;
        private ComboBox comboBox1;
        private PictureBox pictureBox1;
    }
}