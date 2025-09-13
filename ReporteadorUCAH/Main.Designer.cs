namespace GestionPracticas
{
    partial class Main
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            button7 = new Button();
            button2 = new Button();
            btnCerrar = new Button();
            Titulo = new Label();
            PanelVentana = new Panel();
            button1 = new Button();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(55, 71, 79);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(175, 612);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(38, 50, 56);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(175, 60);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(69, 90, 100);
            panel3.Controls.Add(button7);
            panel3.Controls.Add(button2);
            panel3.Controls.Add(btnCerrar);
            panel3.Controls.Add(Titulo);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(175, 0);
            panel3.Margin = new Padding(2);
            panel3.Name = "panel3";
            panel3.Size = new Size(695, 60);
            panel3.TabIndex = 1;
            panel3.MouseMove += panel3_MouseMove;
            // 
            // button7
            // 
            button7.Dock = DockStyle.Left;
            button7.FlatAppearance.BorderSize = 0;
            button7.FlatStyle = FlatStyle.Flat;
            button7.Location = new Point(0, 0);
            button7.Margin = new Padding(2);
            button7.Name = "button7";
            button7.Size = new Size(65, 60);
            button7.TabIndex = 3;
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Right;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(565, 0);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(65, 60);
            button2.TabIndex = 2;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // btnCerrar
            // 
            btnCerrar.Dock = DockStyle.Right;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Location = new Point(630, 0);
            btnCerrar.Margin = new Padding(2);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(65, 60);
            btnCerrar.TabIndex = 1;
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += button1_Click;
            // 
            // Titulo
            // 
            Titulo.AutoSize = true;
            Titulo.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Titulo.ForeColor = SystemColors.ButtonHighlight;
            Titulo.Location = new Point(5, 12);
            Titulo.Margin = new Padding(2, 0, 2, 0);
            Titulo.MinimumSize = new Size(665, 30);
            Titulo.Name = "Titulo";
            Titulo.Size = new Size(665, 30);
            Titulo.TabIndex = 0;
            Titulo.Text = "Home";
            Titulo.TextAlign = ContentAlignment.MiddleCenter;
            Titulo.MouseMove += Titulo_MouseMove;
            // 
            // PanelVentana
            // 
            PanelVentana.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            PanelVentana.BackgroundImageLayout = ImageLayout.Center;
            PanelVentana.Dock = DockStyle.Fill;
            PanelVentana.ForeColor = SystemColors.ControlText;
            PanelVentana.Location = new Point(175, 60);
            PanelVentana.Margin = new Padding(2);
            PanelVentana.Name = "PanelVentana";
            PanelVentana.Size = new Size(695, 552);
            PanelVentana.TabIndex = 2;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Right;
            button1.Location = new Point(668, 0);
            button1.Name = "button1";
            button1.Size = new Size(75, 64);
            button1.TabIndex = 1;
            button1.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 612);
            Controls.Add(PanelVentana);
            Controls.Add(panel3);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "Main";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel PanelVentana;
        private System.Windows.Forms.Label Titulo;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button7;
    }
}

