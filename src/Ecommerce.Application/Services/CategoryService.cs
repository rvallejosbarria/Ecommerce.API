namespace Ecommerce.Application.Services;

using FluentValidation;
using Models;
using Repositories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<Category> _categoryValidator;

    public CategoryService(ICategoryRepository categoryRepository, IValidator<Category> categoryValidator)
    {
        _categoryRepository = categoryRepository;
        _categoryValidator = categoryValidator;
    }

    public async Task<bool> CreateAsync(Category category)
    {
        await _categoryValidator.ValidateAndThrowAsync(category);
        return await _categoryRepository.CreateAsync(category);
    }

    public Task<Category?> GetByIdAsync(Guid id) => _categoryRepository.GetByIdAsync(id);

    public Task<Category?> GetBySlugAsync(string slug) => _categoryRepository.GetBySlugAsync(slug);

    public Task<IEnumerable<Category>> GetAllAsync() => _categoryRepository.GetAllAsync();

    public async Task<Category?> UpdateAsync(Category category)
    {
        await _categoryValidator.ValidateAndThrowAsync(category);
        var categoryExists = await _categoryRepository.ExistsByIdAsync(category.Id);
        if (!categoryExists)
        {
            return null;
        }

        await _categoryRepository.UpdateAsync(category);
        return category;
    }

    public Task<bool> DeleteAsync(Guid id) => _categoryRepository.DeleteAsync(id);
}
