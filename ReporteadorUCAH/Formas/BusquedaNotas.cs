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

namespace ReporteadorUCAH.Formas
{
    public partial class BusquedaNotas : FormModel
    {
        public BusquedaNotas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgvNotas.Rows.Clear();
            List<Modelos.NotaCargo> Notas = new List<Modelos.NotaCargo>();

            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (NotasCargo DB_Notas = new NotasCargo(varCon))
                {
                    Notas = DB_Notas.GetAllNotas();
                }
            }

            foreach (Modelos.NotaCargo nota in Notas)
            {
                dgvNotas.Rows.Add(nota.Id, nota.Fecha, nota._Cliente.Nombre, nota._Cultivo.Nombre, nota.Tons);
            }


        }
    }
}
