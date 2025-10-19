using Microsoft.Data.Sqlite;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.DB_Services
{
    internal class Cosechas : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;
        public Cosechas(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        
        public Cosecha GetCosechaById(int id)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Cosecha WHERE id = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapClasses.MapToCosecha(reader);
                        }
                    }
                }

                // Si no encuentra el registro, retorna null
                return null;
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Cosecha por ID: {ex.Message}");
                throw;
            }
        }

        public List<Cosecha> GetAllCosechas()
        {
            var Cosechas = new List<Cosecha>();

            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Cosecha";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cosecha = MapClasses.MapToCosecha(reader);
                            Cosechas.Add(cosecha);
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Cosechas: {ex.Message}");
                throw;
            }

            return Cosechas;
        }

        public int AgregarCosecha(Cosecha cosecha)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"
                INSERT INTO Cosecha (fechaInicial, fechaFinal) 
                VALUES (@fechaInicial, @fechaFinal);
                SELECT last_insert_rowid();";

                    command.Parameters.AddWithValue("@fechaInicial", cosecha.FechaInicial);
                    command.Parameters.AddWithValue("@fechaFinal", cosecha.FechaFinal);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al agregar cosecha: {ex.Message}");
                return 0;
            }
        }
        public int ActualizarCosecha(Cosecha cosecha)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "UPDATE Cosecha SET fechaInicial = @fechaInicial, fechaFinal = @fechaFinal WHERE id = @Id;";
                    command.Parameters.AddWithValue("@fechaInicial", cosecha.FechaInicial);
                    command.Parameters.AddWithValue("@fechaFinal", cosecha.FechaFinal);
                    command.Parameters.AddWithValue("@Id", cosecha.Id);

                    int filasAfectadas = command.ExecuteNonQuery();
                    return filasAfectadas;
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al actualizar cosecha: {ex.Message}");
                return 0;
            }
        }
        public int EliminarCosecha(int id)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Cosecha WHERE id = @Id;";
                    command.Parameters.AddWithValue("@Id", id);

                    int filasAfectadas = command.ExecuteNonQuery();
                    return filasAfectadas;
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al eliminar cosecha: {ex.Message}");
                throw;
            }
        }



        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
