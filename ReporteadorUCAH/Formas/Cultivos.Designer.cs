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
            label4 = new Label();
            button1 = new Button();
            txtboxCONS = new TextBox();
            txtboxCultivo = new TextBox();
            txtboxNombreCultivo = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
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
            panel1.Size = new Size(671, 213);
            panel1.TabIndex = 5;
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
            ClientSize = new Size(677, 333);
            Controls.Add(panel1);
            Margin = new Padding(3);
            Name = "Cultivos";
            Padding = new Padding(3);
            Text = "Cultivos";
            Controls.SetChildIndex(panel1, 0);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
    }
}