﻿
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
            Menu = new ContextMenuStrip(components);
            cambiarImagenToolStripMenuItem = new ToolStripMenuItem();
            imagenLocalToolStripMenuItem = new ToolStripMenuItem();
            imagenWebToolStripMenuItem = new ToolStripMenuItem();
            camaraToolStripMenuItem = new ToolStripMenuItem();
            agregarComentarioToolStripMenuItem = new ToolStripMenuItem();
            inventarioToolStripMenuItem = new ToolStripMenuItem();
            agregarUnidadesToolStripMenuItem = new ToolStripMenuItem();
            retirarUnidadesToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            editarProductoToolStripMenuItem = new ToolStripMenuItem();
            ampliarToolStripMenuItem = new ToolStripMenuItem();
            Item_Moneda = new Label();
            ((System.ComponentModel.ISupportInitialize)Item_Imagen).BeginInit();
            Menu.SuspendLayout();
            SuspendLayout();
            // 
            // Item_Imagen
            // 
            Item_Imagen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Item_Imagen.Location = new Point(7, 24);
            Item_Imagen.Name = "Item_Imagen";
            Item_Imagen.Size = new Size(102, 107);
            Item_Imagen.SizeMode = PictureBoxSizeMode.StretchImage;
            Item_Imagen.TabIndex = 0;
            Item_Imagen.TabStop = false;
            Item_Imagen.Click += Item_Imagen_Click;
            Item_Imagen.MouseLeave += Item_Imagen_MouseLeave;
            Item_Imagen.MouseHover += Item_Imagen_MouseHover;
            // 
            // Item_Precio
            // 
            Item_Precio.AutoSize = true;
            Item_Precio.BackColor = Color.Transparent;
            Item_Precio.Font = new Font("Bahnschrift SemiCondensed", 11.25F);
            Item_Precio.ForeColor = Color.White;
            Item_Precio.Location = new Point(53, 4);
            Item_Precio.Name = "Item_Precio";
            Item_Precio.Size = new Size(46, 18);
            Item_Precio.TabIndex = 1;
            Item_Precio.Text = "Precio";
            Item_Precio.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Item_Codigo
            // 
            Item_Codigo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Item_Codigo.BackColor = Color.Transparent;
            Item_Codigo.Font = new Font("Bahnschrift SemiCondensed", 11.25F);
            Item_Codigo.ForeColor = Color.White;
            Item_Codigo.Location = new Point(5, 133);
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
            Item_Seleccionado.Location = new Point(6, 8);
            Item_Seleccionado.Name = "Item_Seleccionado";
            Item_Seleccionado.Size = new Size(15, 14);
            Item_Seleccionado.TabIndex = 3;
            Item_Seleccionado.TextAlign = ContentAlignment.BottomCenter;
            Item_Seleccionado.UseVisualStyleBackColor = false;
            Item_Seleccionado.CheckedChanged += Item_Seleccionado_CheckedChanged;
            // 
            // Menu
            // 
            Menu.Items.AddRange(new ToolStripItem[] { cambiarImagenToolStripMenuItem, agregarComentarioToolStripMenuItem, inventarioToolStripMenuItem, ampliarToolStripMenuItem });
            Menu.Name = "Menu";
            Menu.Size = new Size(181, 114);
            Menu.Opening += Menu_Opening;
            // 
            // cambiarImagenToolStripMenuItem
            // 
            cambiarImagenToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { imagenLocalToolStripMenuItem, imagenWebToolStripMenuItem, camaraToolStripMenuItem });
            cambiarImagenToolStripMenuItem.ForeColor = Color.Black;
            cambiarImagenToolStripMenuItem.Image = (Image)resources.GetObject("cambiarImagenToolStripMenuItem.Image");
            cambiarImagenToolStripMenuItem.Name = "cambiarImagenToolStripMenuItem";
            cambiarImagenToolStripMenuItem.Size = new Size(180, 22);
            cambiarImagenToolStripMenuItem.Text = "Imagen";
            cambiarImagenToolStripMenuItem.Click += cambiarImagenToolStripMenuItem_Click;
            // 
            // imagenLocalToolStripMenuItem
            // 
            imagenLocalToolStripMenuItem.Name = "imagenLocalToolStripMenuItem";
            imagenLocalToolStripMenuItem.Size = new Size(132, 22);
            imagenLocalToolStripMenuItem.Text = "Local";
            imagenLocalToolStripMenuItem.Click += imagenLocalToolStripMenuItem_Click;
            // 
            // imagenWebToolStripMenuItem
            // 
            imagenWebToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point, 0);
            imagenWebToolStripMenuItem.ForeColor = Color.Blue;
            imagenWebToolStripMenuItem.Name = "imagenWebToolStripMenuItem";
            imagenWebToolStripMenuItem.Size = new Size(132, 22);
            imagenWebToolStripMenuItem.Text = "Web";
            imagenWebToolStripMenuItem.Click += imagenWebToolStripMenuItem_Click;
            // 
            // camaraToolStripMenuItem
            // 
            camaraToolStripMenuItem.Name = "camaraToolStripMenuItem";
            camaraToolStripMenuItem.Size = new Size(132, 22);
            camaraToolStripMenuItem.Text = "Hacer Foto";
            camaraToolStripMenuItem.Click += camaraToolStripMenuItem_Click;
            // 
            // agregarComentarioToolStripMenuItem
            // 
            agregarComentarioToolStripMenuItem.AutoToolTip = true;
            agregarComentarioToolStripMenuItem.BackColor = SystemColors.Control;
            agregarComentarioToolStripMenuItem.ForeColor = Color.Black;
            agregarComentarioToolStripMenuItem.Image = (Image)resources.GetObject("agregarComentarioToolStripMenuItem.Image");
            agregarComentarioToolStripMenuItem.Name = "agregarComentarioToolStripMenuItem";
            agregarComentarioToolStripMenuItem.Size = new Size(180, 22);
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
            inventarioToolStripMenuItem.Size = new Size(180, 22);
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
            // ampliarToolStripMenuItem
            // 
            ampliarToolStripMenuItem.Image = (Image)resources.GetObject("ampliarToolStripMenuItem.Image");
            ampliarToolStripMenuItem.Name = "ampliarToolStripMenuItem";
            ampliarToolStripMenuItem.Size = new Size(180, 22);
            ampliarToolStripMenuItem.Text = "Ampliar";
            ampliarToolStripMenuItem.Click += ampliarToolStripMenuItem_Click;
            // 
            // Item_Moneda
            // 
            Item_Moneda.AutoSize = true;
            Item_Moneda.BackColor = Color.Transparent;
            Item_Moneda.Font = new Font("Bahnschrift SemiCondensed", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Item_Moneda.ForeColor = Color.FromArgb(0, 192, 192);
            Item_Moneda.Location = new Point(27, 8);
            Item_Moneda.Name = "Item_Moneda";
            Item_Moneda.Size = new Size(26, 13);
            Item_Moneda.TabIndex = 4;
            Item_Moneda.Text = "EUR.";
            Item_Moneda.TextAlign = ContentAlignment.TopRight;
            // 
            // Miniatura_Producto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 32, 45);
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(Item_Moneda);
            Controls.Add(Item_Seleccionado);
            Controls.Add(Item_Codigo);
            Controls.Add(Item_Precio);
            Controls.Add(Item_Imagen);
            ForeColor = Color.White;
            Name = "Miniatura_Producto";
            Size = new Size(118, 156);
            Load += Miniatura_Producto_Load_1;
            Paint += Miniatura_Producto_Paint;
            MouseLeave += Miniatura_Producto_MouseLeave;
            ((System.ComponentModel.ISupportInitialize)Item_Imagen).EndInit();
            Menu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        public PictureBox Item_Imagen;
        private Label Item_Precio;
        private Label Item_Codigo;
        public CheckBox Item_Seleccionado;
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
        public Label Item_Moneda;
        private ToolStripMenuItem camaraToolStripMenuItem;
        private ToolStripMenuItem ampliarToolStripMenuItem;
    }
}
