using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.Modelos
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoPersonaFiscal TipoPersona { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string Rfc { get; set; }
        public string Curp { get; set; }
        public int PaisNumero { get; set; }
        public string CodigoPostal { get; set; }
        public string Calle { get; set; }
        public string NoInterior { get; set; }
        public string NoExterior { get; set; }
        public Colonia _Colonia { get; set; }
        public Ciudad _Ciudad { get; set; }
        public Municipio _Municipio { get; set; }
        public Estado _Estado { get; set; }
        public string Referencia { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string CondicionPago { get; set; }
        public string MetodoPago { get; set; }
        public int LimiteCredito { get; set; }
        public int Moneda { get; set; }
        public int CreditoSuspendido { get; set; }
    }
}
