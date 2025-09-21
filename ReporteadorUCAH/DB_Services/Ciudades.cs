using Microsoft.Data.Sqlite;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.DB_Services
{
    internal class Ciudades : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;
        public Ciudades(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Modelos.Ciudad GetCiudadByid(int id)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Ciudades WHERE id = @Id ";
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapClasses.MapToCiudad(reader);
                        }
                    }
                }

                // Si no encuentra el registro, retorna null
                return null;
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener ciudad: {ex.Message}");
                throw;
            }
        }

        public List<Ciudad> GetAllCiudades()
        {
            var Ciudades = new List<Ciudad>();

            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Ciudades";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Ciudad = MapClasses.MapToCiudad(reader);
                            Ciudades.Add(Ciudad);
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Ciudades: {ex.Message}");
                throw;
            }

            return Ciudades;
        }

        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
