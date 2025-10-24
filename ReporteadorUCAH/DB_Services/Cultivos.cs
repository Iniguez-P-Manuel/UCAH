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

        public List<Cultivo> GetAllCultivos()
        {
            var Cultivos = new List<Cultivo>();

            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Cultivos";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Cultivo = MapClasses.MapToCultivo(reader);
                            Cultivos.Add(Cultivo);
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Cultivos: {ex.Message}");
                throw;
            }

            return Cultivos;
        }

        public int AgregarCultivo(Cultivo cultivo)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"
                INSERT INTO Cultivos (Nombre, Cultivo, CONS) 
                VALUES (@Nombre, @CultivoTipo, @CONS);
                SELECT last_insert_rowid();";

                    command.Parameters.AddWithValue("@Nombre", cultivo.Nombre ?? "");
                    command.Parameters.AddWithValue("@CultivoTipo", cultivo.CultivoTipo ?? "");
                    command.Parameters.AddWithValue("@CONS", cultivo.CONS ?? "");

                    var id = Convert.ToInt32(command.ExecuteScalar());
                    return id;
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al agregar cultivo: {ex.Message}");
                return 0;
            }
        }

        public int ActualizarCultivo(Cultivo cultivo)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "UPDATE Cultivos SET Nombre = @Nombre, Cultivo = @CultivoTipo, CONS = @CONS WHERE Id = @Id;";
                    command.Parameters.AddWithValue("@Nombre", cultivo.Nombre ?? "");
                    command.Parameters.AddWithValue("@CultivoTipo", cultivo.CultivoTipo ?? "");
                    command.Parameters.AddWithValue("@CONS", cultivo.CONS ?? "");
                    command.Parameters.AddWithValue("@Id", cultivo.Id);

                    int filasAfectadas = command.ExecuteNonQuery();
                    return filasAfectadas;
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al actualizar cultivo: {ex.Message}");
                return 0;
            }
        }
        public void EliminarCultivo(int id)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Cultivos WHERE Id = @Id;";
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al eliminar cultivo: {ex.Message}");
                MessageBox.Show($"Error al eliminar cultivo: {ex.Message}", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
