using Microsoft.Data.Sqlite;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.DB_Services
{
    internal class Clientes : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;
        public Clientes(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public Cliente GetClienteById(int id)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Clientes WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapClasses.MapToCliente(reader);
                        }
                    }
                }

                // Si no encuentra el registro, retorna null
                return null;
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Cliente por ID: {ex.Message}");
                throw;
            }
        }





        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
