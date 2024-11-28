namespace Ecommerce.Application.Repositories;

using Dapper;
using Database;
using Models;

public class CategoryRepository : ICategoryRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public CategoryRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> CreateAsync(Category category, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
        using var transaction = connection.BeginTransaction();
        var result = await connection.ExecuteAsync(new CommandDefinition("""
                                                                         insert into categories (id, slug, name, description)
                                                                         values (@Id, @Slug, @Name, @Description)
                                                                         """, category, cancellationToken: cancellationToken));
        transaction.Commit();
        return result > 0;
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
        var category = await connection.QuerySingleOrDefaultAsync<Category>(new CommandDefinition("""
            select * from categories where id = @Id
            """, new { Id = id }, cancellationToken: cancellationToken));
        return category;
    }

    public async Task<Category?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
        var category = await connection.QuerySingleOrDefaultAsync<Category>(new CommandDefinition("""
            select * from categories where slug = @Slug
            """, new { Slug = slug }, cancellationToken: cancellationToken));
        return category;
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
        var result = await connection.QueryAsync(new CommandDefinition("""
                                                                       select * from categories
                                                                       """, cancellationToken: cancellationToken));
        return result.Select(c => new Category { Id = c.id, Name = c.name, Description = c.description });
    }

    public async Task<bool> UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsync(new CommandDefinition("""
                                                                         update categories set slug = @Slug, name = @Name, description = @Description
                                                                         where id = @Id
                                                                         """, category, cancellationToken: cancellationToken));
        transaction.Commit();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsync(new CommandDefinition("""
                                                                         delete from categories where id = @Id
                                                                         """, new { Id = id }, cancellationToken: cancellationToken));

        transaction.Commit();
        return result > 0;
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
                                                                               select count(1) from categories where id = @Id))
                                                                               """, new { Id = id }, cancellationToken: cancellationToken));
    }
}
