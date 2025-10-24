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

            txtID.Text = CultivoActual.Id.ToString();
            txtNombreCultivo.Text = CultivoActual.Nombre;
            txtCultivoTipo.Text = CultivoActual.CultivoTipo;
            txtCONS.Text = CultivoActual.CONS;
        }

        public override void Nuevo()
        {
            //Limpiar objeto nota
            CultivoActual = new Modelos.Cultivo();

            // Limpiar TextBoxes
            txtID.Text = string.Empty;
            txtNombreCultivo.Text = string.Empty;
            txtCultivoTipo.Text = string.Empty;
            txtCONS.Text = string.Empty;
        }

        public override void Guardar()
        {
            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(txtNombreCultivo.Text) ||
                string.IsNullOrWhiteSpace(txtCultivoTipo.Text))
            {
                MessageBox.Show("Por favor, llena todos los campos antes de guardar.", "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CultivoActual.Nombre = txtNombreCultivo.Text;
            CultivoActual.CultivoTipo = txtCultivoTipo.Text;
            CultivoActual.CONS = txtCONS.Text;
            
            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (DB_Services.Cultivos dbCultivos = new DB_Services.Cultivos(varCon))
                {
                    if (CultivoActual.Id == 0)
                    {
                        // Es nuevo
                        CultivoActual.Id = dbCultivos.AgregarCultivo(CultivoActual);
                        txtID.Text = CultivoActual.Id.ToString();
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
            txtID.Text = "";
            txtNombreCultivo.Text = "";
            txtCultivoTipo.Text = "";
            txtCONS.Text = "";
        }
    }
}
