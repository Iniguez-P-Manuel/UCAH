
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
        public frmNotaCargo()
        {
            InitializeComponent();
            // INICIALIZACIÓN TEMPORAL - eliminar después de probar
            InicializarControles();
            this.Size = new Size(677, 563);
            this.Load += NotaCargo_Load;
        }
        Modelos.NotaCargo NotaActual = new Modelos.NotaCargo();

        private string _prevToneladasText = "";
        private string _prevPrecioText = "";
        

        // Nueva bandera para suspender temporalmente los handlers cuando actualizamos programáticamente los TextBoxes
        private bool _suspendTextChanged = false;
        private bool _suspendUUIDTextChanged = false; // ← AGREGAR esta línea

        private void NotaCargo_Load(object sender, EventArgs e)
        {
            this.BotonEliminar(false);

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
            if (maskedtxtFacturaUUID != null)
            {
                // Remover todos los eventos primero
                maskedtxtFacturaUUID.KeyPress -= txtFacturaUUID_KeyPress;
                maskedtxtFacturaUUID.KeyUp -= txtFacturaUUID_KeyUp;
                maskedtxtFacturaUUID.TextChanged -= txtFacturaUUID_TextChanged;
                maskedtxtFacturaUUID.Enter -= txtFacturaUUID_Enter;

                // Agregar eventos
                maskedtxtFacturaUUID.KeyPress += txtFacturaUUID_KeyPress;
                maskedtxtFacturaUUID.KeyUp += txtFacturaUUID_KeyUp;
                maskedtxtFacturaUUID.TextChanged += txtFacturaUUID_TextChanged;
                maskedtxtFacturaUUID.Enter += txtFacturaUUID_Enter;
            }
        }

        private void InicializarControles()
        {
            // Buscar el control si es null
            if (maskedtxtFacturaUUID == null)
            {
                foreach (Control control in this.Controls)
                {
                    if (control.Name == "maskedtxtFacturaUUID" && control is MaskedTextBox)
                    {
                        maskedtxtFacturaUUID = (MaskedTextBox)control;
                        break;
                    }
                }

                // Si aún es null, mostrar mensaje
                if (maskedtxtFacturaUUID == null)
                {
                    MessageBox.Show("No se encontró el control maskedtxtFacturaUUID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("maskedtxtFacturaUUID encontrado y asignado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
            _suspendUUIDTextChanged = true;

            NotaActual = e.ObjetoSeleccionado;

            double totalDeducciones = NotaActual.Deducciones?.Sum(d => d.Importe) ?? 0;

            // Limpiar y cargar todos los campos
            txtCliente.Text = NotaActual._Cliente?.Nombre ?? string.Empty;
            txtCultivo.Text = NotaActual._Cultivo?.Nombre ?? string.Empty;
            dpFechaInicial.Value = (NotaActual._Cosecha?.FechaInicial > dpFechaInicial.MinDate ? NotaActual._Cosecha.FechaInicial : DateTime.Today);
            dpFechaFinal.Value = (NotaActual._Cosecha?.FechaFinal > dpFechaFinal.MinDate ? NotaActual._Cosecha.FechaFinal : DateTime.Today);
            txtDeducciones.Text = Math.Round(totalDeducciones, 2).ToString("F2", CultureInfo.CurrentCulture);
            txtID.Text = NotaActual.Id.ToString();
            txtImporte.Text = NotaActual.Importe.ToString("F2", CultureInfo.CurrentCulture);
            txtPrecio.Text = NotaActual.Precio.ToString(CultureInfo.CurrentCulture);
            txtToneladas.Text = NotaActual.Tons.ToString(CultureInfo.CurrentCulture);

            // SOLUCIÓN DEFINITIVA para el MaskedTextBox
            if (maskedtxtFacturaUUID != null)
            {
                _suspendUUIDTextChanged = true;

                // Método más efectivo para limpiar y cargar
                maskedtxtFacturaUUID.Text = ""; // Primero limpiar
                maskedtxtFacturaUUID.ResetText();

                if (!string.IsNullOrEmpty(NotaActual.FacturaUUID))
                {
                    // Forzar la asignación del texto
                    maskedtxtFacturaUUID.Text = NotaActual.FacturaUUID.ToUpper();
                }
                else
                {
                    // Asegurar que esté completamente limpio
                    maskedtxtFacturaUUID.Text = "";
                    maskedtxtFacturaUUID.ResetText();
                }

                _suspendUUIDTextChanged = false;
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

            if (maskedtxtFacturaUUID != null)
            {
                _suspendUUIDTextChanged = true;
                maskedtxtFacturaUUID.Text = "";
                maskedtxtFacturaUUID.ResetText();
                // Forzar un refresh
                maskedtxtFacturaUUID.Refresh();
                _suspendUUIDTextChanged = false;
            }

            dpFecha.Value = DateTime.Today;
            dpFechaInicial.Value = DateTime.Today;
            dpFechaFinal.Value = DateTime.Today;

            _prevPrecioText = "0.00";
            _prevToneladasText = "0.00";
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
            if (NotaActual == null || NotaActual.Id == 0)
            {
                MessageBox.Show("No hay una nota de cargo seleccionada para eliminar.", "Información",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Mostrar información de la nota a eliminar
            string infoNota = $"ID: {NotaActual.Id}\n" +
                             $"Cliente: {NotaActual._Cliente?.Nombre ?? "N/A"}\n" +
                             $"Cultivo: {NotaActual._Cultivo?.Nombre ?? "N/A"}\n" +
                             $"Fecha: {NotaActual.Fecha:dd/MM/yyyy}\n" +
                             $"Importe: {NotaActual.Importe:C}";

            // Confirmar con el usuario
            var resultado = MessageBox.Show(
                $"¿Está seguro que desea ELIMINAR PERMANENTEMENTE la nota de cargo?\n\n" +
                $"{infoNota}\n\n" +
                "⚠️ Esta acción no se puede deshacer.",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );

            if (resultado != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (var con = new DatabaseConnection())
                {
                    using (var dbNotas = new DB_Services.NotasCargo(con))
                    {
                        // Eliminar la nota de cargo
                        bool eliminado = dbNotas.EliminarNotaCargo(NotaActual.Id);

                        if (eliminado)
                        {
                            MessageBox.Show("Nota de cargo eliminada permanentemente.",
                                           "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Limpiar el formulario después de eliminar
                            Nuevo();
                        }
                        else
                        {
                            MessageBox.Show($"No se pudo eliminar la nota de cargo #${NotaActual.Id}.\n\n" +
                                           "Posibles causas:\n" +
                                           "- La nota no existe en la base de datos\n" +
                                           "- Hay restricciones de clave foránea\n" +
                                           "- Error de conexión a la base de datos",
                                           "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la nota: {ex.Message}\n\nStackTrace: {ex.StackTrace}",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void Guardar()
        {
            //Validaciones...
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

            // MEJORAR validación UUID
            string uuidIngresado = GetActualUUID();
            string uuidSinFormato = GetUUIDWithoutFormatting();

            // Solo validar si el usuario intentó ingresar un UUID
            if (!string.IsNullOrEmpty(uuidSinFormato) && uuidSinFormato.Length > 0)
            {
                if (uuidIngresado == null)
                {
                    MessageBox.Show("El UUID está incompleto. Complete todos los caracteres o déjelo vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    maskedtxtFacturaUUID.Focus();
                    return;
                }

                if (uuidSinFormato.Length != 32)
                {
                    MessageBox.Show("El UUID debe tener exactamente 32 caracteres hexadecimales.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    maskedtxtFacturaUUID.Focus();
                    return;
                }
            }

            // Asignar datos del formulario al objeto
            NotaActual.Fecha = dpFecha.Value;
            NotaActual.Tons = double.TryParse(txtToneladas.Text, out double tons) ? tons : 0;
            NotaActual.Precio = double.TryParse(txtPrecio.Text, out double precio) ? precio : 0;
            NotaActual.FacturaUUID = uuidIngresado; // Usar la variable ya validada
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

        // MÉTODOS PARA MANEJO DE UUID CON MASKEDTEXTBOX - MEJORADOS
        private void txtFacturaUUID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Para MaskedTextBox, manejamos la entrada de caracteres
            if (char.IsControl(e.KeyChar))
                return;

            e.KeyChar = char.ToUpper(e.KeyChar);

            // Permitir solo caracteres hexadecimales (0-9, A-F)
            if (!((e.KeyChar >= 'A' && e.KeyChar <= 'F') ||
                  (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void txtFacturaUUID_KeyUp(object sender, KeyEventArgs e)
        {
            // Forzar mayúsculas después de cada tecla
            if (_suspendUUIDTextChanged) return;

            var maskedBox = sender as MaskedTextBox;
            if (maskedBox == null) return;

            string currentText = maskedBox.Text;
            string upperText = currentText.ToUpper();

            // Solo actualizar si hay cambios y no estamos en medio de una actualización
            if (currentText != upperText)
            {
                _suspendUUIDTextChanged = true;

                int cursorPos = maskedBox.SelectionStart;
                bool hasSelection = maskedBox.SelectionLength > 0;

                maskedBox.Text = upperText;

                // Restaurar posición del cursor
                if (!hasSelection)
                {
                    maskedBox.SelectionStart = Math.Min(cursorPos, upperText.Length);
                }

                _suspendUUIDTextChanged = false;
            }
        }

        private void txtFacturaUUID_TextChanged(object sender, EventArgs e)
        {
            if (_suspendUUIDTextChanged) return;

            var maskedBox = sender as MaskedTextBox;
            if (maskedBox == null) return;

            // Convertir a mayúsculas como respaldo
            string currentText = maskedBox.Text;
            string upperText = currentText.ToUpper();

            if (currentText != upperText)
            {
                _suspendUUIDTextChanged = true;
                int cursorPos = maskedBox.SelectionStart;
                maskedBox.Text = upperText;
                maskedBox.SelectionStart = Math.Min(cursorPos, upperText.Length);
                _suspendUUIDTextChanged = false;
            }
        }

        

        private void txtFacturaUUID_Enter(object sender, EventArgs e)
        {
            // Seleccionar todo el texto al entrar (opcional)
            var maskedBox = sender as MaskedTextBox;
            if (maskedBox != null && string.IsNullOrEmpty(maskedBox.Text.Replace("_", "").Replace("-", "")))
            {
                maskedBox.SelectAll();
            }
        }

        private string GetActualUUID()
        {
            {
                if (maskedtxtFacturaUUID == null) return null;

                string texto = maskedtxtFacturaUUID.Text;

                // Verificar si realmente hay un UUID válido
                if (string.IsNullOrEmpty(texto) ||
                    texto.Replace("-", "").Replace("_", "").Length == 0)
                    return null;

                // Si tiene guiones bajos, no está completo
                if (texto.Contains("_"))
                    return null;

                // Validar formato básico (debe tener 36 caracteres con guiones)
                if (texto.Length != 36)
                    return null;

                return texto.ToUpper();
            }
        }

            private string GetUUIDWithoutFormatting()
        {
            if (maskedtxtFacturaUUID == null) return "";
            return maskedtxtFacturaUUID.Text.Replace("-", "").Replace("_", "");
        }

        private bool IsUUIDComplete()
        {
            if (maskedtxtFacturaUUID == null) return true;
            return !maskedtxtFacturaUUID.Text.Contains("_");
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

            double importe = tons * precio;
            double totalDeducciones = (NotaActual?.Deducciones?.Sum(d => d.Importe)) ?? 0;


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
