using ReporteadorUCAH.DB_Services;
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
    public partial class Clientes : FormModel
    {
        public Clientes()
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
                    lstCiudades =  DB_service.GetAllCiudades();
                using (var DB_service = new Municipios(varCon))
                    lstMunicipios =  DB_service.GetAllMunicipios();
                using (var DB_service = new Estados(varCon))
                    lstEstados =  DB_service.GetAllEstados();
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

            if(ClienteActual._Ciudad != null)
                cbxCiudad.SelectedValue = ClienteActual._Ciudad.Id;
            else
                cbxCiudad.SelectedIndex = -1;

            if (ClienteActual._Colonia != null)
                cbxColonia.SelectedValue = ClienteActual._Colonia.Id;
            else
                cbxColonia.SelectedIndex = -1;

            if (ClienteActual._Estado != null)
                cbxEstado.SelectedValue = ClienteActual._Estado.Id;
            else
                cbxEstado.SelectedIndex = -1;

            if (ClienteActual._Municipio != null)
                cbxMunicipio.SelectedValue = ClienteActual._Municipio.Id;
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
            cbxCiudad.SelectedIndex = 1;
            cbxColonia.SelectedIndex = 1;
            cbxEstado.SelectedIndex = 1;
            cbxMunicipio.SelectedIndex = 1;
        }
        private void Clientes_Load(object sender, EventArgs e)
        {
            CargarCombo();
        }
    }
}
