namespace ReporteadorUCAH.Formas
{
    partial class frmNotaCargo
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
            panel1 = new Panel();
            maskedtxtFacturaUUID = new MaskedTextBox();
            lblUUID = new Label();
            label10 = new Label();
            txtIdCosecha = new TextBox();
            label9 = new Label();
            label8 = new Label();
            dpFechaFinal = new DateTimePicker();
            dpFechaInicial = new DateTimePicker();
            button3 = new Button();
            txtDeducciones = new TextBox();
            btnDeducciones = new Button();
            txtImporte = new TextBox();
            label7 = new Label();
            txtPrecio = new TextBox();
            label6 = new Label();
            txtToneladas = new TextBox();
            label5 = new Label();
            btnSeleccionarCultivo = new Button();
            label4 = new Label();
            txtCultivo = new TextBox();
            btnSeleccionarCliente = new Button();
            label3 = new Label();
            txtCliente = new TextBox();
            label2 = new Label();
            dpFecha = new DateTimePicker();
            label1 = new Label();
            txtID = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(maskedtxtFacturaUUID);
            panel1.Controls.Add(lblUUID);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(txtIdCosecha);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(dpFechaFinal);
            panel1.Controls.Add(dpFechaInicial);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(txtDeducciones);
            panel1.Controls.Add(btnDeducciones);
            panel1.Controls.Add(txtImporte);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(txtPrecio);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(txtToneladas);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(btnSeleccionarCultivo);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(txtCultivo);
            panel1.Controls.Add(btnSeleccionarCliente);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtCliente);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(dpFecha);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtID);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(4, 34);
            panel1.Name = "panel1";
            panel1.Size = new Size(466, 258);
            panel1.TabIndex = 5;
            panel1.Paint += panel1_Paint;
            // 
            // maskedtxtFacturaUUID
            // 
            maskedtxtFacturaUUID.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            maskedtxtFacturaUUID.Location = new Point(94, 138);
            maskedtxtFacturaUUID.Margin = new Padding(7, 0, 7, 0);
            maskedtxtFacturaUUID.Mask = "AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA";
            maskedtxtFacturaUUID.Name = "maskedtxtFacturaUUID";
            maskedtxtFacturaUUID.Size = new Size(283, 21);
            maskedtxtFacturaUUID.TabIndex = 24;
            // 
            // lblUUID
            // 
            lblUUID.AutoSize = true;
            lblUUID.Location = new Point(20, 140);
            lblUUID.Margin = new Padding(2, 0, 2, 0);
            lblUUID.Name = "lblUUID";
            lblUUID.Size = new Size(67, 15);
            lblUUID.TabIndex = 23;
            lblUUID.Text = "UUID/CFDI:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(4, 223);
            label10.Name = "label10";
            label10.Size = new Size(62, 15);
            label10.TabIndex = 21;
            label10.Text = "# Cosecha";
            label10.Visible = false;
            // 
            // txtIdCosecha
            // 
            txtIdCosecha.BackColor = SystemColors.Info;
            txtIdCosecha.Cursor = Cursors.No;
            txtIdCosecha.Location = new Point(76, 221);
            txtIdCosecha.Name = "txtIdCosecha";
            txtIdCosecha.PlaceholderText = "0";
            txtIdCosecha.ReadOnly = true;
            txtIdCosecha.Size = new Size(49, 23);
            txtIdCosecha.TabIndex = 20;
            txtIdCosecha.TextAlign = HorizontalAlignment.Center;
            txtIdCosecha.Visible = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(206, 111);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(72, 15);
            label9.TabIndex = 15;
            label9.Text = "Fin cosecha:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(20, 111);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(85, 15);
            label8.TabIndex = 13;
            label8.Text = "Inicio cosecha:";
            // 
            // dpFechaFinal
            // 
            dpFechaFinal.CustomFormat = "yyyy";
            dpFechaFinal.Format = DateTimePickerFormat.Short;
            dpFechaFinal.Location = new Point(285, 108);
            dpFechaFinal.Margin = new Padding(2);
            dpFechaFinal.Name = "dpFechaFinal";
            dpFechaFinal.ShowUpDown = true;
            dpFechaFinal.Size = new Size(93, 23);
            dpFechaFinal.TabIndex = 8;
            // 
            // dpFechaInicial
            // 
            dpFechaInicial.CustomFormat = "d/m/yyyy";
            dpFechaInicial.Format = DateTimePickerFormat.Short;
            dpFechaInicial.Location = new Point(112, 108);
            dpFechaInicial.Margin = new Padding(2);
            dpFechaInicial.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            dpFechaInicial.Name = "dpFechaInicial";
            dpFechaInicial.ShowUpDown = true;
            dpFechaInicial.Size = new Size(92, 23);
            dpFechaInicial.TabIndex = 7;
            dpFechaInicial.Value = new DateTime(2025, 10, 21, 23, 31, 51, 0);
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Left;
            button3.BackgroundImage = Properties.Resources.icons8_letra_pequeña_20;
            button3.BackgroundImageLayout = ImageLayout.Stretch;
            button3.Location = new Point(147, 1);
            button3.Name = "button3";
            button3.Size = new Size(33, 31);
            button3.TabIndex = 1;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // txtDeducciones
            // 
            txtDeducciones.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            txtDeducciones.BackColor = SystemColors.Info;
            txtDeducciones.Cursor = Cursors.No;
            txtDeducciones.Location = new Point(351, 198);
            txtDeducciones.Name = "txtDeducciones";
            txtDeducciones.ReadOnly = true;
            txtDeducciones.Size = new Size(108, 23);
            txtDeducciones.TabIndex = 12;
            txtDeducciones.Text = "0.00";
            txtDeducciones.TextAlign = HorizontalAlignment.Right;
            // 
            // btnDeducciones
            // 
            btnDeducciones.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDeducciones.BackColor = Color.DarkSeaGreen;
            btnDeducciones.BackgroundImageLayout = ImageLayout.Stretch;
            btnDeducciones.FlatAppearance.MouseOverBackColor = Color.FromArgb(128, 255, 128);
            btnDeducciones.FlatStyle = FlatStyle.Flat;
            btnDeducciones.Image = Properties.Resources.icons8_efectivo_20;
            btnDeducciones.ImageAlign = ContentAlignment.MiddleRight;
            btnDeducciones.Location = new Point(237, 196);
            btnDeducciones.Name = "btnDeducciones";
            btnDeducciones.Size = new Size(108, 25);
            btnDeducciones.TabIndex = 11;
            btnDeducciones.Text = "Deducciones";
            btnDeducciones.TextAlign = ContentAlignment.MiddleLeft;
            btnDeducciones.UseVisualStyleBackColor = false;
            btnDeducciones.Click += btnDeducciones_Click;
            // 
            // txtImporte
            // 
            txtImporte.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            txtImporte.BackColor = SystemColors.Info;
            txtImporte.Cursor = Cursors.No;
            txtImporte.Location = new Point(351, 227);
            txtImporte.Name = "txtImporte";
            txtImporte.ReadOnly = true;
            txtImporte.Size = new Size(108, 23);
            txtImporte.TabIndex = 13;
            txtImporte.Text = "0.00";
            txtImporte.TextAlign = HorizontalAlignment.Right;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(295, 230);
            label7.Name = "label7";
            label7.Size = new Size(49, 15);
            label7.TabIndex = 0;
            label7.Text = "Importe";
            // 
            // txtPrecio
            // 
            txtPrecio.Anchor = AnchorStyles.Left;
            txtPrecio.Location = new Point(253, 167);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.PlaceholderText = "0.00";
            txtPrecio.Size = new Size(108, 23);
            txtPrecio.TabIndex = 10;
            txtPrecio.TextAlign = HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new Point(206, 170);
            label6.Name = "label6";
            label6.Size = new Size(40, 15);
            label6.TabIndex = 19;
            label6.Text = "Precio";
            // 
            // txtToneladas
            // 
            txtToneladas.Anchor = AnchorStyles.Left;
            txtToneladas.Location = new Point(85, 166);
            txtToneladas.Name = "txtToneladas";
            txtToneladas.PlaceholderText = "0.00";
            txtToneladas.Size = new Size(108, 23);
            txtToneladas.TabIndex = 9;
            txtToneladas.TextAlign = HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(20, 169);
            label5.Name = "label5";
            label5.Size = new Size(59, 15);
            label5.TabIndex = 17;
            label5.Text = "Toneladas";
            // 
            // btnSeleccionarCultivo
            // 
            btnSeleccionarCultivo.Anchor = AnchorStyles.Left;
            btnSeleccionarCultivo.BackgroundImage = Properties.Resources.icons8_brote_32;
            btnSeleccionarCultivo.BackgroundImageLayout = ImageLayout.Stretch;
            btnSeleccionarCultivo.Location = new Point(384, 72);
            btnSeleccionarCultivo.Name = "btnSeleccionarCultivo";
            btnSeleccionarCultivo.Size = new Size(33, 29);
            btnSeleccionarCultivo.TabIndex = 6;
            btnSeleccionarCultivo.UseVisualStyleBackColor = true;
            btnSeleccionarCultivo.Click += btnSeleccionarCultivo_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(20, 79);
            label4.Name = "label4";
            label4.Size = new Size(45, 15);
            label4.TabIndex = 10;
            label4.Text = "Cultivo";
            // 
            // txtCultivo
            // 
            txtCultivo.Anchor = AnchorStyles.Left;
            txtCultivo.BackColor = SystemColors.Info;
            txtCultivo.Cursor = Cursors.No;
            txtCultivo.Location = new Point(71, 76);
            txtCultivo.Name = "txtCultivo";
            txtCultivo.ReadOnly = true;
            txtCultivo.Size = new Size(307, 23);
            txtCultivo.TabIndex = 5;
            // 
            // btnSeleccionarCliente
            // 
            btnSeleccionarCliente.Anchor = AnchorStyles.Left;
            btnSeleccionarCliente.BackgroundImage = Properties.Resources.icons8_encuentra_hombre_usuario_32;
            btnSeleccionarCliente.BackgroundImageLayout = ImageLayout.Stretch;
            btnSeleccionarCliente.Location = new Point(384, 42);
            btnSeleccionarCliente.Name = "btnSeleccionarCliente";
            btnSeleccionarCliente.Size = new Size(33, 29);
            btnSeleccionarCliente.TabIndex = 4;
            btnSeleccionarCliente.UseVisualStyleBackColor = true;
            btnSeleccionarCliente.Click += btnSeleccionarCliente_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(20, 51);
            label3.Name = "label3";
            label3.Size = new Size(44, 15);
            label3.TabIndex = 7;
            label3.Text = "Cliente";
            // 
            // txtCliente
            // 
            txtCliente.Anchor = AnchorStyles.Left;
            txtCliente.BackColor = SystemColors.Info;
            txtCliente.Cursor = Cursors.No;
            txtCliente.Location = new Point(71, 48);
            txtCliente.Name = "txtCliente";
            txtCliente.ReadOnly = true;
            txtCliente.Size = new Size(307, 23);
            txtCliente.TabIndex = 3;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(318, 10);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 5;
            label2.Text = "Fecha";
            // 
            // dpFecha
            // 
            dpFecha.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dpFecha.Format = DateTimePickerFormat.Short;
            dpFecha.Location = new Point(363, 7);
            dpFecha.Name = "dpFecha";
            dpFecha.Size = new Size(96, 23);
            dpFecha.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 13);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 2;
            label1.Text = "# Nota";
            // 
            // txtID
            // 
            txtID.BackColor = SystemColors.Info;
            txtID.Cursor = Cursors.No;
            txtID.Location = new Point(71, 7);
            txtID.Name = "txtID";
            txtID.PlaceholderText = "0";
            txtID.ReadOnly = true;
            txtID.Size = new Size(72, 23);
            txtID.TabIndex = 0;
            txtID.TextAlign = HorizontalAlignment.Center;
            // 
            // frmNotaCargo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(474, 338);
            Controls.Add(panel1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "frmNotaCargo";
            Padding = new Padding(4, 5, 4, 5);
            Text = "Nota cargo";
            Controls.SetChildIndex(panel1, 0);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnDeducciones;
        private TextBox txtImporte;
        private Label label7;
        private TextBox txtPrecio;
        private Label label6;
        private TextBox txtToneladas;
        private Label label5;
        private Button btnSeleccionarCultivo;
        private Label label4;
        private TextBox txtCultivo;
        private Button btnSeleccionarCliente;
        private Label label3;
        private TextBox txtCliente;
        private Label label2;
        private DateTimePicker dpFecha;
        private Label label1;
        private TextBox txtID;
        private TextBox txtDeducciones;
        private Button button3;
        private Label label9;
        private Label label8;
        private DateTimePicker dpFechaFinal;
        private DateTimePicker dpFechaInicial;
        private Label label10;
        private TextBox txtIdCosecha;
        private Label lblUUID;
        private MaskedTextBox maskedtxtFacturaUUID;
    }
}