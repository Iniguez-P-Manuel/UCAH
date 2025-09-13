using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using GestionPracticas.Capa_de_negocio.Clases;

namespace GestionPracticas
{
    public partial class Main : Form
    {


        public Main()
        {
            InitializeComponent();
            //Al iniciar el programa se crea la variable de la basede datos
            SqliteDB db = new SqliteDB();
            Capa_de_negocio.Solicitud SolNEgocios = new Capa_de_negocio.Solicitud();
            //Se usa la variable para iniciar las tablas en caso de que no lo esten
            db.SqliteIniciarTablas(db.ConexionDB());
            SolNEgocios.CrearDirectorio();
        }

 
        //VARIABLES PARA UTILIZAR EL ARRASTRAR VENTANA
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        //Metodo para cerrar el form interno
        public void CerrarForms()
        {
            Form[] forms = Application.OpenForms.Cast<Form>().ToArray();
            foreach (Form thisForm in forms)
            {
                if (thisForm.Name != "Main") thisForm.Close();
            }
        }
        //Metodo para abrir el form interno
        public void AgregarForm(Form Nuevoform, String titulo)
        {
            Nuevoform.TopLevel = false;
            Nuevoform.Location = new Point((PanelVentana.Width - Nuevoform.Width) / 2, (PanelVentana.Height - Nuevoform.Height) / 2);
            PanelVentana.Controls.Add(Nuevoform);
            CerrarForms();
            Titulo.Text = titulo;
            Nuevoform.Show();
            PanelVentana.BackgroundImage = null;
        }

        //Boton Registro solicitud
        private void BtnMenuSolicitud_Click(object sender, EventArgs e)
        {
            Forms.Solicitud Frm = new Forms.Solicitud();
            AgregarForm(Frm, "Solicitud");
        }

        //Boton Registro Asistencia
        private void BtnMenuAsistencia_Click(object sender, EventArgs e)
        {
            Forms.ListaPracticas Frm = new Forms.ListaPracticas();
            Frm.FormPadre = this;
            Frm.SetBtnText("Asistencias >>>");
            Frm.BtnTxt = "Asistencias >>>";
            Frm.destino = 1;
            AgregarForm(Frm, "Lista Practicas (Asistencias)");
        }

        //Boton Registro Guia
        private void BtnMenuGuia_Click(object sender, EventArgs e)
        {

            Forms.ListaPracticas Frm = new Forms.ListaPracticas();
            Frm.FormPadre = this;
            Frm.SetBtnText("Guia >>>");
            Frm.BtnTxt = "Guia >>>";
            Frm.destino = 2;
            AgregarForm(Frm, "Lista Practicas (Guia)");
        }

        //Metodo para arrastrar desde panel3
        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        //Metodo para arrastrar desde label titulo
        private void Titulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        //Boton cerrar
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //Boton minimizar
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Forms_1_.GenerarReportes Frm = new Forms_1_.GenerarReportes();
            AgregarForm(Frm, "Generador PDF");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Capa_de_presentacion.Forms.Alumnos Frm = new Capa_de_presentacion.Forms.Alumnos();
            AgregarForm(Frm, "Registro alumnos");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Capa_de_presentacion.Forms.Profesores Frm = new Capa_de_presentacion.Forms.Profesores();
            AgregarForm(Frm, "Registro profesores");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Capa_de_presentacion.Forms.Extras Frm = new Capa_de_presentacion.Forms.Extras();
            Frm.MostrarArea();
            AgregarForm(Frm, "Registro Area");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Forms_1_.GenerarReportes Frm = new Forms_1_.GenerarReportes();
            AgregarForm(Frm, "Generar PDF");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Capa_de_presentacion.Forms.Admin Frm = new Capa_de_presentacion.Forms.Admin();
            Frm.FormPadre = this;
            AgregarForm(Frm, "Activar opciones avanzadas");
        }
        public int admin = 0;
        public void MostrarOpciones()
        {
            admin = 1;
            groupBox3.Visible = true;
        }
        public void OcultarOpciones()
        {
            admin = 0;
            groupBox3.Visible = false;
        }

        private void btnDelSol_Click(object sender, EventArgs e)
        {
            Forms.ListaPracticas Frm = new Forms.ListaPracticas();
            Frm.FormPadre = this;
            Frm.SetBtnText("Borrar");
            Frm.BtnTxt = "Borrar";
            Frm.destino = 3;
            AgregarForm(Frm, "Lista Practicas (Borrar)");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Capa_de_presentacion.Forms.Extras Frm = new Capa_de_presentacion.Forms.Extras();
            Frm.MostrarLaboratorio();
            AgregarForm(Frm, "Registro Laboratorio");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Capa_de_presentacion.Forms.Extras Frm = new Capa_de_presentacion.Forms.Extras();
            Frm.MostrarDepartamento();
            AgregarForm(Frm, "Registro Departamento");
        }
    }
}
