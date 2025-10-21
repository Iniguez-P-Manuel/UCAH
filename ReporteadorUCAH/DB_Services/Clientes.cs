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
        public int Guardar(Cliente cliente)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    // Si el ID es 0, dejamos que SQLite auto-genere el ID
                    if (cliente.Id == 0)
                    {
                        command.CommandText = @"INSERT INTO Clientes 
                (Nombre, idTipoPersona, apellidoPaterno, apellidoMaterno, nombres, 
                 rfc, curp, paisNumero, codigoPostal, calle, noInterior, noExterior, 
                 idColonia, idCiudad, idMunicipio, idEstado, telefonos, 
                 correo, condicionPago, metodoPago, limiteCredito, moneda, 
                 creditoSuspendido, idGrupoFamiliar)
                VALUES 
                (@Nombre, @idTipoPersona, @apellidoPaterno, @apellidoMaterno, @nombres, 
                 @rfc, @curp, @paisNumero, @codigoPostal, @calle, @noInterior, @noExterior, 
                 @idColonia, @idCiudad, @idMunicipio, @idEstado, @telefonos, 
                 @correo, @condicionPago, @metodoPago, @limiteCredito, @moneda, 
                 @creditoSuspendido, @idGrupoFamiliar);
                SELECT last_insert_rowid();";
                    }
                    else
                    {
                        command.CommandText = @"INSERT INTO Clientes 
                (id, Nombre, idTipoPersona, apellidoPaterno, apellidoMaterno, nombres, 
                 rfc, curp, paisNumero, codigoPostal, calle, noInterior, noExterior, 
                 idColonia, idCiudad, idMunicipio, idEstado, telefonos, 
                 correo, condicionPago, metodoPago, limiteCredito, moneda, 
                 creditoSuspendido, idGrupoFamiliar)
                VALUES 
                (@id, @Nombre, @idTipoPersona, @apellidoPaterno, @apellidoMaterno, @nombres, 
                 @rfc, @curp, @paisNumero, @codigoPostal, @calle, @noInterior, @noExterior, 
                 @idColonia, @idCiudad, @idMunicipio, @idEstado, @telefonos, 
                 @correo, @condicionPago, @metodoPago, @limiteCredito, @moneda, 
                 @creditoSuspendido, @idGrupoFamiliar);
                SELECT last_insert_rowid();";
                    }

                    // Solo agregar el parámetro @id si no es 0
                    if (cliente.Id != 0)
                    {
                        command.Parameters.AddWithValue("@id", cliente.Id);
                    }

                    command.Parameters.AddWithValue("@Nombre", cliente.Nombre ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@idTipoPersona", cliente.TipoPersona?.Id ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@apellidoPaterno", cliente.ApellidoPaterno ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@apellidoMaterno", cliente.ApellidoMaterno ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nombres", cliente.Nombres ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@rfc", cliente.Rfc ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@curp", cliente.Curp ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@paisNumero", cliente.PaisNumero);
                    command.Parameters.AddWithValue("@codigoPostal", cliente.CodigoPostal ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@calle", cliente.Calle ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@noInterior", cliente.NoInterior ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@noExterior", cliente.NoExterior ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@idColonia", cliente._Colonia?.Id ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@idCiudad", cliente._Ciudad?.Id ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@idMunicipio", cliente._Municipio?.Id ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@idEstado", cliente._Estado?.Id ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@telefonos", cliente.Telefono ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@correo", cliente.Correo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@condicionPago", cliente.CondicionPago ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@metodoPago", cliente.MetodoPago ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@limiteCredito", cliente.LimiteCredito);
                    command.Parameters.AddWithValue("@moneda", cliente.Moneda);
                    command.Parameters.AddWithValue("@creditoSuspendido", cliente.CreditoSuspendido);
                    command.Parameters.AddWithValue("@idGrupoFamiliar", cliente._GrupoFamiliar?.Id ?? (object)DBNull.Value);

                    // Ejecutar y obtener el ID
                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al insertar cliente: {ex.Message}");
                throw;
            }
        }
        public int ActualizarCliente(Cliente cliente)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"UPDATE Clientes SET 
                Nombre = @Nombre,
                idTipoPersona = @idTipoPersona,
                apellidoPaterno = @apellidoPaterno,
                apellidoMaterno = @apellidoMaterno,
                nombres = @nombres,
                rfc = @rfc,
                curp = @curp,
                paisNumero = @paisNumero,
                codigoPostal = @codigoPostal,
                calle = @calle,
                noInterior = @noInterior,
                noExterior = @noExterior,
                idColonia = @idColonia,
                idCiudad = @idCiudad,
                idMunicipio = @idMunicipio,
                idEstado = @idEstado,
                telefonos = @telefonos,
                correo = @correo,
                condicionPago = @condicionPago,
                metodoPago = @metodoPago,
                limiteCredito = @limiteCredito,
                moneda = @moneda,
                creditoSuspendido = @creditoSuspendido,
                idGrupoFamiliar = @idGrupoFamiliar
                WHERE id = @id";

                    command.Parameters.AddWithValue("@id", cliente.Id);
                    command.Parameters.AddWithValue("@Nombre", cliente.Nombre ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@idTipoPersona", cliente.TipoPersona?.Id ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@apellidoPaterno", cliente.ApellidoPaterno ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@apellidoMaterno", cliente.ApellidoMaterno ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nombres", cliente.Nombres ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@rfc", cliente.Rfc ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@curp", cliente.Curp ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@paisNumero", cliente.PaisNumero);
                    command.Parameters.AddWithValue("@codigoPostal", cliente.CodigoPostal ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@calle", cliente.Calle ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@noInterior", cliente.NoInterior ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@noExterior", cliente.NoExterior ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@idColonia", cliente._Colonia?.Id ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@idCiudad", cliente._Ciudad?.Id ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@idMunicipio", cliente._Municipio?.Id ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@idEstado", cliente._Estado?.Id ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@telefonos", cliente.Telefono ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@correo", cliente.Correo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@condicionPago", cliente.CondicionPago ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@metodoPago", cliente.MetodoPago ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@limiteCredito", cliente.LimiteCredito);
                    command.Parameters.AddWithValue("@moneda", cliente.Moneda);
                    command.Parameters.AddWithValue("@creditoSuspendido", cliente.CreditoSuspendido);
                    command.Parameters.AddWithValue("@idGrupoFamiliar", cliente._GrupoFamiliar?.Id ?? (object)DBNull.Value);

                    return command.ExecuteNonQuery();

                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al actualizar cliente: {ex.Message}");
                return 0;
            }
        }
        public List<Cliente> BuscarClientes(string Busqueda)
        {
            var Clientes = new List<Cliente>();

            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Clientes " +
                                           "WHERE Nombre LIKE '%' || @Busqueda || '%' " +
                                            "LIMIT 100";
                    command.Parameters.AddWithValue("@Busqueda", Busqueda);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cliente = MapClasses.MapToCliente(reader);
                            Clientes.Add(cliente);
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener notas: {ex.Message}");
                throw;
            }

            return Clientes;
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
