namespace ReporteadorUCAH
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
            btnNotasCargo = new Button();
            panel2 = new Panel();
            panel3 = new Panel();
            button3 = new Button();
            button2 = new Button();
            btnCerrar = new Button();
            PanelVentana = new Panel();
            button1 = new Button();
            btnClientes = new Button();
            btnCultivos = new Button();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.PeachPuff;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(btnCultivos);
            panel1.Controls.Add(btnClientes);
            panel1.Controls.Add(btnNotasCargo);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 29);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(175, 583);
            panel1.TabIndex = 0;
            // 
            // btnNotasCargo
            // 
            btnNotasCargo.BackColor = Color.Tan;
            btnNotasCargo.Dock = DockStyle.Top;
            btnNotasCargo.FlatStyle = FlatStyle.Flat;
            btnNotasCargo.Image = Properties.Resources.icons8_documento_20;
            btnNotasCargo.ImageAlign = ContentAlignment.MiddleRight;
            btnNotasCargo.Location = new Point(0, 60);
            btnNotasCargo.Name = "btnNotasCargo";
            btnNotasCargo.Size = new Size(171, 36);
            btnNotasCargo.TabIndex = 2;
            btnNotasCargo.Text = "Notas cargo";
            btnNotasCargo.UseVisualStyleBackColor = false;
            btnNotasCargo.Click += button4_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.PeachPuff;
            panel2.BackgroundImage = Properties.Resources.UCAH_LOGO_trans_2;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(171, 60);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Peru;
            panel3.Controls.Add(button3);
            panel3.Controls.Add(button2);
            panel3.Controls.Add(btnCerrar);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Margin = new Padding(2);
            panel3.Name = "panel3";
            panel3.Size = new Size(870, 29);
            panel3.TabIndex = 1;
            panel3.MouseMove += panel3_MouseMove;
            // 
            // button3
            // 
            button3.Dock = DockStyle.Right;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Image = Properties.Resources.icons8_línea_horizontal_16;
            button3.Location = new Point(774, 0);
            button3.Margin = new Padding(2);
            button3.Name = "button3";
            button3.Size = new Size(32, 29);
            button3.TabIndex = 3;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Right;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Image = Properties.Resources.icons8_squares_16_fliped;
            button2.Location = new Point(806, 0);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(32, 29);
            button2.TabIndex = 2;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // btnCerrar
            // 
            btnCerrar.Dock = DockStyle.Right;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Image = Properties.Resources.icons8_eliminar_16;
            btnCerrar.Location = new Point(838, 0);
            btnCerrar.Margin = new Padding(2);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(32, 29);
            btnCerrar.TabIndex = 1;
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += button1_Click;
            // 
            // PanelVentana
            // 
            PanelVentana.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            PanelVentana.BackColor = Color.LightGray;
            PanelVentana.BackgroundImageLayout = ImageLayout.Stretch;
            PanelVentana.Dock = DockStyle.Fill;
            PanelVentana.ForeColor = SystemColors.ControlText;
            PanelVentana.Location = new Point(175, 29);
            PanelVentana.Margin = new Padding(2);
            PanelVentana.Name = "PanelVentana";
            PanelVentana.Size = new Size(695, 583);
            PanelVentana.TabIndex = 2;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Right;
            button1.Location = new Point(668, 0);
            button1.Name = "button1";
            button1.Size = new Size(75, 64);
            button1.TabIndex = 1;
            button1.Text = "asdasdasd";
            button1.UseVisualStyleBackColor = true;
            // 
            // btnClientes
            // 
            btnClientes.BackColor = Color.Tan;
            btnClientes.Dock = DockStyle.Top;
            btnClientes.FlatStyle = FlatStyle.Flat;
            btnClientes.Image = Properties.Resources.icons8_encuentra_hombre_usuario_23;
            btnClientes.ImageAlign = ContentAlignment.MiddleRight;
            btnClientes.Location = new Point(0, 96);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(171, 36);
            btnClientes.TabIndex = 3;
            btnClientes.Text = "Clientes";
            btnClientes.UseVisualStyleBackColor = false;
            // 
            // btnCultivos
            // 
            btnCultivos.BackColor = Color.Tan;
            btnCultivos.Dock = DockStyle.Top;
            btnCultivos.FlatStyle = FlatStyle.Flat;
            btnCultivos.Image = Properties.Resources.icons8_brote_23;
            btnCultivos.ImageAlign = ContentAlignment.MiddleRight;
            btnCultivos.Location = new Point(0, 132);
            btnCultivos.Name = "btnCultivos";
            btnCultivos.Size = new Size(171, 36);
            btnCultivos.TabIndex = 4;
            btnCultivos.Text = "Cultivos";
            btnCultivos.UseVisualStyleBackColor = false;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 612);
            Controls.Add(PanelVentana);
            Controls.Add(panel1);
            Controls.Add(panel3);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            Margin = new Padding(2);
            Name = "Main";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel PanelVentana;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Button button3;
        private Button btnNotasCargo;
        private Button btnClientes;
        private Button btnCultivos;
    }
}

