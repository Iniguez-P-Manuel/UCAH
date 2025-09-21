using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ReporteadorUCAH.Formas.BusquedaNotas;

namespace ReporteadorUCAH.Formas
{
    public partial class NotaCargo : FormModel
    {
        public NotaCargo()
        {
            InitializeComponent();
        }
        Modelos.NotaCargo NotaActual = new Modelos.NotaCargo();


        private void btnDeducciones_Click(object sender, EventArgs e)
        {
            DeduccionesNota formDeducciones = new DeduccionesNota();
            formDeducciones.lstDeduccionesNota = this.NotaActual.Deducciones;
            formDeducciones.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BusquedaNotas formBusqueda = new BusquedaNotas();
            formBusqueda.ObjetoSeleccionado += BusquedaSeleccionada;
            formBusqueda.ShowDialog();
        }
        private void BusquedaSeleccionada(object sender, BusquedaNotas.ObjetoSeleccionadoEventArgs e)
        {
            NotaActual = e.ObjetoSeleccionado;
            double totalDeducciones = NotaActual.Deducciones?.Sum(d => d.Importe) ?? 0;

            txtCliente.Text = NotaActual._Cliente.Nombre;
            txtCultivo.Text = NotaActual._Cultivo.Nombre;
            txtDeducciones.Text = Math.Round(totalDeducciones, 2).ToString();
            txtID.Text = NotaActual.Id.ToString();
            txtImporte.Text = NotaActual.Importe.ToString();
            txtPrecio.Text = NotaActual.Precio.ToString();
            txtToneladas.Text = NotaActual.Tons.ToString();

        }
        public override void Nuevo()
        {
            //Limpiar objeto nota
            NotaActual = new Modelos.NotaCargo();

            // Limpiar TextBoxes
            txtCliente.Text = string.Empty;
            txtCultivo.Text = string.Empty;
            txtDeducciones.Text = "0.00";
            txtID.Text = string.Empty;
            txtImporte.Text = "0.00";
            txtPrecio.Text = string.Empty;
            txtToneladas.Text = string.Empty;
        }


    }
}
