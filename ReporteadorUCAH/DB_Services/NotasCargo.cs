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
        public List<NotaCargo> BuscarNotas(string busqueda)
        {
            var notas = new List<NotaCargo>();

            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT 
                        -- Datos principales de NotaCargo
                        lnc.id,
                        lnc.FECHA,
                        lnc.FacturaFolio,
                        lnc.TONS,
                        lnc.PRECIO,
                        lnc.IMPORTE,
                        lnc.FacturaUUID,
    
                        -- Datos del Cliente
                        c.id as ClienteId,
                        c.Nombre as ClienteNombre,
                        c.idTipoPersona,
                        c.apellidoPaterno,
                        c.apellidoMaterno,
                        c.nombres,
                        c.rfc as ClienteRfc,
                        c.curp,
                        c.paisNumero,
                        c.codigoPostal,
                        c.calle,
                        c.noInterior,
                        c.noExterior,
                        c.idColonia,
                        c.idCiudad,
                        c.idMunicipio,
                        c.idEstado,
                        c.telefonos as Telefono,
                        c.correo,
                        c.condicionPago,
                        c.metodoPago,
                        c.limiteCredito,
                        c.moneda,
                        c.creditoSuspendido,
                        c.idGrupoFamiliar,
    
                        -- Tipo Persona
                        tp.Nombre as TipoPersonaNombre,
                        tp.NombreCorto as TipoPersonaNombreCorto,
    
                        -- Dirección del Cliente
                        col.Nombre as ColoniaNombre,
                        ciu.Nombre as CiudadNombre,
                        mun.Nombre as MunicipioNombre,
                        est.Nombre as EstadoNombre,
                        est.estadoNumero,
    
                        -- Cultivo
                        cult.id as CultivoId,
                        cult.Nombre as CultivoNombre,
                        cult.Cultivo as CultivoTipo,
                        cult.CONS,
    
                        -- Cosecha
                        cos.id as CosechaId,
                        cos.fechaInicial,
                        cos.fechaFinal,
    
                        -- Grupo Familiar
                        gf.id as GrupoFamiliarId,
                        gf.nombre as GrupoFamiliarNombre

                    FROM LiquidacionNotasCargo lnc

                    -- Joins para Cliente y su información completa
                    INNER JOIN Clientes c ON c.id = lnc.idCliente
                    LEFT JOIN TipoPersona tp ON tp.id = c.idTipoPersona
                    LEFT JOIN Colonia col ON col.id = c.idColonia
                    LEFT JOIN Ciudades ciu ON ciu.id = c.idCiudad
                    LEFT JOIN Municipios mun ON mun.id = c.idMunicipio
                    LEFT JOIN Estados est ON est.id = c.idEstado
                    LEFT JOIN GrupoFamiliar gf ON gf.id = c.idGrupoFamiliar  -- ¡CORREGIDO!

                    -- Joins para otras entidades
                    LEFT JOIN Cultivos cult ON cult.id = lnc.idCultivo
                    LEFT JOIN Cosecha cos ON cos.id = lnc.idCosecha

                    WHERE c.Nombre LIKE '%' || @Busqueda || '%'
                    LIMIT 100";

                    command.Parameters.AddWithValue("@Busqueda", busqueda);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nota = MapClasses.MapToNotaCargoConJoins(reader);
                            notas.Add(nota);
                        }
                    }

                    // Cargar deducciones por lote para todas las notas
                    if (notas.Any())
                    {
                        CargarDeduccionesPorLote(notas, conn);
                    }
                }
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener notas: {ex.Message}");
                throw;
            }

            return notas;
        }
        private void CargarDeduccionesPorLote(List<NotaCargo> notas, SqliteConnection conn)
        {
            if (!notas.Any()) return;

            var idsNotas = string.Join(",", notas.Select(n => n.Id));

            using (var command = conn.CreateCommand())
            {
                command.CommandText = string.Format(@"
            SELECT 
                dn.id,
                dn.idNotaLiquidacion,
                dn.Importe,
                td.id as TipoDeduccionId,
                td.Nombre as TipoDeduccionNombre,
                gd.id as GrupoDeduccionId,
                gd.Nombre as GrupoDeduccionNombre
            FROM DeduccionesNota dn
            INNER JOIN TipoDeducciones td ON td.id = dn.idDeduccion
            LEFT JOIN GrupoDeducciones gd ON gd.id = td.idGrupo
            WHERE dn.idNotaLiquidacion IN ({0})", idsNotas);

                using (var reader = command.ExecuteReader())
                {
                    var deduccionesPorNota = new Dictionary<int, List<DeduccionNota>>();

                    while (reader.Read())
                    {
                        var idNota = reader.GetInt32(reader.GetOrdinal("idNotaLiquidacion"));
                        var deduccion = new DeduccionNota
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Importe = reader.GetDouble(reader.GetOrdinal("Importe")),
                            _Deduccion = new TipoDeduccion
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("TipoDeduccionId")),
                                Nombre = MapClasses.GetStringOrNull(reader, "TipoDeduccionNombre"),
                                _Grupo = new GrupoDeducciones
                                {
                                    Id = MapClasses.GetInt32OrNull(reader, "GrupoDeduccionId"),
                                    Nombre = MapClasses.GetStringOrNull(reader, "GrupoDeduccionNombre")
                                }
                            }
                        };

                        if (!deduccionesPorNota.ContainsKey(idNota))
                            deduccionesPorNota[idNota] = new List<DeduccionNota>();

                        deduccionesPorNota[idNota].Add(deduccion);
                    }

                    // Asignar deducciones a cada nota
                    foreach (var nota in notas)
                    {
                        if (deduccionesPorNota.ContainsKey(nota.Id))
                        {
                            nota.Deducciones = deduccionesPorNota[nota.Id];
                        }
                    }
                }
            }
        }

        public List<NotaCargo> BuscarNotas_old(string Busqueda)
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
                    // Usar la misma consulta completa que en BuscarNotas
                    command.CommandText = @"
                SELECT 
                    -- Todos los campos que tienes en BuscarNotas
                    lnc.id, lnc.FECHA, lnc.FacturaFolio, lnc.TONS, lnc.PRECIO, 
                    lnc.IMPORTE, lnc.FacturaUUID,
                    -- Campos de cliente, cultivo, cosecha, etc.
                    c.id as ClienteId, c.Nombre as ClienteNombre,
                    cult.id as CultivoId, cult.Nombre as CultivoNombre,
                    cos.id as CosechaId, cos.fechaInicial, cos.fechaFinal
                FROM LiquidacionNotasCargo lnc
                INNER JOIN Clientes c ON c.id = lnc.idCliente
                LEFT JOIN Cultivos cult ON cult.id = lnc.idCultivo
                LEFT JOIN Cosecha cos ON cos.id = lnc.idCosecha
                WHERE lnc.id = @Id";

                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var nota = MapClasses.MapToNotaCargoConJoins(reader);

                            // Cargar deducciones
                            CargarDeduccionesParaNota(nota, conn);

                            return nota;
                        }
                    }
                }
                return null;
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Error al obtener nota de cargo por ID: {ex.Message}");
                throw;
            }
        }

        private void CargarDeduccionesParaNota(NotaCargo nota, SqliteConnection conn)
        {
            using (var command = conn.CreateCommand())
            {
                command.CommandText = @"
            SELECT dn.id, dn.idNotaLiquidacion, dn.Importe,
                   td.id as TipoDeduccionId, td.Nombre as TipoDeduccionNombre,
                   gd.id as GrupoDeduccionId, gd.Nombre as GrupoDeduccionNombre
            FROM DeduccionesNota dn
            INNER JOIN TipoDeducciones td ON td.id = dn.idDeduccion
            LEFT JOIN GrupoDeducciones gd ON gd.id = td.idGrupo
            WHERE dn.idNotaLiquidacion = @IdNota";

                command.Parameters.AddWithValue("@IdNota", nota.Id);

                using (var reader = command.ExecuteReader())
                {
                    var deducciones = new List<DeduccionNota>();
                    while (reader.Read())
                    {
                        var deduccion = new DeduccionNota
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Importe = reader.GetDouble(reader.GetOrdinal("Importe")),
                            _Deduccion = new TipoDeduccion
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("TipoDeduccionId")),
                                Nombre = MapClasses.GetStringOrNull(reader, "TipoDeduccionNombre"),
                                _Grupo = new GrupoDeducciones
                                {
                                    Id = MapClasses.GetInt32OrNull(reader, "GrupoDeduccionId"),
                                    Nombre = MapClasses.GetStringOrNull(reader, "GrupoDeduccionNombre")
                                }
                            }
                        };
                        deducciones.Add(deduccion);
                    }
                    nota.Deducciones = deducciones;
                }
            }
        }

        public int AgregarNotaCargo(NotaCargo notaCargo)
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
                        (FECHA, FacturaFolio, idCliente, idCultivo, idCosecha, TONS, PRECIO, IMPORTE, FacturaUUID) 
                        VALUES (@Fecha, @FacturaFolio, @idCliente, @idCultivo, @idCosecha, @Tons, @Precio, @Importe, @FacturaUUID);
                        SELECT last_insert_rowid();";

                        command.Parameters.AddWithValue("@Fecha", notaCargo.Fecha);
                        command.Parameters.AddWithValue("@FacturaFolio", notaCargo.FacturaFolio ?? "");
                        command.Parameters.AddWithValue("@idCliente", notaCargo._Cliente?.Id ?? 0);
                        command.Parameters.AddWithValue("@idCultivo", notaCargo._Cultivo?.Id ?? 0);
                        command.Parameters.AddWithValue("@idCosecha", notaCargo._Cosecha?.Id ?? 0);
                        command.Parameters.AddWithValue("@Tons", notaCargo.Tons);
                        command.Parameters.AddWithValue("@Precio", notaCargo.Precio);
                        command.Parameters.AddWithValue("@Importe", notaCargo.Importe);
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
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error al agregar nota de cargo: {ex.Message}");
                    return 0;
                }
            }
        }
        public int ActualizarNotaCargo(NotaCargo notaCargo)
        {

            using (var conn = _dbConnection.GetConnection())
            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    int filasAfectadas = 0;

                    // 1. Actualizar la cosecha si existe
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

                    // 2. Actualizar la NotaCargo principal - CORREGIDO
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = @"
                        UPDATE LiquidacionNotasCargo 
                        SET FECHA = @Fecha, 
                            FacturaFolio = @FacturaFolio, 
                            idCliente = @idCliente, 
                            idCultivo = @idCultivo, 
                            idCosecha = @idCosecha, 
                            TONS = @Tons, 
                            PRECIO = @Precio, 
                            IMPORTE = @Importe, 
                            FacturaUUID = @FacturaUUID
                        WHERE id = @Id";

                        command.Parameters.AddWithValue("@Fecha", notaCargo.Fecha);
                        command.Parameters.AddWithValue("@FacturaFolio", notaCargo.FacturaFolio ?? "");
                        command.Parameters.AddWithValue("@idCliente", notaCargo._Cliente?.Id ?? 0);
                        command.Parameters.AddWithValue("@idCultivo", notaCargo._Cultivo?.Id ?? 0);

                        // **CRÍTICO: Asegurar que idCosecha no sea 0**
                        int idCosecha = notaCargo._Cosecha?.Id ?? 0;
                        command.Parameters.AddWithValue("@idCosecha", idCosecha);

                        command.Parameters.AddWithValue("@Tons", notaCargo.Tons);
                        command.Parameters.AddWithValue("@Precio", notaCargo.Precio);
                        command.Parameters.AddWithValue("@Importe", notaCargo.Importe);
                        command.Parameters.AddWithValue("@FacturaUUID", notaCargo.FacturaUUID ?? "");
                        command.Parameters.AddWithValue("@Id", notaCargo.Id);

                        filasAfectadas = command.ExecuteNonQuery();
                    }

                    // Si no se encontró la nota de cargo, retornar 0
                    if (filasAfectadas == 0)
                    {
                        transaction.Rollback();
                        return 0;
                    }

                    // 3. Eliminar las deducciones existentes
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM DeduccionesNota WHERE idNotaLiquidacion = @Id";
                        command.Parameters.AddWithValue("@Id", notaCargo.Id);
                        command.ExecuteNonQuery();
                    }

                    // 4. Insertar las nuevas deducciones si existen
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
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error en transacción de ActualizarNotaCargo: {ex.Message}");
                    return 0;
                }
            }
        }


        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
