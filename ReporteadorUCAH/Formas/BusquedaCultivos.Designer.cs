namespace ReporteadorUCAH.Formas
{
    partial class BusquedaCultivos
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
            txtBusqueda = new TextBox();
            button1 = new Button();
            panel1 = new Panel();
            dgvCultivos = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCultivos).BeginInit();
            SuspendLayout();
            // 
            // txtBusqueda
            // 
            txtBusqueda.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtBusqueda.Location = new Point(12, 69);
            txtBusqueda.Margin = new Padding(4, 5, 4, 5);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.PlaceholderText = "Buscar cultivo...";
            txtBusqueda.Size = new Size(1112, 31);
            txtBusqueda.TabIndex = 5;
            // 
            // button1
            // 
            button1.BackgroundImage = Properties.Resources.icons8_letra_pequeña_20;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Location = new Point(1124, 8);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(46, 48);
            button1.TabIndex = 6;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(dgvCultivos);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(4, 53);
            panel1.Name = "panel1";
            panel1.Size = new Size(1174, 471);
            panel1.TabIndex = 7;
            // 
            // dgvCultivos
            // 
            dgvCultivos.AllowUserToAddRows = false;
            dgvCultivos.AllowUserToDeleteRows = false;
            dgvCultivos.AllowUserToOrderColumns = true;
            dgvCultivos.AllowUserToResizeRows = false;
            dgvCultivos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCultivos.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4 });
            dgvCultivos.Dock = DockStyle.Bottom;
            dgvCultivos.Location = new Point(0, 64);
            dgvCultivos.Name = "dgvCultivos";
            dgvCultivos.RowHeadersVisible = false;
            dgvCultivos.RowHeadersWidth = 62;
            dgvCultivos.Size = new Size(1174, 407);
            dgvCultivos.TabIndex = 7;
            dgvCultivos.CellContentClick += dgvCultivos_CellContentClick;
            dgvCultivos.CellDoubleClick += dgvCultivos_CellDoubleClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "#";
            Column1.MinimumWidth = 8;
            Column1.Name = "Column1";
            Column1.Width = 50;
            // 
            // Column2
            // 
            Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column2.HeaderText = "Nombre";
            Column2.MinimumWidth = 8;
            Column2.Name = "Column2";
            // 
            // Column3
            // 
            Column3.HeaderText = "Cultivo";
            Column3.MinimumWidth = 8;
            Column3.Name = "Column3";
            Column3.Width = 150;
            // 
            // Column4
            // 
            Column4.HeaderText = "CONS";
            Column4.MinimumWidth = 8;
            Column4.Name = "Column4";
            Column4.Width = 120;
            // 
            // BusquedaCultivos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 595);
            Controls.Add(txtBusqueda);
            Controls.Add(panel1);
            Name = "BusquedaCultivos";
            Text = "BusquedaCultivos";
            Controls.SetChildIndex(panel1, 0);
            Controls.SetChildIndex(txtBusqueda, 0);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCultivos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBusqueda;
        private Button button1;
        private Panel panel1;
        private DataGridView dgvCultivos;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
    }
}