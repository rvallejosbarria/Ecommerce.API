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

    public async Task<bool> CreateAsync(Category category, CancellationToken cancellationToken = default)
    {
        await _categoryValidator.ValidateAndThrowAsync(category, cancellationToken);
        return await _categoryRepository.CreateAsync(category, cancellationToken);
    }

    public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _categoryRepository.GetByIdAsync(id, cancellationToken);

    public Task<Category?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default) =>
        _categoryRepository.GetBySlugAsync(slug, cancellationToken);

    public Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _categoryRepository.GetAllAsync(cancellationToken);

    public async Task<Category?> UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        await _categoryValidator.ValidateAndThrowAsync(category, cancellationToken);
        var categoryExists = await _categoryRepository.ExistsByIdAsync(category.Id, cancellationToken);
        if (!categoryExists)
        {
            return null;
        }

        await _categoryRepository.UpdateAsync(category, cancellationToken);
        return category;
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        _categoryRepository.DeleteAsync(id, cancellationToken);
}
