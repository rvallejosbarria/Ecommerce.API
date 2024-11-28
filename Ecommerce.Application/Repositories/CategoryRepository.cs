namespace Ecommerce.Application.Repositories;

using Models;

public class CategoryRepository : ICategoryRepository
{
    private readonly List<Category> _categories = new List<Category>();

    public Task<bool> CreateAsync(Category category)
    {
        _categories.Add(category);
        return Task.FromResult(true);
    }

    public Task<Category?> GetByIdAsync(Guid id)
    {
        var category = _categories.SingleOrDefault(c => c.Id == id);
        return Task.FromResult(category);
    }

    public Task<IEnumerable<Category>> GetAllAsync()
    {
        return Task.FromResult(_categories.AsEnumerable());
    }

    public Task<bool> UpdateAsync(Category category)
    {
        var categoryIndex = _categories.FindIndex(c => c.Id == category.Id);
        if (categoryIndex == -1)
        {
            return Task.FromResult(false);
        }
        _categories[categoryIndex] = category;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var removedCount = _categories.RemoveAll(c => c.Id == id);
        var movieRemoved = removedCount > 0;
        return Task.FromResult(movieRemoved);
    }
}
