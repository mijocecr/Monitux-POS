namespace Monitux_POS.Ventanas
{
    partial class V_Configuracion
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
            label6 = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label7 = new Label();
            groupBox1 = new GroupBox();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label6
            // 
            label6.BackColor = Color.FromArgb(11, 8, 20);
            label6.Dock = DockStyle.Top;
            label6.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(0, 0);
            label6.Name = "label6";
            label6.Size = new Size(800, 37);
            label6.TabIndex = 80;
            label6.Text = "Primeros ajustes del sistema";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(364, 264);
            label1.Name = "label1";
            label1.Size = new Size(239, 15);
            label1.TabIndex = 81;
            label1.Text = "Es necesario crear un usuario administrador.";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(364, 230);
            label2.Name = "label2";
            label2.Size = new Size(174, 15);
            label2.TabIndex = 82;
            label2.Text = "Es necesario crear una empresa.";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(28, 364);
            label3.Name = "label3";
            label3.Size = new Size(441, 15);
            label3.TabIndex = 83;
            label3.Text = "Es opcional importar una base de datos de una instalación de Monitux-POS previa.";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(28, 398);
            label7.Name = "label7";
            label7.Size = new Size(257, 15);
            label7.TabIndex = 86;
            label7.Text = "Es opcional establecer la fuente RSS de noticias.";
            label7.TextAlign = ContentAlignment.TopCenter;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Location = new Point(12, 62);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(333, 250);
            groupBox1.TabIndex = 87;
            groupBox1.TabStop = false;
            groupBox1.Text = "Nueva Empresa";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(16, 32);
            label8.Name = "label8";
            label8.Size = new Size(54, 15);
            label8.TabIndex = 0;
            label8.Text = "Nombre:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(16, 63);
            label9.Name = "label9";
            label9.Size = new Size(60, 15);
            label9.TabIndex = 1;
            label9.Text = "Dirección:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(16, 95);
            label10.Name = "label10";
            label10.Size = new Size(56, 15);
            label10.TabIndex = 2;
            label10.Text = "Telefono:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(16, 128);
            label11.Name = "label11";
            label11.Size = new Size(39, 15);
            label11.TabIndex = 3;
            label11.Text = "Email:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(16, 156);
            label12.Name = "label12";
            label12.Size = new Size(96, 15);
            label12.TabIndex = 4;
            label12.Text = "Mensaje Factura:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(16, 184);
            label13.Name = "label13";
            label13.Size = new Size(54, 15);
            label13.TabIndex = 5;
            label13.Text = "Moneda:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(16, 215);
            label14.Name = "label14";
            label14.Size = new Size(32, 15);
            label14.TabIndex = 6;
            label14.Text = "I.S.V:";
            // 
            // V_Configuracion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(label7);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label6);
            Name = "V_Configuracion";
            Text = "V_Configuracion";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label6;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label7;
        private GroupBox groupBox1;
        private Label label8;
        private Label label10;
        private Label label9;
        private Label label11;
        private Label label14;
        private Label label13;
        private Label label12;
    }
}