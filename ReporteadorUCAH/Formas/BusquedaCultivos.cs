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
    public partial class BusquedaCultivos : FormModel
    {
        public event EventHandler<ObjetoSeleccionadoEventArgs> ObjetoSeleccionado;

        private List<Modelos.Cultivo> lstCultivos = new List<Modelos.Cultivo>();
        public BusquedaCultivos()
        {
            InitializeComponent();
            this.BotonEliminar(false);
            this.BotonReporte(false);
            this.BotonNuevo(false);

            Color NuevoColor = Color.Khaki;
            this.CambiarColor(NuevoColor);

            this.AcceptButton = this.button1;
            txtBusqueda.KeyDown += txtBusqueda_KeyDown;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private async Task Buscar()
        {
            string busqueda = txtBusqueda.Text;
            dgvCultivos.Rows.Clear();
            lstCultivos.Clear();

            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (DB_Services.Cultivos DB_Cultivos = new DB_Services.Cultivos(varCon))
                {
                    var todos = await EjecutarConLoading(() => DB_Cultivos.GetAllCultivos());
                    lstCultivos = todos.Where(c => c.Nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase)).ToList();
                }
            }

            foreach (Cultivo cultivo in lstCultivos)
            {
                dgvCultivos.Rows.Add(cultivo.Id, cultivo.Nombre, cultivo.CultivoTipo, cultivo.CONS);
            }
        }

        public class ObjetoSeleccionadoEventArgs : EventArgs
        {
            public Modelos.Cultivo ObjetoSeleccionado { get; }

            public ObjetoSeleccionadoEventArgs(Modelos.Cultivo objeto)
            {
                ObjetoSeleccionado = objeto;
            }
        }

        public void Seleccionar()
        {
            // Obtener el ID de la primera columna
            int idSeleccionado = Convert.ToInt32(dgvCultivos.Rows[dgvCultivos.CurrentRow.Index].Cells[0].Value);

            // Buscar el objeto completo en la lista
            Cultivo objetoSeleccionado = lstCultivos.FirstOrDefault(cultivo => cultivo.Id == idSeleccionado);

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
            if (dgvCultivos.CurrentRow != null && dgvCultivos.CurrentRow.Index >= 0)
            {
                Seleccionar();
            }
        }

        private void dgvCultivos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Seleccionar();
            }
        }

        private void dgvCultivos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
    }
}
