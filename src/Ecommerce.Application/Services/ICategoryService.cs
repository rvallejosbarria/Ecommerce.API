namespace Ecommerce.Application.Services;

using Models;

public interface ICategoryService
{
    Task<bool> CreateAsync(Category category, CancellationToken cancellationToken = default);
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Category?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Category?> UpdateAsync(Category category, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
