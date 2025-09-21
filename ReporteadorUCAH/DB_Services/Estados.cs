using Microsoft.Data.Sqlite;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.DB_Services
{
    internal class Estados : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;
        public Estados(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Modelos.Estado GetEstadoByid(int id)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Estados WHERE id = @Id ";
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapClasses.MapToEstado(reader);
                        }
                    }
                }

                // Si no encuentra el registro, retorna null
                return null;
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener estado: {ex.Message}");
                throw;
            }
        }

        public List<Estado> GetAllEstados()
        {
            var Estados = new List<Estado>();

            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Estados";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Estado = MapClasses.MapToEstado(reader);
                            Estados.Add(Estado);
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Estados: {ex.Message}");
                throw;
            }

            return Estados;
        }

        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
