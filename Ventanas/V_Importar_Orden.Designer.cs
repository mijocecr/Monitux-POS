namespace Monitux_POS.Ventanas
{
    partial class V_Importar_Orden
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
            panel1 = new Panel();
            dataGridView1 = new DataGridView();
            label3 = new Label();
            dataGridView2 = new DataGridView();
            button2 = new Button();
            label1 = new Label();
            button1 = new Button();
            comboCliente = new ComboBox();
            label2 = new Label();
            textBox2 = new TextBox();
            label6 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 32, 40);
            panel1.Controls.Add(dataGridView1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(dataGridView2);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(comboCliente);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textBox2);
            panel1.Location = new Point(4, 51);
            panel1.Name = "panel1";
            panel1.Size = new Size(377, 425);
            panel1.TabIndex = 39;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(7, 61);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 10;
            dataGridView1.Size = new Size(363, 138);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellEnter += dataGridView1_CellEnter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(143, 43);
            label3.Name = "label3";
            label3.Size = new Size(77, 15);
            label3.TabIndex = 37;
            label3.Text = "Cotizaciones:";
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Enabled = false;
            dataGridView2.Location = new Point(7, 227);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 10;
            dataGridView2.Size = new Size(363, 150);
            dataGridView2.TabIndex = 1;
            // 
            // button2
            // 
            button2.Location = new Point(8, 388);
            button2.Name = "button2";
            button2.Size = new Size(88, 29);
            button2.TabIndex = 36;
            button2.Text = "Cancelar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(4, 10);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 32;
            label1.Text = "Proveedor:";
            // 
            // button1
            // 
            button1.Location = new Point(231, 388);
            button1.Name = "button1";
            button1.Size = new Size(139, 29);
            button1.TabIndex = 35;
            button1.Text = "Importar Orden";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboCliente
            // 
            comboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCliente.FormattingEnabled = true;
            comboCliente.Location = new Point(69, 7);
            comboCliente.Name = "comboCliente";
            comboCliente.Size = new Size(214, 23);
            comboCliente.TabIndex = 31;
            comboCliente.SelectedIndexChanged += comboCliente_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(163, 208);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 34;
            label2.Text = "Detalle:";
            // 
            // textBox2
            // 
            textBox2.ForeColor = Color.FromArgb(192, 0, 192);
            textBox2.Location = new Point(289, 7);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Telefono...";
            textBox2.Size = new Size(83, 23);
            textBox2.TabIndex = 33;
            // 
            // label6
            // 
            label6.Dock = DockStyle.Top;
            label6.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(0, 0);
            label6.Name = "label6";
            label6.Size = new Size(381, 37);
            label6.TabIndex = 40;
            label6.Text = "Ordenes de Compra";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // V_Importar_Orden
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            ClientSize = new Size(381, 479);
            ControlBox = false;
            Controls.Add(label6);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "V_Importar_Orden";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ordenes";
            Load += V_Importar_Orden_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView dataGridView1;
        private Label label3;
        private DataGridView dataGridView2;
        private Button button2;
        public Label label1;
        private Button button1;
        private ComboBox comboCliente;
        private Label label2;
        private TextBox textBox2;
        private Label label6;
    }
}