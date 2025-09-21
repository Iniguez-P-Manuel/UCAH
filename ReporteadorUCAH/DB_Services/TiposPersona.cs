using Microsoft.Data.Sqlite;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.DB_Services
{
    internal class TiposPersona : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;
        public TiposPersona(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Modelos.TipoPersonaFiscal GetTipoPersonaByID(int id)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM TipoPersona WHERE id = @Id ";
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapClasses.MapToTipoPersonaFiscal(reader);
                        }
                    }
                }

                // Si no encuentra el registro, retorna null
                return null;
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Tipo persona: {ex.Message}");
                throw;
            }
        }
        public List<TipoPersonaFiscal> GetAllTiposPersona()
        {
            var TiposPErsona = new List<TipoPersonaFiscal>();

            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM TipoPersona";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tipoPersona = MapClasses.MapToTipoPersonaFiscal(reader);
                            TiposPErsona.Add(tipoPersona);
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Tipos persona fiscal: {ex.Message}");
                throw;
            }

            return TiposPErsona;
        }


        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
