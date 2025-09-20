using Microsoft.Data.Sqlite;
using System;
using System.IO;
using SQLitePCL;

public class DatabaseConnection : IDisposable
{
    private SqliteConnection _connection;
    private readonly string _dbPath;
    private readonly string _connectionString;
    private static bool _isInitialized = false;

   
    public DatabaseConnection(string dbFileName = "UCAH.db")
    {
        // Inicializar SQLitePCL solo una vez
        if (!_isInitialized)
        {
            InitializeSQLite();
            _isInitialized = true;
        }

        _dbPath = Path.Combine(Directory.GetCurrentDirectory(), dbFileName);
        _connectionString = $"Data Source={_dbPath};";
    }

    private void InitializeSQLite()
    {
        try
        {
            // Inicializar el proveedor de SQLite
            Batteries.Init();
            Console.WriteLine("✅ SQLitePCL inicializado correctamente");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al inicializar SQLitePCL: {ex.Message}");
            throw;
        }
    }

    public string DatabasePath => _dbPath;

    public bool DatabaseExists()
    {
        return File.Exists(_dbPath);
    }

    public long GetDatabaseSize()
    {
        if (!DatabaseExists()) return 0;

        var fileInfo = new FileInfo(_dbPath);
        return fileInfo.Length;
    }

    public SqliteConnection GetConnection()
    {
        if (_connection?.State == System.Data.ConnectionState.Open)
            return _connection;

        _connection = new SqliteConnection(_connectionString);
        _connection.Open();

        return _connection;
    }

    public bool TestConnection()
    {
        try
        {
            using (SqliteConnection conn = GetConnection())
            {
                if (GetDatabaseSize() < 1)
                {
                    MessageBox.Show("Base de datos invalida: " + _dbPath);
                    return false;
                }
                MessageBox.Show("Conexión exitosa a la base de datos" +
                    $"\nUbicación: {_dbPath}" +
                    $"\nTamaño: {GetDatabaseSize()} bytes");
                return true;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error de conexión: {ex.Message}");
            return false;
        }
    }

    public bool TableExists(string tableName)
    {
        try
        {
            using (var conn = GetConnection())
            using (var command = conn.CreateCommand())
            {
                command.CommandText = @"
                    SELECT COUNT(*) 
                    FROM sqlite_master 
                    WHERE type = 'table' AND name = @tableName";

                command.Parameters.AddWithValue("@tableName", tableName);

                var result = Convert.ToInt32(command.ExecuteScalar());
                return result > 0;
            }
        }
        catch
        {
            return false;
        }
    }

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
        GC.SuppressFinalize(this);
    }
}