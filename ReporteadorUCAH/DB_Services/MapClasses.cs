using Microsoft.Data.Sqlite;
using ReporteadorUCAH.Formas;
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
        public static Modelos.NotaCargo MapToNotaCargoConJoins(SqliteDataReader reader)
        {
            return new Modelos.NotaCargo
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Fecha = GetDateTimeOrNull(reader, "FECHA"),
                FacturaFolio = GetStringOrNull(reader, "FacturaFolio"),
                Tons = reader.GetDouble(reader.GetOrdinal("TONS")),
                Precio = reader.GetDouble(reader.GetOrdinal("PRECIO")),
                Importe = reader.GetDouble(reader.GetOrdinal("IMPORTE")),
                FacturaUUID = GetStringOrNull(reader, "FacturaUUID"),

                _Cliente = new Cliente
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                    Nombre = GetStringOrNull(reader, "ClienteNombre"),
                    TipoPersona = new TipoPersonaFiscal
                    {
                        Id = GetInt32OrNull(reader, "idTipoPersona"),
                        Nombre = GetStringOrNull(reader, "TipoPersonaNombre"),
                        NombreCorto = GetStringOrNull(reader, "TipoPersonaNombreCorto")
                    },
                    ApellidoPaterno = GetStringOrNull(reader, "apellidoPaterno"),
                    ApellidoMaterno = GetStringOrNull(reader, "apellidoMaterno"),
                    Nombres = GetStringOrNull(reader, "nombres"),
                    Rfc = GetStringOrNull(reader, "ClienteRfc"),
                    Curp = GetStringOrNull(reader, "curp"),
                    PaisNumero = reader.GetInt32(reader.GetOrdinal("paisNumero")),
                    CodigoPostal = GetStringOrNull(reader, "codigoPostal"),
                    Calle = GetStringOrNull(reader, "calle"),
                    NoInterior = GetStringOrNull(reader, "noInterior"),
                    NoExterior = GetStringOrNull(reader, "noExterior"),
                    Correo = GetStringOrNull(reader, "correo"),
                    Telefono = GetStringOrNull(reader, "Telefono"),
                    CondicionPago = GetStringOrNull(reader, "condicionPago"),
                    MetodoPago = GetStringOrNull(reader, "metodoPago"),
                    LimiteCredito = GetInt32OrNull(reader, "limiteCredito"),
                    Moneda = GetInt32OrNull(reader, "moneda"),
                    CreditoSuspendido = GetInt32OrNull(reader, "creditoSuspendido"),
                    _Colonia = new Colonia
                    {
                        Id = GetInt32OrNull(reader, "idColonia"),
                        Nombre = GetStringOrNull(reader, "ColoniaNombre"),
                        _Ciudad = new Ciudad
                        {
                            Id = GetInt32OrNull(reader, "idCiudad"),
                            Nombre = GetStringOrNull(reader, "CiudadNombre"),
                            _Municipio = new Municipio
                            {
                                Id = GetInt32OrNull(reader, "idMunicipio"),
                                Nombre = GetStringOrNull(reader, "MunicipioNombre"),
                                _Estado = new Estado
                                {
                                    Id = GetInt32OrNull(reader, "idEstado"),
                                    EstadoNumero = GetStringOrNull(reader, "estadoNumero"),
                                    Nombre = GetStringOrNull(reader, "EstadoNombre")
                                }
                            }
                        }
                    }
                },

                _Cultivo = new Cultivo
                {
                    Id = GetInt32OrNull(reader, "CultivoId"),
                    Nombre = GetStringOrNull(reader, "CultivoNombre"),
                    CultivoTipo = GetStringOrNull(reader, "CultivoTipo"),
                    CONS = GetStringOrNull(reader, "CONS")
                },

                _Cosecha = new Cosecha
                {
                    Id = GetInt32OrNull(reader, "CosechaId"),
                    FechaInicial = GetDateTimeOrNull(reader, "fechaInicial"),
                    FechaFinal = GetDateTimeOrNull(reader, "fechaFinal")
                },

                _GrupoFamiliar = new GrupoFamiliar
                {
                    Id = GetInt32OrNull(reader, "GrupoFamiliarId"),
                    Nombre = GetStringOrNull(reader, "GrupoFamiliarNombre")
                },

                Deducciones = new List<DeduccionNota>() // Se llena después
            };
        }
        public static Modelos.NotaCargo MapToNotaCargo(SqliteDataReader reader)
        {
            Cliente _cliente = new Cliente();
            Cultivo _cultivo = new Cultivo();
            List<DeduccionNota> _deducciones = new List<DeduccionNota>();
            Cosecha _cosecha = new Cosecha();
            GrupoFamiliar _grupoFamiliar = new GrupoFamiliar();

            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (Cultivos DB_Cultivo = new Cultivos(varCon)) 
                    _cultivo = DB_Cultivo.GetCultivoById(reader.GetInt32(reader.GetOrdinal("idCultivo")));

                using (Clientes DB_Clientes = new Clientes(varCon))
                    _cliente = DB_Clientes.GetClienteById(reader.GetInt32(reader.GetOrdinal("idCliente")));

                using (Cosechas DB_Cosechas = new Cosechas(varCon))
                    _cosecha = DB_Cosechas.GetCosechaById(reader.GetInt32(reader.GetOrdinal("idCosecha")));

                using (GruposFamiliares DB_GrupoFamiliar = new GruposFamiliares(varCon))
                    _grupoFamiliar = DB_GrupoFamiliar.GetGrupoFamiliarById(reader.GetInt32(reader.GetOrdinal("idGrupoFamiliar")));

                using (DeduccionesNota DB_Deducciones = new DeduccionesNota(varCon))
                    _deducciones = DB_Deducciones.GetDeduccionesByNota(reader.GetInt32(reader.GetOrdinal("id")));
            }

            return new Modelos.NotaCargo
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Fecha = GetDateTimeOrNull(reader, "FECHA"),
                FacturaFolio = GetStringOrNull(reader, "FacturaFolio"),
                _Cliente = _cliente,
                _Cultivo = _cultivo,
                _Cosecha = _cosecha,
                Tons = reader.GetDouble(reader.GetOrdinal("TONS")),
                Precio = reader.GetDouble(reader.GetOrdinal("PRECIO")),
                Importe = reader.GetDouble(reader.GetOrdinal("IMPORTE")),
                FacturaUUID = GetStringOrNull(reader, "FacturaUUID"),
                Deducciones = _deducciones,
                _GrupoFamiliar = _grupoFamiliar,
            };
        }
        public static Cliente MapToCliente(SqliteDataReader reader)
        {

            TipoPersonaFiscal _tipoPersona = new TipoPersonaFiscal();
            Colonia _colonia = new Colonia();
            Ciudad _ciudad = new Ciudad();
            Municipio _municipio = new Municipio();
            Estado _estado = new Estado();
            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (TiposPersona DB_TiposPersona = new TiposPersona(varCon))
                    _tipoPersona = DB_TiposPersona.GetTipoPersonaByID(reader.GetInt32(reader.GetOrdinal("idTipoPersona")));
                using (Colonias DB_Colonia = new Colonias(varCon))
                    _colonia = DB_Colonia.GetColoniaByid(reader.GetInt32(reader.GetOrdinal("idColonia")));
                using (Ciudades DB_Ciudad = new Ciudades(varCon))
                    _ciudad = DB_Ciudad.GetCiudadByid(reader.GetInt32(reader.GetOrdinal("idCiudad")));
                using (Municipios DB_Municipio = new Municipios(varCon))
                    _municipio = DB_Municipio.GetMunicipioByid(reader.GetInt32(reader.GetOrdinal("idMunicipio")));
                using (Estados DB_Estado = new Estados(varCon))
                    _estado = DB_Estado.GetEstadoByid(reader.GetInt32(reader.GetOrdinal("idEstado")));
                }
            return new Cliente
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nombre = GetStringOrNull(reader, "Nombre"),
                TipoPersona = _tipoPersona,
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
                _Colonia = _colonia, 
                _Ciudad = _ciudad, 
                _Municipio = _municipio, 
                _Estado = _estado, 
                Correo = GetStringOrNull(reader, "correo"),
                Telefono = GetStringOrNull(reader, "telefonos"),
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
        public static GrupoFamiliar MapToGrupoFamiliar(SqliteDataReader reader)
        {
            return new GrupoFamiliar
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nombre = GetStringOrNull(reader, "nombre")
            };
        }
        public static Cosecha MapToCosecha(SqliteDataReader reader)
        {
            return new Cosecha
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                FechaInicial = GetDateTimeOrNull(reader, "fechaInicial"),
                FechaFinal = GetDateTimeOrNull(reader, "fechaFinal"),
            };
        }
        public static Modelos.DeduccionNota MapToDeduccionNota(SqliteDataReader reader)
        {
            Modelos.TipoDeduccion _tipoDeduccion = new Modelos.TipoDeduccion();

            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (TiposDeduccion DB_TipoDeduccion = new TiposDeduccion(varCon))
                    _tipoDeduccion = DB_TipoDeduccion.GetTipoDeduccionByID(reader.GetInt32(reader.GetOrdinal("idDeduccion")));
            }
            return new Modelos.DeduccionNota
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                _Deduccion = _tipoDeduccion,
                Importe = reader.GetDouble(reader.GetOrdinal("Importe"))
            };
        }
        public static Modelos.TipoDeduccion MapToTipoDeduccion(SqliteDataReader reader)
        {
            Modelos.GrupoDeducciones _grupoDedu = new Modelos.GrupoDeducciones();

            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (GruposDeducciones DB_GruposDedu = new GruposDeducciones(varCon))
                    _grupoDedu = DB_GruposDedu.GetGrupoDeduccionesById(reader.GetInt32(reader.GetOrdinal("idGrupo")));
            }
            return new Modelos.TipoDeduccion
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                _Grupo = _grupoDedu,
                Nombre = GetStringOrNull(reader, "Nombre")
            };
        }

        public static Modelos.TipoPersonaFiscal MapToTipoPersonaFiscal(SqliteDataReader reader)
        {
            
            return new Modelos.TipoPersonaFiscal
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                NombreCorto = GetStringOrNull(reader, "NombreCorto"),
                Nombre = GetStringOrNull(reader, "Nombre")
            };
        }
        public static Colonia MapToColonia(SqliteDataReader reader)
        {
            Ciudad _ciudad = new Ciudad();
            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (Ciudades DB_Ciudades = new Ciudades(varCon))
                    _ciudad = DB_Ciudades.GetCiudadByid(reader.GetInt32(reader.GetOrdinal("idCiudad")));
            }
            return new Colonia
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nombre = GetStringOrNull(reader, "Nombre"),
                _Ciudad = _ciudad,
            };
        }

        public static Ciudad MapToCiudad(SqliteDataReader reader)
        {
            Municipio _municipio = new Municipio();
            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (Municipios DB_Municipios = new Municipios(varCon))
                    _municipio = DB_Municipios.GetMunicipioByid(reader.GetInt32(reader.GetOrdinal("idMunicipio")));
            }
            return new Ciudad
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nombre = GetStringOrNull(reader, "Nombre"),
                _Municipio = _municipio,
            };
        }

        public static Municipio MapToMunicipio(SqliteDataReader reader)
        {
            Estado _estado = new Estado();
            using (DatabaseConnection varCon = new DatabaseConnection())
            {
                using (Estados DB_Estados = new Estados(varCon))
                    _estado = DB_Estados.GetEstadoByid(reader.GetInt32(reader.GetOrdinal("idEstado")));
            }
            return new Municipio
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nombre = GetStringOrNull(reader, "Nombre"),
                _Estado = _estado,
            };
        }
        public static Estado MapToEstado(SqliteDataReader reader)
        {
            return new Estado
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nombre = GetStringOrNull(reader, "Nombre"),
                EstadoNumero = GetStringOrNull(reader, "estadoNumero"),
            };
        }
        public static GrupoDeducciones MapToGrupoDeducciones(SqliteDataReader reader)
        {
            return new GrupoDeducciones
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Nombre = GetStringOrNull(reader, "Nombre")
            };
        }
        public static int GetInt32OrNull(SqliteDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? 0 : reader.GetInt32(ordinal);
        }
        private static double getDoubleOrNull(SqliteDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? 0 : reader.GetDouble(ordinal);
        }
        public static string GetStringOrNull(SqliteDataReader reader, string columnName)
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
