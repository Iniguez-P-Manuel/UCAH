using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReporteadorUCAH.Formas
{
    public partial class DeduccionesNota : FormModel
    {

        public DeduccionesNota()
        {
            InitializeComponent();
        }
        public List<DeduccionNota> lstDeduccionesNota = new List<DeduccionNota>();

        private void NotaDeducciones_Load(object sender, EventArgs e)
        {
            this.BotonEliminar(false);
            this.BotonReporte(false);

            Color NuevoColor = Color.Khaki;
            this.CambiarColor(NuevoColor);
            
            CargarDatos();
        }
        public void CargarDatos()
        {
            dgvDeducciones.Rows.Add("1", "Comision", 0);
            dgvDeducciones.Rows.Add("2", "Habilitacion o Avio", 0);
            dgvDeducciones.Rows.Add("3", "Refaccionario", 0);
            dgvDeducciones.Rows.Add("4", "CSGH", 0);
            dgvDeducciones.Rows.Add("5", "Quirografario", 0);
            dgvDeducciones.Rows.Add("6", "Seguro de vida", 0);
            dgvDeducciones.Rows.Add("7", "Otros deudores", 0);
            dgvDeducciones.Rows.Add("8", "Seguro contra incendio", 0);
            dgvDeducciones.Rows.Add("9", "Agroinsumos", 0);
            dgvDeducciones.Rows.Add("10", "Semilla", 0);
            dgvDeducciones.Rows.Add("11", "Importe Retenido a Favor", 0);
            dgvDeducciones.Rows.Add("12", "Buro de Credito", 0);
            dgvDeducciones.Rows.Add("13", "Impuesto ejidal", 0);
            dgvDeducciones.Rows.Add("14", "Seguro agricola", 0);
            dgvDeducciones.Rows.Add("15", "Criba Ahsa", 0);
            dgvDeducciones.Rows.Add("16", "Criba Industrial", 0);
            dgvDeducciones.Rows.Add("17", "Impuesto ejidal", 0);
            dgvDeducciones.Rows.Add("18", "Anticipo", 0);
            dgvDeducciones.Rows.Add("19", "Prendario", 0);

            if (lstDeduccionesNota != null && lstDeduccionesNota.Count > 0)
            {
                RellenarImportesDeducciones(lstDeduccionesNota);
            }
        }
        public void RellenarImportesDeducciones(List<DeduccionNota> deducciones)
        {
            // Recorrer todas las filas del DataGridView
            foreach (DataGridViewRow row in dgvDeducciones.Rows)
            {
                if (!row.IsNewRow) // Evitar la fila nueva vacía
                {
                    // Obtener el ID de la primera columna
                    if (int.TryParse(row.Cells[0].Value?.ToString(), out int id))
                    {
                        // Buscar la deducción correspondiente en la lista
                        DeduccionNota deduccion = deducciones.FirstOrDefault(d => d._Deduccion.Id == id);

                        if (deduccion != null)
                        {
                            // Asignar el importe a la tercera columna (índice 2)
                            row.Cells[2].Value = deduccion.Importe;
                        }
                    }
                }
            }
        }

        private void NotaDeducciones_Leave(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
