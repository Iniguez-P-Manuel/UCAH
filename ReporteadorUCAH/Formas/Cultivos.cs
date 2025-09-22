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
    public partial class Cultivos : FormModel
    {
        public Cultivos()
        {
            InitializeComponent();
        }
        Modelos.Cultivo CultivoActual = new Modelos.Cultivo();

        private void button1_Click(object sender, EventArgs e)
        {
            BusquedaCultivos formBusqueda = new BusquedaCultivos();
            formBusqueda.ObjetoSeleccionado += BusquedaSeleccionada;
            formBusqueda.ShowDialog();
        }

        private void BusquedaSeleccionada(object sender, BusquedaCultivos.ObjetoSeleccionadoEventArgs e)
        {
            CultivoActual = e.ObjetoSeleccionado;

            txtboxNombreCultivo.Text = CultivoActual.Nombre;
            txtboxCultivo.Text = CultivoActual.CultivoTipo;
            txtboxCONS.Text = CultivoActual.CONS;
        }

        public override void Nuevo()
        {
            //Limpiar objeto nota
            CultivoActual = new Modelos.Cultivo();

            // Limpiar TextBoxes
            txtboxNombreCultivo.Text = string.Empty;
            txtboxCultivo.Text = string.Empty;
            txtboxCONS.Text = string.Empty;
        }

        public override void Guardar()
        {
            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(txtboxNombreCultivo.Text) ||
                string.IsNullOrWhiteSpace(txtboxCultivo.Text) ||
                string.IsNullOrWhiteSpace(txtboxCONS.Text))
            {
                MessageBox.Show("Por favor, llena todos los campos antes de guardar.", "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CultivoActual.Nombre = txtboxNombreCultivo.Text;
            CultivoActual.CultivoTipo = txtboxCultivo.Text;
            CultivoActual.CONS = txtboxCONS.Text;
            
            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (DB_Services.Cultivos dbCultivos = new DB_Services.Cultivos(varCon))
                {
                    if (CultivoActual.Id == 0)
                    {
                        // Es nuevo
                        dbCultivos.AgregarCultivo(CultivoActual);
                        MessageBox.Show("Cultivo agregado correctamente.");
                    }
                    else
                    {
                        // Es edición
                        dbCultivos.ActualizarCultivo(CultivoActual);
                        MessageBox.Show("Cultivo actualizado correctamente.");
                    }
                }
            }
            LimpiarFormulario();
        }

        public override void Eliminar()
        {
            if (CultivoActual == null || CultivoActual.Id == 0)
            {
                MessageBox.Show("No hay un cultivo seleccionado para eliminar.");
                return;
            }

            var confirm = MessageBox.Show($"¿Está seguro de eliminar el cultivo '{CultivoActual.Nombre}'?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                using (DatabaseConnection varCon = new DatabaseConnection())
                {
                    using (DB_Services.Cultivos dbCultivos = new DB_Services.Cultivos(varCon))
                    {
                        dbCultivos.EliminarCultivo(CultivoActual.Id);
                    }
                }
                MessageBox.Show("Cultivo eliminado correctamente.");
                LimpiarFormulario();
            }
        }

        private void LimpiarFormulario()
        {
            CultivoActual = new Cultivo();
            txtboxNombreCultivo.Text = "";
            txtboxCultivo.Text = "";
            txtboxCONS.Text = "";
        }
    }
}
