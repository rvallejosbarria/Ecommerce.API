namespace Ecommerce.Application.Database;

using Dapper;

public class DbInitializer
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public DbInitializer(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync("""
                                      CREATE TABLE IF NOT EXISTS categories (
                                          id UUID PRIMARY KEY,
                                          slug TEXT NOT NULL,
                                          name TEXT NOT NULL,
                                          description TEXT NOT NULL
                                      );
                                      """);

        await connection.ExecuteAsync("""
                                      CREATE UNIQUE INDEX CONCURRENTLY IF NOT EXISTS categories_slug_idx
                                      ON categories USING btree(slug);
                                      """);
    }
}
