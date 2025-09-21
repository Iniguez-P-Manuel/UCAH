namespace ReporteadorUCAH.Formas
{
    partial class BusquedaNotas
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panel1 = new Panel();
            button1 = new Button();
            txtBusqueda = new TextBox();
            dgvNotas = new DataGridView();
            colidNota = new DataGridViewTextBoxColumn();
            colFecha = new DataGridViewTextBoxColumn();
            colCliente = new DataGridViewTextBoxColumn();
            colCultivo = new DataGridViewTextBoxColumn();
            colTons = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNotas).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(txtBusqueda);
            panel1.Controls.Add(dgvNotas);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 32);
            panel1.Name = "panel1";
            panel1.Size = new Size(579, 310);
            panel1.TabIndex = 5;
            // 
            // button1
            // 
            button1.BackgroundImage = Properties.Resources.icons8_letra_pequeña_20;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Location = new Point(544, 10);
            button1.Name = "button1";
            button1.Size = new Size(32, 29);
            button1.TabIndex = 2;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtBusqueda
            // 
            txtBusqueda.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtBusqueda.Location = new Point(3, 14);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.PlaceholderText = "Buscar cliente...";
            txtBusqueda.Size = new Size(539, 23);
            txtBusqueda.TabIndex = 1;
            txtBusqueda.KeyDown += txtBusqueda_KeyDown;
            // 
            // dgvNotas
            // 
            dgvNotas.AllowUserToAddRows = false;
            dgvNotas.AllowUserToDeleteRows = false;
            dgvNotas.AllowUserToOrderColumns = true;
            dgvNotas.AllowUserToResizeRows = false;
            dgvNotas.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dgvNotas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvNotas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNotas.Columns.AddRange(new DataGridViewColumn[] { colidNota, colFecha, colCliente, colCultivo, colTons });
            dgvNotas.Location = new Point(3, 43);
            dgvNotas.Name = "dgvNotas";
            dgvNotas.RowHeadersVisible = false;
            dgvNotas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNotas.Size = new Size(573, 266);
            dgvNotas.TabIndex = 0;
            dgvNotas.CellDoubleClick += dgvNotas_CellDoubleClick;
            // 
            // colidNota
            // 
            colidNota.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colidNota.DefaultCellStyle = dataGridViewCellStyle1;
            colidNota.HeaderText = "#";
            colidNota.Name = "colidNota";
            colidNota.ReadOnly = true;
            colidNota.Width = 60;
            // 
            // colFecha
            // 
            colFecha.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colFecha.HeaderText = "Fecha";
            colFecha.Name = "colFecha";
            colFecha.ReadOnly = true;
            // 
            // colCliente
            // 
            colCliente.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCliente.HeaderText = "Cliente";
            colCliente.Name = "colCliente";
            colCliente.ReadOnly = true;
            // 
            // colCultivo
            // 
            colCultivo.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colCultivo.HeaderText = "Cultivo";
            colCultivo.Name = "colCultivo";
            colCultivo.ReadOnly = true;
            colCultivo.Width = 120;
            // 
            // colTons
            // 
            colTons.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTons.DefaultCellStyle = dataGridViewCellStyle2;
            colTons.HeaderText = "Toneladas";
            colTons.Name = "colTons";
            colTons.ReadOnly = true;
            colTons.Width = 70;
            // 
            // BusquedaNotas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(585, 386);
            Controls.Add(panel1);
            Name = "BusquedaNotas";
            Text = "Busqueda Notas";
            Controls.SetChildIndex(panel1, 0);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNotas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox txtBusqueda;
        private DataGridView dgvNotas;
        private Button button1;
        private DataGridViewTextBoxColumn colidNota;
        private DataGridViewTextBoxColumn colFecha;
        private DataGridViewTextBoxColumn colCliente;
        private DataGridViewTextBoxColumn colCultivo;
        private DataGridViewTextBoxColumn colTons;
    }
}