namespace ReporteadorUCAH.Formas
{
    partial class BusquedaClientes
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
            dgvClientes = new DataGridView();
            colidNota = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colCalle = new DataGridViewTextBoxColumn();
            colRFC = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(txtBusqueda);
            panel1.Controls.Add(dgvClientes);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(6, 56);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(882, 528);
            panel1.TabIndex = 5;
            // 
            // button1
            // 
            button1.BackgroundImage = Properties.Resources.icons8_letra_pequeña_20;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Location = new Point(837, 23);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(46, 48);
            button1.TabIndex = 2;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtBusqueda
            // 
            txtBusqueda.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtBusqueda.Location = new Point(4, 28);
            txtBusqueda.Margin = new Padding(4, 5, 4, 5);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.PlaceholderText = "Buscar cliente...";
            txtBusqueda.Size = new Size(823, 31);
            txtBusqueda.TabIndex = 1;
            txtBusqueda.KeyDown += txtBusqueda_KeyDown;
            // 
            // dgvClientes
            // 
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.AllowUserToOrderColumns = true;
            dgvClientes.AllowUserToResizeRows = false;
            dgvClientes.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dgvClientes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Columns.AddRange(new DataGridViewColumn[] { colidNota, colNombre, colCalle, colRFC });
            dgvClientes.Location = new Point(4, 76);
            dgvClientes.Margin = new Padding(4, 5, 4, 5);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.RowHeadersVisible = false;
            dgvClientes.RowHeadersWidth = 62;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.Size = new Size(873, 443);
            dgvClientes.TabIndex = 0;
            dgvClientes.CellDoubleClick += dgvNotas_CellDoubleClick;
            // 
            // colidNota
            // 
            colidNota.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colidNota.DefaultCellStyle = dataGridViewCellStyle1;
            colidNota.HeaderText = "#";
            colidNota.MinimumWidth = 8;
            colidNota.Name = "colidNota";
            colidNota.ReadOnly = true;
            colidNota.Width = 60;
            // 
            // colNombre
            // 
            colNombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNombre.HeaderText = "Nombres";
            colNombre.MinimumWidth = 8;
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // colCalle
            // 
            colCalle.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            colCalle.DefaultCellStyle = dataGridViewCellStyle2;
            colCalle.HeaderText = "Calle";
            colCalle.MinimumWidth = 8;
            colCalle.Name = "colCalle";
            colCalle.ReadOnly = true;
            colCalle.Width = 150;
            // 
            // colRFC
            // 
            colRFC.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colRFC.HeaderText = "RFC";
            colRFC.MinimumWidth = 8;
            colRFC.Name = "colRFC";
            colRFC.ReadOnly = true;
            colRFC.Width = 150;
            // 
            // BusquedaClientes
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(894, 658);
            Controls.Add(panel1);
            Margin = new Padding(6, 8, 6, 8);
            Name = "BusquedaClientes";
            Padding = new Padding(6, 8, 6, 8);
            Text = "Busqueda Notas";
            Controls.SetChildIndex(panel1, 0);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox txtBusqueda;
        private DataGridView dgvClientes;
        private Button button1;
        private DataGridViewTextBoxColumn colidNota;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colCalle;
        private DataGridViewTextBoxColumn colRFC;
    }
}