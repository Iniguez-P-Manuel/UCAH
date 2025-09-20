using Microsoft.Data.Sqlite;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteadorUCAH.DB_Services
{
    internal class DeduccionesNota : IDisposable
    {
        private readonly DatabaseConnection _dbConnection;
        public DeduccionesNota(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Modelos.DeduccionNota> GetDeduccionesByNota(int id)
        {
            var Deducciones = new List<Modelos.DeduccionNota>();

            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM DeduccionesNota WHERE idNotaLiquidacion = @Id ";
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Deduccion = MapClasses.MapToDeduccionNota(reader);
                            Deducciones.Add(Deduccion);
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener notas: {ex.Message}");
                throw;
            }

            return Deducciones;
        }



        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
