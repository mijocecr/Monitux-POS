namespace Monitux_POS.Ventanas
{
    partial class V_Compras_Ventas
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            panel3 = new Panel();
            panel2 = new Panel();
            button5 = new Button();
            button6 = new Button();
            dataGridView3 = new DataGridView();
            label4 = new Label();
            dataGridView4 = new DataGridView();
            label5 = new Label();
            comboProveedor = new ComboBox();
            label7 = new Label();
            textBox1 = new TextBox();
            label8 = new Label();
            panel1 = new Panel();
            button4 = new Button();
            button3 = new Button();
            dataGridView1 = new DataGridView();
            label3 = new Label();
            dataGridView2 = new DataGridView();
            label1 = new Label();
            button1 = new Button();
            comboCliente = new ComboBox();
            label2 = new Label();
            textBox2 = new TextBox();
            label6 = new Label();
            label9 = new Label();
            label15 = new Label();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(35, 32, 45);
            panel3.Controls.Add(panel2);
            panel3.Controls.Add(panel1);
            panel3.Location = new Point(12, 48);
            panel3.Name = "panel3";
            panel3.Size = new Size(776, 542);
            panel3.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(0, 75, 160);
            panel2.Controls.Add(button5);
            panel2.Controls.Add(button6);
            panel2.Controls.Add(dataGridView3);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(dataGridView4);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(comboProveedor);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label8);
            panel2.Location = new Point(391, 6);
            panel2.Name = "panel2";
            panel2.Size = new Size(380, 500);
            panel2.TabIndex = 2;
            // 
            // button5
            // 
            button5.FlatStyle = FlatStyle.Flat;
            button5.ForeColor = Color.White;
            button5.Location = new Point(207, 465);
            button5.Name = "button5";
            button5.Size = new Size(111, 29);
            button5.TabIndex = 52;
            button5.Text = "Imprimir";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(35, 32, 45);
            button6.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 0);
            button6.FlatStyle = FlatStyle.Flat;
            button6.ForeColor = Color.Yellow;
            button6.Location = new Point(71, 466);
            button6.Name = "button6";
            button6.Size = new Size(111, 29);
            button6.TabIndex = 51;
            button6.Text = "Modificar";
            button6.UseVisualStyleBackColor = false;
            // 
            // dataGridView3
            // 
            dataGridView3.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView3.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Location = new Point(10, 96);
            dataGridView3.MultiSelect = false;
            dataGridView3.Name = "dataGridView3";
            dataGridView3.RowHeadersWidth = 10;
            dataGridView3.Size = new Size(363, 138);
            dataGridView3.TabIndex = 40;
            dataGridView3.CellContentClick += dataGridView3_CellContentClick;
            dataGridView3.CellEnter += dataGridView3_CellEnter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(156, 78);
            label4.Name = "label4";
            label4.Size = new Size(54, 15);
            label4.TabIndex = 47;
            label4.Text = "Facturas:";
            // 
            // dataGridView4
            // 
            dataGridView4.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView4.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView4.Enabled = false;
            dataGridView4.Location = new Point(10, 262);
            dataGridView4.Name = "dataGridView4";
            dataGridView4.RowHeadersWidth = 10;
            dataGridView4.Size = new Size(363, 197);
            dataGridView4.TabIndex = 41;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.White;
            label5.Location = new Point(4, 49);
            label5.Name = "label5";
            label5.Size = new Size(64, 15);
            label5.TabIndex = 43;
            label5.Text = "Proveedor:";
            // 
            // comboProveedor
            // 
            comboProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            comboProveedor.FormattingEnabled = true;
            comboProveedor.Location = new Point(71, 46);
            comboProveedor.Name = "comboProveedor";
            comboProveedor.Size = new Size(213, 23);
            comboProveedor.TabIndex = 42;
            comboProveedor.SelectedIndexChanged += comboProveedor_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(164, 243);
            label7.Name = "label7";
            label7.Size = new Size(46, 15);
            label7.TabIndex = 45;
            label7.Text = "Detalle:";
            // 
            // textBox1
            // 
            textBox1.ForeColor = Color.FromArgb(192, 0, 192);
            textBox1.Location = new Point(290, 46);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Telefono...";
            textBox1.Size = new Size(83, 23);
            textBox1.TabIndex = 44;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label8
            // 
            label8.Dock = DockStyle.Top;
            label8.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.White;
            label8.Location = new Point(0, 0);
            label8.Name = "label8";
            label8.Size = new Size(380, 37);
            label8.TabIndex = 48;
            label8.Text = "Compras";
            label8.TextAlign = ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 168, 107);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(dataGridView1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(dataGridView2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(comboCliente);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(label6);
            panel1.Location = new Point(5, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(381, 500);
            panel1.TabIndex = 1;
            panel1.Paint += panel1_Paint;
            // 
            // button4
            // 
            button4.FlatStyle = FlatStyle.Flat;
            button4.ForeColor = Color.White;
            button4.Location = new Point(259, 466);
            button4.Name = "button4";
            button4.Size = new Size(111, 29);
            button4.TabIndex = 50;
            button4.Text = "Enviar";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.FlatStyle = FlatStyle.Flat;
            button3.ForeColor = Color.White;
            button3.Location = new Point(132, 466);
            button3.Name = "button3";
            button3.Size = new Size(111, 29);
            button3.TabIndex = 49;
            button3.Text = "Imprimir";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(7, 97);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 10;
            dataGridView1.Size = new Size(363, 138);
            dataGridView1.TabIndex = 40;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellEnter += dataGridView1_CellEnter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(166, 79);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 47;
            label3.Text = "Facturas:";
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Enabled = false;
            dataGridView2.Location = new Point(7, 263);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 10;
            dataGridView2.Size = new Size(363, 197);
            dataGridView2.TabIndex = 41;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(8, 46);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 43;
            label1.Text = "Cliente:";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(35, 32, 45);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 0);
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.Yellow;
            button1.Location = new Point(8, 466);
            button1.Name = "button1";
            button1.Size = new Size(111, 29);
            button1.TabIndex = 46;
            button1.Text = "Modificar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // comboCliente
            // 
            comboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCliente.FormattingEnabled = true;
            comboCliente.Location = new Point(61, 43);
            comboCliente.Name = "comboCliente";
            comboCliente.Size = new Size(222, 23);
            comboCliente.TabIndex = 42;
            comboCliente.SelectedIndexChanged += comboCliente_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(165, 244);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 45;
            label2.Text = "Detalle:";
            // 
            // textBox2
            // 
            textBox2.ForeColor = Color.FromArgb(192, 0, 192);
            textBox2.Location = new Point(289, 43);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Telefono...";
            textBox2.Size = new Size(83, 23);
            textBox2.TabIndex = 44;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label6
            // 
            label6.Dock = DockStyle.Top;
            label6.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(0, 0);
            label6.Name = "label6";
            label6.Size = new Size(381, 37);
            label6.TabIndex = 48;
            label6.Text = "Ventas";
            label6.TextAlign = ContentAlignment.TopCenter;
            label6.Click += label6_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.FromArgb(35, 32, 45);
            label9.ForeColor = Color.White;
            label9.Location = new Point(51, 566);
            label9.Name = "label9";
            label9.Size = new Size(700, 15);
            label9.TabIndex = 3;
            label9.Text = "🔔 Atención: Modificar una factura es una acción delicada. Los cambios realizados no se pueden deshacer. Proceda con precaución.";
            // 
            // label15
            // 
            label15.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.ForeColor = Color.Yellow;
            label15.Location = new Point(207, 7);
            label15.Name = "label15";
            label15.Size = new Size(379, 37);
            label15.TabIndex = 39;
            label15.Text = "Gestiónar Facturas";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // V_Compras_Ventas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            ClientSize = new Size(798, 594);
            Controls.Add(label9);
            Controls.Add(label15);
            Controls.Add(panel3);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "V_Compras_Ventas";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "V_Compras_Ventas";
            Load += V_Compras_Ventas_Load;
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel3;
        private Panel panel1;
        private Button button4;
        private Button button3;
        private DataGridView dataGridView1;
        private Label label3;
        private DataGridView dataGridView2;
        public Label label1;
        private Button button1;
        private ComboBox comboCliente;
        private Label label2;
        private TextBox textBox2;
        private Label label6;
        private Panel panel2;
        private Button button5;
        private Button button6;
        private DataGridView dataGridView3;
        private Label label4;
        private DataGridView dataGridView4;
        public Label label5;
        private ComboBox comboProveedor;
        private Label label7;
        private TextBox textBox1;
        private Label label8;
        private Label label15;
        private Label label9;
    }
}