namespace Monitux_POS.Ventanas
{
    partial class V_Validador_Licencia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Validador_Licencia));
            lblTitulo = new Label();
            txtLicencia = new TextBox();
            btnValidar = new Button();
            lblResultado = new Label();
            lblCliente = new Label();
            lblExpira = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(66, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(134, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Monitux-POS";
            // 
            // txtLicencia
            // 
            txtLicencia.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtLicencia.Location = new Point(128, 55);
            txtLicencia.Name = "txtLicencia";
            txtLicencia.Size = new Size(112, 25);
            txtLicencia.TabIndex = 1;
            // 
            // btnValidar
            // 
            btnValidar.FlatStyle = FlatStyle.Flat;
            btnValidar.ForeColor = Color.Lime;
            btnValidar.Location = new Point(14, 89);
            btnValidar.Name = "btnValidar";
            btnValidar.Size = new Size(75, 23);
            btnValidar.TabIndex = 2;
            btnValidar.Text = "Activar";
            btnValidar.UseVisualStyleBackColor = true;
            btnValidar.Click += btnValidar_Click;
            // 
            // lblResultado
            // 
            lblResultado.AutoSize = true;
            lblResultado.ForeColor = Color.FromArgb(0, 192, 192);
            lblResultado.Location = new Point(89, 127);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new Size(69, 15);
            lblResultado.TabIndex = 3;
            lblResultado.Text = "Sin Licencia";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.ForeColor = Color.FromArgb(0, 192, 192);
            lblCliente.Location = new Point(89, 155);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(122, 15);
            lblCliente.TabIndex = 4;
            lblCliente.Text = "Cliente No Registrado";
            // 
            // lblExpira
            // 
            lblExpira.AutoSize = true;
            lblExpira.ForeColor = Color.FromArgb(0, 192, 192);
            lblExpira.Location = new Point(89, 183);
            lblExpira.Name = "lblExpira";
            lblExpira.Size = new Size(126, 15);
            lblExpira.TabIndex = 5;
            lblExpira.Text = "Duración Desconocida";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(14, 58);
            label1.Name = "label1";
            label1.Size = new Size(108, 15);
            label1.TabIndex = 6;
            label1.Text = "Codigo de licencia:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(14, 127);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 7;
            label2.Text = "Estado:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(14, 155);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 8;
            label3.Text = "Cliente:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.White;
            label4.Location = new Point(14, 183);
            label4.Name = "label4";
            label4.Size = new Size(41, 15);
            label4.TabIndex = 9;
            label4.Text = "Expira:";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 32, 40);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(lblTitulo);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(txtLicencia);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(btnValidar);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(lblResultado);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblCliente);
            panel1.Controls.Add(lblExpira);
            panel1.Location = new Point(12, 13);
            panel1.Name = "panel1";
            panel1.Size = new Size(255, 214);
            panel1.TabIndex = 10;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(48, 43);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 51;
            pictureBox1.TabStop = false;
            pictureBox1.Visible = false;
            // 
            // V_Validador_Licencia
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            ClientSize = new Size(279, 238);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "V_Validador_Licencia";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Activación de Sistema";
            Load += V_Validador_Licencia_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private TextBox txtLicencia;
        private Button btnValidar;
        private Label lblResultado;
        private Label lblCliente;
        private Label lblExpira;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private PictureBox pictureBox1;
    }
}