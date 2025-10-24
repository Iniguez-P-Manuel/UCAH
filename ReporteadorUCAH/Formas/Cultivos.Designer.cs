namespace ReporteadorUCAH.Formas
{
    partial class Cultivos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cultivos));
            panel1 = new Panel();
            panel2 = new Panel();
            label9 = new Label();
            txtID = new TextBox();
            button2 = new Button();
            txtCONS = new TextBox();
            txtCultivoTipo = new TextBox();
            txtNombreCultivo = new TextBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label4 = new Label();
            button1 = new Button();
            txtboxCONS = new TextBox();
            txtboxCultivo = new TextBox();
            txtboxNombreCultivo = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(dateTimePicker1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(txtboxCONS);
            panel1.Controls.Add(txtboxCultivo);
            panel1.Controls.Add(txtboxNombreCultivo);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(2, 31);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(470, 127);
            panel1.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(label9);
            panel2.Controls.Add(txtID);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(txtCONS);
            panel2.Controls.Add(txtCultivoTipo);
            panel2.Controls.Add(txtNombreCultivo);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label8);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(470, 127);
            panel2.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(21, 14);
            label9.Name = "label9";
            label9.Size = new Size(55, 15);
            label9.TabIndex = 11;
            label9.Text = "# Cultivo";
            // 
            // txtID
            // 
            txtID.BackColor = SystemColors.Info;
            txtID.Cursor = Cursors.No;
            txtID.Location = new Point(78, 10);
            txtID.Name = "txtID";
            txtID.PlaceholderText = "0";
            txtID.ReadOnly = true;
            txtID.Size = new Size(72, 23);
            txtID.TabIndex = 10;
            txtID.TextAlign = HorizontalAlignment.Center;
            // 
            // button2
            // 
            button2.BackgroundImage = Properties.Resources.icons8_búsqueda_32;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.Location = new Point(155, 5);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(34, 32);
            button2.TabIndex = 6;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button1_Click;
            // 
            // txtCONS
            // 
            txtCONS.BackColor = SystemColors.Info;
            txtCONS.Location = new Point(256, 80);
            txtCONS.Margin = new Padding(2);
            txtCONS.Name = "txtCONS";
            txtCONS.Size = new Size(119, 23);
            txtCONS.TabIndex = 5;
            // 
            // txtCultivoTipo
            // 
            txtCultivoTipo.BackColor = SystemColors.Info;
            txtCultivoTipo.Location = new Point(78, 80);
            txtCultivoTipo.Margin = new Padding(2);
            txtCultivoTipo.Name = "txtCultivoTipo";
            txtCultivoTipo.Size = new Size(122, 23);
            txtCultivoTipo.TabIndex = 4;
            // 
            // txtNombreCultivo
            // 
            txtNombreCultivo.BackColor = SystemColors.Info;
            txtNombreCultivo.Location = new Point(78, 52);
            txtNombreCultivo.Margin = new Padding(2);
            txtNombreCultivo.Name = "txtNombreCultivo";
            txtNombreCultivo.Size = new Size(297, 23);
            txtNombreCultivo.TabIndex = 3;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(210, 83);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 2;
            label6.Text = "CONS";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(27, 83);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(45, 15);
            label7.TabIndex = 1;
            label7.Text = "Cultivo";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(20, 56);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(51, 15);
            label8.TabIndex = 0;
            label8.Text = "Nombre";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(53, 80);
            dateTimePicker1.Margin = new Padding(2);
            dateTimePicker1.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(73, 23);
            dateTimePicker1.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(418, 18);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 9;
            label4.Text = "Buscar";
            // 
            // button1
            // 
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Location = new Point(381, 11);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(33, 31);
            button1.TabIndex = 6;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtboxCONS
            // 
            txtboxCONS.BackColor = SystemColors.Info;
            txtboxCONS.Location = new Point(249, 44);
            txtboxCONS.Margin = new Padding(2);
            txtboxCONS.Name = "txtboxCONS";
            txtboxCONS.Size = new Size(119, 23);
            txtboxCONS.TabIndex = 5;
            // 
            // txtboxCultivo
            // 
            txtboxCultivo.BackColor = SystemColors.Info;
            txtboxCultivo.Location = new Point(71, 44);
            txtboxCultivo.Margin = new Padding(2);
            txtboxCultivo.Name = "txtboxCultivo";
            txtboxCultivo.Size = new Size(122, 23);
            txtboxCultivo.TabIndex = 4;
            // 
            // txtboxNombreCultivo
            // 
            txtboxNombreCultivo.BackColor = SystemColors.Info;
            txtboxNombreCultivo.Location = new Point(71, 16);
            txtboxNombreCultivo.Margin = new Padding(2);
            txtboxNombreCultivo.Name = "txtboxNombreCultivo";
            txtboxNombreCultivo.Size = new Size(297, 23);
            txtboxNombreCultivo.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(203, 47);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 2;
            label3.Text = "CONS";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 47);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 1;
            label2.Text = "Cultivo";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 20);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 0;
            label1.Text = "Nombre";
            // 
            // Cultivos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(474, 201);
            Controls.Add(panel1);
            Margin = new Padding(2);
            Name = "Cultivos";
            Padding = new Padding(2);
            Text = "Cultivos";
            Controls.SetChildIndex(panel1, 0);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private TextBox txtboxCONS;
        private TextBox txtboxCultivo;
        private TextBox txtboxNombreCultivo;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label4;
        private DateTimePicker dateTimePicker1;
        private Panel panel2;
        private Label label5;
        private Button button2;
        private TextBox txtCONS;
        private TextBox txtCultivoTipo;
        private TextBox txtNombreCultivo;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox txtID;
    }
}