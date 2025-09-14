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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            panel1 = new Panel();
            dgvDeducciones = new DataGridView();
            colidDeduccion = new DataGridViewTextBoxColumn();
            colDeduccion = new DataGridViewTextBoxColumn();
            colImporte = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDeducciones).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(dgvDeducciones);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 32);
            panel1.Name = "panel1";
            panel1.Size = new Size(444, 239);
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
            dgvDeducciones.Location = new Point(3, 5);
            dgvDeducciones.Name = "dgvDeducciones";
            dgvDeducciones.RowHeadersVisible = false;
            dgvDeducciones.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvDeducciones.Size = new Size(438, 228);
            dgvDeducciones.TabIndex = 0;
            // 
            // colidDeduccion
            // 
            colidDeduccion.HeaderText = "idDeduccion";
            colidDeduccion.Name = "colidDeduccion";
            colidDeduccion.ReadOnly = true;
            colidDeduccion.Visible = false;
            // 
            // colDeduccion
            // 
            colDeduccion.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDeduccion.HeaderText = "Deducción";
            colDeduccion.Name = "colDeduccion";
            colDeduccion.ReadOnly = true;
            // 
            // colImporte
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(192, 255, 192);
            colImporte.DefaultCellStyle = dataGridViewCellStyle1;
            colImporte.HeaderText = "Importe";
            colImporte.Name = "colImporte";
            colImporte.Width = 80;
            // 
            // DeduccionesNota
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 306);
            Controls.Add(panel1);
            Name = "DeduccionesNota";
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