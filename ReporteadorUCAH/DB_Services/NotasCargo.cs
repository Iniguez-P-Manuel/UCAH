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

        public List<NotaCargo> BuscarNotas(string Busqueda)
        {
            var Notas = new List<NotaCargo>();

            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT LiquidacionNotasCargo.* FROM LiquidacionNotasCargo " +
                                           "INNER JOIN Clientes ON Clientes.id = LiquidacionNotasCargo.idCliente " +
                                           "WHERE Clientes.Nombre LIKE '%' || @Busqueda || '%' " +
                                            "LIMIT 100";
                    command.Parameters.AddWithValue("@Busqueda", Busqueda);

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

        public int AgregarNotaCargo(NotaCargo notaCargo)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int notaCargoId;

                        // Insertar la NotaCargo principal
                        using (var command = conn.CreateCommand())
                        {
                            command.CommandText = @"
                        INSERT INTO LiquidacionNotasCargo 
                        (FECHA, FacturaFolio, idCliente, idCultivo, idCosecha, idGrupoFamiliar, TONS, PRECIO, IMPORTE, CFDI, FacturaUUID) 
                        VALUES (@Fecha, @FacturaFolio, @idCliente, @idCultivo, @idCosecha, @idGrupoFamiliar, @Tons, @Precio, @Importe, @CFDI, @FacturaUUID);
                        SELECT last_insert_rowid();";

                            command.Parameters.AddWithValue("@Fecha", notaCargo.Fecha);
                            command.Parameters.AddWithValue("@FacturaFolio", notaCargo.FacturaFolio ?? "");
                            command.Parameters.AddWithValue("@idCliente", notaCargo._Cliente?.Id ?? 0);
                            command.Parameters.AddWithValue("@idCultivo", notaCargo._Cultivo?.Id ?? 0);
                            command.Parameters.AddWithValue("@idCosecha", notaCargo._Cosecha?.Id ?? 0);
                            command.Parameters.AddWithValue("@idGrupoFamiliar", notaCargo._GrupoFamiliar?.Id ?? 0);
                            command.Parameters.AddWithValue("@Tons", notaCargo.Tons);
                            command.Parameters.AddWithValue("@Precio", notaCargo.Precio);
                            command.Parameters.AddWithValue("@Importe", notaCargo.Importe);
                            command.Parameters.AddWithValue("@CFDI", ""); // Si no tienes este dato en el objeto
                            command.Parameters.AddWithValue("@FacturaUUID", notaCargo.FacturaUUID ?? "");

                            notaCargoId = Convert.ToInt32(command.ExecuteScalar());
                        }

                        // Insertar las deducciones si existen
                        if (notaCargo.Deducciones != null && notaCargo.Deducciones.Any())
                        {
                            foreach (var deduccion in notaCargo.Deducciones)
                            {
                                using (var command = conn.CreateCommand())
                                {
                                    command.CommandText = @"
                                INSERT INTO DeduccionesNota 
                                (idNotaLiquidacion, idDeduccion, Importe) 
                                VALUES (@idNotaLiquidacion, @idDeduccion, @Importe)";

                                    command.Parameters.AddWithValue("@idNotaLiquidacion", notaCargoId);
                                    command.Parameters.AddWithValue("@idDeduccion", deduccion._Deduccion?.Id ?? 0);
                                    command.Parameters.AddWithValue("@Importe", deduccion.Importe);

                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                        return notaCargoId;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al agregar nota de cargo: {ex.Message}");
                return 0;
            }
        }
        public int ActualizarNotaCargo(NotaCargo notaCargo)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int filasAfectadas;

                        // Actualizar la cosecha si existe
                        if (notaCargo._Cosecha != null && notaCargo._Cosecha.Id > 0)
                        {
                            using (var command = conn.CreateCommand())
                            {
                                command.CommandText = @"
                            UPDATE Cosecha 
                            SET fechaInicial = @fechaInicial, 
                                fechaFinal = @fechaFinal 
                            WHERE id = @Id";

                                command.Parameters.AddWithValue("@fechaInicial", notaCargo._Cosecha.FechaInicial);
                                command.Parameters.AddWithValue("@fechaFinal", notaCargo._Cosecha.FechaFinal);
                                command.Parameters.AddWithValue("@Id", notaCargo._Cosecha.Id);

                                command.ExecuteNonQuery();
                            }
                        }

                        // Actualizar la NotaCargo principal
                        using (var command = conn.CreateCommand())
                        {
                            command.CommandText = @"
                        UPDATE LiquidacionNotasCargo 
                        SET FECHA = @Fecha, 
                            FacturaFolio = @FacturaFolio, 
                            idCliente = @idCliente, 
                            idCultivo = @idCultivo, 
                            idCosecha = @idCosecha, 
                            idGrupoFamiliar = @idGrupoFamiliar, 
                            TONS = @Tons, 
                            PRECIO = @Precio, 
                            IMPORTE = @Importe, 
                            CFDI = @CFDI, 
                            FacturaUUID = @FacturaUUID
                        WHERE id = @Id";

                            command.Parameters.AddWithValue("@Fecha", notaCargo.Fecha);
                            command.Parameters.AddWithValue("@FacturaFolio", notaCargo.FacturaFolio ?? "");
                            command.Parameters.AddWithValue("@idCliente", notaCargo._Cliente?.Id ?? 0);
                            command.Parameters.AddWithValue("@idCultivo", notaCargo._Cultivo?.Id ?? 0);
                            command.Parameters.AddWithValue("@idCosecha", notaCargo._Cosecha?.Id ?? 0);
                            command.Parameters.AddWithValue("@idGrupoFamiliar", notaCargo._GrupoFamiliar?.Id ?? 0);
                            command.Parameters.AddWithValue("@Tons", notaCargo.Tons);
                            command.Parameters.AddWithValue("@Precio", notaCargo.Precio);
                            command.Parameters.AddWithValue("@Importe", notaCargo.Importe);
                            command.Parameters.AddWithValue("@CFDI", "");
                            command.Parameters.AddWithValue("@FacturaUUID", notaCargo.FacturaUUID ?? "");
                            command.Parameters.AddWithValue("@Id", notaCargo.Id);

                            filasAfectadas = command.ExecuteNonQuery();
                        }

                        // Si no se encontró la nota de cargo, retornar 0
                        if (filasAfectadas == 0)
                        {
                            transaction.Commit();
                            return 0;
                        }

                        // Eliminar las deducciones existentes
                        using (var command = conn.CreateCommand())
                        {
                            command.CommandText = "DELETE FROM DeduccionesNota WHERE idNotaLiquidacion = @Id";
                            command.Parameters.AddWithValue("@Id", notaCargo.Id);
                            command.ExecuteNonQuery();
                        }

                        // Insertar las nuevas deducciones si existen
                        if (notaCargo.Deducciones != null && notaCargo.Deducciones.Any())
                        {
                            foreach (var deduccion in notaCargo.Deducciones)
                            {
                                using (var command = conn.CreateCommand())
                                {
                                    command.CommandText = @"
                                INSERT INTO DeduccionesNota 
                                (idNotaLiquidacion, idDeduccion, Importe) 
                                VALUES (@idNotaLiquidacion, @idDeduccion, @Importe)";

                                    command.Parameters.AddWithValue("@idNotaLiquidacion", notaCargo.Id);
                                    command.Parameters.AddWithValue("@idDeduccion", deduccion._Deduccion?.Id ?? 0);
                                    command.Parameters.AddWithValue("@Importe", deduccion.Importe);

                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                        return filasAfectadas;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al actualizar nota de cargo: {ex.Message}");
                return 0;
            }
        }


        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
