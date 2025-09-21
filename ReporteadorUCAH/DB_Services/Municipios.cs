using Microsoft.Data.Sqlite;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.DB_Services
{
    internal class Municipios : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;
        public Municipios(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Modelos.Municipio GetMunicipioByid(int id)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Municipios WHERE id = @Id ";
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapClasses.MapToMunicipio(reader);
                        }
                    }
                }

                // Si no encuentra el registro, retorna null
                return null;
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener municipio: {ex.Message}");
                throw;
            }
        }

        public List<Municipio> GetAllMunicipios()
        {
            var Municipios = new List<Municipio>();

            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Municipios";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Municipio = MapClasses.MapToMunicipio(reader);
                            Municipios.Add(Municipio);
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener Municipios: {ex.Message}");
                throw;
            }

            return Municipios;
        }

        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
