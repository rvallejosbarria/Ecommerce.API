namespace Ecommerce.Application.Database;

using System.Data;
using Npgsql;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}

public class NpgqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public NpgqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}