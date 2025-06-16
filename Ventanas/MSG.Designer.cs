namespace Monitux_POS.Ventanas
{
    partial class MSG
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
            panel1 = new Panel();
            lbl_Mensaje = new Label();
            btn_Aceptar = new Button();
            btn_Cancelar = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.BackColor = Color.FromArgb(35, 32, 45);
            panel1.Controls.Add(lbl_Mensaje);
            panel1.Location = new Point(6, 10);
            panel1.Name = "panel1";
            panel1.Size = new Size(270, 92);
            panel1.TabIndex = 0;
            // 
            // lbl_Mensaje
            // 
            lbl_Mensaje.ForeColor = Color.White;
            lbl_Mensaje.Location = new Point(6, 9);
            lbl_Mensaje.Name = "lbl_Mensaje";
            lbl_Mensaje.Size = new Size(261, 73);
            lbl_Mensaje.TabIndex = 0;
            lbl_Mensaje.Text = "Mensaje";
            lbl_Mensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn_Aceptar
            // 
            btn_Aceptar.DialogResult = DialogResult.Yes;
            btn_Aceptar.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            btn_Aceptar.FlatStyle = FlatStyle.Flat;
            btn_Aceptar.ForeColor = Color.White;
            btn_Aceptar.Location = new Point(201, 111);
            btn_Aceptar.Name = "btn_Aceptar";
            btn_Aceptar.Size = new Size(75, 23);
            btn_Aceptar.TabIndex = 1;
            btn_Aceptar.Text = "Aceptar";
            btn_Aceptar.UseVisualStyleBackColor = true;
            btn_Aceptar.Click += button1_Click;
            // 
            // btn_Cancelar
            // 
            btn_Cancelar.DialogResult = DialogResult.Cancel;
            btn_Cancelar.FlatAppearance.MouseOverBackColor = Color.FromArgb(87, 87, 163);
            btn_Cancelar.FlatStyle = FlatStyle.Flat;
            btn_Cancelar.ForeColor = Color.White;
            btn_Cancelar.Location = new Point(6, 111);
            btn_Cancelar.Name = "btn_Cancelar";
            btn_Cancelar.Size = new Size(75, 23);
            btn_Cancelar.TabIndex = 2;
            btn_Cancelar.Text = "Cancelar";
            btn_Cancelar.UseVisualStyleBackColor = true;
            btn_Cancelar.Click += button2_Click;
            // 
            // MSG
            // 
            AcceptButton = btn_Aceptar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 8, 20);
            CancelButton = btn_Cancelar;
            ClientSize = new Size(282, 139);
            ControlBox = false;
            Controls.Add(btn_Cancelar);
            Controls.Add(btn_Aceptar);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MSG";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "MSG";
            Load += MSG_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btn_Aceptar;
        private Button btn_Cancelar;
        public Label lbl_Mensaje;
    }
}