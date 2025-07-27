namespace Monitux_POS.Ventanas
{
    partial class V_Dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Dashboard));
            flowLayoutPanel1 = new FlowLayoutPanel();
            dateTimePicker1 = new DateTimePicker();
            label6 = new Label();
            dataGridViewClientesDestacados = new DataGridView();
            panelClientes = new Panel();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            listBox1 = new ListBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewClientesDestacados).BeginInit();
            panelClientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Location = new Point(23, 91);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(441, 317);
            flowLayoutPanel1.TabIndex = 0;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(63, 58);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(109, 23);
            dateTimePicker1.TabIndex = 1;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // label6
            // 
            label6.BackColor = Color.FromArgb(11, 8, 20);
            label6.Dock = DockStyle.Top;
            label6.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(0, 0);
            label6.Name = "label6";
            label6.Size = new Size(800, 37);
            label6.TabIndex = 53;
            label6.Text = "Dashboard";
            label6.TextAlign = ContentAlignment.TopCenter;
            label6.Click += label6_Click;
            // 
            // dataGridViewClientesDestacados
            // 
            dataGridViewClientesDestacados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewClientesDestacados.Location = new Point(7, 3);
            dataGridViewClientesDestacados.Name = "dataGridViewClientesDestacados";
            dataGridViewClientesDestacados.Size = new Size(271, 97);
            dataGridViewClientesDestacados.TabIndex = 54;
            dataGridViewClientesDestacados.Visible = false;
            // 
            // panelClientes
            // 
            panelClientes.Controls.Add(dataGridViewClientesDestacados);
            panelClientes.Location = new Point(487, 123);
            panelClientes.Name = "panelClientes";
            panelClientes.Size = new Size(281, 285);
            panelClientes.TabIndex = 55;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(494, 98);
            label1.Name = "label1";
            label1.Size = new Size(240, 15);
            label1.TabIndex = 56;
            label1.Text = "Mejores clientes segun los registros del mes.";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(23, 52);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 36);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 60;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(35, 32, 45);
            listBox1.ForeColor = Color.White;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(23, 455);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(742, 109);
            listBox1.TabIndex = 61;
            // 
            // V_Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 32, 45);
            ClientSize = new Size(800, 576);
            Controls.Add(listBox1);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(panelClientes);
            Controls.Add(label6);
            Controls.Add(dateTimePicker1);
            Controls.Add(flowLayoutPanel1);
            Name = "V_Dashboard";
            Text = "V_Dashboard";
            Load += V_Dashboard_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewClientesDestacados).EndInit();
            panelClientes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private DateTimePicker dateTimePicker1;
        private Label label6;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private DataGridView dataGridViewClientesDestacados;
        private Panel panelClientes;
        private Label label1;
        private PictureBox pictureBox1;
        private ListBox listBox1;
    }
}