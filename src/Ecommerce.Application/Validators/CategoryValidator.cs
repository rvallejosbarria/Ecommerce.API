namespace Ecommerce.Application.Validators;

using FluentValidation;
using Models;
using Repositories;

public class CategoryValidator : AbstractValidator<Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Slug).MustAsync(ValidateSlug).WithMessage("This category already exists");
    }

    private async Task<bool> ValidateSlug(Category category, string slug, CancellationToken cancellationToken)
    {
        var existingCategory = await _categoryRepository.GetBySlugAsync(slug);

        if (existingCategory is not null)
        {
            return existingCategory.Id == category.Id;
        }
        return existingCategory is null;
    }
}
