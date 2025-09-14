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
        


        //Metodo para cerrar el form interno
        public void CerrarForms()
        {
            Form[] forms = Application.OpenForms.Cast<Form>().ToArray();
            foreach (Form thisForm in forms)
            {
                if (thisForm.Name != "Main") thisForm.Close();
            }
        }


        //Boton cerrar
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //Boton maximizar
        private void button2_Click_1(object sender, EventArgs e)
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
        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Nuevoform"></param>
        /// <param name="titulo"></param>


        //Metodo para abrir el form interno
        public void AgregarForm(Form Nuevoform, String titulo)
        {
            Nuevoform.TopLevel = false;
            Nuevoform.Location = new Point((PanelVentana.Width - Nuevoform.Width) / 2, (PanelVentana.Height - Nuevoform.Height) / 2);
            PanelVentana.Controls.Add(Nuevoform);
            CerrarForms();
            //Titulo.Text = titulo;
            Nuevoform.Show();
            PanelVentana.BackgroundImage = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Formas.NotaCargo form = new Formas.NotaCargo();
            AgregarForm(form, "Test");

        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
