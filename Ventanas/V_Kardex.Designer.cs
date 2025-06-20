namespace Monitux_POS.Ventanas
{
    partial class V_Kardex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Kardex));
            label6 = new Label();
            panel1 = new Panel();
            label5 = new Label();
            label7 = new Label();
            label4 = new Label();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            label3 = new Label();
            dataGridView2 = new DataGridView();
            button2 = new Button();
            label2 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // label6
            // 
            label6.Dock = DockStyle.Top;
            label6.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(0, 0);
            label6.Name = "label6";
            label6.Size = new Size(432, 37);
            label6.TabIndex = 41;
            label6.Text = "Codigo";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 32, 40);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(dataGridView1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(dataGridView2);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(5, 43);
            panel1.Name = "panel1";
            panel1.Size = new Size(421, 399);
            panel1.TabIndex = 40;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Red;
            label5.Location = new Point(350, 368);
            label5.Name = "label5";
            label5.Size = new Size(63, 15);
            label5.TabIndex = 41;
            label5.Text = "0";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            label7.ForeColor = Color.White;
            label7.Image = (Image)resources.GetObject("label7.Image");
            label7.Location = new Point(301, 356);
            label7.Name = "label7";
            label7.Size = new Size(46, 34);
            label7.TabIndex = 40;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Lime;
            label4.Location = new Point(189, 368);
            label4.Name = "label4";
            label4.Size = new Size(64, 15);
            label4.TabIndex = 39;
            label4.Text = "0";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.ForeColor = Color.White;
            label1.Image = (Image)resources.GetObject("label1.Image");
            label1.Location = new Point(139, 356);
            label1.Name = "label1";
            label1.Size = new Size(44, 34);
            label1.TabIndex = 38;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(7, 42);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 10;
            dataGridView1.Size = new Size(406, 138);
            dataGridView1.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(198, 24);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 37;
            label3.Text = "Entradas:";
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Enabled = false;
            dataGridView2.Location = new Point(7, 208);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 10;
            dataGridView2.Size = new Size(406, 138);
            dataGridView2.TabIndex = 1;
            // 
            // button2
            // 
            button2.Location = new Point(7, 361);
            button2.Name = "button2";
            button2.Size = new Size(88, 29);
            button2.TabIndex = 36;
            button2.Text = "Cerrar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(198, 190);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 34;
            label2.Text = "Salidas:";
            // 
            // V_Kardex
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            ClientSize = new Size(432, 453);
            ControlBox = false;
            Controls.Add(label6);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "V_Kardex";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kardex";
            Load += Kardex_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label6;
        private Panel panel1;
        private DataGridView dataGridView1;
        private Label label3;
        private DataGridView dataGridView2;
        private Button button2;
        private Label label2;
        private Label label1;
        private Label label5;
        private Label label7;
        private Label label4;
    }
}