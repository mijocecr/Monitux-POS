namespace Monitux_POS
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            listBox1 = new ListBox();
            textBox1 = new TextBox();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            button7 = new Button();
            reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            button8 = new Button();
            listView1 = new ListView();
            miniatura_Producto1 = new Miniatura_Producto();
            button9 = new Button();
            button10 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(65, 426);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(65, 455);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 533);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(776, 94);
            listBox1.TabIndex = 3;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(2, 234);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(410, 23);
            textBox1.TabIndex = 4;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button3
            // 
            button3.Location = new Point(182, 292);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 5;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(182, 412);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 6;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(187, 454);
            button5.Name = "button5";
            button5.Size = new Size(70, 23);
            button5.TabIndex = 7;
            button5.Text = "button5";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(87, 91);
            button6.Name = "button6";
            button6.Size = new Size(91, 36);
            button6.TabIndex = 8;
            button6.Text = "button6";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Location = new Point(457, 158);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(331, 309);
            flowLayoutPanel1.TabIndex = 9;
            // 
            // button7
            // 
            button7.Location = new Point(297, 158);
            button7.Name = "button7";
            button7.Size = new Size(83, 50);
            button7.TabIndex = 10;
            button7.Text = "button7";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // reportViewer1
            // 
            reportViewer1.Location = new Point(0, 0);
            reportViewer1.Name = "ReportViewer";
            reportViewer1.ServerReport.BearerToken = null;
            reportViewer1.Size = new Size(396, 246);
            reportViewer1.TabIndex = 0;
            // 
            // button8
            // 
            button8.Location = new Point(871, 474);
            button8.Name = "button8";
            button8.Size = new Size(75, 23);
            button8.TabIndex = 13;
            button8.Text = "button8";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click_1;
            // 
            // listView1
            // 
            listView1.Location = new Point(794, 12);
            listView1.Name = "listView1";
            listView1.Size = new Size(382, 456);
            listView1.TabIndex = 14;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.ItemSelectionChanged += listView1_ItemSelectionChanged;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // miniatura_Producto1
            // 
            miniatura_Producto1.actualizarItem = false;
            miniatura_Producto1.BorderStyle = BorderStyle.FixedSingle;
            miniatura_Producto1.Cantidad = 0D;
            miniatura_Producto1.cantidadSelecccionItem = 0;
            miniatura_Producto1.Codigo = null;
            miniatura_Producto1.Codigo_Barra = null;
            miniatura_Producto1.Codigo_Fabricante = null;
            miniatura_Producto1.Codigo_QR = null;
            miniatura_Producto1.Comentario = "";
            miniatura_Producto1.Descripcion = "Prueba";
            miniatura_Producto1.Existencia_Minima = 0D;
            miniatura_Producto1.Imagen = null;
            miniatura_Producto1.Location = new Point(1188, 289);
            miniatura_Producto1.Marca = null;
            miniatura_Producto1.Name = "miniatura_Producto1";
            miniatura_Producto1.Precio_Costo = 0D;
            miniatura_Producto1.Precio_Venta = 0D;
            miniatura_Producto1.Producto = null;
            miniatura_Producto1.Secuencial = 0;
            miniatura_Producto1.Secuencial_Categoria = 0;
            miniatura_Producto1.Secuencial_Proveedor = 0;
            miniatura_Producto1.Secuencial_Usuario = 0;
            miniatura_Producto1.Seleccionado = false;
            miniatura_Producto1.Size = new Size(118, 160);
            miniatura_Producto1.TabIndex = 11;
            miniatura_Producto1.unidadesAgregar = 0D;
            miniatura_Producto1.unidadesRetirar = 0D;
            miniatura_Producto1.vista = null;
            miniatura_Producto1.vistaEditar = null;
            // 
            // button9
            // 
            button9.Location = new Point(1063, 493);
            button9.Name = "button9";
            button9.Size = new Size(113, 45);
            button9.TabIndex = 15;
            button9.Text = "button9";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button10
            // 
            button10.Location = new Point(348, 91);
            button10.Name = "button10";
            button10.Size = new Size(75, 23);
            button10.TabIndex = 16;
            button10.Text = "button10";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1318, 690);
            Controls.Add(button10);
            Controls.Add(button9);
            Controls.Add(listView1);
            Controls.Add(button8);
            Controls.Add(miniatura_Producto1);
            Controls.Add(button7);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(textBox1);
            Controls.Add(listBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private Button button2;
        private ListBox listBox1;
        private TextBox textBox1;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button button7;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Button button8;
        private ListView listView1;
        private Miniatura_Producto miniatura_Producto1;
        private Button button9;
        private Button button10;
    }
}
