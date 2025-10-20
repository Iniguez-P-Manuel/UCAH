namespace ReporteadorUCAH.Formas
{
    partial class AgregarGrupoFamiliar
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
            groupBox1 = new GroupBox();
            label1 = new Label();
            txtID = new TextBox();
            label4 = new Label();
            txtNombre = new TextBox();
            txtBusqueda = new TextBox();
            dgvGrupofamiliar = new DataGridView();
            colid = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGrupofamiliar).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(txtBusqueda);
            panel1.Controls.Add(dgvGrupofamiliar);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 32);
            panel1.Name = "panel1";
            panel1.Size = new Size(350, 277);
            panel1.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtID);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Location = new Point(5, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(341, 78);
            groupBox1.TabIndex = 25;
            groupBox1.TabStop = false;
            groupBox1.Text = "Información";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(111, 24);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(14, 15);
            label1.TabIndex = 23;
            label1.Text = "#";
            // 
            // txtID
            // 
            txtID.BackColor = SystemColors.Info;
            txtID.Cursor = Cursors.No;
            txtID.Location = new Point(129, 21);
            txtID.Margin = new Padding(2);
            txtID.Name = "txtID";
            txtID.PlaceholderText = "0";
            txtID.ReadOnly = true;
            txtID.Size = new Size(41, 23);
            txtID.TabIndex = 24;
            txtID.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(40, 51);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(85, 15);
            label4.TabIndex = 21;
            label4.Text = "Grupo Familiar";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = SystemColors.Info;
            txtNombre.Location = new Point(129, 48);
            txtNombre.Margin = new Padding(2);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(139, 23);
            txtNombre.TabIndex = 22;
            // 
            // txtBusqueda
            // 
            txtBusqueda.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtBusqueda.Location = new Point(5, 85);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.PlaceholderText = "Buscar grupo familiar...";
            txtBusqueda.Size = new Size(342, 23);
            txtBusqueda.TabIndex = 24;
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            // 
            // dgvGrupofamiliar
            // 
            dgvGrupofamiliar.AllowUserToAddRows = false;
            dgvGrupofamiliar.AllowUserToDeleteRows = false;
            dgvGrupofamiliar.AllowUserToOrderColumns = true;
            dgvGrupofamiliar.AllowUserToResizeRows = false;
            dgvGrupofamiliar.Anchor = AnchorStyles.Left;
            dgvGrupofamiliar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvGrupofamiliar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGrupofamiliar.Columns.AddRange(new DataGridViewColumn[] { colid, colNombre });
            dgvGrupofamiliar.Location = new Point(5, 112);
            dgvGrupofamiliar.Name = "dgvGrupofamiliar";
            dgvGrupofamiliar.RowHeadersVisible = false;
            dgvGrupofamiliar.RowHeadersWidth = 62;
            dgvGrupofamiliar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGrupofamiliar.Size = new Size(341, 150);
            dgvGrupofamiliar.TabIndex = 23;
            dgvGrupofamiliar.CellDoubleClick += dgvGrupofamiliar_CellDoubleClick;
            // 
            // colid
            // 
            colid.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colid.DefaultCellStyle = dataGridViewCellStyle2;
            colid.HeaderText = "#";
            colid.MinimumWidth = 8;
            colid.Name = "colid";
            colid.ReadOnly = true;
            colid.Width = 60;
            // 
            // colNombre
            // 
            colNombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNombre.HeaderText = "Nombre Grupo";
            colNombre.MinimumWidth = 8;
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // AgregarGrupoFamiliar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(356, 353);
            Controls.Add(panel1);
            Name = "AgregarGrupoFamiliar";
            Text = "AgregarGrupoFamiliar";
            Load += AgregarGrupoFamiliar_Load;
            Controls.SetChildIndex(panel1, 0);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGrupofamiliar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label4;
        private TextBox txtNombre;
        private TextBox txtBusqueda;
        private DataGridView dgvGrupofamiliar;
        private DataGridViewTextBoxColumn colid;
        private DataGridViewTextBoxColumn colNombre;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtID;
    }
}