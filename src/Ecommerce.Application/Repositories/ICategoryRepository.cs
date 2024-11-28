namespace Ecommerce.Application.Repositories;

using Models;

public interface ICategoryRepository
{
    Task<bool> CreateAsync(Category category);
    Task<Category?> GetByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<bool> UpdateAsync(Category category);
    Task<bool> DeleteAsync(Guid id);
}
