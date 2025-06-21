namespace Monitux_POS.Ventanas
{
    partial class V_Vista_Ampliada
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Vista_Ampliada));
            pictureBox1 = new PictureBox();
            label8 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(28, 70);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(608, 343);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label8
            // 
            label8.BackColor = SystemColors.ActiveCaptionText;
            label8.Dock = DockStyle.Top;
            label8.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.White;
            label8.Location = new Point(0, 0);
            label8.Name = "label8";
            label8.Size = new Size(672, 46);
            label8.TabIndex = 38;
            label8.Text = "Codigo";
            label8.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.BackColor = Color.Black;
            label1.Dock = DockStyle.Bottom;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(0, 442);
            label1.Name = "label1";
            label1.Size = new Size(672, 46);
            label1.TabIndex = 39;
            label1.Text = "Descripcion";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // V_Vista_Ampliada
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 32, 40);
            ClientSize = new Size(672, 488);
            Controls.Add(label1);
            Controls.Add(label8);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "V_Vista_Ampliada";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "V_Vista_Ampliada";
            Load += V_Vista_Ampliada_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label8;
        private Label label1;
    }
}