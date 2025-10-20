using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.Modelos
{
    public class NotaCargo
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string FacturaFolio { get; set; }
        public Cliente _Cliente { get; set; }
        public Cultivo _Cultivo { get; set; }
        public Cosecha _Cosecha { get; set; }
        public double Tons { get; set; }
        public double Precio { get; set; }
        public double Importe { get; set; }
        public string FacturaUUID { get; set; }
        public List<DeduccionNota> Deducciones {  get; set; }
    }
}
