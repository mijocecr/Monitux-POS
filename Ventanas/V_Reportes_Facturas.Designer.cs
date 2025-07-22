namespace Monitux_POS.Ventanas
{
    partial class V_Reportes_Facturas
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
            button1 = new Button();
            label4 = new Label();
            combo_TipoVenta = new ComboBox();
            label1 = new Label();
            dateTimePicker1 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            label2 = new Label();
            label3 = new Label();
            panel2 = new Panel();
            button2 = new Button();
            combo_Cliente = new ComboBox();
            label8 = new Label();
            panel3 = new Panel();
            vmax = new TextBox();
            label9 = new Label();
            vmin = new TextBox();
            button3 = new Button();
            label6 = new Label();
            label7 = new Label();
            panel4 = new Panel();
            button4 = new Button();
            label11 = new Label();
            panel5 = new Panel();
            button5 = new Button();
            label13 = new Label();
            panel6 = new Panel();
            label12 = new Label();
            panel7 = new Panel();
            label14 = new Label();
            panel8 = new Panel();
            btn = new Button();
            label15 = new Label();
            combo_TipoCompra = new ComboBox();
            label16 = new Label();
            panel9 = new Panel();
            button7 = new Button();
            label17 = new Label();
            panel10 = new Panel();
            button8 = new Button();
            combo_Proveedor = new ComboBox();
            label19 = new Label();
            panel11 = new Panel();
            button9 = new Button();
            label21 = new Label();
            panel12 = new Panel();
            cmax = new TextBox();
            label22 = new Label();
            cmin = new TextBox();
            button10 = new Button();
            label23 = new Label();
            label24 = new Label();
            label5 = new Label();
            label10 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            panel9.SuspendLayout();
            panel10.SuspendLayout();
            panel11.SuspendLayout();
            panel12.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 32, 45);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(combo_TipoVenta);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(12, 39);
            panel1.Name = "panel1";
            panel1.Size = new Size(335, 74);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(245, 38);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "Generar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(17, 42);
            label4.Name = "label4";
            label4.Size = new Size(34, 15);
            label4.TabIndex = 4;
            label4.Text = "Tipo:";
            // 
            // combo_TipoVenta
            // 
            combo_TipoVenta.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_TipoVenta.FormattingEnabled = true;
            combo_TipoVenta.Items.AddRange(new object[] { "Contado", "Credito" });
            combo_TipoVenta.Location = new Point(58, 39);
            combo_TipoVenta.Name = "combo_TipoVenta";
            combo_TipoVenta.Size = new Size(131, 23);
            combo_TipoVenta.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(17, 10);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 1;
            label1.Text = "Ventas por Tipo";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(248, 42);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(109, 23);
            dateTimePicker1.TabIndex = 1;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Format = DateTimePickerFormat.Short;
            dateTimePicker2.Location = new Point(422, 42);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(109, 23);
            dateTimePicker2.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Yellow;
            label2.Location = new Point(199, 48);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 1;
            label2.Text = "Desde:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Yellow;
            label3.Location = new Point(376, 48);
            label3.Name = "label3";
            label3.Size = new Size(40, 15);
            label3.TabIndex = 3;
            label3.Text = "Hasta:";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(35, 32, 45);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(combo_Cliente);
            panel2.Controls.Add(label8);
            panel2.Location = new Point(12, 122);
            panel2.Name = "panel2";
            panel2.Size = new Size(335, 74);
            panel2.TabIndex = 1;
            // 
            // button2
            // 
            button2.Location = new Point(245, 39);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 5;
            button2.Text = "Generar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // combo_Cliente
            // 
            combo_Cliente.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_Cliente.FormattingEnabled = true;
            combo_Cliente.Location = new Point(17, 39);
            combo_Cliente.Name = "combo_Cliente";
            combo_Cliente.Size = new Size(215, 23);
            combo_Cliente.TabIndex = 1;
            combo_Cliente.SelectedIndexChanged += combo_Cliente_SelectedIndexChanged;
            combo_Cliente.Click += combo_Cliente_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.White;
            label8.Location = new Point(17, 10);
            label8.Name = "label8";
            label8.Size = new Size(102, 15);
            label8.TabIndex = 1;
            label8.Text = "Ventas por Cliente";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(35, 32, 45);
            panel3.Controls.Add(vmax);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(vmin);
            panel3.Controls.Add(button3);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(label7);
            panel3.Location = new Point(12, 206);
            panel3.Name = "panel3";
            panel3.Size = new Size(335, 74);
            panel3.TabIndex = 4;
            // 
            // vmax
            // 
            vmax.Location = new Point(151, 38);
            vmax.Name = "vmax";
            vmax.Size = new Size(85, 23);
            vmax.TabIndex = 7;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(127, 41);
            label9.Name = "label9";
            label9.Size = new Size(18, 15);
            label9.TabIndex = 6;
            label9.Text = "A:";
            // 
            // vmin
            // 
            vmin.Location = new Point(34, 38);
            vmin.Name = "vmin";
            vmin.Size = new Size(85, 23);
            vmin.TabIndex = 5;
            // 
            // button3
            // 
            button3.Location = new Point(245, 37);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 5;
            button3.Text = "Generar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.White;
            label6.Location = new Point(4, 41);
            label6.Name = "label6";
            label6.Size = new Size(24, 15);
            label6.TabIndex = 4;
            label6.Text = "De:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(17, 10);
            label7.Name = "label7";
            label7.Size = new Size(155, 15);
            label7.TabIndex = 1;
            label7.Text = "Ventas por Rango de Totales";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(35, 32, 45);
            panel4.Controls.Add(button4);
            panel4.Controls.Add(label11);
            panel4.Location = new Point(12, 290);
            panel4.Name = "panel4";
            panel4.Size = new Size(335, 74);
            panel4.TabIndex = 5;
            // 
            // button4
            // 
            button4.Location = new Point(245, 29);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 5;
            button4.Text = "Generar";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = Color.White;
            label11.Location = new Point(17, 29);
            label11.Name = "label11";
            label11.Size = new Size(170, 15);
            label11.TabIndex = 1;
            label11.Text = "Unidades Vendidas por Usuario";
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(35, 32, 45);
            panel5.Controls.Add(button5);
            panel5.Controls.Add(label13);
            panel5.Location = new Point(12, 375);
            panel5.Name = "panel5";
            panel5.Size = new Size(335, 64);
            panel5.TabIndex = 6;
            // 
            // button5
            // 
            button5.Location = new Point(245, 24);
            button5.Name = "button5";
            button5.Size = new Size(75, 23);
            button5.TabIndex = 5;
            button5.Text = "Generar";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.ForeColor = Color.White;
            label13.Location = new Point(17, 28);
            label13.Name = "label13";
            label13.Size = new Size(155, 15);
            label13.TabIndex = 1;
            label13.Text = "Todas las Ventas Registradas";
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(0, 168, 107);
            panel6.Controls.Add(label12);
            panel6.Controls.Add(panel1);
            panel6.Controls.Add(panel5);
            panel6.Controls.Add(panel2);
            panel6.Controls.Add(panel4);
            panel6.Controls.Add(panel3);
            panel6.Location = new Point(25, 78);
            panel6.Name = "panel6";
            panel6.Size = new Size(359, 479);
            panel6.TabIndex = 7;
            // 
            // label12
            // 
            label12.Dock = DockStyle.Top;
            label12.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.White;
            label12.Location = new Point(0, 0);
            label12.Name = "label12";
            label12.Size = new Size(359, 37);
            label12.TabIndex = 49;
            label12.Text = "Ventas";
            label12.TextAlign = ContentAlignment.TopCenter;
            // 
            // panel7
            // 
            panel7.BackColor = Color.FromArgb(0, 75, 160);
            panel7.Controls.Add(label14);
            panel7.Controls.Add(panel8);
            panel7.Controls.Add(panel9);
            panel7.Controls.Add(panel10);
            panel7.Controls.Add(panel11);
            panel7.Controls.Add(panel12);
            panel7.Location = new Point(398, 78);
            panel7.Name = "panel7";
            panel7.Size = new Size(361, 479);
            panel7.TabIndex = 8;
            // 
            // label14
            // 
            label14.Dock = DockStyle.Top;
            label14.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = Color.White;
            label14.Location = new Point(0, 0);
            label14.Name = "label14";
            label14.Size = new Size(361, 37);
            label14.TabIndex = 49;
            label14.Text = "Compras";
            label14.TextAlign = ContentAlignment.TopCenter;
            // 
            // panel8
            // 
            panel8.BackColor = Color.FromArgb(35, 32, 45);
            panel8.Controls.Add(btn);
            panel8.Controls.Add(label15);
            panel8.Controls.Add(combo_TipoCompra);
            panel8.Controls.Add(label16);
            panel8.Location = new Point(12, 39);
            panel8.Name = "panel8";
            panel8.Size = new Size(337, 74);
            panel8.TabIndex = 0;
            // 
            // btn
            // 
            btn.Location = new Point(248, 38);
            btn.Name = "btn";
            btn.Size = new Size(75, 23);
            btn.TabIndex = 4;
            btn.Text = "Generar";
            btn.UseVisualStyleBackColor = true;
            btn.Click += button6_Click;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.ForeColor = Color.White;
            label15.Location = new Point(17, 42);
            label15.Name = "label15";
            label15.Size = new Size(34, 15);
            label15.TabIndex = 4;
            label15.Text = "Tipo:";
            // 
            // combo_TipoCompra
            // 
            combo_TipoCompra.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_TipoCompra.FormattingEnabled = true;
            combo_TipoCompra.Items.AddRange(new object[] { "Contado", "Credito" });
            combo_TipoCompra.Location = new Point(58, 39);
            combo_TipoCompra.Name = "combo_TipoCompra";
            combo_TipoCompra.Size = new Size(131, 23);
            combo_TipoCompra.TabIndex = 1;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.ForeColor = Color.White;
            label16.Location = new Point(37, 10);
            label16.Name = "label16";
            label16.Size = new Size(103, 15);
            label16.TabIndex = 1;
            label16.Text = "Compras por Tipo";
            // 
            // panel9
            // 
            panel9.BackColor = Color.FromArgb(35, 32, 45);
            panel9.Controls.Add(button7);
            panel9.Controls.Add(label17);
            panel9.Location = new Point(12, 375);
            panel9.Name = "panel9";
            panel9.Size = new Size(337, 64);
            panel9.TabIndex = 6;
            // 
            // button7
            // 
            button7.Location = new Point(248, 24);
            button7.Name = "button7";
            button7.Size = new Size(75, 23);
            button7.TabIndex = 5;
            button7.Text = "Generar";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.ForeColor = Color.White;
            label17.Location = new Point(45, 28);
            label17.Name = "label17";
            label17.Size = new Size(169, 15);
            label17.TabIndex = 1;
            label17.Text = "Todas las Compras Registradas";
            // 
            // panel10
            // 
            panel10.BackColor = Color.FromArgb(35, 32, 45);
            panel10.Controls.Add(button8);
            panel10.Controls.Add(combo_Proveedor);
            panel10.Controls.Add(label19);
            panel10.Location = new Point(12, 122);
            panel10.Name = "panel10";
            panel10.Size = new Size(337, 74);
            panel10.TabIndex = 1;
            // 
            // button8
            // 
            button8.Location = new Point(248, 39);
            button8.Name = "button8";
            button8.Size = new Size(75, 23);
            button8.TabIndex = 5;
            button8.Text = "Generar";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // combo_Proveedor
            // 
            combo_Proveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_Proveedor.FormattingEnabled = true;
            combo_Proveedor.Location = new Point(17, 37);
            combo_Proveedor.Name = "combo_Proveedor";
            combo_Proveedor.Size = new Size(215, 23);
            combo_Proveedor.TabIndex = 1;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.ForeColor = Color.White;
            label19.Location = new Point(37, 10);
            label19.Name = "label19";
            label19.Size = new Size(133, 15);
            label19.TabIndex = 1;
            label19.Text = "Compras por Proveedor";
            // 
            // panel11
            // 
            panel11.BackColor = Color.FromArgb(35, 32, 45);
            panel11.Controls.Add(button9);
            panel11.Controls.Add(label21);
            panel11.Location = new Point(12, 290);
            panel11.Name = "panel11";
            panel11.Size = new Size(337, 74);
            panel11.TabIndex = 5;
            // 
            // button9
            // 
            button9.Location = new Point(248, 25);
            button9.Name = "button9";
            button9.Size = new Size(75, 23);
            button9.TabIndex = 5;
            button9.Text = "Generar";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.ForeColor = Color.White;
            label21.Location = new Point(45, 29);
            label21.Name = "label21";
            label21.Size = new Size(184, 15);
            label21.TabIndex = 1;
            label21.Text = "Unidades Compradas por Usuario";
            // 
            // panel12
            // 
            panel12.BackColor = Color.FromArgb(35, 32, 45);
            panel12.Controls.Add(cmax);
            panel12.Controls.Add(label22);
            panel12.Controls.Add(cmin);
            panel12.Controls.Add(button10);
            panel12.Controls.Add(label23);
            panel12.Controls.Add(label24);
            panel12.Location = new Point(12, 206);
            panel12.Name = "panel12";
            panel12.Size = new Size(337, 74);
            panel12.TabIndex = 4;
            // 
            // cmax
            // 
            cmax.Location = new Point(153, 38);
            cmax.Name = "cmax";
            cmax.Size = new Size(85, 23);
            cmax.TabIndex = 7;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.ForeColor = Color.White;
            label22.Location = new Point(129, 41);
            label22.Name = "label22";
            label22.Size = new Size(18, 15);
            label22.TabIndex = 6;
            label22.Text = "A:";
            // 
            // cmin
            // 
            cmin.Location = new Point(34, 38);
            cmin.Name = "cmin";
            cmin.Size = new Size(85, 23);
            cmin.TabIndex = 5;
            // 
            // button10
            // 
            button10.Location = new Point(248, 38);
            button10.Name = "button10";
            button10.Size = new Size(75, 23);
            button10.TabIndex = 5;
            button10.Text = "Generar";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.ForeColor = Color.White;
            label23.Location = new Point(4, 41);
            label23.Name = "label23";
            label23.Size = new Size(24, 15);
            label23.TabIndex = 4;
            label23.Text = "De:";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.ForeColor = Color.White;
            label24.Location = new Point(37, 10);
            label24.Name = "label24";
            label24.Size = new Size(169, 15);
            label24.TabIndex = 1;
            label24.Text = "Compras por Rango de Totales";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.Yellow;
            label5.Location = new Point(164, 574);
            label5.Name = "label5";
            label5.Size = new Size(407, 15);
            label5.TabIndex = 9;
            label5.Text = "Para generar cualquier reporte, debe seleccionar primero el rango de fechas.";
            // 
            // label10
            // 
            label10.BackColor = Color.FromArgb(11, 8, 20);
            label10.Dock = DockStyle.Top;
            label10.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.White;
            label10.Location = new Point(0, 0);
            label10.Name = "label10";
            label10.Size = new Size(772, 37);
            label10.TabIndex = 54;
            label10.Text = "Reportes de Facturas";
            label10.TextAlign = ContentAlignment.TopCenter;
            // 
            // V_Reportes_Facturas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            ClientSize = new Size(772, 602);
            Controls.Add(label10);
            Controls.Add(label5);
            Controls.Add(panel7);
            Controls.Add(panel6);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dateTimePicker2);
            Controls.Add(dateTimePicker1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "V_Reportes_Facturas";
            Text = "V_Reportes_Facturas";
            Load += V_Reportes_Facturas_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel6.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            panel12.ResumeLayout(false);
            panel12.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label4;
        private Label label3;
        private Label label2;
        private ComboBox combo_TipoVenta;
        private DateTimePicker dateTimePicker2;
        private DateTimePicker dateTimePicker1;
        private Label label1;
        private Panel panel2;
        private ComboBox combo_Cliente;
        private Label label8;
        private Button button1;
        private Button button2;
        private Panel panel3;
        private TextBox vmax;
        private Label label9;
        private TextBox vmin;
        private Button button3;
        private Label label6;
        private Label label7;
        private Panel panel4;
        private Button button4;
        private Label label11;
        private Panel panel5;
        private Button button5;
        private Label label13;
        private Panel panel6;
        private Label label12;
        private Panel panel7;
        private Label label14;
        private Panel panel8;
        private Button btn;
        private Label label15;
        private ComboBox combo_TipoCompra;
        private Label label16;
        private Panel panel9;
        private Button button7;
        private Label label17;
        private Panel panel10;
        private Button button8;
        private ComboBox combo_Proveedor;
        private Label label19;
        private Panel panel11;
        private Button button9;
        private Label label21;
        private Panel panel12;
        private TextBox cmax;
        private Label label22;
        private TextBox cmin;
        private Button button10;
        private Label label23;
        private Label label24;
        private Label label5;
        private Label label10;
    }
}