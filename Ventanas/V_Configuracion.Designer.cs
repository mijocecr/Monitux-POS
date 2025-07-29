namespace Monitux_POS.Ventanas
{
    partial class V_Configuracion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V_Configuracion));
            label6 = new Label();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            folderBrowserDialog1 = new FolderBrowserDialog();
            label3 = new Label();
            button3 = new Button();
            button4 = new Button();
            label4 = new Label();
            SuspendLayout();
            // 
            // label6
            // 
            label6.BackColor = Color.FromArgb(11, 8, 20);
            label6.Dock = DockStyle.Top;
            label6.Font = new Font("Segoe UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(0, 0);
            label6.Name = "label6";
            label6.Size = new Size(881, 37);
            label6.TabIndex = 80;
            label6.Text = "Base de Datos";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label1.Location = new Point(56, 303);
            label1.Name = "label1";
            label1.Size = new Size(113, 19);
            label1.TabIndex = 81;
            label1.Text = "Ruta destino BD:";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label2.Location = new Point(45, 257);
            label2.Name = "label2";
            label2.Size = new Size(124, 19);
            label2.TabIndex = 82;
            label2.Text = "[📂] Archivo SQL:";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // button1
            // 
            button1.Location = new Point(636, 252);
            button1.Name = "button1";
            button1.Size = new Size(75, 24);
            button1.TabIndex = 84;
            button1.Text = "Cargar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(636, 298);
            button2.Name = "button2";
            button2.Size = new Size(75, 24);
            button2.TabIndex = 85;
            button2.Text = "Escoger";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(175, 254);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(455, 23);
            textBox1.TabIndex = 86;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(175, 299);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(455, 23);
            textBox2.TabIndex = 87;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Red;
            label3.Location = new Point(45, 65);
            label3.Name = "label3";
            label3.Size = new Size(599, 128);
            label3.TabIndex = 88;
            label3.Text = resources.GetString("label3.Text");
            // 
            // button3
            // 
            button3.Location = new Point(268, 366);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 89;
            button3.Text = "Importar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(175, 366);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 90;
            button4.Text = "Exportar";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Blue;
            label4.Location = new Point(412, 394);
            label4.Name = "label4";
            label4.Size = new Size(218, 30);
            label4.TabIndex = 91;
            label4.Text = "Motor de Datos: [SQLITE 3]";
            // 
            // V_Configuracion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(881, 537);
            Controls.Add(label4);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label6);
            Name = "V_Configuracion";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "V_Configuracion";
            Load += V_Configuracion_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label6;
        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
        private TextBox textBox1;
        private TextBox textBox2;
        private OpenFileDialog openFileDialog1;
        private FolderBrowserDialog folderBrowserDialog1;
        private Label label3;
        private Button button3;
        private Button button4;
        private Label label4;
    }
}