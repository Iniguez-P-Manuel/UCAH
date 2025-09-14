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
            btnCultivos = new Button();
            btnClientes = new Button();
            btnNotasCargo = new Button();
            panel2 = new Panel();
            barraPrincipal = new Panel();
            btnMinimizar = new Button();
            btnMaximizar = new Button();
            btnCerrar = new Button();
            PanelVentana = new Panel();
            button1 = new Button();
            panel1.SuspendLayout();
            barraPrincipal.SuspendLayout();
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
            btnNotasCargo.Click += btnNotasCargo_Click;
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
            // barraPrincipal
            // 
            barraPrincipal.BackColor = Color.Peru;
            barraPrincipal.Controls.Add(btnMinimizar);
            barraPrincipal.Controls.Add(btnMaximizar);
            barraPrincipal.Controls.Add(btnCerrar);
            barraPrincipal.Dock = DockStyle.Top;
            barraPrincipal.Location = new Point(0, 0);
            barraPrincipal.Margin = new Padding(2);
            barraPrincipal.Name = "barraPrincipal";
            barraPrincipal.Size = new Size(870, 29);
            barraPrincipal.TabIndex = 1;
            barraPrincipal.DoubleClick += barraPrincipal_DoubleClick;
            barraPrincipal.MouseMove += barraPrincipal_MouseMove;
            // 
            // btnMinimizar
            // 
            btnMinimizar.Dock = DockStyle.Right;
            btnMinimizar.FlatAppearance.BorderSize = 0;
            btnMinimizar.FlatStyle = FlatStyle.Flat;
            btnMinimizar.Image = Properties.Resources.icons8_línea_horizontal_16;
            btnMinimizar.Location = new Point(774, 0);
            btnMinimizar.Margin = new Padding(2);
            btnMinimizar.Name = "btnMinimizar";
            btnMinimizar.Size = new Size(32, 29);
            btnMinimizar.TabIndex = 3;
            btnMinimizar.UseVisualStyleBackColor = true;
            btnMinimizar.Click += btnMinimizar_Click;
            // 
            // btnMaximizar
            // 
            btnMaximizar.Dock = DockStyle.Right;
            btnMaximizar.FlatAppearance.BorderSize = 0;
            btnMaximizar.FlatStyle = FlatStyle.Flat;
            btnMaximizar.Image = Properties.Resources.icons8_squares_16_fliped;
            btnMaximizar.Location = new Point(806, 0);
            btnMaximizar.Margin = new Padding(2);
            btnMaximizar.Name = "btnMaximizar";
            btnMaximizar.Size = new Size(32, 29);
            btnMaximizar.TabIndex = 2;
            btnMaximizar.UseVisualStyleBackColor = true;
            btnMaximizar.Click += btnMaximizar_Click;
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
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 612);
            Controls.Add(PanelVentana);
            Controls.Add(panel1);
            Controls.Add(barraPrincipal);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            Margin = new Padding(2);
            Name = "Main";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            barraPrincipal.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel barraPrincipal;
        private System.Windows.Forms.Panel PanelVentana;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnMaximizar;
        private Button btnMinimizar;
        private Button btnNotasCargo;
        private Button btnClientes;
        private Button btnCultivos;
    }
}

