namespace Ecommerce.Application.Services;

using Models;

public interface ICategoryService
{
    Task<bool> CreateAsync(Category category);
    Task<Category?> GetByIdAsync(Guid id);
    Task<Category?> GetBySlugAsync(string slug);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> UpdateAsync(Category category);
    Task<bool> DeleteAsync(Guid id);
}
