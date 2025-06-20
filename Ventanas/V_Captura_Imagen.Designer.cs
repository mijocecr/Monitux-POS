namespace Monitux_POS.Ventanas
{
    partial class V_Captura_Imagen
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
            cboCamaras = new ComboBox();
            label6 = new Label();
            panel1 = new Panel();
            picImagen = new PictureBox();
            button1 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picImagen).BeginInit();
            SuspendLayout();
            // 
            // cboCamaras
            // 
            cboCamaras.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCamaras.FlatStyle = FlatStyle.System;
            cboCamaras.FormattingEnabled = true;
            cboCamaras.Location = new Point(10, 49);
            cboCamaras.Name = "cboCamaras";
            cboCamaras.Size = new Size(250, 23);
            cboCamaras.TabIndex = 0;
            // 
            // label6
            // 
            label6.Dock = DockStyle.Top;
            label6.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(0, 0);
            label6.Name = "label6";
            label6.Size = new Size(271, 37);
            label6.TabIndex = 52;
            label6.Text = "Codigo";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 32, 40);
            panel1.Controls.Add(picImagen);
            panel1.Location = new Point(10, 84);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 196);
            panel1.TabIndex = 53;
            // 
            // picImagen
            // 
            picImagen.Location = new Point(3, 3);
            picImagen.Name = "picImagen";
            picImagen.Size = new Size(244, 190);
            picImagen.SizeMode = PictureBoxSizeMode.Zoom;
            picImagen.TabIndex = 0;
            picImagen.TabStop = false;
            picImagen.Click += picImage_Click;
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(10, 286);
            button1.Name = "button1";
            button1.Size = new Size(250, 38);
            button1.TabIndex = 54;
            button1.Text = "Hacer Foto";
            button1.TextImageRelation = TextImageRelation.ImageAboveText;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // V_Captura_Imagen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            ClientSize = new Size(271, 343);
            Controls.Add(button1);
            Controls.Add(panel1);
            Controls.Add(label6);
            Controls.Add(cboCamaras);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "V_Captura_Imagen";
            Text = "V_Captura_Imagen";
            FormClosing += V_Captura_Imagen_FormClosing;
            Load += V_Captura_Imagen_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picImagen).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cboCamaras;
        private Label label6;
        private Panel panel1;
        private PictureBox picImagen;
        private Button button1;
    }
}