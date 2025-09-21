using ReporteadorUCAH.DB_Services;
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


    public partial class BusquedaNotas : FormModel
    {
        public event EventHandler<ObjetoSeleccionadoEventArgs> ObjetoSeleccionado;

        private List<Modelos.NotaCargo> lstNotas = new List<Modelos.NotaCargo>();
        public BusquedaNotas()
        {
            InitializeComponent();

            this.BotonEliminar(false);
            this.BotonReporte(false);

            Color NuevoColor = Color.Khaki;
            this.CambiarColor(NuevoColor);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Buscar();
        }
        private void Buscar()
        {
            string Busqueda = txtBusqueda.Text;
            dgvNotas.Rows.Clear();
            lstNotas.Clear();

            lstNotas = new List<Modelos.NotaCargo>();

            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (NotasCargo DB_Notas = new NotasCargo(varCon))
                {
                    lstNotas = DB_Notas.BuscarNotas(Busqueda);
                }
            }

            foreach (Modelos.NotaCargo nota in lstNotas)
            {
                dgvNotas.Rows.Add(nota.Id, nota.Fecha, nota._Cliente.Nombre, nota._Cultivo.Nombre, nota.Tons);
            }
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                Buscar();
            }

        }


        public class ObjetoSeleccionadoEventArgs : EventArgs
        {
            public Modelos.NotaCargo ObjetoSeleccionado { get; }

            public ObjetoSeleccionadoEventArgs(Modelos.NotaCargo objeto)
            {
                ObjetoSeleccionado = objeto;
            }
        }

        public void Seleccionar()
        {
            // Obtener el ID de la primera columna
            int idSeleccionado = Convert.ToInt32(dgvNotas.Rows[dgvNotas.CurrentRow.Index].Cells[0].Value);

            // Buscar el objeto completo en la lista
            Modelos.NotaCargo objetoSeleccionado = lstNotas.FirstOrDefault(nota => nota.Id == idSeleccionado);

            if (objetoSeleccionado != null)
            {
                // Disparar el evento
                ObjetoSeleccionado?.Invoke(this, new ObjetoSeleccionadoEventArgs(objetoSeleccionado));

                // Cerrar la ventana si es necesario
                this.Close();
            }
        }
        public override void Guardar()
        {
            if (dgvNotas.CurrentRow != null && dgvNotas.CurrentRow.Index >= 0 )
            {
                Seleccionar();
            }
        }
        private void dgvNotas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Seleccionar();
            }
        }
    }
}
