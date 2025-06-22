namespace Monitux_POS.Ventanas
{
    partial class IPB
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
            btn_Cancelar = new Button();
            btn_Aceptar = new Button();
            lbl_Mensaje = new Label();
            panel1 = new Panel();
            textBox1 = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btn_Cancelar
            // 
            btn_Cancelar.DialogResult = DialogResult.Cancel;
            btn_Cancelar.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            btn_Cancelar.FlatStyle = FlatStyle.Flat;
            btn_Cancelar.ForeColor = Color.White;
            btn_Cancelar.Location = new Point(7, 107);
            btn_Cancelar.Name = "btn_Cancelar";
            btn_Cancelar.Size = new Size(75, 23);
            btn_Cancelar.TabIndex = 5;
            btn_Cancelar.Text = "Cancelar";
            btn_Cancelar.UseVisualStyleBackColor = true;
            btn_Cancelar.Click += btn_Cancelar_Click;
            // 
            // btn_Aceptar
            // 
            btn_Aceptar.DialogResult = DialogResult.Yes;
            btn_Aceptar.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            btn_Aceptar.FlatStyle = FlatStyle.Flat;
            btn_Aceptar.ForeColor = Color.White;
            btn_Aceptar.Location = new Point(203, 107);
            btn_Aceptar.Name = "btn_Aceptar";
            btn_Aceptar.Size = new Size(75, 23);
            btn_Aceptar.TabIndex = 4;
            btn_Aceptar.Text = "Aceptar";
            btn_Aceptar.UseVisualStyleBackColor = true;
            btn_Aceptar.Click += btn_Aceptar_Click;
            // 
            // lbl_Mensaje
            // 
            lbl_Mensaje.ForeColor = Color.White;
            lbl_Mensaje.Location = new Point(4, 1);
            lbl_Mensaje.Name = "lbl_Mensaje";
            lbl_Mensaje.Size = new Size(261, 38);
            lbl_Mensaje.TabIndex = 0;
            lbl_Mensaje.Text = "Mensaje";
            lbl_Mensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.BackColor = Color.FromArgb(35, 32, 45);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(lbl_Mensaje);
            panel1.Location = new Point(7, 7);
            panel1.Name = "panel1";
            panel1.Size = new Size(271, 92);
            panel1.TabIndex = 3;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(4, 54);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(261, 22);
            textBox1.TabIndex = 1;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // IPB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            ClientSize = new Size(287, 135);
            ControlBox = false;
            Controls.Add(btn_Cancelar);
            Controls.Add(btn_Aceptar);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "IPB";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "IPB";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Cancelar;
        private Button btn_Aceptar;
        public Label lbl_Mensaje;
        private Panel panel1;
        private TextBox textBox1;
    }
}