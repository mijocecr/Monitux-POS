namespace Monitux_POS.Ventanas
{
    partial class V_Ingresos_Egresos
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
            comboBox1 = new ComboBox();
            panel1 = new Panel();
            txtTotal = new TextBox();
            label3 = new Label();
            label2 = new Label();
            txtDescripcion = new TextBox();
            label1 = new Label();
            button1 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Efectivo", "Tarjeta", "Deposito a CTA", "Transferencia", "Otro" });
            comboBox1.Location = new Point(59, 11);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(141, 23);
            comboBox1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 32, 40);
            panel1.Controls.Add(txtTotal);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txtDescripcion);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(comboBox1);
            panel1.Location = new Point(12, 17);
            panel1.Name = "panel1";
            panel1.Size = new Size(212, 187);
            panel1.TabIndex = 1;
            // 
            // txtTotal
            // 
            txtTotal.Location = new Point(59, 141);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(141, 23);
            txtTotal.TabIndex = 5;
            txtTotal.TextAlign = HorizontalAlignment.Center;
            txtTotal.KeyPress += textBox2_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(7, 144);
            label3.Name = "label3";
            label3.Size = new Size(46, 15);
            label3.TabIndex = 4;
            label3.Text = "Monto:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(73, 49);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 3;
            label2.Text = "Descripcion";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(19, 72);
            txtDescripcion.MaxLength = 120;
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ScrollBars = ScrollBars.Vertical;
            txtDescripcion.Size = new Size(181, 54);
            txtDescripcion.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(19, 14);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 1;
            label1.Text = "Tipo:";
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.Yellow;
            button1.Location = new Point(47, 217);
            button1.Name = "button1";
            button1.Size = new Size(134, 38);
            button1.TabIndex = 2;
            button1.Text = "Registrar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // V_Ingresos_Egresos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            ClientSize = new Size(241, 263);
            Controls.Add(button1);
            Controls.Add(panel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "V_Ingresos_Egresos";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "V_Ingresos_Egresos";
            Load += V_Ingresos_Egresos_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox comboBox1;
        private Panel panel1;
        private Label label3;
        private Label label2;
        private TextBox txtDescripcion;
        private Label label1;
        private TextBox txtTotal;
        private Button button1;
    }
}