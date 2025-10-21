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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ReporteadorUCAH.Formas
{
    public partial class frmClientes : FormModel
    {
        public frmClientes()
        {
            InitializeComponent();
        }
        Modelos.Cliente ClienteActual = new Modelos.Cliente();
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BusquedaClientes formBusqueda = new BusquedaClientes();
            formBusqueda.ObjetoSeleccionado += BusquedaSeleccionada;
            formBusqueda.ShowDialog();

        }

        public async void CargarComboGrupoFamiliar()
        {
            List<Modelos.GrupoFamiliar> lstGrupos = new List<Modelos.GrupoFamiliar>();

            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (var DB_service = new GruposFamiliares(varCon))
                {
                    lstGrupos = await EjecutarConLoading(() =>
                    {
                        return DB_service.GetAllGruposFamiliares();
                    });
                }
            }

            // Configurar el ComboBox
            cbxGrupoFamiliar.DataSource = lstGrupos;
            cbxGrupoFamiliar.ValueMember = "Id";
            cbxGrupoFamiliar.DisplayMember = "Nombre";

            // Configurar AutoComplete (DEBE SER EN ESTE ORDEN)
            cbxGrupoFamiliar.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbxGrupoFamiliar.AutoCompleteSource = AutoCompleteSource.ListItems;

            // Establecer selección
            if (ClienteActual._GrupoFamiliar != null)
            {
                // Validar que el valor existe en el DataSource
                var existe = lstGrupos.Any(g => g.Id == ClienteActual._GrupoFamiliar.Id);
                if (existe)
                    cbxGrupoFamiliar.SelectedValue = ClienteActual._GrupoFamiliar.Id;
                else
                    cbxGrupoFamiliar.SelectedIndex = -1;
            }
            else
            {
                cbxGrupoFamiliar.SelectedIndex = -1;
            }
        }

        public async void CargarCombo_old()
        {
            List<Modelos.Colonia> lstColonias = new List<Modelos.Colonia>();
            List<Modelos.Ciudad> lstCiudades = new List<Modelos.Ciudad>();
            List<Modelos.Municipio> lstMunicipios = new List<Modelos.Municipio>();
            List<Modelos.Estado> lstEstados = new List<Modelos.Estado>();

            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (var DB_service = new Colonias(varCon))
                    lstColonias = DB_service.GetAllColonias();
                using (var DB_service = new Ciudades(varCon))
                    lstCiudades = DB_service.GetAllCiudades();
                using (var DB_service = new Municipios(varCon))
                    lstMunicipios = DB_service.GetAllMunicipios();
                using (var DB_service = new Estados(varCon))
                    lstEstados = DB_service.GetAllEstados();
            }

            cbxColonia.DataSource = lstColonias;
            cbxColonia.DisplayMember = "Nombre";
            cbxColonia.ValueMember = "Id";

            cbxCiudad.DataSource = lstCiudades;
            cbxCiudad.DisplayMember = "Nombre";
            cbxCiudad.ValueMember = "Id";

            cbxMunicipio.DataSource = lstMunicipios;
            cbxMunicipio.DisplayMember = "Nombre";
            cbxMunicipio.ValueMember = "Id";

            cbxEstado.DataSource = lstEstados;
            cbxEstado.DisplayMember = "Nombre";
            cbxEstado.ValueMember = "Id";


        }
        private void BusquedaSeleccionada(object sender, BusquedaClientes.ObjetoSeleccionadoEventArgs e)
        {
            ClienteActual = e.ObjetoSeleccionado;

            txtApellidoMat.Text = ClienteActual.ApellidoMaterno;
            txtApellidoPat.Text = ClienteActual.ApellidoPaterno;
            txtCalle.Text = ClienteActual.Calle;
            txtCodigoPostal.Text = ClienteActual.CodigoPostal;
            txtCorreo.Text = ClienteActual.Correo;
            txtCURP.Text = ClienteActual.Curp;
            txtID.Text = ClienteActual.Id.ToString();
            txtNombres.Text = ClienteActual.Nombres;
            txtNumExterior.Text = ClienteActual.NoExterior;
            txtNumInterior.Text = ClienteActual.NoInterior;
            txtRFC.Text = ClienteActual.Rfc;
            txtTelefono.Text = ClienteActual.Telefono;

            if (ClienteActual.TipoPersona.Id == 1)
                radioPF.Checked = true;
            else
                radioPM.Checked = true;

                // Uso:
                SetComboValue(cbxEstado, ClienteActual._Estado?.Id);
            SetComboValue(cbxMunicipio, ClienteActual._Municipio?.Id);
            SetComboValue(cbxCiudad, ClienteActual._Ciudad?.Id);
            SetComboValue(cbxColonia, ClienteActual._Colonia?.Id);
            SetComboValue(cbxGrupoFamiliar, ClienteActual._GrupoFamiliar?.Id);

        }

        private void SetComboValue(System.Windows.Forms.ComboBox combo, int? value)
        {
            if (value.HasValue && combo.DataSource != null)
            {
                var exists = ((System.Collections.IList)combo.DataSource)
                    .Cast<dynamic>()
                    .Any(item => item.Id == value.Value);

                if (exists)
                    combo.SelectedValue = value.Value;
                else
                    combo.SelectedIndex = -1;
            }
            else
            {
                combo.SelectedIndex = -1;
            }
        }

        private bool ValidarGuardado()
        {
            if (cbxGrupoFamiliar.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona grupo familiar valido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtNombres.Text.Trim().Length < 3)
            {
                MessageBox.Show("Campo Nombres debe contener al menos 3 caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtCalle.Text.Trim().Length < 1)
            {
                MessageBox.Show("Campo Calle vacio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtCodigoPostal.Text.Trim().Length < 1)
            {
                MessageBox.Show("Campo Codigo postal vacio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        public override void Guardar()
        {

            if (!ValidarGuardado())
                return;

            if (txtID.Text.Trim().Contains(""))
            {
                LlenarClienteActual();
                using (DatabaseConnection varCon = new DatabaseConnection())
                {
                    using (var DB_service = new Clientes(varCon))
                        ClienteActual.Id = DB_service.Guardar(ClienteActual);

                    if (ClienteActual.Id > 0)
                    {
                        txtID.Text = ClienteActual.Id.ToString();
                        MessageBox.Show("Los datos se guardaron correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error al guardar cliente.", "SQLITE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                LlenarClienteActual();
                using (DatabaseConnection varCon = new DatabaseConnection())
                {
                    int resultado;
                    using (var DB_service = new Clientes(varCon))
                        resultado = DB_service.ActualizarCliente(ClienteActual);

                    if (resultado > 0)
                    {
                        MessageBox.Show("Los datos se actualizaron correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    {
                        MessageBox.Show("Error al actualizar cliente.", "SQLITE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void LlenarClienteActual()
        {

            ClienteActual.ApellidoMaterno = txtApellidoMat.Text;
            ClienteActual.ApellidoPaterno = txtApellidoPat.Text;
            ClienteActual.Calle = txtCalle.Text;
            ClienteActual.CodigoPostal = txtCodigoPostal.Text;
            ClienteActual.Correo = txtCorreo.Text;
            ClienteActual.Curp = txtCURP.Text;

            int idPersona = 0;
            if (radioPM.Checked)
                idPersona = 2; //PERSONA MORAL
            else
                idPersona = 1;//PERSONA FISICA

            ClienteActual.TipoPersona = lstTipoPersona.FirstOrDefault(t => t.Id == idPersona);

            if (!txtID.Text.Trim().Contains(""))
                ClienteActual.Id = Convert.ToInt32(txtID.Text);

            ClienteActual.Nombres = txtNombres.Text;

            ClienteActual.Nombre = txtNombres.Text + " " + txtApellidoPat.Text + " " + txtApellidoMat.Text;
            ClienteActual.NoExterior = txtNumExterior.Text;
            ClienteActual.NoInterior = txtNumInterior.Text;
            ClienteActual.Rfc = txtRFC.Text;
            ClienteActual.Telefono = txtTelefono.Text;

            // Para los combobox, asumiendo que necesitas crear objetos o asignar referencias
            if (cbxCiudad.SelectedValue != null && cbxCiudad.SelectedValue is int ciudadId)
            {
                ClienteActual._Ciudad = new Ciudad { Id = ciudadId };
                // Si necesitas más propiedades de Ciudad, deberías cargar el objeto completo
            }
            else
            {
                ClienteActual._Ciudad = null;
            }

            if (cbxColonia.SelectedValue != null && cbxColonia.SelectedValue is int coloniaId)
            {
                ClienteActual._Colonia = new Colonia { Id = coloniaId };
            }
            else
            {
                ClienteActual._Colonia = null;
            }

            if (cbxEstado.SelectedValue != null && cbxEstado.SelectedValue is int estadoId)
            {
                ClienteActual._Estado = new Estado { Id = estadoId };
            }
            else
            {
                ClienteActual._Estado = null;
            }

            if (cbxMunicipio.SelectedValue != null && cbxMunicipio.SelectedValue is int municipioId)
            {
                ClienteActual._Municipio = new Municipio { Id = municipioId };
            }
            else
            {
                ClienteActual._Municipio = null;
            }

            if (cbxGrupoFamiliar.SelectedValue != null && cbxGrupoFamiliar.SelectedValue is int grupoId)
            {
                ClienteActual._GrupoFamiliar = new GrupoFamiliar { Id = grupoId };
            }
            else
            {
                ClienteActual._GrupoFamiliar = null;
            }
        }
        private void GrupoFamiliarSeleccionado(object sender, AgregarGrupoFamiliar.ObjetoSeleccionadoEventArgs e)
        {
            CargarComboGrupoFamiliar();
            ClienteActual._GrupoFamiliar = e.ObjetoSeleccionado;

            if (ClienteActual._GrupoFamiliar != null)
                cbxGrupoFamiliar.SelectedValue = ClienteActual._GrupoFamiliar.Id;
            else
                cbxMunicipio.SelectedIndex = -1;

        }
        public override void Nuevo()
        {
            //Limpiar objeto cliente
            ClienteActual = new Modelos.Cliente();

            // Limpiar TextBoxes
            txtApellidoMat.Text = string.Empty;
            txtApellidoPat.Text = string.Empty;
            txtCalle.Text = string.Empty;
            txtCodigoPostal.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtCURP.Text = string.Empty;
            txtID.Text = string.Empty;
            txtNombres.Text = string.Empty;
            txtNumExterior.Text = string.Empty;
            txtNumInterior.Text = string.Empty;
            txtRFC.Text = string.Empty;
            txtTelefono.Text = string.Empty;

            // Limpiar ComboBoxes
            cbxCiudad.SelectedIndex = -1;
            cbxColonia.SelectedIndex = -1;
            cbxEstado.SelectedIndex = 1;
            cbxMunicipio.SelectedIndex = -1;

            cbxGrupoFamiliar.SelectedIndex = -1;
        }
        private void Clientes_Load(object sender, EventArgs e)
        {
            CargarCombo();
            CargarComboGrupoFamiliar();
            CargarTiposPersona();

        }

        List<Modelos.TipoPersonaFiscal> lstTipoPersona = new List<TipoPersonaFiscal>();
        public void CargarTiposPersona()
        {
            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (var DB_service = new TiposPersona(varCon))
                    lstTipoPersona = DB_service.GetAllTiposPersona();
            }
        }
        private void btnGrupoFamiliar_Click(object sender, EventArgs e)
        {
            AgregarGrupoFamiliar formGrupoFamiliar = new AgregarGrupoFamiliar();
            formGrupoFamiliar.ObjetoSeleccionado += GrupoFamiliarSeleccionado;
            formGrupoFamiliar.ShowDialog();
        }

        private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtCalle.Focus();
                e.Handled = true; // Marcar como manejado
                e.SuppressKeyPress = true; // Suprimir el procesamiento normal
            }
        }
        private void txtTelefono_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtCalle.Focus();
                e.IsInputKey = true; // Esto marca la tecla como manejada
            }
        }

        public async void CargarCombo()
        {
            List<Modelos.Colonia> lstColonias = new List<Modelos.Colonia>();
            List<Modelos.Ciudad> lstCiudades = new List<Modelos.Ciudad>();
            List<Modelos.Municipio> lstMunicipios = new List<Modelos.Municipio>();
            List<Modelos.Estado> lstEstados = new List<Modelos.Estado>();

            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (var DB_service = new Colonias(varCon))
                    lstColonias = DB_service.GetAllColonias();
                using (var DB_service = new Ciudades(varCon))
                    lstCiudades = DB_service.GetAllCiudades();
                using (var DB_service = new Municipios(varCon))
                    lstMunicipios = DB_service.GetAllMunicipios();
                using (var DB_service = new Estados(varCon))
                    lstEstados = DB_service.GetAllEstados();
            }

            // Guardar las listas completas para filtrar después
            _todasColonias = lstColonias;
            _todasCiudades = lstCiudades;
            _todasMunicipios = lstMunicipios;

            // Ordenar Estados - palabras con S al principio Buscando sonora y sinaloa
            lstEstados = lstEstados
                .OrderByDescending(e => e.Nombre?.StartsWith("S") == true)
                .ThenBy(e => e.Nombre)
                .ToList();

            // Cargar Estados (nivel más alto)
            cbxEstado.DataSource = lstEstados;
            cbxEstado.DisplayMember = "Nombre";
            cbxEstado.ValueMember = "Id";

            // Suscribir eventos
            cbxEstado.SelectedIndexChanged += CbxEstado_SelectedIndexChanged;
            cbxMunicipio.SelectedIndexChanged += CbxMunicipio_SelectedIndexChanged;
            cbxCiudad.SelectedIndexChanged += CbxCiudad_SelectedIndexChanged;

            cbxEstado.SelectedIndex = 1;
        }

        // Variables para guardar las listas completas
        private List<Colonia> _todasColonias;
        private List<Ciudad> _todasCiudades;
        private List<Municipio> _todasMunicipios;

        private void CbxEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxEstado.SelectedItem is Estado estadoSeleccionado)
            {
                // Filtrar municipios por el estado seleccionado
                var municipiosFiltrados = _todasMunicipios
                    .Where(m => m._Estado?.Id == estadoSeleccionado.Id)
                    .ToList();

                cbxMunicipio.DataSource = municipiosFiltrados;
                cbxMunicipio.DisplayMember = "Nombre";
                cbxMunicipio.ValueMember = "Id";

                // Limpiar los combobox dependientes
                cbxCiudad.DataSource = null;
                cbxColonia.DataSource = null;
            }
        }

        private void CbxMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxMunicipio.SelectedItem is Municipio municipioSeleccionado)
            {
                // Filtrar ciudades por el municipio seleccionado
                var ciudadesFiltradas = _todasCiudades
                    .Where(c => c._Municipio?.Id == municipioSeleccionado.Id)
                    .ToList();

                cbxCiudad.DataSource = ciudadesFiltradas;
                cbxCiudad.DisplayMember = "Nombre";
                cbxCiudad.ValueMember = "Id";

                // Limpiar el combobox dependiente
                cbxColonia.DataSource = null;
            }
        }

        private void CbxCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCiudad.SelectedItem is Ciudad ciudadSeleccionada)
            {
                // Filtrar colonias por la ciudad seleccionada
                var coloniasFiltradas = _todasColonias
                    .Where(col => col._Ciudad?.Id == ciudadSeleccionada.Id)
                    .ToList();

                cbxColonia.DataSource = coloniasFiltradas;
                cbxColonia.DisplayMember = "Nombre";
                cbxColonia.ValueMember = "Id";
            }
        }

        private void cbxGrupoFamiliar_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtApellidoPat.Focus();
                e.IsInputKey = true; // Esto marca la tecla como manejada
            }
        }
    }
}
