
namespace Monitux_POS
{
    partial class Miniatura_Producto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Miniatura_Producto));
            Item_Imagen = new PictureBox();
            Item_Precio = new Label();
            Item_Codigo = new Label();
            Item_Seleccionado = new CheckBox();
            numericUpDown1 = new NumericUpDown();
            Menu = new ContextMenuStrip(components);
            cambiarImagenToolStripMenuItem = new ToolStripMenuItem();
            imagenLocalToolStripMenuItem = new ToolStripMenuItem();
            imagenWebToolStripMenuItem = new ToolStripMenuItem();
            agregarComentarioToolStripMenuItem = new ToolStripMenuItem();
            inventarioToolStripMenuItem = new ToolStripMenuItem();
            agregarUnidadesToolStripMenuItem = new ToolStripMenuItem();
            retirarUnidadesToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            editarProductoToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)Item_Imagen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            Menu.SuspendLayout();
            SuspendLayout();
            // 
            // Item_Imagen
            // 
            Item_Imagen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Item_Imagen.Location = new Point(7, 27);
            Item_Imagen.Name = "Item_Imagen";
            Item_Imagen.Size = new Size(102, 112);
            Item_Imagen.SizeMode = PictureBoxSizeMode.StretchImage;
            Item_Imagen.TabIndex = 0;
            Item_Imagen.TabStop = false;
            Item_Imagen.Click += Item_Imagen_Click;
            Item_Imagen.MouseHover += Item_Imagen_MouseHover;
            // 
            // Item_Precio
            // 
            Item_Precio.AutoSize = true;
            Item_Precio.BackColor = Color.Transparent;
            Item_Precio.Font = new Font("Bahnschrift SemiCondensed", 11.25F);
            Item_Precio.Location = new Point(25, 5);
            Item_Precio.Name = "Item_Precio";
            Item_Precio.Size = new Size(46, 18);
            Item_Precio.TabIndex = 1;
            Item_Precio.Text = "Precio";
            // 
            // Item_Codigo
            // 
            Item_Codigo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Item_Codigo.BackColor = Color.Transparent;
            Item_Codigo.Font = new Font("Bahnschrift SemiCondensed", 11.25F);
            Item_Codigo.Location = new Point(5, 135);
            Item_Codigo.Name = "Item_Codigo";
            Item_Codigo.Size = new Size(110, 22);
            Item_Codigo.TabIndex = 2;
            Item_Codigo.Text = "Codigo";
            Item_Codigo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Item_Seleccionado
            // 
            Item_Seleccionado.AutoSize = true;
            Item_Seleccionado.BackColor = Color.Transparent;
            Item_Seleccionado.Enabled = false;
            Item_Seleccionado.Location = new Point(6, 9);
            Item_Seleccionado.Name = "Item_Seleccionado";
            Item_Seleccionado.Size = new Size(15, 14);
            Item_Seleccionado.TabIndex = 3;
            Item_Seleccionado.TextAlign = ContentAlignment.BottomCenter;
            Item_Seleccionado.UseVisualStyleBackColor = false;
            Item_Seleccionado.CheckedChanged += Item_Seleccionado_CheckedChanged;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numericUpDown1.Location = new Point(75, 3);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(33, 23);
            numericUpDown1.TabIndex = 4;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // Menu
            // 
            Menu.Items.AddRange(new ToolStripItem[] { cambiarImagenToolStripMenuItem, agregarComentarioToolStripMenuItem, inventarioToolStripMenuItem });
            Menu.Name = "Menu";
            Menu.Size = new Size(147, 70);
            // 
            // cambiarImagenToolStripMenuItem
            // 
            cambiarImagenToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { imagenLocalToolStripMenuItem, imagenWebToolStripMenuItem });
            cambiarImagenToolStripMenuItem.ForeColor = Color.Black;
            cambiarImagenToolStripMenuItem.Image = (Image)resources.GetObject("cambiarImagenToolStripMenuItem.Image");
            cambiarImagenToolStripMenuItem.Name = "cambiarImagenToolStripMenuItem";
            cambiarImagenToolStripMenuItem.Size = new Size(146, 22);
            cambiarImagenToolStripMenuItem.Text = "Imagen";
            cambiarImagenToolStripMenuItem.Click += cambiarImagenToolStripMenuItem_Click;
            // 
            // imagenLocalToolStripMenuItem
            // 
            imagenLocalToolStripMenuItem.Name = "imagenLocalToolStripMenuItem";
            imagenLocalToolStripMenuItem.Size = new Size(102, 22);
            imagenLocalToolStripMenuItem.Text = "Local";
            imagenLocalToolStripMenuItem.Click += imagenLocalToolStripMenuItem_Click;
            // 
            // imagenWebToolStripMenuItem
            // 
            imagenWebToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point, 0);
            imagenWebToolStripMenuItem.ForeColor = Color.Blue;
            imagenWebToolStripMenuItem.Name = "imagenWebToolStripMenuItem";
            imagenWebToolStripMenuItem.Size = new Size(102, 22);
            imagenWebToolStripMenuItem.Text = "Web";
            imagenWebToolStripMenuItem.Click += imagenWebToolStripMenuItem_Click;
            // 
            // agregarComentarioToolStripMenuItem
            // 
            agregarComentarioToolStripMenuItem.AutoToolTip = true;
            agregarComentarioToolStripMenuItem.BackColor = SystemColors.Control;
            agregarComentarioToolStripMenuItem.ForeColor = Color.Black;
            agregarComentarioToolStripMenuItem.Image = (Image)resources.GetObject("agregarComentarioToolStripMenuItem.Image");
            agregarComentarioToolStripMenuItem.Name = "agregarComentarioToolStripMenuItem";
            agregarComentarioToolStripMenuItem.Size = new Size(146, 22);
            agregarComentarioToolStripMenuItem.Text = "Comentario...";
            agregarComentarioToolStripMenuItem.ToolTipText = "Agregar o actualizar comentario de este producto.";
            agregarComentarioToolStripMenuItem.Click += agregarComentarioToolStripMenuItem_Click;
            // 
            // inventarioToolStripMenuItem
            // 
            inventarioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { agregarUnidadesToolStripMenuItem, retirarUnidadesToolStripMenuItem, toolStripSeparator1, editarProductoToolStripMenuItem });
            inventarioToolStripMenuItem.ForeColor = Color.Black;
            inventarioToolStripMenuItem.Image = (Image)resources.GetObject("inventarioToolStripMenuItem.Image");
            inventarioToolStripMenuItem.Name = "inventarioToolStripMenuItem";
            inventarioToolStripMenuItem.Size = new Size(146, 22);
            inventarioToolStripMenuItem.Text = "Producto";
            inventarioToolStripMenuItem.Click += inventarioToolStripMenuItem_Click;
            // 
            // agregarUnidadesToolStripMenuItem
            // 
            agregarUnidadesToolStripMenuItem.BackColor = SystemColors.Control;
            agregarUnidadesToolStripMenuItem.ForeColor = Color.Black;
            agregarUnidadesToolStripMenuItem.Image = (Image)resources.GetObject("agregarUnidadesToolStripMenuItem.Image");
            agregarUnidadesToolStripMenuItem.Name = "agregarUnidadesToolStripMenuItem";
            agregarUnidadesToolStripMenuItem.Size = new Size(168, 22);
            agregarUnidadesToolStripMenuItem.Text = "Agregar Unidades";
            agregarUnidadesToolStripMenuItem.TextImageRelation = TextImageRelation.TextBeforeImage;
            agregarUnidadesToolStripMenuItem.Click += agregarUnidadesToolStripMenuItem_Click;
            // 
            // retirarUnidadesToolStripMenuItem
            // 
            retirarUnidadesToolStripMenuItem.BackColor = SystemColors.Control;
            retirarUnidadesToolStripMenuItem.ForeColor = Color.Black;
            retirarUnidadesToolStripMenuItem.Image = (Image)resources.GetObject("retirarUnidadesToolStripMenuItem.Image");
            retirarUnidadesToolStripMenuItem.Name = "retirarUnidadesToolStripMenuItem";
            retirarUnidadesToolStripMenuItem.Size = new Size(168, 22);
            retirarUnidadesToolStripMenuItem.Text = "Retirar Unidades";
            retirarUnidadesToolStripMenuItem.Click += retirarUnidadesToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(165, 6);
            // 
            // editarProductoToolStripMenuItem
            // 
            editarProductoToolStripMenuItem.BackColor = SystemColors.Control;
            editarProductoToolStripMenuItem.ForeColor = Color.Black;
            editarProductoToolStripMenuItem.Image = (Image)resources.GetObject("editarProductoToolStripMenuItem.Image");
            editarProductoToolStripMenuItem.Name = "editarProductoToolStripMenuItem";
            editarProductoToolStripMenuItem.Size = new Size(168, 22);
            editarProductoToolStripMenuItem.Text = "Ver Producto";
            editarProductoToolStripMenuItem.Click += editarProductoToolStripMenuItem_Click;
            // 
            // Miniatura_Producto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(numericUpDown1);
            Controls.Add(Item_Seleccionado);
            Controls.Add(Item_Codigo);
            Controls.Add(Item_Precio);
            Controls.Add(Item_Imagen);
            Name = "Miniatura_Producto";
            Size = new Size(118, 156);
            Load += Miniatura_Producto_Load_1;
            Paint += Miniatura_Producto_Paint;
            MouseLeave += Miniatura_Producto_MouseLeave;
            ((System.ComponentModel.ISupportInitialize)Item_Imagen).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            Menu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        public PictureBox Item_Imagen;
        private Label Item_Precio;
        private Label Item_Codigo;
        public CheckBox Item_Seleccionado;
        private NumericUpDown numericUpDown1;
        private ContextMenuStrip Menu;
        private ToolStripMenuItem cambiarImagenToolStripMenuItem;
        private ToolStripMenuItem imagenLocalToolStripMenuItem;
        private ToolStripMenuItem imagenWebToolStripMenuItem;
        private ToolStripMenuItem agregarComentarioToolStripMenuItem;
        private ToolStripMenuItem inventarioToolStripMenuItem;
        private ToolStripMenuItem agregarUnidadesToolStripMenuItem;
        private ToolStripMenuItem retirarUnidadesToolStripMenuItem;
        private ToolStripMenuItem editarProductoToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
    }
}
