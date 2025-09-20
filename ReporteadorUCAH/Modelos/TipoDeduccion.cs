using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.Modelos
{
    public class TipoDeduccion
    {
        public int Id { get; set; }
        public GrupoDeducciones _Grupo { get; set; }
        public string Nombre { get; set; }
    }
}
