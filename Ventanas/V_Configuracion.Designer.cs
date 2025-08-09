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
            panel1 = new Panel();
            pictureBox3 = new PictureBox();
            panel2 = new Panel();
            pictureBox2 = new PictureBox();
            textBox3 = new TextBox();
            button5 = new Button();
            label5 = new Label();
            button6 = new Button();
            label7 = new Label();
            label8 = new Label();
            button7 = new Button();
            button8 = new Button();
            textBox4 = new TextBox();
            panel3 = new Panel();
            pictureBox1 = new PictureBox();
            textBox5 = new TextBox();
            button9 = new Button();
            label9 = new Label();
            button10 = new Button();
            label10 = new Label();
            label11 = new Label();
            button11 = new Button();
            button12 = new Button();
            textBox6 = new TextBox();
            label12 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            label6.Size = new Size(806, 37);
            label6.TabIndex = 80;
            label6.Text = "Base de Datos";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(192, 0, 192);
            label1.Location = new Point(110, 49);
            label1.Name = "label1";
            label1.Size = new Size(116, 19);
            label1.TabIndex = 81;
            label1.Text = "Ruta destino .DB:";
            label1.TextAlign = ContentAlignment.TopCenter;
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(0, 192, 0);
            label2.Location = new Point(107, 19);
            label2.Name = "label2";
            label2.Size = new Size(119, 19);
            label2.TabIndex = 82;
            label2.Text = "[📂] Archivo .DB:";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // button1
            // 
            button1.ForeColor = Color.FromArgb(0, 192, 0);
            button1.Location = new Point(629, 15);
            button1.Name = "button1";
            button1.Size = new Size(75, 24);
            button1.TabIndex = 84;
            button1.Text = "Cargar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.ForeColor = Color.FromArgb(192, 0, 192);
            button2.Location = new Point(629, 49);
            button2.Name = "button2";
            button2.Size = new Size(75, 24);
            button2.TabIndex = 85;
            button2.Text = "Escoger";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(228, 16);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(396, 23);
            textBox1.TabIndex = 86;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(228, 49);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(396, 23);
            textBox2.TabIndex = 87;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // label3
            // 
            label3.Dock = DockStyle.Top;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.MidnightBlue;
            label3.Location = new Point(0, 37);
            label3.Name = "label3";
            label3.Size = new Size(806, 128);
            label3.TabIndex = 88;
            label3.Text = resources.GetString("label3.Text");
            // 
            // button3
            // 
            button3.ForeColor = Color.Red;
            button3.Location = new Point(710, 16);
            button3.Name = "button3";
            button3.Size = new Size(75, 24);
            button3.TabIndex = 89;
            button3.Text = "Importar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.ForeColor = Color.FromArgb(192, 0, 192);
            button4.Location = new Point(710, 49);
            button4.Name = "button4";
            button4.Size = new Size(75, 24);
            button4.TabIndex = 90;
            button4.Text = "Exportar";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Gray;
            label4.Location = new Point(426, 75);
            label4.Name = "label4";
            label4.Size = new Size(198, 30);
            label4.TabIndex = 91;
            label4.Text = "Motor de Datos: [SQLITE 3]";
            // 
            // panel1
            // 
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(textBox2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 165);
            panel1.Name = "panel1";
            panel1.Size = new Size(806, 117);
            panel1.TabIndex = 92;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(17, 14);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(84, 81);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 92;
            pictureBox3.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(textBox3);
            panel2.Controls.Add(button5);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(button6);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(button7);
            panel2.Controls.Add(button8);
            panel2.Controls.Add(textBox4);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 282);
            panel2.Name = "panel2";
            panel2.Size = new Size(806, 117);
            panel2.TabIndex = 93;
            // 
            // pictureBox2
            // 
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(17, 14);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(84, 81);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 92;
            pictureBox2.TabStop = false;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(240, 16);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(384, 23);
            textBox3.TabIndex = 86;
            // 
            // button5
            // 
            button5.ForeColor = Color.FromArgb(192, 0, 192);
            button5.Location = new Point(708, 50);
            button5.Name = "button5";
            button5.Size = new Size(75, 24);
            button5.TabIndex = 90;
            button5.Text = "Exportar";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Gray;
            label5.Location = new Point(437, 75);
            label5.Name = "label5";
            label5.Size = new Size(187, 30);
            label5.TabIndex = 91;
            label5.Text = "Motor de Datos: [MYSQL]";
            // 
            // button6
            // 
            button6.ForeColor = Color.Red;
            button6.Location = new Point(708, 14);
            button6.Name = "button6";
            button6.Size = new Size(75, 24);
            button6.TabIndex = 89;
            button6.Text = "Importar";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(192, 0, 192);
            label7.Location = new Point(110, 49);
            label7.Name = "label7";
            label7.Size = new Size(124, 19);
            label7.TabIndex = 81;
            label7.Text = "Ruta destino .SQL:";
            label7.TextAlign = ContentAlignment.TopCenter;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label8.ForeColor = Color.FromArgb(0, 192, 0);
            label8.Location = new Point(107, 19);
            label8.Name = "label8";
            label8.Size = new Size(127, 19);
            label8.TabIndex = 82;
            label8.Text = "[📂] Archivo .SQL:";
            label8.TextAlign = ContentAlignment.TopCenter;
            // 
            // button7
            // 
            button7.ForeColor = Color.FromArgb(0, 192, 0);
            button7.Location = new Point(630, 14);
            button7.Name = "button7";
            button7.Size = new Size(75, 24);
            button7.TabIndex = 84;
            button7.Text = "Cargar";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.ForeColor = Color.FromArgb(192, 0, 192);
            button8.Location = new Point(630, 49);
            button8.Name = "button8";
            button8.Size = new Size(75, 24);
            button8.TabIndex = 85;
            button8.Text = "Escoger";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(240, 49);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(384, 23);
            textBox4.TabIndex = 87;
            // 
            // panel3
            // 
            panel3.Controls.Add(pictureBox1);
            panel3.Controls.Add(textBox5);
            panel3.Controls.Add(button9);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(button10);
            panel3.Controls.Add(label10);
            panel3.Controls.Add(label11);
            panel3.Controls.Add(button11);
            panel3.Controls.Add(button12);
            panel3.Controls.Add(textBox6);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 399);
            panel3.Name = "panel3";
            panel3.Size = new Size(806, 117);
            panel3.TabIndex = 94;
            // 
            // pictureBox1
            // 
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Image = Properties.Resources.microsoft_sql_server_logo_png_seeklogo_298266;
            pictureBox1.Location = new Point(17, 14);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(84, 81);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 92;
            pictureBox1.TabStop = false;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(240, 16);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(384, 23);
            textBox5.TabIndex = 86;
            // 
            // button9
            // 
            button9.ForeColor = Color.FromArgb(192, 0, 192);
            button9.Location = new Point(707, 50);
            button9.Name = "button9";
            button9.Size = new Size(75, 24);
            button9.TabIndex = 90;
            button9.Text = "Exportar";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // label9
            // 
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.Gray;
            label9.Location = new Point(405, 75);
            label9.Name = "label9";
            label9.Size = new Size(219, 30);
            label9.TabIndex = 91;
            label9.Text = "Motor de Datos: [SQLSERVER]";
            // 
            // button10
            // 
            button10.ForeColor = Color.Red;
            button10.Location = new Point(707, 15);
            button10.Name = "button10";
            button10.Size = new Size(75, 24);
            button10.TabIndex = 89;
            button10.Text = "Importar";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label10.ForeColor = Color.FromArgb(192, 0, 192);
            label10.Location = new Point(110, 49);
            label10.Name = "label10";
            label10.Size = new Size(124, 19);
            label10.TabIndex = 81;
            label10.Text = "Ruta destino .SQL:";
            label10.TextAlign = ContentAlignment.TopCenter;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            label11.ForeColor = Color.FromArgb(0, 192, 0);
            label11.Location = new Point(107, 19);
            label11.Name = "label11";
            label11.Size = new Size(127, 19);
            label11.TabIndex = 82;
            label11.Text = "[📂] Archivo .SQL:";
            label11.TextAlign = ContentAlignment.TopCenter;
            // 
            // button11
            // 
            button11.ForeColor = Color.FromArgb(0, 192, 0);
            button11.Location = new Point(629, 14);
            button11.Name = "button11";
            button11.Size = new Size(75, 24);
            button11.TabIndex = 84;
            button11.Text = "Cargar";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // button12
            // 
            button12.ForeColor = Color.FromArgb(192, 0, 192);
            button12.Location = new Point(629, 49);
            button12.Name = "button12";
            button12.Size = new Size(75, 24);
            button12.TabIndex = 85;
            button12.Text = "Escoger";
            button12.UseVisualStyleBackColor = true;
            button12.Click += button12_Click;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(240, 49);
            textBox6.Name = "textBox6";
            textBox6.ReadOnly = true;
            textBox6.Size = new Size(384, 23);
            textBox6.TabIndex = 87;
            // 
            // label12
            // 
            label12.Dock = DockStyle.Top;
            label12.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.MidnightBlue;
            label12.Location = new Point(0, 516);
            label12.Name = "label12";
            label12.Size = new Size(806, 48);
            label12.TabIndex = 95;
            label12.Text = "Atención: Se recomienda crear una carpeta llama \"Respaldo\" en [Disco Local C:]  algunos servidores tienen conflictos de permisos para escribir sus respaldos.";
            // 
            // V_Configuracion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(806, 701);
            Controls.Add(label12);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(label3);
            Controls.Add(label6);
            Name = "V_Configuracion";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "V_Configuracion";
            Load += V_Configuracion_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
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
        private Panel panel1;
        private PictureBox pictureBox3;
        private Panel panel2;
        private TextBox textBox3;
        private Button button5;
        private Label label5;
        private Button button6;
        private Label label7;
        private Label label8;
        private Button button7;
        private Button button8;
        private TextBox textBox4;
        private PictureBox pictureBox2;
        private Panel panel3;
        private TextBox textBox5;
        private Button button9;
        private Label label9;
        private Button button10;
        private Label label10;
        private Label label11;
        private Button button11;
        private Button button12;
        private TextBox textBox6;
        private PictureBox pictureBox1;
        private Label label12;
    }
}