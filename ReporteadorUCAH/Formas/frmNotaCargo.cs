
using ReporteadorUCAH.DB_Services;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ReporteadorUCAH.Formas.BusquedaNotas;

namespace ReporteadorUCAH.Formas
{
    public partial class frmNotaCargo : FormModel
    {
        public frmNotaCargo()
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

            dpFecha.Value = NotaActual.Fecha;
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

        public override void Reporte()
        {
            int idNota = 0;
            int.TryParse(txtID.Text, out idNota);
            if (idNota == 0 || NotaActual == null || NotaActual == new NotaCargo())
            {
                MessageBox.Show("No hay una nota seleccionada");
                return;
            }
            string rutaPDF = @"C:\Temp\MiNotaDeCargo.pdf";

            Reportes.PdfService service = new Reportes.PdfService();
            //service.MostrarVistaPrevia(NotaActual);
            service.GenerarYAbrir(NotaActual);
        }


        public DataSet ConvertirNotaCargoADataset(NotaCargo notaCargo)
        {
            DataSet ds = new DataSet("JSON");

            // === TABLA PRINCIPAL: NotaCargo ===
            DataTable dtNota = new DataTable("NotaCargo");

            // Columnas principales
            dtNota.Columns.Add("Id", typeof(int));
            dtNota.Columns.Add("Fecha", typeof(DateTime));
            dtNota.Columns.Add("FacturaFolio", typeof(string));
            dtNota.Columns.Add("Tons", typeof(decimal));
            dtNota.Columns.Add("Precio", typeof(decimal));
            dtNota.Columns.Add("Importe", typeof(decimal));
            dtNota.Columns.Add("FacturaUUID", typeof(string));

            // Columnas del Cliente
            dtNota.Columns.Add("ClienteId", typeof(int));
            dtNota.Columns.Add("ClienteNombre", typeof(string));
            dtNota.Columns.Add("ClienteRFC", typeof(string));
            dtNota.Columns.Add("ClienteTelefono", typeof(string));
            dtNota.Columns.Add("ClienteCorreo", typeof(string));
            dtNota.Columns.Add("ClienteDireccion", typeof(string));

            // Columnas del Cultivo
            dtNota.Columns.Add("CultivoId", typeof(int));
            dtNota.Columns.Add("CultivoNombre", typeof(string));
            dtNota.Columns.Add("CultivoTipo", typeof(string));

            // Columnas de Cosecha
            dtNota.Columns.Add("CosechaId", typeof(int));
            dtNota.Columns.Add("CosechaInicial", typeof(DateTime));
            dtNota.Columns.Add("CosechaFinal", typeof(DateTime));

            // Columnas de Grupo Familiar
            dtNota.Columns.Add("GrupoFamiliarId", typeof(int));
            dtNota.Columns.Add("GrupoFamiliarNombre", typeof(string));

            // Llenar datos principales
            DataRow rowNota = dtNota.NewRow();
            rowNota["Id"] = notaCargo.Id;
            rowNota["Fecha"] = notaCargo.Fecha;
            rowNota["FacturaFolio"] = notaCargo.FacturaFolio ?? "";
            rowNota["Tons"] = notaCargo.Tons;
            rowNota["Precio"] = notaCargo.Precio;
            rowNota["Importe"] = notaCargo.Importe;
            rowNota["FacturaUUID"] = notaCargo.FacturaUUID ?? "";

            // Datos del Cliente
            if (notaCargo._Cliente != null)
            {
                rowNota["ClienteId"] = notaCargo._Cliente.Id;
                rowNota["ClienteNombre"] = notaCargo._Cliente.Nombres ?? "";
                rowNota["ClienteRFC"] = notaCargo._Cliente.Rfc ?? "";
                rowNota["ClienteTelefono"] = notaCargo._Cliente.Telefono ?? "";
                rowNota["ClienteCorreo"] = notaCargo._Cliente.Correo ?? "";
                rowNota["ClienteDireccion"] = ObtenerDireccionCompleta(notaCargo._Cliente);
            }

            // Datos del Cultivo
            if (notaCargo._Cultivo != null)
            {
                rowNota["CultivoId"] = notaCargo._Cultivo.Id;
                rowNota["CultivoNombre"] = notaCargo._Cultivo.Nombre ?? "";
                rowNota["CultivoTipo"] = notaCargo._Cultivo.CultivoTipo ?? "";
            }

            // Datos de Cosecha
            if (notaCargo._Cosecha != null)
            {
                rowNota["CosechaId"] = notaCargo._Cosecha.Id;
                rowNota["CosechaInicial"] = notaCargo._Cosecha.FechaInicial;
                rowNota["CosechaFinal"] = notaCargo._Cosecha.FechaFinal;
            }

            // Datos de Grupo Familiar
            if (notaCargo._Cliente._GrupoFamiliar != null)
            {
                rowNota["GrupoFamiliarId"] = notaCargo._Cliente._GrupoFamiliar.Id;
                rowNota["GrupoFamiliarNombre"] = notaCargo._Cliente._GrupoFamiliar.Nombre ?? "";
            }

            dtNota.Rows.Add(rowNota);

            // === TABLA DEDUCCIONES ===
            DataTable dtDeducciones = new DataTable("Deducciones");
            dtDeducciones.Columns.Add("Id", typeof(int));
            dtDeducciones.Columns.Add("NotaCargoId", typeof(int));
            dtDeducciones.Columns.Add("DeduccionNombre", typeof(string));
            dtDeducciones.Columns.Add("GrupoDeduccion", typeof(string));
            dtDeducciones.Columns.Add("Importe", typeof(decimal));
            dtDeducciones.Columns.Add("ImporteFormateado", typeof(string));

            if (notaCargo.Deducciones != null)
            {
                foreach (var deduccion in notaCargo.Deducciones)
                {
                    DataRow rowDed = dtDeducciones.NewRow();
                    rowDed["Id"] = deduccion.Id;
                    rowDed["NotaCargoId"] = notaCargo.Id;
                    rowDed["Importe"] = deduccion.Importe;
                    rowDed["ImporteFormateado"] = deduccion.Importe.ToString("C2");

                    if (deduccion._Deduccion != null)
                    {
                        rowDed["DeduccionNombre"] = deduccion._Deduccion.Nombre ?? "";

                        if (deduccion._Deduccion._Grupo != null)
                        {
                            rowDed["GrupoDeduccion"] = deduccion._Deduccion._Grupo.Nombre ?? "";
                        }
                    }

                    dtDeducciones.Rows.Add(rowDed);
                }
            }

            // Agregar tablas al DataSet
            ds.Tables.Add(dtNota);
            ds.Tables.Add(dtDeducciones);

            return ds;
        }

