using Microsoft.Data.Sqlite;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.DB_Services
{
    internal class Cultivos : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;
        public Cultivos(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        
        public Cultivo GetCultivoById(int id)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Cultivos WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapClasses.MapToCultivo(reader);
                        }
                    }
                }

                // Si no encuentra el registro, retorna null
                return null;
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Cultivo por ID: {ex.Message}");
                throw;
            }
        }





        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
