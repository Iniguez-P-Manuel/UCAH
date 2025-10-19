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
using ReporteadorUCAH;

namespace ReporteadorUCAH
{
    public partial class Main : Form
    {
        //VARIABLES PARA UTILIZAR EL ARRASTRAR VENTANA
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public Main()
        {
            InitializeComponent();
            // Evitar que oculte la barra de tareas
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private List<Form> ventanasAbiertas = new List<Form>();

        //Metodo para abrir el form interno
        public void AgregarForm(Form Nuevoform, String titulo)
        {
            if (!FocusForm(Nuevoform))
            {
                Nuevoform.TopLevel = false;
                Nuevoform.Location = new Point((PanelVentana.Width - Nuevoform.Width) / 2, (PanelVentana.Height - Nuevoform.Height) / 2);
                PanelVentana.Controls.Add(Nuevoform);
                Nuevoform.Show();
                Nuevoform.BringToFront();
            }
        }

        //Metodo para cerrar el form interno
        public bool FocusForm(Form FormoNuevo)
        {
            Form[] forms = Application.OpenForms.Cast<Form>().ToArray();
            foreach (Form thisForm in forms)
            {
                if (thisForm.Name.Equals(FormoNuevo.Name))
                {
                    thisForm.Focus();
                    thisForm.BringToFront();
                    return true;
                }
            }
            return false;
        }


        //Boton cerrar
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //Boton maximizar
        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        //Boton minimizar
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }




        private void btnNotasCargo_Click(object sender, EventArgs e)
        {
            Formas.NotaCargo form = new Formas.NotaCargo();
            AgregarForm(form, "Test");

        }

        private void barraPrincipal_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void barraPrincipal_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            Formas.Clientes form = new Formas.Clientes();
            AgregarForm(form, "Test");
        }

        private void Main_Load(object sender, EventArgs e)
        {
            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                if (!varCon.TestConnection())
                    this.Dispose();
            }
        }

        private void btnCultivos_Click(object sender, EventArgs e)
        {
            Formas.Cultivos form = new Formas.Cultivos();
            AgregarForm(form, "Test");
        }

        private void lblTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void lblTitulo_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
    }
}
