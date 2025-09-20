using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.Modelos
{
    public class Colonia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Ciudad _Ciudad { get; set; }
    }
}
