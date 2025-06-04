namespace Monitux_POS.Ventanas
{
   public partial class V_Factura_Venta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Factura_Venta));
            menuStrip1 = new MenuStrip();
            opcionesToolStripMenuItem = new ToolStripMenuItem();
            nuevoToolStripMenuItem1 = new ToolStripMenuItem();
            guardarToolStripMenuItem = new ToolStripMenuItem();
            eliminarToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            cerrarToolStripMenuItem = new ToolStripMenuItem();
            flowLayoutPanel1 = new FlowLayoutPanel();
            dataGridView1 = new DataGridView();
            comboCliente = new ComboBox();
            label1 = new Label();
            button2 = new Button();
            textBox1 = new TextBox();
            comboBox2 = new ComboBox();
            groupBox1 = new GroupBox();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button1 = new Button();
            button6 = new Button();
            label2 = new Label();
            comboBox3 = new ComboBox();
            dateTimePicker1 = new DateTimePicker();
            label3 = new Label();
            groupBox2 = new GroupBox();
            textBox2 = new TextBox();
            listBox2 = new ListBox();
            linkLabel3 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            button8 = new Button();
            button7 = new Button();
            listBox1 = new ListBox();
            label4 = new Label();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.White;
            menuStrip1.Items.AddRange(new ToolStripItem[] { opcionesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(806, 24);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // opcionesToolStripMenuItem
            // 
            opcionesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { nuevoToolStripMenuItem1, guardarToolStripMenuItem, eliminarToolStripMenuItem, toolStripSeparator1, cerrarToolStripMenuItem });
            opcionesToolStripMenuItem.ForeColor = Color.DarkViolet;
            opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            opcionesToolStripMenuItem.Size = new Size(69, 20);
            opcionesToolStripMenuItem.Text = "Opciones";
            // 
            // nuevoToolStripMenuItem1
            // 
            nuevoToolStripMenuItem1.Image = (Image)resources.GetObject("nuevoToolStripMenuItem1.Image");
            nuevoToolStripMenuItem1.Name = "nuevoToolStripMenuItem1";
            nuevoToolStripMenuItem1.Size = new Size(117, 22);
            nuevoToolStripMenuItem1.Text = "Nuevo";
            // 
            // guardarToolStripMenuItem
            // 
            guardarToolStripMenuItem.ForeColor = Color.FromArgb(0, 192, 0);
            guardarToolStripMenuItem.Image = (Image)resources.GetObject("guardarToolStripMenuItem.Image");
            guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            guardarToolStripMenuItem.Size = new Size(117, 22);
            guardarToolStripMenuItem.Text = "Guardar";
            // 
            // eliminarToolStripMenuItem
            // 
            eliminarToolStripMenuItem.ForeColor = Color.Red;
            eliminarToolStripMenuItem.Image = (Image)resources.GetObject("eliminarToolStripMenuItem.Image");
            eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            eliminarToolStripMenuItem.Size = new Size(117, 22);
            eliminarToolStripMenuItem.Text = "Eliminar";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(114, 6);
            // 
            // cerrarToolStripMenuItem
            // 
            cerrarToolStripMenuItem.Image = (Image)resources.GetObject("cerrarToolStripMenuItem.Image");
            cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            cerrarToolStripMenuItem.Size = new Size(117, 22);
            cerrarToolStripMenuItem.Text = "Salir";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel1.Location = new Point(12, 128);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(396, 344);
            flowLayoutPanel1.TabIndex = 7;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 96);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(365, 202);
            dataGridView1.TabIndex = 8;
            // 
            // comboCliente
            // 
            comboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCliente.FormattingEnabled = true;
            comboCliente.Location = new Point(60, 35);
            comboCliente.Name = "comboCliente";
            comboCliente.Size = new Size(222, 23);
            comboCliente.TabIndex = 9;
            comboCliente.SelectedIndexChanged += comboCliente_SelectedIndexChanged;
            comboCliente.TextChanged += comboCliente_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 38);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 10;
            label1.Text = "Cliente:";
            // 
            // button2
            // 
            button2.Location = new Point(171, 478);
            button2.Name = "button2";
            button2.Size = new Size(75, 90);
            button2.TabIndex = 12;
            button2.Text = "Agregar  Seleccion a Factura";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox1
            // 
            textBox1.ForeColor = Color.FromArgb(192, 0, 192);
            textBox1.Location = new Point(7, 51);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Quiero encontrar...";
            textBox1.Size = new Size(146, 23);
            textBox1.TabIndex = 13;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Codigo", "Descripcion", "Marca", "Codigo_Barra", "Codigo_QR" });
            comboBox2.Location = new Point(7, 22);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(146, 23);
            comboBox2.TabIndex = 14;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(12, 32);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(159, 90);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            groupBox1.Text = "Buscar Por:";
            // 
            // button3
            // 
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.Location = new Point(176, 32);
            button3.Name = "button3";
            button3.Size = new Size(75, 90);
            button3.TabIndex = 17;
            button3.Text = "Nuevo Producto";
            button3.TextAlign = ContentAlignment.BottomCenter;
            button3.TextImageRelation = TextImageRelation.ImageAboveText;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Image = (Image)resources.GetObject("button4.Image");
            button4.Location = new Point(254, 31);
            button4.Name = "button4";
            button4.Size = new Size(75, 90);
            button4.TabIndex = 18;
            button4.Text = "Nuevo Cliente";
            button4.TextImageRelation = TextImageRelation.ImageAboveText;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Image = (Image)resources.GetObject("button5.Image");
            button5.Location = new Point(332, 32);
            button5.Name = "button5";
            button5.Size = new Size(75, 90);
            button5.TabIndex = 19;
            button5.Text = "Importar Cotizacion";
            button5.TextImageRelation = TextImageRelation.ImageAboveText;
            button5.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 255, 128);
            button1.Location = new Point(215, 435);
            button1.Name = "button1";
            button1.Size = new Size(75, 90);
            button1.TabIndex = 20;
            button1.Text = "Generar Cotizacion";
            button1.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(128, 255, 128);
            button6.Location = new Point(296, 435);
            button6.Name = "button6";
            button6.Size = new Size(75, 90);
            button6.TabIndex = 21;
            button6.Text = "Generar Factura";
            button6.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 72);
            label2.Name = "label2";
            label2.Size = new Size(82, 15);
            label2.TabIndex = 22;
            label2.Text = "Tipo de Venta:";
            // 
            // comboBox3
            // 
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "Contado", "Credito" });
            comboBox3.Location = new Point(95, 68);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(91, 23);
            comboBox3.TabIndex = 23;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(288, 68);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(83, 23);
            dateTimePicker1.TabIndex = 24;
            dateTimePicker1.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(226, 72);
            label3.Name = "label3";
            label3.Size = new Size(56, 15);
            label3.TabIndex = 25;
            label3.Text = "Se vence:";
            label3.Visible = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(listBox2);
            groupBox2.Controls.Add(linkLabel3);
            groupBox2.Controls.Add(linkLabel2);
            groupBox2.Controls.Add(linkLabel1);
            groupBox2.Controls.Add(dataGridView1);
            groupBox2.Controls.Add(button6);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(comboCliente);
            groupBox2.Controls.Add(dateTimePicker1);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(comboBox3);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(414, 34);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(379, 534);
            groupBox2.TabIndex = 26;
            groupBox2.TabStop = false;
            groupBox2.Text = "Factura Actual";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // textBox2
            // 
            textBox2.ForeColor = Color.FromArgb(192, 0, 192);
            textBox2.Location = new Point(288, 35);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Telefono...";
            textBox2.Size = new Size(83, 23);
            textBox2.TabIndex = 30;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(7, 316);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(364, 49);
            listBox2.TabIndex = 29;
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.Location = new Point(250, 382);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(121, 15);
            linkLabel3.TabIndex = 28;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "Agregar Otros Cargos";
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(18, 409);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(108, 15);
            linkLabel2.TabIndex = 27;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Agregar Descuento";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(18, 382);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(102, 15);
            linkLabel1.TabIndex = 26;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Agregar Impuesto";
            // 
            // button8
            // 
            button8.BackColor = Color.Red;
            button8.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button8.ForeColor = Color.White;
            button8.Image = (Image)resources.GetObject("button8.Image");
            button8.Location = new Point(11, 476);
            button8.Name = "button8";
            button8.Size = new Size(75, 90);
            button8.TabIndex = 30;
            button8.Text = "Limpiar Factura";
            button8.TextImageRelation = TextImageRelation.ImageAboveText;
            button8.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            button7.BackColor = SystemColors.ControlLightLight;
            button7.Image = (Image)resources.GetObject("button7.Image");
            button7.Location = new Point(91, 478);
            button7.Name = "button7";
            button7.Size = new Size(75, 90);
            button7.TabIndex = 27;
            button7.Text = "Recargar";
            button7.TextImageRelation = TextImageRelation.ImageAboveText;
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // listBox1
            // 
            listBox1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            listBox1.ForeColor = SystemColors.MenuHighlight;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(255, 495);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(149, 64);
            listBox1.TabIndex = 28;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(255, 476);
            label4.Name = "label4";
            label4.Size = new Size(117, 15);
            label4.TabIndex = 29;
            label4.Text = "Items Seleccionados:";
            // 
            // V_Factura_Venta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(806, 578);
            Controls.Add(button8);
            Controls.Add(label4);
            Controls.Add(listBox1);
            Controls.Add(button7);
            Controls.Add(groupBox2);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(menuStrip1);
            Name = "V_Factura_Venta";
            Text = "Ventas";
            Load += V_Factura_Venta_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem opcionesToolStripMenuItem;
        private ToolStripMenuItem nuevoToolStripMenuItem1;
        private ToolStripMenuItem guardarToolStripMenuItem;
        private ToolStripMenuItem eliminarToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem cerrarToolStripMenuItem;
        public FlowLayoutPanel flowLayoutPanel1;
        private DataGridView dataGridView1;
        private ComboBox comboCliente;
        private Label label1;
        private Button button2;
        private TextBox textBox1;
        private ComboBox comboBox2;
        private GroupBox groupBox1;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button1;
        private Button button6;
        private Label label2;
        private ComboBox comboBox3;
        private DateTimePicker dateTimePicker1;
        private Label label3;
        private GroupBox groupBox2;
        private Button button7;
        private ListBox listBox1;
        private Label label4;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel3;
        private ListBox listBox2;
        private Button button8;
        private TextBox textBox2;
    }
}