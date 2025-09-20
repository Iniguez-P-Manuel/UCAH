namespace ReporteadorUCAH.Formas
{
    partial class DeduccionesNota
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panel1 = new Panel();
            dgvDeducciones = new DataGridView();
            colImporte = new DataGridViewTextBoxColumn();
            colDeduccion = new DataGridViewTextBoxColumn();
            colidDeduccion = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDeducciones).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(dgvDeducciones);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(6, 56);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(631, 395);
            panel1.TabIndex = 5;
            // 
            // dgvDeducciones
            // 
            dgvDeducciones.AllowUserToAddRows = false;
            dgvDeducciones.AllowUserToDeleteRows = false;
            dgvDeducciones.AllowUserToResizeColumns = false;
            dgvDeducciones.AllowUserToResizeRows = false;
            dgvDeducciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDeducciones.Columns.AddRange(new DataGridViewColumn[] { colidDeduccion, colDeduccion, colImporte });
            dgvDeducciones.Location = new Point(4, 8);
            dgvDeducciones.Margin = new Padding(4, 5, 4, 5);
            dgvDeducciones.Name = "dgvDeducciones";
            dgvDeducciones.RowHeadersVisible = false;
            dgvDeducciones.RowHeadersWidth = 62;
            dgvDeducciones.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvDeducciones.Size = new Size(626, 380);
            dgvDeducciones.TabIndex = 0;
            // 
            // colImporte
            // 
            dataGridViewCellStyle2.BackColor = Color.FromArgb(192, 255, 192);
            colImporte.DefaultCellStyle = dataGridViewCellStyle2;
            colImporte.HeaderText = "Importe";
            colImporte.MinimumWidth = 8;
            colImporte.Name = "colImporte";
            colImporte.Width = 80;
            // 
            // colDeduccion
            // 
            colDeduccion.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDeduccion.HeaderText = "Deducción";
            colDeduccion.MinimumWidth = 8;
            colDeduccion.Name = "colDeduccion";
            colDeduccion.ReadOnly = true;
            // 
            // colidDeduccion
            // 
            colidDeduccion.HeaderText = "idDeduccion";
            colidDeduccion.MinimumWidth = 8;
            colidDeduccion.Name = "colidDeduccion";
            colidDeduccion.ReadOnly = true;
            colidDeduccion.Visible = false;
            colidDeduccion.Width = 150;
            // 
            // DeduccionesNota
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(643, 510);
            Controls.Add(panel1);
            Margin = new Padding(6, 8, 6, 8);
            Name = "DeduccionesNota";
            Padding = new Padding(6, 8, 6, 8);
            Text = "Deducciones nota";
            Load += NotaDeducciones_Load;
            Leave += NotaDeducciones_Leave;
            Controls.SetChildIndex(panel1, 0);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDeducciones).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView dgvDeducciones;
        private DataGridViewTextBoxColumn colidDeduccion;
        private DataGridViewTextBoxColumn colDeduccion;
        private DataGridViewTextBoxColumn colImporte;
    }
}