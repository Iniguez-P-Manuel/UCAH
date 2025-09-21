using Microsoft.Data.Sqlite;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.DB_Services
{
    internal class Colonias : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;
        public Colonias(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Modelos.Colonia GetColoniaByid(int id)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Colonia WHERE id = @Id ";
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapClasses.MapToColonia(reader);
                        }
                    }
                }

                // Si no encuentra el registro, retorna null
                return null;
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener colonia: {ex.Message}");
                throw;
            }
        }

        public List<Colonia> GetAllColonias()
        {
            var Colonias = new List<Colonia>();

            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Colonia";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Colonia = MapClasses.MapToColonia(reader);
                            Colonias.Add(Colonia);
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Colonias: {ex.Message}");
                throw;
            }

            return Colonias;
        }

        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
