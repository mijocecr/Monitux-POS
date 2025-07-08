namespace Monitux_POS.Ventanas
{
    partial class V_Abono_Proveedor
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
            panel1 = new Panel();
            label11 = new Label();
            textBox1 = new TextBox();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            button2 = new Button();
            button1 = new Button();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            label6 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 32, 45);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(8, 37);
            panel1.Name = "panel1";
            panel1.Size = new Size(166, 200);
            panel1.TabIndex = 8;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.White;
            label11.Location = new Point(87, 175);
            label11.Name = "label11";
            label11.Size = new Size(17, 20);
            label11.TabIndex = 12;
            label11.Text = "0";
            // 
            // textBox1
            // 
            textBox1.ForeColor = Color.Fuchsia;
            textBox1.Location = new Point(59, 52);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(86, 23);
            textBox1.TabIndex = 1;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9.75F);
            label10.ForeColor = Color.Red;
            label10.Location = new Point(79, 78);
            label10.Name = "label10";
            label10.Size = new Size(15, 17);
            label10.TabIndex = 11;
            label10.Text = "0";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9.75F);
            label9.ForeColor = Color.FromArgb(0, 192, 0);
            label9.Location = new Point(79, 31);
            label9.Name = "label9";
            label9.Size = new Size(15, 17);
            label9.TabIndex = 10;
            label9.Text = "0";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.75F);
            label8.ForeColor = Color.FromArgb(0, 192, 192);
            label8.Location = new Point(79, 12);
            label8.Name = "label8";
            label8.Size = new Size(15, 17);
            label8.TabIndex = 9;
            label8.Text = "0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(12, 178);
            label7.Name = "label7";
            label7.Size = new Size(71, 15);
            label7.TabIndex = 8;
            label7.Text = "No. Factura:";
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(128, 255, 255);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(6, 142);
            button2.Name = "button2";
            button2.Size = new Size(151, 30);
            button2.TabIndex = 6;
            button2.Text = "Registrar Pago";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.FromArgb(128, 255, 255);
            button1.Location = new Point(6, 104);
            button1.Name = "button1";
            button1.Size = new Size(151, 30);
            button1.TabIndex = 1;
            button1.Text = "Ver Abonos";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(12, 78);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 4;
            label4.Text = "Saldo:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(12, 55);
            label3.Name = "label3";
            label3.Size = new Size(40, 15);
            label3.TabIndex = 3;
            label3.Text = "Pagar:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(12, 31);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 2;
            label2.Text = "Pagado:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 1;
            label1.Text = "Total:";
            // 
            // label6
            // 
            label6.ForeColor = Color.White;
            label6.Location = new Point(8, 6);
            label6.Name = "label6";
            label6.Size = new Size(166, 31);
            label6.TabIndex = 9;
            label6.Text = "Nombre de Proveedor";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // V_Abono_Proveedor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            ClientSize = new Size(182, 243);
            Controls.Add(panel1);
            Controls.Add(label6);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "V_Abono_Proveedor";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "V_Abono_Proveedor";
            Load += V_Abono_Proveedor_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label11;
        private TextBox textBox1;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Button button2;
        private Button button1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label6;
    }
}