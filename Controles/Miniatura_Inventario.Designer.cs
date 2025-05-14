namespace Prueba
{
    partial class Miniatura_Inventario
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Miniatura_Inventario));
            Imagen = new PictureBox();
            Codigo = new Label();
            Check = new CheckBox();
            Cantidad = new NumericUpDown();
            Precio = new Label();
            Menu = new ContextMenuStrip(components);
            Menu_Cambiar_Imagen = new ToolStripMenuItem();
            Menu_Imagen_Local = new ToolStripMenuItem();
            Menu_Imagen_Web = new ToolStripMenuItem();
            Menu_Agregar_Comentario = new ToolStripMenuItem();
            Menu_Inventario = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)Imagen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Cantidad).BeginInit();
            Menu.SuspendLayout();
            SuspendLayout();
            // 
            // Imagen
            // 
            Imagen.ErrorImage = (Image)resources.GetObject("Imagen.ErrorImage");
            Imagen.Image = (Image)resources.GetObject("Imagen.Image");
            Imagen.InitialImage = null;
            Imagen.Location = new Point(5, 27);
            Imagen.Name = "Imagen";
            Imagen.Size = new Size(104, 104);
            Imagen.SizeMode = PictureBoxSizeMode.StretchImage;
            Imagen.TabIndex = 0;
            Imagen.TabStop = false;
            Imagen.Click += pictureBox1_Click;
            Imagen.MouseLeave += pictureBox1_MouseLeave;
            Imagen.MouseHover += pictureBox1_MouseHover;
            // 
            // Codigo
            // 
            Codigo.Font = new Font("Bahnschrift SemiCondensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Codigo.Location = new Point(6, 133);
            Codigo.Name = "Codigo";
            Codigo.Size = new Size(103, 18);
            Codigo.TabIndex = 1;
            Codigo.Text = "Codigo";
            Codigo.TextAlign = ContentAlignment.TopCenter;
            Codigo.Click += label1_Click;
            // 
            // Check
            // 
            Check.AutoSize = true;
            Check.Location = new Point(6, 8);
            Check.Name = "Check";
            Check.Size = new Size(15, 14);
            Check.TabIndex = 2;
            Check.UseVisualStyleBackColor = true;
            Check.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // Cantidad
            // 
            Cantidad.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Cantidad.Location = new Point(70, 4);
            Cantidad.Name = "Cantidad";
            Cantidad.Size = new Size(37, 20);
            Cantidad.TabIndex = 3;
            Cantidad.TextAlign = HorizontalAlignment.Center;
            Cantidad.Visible = false;
            // 
            // Precio
            // 
            Precio.AutoSize = true;
            Precio.Font = new Font("Bahnschrift SemiCondensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Precio.ForeColor = SystemColors.MenuHighlight;
            Precio.Location = new Point(23, 5);
            Precio.Name = "Precio";
            Precio.Size = new Size(46, 18);
            Precio.TabIndex = 4;
            Precio.Text = "Precio";
            // 
            // Menu
            // 
            Menu.Items.AddRange(new ToolStripItem[] { Menu_Cambiar_Imagen, Menu_Agregar_Comentario, Menu_Inventario });
            Menu.Name = "contextMenuStrip1";
            Menu.Size = new Size(183, 70);
            // 
            // Menu_Cambiar_Imagen
            // 
            Menu_Cambiar_Imagen.DropDownItems.AddRange(new ToolStripItem[] { Menu_Imagen_Local, Menu_Imagen_Web });
            Menu_Cambiar_Imagen.Name = "Menu_Cambiar_Imagen";
            Menu_Cambiar_Imagen.Size = new Size(182, 22);
            Menu_Cambiar_Imagen.Text = "Cambiar Imagen";
            Menu_Cambiar_Imagen.Click += cambiarImagenToolStripMenuItem_Click;
            // 
            // Menu_Imagen_Local
            // 
            Menu_Imagen_Local.Name = "Menu_Imagen_Local";
            Menu_Imagen_Local.Size = new Size(145, 22);
            Menu_Imagen_Local.Text = "Imagen Local";
            Menu_Imagen_Local.Click += toolStripMenuItem1_Click_1;
            // 
            // Menu_Imagen_Web
            // 
            Menu_Imagen_Web.Name = "Menu_Imagen_Web";
            Menu_Imagen_Web.Size = new Size(145, 22);
            Menu_Imagen_Web.Text = "Imagen Web";
            Menu_Imagen_Web.Click += toolStripMenuItem2_Click;
            // 
            // Menu_Agregar_Comentario
            // 
            Menu_Agregar_Comentario.Name = "Menu_Agregar_Comentario";
            Menu_Agregar_Comentario.Size = new Size(182, 22);
            Menu_Agregar_Comentario.Text = "Agregar Comentario";
            Menu_Agregar_Comentario.Click += agregarComentarioToolStripMenuItem_Click;
            // 
            // Menu_Inventario
            // 
            Menu_Inventario.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem5 });
            Menu_Inventario.Name = "Menu_Inventario";
            Menu_Inventario.Size = new Size(182, 22);
            Menu_Inventario.Text = "Inventario";
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(180, 22);
            toolStripMenuItem3.Text = "Agregar Unidades";
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(180, 22);
            toolStripMenuItem4.Text = "Retirar Unidades";
            toolStripMenuItem4.Click += toolStripMenuItem4_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(180, 22);
            toolStripMenuItem5.Text = "Editar Producto";
            toolStripMenuItem5.Click += toolStripMenuItem5_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // Miniatura_Inventario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(Precio);
            Controls.Add(Cantidad);
            Controls.Add(Check);
            Controls.Add(Codigo);
            Controls.Add(Imagen);
            MaximumSize = new Size(116, 156);
            MinimumSize = new Size(116, 156);
            Name = "Miniatura_Inventario";
            Size = new Size(114, 154);
            Load += UserControl1_Load;
            Paint += UserControl1_Paint;
            MouseLeave += UserControl1_MouseLeave;
            MouseHover += Miniatura_Inventario_MouseHover;
            ((System.ComponentModel.ISupportInitialize)Imagen).EndInit();
            ((System.ComponentModel.ISupportInitialize)Cantidad).EndInit();
            Menu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public PictureBox Imagen;
        public Label Codigo;
        public CheckBox Check;
        public NumericUpDown Cantidad;
        public Label Precio;
        public ContextMenuStrip Menu;
        public OpenFileDialog openFileDialog1;
        public ToolStripMenuItem Menu_Cambiar_Imagen;
        public ToolStripMenuItem Menu_Imagen_Local;
        public ToolStripMenuItem Menu_Imagen_Web;
        public ToolStripMenuItem Menu_Agregar_Comentario;
        public ToolStripMenuItem Menu_Inventario;
        public ToolStripMenuItem toolStripMenuItem3;
        public ToolStripMenuItem toolStripMenuItem4;
        public ToolStripMenuItem toolStripMenuItem5;
    }
}
