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
                        COALESCE(f.UUID, lnc.FacturaUUID) as FacturaUUID,  -- Usar f.UUID si existe, sino lnc.FacturaUUID
    
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
                    LEFT JOIN Facturas f ON f.UUID = lnc.FacturaUUID
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
                            // DEBUG: Ver qué devuelve la consulta
                            int id = reader.GetInt32(0);
                            string fecha = reader.GetString(1);
                            string facturaUUID = reader.IsDBNull(6) ? "NULL" : reader.GetString(6);

                            Console.WriteLine($"ID: {id}, Fecha: {fecha}, FacturaUUID: {facturaUUID}");

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
        // Método mejorado para actualizar notas que usa la lógica manual cuando es necesario
        public int ActualizarNotaCargo(NotaCargo notaCargo)
        {
            // Primero intentar el método normal
            int resultado = ActualizarNotaCargoNormal(notaCargo);

            // Si falla, usar el método manual
            if (resultado == 0)
            {
                resultado = ActualizarNotaCargoManual(notaCargo) ? 1 : 0;
            }

            return resultado;
        }

        // Método normal de actualización (tu implementación actual)
        private int ActualizarNotaCargoNormal(NotaCargo notaCargo)
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

                    // 2. Actualizar la NotaCargo principal
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
                        command.Parameters.AddWithValue("@idCosecha", notaCargo._Cosecha?.Id ?? 0);
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

                    // 3. Manejar deducciones
                    ActualizarDeduccionesNota(notaCargo, conn);

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

        // Método centralizado para manejar deducciones
        private void ActualizarDeduccionesNota(NotaCargo notaCargo, SqliteConnection conn)
        {
            // Eliminar deducciones existentes
            using (var command = conn.CreateCommand())
            {
                command.CommandText = "DELETE FROM DeduccionesNota WHERE idNotaLiquidacion = @IdNota";
                command.Parameters.AddWithValue("@IdNota", notaCargo.Id);
                command.ExecuteNonQuery();
            }

            // Insertar nuevas deducciones si existen
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
        }

        // Método manual mejorado (basado en tu implementación en frmNotaCargo)
        private bool ActualizarNotaCargoManual(NotaCargo nota)
        {
            using (var conn = _dbConnection.GetConnection())
            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    // 1. ACTUALIZAR NOTA PRINCIPAL
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = @"
                            UPDATE LiquidacionNotasCargo 
                            SET FECHA = @Fecha,
                                idCliente = @idCliente,
                                idCultivo = @idCultivo,
                                idCosecha = @idCosecha,
                                TONS = @Tons,
                                PRECIO = @Precio,
                                IMPORTE = @Importe
                            WHERE id = @Id";

                        command.Parameters.AddWithValue("@Fecha", nota.Fecha);
                        command.Parameters.AddWithValue("@idCliente", nota._Cliente?.Id ?? 0);
                        command.Parameters.AddWithValue("@idCultivo", nota._Cultivo?.Id ?? 0);
                        command.Parameters.AddWithValue("@idCosecha", nota._Cosecha?.Id ?? 0);
                        command.Parameters.AddWithValue("@Tons", nota.Tons);
                        command.Parameters.AddWithValue("@Precio", nota.Precio);
                        command.Parameters.AddWithValue("@Importe", nota.Importe);
                        command.Parameters.AddWithValue("@Id", nota.Id);

                        int filasAfectadas = command.ExecuteNonQuery();

                        if (filasAfectadas == 0)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }

                    // 2. Manejar deducciones
                    ActualizarDeduccionesNota(nota, conn);

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error en actualización manual: {ex.Message}");
                    return false;
                }
            }
        }

        // Método para verificar el guardado en base de datos
        public bool VerificarGuardadoEnBaseDatos(int idNota)
        {
            try
            {
                using (var conn = _dbConnection.GetConnection())
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT COUNT(*) 
                        FROM LiquidacionNotasCargo 
                        WHERE id = @IdNota";

                    command.Parameters.AddWithValue("@IdNota", idNota);

                    var resultado = Convert.ToInt32(command.ExecuteScalar());
                    return resultado > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en verificación: {ex.Message}");
                return false;
            }
        }


        public void Dispose()
        {
            // El DatabaseConnection se maneja externamente
        }
    }
}
