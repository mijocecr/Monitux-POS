namespace Monitux_POS.Ventanas
{
    partial class V_CTA_Cliente
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
            button1 = new Button();
            label6 = new Label();
            label1 = new Label();
            panel2 = new Panel();
            label3 = new Label();
            label2 = new Label();
            panel4 = new Panel();
            label5 = new Label();
            panel3 = new Panel();
            label4 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 32, 40);
            panel1.Controls.Add(dataGridView1);
            panel1.Location = new Point(12, 82);
            panel1.Name = "panel1";
            panel1.Size = new Size(445, 262);
            panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(4, 10);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(437, 239);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // button1
            // 
            button1.BackColor = Color.Lime;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(333, 52);
            button1.Name = "button1";
            button1.Size = new Size(124, 24);
            button1.TabIndex = 39;
            button1.Text = "Registrar Abono";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label6
            // 
            label6.Dock = DockStyle.Top;
            label6.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(0, 0);
            label6.Name = "label6";
            label6.Size = new Size(469, 37);
            label6.TabIndex = 37;
            label6.Text = "Estado de Cuenta";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 57);
            label1.Name = "label1";
            label1.Size = new Size(141, 21);
            label1.TabIndex = 38;
            label1.Text = "Nombre de Cliente";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(35, 32, 40);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Location = new Point(12, 353);
            panel2.Name = "panel2";
            panel2.Size = new Size(445, 83);
            panel2.TabIndex = 40;
            // 
            // label3
            // 
            label3.ForeColor = Color.White;
            label3.Location = new Point(233, 9);
            label3.Name = "label3";
            label3.Size = new Size(196, 20);
            label3.TabIndex = 43;
            label3.Text = "Total en Facturas Credito";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.ForeColor = Color.White;
            label2.Location = new Point(10, 9);
            label2.Name = "label2";
            label2.Size = new Size(192, 20);
            label2.TabIndex = 39;
            label2.Text = "Saldo Actual";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(11, 8, 20);
            panel4.Controls.Add(label5);
            panel4.Location = new Point(233, 29);
            panel4.Name = "panel4";
            panel4.Size = new Size(199, 43);
            panel4.TabIndex = 42;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(0, 192, 192);
            label5.Location = new Point(3, 0);
            label5.Name = "label5";
            label5.Size = new Size(196, 43);
            label5.TabIndex = 41;
            label5.Text = "Total";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(11, 8, 20);
            panel3.Controls.Add(label4);
            panel3.Location = new Point(7, 29);
            panel3.Name = "panel3";
            panel3.Size = new Size(195, 43);
            panel3.TabIndex = 41;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Red;
            label4.Location = new Point(3, 0);
            label4.Name = "label4";
            label4.Size = new Size(189, 43);
            label4.TabIndex = 40;
            label4.Text = "Saldo Actual";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // V_CTA_Cliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            ClientSize = new Size(469, 448);
            Controls.Add(panel2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(label6);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "V_CTA_Cliente";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "V_CTA_Cliente";
            Load += V_CTA_Cliente_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private DataGridView dataGridView1;
        private Label label6;
        private Label label1;
        private Button button1;
        private Panel panel2;
        private Label label2;
        private Panel panel4;
        private Panel panel3;
        private Label label3;
        private Label label5;
        private Label label4;
    }
}