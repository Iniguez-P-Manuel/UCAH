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
            panel1.Location = new Point(6, 56);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(665, 433);
            panel1.TabIndex = 5;
            panel1.Paint += panel1_Paint;
            // 
            // maskedtxtFacturaUUID
            // 
            maskedtxtFacturaUUID.Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            maskedtxtFacturaUUID.Location = new Point(135, 230);
            maskedtxtFacturaUUID.Margin = new Padding(10, 0, 10, 0);
            maskedtxtFacturaUUID.Mask = "AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA";
            maskedtxtFacturaUUID.Name = "maskedtxtFacturaUUID";
            maskedtxtFacturaUUID.Size = new Size(403, 28);
            maskedtxtFacturaUUID.TabIndex = 24;
            // 
            // lblUUID
            // 
            lblUUID.AutoSize = true;
            lblUUID.Location = new Point(29, 233);
            lblUUID.Name = "lblUUID";
            lblUUID.Size = new Size(103, 25);
            lblUUID.TabIndex = 23;
            lblUUID.Text = "UUID/CFID:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 371);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(94, 25);
            label10.TabIndex = 21;
            label10.Text = "# Cosecha";
            label10.Visible = false;
            // 
            // txtIdCosecha
            // 
            txtIdCosecha.BackColor = SystemColors.Info;
            txtIdCosecha.Cursor = Cursors.No;
            txtIdCosecha.Location = new Point(108, 368);
            txtIdCosecha.Margin = new Padding(4, 5, 4, 5);
            txtIdCosecha.Name = "txtIdCosecha";
            txtIdCosecha.PlaceholderText = "0";
            txtIdCosecha.ReadOnly = true;
            txtIdCosecha.Size = new Size(68, 31);
            txtIdCosecha.TabIndex = 20;
            txtIdCosecha.TextAlign = HorizontalAlignment.Center;
            txtIdCosecha.Visible = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(295, 185);
            label9.Name = "label9";
            label9.Size = new Size(107, 25);
            label9.TabIndex = 15;
            label9.Text = "Fin cosecha:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(29, 185);
            label8.Name = "label8";
            label8.Size = new Size(126, 25);
            label8.TabIndex = 13;
            label8.Text = "Inicio cosecha:";
            // 
            // dpFechaFinal
            // 
            dpFechaFinal.CustomFormat = "yyyy";
            dpFechaFinal.Format = DateTimePickerFormat.Short;
            dpFechaFinal.Location = new Point(407, 180);
            dpFechaFinal.Name = "dpFechaFinal";
            dpFechaFinal.ShowUpDown = true;
            dpFechaFinal.Size = new Size(131, 31);
            dpFechaFinal.TabIndex = 8;
            // 
            // dpFechaInicial
            // 
            dpFechaInicial.CustomFormat = "d/m/yyyy";
            dpFechaInicial.Format = DateTimePickerFormat.Short;
            dpFechaInicial.Location = new Point(160, 180);
            dpFechaInicial.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            dpFechaInicial.Name = "dpFechaInicial";
            dpFechaInicial.ShowUpDown = true;
            dpFechaInicial.Size = new Size(129, 31);
            dpFechaInicial.TabIndex = 7;
            dpFechaInicial.Value = new DateTime(2025, 10, 21, 23, 31, 51, 0);
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Left;
            button3.BackgroundImage = Properties.Resources.icons8_letra_pequeña_20;
            button3.BackgroundImageLayout = ImageLayout.Stretch;
            button3.Location = new Point(210, 3);
            button3.Margin = new Padding(4, 5, 4, 5);
            button3.Name = "button3";
            button3.Size = new Size(47, 52);
            button3.TabIndex = 1;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // txtDeducciones
            // 
            txtDeducciones.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            txtDeducciones.BackColor = SystemColors.Info;
            txtDeducciones.Cursor = Cursors.No;
            txtDeducciones.Location = new Point(501, 333);
            txtDeducciones.Margin = new Padding(4, 5, 4, 5);
            txtDeducciones.Name = "txtDeducciones";
            txtDeducciones.ReadOnly = true;
            txtDeducciones.Size = new Size(153, 31);
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
            btnDeducciones.Location = new Point(338, 329);
            btnDeducciones.Margin = new Padding(4, 5, 4, 5);
            btnDeducciones.Name = "btnDeducciones";
            btnDeducciones.Size = new Size(154, 42);
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
            txtImporte.Location = new Point(501, 381);
            txtImporte.Margin = new Padding(4, 5, 4, 5);
            txtImporte.Name = "txtImporte";
            txtImporte.ReadOnly = true;
            txtImporte.Size = new Size(153, 31);
            txtImporte.TabIndex = 13;
            txtImporte.Text = "0.00";
            txtImporte.TextAlign = HorizontalAlignment.Right;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(422, 386);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(76, 25);
            label7.TabIndex = 0;
            label7.Text = "Importe";
            // 
            // txtPrecio
            // 
            txtPrecio.Anchor = AnchorStyles.Left;
            txtPrecio.Location = new Point(361, 280);
            txtPrecio.Margin = new Padding(4, 5, 4, 5);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.PlaceholderText = "0.00";
            txtPrecio.Size = new Size(153, 31);
            txtPrecio.TabIndex = 10;
            txtPrecio.TextAlign = HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new Point(295, 285);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(60, 25);
            label6.TabIndex = 19;
            label6.Text = "Precio";
            // 
            // txtToneladas
            // 
            txtToneladas.Anchor = AnchorStyles.Left;
            txtToneladas.Location = new Point(121, 279);
            txtToneladas.Margin = new Padding(4, 5, 4, 5);
            txtToneladas.Name = "txtToneladas";
            txtToneladas.PlaceholderText = "0.00";
            txtToneladas.Size = new Size(153, 31);
            txtToneladas.TabIndex = 9;
            txtToneladas.TextAlign = HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(29, 284);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(90, 25);
            label5.TabIndex = 17;
            label5.Text = "Toneladas";
            // 
            // btnSeleccionarCultivo
            // 
            btnSeleccionarCultivo.Anchor = AnchorStyles.Left;
            btnSeleccionarCultivo.BackgroundImage = Properties.Resources.icons8_brote_32;
            btnSeleccionarCultivo.BackgroundImageLayout = ImageLayout.Stretch;
            btnSeleccionarCultivo.Location = new Point(549, 122);
            btnSeleccionarCultivo.Margin = new Padding(4, 5, 4, 5);
            btnSeleccionarCultivo.Name = "btnSeleccionarCultivo";
            btnSeleccionarCultivo.Size = new Size(47, 48);
            btnSeleccionarCultivo.TabIndex = 6;
            btnSeleccionarCultivo.UseVisualStyleBackColor = true;
            btnSeleccionarCultivo.Click += btnSeleccionarCultivo_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(29, 134);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(67, 25);
            label4.TabIndex = 10;
            label4.Text = "Cultivo";
            // 
            // txtCultivo
            // 
            txtCultivo.Anchor = AnchorStyles.Left;
            txtCultivo.BackColor = SystemColors.Info;
            txtCultivo.Cursor = Cursors.No;
            txtCultivo.Location = new Point(101, 129);
            txtCultivo.Margin = new Padding(4, 5, 4, 5);
            txtCultivo.Name = "txtCultivo";
            txtCultivo.ReadOnly = true;
            txtCultivo.Size = new Size(437, 31);
            txtCultivo.TabIndex = 5;
            // 
            // btnSeleccionarCliente
            // 
            btnSeleccionarCliente.Anchor = AnchorStyles.Left;
            btnSeleccionarCliente.BackgroundImage = Properties.Resources.icons8_encuentra_hombre_usuario_32;
            btnSeleccionarCliente.BackgroundImageLayout = ImageLayout.Stretch;
            btnSeleccionarCliente.Location = new Point(549, 72);
            btnSeleccionarCliente.Margin = new Padding(4, 5, 4, 5);
            btnSeleccionarCliente.Name = "btnSeleccionarCliente";
            btnSeleccionarCliente.Size = new Size(47, 48);
            btnSeleccionarCliente.TabIndex = 4;
            btnSeleccionarCliente.UseVisualStyleBackColor = true;
            btnSeleccionarCliente.Click += btnSeleccionarCliente_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(29, 86);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(65, 25);
            label3.TabIndex = 7;
            label3.Text = "Cliente";
            // 
            // txtCliente
            // 
            txtCliente.Anchor = AnchorStyles.Left;
            txtCliente.BackColor = SystemColors.Info;
            txtCliente.Cursor = Cursors.No;
            txtCliente.Location = new Point(101, 81);
            txtCliente.Margin = new Padding(4, 5, 4, 5);
            txtCliente.Name = "txtCliente";
            txtCliente.ReadOnly = true;
            txtCliente.Size = new Size(437, 31);
            txtCliente.TabIndex = 3;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(455, 17);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(57, 25);
            label2.TabIndex = 5;
            label2.Text = "Fecha";
            // 
            // dpFecha
            // 
            dpFecha.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dpFecha.Format = DateTimePickerFormat.Short;
            dpFecha.Location = new Point(518, 12);
            dpFecha.Margin = new Padding(4, 5, 4, 5);
            dpFecha.Name = "dpFecha";
            dpFecha.Size = new Size(135, 31);
            dpFecha.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 22);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(67, 25);
            label1.TabIndex = 2;
            label1.Text = "# Nota";
            // 
            // txtID
            // 
            txtID.BackColor = SystemColors.Info;
            txtID.Cursor = Cursors.No;
            txtID.Location = new Point(101, 12);
            txtID.Margin = new Padding(4, 5, 4, 5);
            txtID.Name = "txtID";
            txtID.PlaceholderText = "0";
            txtID.ReadOnly = true;
            txtID.Size = new Size(101, 31);
            txtID.TabIndex = 0;
            txtID.TextAlign = HorizontalAlignment.Center;
            // 
            // frmNotaCargo
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(677, 563);
            Controls.Add(panel1);
            Margin = new Padding(6, 8, 6, 8);
            Name = "frmNotaCargo";
            Padding = new Padding(6, 8, 6, 8);
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