
using Microsoft.Data.Sqlite;
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
        private System.Windows.Forms.TextBox txtFacturaUUID;
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

            // Configurar MaskedTextBox para UUID
            if (txtFacturaUUID != null)
            {
                // Convertir a mayúsculas automáticamente y validar caracteres
                txtFacturaUUID.KeyPress -= txtFacturaUUID_KeyPress;
                txtFacturaUUID.KeyPress += txtFacturaUUID_KeyPress;

                // Forzar mayúsculas en cada cambio de texto
                txtFacturaUUID.TextChanged -= txtFacturaUUID_TextChanged;
                txtFacturaUUID.TextChanged += txtFacturaUUID_TextChanged;
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
            _suspendTextChanged = true;

            NotaActual = e.ObjetoSeleccionado;

            // SOLUCIÓN: Buscar el UUID en ambas tablas
            if (NotaActual != null && NotaActual.Id > 0)
            {
                try
                {
                    using (var con = new DatabaseConnection())
                    {
                        using (var command = con.GetConnection().CreateCommand())
                        {
                            // PRIMERO: Intentar obtener directamente de LiquidacionNotasCargo
                            command.CommandText = "SELECT FacturaUUID FROM LiquidacionNotasCargo WHERE id = @Id";
                            command.Parameters.AddWithValue("@Id", NotaActual.Id);

                            var result = command.ExecuteScalar();

                            if (result != null && result != DBNull.Value && !string.IsNullOrEmpty(result.ToString()))
                            {
                                NotaActual.FacturaUUID = result.ToString();
                                MessageBox.Show($"✅ UUID encontrado en LiquidacionNotasCargo:\n{NotaActual.FacturaUUID}",
                                              "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                // SEGUNDO: Si no está en LiquidacionNotasCargo, buscar en Facturas
                                command.CommandText = @"
                            SELECT f.UUID 
                            FROM Facturas f
                            INNER JOIN LiquidacionNotasCargo lnc ON lnc.id = @Id
                            WHERE f.id = lnc.FacturaUUID";  // Asumiendo que FacturaUUID es ID numérico

                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@Id", NotaActual.Id);

                                result = command.ExecuteScalar();
                                if (result != null && result != DBNull.Value && !string.IsNullOrEmpty(result.ToString()))
                                {
                                    NotaActual.FacturaUUID = result.ToString();
                                    MessageBox.Show($"✅ UUID encontrado en Facturas:\n{NotaActual.FacturaUUID}",
                                                  "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("❌ No se encontró UUID en ninguna tabla\n" +
                                                  "El campo FacturaUUID probablemente está vacío",
                                                  "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    NotaActual.FacturaUUID = null;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Error al cargar UUID:\n{ex.Message}",
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    NotaActual.FacturaUUID = null;
                }
            }

            double totalDeducciones = NotaActual.Deducciones?.Sum(d => d.Importe) ?? 0;

            txtCliente.Text = NotaActual._Cliente?.Nombre ?? string.Empty;
            txtCultivo.Text = NotaActual._Cultivo?.Nombre ?? string.Empty;
            dpFechaInicial.Value = (NotaActual._Cosecha?.FechaInicial > dpFechaInicial.MinDate ? NotaActual._Cosecha.FechaInicial : DateTime.Today);
            dpFechaFinal.Value = (NotaActual._Cosecha?.FechaFinal > dpFechaFinal.MinDate ? NotaActual._Cosecha.FechaFinal : DateTime.Today);
            txtDeducciones.Text = Math.Round(totalDeducciones, 2).ToString("F2", CultureInfo.CurrentCulture);
            txtID.Text = NotaActual.Id.ToString();
            txtImporte.Text = NotaActual.Importe.ToString("F2", CultureInfo.CurrentCulture);
            txtPrecio.Text = NotaActual.Precio.ToString(CultureInfo.CurrentCulture);
            txtToneladas.Text = NotaActual.Tons.ToString(CultureInfo.CurrentCulture);

            // CARGAR EL UUID EN EL TEXTBOX
            if (txtFacturaUUID != null)
            {
                if (!string.IsNullOrEmpty(NotaActual.FacturaUUID))
                {
                    txtFacturaUUID.Text = NotaActual.FacturaUUID;
                }
                else
                {
                    txtFacturaUUID.Clear();
                }
            }

            _prevPrecioText = txtPrecio.Text;
            _prevToneladasText = txtToneladas.Text;

            if (dpFecha != null && NotaActual.Fecha != DateTime.MinValue)
                dpFecha.Value = NotaActual.Fecha;

            _suspendTextChanged = false;

            RecalcularImporte();

            if (NotaActual._Cosecha != null)
            {
                dpFechaInicial.Value = NotaActual._Cosecha.FechaInicial != DateTime.MinValue ? NotaActual._Cosecha.FechaInicial : DateTime.Today;
                dpFechaFinal.Value = NotaActual._Cosecha.FechaFinal != DateTime.MinValue ? NotaActual._Cosecha.FechaFinal : DateTime.Today;
            }
            else
            {
                dpFechaInicial.Value = DateTime.Today;
                dpFechaFinal.Value = DateTime.Today;
            }
        }
        public override void Nuevo()
        {
            NotaActual = new Modelos.NotaCargo();

            txtID.Text = string.Empty;
            txtCliente.Text = string.Empty;
            txtCultivo.Text = string.Empty;
            txtToneladas.Text = "0.00";
            txtPrecio.Text = "0.00";
            txtDeducciones.Text = "0.00";
            txtImporte.Text = "0.00";

            // Solo asignar si el control existe
            if (txtFacturaUUID != null)
            {
                _suspendUUIDTextChanged = true;
                txtFacturaUUID.Text = "________-____-____-____-____________";
                _suspendUUIDTextChanged = false;
            }

            dpFecha.Value = DateTime.Today;
            dpFechaInicial.Value = DateTime.Today;
            dpFechaFinal.Value = DateTime.Today;

            _prevPrecioText = "0.00";
            _prevToneladasText = "0.00";
            _prevUUIDText = "________-____-____-____-____________";
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
            if (NotaActual._Cliente == null)
            {
                MessageBox.Show("Debes seleccionar un cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (NotaActual._Cultivo == null)
            {
                MessageBox.Show("Debes seleccionar un cultivo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar UUID si se ingresó parcialmente
            if (!IsUUIDComplete() && !string.IsNullOrEmpty(GetUUIDWithoutFormatting()))
            {
                MessageBox.Show("El UUID está incompleto. Complete todos los caracteres o déjelo vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFacturaUUID.Focus();
                return;
            }

            NotaActual.Fecha = dpFecha.Value;
            NotaActual.Tons = double.TryParse(txtToneladas.Text, out double tons) ? tons : 0;
            NotaActual.Precio = double.TryParse(txtPrecio.Text, out double precio) ? precio : 0;
            NotaActual.FacturaUUID = GetActualUUID();

            RecalcularImporte();
            NotaActual.Importe = double.TryParse(txtImporte.Text, out double importe) ? importe : 0;

            bool esNuevo = NotaActual.Id == 0;

            try
            {
                int idCosechaParaGuardar = 0;

                using (var con = new DatabaseConnection())
                {
                    using (var dbCosechas = new DB_Services.Cosechas(con))
                    {
                        if (NotaActual._Cosecha == null)
                        {
                            NotaActual._Cosecha = new Cosecha();
                        }

                        NotaActual._Cosecha.FechaInicial = dpFechaInicial.Value;
                        NotaActual._Cosecha.FechaFinal = dpFechaFinal.Value;

                        if (NotaActual._Cosecha.Id == 0)
                        {
                            idCosechaParaGuardar = dbCosechas.AgregarCosecha(NotaActual._Cosecha);
                            NotaActual._Cosecha.Id = idCosechaParaGuardar;
                        }
                        else
                        {
                            var existe = dbCosechas.GetCosechaById(NotaActual._Cosecha.Id);
                            if (existe == null)
                            {
                                idCosechaParaGuardar = dbCosechas.AgregarCosecha(NotaActual._Cosecha);
                                NotaActual._Cosecha.Id = idCosechaParaGuardar;
                            }
                            else
                            {
                                dbCosechas.ActualizarCosecha(NotaActual._Cosecha);
                                idCosechaParaGuardar = NotaActual._Cosecha.Id;
                            }
                        }
                    }
                }

                using (var con = new DatabaseConnection())
                {
                    using (var dbNotas = new DB_Services.NotasCargo(con))
                    {
                        NotaActual._Cosecha.Id = idCosechaParaGuardar;

                        if (esNuevo)
                        {
                            int nuevoId = dbNotas.AgregarNotaCargo(NotaActual);
                            NotaActual.Id = nuevoId;
                            txtID.Text = nuevoId.ToString();
                            MessageBox.Show("Nota de cargo guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            int filasAfectadas = dbNotas.ActualizarNotaCargo(NotaActual);

                            if (filasAfectadas > 0)
                            {
                                MessageBox.Show("Nota de cargo actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No se pudo actualizar la nota.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        // VERIFICACIÓN USANDO EL MÉTODO DE NotasCargo
                        bool guardadoExitoso = dbNotas.VerificarGuardadoEnBaseDatos(NotaActual.Id);
                        if (guardadoExitoso)
                        {
                            Console.WriteLine("✓ Verificación: Nota guardada correctamente en base de datos");
                        }
                        else
                        {
                            Console.WriteLine("❌ Verificación: La nota no se encontró en base de datos");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -----------------------------
        // Helpers y handlers requeridos por el diseñador
        // -----------------------------

        //KeyPress: Bloquea caracteres minuscula y mantiene uso de separador '-'
        private string _prevUUIDText = "";
        private bool _suspendUUIDTextChanged = false;

        // MÉTODOS PARA MANEJO DE UUID CON MASKEDTEXTBOX - MEJORADOS
        private void txtFacturaUUID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            // Convertir a mayúsculas automáticamente
            e.KeyChar = char.ToUpper(e.KeyChar);

            // Validar que solo sean caracteres A-F y 0-9
            if (!((e.KeyChar >= 'A' && e.KeyChar <= 'F') ||
                  (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void txtFacturaUUID_TextChanged(object sender, EventArgs e)
        {
            // Asegurar que todo esté en mayúsculas
            var maskedBox = sender as MaskedTextBox;
            if (maskedBox == null) return;

            // Obtener la posición actual del cursor
            int cursorPosition = maskedBox.SelectionStart;

            // Convertir todo a mayúsculas
            string currentText = maskedBox.Text;
            string upperText = currentText.ToUpper();

            // Solo actualizar si hay cambios
            if (currentText != upperText)
            {
                maskedBox.Text = upperText;
                // Restaurar la posición del cursor
                maskedBox.SelectionStart = cursorPosition;
            }
        }

        private string GetActualUUID()
        {
            if (txtFacturaUUID == null) return null;

            string textWithFormat = txtFacturaUUID.Text;

            // Si el texto contiene placeholders (_), no está completo
            if (string.IsNullOrEmpty(textWithFormat) || textWithFormat.Contains("_"))
                return null;

            return textWithFormat;
        }

        private string GetUUIDWithoutFormatting()
        {
            if (txtFacturaUUID == null) return "";
            return txtFacturaUUID.Text.Replace("-", "").Replace("_", "");
        }

        private bool IsUUIDComplete()
        {
            if (txtFacturaUUID == null) return true; // Si no existe, considerar completo
            return !txtFacturaUUID.Text.Contains("_");
        }

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