        // Método helper para dirección completa
        private string ObtenerDireccionCompleta(Cliente cliente)
        {
            var partes = new List<string>();

            if (!string.IsNullOrEmpty(cliente.Calle))
                partes.Add(cliente.Calle);

            if (!string.IsNullOrEmpty(cliente.NoExterior))
                partes.Add($"No. {cliente.NoExterior}");

            if (!string.IsNullOrEmpty(cliente.NoInterior))
                partes.Add($"Int. {cliente.NoInterior}");

            if (cliente._Colonia != null && !string.IsNullOrEmpty(cliente._Colonia.Nombre))
                partes.Add($"Col. {cliente._Colonia.Nombre}");

            if (cliente._Ciudad != null && !string.IsNullOrEmpty(cliente._Ciudad.Nombre))
                partes.Add(cliente._Ciudad.Nombre);

            if (cliente._Municipio != null && !string.IsNullOrEmpty(cliente._Municipio.Nombre))
                partes.Add(cliente._Municipio.Nombre);

            if (cliente._Estado != null && !string.IsNullOrEmpty(cliente._Estado.Nombre))
                partes.Add(cliente._Estado.Nombre);

            if (!string.IsNullOrEmpty(cliente.CodigoPostal))
                partes.Add($"C.P. {cliente.CodigoPostal}");

            return string.Join(", ", partes);
        }

    }
}
