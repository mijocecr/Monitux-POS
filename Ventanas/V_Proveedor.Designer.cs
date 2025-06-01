namespace Monitux_POS.Ventanas
{
    partial class V_Proveedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Proveedor));
            label1 = new Label();
            txt_Nombre = new TextBox();
            txt_Telefono = new TextBox();
            label2 = new Label();
            txt_Direccion = new TextBox();
            label3 = new Label();
            txt_Email = new TextBox();
            label4 = new Label();
            txt_Contacto = new TextBox();
            label5 = new Label();
            label6 = new Label();
            checkBox1 = new CheckBox();
            comboBox1 = new ComboBox();
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            Menu_Agregar = new ToolStripMenuItem();
            nuevoProveedorToolStripMenuItem = new ToolStripMenuItem();
            categoriaToolStripMenuItem = new ToolStripMenuItem();
            Menu_Guardar = new ToolStripMenuItem();
            Menu_Eliminar = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            salirToolStripMenuItem = new ToolStripMenuItem();
            pictureBox1 = new PictureBox();
            dataGridView1 = new DataGridView();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 39);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 0;
            label1.Text = "Nombre:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_Nombre
            // 
            txt_Nombre.Location = new Point(81, 36);
            txt_Nombre.Name = "txt_Nombre";
            txt_Nombre.Size = new Size(205, 23);
            txt_Nombre.TabIndex = 1;
            // 
            // txt_Telefono
            // 
            txt_Telefono.Location = new Point(81, 65);
            txt_Telefono.Name = "txt_Telefono";
            txt_Telefono.Size = new Size(205, 23);
            txt_Telefono.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 68);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 2;
            label2.Text = "Telefono:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_Direccion
            // 
            txt_Direccion.Location = new Point(81, 93);
            txt_Direccion.Name = "txt_Direccion";
            txt_Direccion.Size = new Size(205, 23);
            txt_Direccion.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 96);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 4;
            label3.Text = "Direccion:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_Email
            // 
            txt_Email.Location = new Point(81, 123);
            txt_Email.Name = "txt_Email";
            txt_Email.Size = new Size(205, 23);
            txt_Email.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(27, 126);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 6;
            label4.Text = "Email:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txt_Contacto
            // 
            txt_Contacto.Location = new Point(81, 151);
            txt_Contacto.Name = "txt_Contacto";
            txt_Contacto.Size = new Size(205, 23);
            txt_Contacto.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 154);
            label5.Name = "label5";
            label5.Size = new Size(59, 15);
            label5.TabIndex = 8;
            label5.Text = "Contacto:";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(32, 183);
            label6.Name = "label6";
            label6.Size = new Size(34, 15);
            label6.TabIndex = 10;
            label6.Text = "Tipo:";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(226, 182);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(60, 19);
            checkBox1.TabIndex = 12;
            checkBox1.Text = "Activo";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Local", "Nacional", "Internacional", "Virtual" });
            comboBox1.Location = new Point(81, 180);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(107, 23);
            comboBox1.TabIndex = 13;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ControlLightLight;
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(420, 24);
            menuStrip1.TabIndex = 14;
            menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { Menu_Agregar, Menu_Guardar, Menu_Eliminar, toolStripSeparator1, salirToolStripMenuItem });
            archivoToolStripMenuItem.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            archivoToolStripMenuItem.ForeColor = Color.DarkViolet;
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(69, 20);
            archivoToolStripMenuItem.Text = "Opciones";
            // 
            // Menu_Agregar
            // 
            Menu_Agregar.DropDownItems.AddRange(new ToolStripItem[] { nuevoProveedorToolStripMenuItem, categoriaToolStripMenuItem });
            Menu_Agregar.Image = (Image)resources.GetObject("Menu_Agregar.Image");
            Menu_Agregar.Name = "Menu_Agregar";
            Menu_Agregar.Size = new Size(180, 22);
            Menu_Agregar.Text = "Registrar";
            // 
            // nuevoProveedorToolStripMenuItem
            // 
            nuevoProveedorToolStripMenuItem.Image = (Image)resources.GetObject("nuevoProveedorToolStripMenuItem.Image");
            nuevoProveedorToolStripMenuItem.Name = "nuevoProveedorToolStripMenuItem";
            nuevoProveedorToolStripMenuItem.Size = new Size(128, 22);
            nuevoProveedorToolStripMenuItem.Text = "Proveedor";
            // 
            // categoriaToolStripMenuItem
            // 
            categoriaToolStripMenuItem.Image = (Image)resources.GetObject("categoriaToolStripMenuItem.Image");
            categoriaToolStripMenuItem.Name = "categoriaToolStripMenuItem";
            categoriaToolStripMenuItem.Size = new Size(128, 22);
            categoriaToolStripMenuItem.Text = "Categoria";
            // 
            // Menu_Guardar
            // 
            Menu_Guardar.ForeColor = Color.ForestGreen;
            Menu_Guardar.Image = (Image)resources.GetObject("Menu_Guardar.Image");
            Menu_Guardar.Name = "Menu_Guardar";
            Menu_Guardar.Size = new Size(180, 22);
            Menu_Guardar.Text = "Guardar";
            // 
            // Menu_Eliminar
            // 
            Menu_Eliminar.ForeColor = Color.Red;
            Menu_Eliminar.Image = (Image)resources.GetObject("Menu_Eliminar.Image");
            Menu_Eliminar.Name = "Menu_Eliminar";
            Menu_Eliminar.Size = new Size(180, 22);
            Menu_Eliminar.Text = "Eliminar";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // salirToolStripMenuItem
            // 
            salirToolStripMenuItem.Image = (Image)resources.GetObject("salirToolStripMenuItem.Image");
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(180, 22);
            salirToolStripMenuItem.Text = "Salir";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(292, 39);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(122, 135);
            pictureBox1.TabIndex = 15;
            pictureBox1.TabStop = false;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 238);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(402, 185);
            dataGridView1.TabIndex = 16;
            // 
            // V_Proveedor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 435);
            Controls.Add(dataGridView1);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            Controls.Add(comboBox1);
            Controls.Add(checkBox1);
            Controls.Add(label6);
            Controls.Add(txt_Contacto);
            Controls.Add(label5);
            Controls.Add(txt_Email);
            Controls.Add(label4);
            Controls.Add(txt_Direccion);
            Controls.Add(label3);
            Controls.Add(txt_Telefono);
            Controls.Add(label2);
            Controls.Add(txt_Nombre);
            Controls.Add(label1);
            Name = "V_Proveedor";
            Text = "V_Proveedor";
            Load += V_Proveedor_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txt_Nombre;
        private TextBox txt_Telefono;
        private Label label2;
        private TextBox txt_Direccion;
        private Label label3;
        private TextBox txt_Email;
        private Label label4;
        private TextBox txt_Contacto;
        private Label label5;
        private Label label6;
        private CheckBox checkBox1;
        private ComboBox comboBox1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem Menu_Agregar;
        private ToolStripMenuItem nuevoProveedorToolStripMenuItem;
        private ToolStripMenuItem categoriaToolStripMenuItem;
        private ToolStripMenuItem Menu_Guardar;
        private ToolStripMenuItem Menu_Eliminar;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem salirToolStripMenuItem;
        private PictureBox pictureBox1;
        private DataGridView dataGridView1;
    }
}