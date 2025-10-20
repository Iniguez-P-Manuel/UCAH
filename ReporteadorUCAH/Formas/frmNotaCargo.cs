
using ReporteadorUCAH.DB_Services;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
            this.Load += NotaCargo_Load;
        }
        Modelos.NotaCargo NotaActual = new Modelos.NotaCargo();

        private string _prevToneladasText = "";
        private string _prevPrecioText = "";

        // Nueva bandera para suspender temporalmente los handlers cuando actualizamos programáticamente los TextBoxes
        private bool _suspendTextChanged = false;

        private void NotaCargo_Load(object sender, EventArgs e)
        {
            _prevToneladasText = (txtToneladas?.Text) ?? "";
            _prevPrecioText = (txtPrecio?.Text) ?? "";

            if (txtToneladas != null)
            {
                txtToneladas.KeyPress -= NumericTextBox_KeyPress;
                txtToneladas.KeyPress += NumericTextBox_KeyPress;
                txtToneladas.TextChanged -= TxtToneladas_TextChanged;
                txtToneladas.TextChanged += TxtToneladas_TextChanged;
            }

            if (txtPrecio != null)
            {
                txtPrecio.KeyPress -= NumericTextBox_KeyPress;
                txtPrecio.KeyPress += NumericTextBox_KeyPress;
                txtPrecio.TextChanged -= TxtPrecio_TextChanged;
                txtPrecio.TextChanged += TxtPrecio_TextChanged;
            }
        }


        private void btnDeducciones_Click(object sender, EventArgs e)
        {
            DeduccionesNota formDeducciones = new DeduccionesNota();
            formDeducciones.lstDeduccionesNota = this.NotaActual.Deducciones ?? new List<Modelos.DeduccionNota>();

            var result = formDeducciones.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.NotaActual.Deducciones = formDeducciones.lstDeduccionesNota ?? new List<Modelos.DeduccionNota>();
                RecalcularImporte();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BusquedaNotas formBusqueda = new BusquedaNotas();
            formBusqueda.ObjetoSeleccionado += BusquedaSeleccionada;
            formBusqueda.ShowDialog();
        }

        // ---------------------------
        // Selección Cliente / Cultivo
        // ---------------------------

        // Método público/handler para abrir búsqueda de clientes desde el formulario

        public void btnSeleccionarCliente_Click(object sender, EventArgs e)
        {
            BusquedaClientes formBusqueda = new BusquedaClientes();
            formBusqueda.ObjetoSeleccionado += (s, ev) =>
            {
                // Al seleccionar en la ventana de búsqueda se asigna al objeto NotaActual
                this.NotaActual._Cliente = ev.ObjetoSeleccionado;
                // Actualizar UI
                txtCliente.Text = this.NotaActual._Cliente?.Nombre ?? string.Empty;
            };
            formBusqueda.ShowDialog();
        }

        // Método público/handler para abrir búsqueda de cultivos desde el formulario
        public void btnSeleccionarCultivo_Click(object sender, EventArgs e)
        {
            BusquedaCultivos formBusqueda = new BusquedaCultivos();
            formBusqueda.ObjetoSeleccionado += (s, ev) =>
            {
                this.NotaActual._Cultivo = ev.ObjetoSeleccionado;
                txtCultivo.Text = this.NotaActual._Cultivo?.Nombre ?? string.Empty;
            };
            formBusqueda.ShowDialog();
        }

        private void BusquedaSeleccionada(object sender, BusquedaNotas.ObjetoSeleccionadoEventArgs e)
        {
            // Suspendemos los handlers antes de asignar a los TextBoxes programáticamente
            _suspendTextChanged = true;

            NotaActual = e.ObjetoSeleccionado;
            double totalDeducciones = NotaActual.Deducciones?.Sum(d => d.Importe) ?? 0;

            txtCliente.Text = NotaActual._Cliente?.Nombre ?? string.Empty;
            txtCultivo.Text = NotaActual._Cultivo?.Nombre ?? string.Empty;
            txtDeducciones.Text = Math.Round(totalDeducciones, 2).ToString("F2", CultureInfo.CurrentCulture);
            txtID.Text = NotaActual.Id.ToString();
            txtImporte.Text = NotaActual.Importe.ToString("F2", CultureInfo.CurrentCulture);

            // Asegurarnos de usar la culture actual al convertir a texto (evita que TextChanged haga parse invalido)
            txtPrecio.Text = NotaActual.Precio.ToString(CultureInfo.CurrentCulture);
            txtToneladas.Text = NotaActual.Tons.ToString(CultureInfo.CurrentCulture);

            // Actualizar previos para que los handlers no restauren un valor antiguo
            _prevPrecioText = txtPrecio.Text;
            _prevToneladasText = txtToneladas.Text;

            if (dpFecha != null && NotaActual.Fecha != DateTime.MinValue)
                dpFecha.Value = NotaActual.Fecha;

            // Rehabilitar handlers y recalcular importe
            _suspendTextChanged = false;

            // Recalcular importe explícitamente (por si hay diferencias de formato)
            RecalcularImporte();

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

            // Resetear previos
            _prevPrecioText = "";
            _prevToneladasText = "";
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

        public override void Eliminar()
        {
            base.Eliminar();
        }

        public override void Guardar()
        {
            base.Guardar();
        }

        // -----------------------------
        // Helpers y handlers requeridos por el diseñador
        // -----------------------------

        // KeyPress: bloquea caracteres no numéricos (permite separador decimal)
        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var decimalSeparator = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            var tb = sender as System.Windows.Forms.TextBox;

            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator)
            {
                if (tb != null && tb.Text.IndexOf(decimalSeparator) == -1)
                    return;
            }

            e.Handled = true;
        }

        // TextChanged handlers (nombres exactos que el diseñador buscó)
        private void TxtToneladas_TextChanged(object sender, EventArgs e)
        {
            // Si estamos actualizando programáticamente, no ejecutar la lógica de sanitización/reemplazo
            if (_suspendTextChanged) return;

            var tb = sender as System.Windows.Forms.TextBox;
            if (tb == null) return;

            // Sanitizar el texto (quita letras y deja un solo separador decimal)
            string sanitized = SanitizeDecimalText(tb.Text);
            if (sanitized != tb.Text)
            {
                int oldSel = tb.SelectionStart;
                tb.Text = sanitized;
                tb.SelectionStart = Math.Min(sanitized.Length, Math.Max(0, oldSel - 1));
            }

            if (IsValidDecimalText(tb.Text))
            {
                _prevToneladasText = tb.Text;
            }
            else
            {
                int sel = Math.Min(_prevToneladasText.Length, tb.SelectionStart);
                tb.Text = _prevToneladasText;
                tb.SelectionStart = sel;
            }

            RecalcularImporte();
        }

        private void TxtPrecio_TextChanged(object sender, EventArgs e)
        {
            // Si estamos actualizando programáticamente, no ejecutar la lógica de sanitización/reemplazo
            if (_suspendTextChanged) return;

            var tb = sender as System.Windows.Forms.TextBox;
            if (tb == null) return;

            string sanitized = SanitizeDecimalText(tb.Text);
            if (sanitized != tb.Text)
            {
                int oldSel = tb.SelectionStart;
                tb.Text = sanitized;
                tb.SelectionStart = Math.Min(sanitized.Length, Math.Max(0, oldSel - 1));
            }

            if (IsValidDecimalText(tb.Text))
            {
                _prevPrecioText = tb.Text;
            }
            else
            {
                int sel = Math.Min(_prevPrecioText.Length, tb.SelectionStart);
                tb.Text = _prevPrecioText;
                tb.SelectionStart = sel;
            }

            RecalcularImporte();
        }

        // Quita todo lo que no sea dígito o separador decimal y permite solo un separador
        private string SanitizeDecimalText(string text)
        {
            if (string.IsNullOrEmpty(text)) return text ?? "";

            var decimalSeparator = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            var sb = new StringBuilder();
            bool hasDecimal = false;

            foreach (char c in text)
            {
                if (char.IsDigit(c))
                {
                    sb.Append(c);
                }
                else if (c == decimalSeparator)
                {
                    if (!hasDecimal)
                    {
                        sb.Append(decimalSeparator);
                        hasDecimal = true;
                    }
                }
                // ignorar cualquier otro carácter
            }

            return sb.ToString();
        }

        // Permite vacío o número válido
        private bool IsValidDecimalText(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return true;
            return double.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out _);
        }

        // Método que recalcula el importe; si ya existe en otra parte del form, puedes eliminar esta copia
        private void RecalcularImporte()
        {
            double tons = 0;
            double precio = 0;

            double.TryParse(txtToneladas.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out tons);
            double.TryParse(txtPrecio.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out precio);

            double subtotal = tons * precio;
            double totalDeducciones = (NotaActual?.Deducciones?.Sum(d => d.Importe)) ?? 0;

            double importe = subtotal - totalDeducciones;

            // Actualizar objeto y UI (asegúrate que txtImporte y txtDeducciones existan)
            if (NotaActual != null)
            {
                NotaActual.Tons = tons;
                NotaActual.Precio = precio;
                NotaActual.Importe = importe;
            }

            if (txtImporte != null) txtImporte.Text = Math.Round(importe, 2).ToString("F2", CultureInfo.CurrentCulture);
            if (txtDeducciones != null) txtDeducciones.Text = Math.Round(totalDeducciones, 2).ToString("F2", CultureInfo.CurrentCulture);
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
            if (notaCargo._GrupoFamiliar != null)
            {
                rowNota["GrupoFamiliarId"] = notaCargo._GrupoFamiliar.Id;
                rowNota["GrupoFamiliarNombre"] = notaCargo._GrupoFamiliar.Nombre ?? "";
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
