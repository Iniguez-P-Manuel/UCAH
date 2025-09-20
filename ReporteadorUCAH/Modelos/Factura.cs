using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.Modelos
{
    public class Facturas
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public DateTime Fecha { get; set; }
        public string UUID { get; set; }
        public double Importe { get; set; }
        public string EmisorRfc { get; set; }
        public string ReceptorRfc { get; set; }
    }
}
