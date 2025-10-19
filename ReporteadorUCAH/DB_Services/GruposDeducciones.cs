using Microsoft.Data.Sqlite;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.DB_Services
{
    internal class GruposDeducciones : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;
        public GruposDeducciones(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public GrupoDeducciones GetGrupoDeduccionesById(int id)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM GrupoDeducciones WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapClasses.MapToGrupoDeducciones(reader);
                        }
                    }
                }

                // Si no encuentra el registro, retorna null
                return null;
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener gropo deducciones por ID: {ex.Message}");
                throw;
            }
        }

        public List<Modelos.GrupoDeducciones> GetAllGruposDeducciones()
        {
            var Grupos = new List<Modelos.GrupoDeducciones>();
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM GrupoDeducciones";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var grupoDeduccion = MapClasses.MapToGrupoDeducciones(reader);
                            Grupos.Add(grupoDeduccion);
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Grupos deducciones: {ex.Message}");
                throw;
            }

            return Grupos;
        }

       

        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
