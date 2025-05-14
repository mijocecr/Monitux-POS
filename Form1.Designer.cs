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
            miniatura_Inventario1 = new Prueba.Miniatura_Inventario();
            button1 = new Button();
            button2 = new Button();
            listBox1 = new ListBox();
            textBox1 = new TextBox();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            SuspendLayout();
            // 
            // miniatura_Inventario1
            // 
            miniatura_Inventario1.actualizarItem = false;
            miniatura_Inventario1.BorderStyle = BorderStyle.FixedSingle;
            miniatura_Inventario1.cantidad = 0D;
            miniatura_Inventario1.codigo = null;
            miniatura_Inventario1.codigo_barra = null;
            miniatura_Inventario1.codigo_fabricante = null;
            miniatura_Inventario1.codigo_qr = null;
            miniatura_Inventario1.comentario = null;
            miniatura_Inventario1.descripcion = null;
            miniatura_Inventario1.Location = new Point(12, 24);
            miniatura_Inventario1.marca = null;
            miniatura_Inventario1.MaximumSize = new Size(116, 156);
            miniatura_Inventario1.MinimumSize = new Size(116, 156);
            miniatura_Inventario1.Name = "miniatura_Inventario1";
            miniatura_Inventario1.precio_costo = 0D;
            miniatura_Inventario1.precio_venta = 0D;
            miniatura_Inventario1.proveedor = null;
            miniatura_Inventario1.secuencial = 0;
            miniatura_Inventario1.secuencial_categoria = 0;
            miniatura_Inventario1.secuencial_proveedor = 0;
            miniatura_Inventario1.seleccionado = false;
            miniatura_Inventario1.Size = new Size(116, 156);
            miniatura_Inventario1.TabIndex = 0;
            miniatura_Inventario1.unidadesAgregar = 0D;
            miniatura_Inventario1.unidadesRetirar = 0D;
            miniatura_Inventario1.urlImagen = null;
            miniatura_Inventario1.vista = null;
            miniatura_Inventario1.vistaEditar = null;
            // 
            // button1
            // 
            button1.Location = new Point(391, 263);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(391, 292);
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
            listBox1.Location = new Point(12, 344);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(776, 94);
            listBox1.TabIndex = 3;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(339, 94);
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
            button4.Location = new Point(660, 183);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 6;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(665, 225);
            button5.Name = "button5";
            button5.Size = new Size(70, 23);
            button5.TabIndex = 7;
            button5.Text = "button5";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(textBox1);
            Controls.Add(listBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(miniatura_Inventario1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Prueba.Miniatura_Inventario miniatura_Inventario1;
        private Button button1;
        private Button button2;
        private ListBox listBox1;
        private TextBox textBox1;
        private Button button3;
        private Button button4;
        private Button button5;
    }
}
