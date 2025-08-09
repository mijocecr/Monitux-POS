namespace Monitux_POS.Ventanas
{
    partial class ProgressForm
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
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressBar;

        private void InitializeComponent()
        {
            lblStatus = new Label();
            progressBar = new ProgressBar();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 20);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(77, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Preparando...";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 50);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(360, 23);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 1;
            // 
            // ProgressForm
            // 
            ClientSize = new Size(384, 100);
            Controls.Add(lblStatus);
            Controls.Add(progressBar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "ProgressForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Instalación de SQL Server";
            Load += ProgressForm_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

    }
}