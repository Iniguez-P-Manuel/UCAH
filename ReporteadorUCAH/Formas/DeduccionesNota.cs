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
        }

        private void NotaDeducciones_Leave(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
