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
            label5 = new Label();
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
            panel1.Location = new Point(3, 51);
            panel1.Name = "panel1";
            panel1.Size = new Size(671, 215);
            panel1.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(label9);
            panel2.Controls.Add(txtID);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(txtCONS);
            panel2.Controls.Add(txtCultivoTipo);
            panel2.Controls.Add(txtNombreCultivo);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label8);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(671, 215);
            panel2.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(30, 18);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(83, 25);
            label9.TabIndex = 11;
            label9.Text = "# Cultivo";
            // 
            // txtID
            // 
            txtID.BackColor = SystemColors.Info;
            txtID.Cursor = Cursors.No;
            txtID.Location = new Point(111, 12);
            txtID.Margin = new Padding(4, 5, 4, 5);
            txtID.Name = "txtID";
            txtID.PlaceholderText = "0";
            txtID.ReadOnly = true;
            txtID.Size = new Size(101, 31);
            txtID.TabIndex = 10;
            txtID.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(597, 30);
            label5.Name = "label5";
            label5.Size = new Size(63, 25);
            label5.TabIndex = 9;
            label5.Text = "Buscar";
            // 
            // button2
            // 
            button2.BackgroundImage = (Image)resources.GetObject("button2.BackgroundImage");
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.Location = new Point(544, 18);
            button2.Name = "button2";
            button2.Size = new Size(47, 52);
            button2.TabIndex = 6;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button1_Click;
            // 
            // txtCONS
            // 
            txtCONS.BackColor = SystemColors.Info;
            txtCONS.Location = new Point(366, 98);
            txtCONS.Name = "txtCONS";
            txtCONS.Size = new Size(168, 31);
            txtCONS.TabIndex = 5;
            // 
            // txtCultivoTipo
            // 
            txtCultivoTipo.BackColor = SystemColors.Info;
            txtCultivoTipo.Location = new Point(111, 98);
            txtCultivoTipo.Name = "txtCultivoTipo";
            txtCultivoTipo.Size = new Size(173, 31);
            txtCultivoTipo.TabIndex = 4;
            // 
            // txtNombreCultivo
            // 
            txtNombreCultivo.BackColor = SystemColors.Info;
            txtNombreCultivo.Location = new Point(111, 52);
            txtNombreCultivo.Name = "txtNombreCultivo";
            txtNombreCultivo.Size = new Size(423, 31);
            txtNombreCultivo.TabIndex = 3;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(300, 103);
            label6.Name = "label6";
            label6.Size = new Size(60, 25);
            label6.TabIndex = 2;
            label6.Text = "CONS";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(39, 103);
            label7.Name = "label7";
            label7.Size = new Size(67, 25);
            label7.TabIndex = 1;
            label7.Text = "Cultivo";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(29, 58);
            label8.Name = "label8";
            label8.Size = new Size(78, 25);
            label8.TabIndex = 0;
            label8.Text = "Nombre";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(76, 134);
            dateTimePicker1.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(102, 31);
            dateTimePicker1.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(597, 30);
            label4.Name = "label4";
            label4.Size = new Size(63, 25);
            label4.TabIndex = 9;
            label4.Text = "Buscar";
            // 
            // button1
            // 
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Location = new Point(544, 18);
            button1.Name = "button1";
            button1.Size = new Size(47, 52);
            button1.TabIndex = 6;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtboxCONS
            // 
            txtboxCONS.BackColor = SystemColors.Info;
            txtboxCONS.Location = new Point(356, 73);
            txtboxCONS.Name = "txtboxCONS";
            txtboxCONS.Size = new Size(168, 31);
            txtboxCONS.TabIndex = 5;
            // 
            // txtboxCultivo
            // 
            txtboxCultivo.BackColor = SystemColors.Info;
            txtboxCultivo.Location = new Point(101, 73);
            txtboxCultivo.Name = "txtboxCultivo";
            txtboxCultivo.Size = new Size(173, 31);
            txtboxCultivo.TabIndex = 4;
            // 
            // txtboxNombreCultivo
            // 
            txtboxNombreCultivo.BackColor = SystemColors.Info;
            txtboxNombreCultivo.Location = new Point(101, 27);
            txtboxNombreCultivo.Name = "txtboxNombreCultivo";
            txtboxNombreCultivo.Size = new Size(423, 31);
            txtboxNombreCultivo.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(290, 78);
            label3.Name = "label3";
            label3.Size = new Size(60, 25);
            label3.TabIndex = 2;
            label3.Text = "CONS";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 78);
            label2.Name = "label2";
            label2.Size = new Size(67, 25);
            label2.TabIndex = 1;
            label2.Text = "Cultivo";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 33);
            label1.Name = "label1";
            label1.Size = new Size(78, 25);
            label1.TabIndex = 0;
            label1.Text = "Nombre";
            // 
            // Cultivos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(677, 335);
            Controls.Add(panel1);
            Margin = new Padding(3);
            Name = "Cultivos";
            Padding = new Padding(3);
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