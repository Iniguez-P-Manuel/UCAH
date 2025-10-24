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

        public override void Nuevo()
        {
            // Verificar si hay deducciones con valores diferentes de 0
            bool hayDeduccionesConValor = false;

            // Verificar en la lista actual
            if (lstDeduccionesNota != null && lstDeduccionesNota.Count > 0)
            {
                hayDeduccionesConValor = lstDeduccionesNota.Any(d => d.Importe != 0);
            }

            // También verificar en el DataGridView por si hay cambios no guardados
            if (!hayDeduccionesConValor)
            {
                foreach (DataGridViewRow row in dgvDeducciones.Rows)
                {
                    if (!row.IsNewRow && row.Cells[2].Value != null)
                    {
                        if (double.TryParse(row.Cells[2].Value.ToString(), out double importe) && importe != 0)
                        {
                            hayDeduccionesConValor = true;
                            break;
                        }
                    }
                }
            }

            // Si hay deducciones con valores, preguntar confirmación
            if (hayDeduccionesConValor)
            {
                var resultado = MessageBox.Show(
                    "¿Está seguro que desea eliminar todos los importes de deducciones?",
                    "Confirmar Nuevo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2 // No como opción por defecto
                );

                if (resultado != DialogResult.Yes)
                {
                    return; // El usuario canceló, mantener los datos actuales
                }
            }

            // Limpiar la lista de deducciones
            lstDeduccionesNota = new List<DeduccionNota>();

            // Limpiar todos los importes en el DataGridView
            foreach (DataGridViewRow row in dgvDeducciones.Rows)
            {
                if (!row.IsNewRow) // Evitar la fila nueva vacía
                {
                    // Poner el importe en 0 pero mantener el ID y nombre
                    row.Cells[2].Value = 0;
                }
            }

            // Opcional: Mostrar mensaje de confirmación solo si se limpiaron datos
            if (hayDeduccionesConValor)
            {
                MessageBox.Show("Todos los importes de deducciones han sido reseteados.", "Nuevo",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        // Construye la lista desde el DataGridView y la deja pública
        public override void Guardar()
        {
            try
            {
                var lista = new List<DeduccionNota>();
                foreach (DataGridViewRow row in dgvDeducciones.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (!int.TryParse(row.Cells[0].Value?.ToString(), out int idTipo)) continue;

                    double importe = 0;
                    double.TryParse(row.Cells[2].Value?.ToString(), out importe);

                    lista.Add(new DeduccionNota
                    {
                        Id = 0,
                        _Deduccion = new TipoDeduccion { Id = idTipo, Nombre = row.Cells[1].Value?.ToString() },
                        Importe = importe
                    });
                }

                lstDeduccionesNota = lista;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar deducciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NotaDeducciones_Leave(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}