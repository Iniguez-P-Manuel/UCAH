using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.Modelos
{
    public class DeduccionNota
    {
        public int Id { get; set; }
        public TipoDeduccion _Deduccion { get; set; }
        public double Importe { get; set; }
    }
}
