using Microsoft.Data.Sqlite;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.DB_Services
{
    internal class GruposFamiliares : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;
        public GruposFamiliares(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public GrupoFamiliar GetGrupoFamiliarById(int id)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM GrupoFamiliar WHERE id = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapClasses.MapToGrupoFamiliar(reader);
                        }
                    }
                }

                // Si no encuentra el registro, retorna null
                return null;
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Grupo Familiar por ID: {ex.Message}");
                throw;
            }
        }

        public List<GrupoFamiliar> GetAllGruposFamiliares()
        {
            var gruposFamiliares = new List<GrupoFamiliar>();

            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM GrupoFamiliar";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var grupoFamiliar = MapClasses.MapToGrupoFamiliar(reader);
                            gruposFamiliares.Add(grupoFamiliar);
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Grupos Familiares: {ex.Message}");
                throw;
            }

            return gruposFamiliares;
        }

        public int AgregarGrupoFamiliar(GrupoFamiliar grupoFamiliar)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"
                INSERT INTO GrupoFamiliar (nombre) 
                VALUES (@nombre);
                SELECT last_insert_rowid();";

                    command.Parameters.AddWithValue("@nombre", grupoFamiliar.Nombre ?? "");

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al agregar grupo familiar: {ex.Message}");
                return 0;
            }
        }

        public int ActualizarGrupoFamiliar(GrupoFamiliar grupoFamiliar)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "UPDATE GrupoFamiliar SET nombre = @nombre WHERE id = @Id;";
                    command.Parameters.AddWithValue("@nombre", grupoFamiliar.Nombre ?? "");
                    command.Parameters.AddWithValue("@Id", grupoFamiliar.Id);

                    int filasAfectadas = command.ExecuteNonQuery();
                    return filasAfectadas;
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al actualizar grupo familiar: {ex.Message}");
                return 0;
            }
        }

        public int EliminarGrupoFamiliar(int id)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "DELETE FROM GrupoFamiliar WHERE id = @Id;";
                    command.Parameters.AddWithValue("@Id", id);

                    int filasAfectadas = command.ExecuteNonQuery();
                    return filasAfectadas;
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al eliminar grupo familiar: {ex.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}