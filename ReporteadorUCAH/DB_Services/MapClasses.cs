using Microsoft.Data.Sqlite;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.DB_Services
{
    public class MapClasses
    {
        public static NotaCargo MapToNotaCargo(SqliteDataReader reader)
        {
            Cliente _cliente = new Cliente();
            Cultivo _cultivo = new Cultivo();
            List<DeduccionNota> _deducciones = new List<DeduccionNota>();

            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (Cultivos DB_Cultivo = new Cultivos(varCon)) 
                {
                    _cultivo = DB_Cultivo.GetCultivoById(reader.GetInt32(reader.GetOrdinal("idCultivo")));
                }
                using (Clientes DB_Clientes = new Clientes(varCon))
                {
                    _cliente = DB_Clientes.GetClienteById(reader.GetInt32(reader.GetOrdinal("idCliente")));
                }
                using (DeduccionesNota DB_Deducciones = new DeduccionesNota(varCon))
                {
                    _deducciones = DB_Deducciones.GetDeduccionesByNota(reader.GetInt32(reader.GetOrdinal("id")));
                }
            }

            return new NotaCargo
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Fecha = GetDateTimeOrNull(reader, "FECHA"),
                FacturaFolio = GetStringOrNull(reader, "FacturaFolio"),
                _Cliente = _cliente,
                _Cultivo = _cultivo,
                Tons = reader.GetDouble(reader.GetOrdinal("TONS")),
                Precio = reader.GetDouble(reader.GetOrdinal("PRECIO")),
                Importe = reader.GetDouble(reader.GetOrdinal("IMPORTE")),
                FacturaUUID = GetStringOrNull(reader, "CFDI"),
                Deducciones = _deducciones
            };
        }
        public static Cliente MapToCliente(SqliteDataReader reader)
        {
            return new Cliente
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nombre = GetStringOrNull(reader, "Nombre"),
                TipoPersona = new TipoPersonaFiscal(), // Se resuelve después
                ApellidoPaterno = GetStringOrNull(reader, "apellidoPaterno"),
                ApellidoMaterno = GetStringOrNull(reader, "apellidoMaterno"),
                Nombres = GetStringOrNull(reader, "nombres"),
                Rfc = GetStringOrNull(reader, "rfc"),
                Curp = GetStringOrNull(reader, "curp"),
                PaisNumero = GetInt32OrNull(reader, "paisNumero"),
                CodigoPostal = GetStringOrNull(reader, "codigoPostal"),
                Calle = GetStringOrNull(reader, "calle"),
                NoInterior = GetStringOrNull(reader, "noInterior"),
                NoExterior = GetStringOrNull(reader, "noExterior"),
                _Colonia = new Colonia(), // Se resuelve después
                _Ciudad = new Ciudad(), // Se resuelve después
                _Municipio = new Municipio(), // Se resuelve después
                _Estado = new Estado(), // Se resuelve después
                Referencia = GetStringOrNull(reader, "referencia"),
                Telefonos = GetStringOrNull(reader, "telefonos"),
                CondicionPago = GetStringOrNull(reader, "condicionPago"), 
                MetodoPago = GetStringOrNull(reader, "metodoPago"), 
                LimiteCredito = GetInt32OrNull(reader, "limiteCredito"),
                Moneda = GetInt32OrNull(reader, "moneda"),
                CreditoSuspendido = GetInt32OrNull(reader, "creditoSuspendido")
            };
        }
        public static Cultivo MapToCultivo(SqliteDataReader reader)
        {
            return new Cultivo
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nombre = GetStringOrNull(reader, "Nombre"),
                CultivoTipo = GetStringOrNull(reader, "Cultivo"),
                CONS = GetStringOrNull(reader, "CONS")
            };
        }

        public static Modelos.DeduccionNota MapToDeduccionNota(SqliteDataReader reader)
        {
            return new Modelos.DeduccionNota
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                _Deduccion = new TipoDeduccion(),
                Importe = reader.GetDouble(reader.GetOrdinal("Importe"))
            };
        }

        private static int GetInt32OrNull(SqliteDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? 0 : reader.GetInt32(ordinal);
        }

        private static string GetStringOrNull(SqliteDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        private static DateTime GetDateTimeOrNull(SqliteDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? (DateTime.MinValue) : reader.GetDateTime(ordinal);
        }

        private static bool GetBoolean(SqliteDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);

            if (reader.IsDBNull(ordinal))
                return false;

            // SQLite puede almacenar booleanos como 0/1 o true/false
            var value = reader.GetValue(ordinal);

            if (value is bool boolValue)
                return boolValue;

            if (value is long longValue)
                return longValue != 0;

            if (value is int intValue)
                return intValue != 0;

            if (value is string stringValue)
                return stringValue.ToLower() == "true" || stringValue == "1";

            return false;
        }
    }
}
