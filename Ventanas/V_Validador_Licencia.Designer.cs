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
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(28, 24);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(123, 15);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Activacion de Sistema";
            // 
            // txtLicencia
            // 
            txtLicencia.Location = new Point(143, 64);
            txtLicencia.Name = "txtLicencia";
            txtLicencia.Size = new Size(100, 23);
            txtLicencia.TabIndex = 1;
            // 
            // btnValidar
            // 
            btnValidar.Location = new Point(29, 98);
            btnValidar.Name = "btnValidar";
            btnValidar.Size = new Size(75, 23);
            btnValidar.TabIndex = 2;
            btnValidar.Text = "button1";
            btnValidar.UseVisualStyleBackColor = true;
            btnValidar.Click += btnValidar_Click;
            // 
            // lblResultado
            // 
            lblResultado.AutoSize = true;
            lblResultado.Location = new Point(155, 134);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new Size(42, 15);
            lblResultado.TabIndex = 3;
            lblResultado.Text = "Estado";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(153, 164);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(44, 15);
            lblCliente.TabIndex = 4;
            lblCliente.Text = "Cliente";
            // 
            // lblExpira
            // 
            lblExpira.AutoSize = true;
            lblExpira.Location = new Point(159, 192);
            lblExpira.Name = "lblExpira";
            lblExpira.Size = new Size(38, 15);
            lblExpira.TabIndex = 5;
            lblExpira.Text = "Expira";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 67);
            label1.Name = "label1";
            label1.Size = new Size(108, 15);
            label1.TabIndex = 6;
            label1.Text = "Codigo de licencia:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 136);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 7;
            label2.Text = "Estado:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(29, 164);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 8;
            label3.Text = "Cliente:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(29, 192);
            label4.Name = "label4";
            label4.Size = new Size(41, 15);
            label4.TabIndex = 9;
            label4.Text = "Expira:";
            // 
            // V_Validador_Licencia
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblExpira);
            Controls.Add(lblCliente);
            Controls.Add(lblResultado);
            Controls.Add(btnValidar);
            Controls.Add(txtLicencia);
            Controls.Add(lblTitulo);
            Name = "V_Validador_Licencia";
            Text = "V_Validador_Licencia";
            Load += V_Validador_Licencia_Load;
            ResumeLayout(false);
            PerformLayout();
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
    }
}