
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
            txtIdCosecha.Text = NotaActual._Cosecha?.Id.ToString() ?? "0";
            dpFechaInicial.Value = (NotaActual._Cosecha?.FechaInicial > dpFechaInicial.MinDate ? NotaActual._Cosecha.FechaInicial : DateTime.Today);
            dpFechaFinal.Value = (NotaActual._Cosecha?.FechaFinal > dpFechaFinal.MinDate ? NotaActual._Cosecha.FechaFinal : DateTime.Today);
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

            // Mostrar rango de cosecha
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
            // Limpiar objeto nota
            NotaActual = new Modelos.NotaCargo();

            // Limpiar UI
            txtID.Text = string.Empty;
            txtCliente.Text = string.Empty;
            txtCultivo.Text = string.Empty;
            txtToneladas.Text = "0.00";
            txtPrecio.Text = "0.00";
            txtDeducciones.Text = "0.00";
            txtImporte.Text = "0.00";
            dpFecha.Value = DateTime.Today;
            dpFechaInicial.Value = DateTime.Today;
            dpFechaFinal.Value = DateTime.Today;
            txtIdCosecha.Text = "0";

            // Resetear previos
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
            base.Eliminar();
        }

        public override void Guardar()
        {
            // Validar datos obligatorios
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

            // Asignar datos del formulario
            NotaActual.Fecha = dpFecha.Value;
            NotaActual.Tons = double.TryParse(txtToneladas.Text, out double tons) ? tons : 0;
            NotaActual.Precio = double.TryParse(txtPrecio.Text, out double precio) ? precio : 0;

            // Recalcular importe antes de guardar
            RecalcularImporte();
            NotaActual.Importe = double.TryParse(txtImporte.Text, out double importe) ? importe : 0;

            bool esNuevo = NotaActual.Id == 0;

            try
            {
                int idCosechaParaGuardar = 0;

                // === 1. GUARDAR COSECHA ===
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

                        Console.WriteLine($"DEBUG - Fechas cosecha a guardar: {NotaActual._Cosecha.FechaInicial:dd/MM/yyyy} - {NotaActual._Cosecha.FechaFinal:dd/MM/yyyy}");

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

                        txtIdCosecha.Text = idCosechaParaGuardar.ToString();
                        Console.WriteLine($"DEBUG - ID Cosecha obtenido: {idCosechaParaGuardar}");
                    }
                }

                // === 2. VERIFICAR ANTES DE GUARDAR LA NOTA ===
                Console.WriteLine($"DEBUG - Antes de guardar nota:");
                Console.WriteLine($"  - ID Nota: {NotaActual.Id}");
                Console.WriteLine($"  - ID Cosecha: {idCosechaParaGuardar}");
                Console.WriteLine($"  - Tons: {NotaActual.Tons}");
                Console.WriteLine($"  - Precio: {NotaActual.Precio}");
                Console.WriteLine($"  - Importe: {NotaActual.Importe}");

                // === 3. GUARDAR NOTA - CON MÉTODO ALTERNATIVO SI FALLA ===
                bool guardadoExitoso = false;

                using (var con = new DatabaseConnection())
                {
                    using (var dbNotas = new DB_Services.NotasCargo(con))
                    {
                        // Asegurar que la cosecha esté asignada
                        NotaActual._Cosecha.Id = idCosechaParaGuardar;

                        if (esNuevo)
                        {
                            int nuevoId = dbNotas.AgregarNotaCargo(NotaActual);
                            NotaActual.Id = nuevoId;
                            txtID.Text = nuevoId.ToString();
                            guardadoExitoso = true;
                            MessageBox.Show("Nota de cargo guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Intentar actualizar normalmente
                            int filasAfectadas = dbNotas.ActualizarNotaCargo(NotaActual);

                            if (filasAfectadas > 0)
                            {
                                guardadoExitoso = true;
                                MessageBox.Show("Nota de cargo actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                // **SI FALLA, USAR MÉTODO ALTERNATIVO**
                                guardadoExitoso = ActualizarNotaManual(NotaActual, idCosechaParaGuardar);
                            }
                        }
                    }
                }

                if (guardadoExitoso)
                {
                    VerificarGuardadoEnBaseDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método alternativo para actualizar la nota manualmente INCLUYENDO DEDUCCIONES
        private bool ActualizarNotaManual(NotaCargo nota, int idCosecha)
        {
            try
            {
                using (var con = new DatabaseConnection())
                {
                    using (var transaction = con.GetConnection().BeginTransaction())
                    {
                        try
                        {
                            // 1. ACTUALIZAR NOTA PRINCIPAL
                            using (var command = con.GetConnection().CreateCommand())
                            {
                                command.CommandText = @"
                                    UPDATE LiquidacionNotasCargo 
                                    SET FECHA = @Fecha,
                                        idCliente = @idCliente,
                                        idCultivo = @idCultivo,
                                        idCosecha = @idCosecha,
                                        TONS = @Tons,
                                        PRECIO = @Precio,
                                        IMPORTE = @Importe
                                    WHERE id = @Id";

                                command.Parameters.AddWithValue("@Fecha", nota.Fecha);
                                command.Parameters.AddWithValue("@idCliente", nota._Cliente?.Id ?? 0);
                                command.Parameters.AddWithValue("@idCultivo", nota._Cultivo?.Id ?? 0);
                                command.Parameters.AddWithValue("@idCosecha", idCosecha);
                                command.Parameters.AddWithValue("@Tons", nota.Tons);
                                command.Parameters.AddWithValue("@Precio", nota.Precio);
                                command.Parameters.AddWithValue("@Importe", nota.Importe);
                                command.Parameters.AddWithValue("@Id", nota.Id);

                                int filasAfectadas = command.ExecuteNonQuery();

                                if (filasAfectadas == 0)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("No se pudo actualizar la nota principal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return false;
                                }
                            }

                            // 2. ELIMINAR DEDUCCIONES EXISTENTES
                            using (var command = con.GetConnection().CreateCommand())
                            {
                                command.CommandText = "DELETE FROM DeduccionesNota WHERE idNotaLiquidacion = @IdNota";
                                command.Parameters.AddWithValue("@IdNota", nota.Id);
                                command.ExecuteNonQuery();
                            }

                            // 3. INSERTAR NUEVAS DEDUCCIONES
                            if (nota.Deducciones != null && nota.Deducciones.Any())
                            {
                                foreach (var deduccion in nota.Deducciones)
                                {
                                    using (var command = con.GetConnection().CreateCommand())
                                    {
                                        command.CommandText = @"
                                            INSERT INTO DeduccionesNota 
                                            (idNotaLiquidacion, idDeduccion, Importe) 
                                            VALUES (@idNotaLiquidacion, @idDeduccion, @Importe)";

                                        command.Parameters.AddWithValue("@idNotaLiquidacion", nota.Id);
                                        command.Parameters.AddWithValue("@idDeduccion", deduccion._Deduccion?.Id ?? 0);
                                        command.Parameters.AddWithValue("@Importe", deduccion.Importe);

                                        command.ExecuteNonQuery();
                                    }
                                }
                            }

                            transaction.Commit();

                            // 4. ACTUALIZAR EL OBJETO EN MEMORIA
                            ActualizarDeduccionesEnObjeto(nota);

                            MessageBox.Show("Nota y deducciones actualizadas correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception($"Error en transacción: {ex.Message}", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en actualización manual: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Método para verificar inmediatamente si los datos se guardaron
        private void VerificarGuardadoEnBaseDatos()
        {
            try
            {
                using (var con = new DatabaseConnection())
                {
                    using (var command = con.GetConnection().CreateCommand())
                    {
                        command.CommandText = @"
                            SELECT 
                                lnc.TONS, 
                                lnc.PRECIO, 
                                lnc.IMPORTE,
                                lnc.idCosecha,
                                cos.fechaInicial,
                                cos.fechaFinal,
                                (SELECT SUM(Importe) FROM DeduccionesNota WHERE idNotaLiquidacion = @IdNota) as TotalDeducciones
                            FROM LiquidacionNotasCargo lnc
                            LEFT JOIN Cosecha cos ON cos.id = lnc.idCosecha
                            WHERE lnc.id = @IdNota";

                        command.Parameters.AddWithValue("@IdNota", NotaActual.Id);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                double tonsBD = reader.GetDouble(0);
                                double precioBD = reader.GetDouble(1);
                                double importeBD = reader.GetDouble(2);
                                int idCosechaBD = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                                DateTime? fechaInicialBD = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                                DateTime? fechaFinalBD = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5);
                                double deduccionesBD = reader.IsDBNull(6) ? 0 : reader.GetDouble(6);

                                string mensaje = "VERIFICACIÓN COMPLETA:\n\n";

                                // Verificar toneladas
                                if (Math.Abs(tonsBD - NotaActual.Tons) < 0.01)
                                    mensaje += "✓ Toneladas correctas\n";
                                else
                                    mensaje += $"❌ Toneladas: BD={tonsBD}, Esperado={NotaActual.Tons}\n";

                                // Verificar precio
                                if (Math.Abs(precioBD - NotaActual.Precio) < 0.01)
                                    mensaje += "✓ Precio correcto\n";
                                else
                                    mensaje += $"❌ Precio: BD={precioBD}, Esperado={NotaActual.Precio}\n";

                                // Verificar importe
                                if (Math.Abs(importeBD - NotaActual.Importe) < 0.01)
                                    mensaje += "✓ Importe correcto\n";
                                else
                                    mensaje += $"❌ Importe: BD={importeBD}, Esperado={NotaActual.Importe}\n";

                                // Verificar cosecha
                                if (idCosechaBD > 0)
                                    mensaje += $"✓ idCosecha: {idCosechaBD}\n";
                                else
                                    mensaje += "❌ idCosecha: NULL\n";

                                // Verificar fechas
                                if (fechaInicialBD.HasValue && fechaFinalBD.HasValue)
                                    mensaje += $"✓ Fechas: {fechaInicialBD:dd/MM/yyyy} - {fechaFinalBD:dd/MM/yyyy}\n";
                                else
                                    mensaje += "❌ Fechas cosecha: NULL\n";

                                // Verificar deducciones
                                double deduccionesEsperadas = NotaActual.Deducciones?.Sum(d => d.Importe) ?? 0;
                                if (Math.Abs(deduccionesBD - deduccionesEsperadas) < 0.01)
                                    mensaje += "✓ Deducciones correctas\n";
                                else
                                    mensaje += $"❌ Deducciones: BD={deduccionesBD:C}, Esperado={deduccionesEsperadas:C}\n";

                                MessageBox.Show(mensaje, "Verificación Completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en verificación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Métodos helpers para manejar valores NULL de la base de datos
        private string GetStringOrNull(SqliteDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer columna {columnName}: {ex.Message}");
                return null;
            }
        }

        private int? GetInt32OrNull(SqliteDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? (int?)null : reader.GetInt32(ordinal);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer columna {columnName}: {ex.Message}");
                return null;
            }
        }

        private double? GetDoubleOrNull(SqliteDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return reader.IsDBNull(ordinal) ? (double?)null : reader.GetDouble(ordinal);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer columna {columnName}: {ex.Message}");
                return null;
            }
        }

        // Método más robusto para actualizar las deducciones
        private void ActualizarDeduccionesEnObjeto(NotaCargo nota)
        {
            try
            {
                // Recargar las deducciones desde la base de datos
                using (var con = new DatabaseConnection())
                {
                    var deduccionesActualizadas = new List<DeduccionNota>();

                    using (var command = con.GetConnection().CreateCommand())
                    {
                        command.CommandText = @"
                            SELECT dn.id, dn.idNotaLiquidacion, dn.Importe,
                                   td.id as TipoDeduccionId, td.Nombre as TipoDeduccionNombre,
                                   gd.id as GrupoDeduccionId, gd.Nombre as GrupoDeduccionNombre
                            FROM DeduccionesNota dn
                            INNER JOIN TipoDeducciones td ON td.id = dn.idDeduccion
                            LEFT JOIN GrupoDeducciones gd ON gd.id = td.idGrupo
                            WHERE dn.idNotaLiquidacion = @IdNota";

                        command.Parameters.AddWithValue("@IdNota", nota.Id);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var deduccion = new DeduccionNota
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    Importe = reader.GetDouble(reader.GetOrdinal("Importe")),
                                    _Deduccion = new TipoDeduccion
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("TipoDeduccionId")),
                                        Nombre = GetStringOrNull(reader, "TipoDeduccionNombre")
                                    }
                                };

                                // Manejar el grupo de deducción de forma segura
                                var grupoId = GetInt32OrNull(reader, "GrupoDeduccionId");
                                var grupoNombre = GetStringOrNull(reader, "GrupoDeduccionNombre");

                                if (grupoId.HasValue || !string.IsNullOrEmpty(grupoNombre))
                                {
                                    deduccion._Deduccion._Grupo = new GrupoDeducciones
                                    {
                                        Id = grupoId ?? 0,
                                        Nombre = grupoNombre
                                    };
                                }

                                deduccionesActualizadas.Add(deduccion);
                            }
                        }
                    }

                    // Actualizar el objeto en memoria
                    nota.Deducciones = deduccionesActualizadas;

                    // Actualizar la UI
                    double totalDeducciones = nota.Deducciones.Sum(d => d.Importe);
                    txtDeducciones.Text = Math.Round(totalDeducciones, 2).ToString("F2", CultureInfo.CurrentCulture);

                    // Recalcular importe final
                    RecalcularImporte();

                    Console.WriteLine($"DEBUG - Deducciones actualizadas: {totalDeducciones:C}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar deducciones en objeto: {ex.Message}");
            }
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
