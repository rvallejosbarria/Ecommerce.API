namespace Ecommerce.Application.Repositories;

using Dapper;
using Database;
using Models;

public class ProductRepository : IProductRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public ProductRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
        using var transaction = connection.BeginTransaction();
        var result = await connection.ExecuteAsync(new CommandDefinition("""
                                                                         insert into products (id, slug, name, description, price, stock, image)
                                                                         values (@Id, @Slug, @Name, @Description, @Price, @Stock, @Image)
                                                                         """, product,
            cancellationToken: cancellationToken));
        transaction.Commit();
        return result > 0;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
        var product = await connection.QuerySingleOrDefaultAsync<Product>(new CommandDefinition("""
            select * from products where id = @Id
            """, new { Id = id }, cancellationToken: cancellationToken));
        return product;
    }

    public async Task<Product?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
        var product = await connection.QuerySingleOrDefaultAsync<Product>(new CommandDefinition("""
            select * from products where slug = @Slug
            """, new { Slug = slug }, cancellationToken: cancellationToken));
        return product;
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
        var result = await connection.QueryAsync(new CommandDefinition("""
                                                                       select * from products
                                                                       """, cancellationToken: cancellationToken));
        return result.Select(c => new Product
        {
            Id = c.id,
            Name = c.name,
            Description = c.description,
            Price = c.price,
            Stock = c.stock,
            Image = c.image
        });
    }

    public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsync(new CommandDefinition("""
                                                                         update products set slug = @Slug, name = @Name, description = @Description, price = @Price, stock = @Stock, image = @Image
                                                                         where id = @Id
                                                                         """, product,
            cancellationToken: cancellationToken));
        transaction.Commit();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsync(new CommandDefinition("""
                                                                         delete from products where id = @Id
                                                                         """, new { Id = id },
            cancellationToken: cancellationToken));

        transaction.Commit();
        return result > 0;
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
                                                                               select count(1) from products where id = @Id))
                                                                               """, new { Id = id },
            cancellationToken: cancellationToken));
    }
}
