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


    public partial class BusquedaClientes : FormModel
    {
        public event EventHandler<ObjetoSeleccionadoEventArgs> ObjetoSeleccionado;

        private List<Modelos.Cliente> lstClientes = new List<Modelos.Cliente>();
        public BusquedaClientes()
        {
            InitializeComponent();

            this.BotonEliminar(false);
            this.BotonReporte(false);
            this.BotonNuevo(false);

            Color NuevoColor = Color.Khaki;
            this.CambiarColor(NuevoColor);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Buscar();
        }
        private async void Buscar()
        {
            string Busqueda = txtBusqueda.Text;
            dgvClientes.Rows.Clear();
            lstClientes.Clear();


            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (DB_Services.Clientes DB_Clientes = new DB_Services.Clientes(varCon))
                {
                    lstClientes = await EjecutarConLoading(() => {
                        return DB_Clientes.BuscarClientes(Busqueda);
                    });
                }
            }

            foreach (Modelos.Cliente cliente in lstClientes)
            {
                dgvClientes.Rows.Add(cliente.Id, cliente.Nombres, cliente.Calle, cliente.Rfc);
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
            public Modelos.Cliente ObjetoSeleccionado { get; }

            public ObjetoSeleccionadoEventArgs(Modelos.Cliente objeto)
            {
                ObjetoSeleccionado = objeto;
            }
        }

        public void Seleccionar()
        {
            // Obtener el ID de la primera columna
            int idSeleccionado = Convert.ToInt32(dgvClientes.Rows[dgvClientes.CurrentRow.Index].Cells[0].Value);

            // Buscar el objeto completo en la lista
            Modelos.Cliente objetoSeleccionado = lstClientes.FirstOrDefault(cliente => cliente.Id == idSeleccionado);

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
            if (dgvClientes.CurrentRow != null && dgvClientes.CurrentRow.Index >= 0 )
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
