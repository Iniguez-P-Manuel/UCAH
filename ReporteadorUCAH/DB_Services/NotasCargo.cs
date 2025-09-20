using Microsoft.Data.Sqlite;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.DB_Services
{
    internal class NotasCargo : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;
        public NotasCargo(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<NotaCargo> GetAllNotas()
        {
            var Notas = new List<NotaCargo>();

            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM LiquidacionNotasCargo";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Nota = MapClasses.MapToNotaCargo(reader);
                            Notas.Add(Nota);
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener notas: {ex.Message}");
                throw;
            }

            return Notas;
        }
        public NotaCargo GetNotaCargoById(int id)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM LiquidacionNotasCargo WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapClasses.MapToNotaCargo(reader);
                        }
                    }
                }

                // Si no encuentra el registro, retorna null
                return null;
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener nota de cargo por ID: {ex.Message}");
                throw;
            }
        }





        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
