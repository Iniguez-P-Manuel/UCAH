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
    public partial class AgregarGrupoFamiliar : FormModel
    {
        public event EventHandler<ObjetoSeleccionadoEventArgs> ObjetoSeleccionado;

        private List<Modelos.GrupoFamiliar> lstGrupoFamiliar = new List<Modelos.GrupoFamiliar>();

        Modelos.GrupoFamiliar grupoActual = new Modelos.GrupoFamiliar();

        public AgregarGrupoFamiliar()
        {
            InitializeComponent();
        }

        private void AgregarGrupoFamiliar_Load(object sender, EventArgs e)
        {
            this.BotonEliminar(false);
            this.BotonReporte(false);
            this.CambiarColor(Color.Khaki);

            Buscar();
        }
        public override void Nuevo()
        {
            txtID.Text = "";
            txtNombre.Text = "";
            grupoActual = new Modelos.GrupoFamiliar();
        }


        private async void Buscar()
        {
            string Busqueda = txtBusqueda.Text;
            dgvGrupofamiliar.Rows.Clear();
            lstGrupoFamiliar.Clear();


            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (DB_Services.GruposFamiliares DB_GruposFamiliares = new DB_Services.GruposFamiliares(varCon))
                {
                    lstGrupoFamiliar = await EjecutarConLoading(() =>
                    {
                        return DB_GruposFamiliares.BuscarGrupo(Busqueda);
                    });
                }
            }

            foreach (Modelos.GrupoFamiliar grupo in lstGrupoFamiliar)
            {
                dgvGrupofamiliar.Rows.Add(grupo.Id, grupo.Nombre);
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            Buscar();
        }

        private void dgvGrupofamiliar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Obtener el ID de la primera columna
                int idSeleccionado = Convert.ToInt32(dgvGrupofamiliar.Rows[dgvGrupofamiliar.CurrentRow.Index].Cells[0].Value);

                // Buscar el objeto completo en la lista
                Modelos.GrupoFamiliar objetoSeleccionado = lstGrupoFamiliar.FirstOrDefault(grupo => grupo.Id == idSeleccionado);

                if (objetoSeleccionado != null)
                {
                    grupoActual = objetoSeleccionado;
                    txtID.Text = objetoSeleccionado.Id.ToString();
                    txtNombre.Text = objetoSeleccionado.Nombre;
                }
            }
        }

        public override void Guardar()
        {
            if (txtID.Text.Equals(""))
            {
                if (txtNombre.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Selecciona un grupo o escribe el nombre para guardar uno nuevo");
                    return;
                }
                else
                {
                    using (DatabaseConnection varCon = new DatabaseConnection())
                    {
                        using (DB_Services.GruposFamiliares dbGruposFamiliares = new DB_Services.GruposFamiliares(varCon))
                        {
                            grupoActual.Nombre = txtNombre.Text;
                            // Es nuevo
                            grupoActual.Id = dbGruposFamiliares.AgregarGrupo(grupoActual);
                            MessageBox.Show("Grupo familiar agregado correctamente.");
                        }
                    }
                }
            }
            else
            {
                if (txtNombre.Text.Trim().Equals(""))
                {
                    MessageBox.Show("El nombre es requerido");
                    return;
                }
                using (DatabaseConnection varCon = new DatabaseConnection())
                {
                    using (DB_Services.GruposFamiliares dbGruposFamiliares = new DB_Services.GruposFamiliares(varCon))
                    {
                        grupoActual.Nombre = txtNombre.Text;
                        //update
                        dbGruposFamiliares.ActualizarGrupo(grupoActual);
                        MessageBox.Show("Grupo familiar actualizado correctamente.");
                    }
                }
            }

            // Disparar el evento
            ObjetoSeleccionado?.Invoke(this, new ObjetoSeleccionadoEventArgs(grupoActual));

            // Cerrar la ventana si es necesario
            this.Close();
        }



        public class ObjetoSeleccionadoEventArgs : EventArgs
        {
            public Modelos.GrupoFamiliar ObjetoSeleccionado { get; }

            public ObjetoSeleccionadoEventArgs(Modelos.GrupoFamiliar objeto)
            {
                ObjetoSeleccionado = objeto;
            }
        }









    }
}
