using Microsoft.Data.Sqlite;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.DB_Services
{
    internal class TiposDeduccion : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;
        public TiposDeduccion(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Modelos.TipoDeduccion GetTipoDeduccionByID(int id)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM TipoDeducciones WHERE id = @Id ";
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            return MapClasses.MapToTipoDeduccion(reader);
                        }
                    }
                }

                // Si no encuentra el registro, retorna null
                return null;
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Tipo deduccion: {ex.Message}");
                throw;
            }
        }

        public List<TipoDeduccion> GetAllTiposDeduccion()
        {
            var TiposDeduccion = new List<TipoDeduccion>();

            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM TipoDeducciones";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tipoDedu = MapClasses.MapToTipoDeduccion(reader);
                            TiposDeduccion.Add(tipoDedu);
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Tipos Deducciones: {ex.Message}");
                throw;
            }

            return TiposDeduccion;
        }



        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
